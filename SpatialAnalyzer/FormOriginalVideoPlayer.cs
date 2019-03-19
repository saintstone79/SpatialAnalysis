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

namespace SpatialAnalyzer
{
    public partial class FormOriginalVideoPlayer : Form
    {
        private VideoCapture m_videoCapture;
        private Mat m_currentDisplayImage = null;
        public FormOriginalVideoPlayer(VideoCapture _capture)
        {
            InitializeComponent();
            m_videoCapture = _capture;
        }

        public void UpdateVideoImage(Image<Bgr, Byte> imgeOrigenal)
        {
            //double dCurrent = m_videoCapture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.PosMsec);
            //Image<Bgr, Byte> imgeOrigenal = m_videoCapture.QueryFrame().ToImage<Bgr, Byte>();
            if (imgeOrigenal == null)
                return;
            //m_videoCapture.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.PosMsec, dCurrent);

            Mat imNewSized = new Mat();
            CvInvoke.Resize(imgeOrigenal, imNewSized, new Size(imageBoxOrigVideo.Width, imageBoxOrigVideo.Height), 1);
            imageBoxOrigVideo.Image = imNewSized.ToImage<Bgr, byte>();
            m_currentDisplayImage = imNewSized;
        }

        private void imageBoxOrigVideo_SizeChanged(object sender, EventArgs e)
        {
            Mat imNewSized = new Mat();
            CvInvoke.Resize(m_currentDisplayImage, imNewSized, new Size(imageBoxOrigVideo.Width, imageBoxOrigVideo.Height), 1);
            imageBoxOrigVideo.Image = imNewSized.ToImage<Bgr, byte>();
            m_currentDisplayImage = imNewSized;
        }        

        private void FormOriginalVideoPlayer_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

    }
}
