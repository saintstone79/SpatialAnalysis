using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Features2D;
using Emgu.CV.Flann;
using Emgu.CV.Structure;
using System.Diagnostics;
using System.Threading;

namespace SpatialAnalyzer
{
    public partial class FormSecondVideoPlayer : Form
    {
        private VideoCapture m_VideoCapture;
        private Mat m_currentDisplayImage = null;
        private HashSet<FormSecondVideoPlayer> m_hsOtherVideoPlayers;
        private double m_dVideoDuration = 0;
        private int m_nInterval = 0;
        private double m_dTotalFrame = 0;

        private bool IsPlaying = false;
        // Thread operators 
        bool _isThreadWorking = false;
        bool IsThreadWorking
        {
            get { lock (this) { return _isThreadWorking; } }
            set { lock (this) { _isThreadWorking = value; } }
        }

        System.Threading.Timer m_timerProcessFrame = null;

        public FormSecondVideoPlayer(HashSet<FormSecondVideoPlayer> _hsOtherVideoPlayer)
        {
            InitializeComponent();
            m_hsOtherVideoPlayers = _hsOtherVideoPlayer;
        }

        public delegate void DisplayImageDelegate(Image<Bgr, Byte> img);
        private void DisplayImage(Image<Bgr, Byte> img)
        {
            if (imageViewer.InvokeRequired)
            {
                DisplayImageDelegate DID = new DisplayImageDelegate(DisplayImage);
                imageViewer.BeginInvoke(DID, new object[] { img });
            }
            else
            {
                if (imageViewer.Width > 0 && imageViewer.Height > 0)
                    imageViewer.Image = img;
            }
        }

        public delegate void PlayingTimeUpdateDelegate(string strTimeLog);
        private void PlayingTimeUpdate(string strTimeLog)
        {
            if (lbCurrentTimeStamp.InvokeRequired)
            {
                PlayingTimeUpdateDelegate PTU = new PlayingTimeUpdateDelegate(PlayingTimeUpdate);
                lbCurrentTimeStamp.BeginInvoke(PTU, new object[] { strTimeLog });
            }
            else
            {
                lbCurrentTimeStamp.Text = strTimeLog;
            }
        }

        public delegate void NotifyExitPlayThreadDelegate();
        private void NotifyExitPlayThread()
        {
            if (this.InvokeRequired == true)
            {
                NotifyExitPlayThreadDelegate eh = new NotifyExitPlayThreadDelegate(NotifyExitPlayThread);
                this.BeginInvoke(eh, new object[] { });
            }
            else
            {                
                if (m_bClosing == true)
                    Close();
            }
        }

        private bool OpenVideoFile()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Window Video (*.avi;*.mp4;*.mov)|*.avi;*.mp4;*.mov";
            openFileDialog1.Title = "Select a video file";

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                m_VideoCapture = new VideoCapture(openFileDialog1.FileName);
                this.Text = openFileDialog1.FileName;
                if (m_VideoCapture != null)
                    return true;
                else
                    MessageBox.Show("Failed to load video file");
            }
            return false;
        }

        private void FormSecondVideoPlayer_Load(object sender, EventArgs e)
        {
            if (OpenVideoFile() == false)
            {
                Close();
                return;
            }
                

            m_dVideoDuration = m_VideoCapture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.FrameCount) / m_VideoCapture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.Fps);
            lbTotalDuration.Text = TimeSpan.FromSeconds(m_dVideoDuration).ToString(@"hh\:mm\:ss");
            lbCurrentTimeStamp.Text = TimeSpan.FromMilliseconds(0).ToString(@"hh\:mm\:ss");

            buttonNext10.Enabled = true;
            buttonNext30.Enabled = true;
            buttonPrev10.Enabled = true;
            buttonPrev30.Enabled = true;
            buttonPlay.Enabled = true;

            double dFps = m_VideoCapture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.Fps);
            m_dTotalFrame = m_VideoCapture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.FrameCount);
            m_nInterval = (int)(1000.0 / dFps / 1.5); // play 1.5x speed
            Trace.WriteLine("dFps: " + dFps.ToString() + "; Interval: " + ((int)(1000.0 / dFps)).ToString());
            ProcessFrame(null);
            m_hsOtherVideoPlayers.Add(this);
        }

        public void Forward(int nSeconds)
        {
            StopVideo();
            double dCurrent = m_VideoCapture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.PosMsec);
            if ((dCurrent + nSeconds) > (m_dVideoDuration * 1000))
                m_VideoCapture.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.PosMsec, (m_dVideoDuration - 1) * 1000);
            else
                m_VideoCapture.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.PosMsec, dCurrent + nSeconds);
            ShowVideoFrame();
        }

        public void Prev(int nSeconds)
        {
            StopVideo();
            double dCurrent = m_VideoCapture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.PosMsec);
            if (dCurrent - nSeconds < 0)
                m_VideoCapture.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.PosMsec, 0);
            else
                m_VideoCapture.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.PosMsec, dCurrent - nSeconds);
            ShowVideoFrame();
        }

        private delegate void UpdatePlayButtonDelegate(bool bPlaying);
        private void UpdatePlayButton(bool bPlaying)
        {
            if (buttonPlay.InvokeRequired)
            {
                UpdatePlayButtonDelegate UPB = new UpdatePlayButtonDelegate(UpdatePlayButton);
                buttonPlay.BeginInvoke(UPB, new object[] { bPlaying });
            }
            else
            {
                if (bPlaying)
                {
                    buttonPlay.Text = "||";
                    Trace.WriteLine("Button pause");
                }
                else
                {
                    buttonPlay.Text = ">";
                    Trace.WriteLine("Button play");
                }                    
            }
        }

        private void ProcessFrame(Object obj)
        {
            IsThreadWorking = true;
            if (m_VideoCapture != null)
            {
                try
                {
                    Mat imgeOrigenal = m_VideoCapture.QueryFrame();
                    if (imgeOrigenal != null)
                    {
                        Mat imNewSized = new Mat();
                        if (imageViewer.Width > 0 && imageViewer.Height > 0)
                            CvInvoke.Resize(imgeOrigenal, imNewSized, new Size(imageViewer.Width, imageViewer.Height), 1);
                        else
                            imNewSized = imgeOrigenal;
                        DisplayImage(imNewSized.ToImage<Bgr, Byte>());
                        m_currentDisplayImage = imNewSized;
                        double dCurrent = m_VideoCapture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.PosMsec);
                        PlayingTimeUpdate(TimeSpan.FromMilliseconds(dCurrent).ToString(@"hh\:mm\:ss"));
                        double dFrameNo = m_VideoCapture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.PosFrames);
                        if (dFrameNo == m_dTotalFrame)
                        {
                            StopVideo();
                            return;
                        }
                    }
                }
                catch (Exception)
                {
                    Trace.WriteLine("Exception-end of frame");
                }
            }
            IsThreadWorking = false;
            NotifyExitPlayThread();
        }

        private void PlayVideo()
        {
            if (m_VideoCapture != null)
            {
                if (m_timerProcessFrame != null)
                    m_timerProcessFrame.Dispose();
                m_timerProcessFrame = new System.Threading.Timer(ProcessFrame, null, 0, m_nInterval);
                IsPlaying = true;
                UpdatePlayButton(true);
            }
        }

        private void StopVideo()
        {
            if (m_timerProcessFrame != null)
            {
                m_timerProcessFrame.Dispose();
                m_timerProcessFrame = null;
            }

            IsPlaying = false;
            UpdatePlayButton(false);
        }

        private void ShowVideoFrame()
        {
            if (m_VideoCapture == null)
                return;

            double dCurrent = m_VideoCapture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.PosMsec);
            lbCurrentTimeStamp.Text = TimeSpan.FromMilliseconds(dCurrent).ToString(@"hh\:mm\:ss");

            Image<Bgr, Byte> imgeOrigenal = m_VideoCapture.QueryFrame().ToImage<Bgr, Byte>();
            if (imgeOrigenal == null)
                return;

            Mat imNewSized = new Mat();
            CvInvoke.Resize(imgeOrigenal, imNewSized, new Size(imageViewer.Width, imageViewer.Height), 1);
            imageViewer.Image = imNewSized;
            m_currentDisplayImage = imNewSized;
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            if (IsPlaying) // now playing, pause button clicked                      
                StopVideo();
            else // now paused, play button clicked             
                PlayVideo();

        }

        private void buttonPrev30_Click(object sender, EventArgs e)
        {
            bool bPrevPlaying = IsPlaying;
            StopVideo();
            double dCurrent = m_VideoCapture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.PosMsec);
            if ((dCurrent - 30000) < 0)
                m_VideoCapture.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.PosMsec, 0);
            else
                m_VideoCapture.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.PosMsec, dCurrent - 30000);
            if (bPrevPlaying)
                PlayVideo();
            else
                ProcessFrame(null);
        }

        private void buttonPrev10_Click(object sender, EventArgs e)
        {
            bool bPrevPlaying = IsPlaying;
            StopVideo();
            double dCurrent = m_VideoCapture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.PosMsec);
            if ((dCurrent - 10000) < 0)
                m_VideoCapture.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.PosMsec, 0);
            else
                m_VideoCapture.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.PosMsec, dCurrent - 10000);
            if (bPrevPlaying)
                PlayVideo();
            else
                ProcessFrame(null);
        }

        private void buttonNext10_Click(object sender, EventArgs e)
        {
            bool bPrevPlaying = IsPlaying;
            StopVideo();
            double dCurrent = m_VideoCapture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.PosMsec);

            if ((dCurrent + 10000) > (m_dVideoDuration * 1000))
                m_VideoCapture.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.PosMsec, (m_dVideoDuration - 1) * 1000);
            else
                m_VideoCapture.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.PosMsec, dCurrent + 10000);
            if (bPrevPlaying)
                PlayVideo();
            else
                ProcessFrame(null);
        }

        private void buttonNext30_Click(object sender, EventArgs e)
        {
            bool bPrevPlaying = IsPlaying;
            StopVideo();
            double dCurrent = m_VideoCapture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.PosMsec);
            if ((dCurrent + 30000) > (m_dVideoDuration * 1000))
                m_VideoCapture.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.PosMsec, (m_dVideoDuration - 1) * 1000);
            else
                m_VideoCapture.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.PosMsec, dCurrent + 30000);
            if (bPrevPlaying)
                PlayVideo();
            else
                ProcessFrame(null);
        }

        private delegate void CloseWindowDelegate();
        private void CloseWindow()
        {
            if (this.InvokeRequired == true)
            {
                CloseWindowDelegate CWD = new CloseWindowDelegate(CloseWindow);
                this.BeginInvoke(CWD, new object[] { });
            }
            else
            {
                Close();
            }
        }

        private bool m_bClosing = false;
        private void FormSecondVideoPlayer_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopVideo();
            if (m_VideoCapture != null)
                m_VideoCapture.Dispose();
            m_hsOtherVideoPlayers.Remove(this);
        }

        private void imageViewer_SizeChanged(object sender, EventArgs e)
        {
            if (m_currentDisplayImage != null)
            {
                if (imageViewer.Width > 0 && imageViewer.Height > 0)
                {
                    Mat imNewSized = new Mat();
                    CvInvoke.Resize(m_currentDisplayImage, imNewSized, new Size(imageViewer.Width, imageViewer.Height), 1);
                    imageViewer.Image = imNewSized.ToImage<Bgr, byte>();
                    m_currentDisplayImage = imNewSized;
                }                
            }
        }
    }
}
