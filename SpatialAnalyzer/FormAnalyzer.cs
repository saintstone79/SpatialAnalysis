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
    public partial class FormAnalyzer : Form
    {
        public int m_nClassWidth;
        public int m_nClassHeight;
        private static int GRID_WIDTH = 320;
        private static int GRID_HEIGHT = 224;

        private bool m_bStart = false;

        private int m_nCurrentDataPos;
        private bool m_bSaved;
        private List<SpatialData> m_listSpatialData;
        public int m_nInterval;
        public int m_nInstructor;
        public int m_nStudent;
        private VideoCapture m_VideoCapture;
        private string m_strFilename;
        public PointF[] m_ptsGridBounds = null;
        private FormOriginalVideoPlayer m_formOrigVideoPlayer = null;
        private Mat m_currentDisplayImage = null;
        private Mat m_currentAnnotatedImage = null;
        private bool m_bShowTeacherPeripheral = false;

        // Draw objects on the screen
        private bool m_bDrawingObject = false;
        private bool m_bDrawingMode = false;
        private PointF m_ptDrawingStart;
        private List<Rectangle> m_listRect;


        public FormAnalyzer()
        {
            InitializeComponent();
            m_bSaved = false;
            m_nCurrentDataPos = 0;
            m_listRect = new List<Rectangle>();
            m_listSpatialData = new List<SpatialData>();
            m_hsOtherVideoPlayers = new HashSet<FormSecondVideoPlayer>();
        }

        private void FormAnalyzer_Load(object sender, EventArgs e)
        {
            FormInputParam inputDlg = new FormInputParam(this);
            if (inputDlg.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                Close();
                return;
            }

            if (LoadVideoFile() == true)
            {
                btnSetGridBound.Enabled = true;
                btnVideoForward.Enabled = true;
                btnVideoPrev.Enabled = true;
                btnDrawObjects.Enabled = true;
                btnEraseObjects.Enabled = true;

                double dTotalLength = m_VideoCapture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.FrameCount) / m_VideoCapture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.Fps);
                lbTotalDuration.Text = TimeSpan.FromSeconds(dTotalLength).ToString(@"hh\:mm\:ss");

                ShowVideoFrame();
            }
        }

        private bool LoadVideoFile()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Window Video (*.avi;*.mp4;*.mov)|*.avi;*.mp4;*.mov";
            openFileDialog1.Title = "Select a video file";
            // Show the Dialog.  
            // If the user clicked OK in the dialog and              
            if (openFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return false;

            m_strFilename = openFileDialog1.FileName;
            this.Text += ": " + m_strFilename;
            m_VideoCapture = new VideoCapture(openFileDialog1.FileName);

            if (m_VideoCapture == null)
            {
                MessageBox.Show("Failed to load video file");
                return false;
            }
            return true;
        }

        private void AddNewSpatialData()
        {
            SpatialData newData = new SpatialData();
            newData.dPosition = m_VideoCapture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.PosMsec);

            for (int i = 1; i < m_nInstructor + 1; i++)
            {
                SpatialRowData aInstructor = new SpatialRowData("T" + i.ToString(), -1, -1);
                newData.AddRow(aInstructor);
            }

            for (int j = 1; j < m_nStudent + 1; j++)
            {
                SpatialRowData aStudent = new SpatialRowData("K" + j.ToString(), -1, -1);
                newData.AddRow(aStudent);
            }
            newData.UpdateDistanceFromInstructor();
            m_listSpatialData.Add(newData);
            UpdateSpatialDataToViewer(newData);
        }

        private void InitializeDataGridViewer()
        {
            for (int i = 1; i < m_nInstructor + 1; i++)
            {
                dataGridView1.Rows.Add("T" + i.ToString());
            }

            for (int j = 1; j < m_nStudent + 1; j++)
            {
                dataGridView1.Rows.Add("K" + j.ToString());
            }

            dataGridView1.Rows[0].Selected = true;
            dataGridView1.Columns[0].ReadOnly = true;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (LoadVideoFile() == true)
            {
                btnSetGridBound.Enabled = true;
                btnVideoForward.Enabled = true;
                btnVideoPrev.Enabled = true;
                ShowVideoFrame();
            }
        }


        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            btnLoad.Enabled = false;
            m_bStart = true;
            InitializeDataGridViewer();
            AddNewSpatialData();
            m_nCurrentDataPos = 0;
            btnSave.Enabled = true;
            btnCopyPrevLog.Enabled = true;
        }



        private void UpdateGridDataToSpatialData()
        {
            if (m_listSpatialData == null || m_listSpatialData.Count <= m_nCurrentDataPos) 

                return;

            SpatialData spatialData = m_listSpatialData[m_nCurrentDataPos];
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    int x, y;
                    string strCode, strNote;
                    if (row.Cells[1].Value == null) x = -1;
                    else x = (int)row.Cells[1].Value;
                    if (row.Cells[2].Value == null) y = -1;
                    else y = (int)row.Cells[2].Value;
                    if (row.Cells[3].Value == null) strCode = "";
                    else strCode = row.Cells[3].Value.ToString();
                    if (row.Cells[4].Value == null) strNote = "";
                    else strNote = row.Cells[4].Value.ToString();
                    SpatialRowData aRowData = spatialData.GetRow(row.Index);
                    aRowData.nX = x;
                    aRowData.nY = y;
                    aRowData.strCode = strCode;
                    aRowData.strNote = strNote;
                }
            }
            spatialData.UpdateDistanceFromInstructor();
        }

        private void UpdateSpatialDataToViewer(SpatialData spatialData)
        {
            for (int i = 0; i < spatialData.GetCount(); i++)
            {
                if (spatialData.GetRow(i).nX == -1)
                    dataGridView1.Rows[i].Cells[1].Value = null;
                else
                    dataGridView1.Rows[i].Cells[1].Value = (int)spatialData.GetRow(i).nX;
                if (spatialData.GetRow(i).nY == -1)
                    dataGridView1.Rows[i].Cells[2].Value = null;
                else
                    dataGridView1.Rows[i].Cells[2].Value = (int)spatialData.GetRow(i).nY;
                if (spatialData.GetRow(i).strCode == "")
                    dataGridView1.Rows[i].Cells[3].Value = null;
                else
                    dataGridView1.Rows[i].Cells[3].Value = spatialData.GetRow(i).strCode;
                if (spatialData.GetRow(i).strNote == "")
                    dataGridView1.Rows[i].Cells[4].Value = null;
                else
                    dataGridView1.Rows[i].Cells[4].Value = spatialData.GetRow(i).strNote;
            }
            dataGridView1.Rows[0].Selected = true;

            m_VideoCapture.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.PosMsec, spatialData.dPosition);
            ShowVideoFrame();
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            UpdateGridDataToSpatialData();
            SaveData();
        }

        private bool SaveData()
        {
            try
            {
                String strFilePath = m_strFilename;
                strFilePath += "_spatialData.csv";
                if (m_bSaved == false && System.IO.File.Exists(strFilePath))
                {
                    MessageBox.Show(String.Format("The same file already exist, {0}", strFilePath));
                    return false;
                }

                using (var csvFile = new CsvFileWriter(strFilePath))
                {

                    CsvRow rowHeader = new CsvRow();
                    rowHeader.Add("Time");
                    rowHeader.Add("Individual");
                    rowHeader.Add("X");
                    rowHeader.Add("Y");
                    rowHeader.Add("Code");
                    rowHeader.Add("Note");
                    rowHeader.Add("DistanceMoved");
                    rowHeader.Add("DistanceFromT1");
                    rowHeader.Add("DistanceFromT2");
                    csvFile.WriteRow(rowHeader);

                    CalculateMovingDistance();
                    foreach (SpatialData spatialData in m_listSpatialData)
                    {
                        for (int i = 0; i < spatialData.GetCount(); i++)
                        {
                            SpatialRowData spatialRowData = spatialData.GetRow(i);
                            CsvRow aCsvRow = new CsvRow();
                            aCsvRow.Add(TimeSpan.FromMilliseconds(spatialData.dPosition).ToString(@"hh\:mm\:ss"));
                            aCsvRow.Add(spatialRowData.strIndividual);
                            aCsvRow.Add(spatialRowData.nX.ToString());
                            aCsvRow.Add(spatialRowData.nY.ToString());
                            aCsvRow.Add(spatialRowData.strCode);
                            aCsvRow.Add(spatialRowData.strNote);
                            aCsvRow.Add(spatialRowData.dDistanceFromPrev.ToString());
                            aCsvRow.Add(spatialRowData.dDistanceFromT1.ToString());
                            aCsvRow.Add(spatialRowData.dDistanceFromT2.ToString());
                            csvFile.WriteRow(aCsvRow);
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Failed to save file");
                return false;
            }
            m_bSaved = true;
            return true;
        }
        
        private void GetDistanceFromPreviousLog(String strIndividual)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (m_bSaved == false)
            {
                DialogResult dialogResult = MessageBox.Show("Not saved data file. Do you want save before closing?", "Confirm", MessageBoxButtons.YesNoCancel);
                if (dialogResult == DialogResult.Yes)
                {
                    if (SaveData() == false) return;
                    UpdateGridDataToSpatialData();
                }
                else if (dialogResult == DialogResult.Cancel)
                {
                    return;
                }
            }
            Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ClearDataGridSelection();
            dataGridView1.Rows[e.RowIndex].Selected = true;
        }

        private void ClearDataGridSelection()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Selected = false;
            }
        }

        private Point GetScreenCoordinationFromRelative(Point ptRelative)
        {
            int nScreenX = ptRelative.X * imageBox_Homography.Width / m_nClassWidth;
            int nScreenY = ptRelative.Y * imageBox_Homography.Height / m_nClassHeight;
            return new Point(nScreenX, nScreenY);
        }

        private void UpdateScreenAnnotation()
        {
            if (m_currentDisplayImage == null) return;

            if (m_currentAnnotatedImage != null)
                m_currentAnnotatedImage = null;

            m_currentAnnotatedImage = new Mat();
            m_currentDisplayImage.CopyTo(m_currentAnnotatedImage);

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    if (row.Cells[1].Value != null && row.Cells[2].Value != null)
                    {
                        Point ptLocation = GetScreenCoordinationFromRelative(new Point((int)row.Cells[1].Value, (int)row.Cells[2].Value));
                        CvInvoke.Circle(m_currentAnnotatedImage, ptLocation, 5, new Bgr(255, 0, 0).MCvScalar, 2);
                        CvInvoke.PutText(m_currentAnnotatedImage, row.Cells[0].Value.ToString(), new System.Drawing.Point(ptLocation.X + 5, ptLocation.Y + 2), FontFace.HersheyDuplex, 0.5, new Bgr(0, 255, 0).MCvScalar);
                    }
                }
            }

            if (m_bShowTeacherPeripheral)
            {
                Size szEllipseSize = new Size();
                szEllipseSize.Width = 50 * imageBox_Homography.Width / m_nClassWidth;
                szEllipseSize.Height = 50 * imageBox_Homography.Height / m_nClassHeight;

                if (dataGridView1.Rows[0].Cells[1].Value != null && dataGridView1.Rows[0].Cells[2].Value != null)
                {
                    Point ptT1 = GetScreenCoordinationFromRelative(new Point((int)dataGridView1.Rows[0].Cells[1].Value, (int)dataGridView1.Rows[0].Cells[2].Value));
                    if (ptT1.X != -1 && ptT1.Y != -1)
                        CvInvoke.Ellipse(m_currentAnnotatedImage, ptT1, szEllipseSize, 0, 0, 360, new Bgr(2, 14, 247).MCvScalar, 1, LineType.Filled);

                }

                if (dataGridView1.Rows[1].Cells[1].Value != null && dataGridView1.Rows[1].Cells[2].Value != null)
                {
                    Point ptT2 = GetScreenCoordinationFromRelative(new Point((int)dataGridView1.Rows[1].Cells[1].Value, (int)dataGridView1.Rows[1].Cells[2].Value));
                    if (ptT2.X != -1 && ptT2.Y != -1)
                        CvInvoke.Ellipse(m_currentAnnotatedImage, ptT2, szEllipseSize, 0, 0, 360, new Bgr(2, 14, 247).MCvScalar, 1, LineType.Filled);
                }
            }

            foreach (Rectangle rect in m_listRect)
            {
                CvInvoke.Rectangle(m_currentAnnotatedImage, ConvertRelativeRectToScreen(rect), new Bgr(Color.Red).MCvScalar);
            }

            imageBox_Homography.Image = m_currentAnnotatedImage;
        }
        
        private void imageBox_Homography_MouseClick(object sender, MouseEventArgs e)
        {
            if (m_bDrawingMode == true)
            {
                if (m_bDrawingObject == false)
                {
                    m_ptDrawingStart = new PointF(e.X, e.Y);
                    m_bDrawingObject = true;
                }
                else
                {
                    int nWidth = e.X - (int)m_ptDrawingStart.X;
                    int nHeight = e.Y - (int)m_ptDrawingStart.Y;
                    Rectangle newRect = new Rectangle((int)m_ptDrawingStart.X, (int)m_ptDrawingStart.Y, nWidth, nHeight);
                    m_listRect.Add(ConvertScreenRectToRelative(newRect));
                    UpdateScreenAnnotation();

                    this.Cursor = Cursors.Default;
                    m_bDrawingObject = false;
                    m_bDrawingMode = false;

                }
            }
            else if (m_bStart == true)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                if (selectedRow.Cells[0].Value != null)
                {
                    int relativeX = e.X * m_nClassWidth / imageBox_Homography.Width;
                    int relativeY = e.Y * m_nClassHeight / imageBox_Homography.Height;
                    tbLog.AppendText(String.Format("Object {0} ---- Mouse click x={1}, y={2}\r\n", selectedRow.Cells[0].Value.ToString(), relativeX, relativeY));
                    selectedRow.Cells[1].Value = relativeX;
                    selectedRow.Cells[2].Value = relativeY;
                    dataGridView1.Rows[selectedRow.Index + 1].Selected = true;
                    UpdateScreenAnnotation();
                }
            }            
        }

        private Rectangle ConvertScreenRectToRelative(Rectangle rectScreen)
        {
            int nRelativeX = rectScreen.X * m_nClassWidth / imageBox_Homography.Width;
            int nRelativeY = rectScreen.Y * m_nClassHeight / imageBox_Homography.Height;
            int nRelativeWidth = rectScreen.Width * m_nClassWidth / imageBox_Homography.Width;
            int nRelativeHeight = rectScreen.Height * m_nClassHeight / imageBox_Homography.Height;

            return new Rectangle(nRelativeX, nRelativeY, nRelativeWidth, nRelativeHeight);
        }
        private Rectangle ConvertRelativeRectToScreen(Rectangle rectRelative)
        {
            int nScreenX = rectRelative.X * imageBox_Homography.Width / m_nClassWidth;
            int nScreenY = rectRelative.Y * imageBox_Homography.Height / m_nClassHeight;
            int nScreenWidth = rectRelative.Width * imageBox_Homography.Width / m_nClassWidth;
            int nScreenHeight = rectRelative.Height * imageBox_Homography.Height / m_nClassHeight;

            return new Rectangle(nScreenX, nScreenY, nScreenWidth, nScreenHeight);
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
            m_VideoCapture.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.PosMsec, dCurrent);
            Mat imNewSized = new Mat();

            if (m_ptsGridBounds == null)
            {
                CvInvoke.Resize(imgeOrigenal, imNewSized, new Size(imageBox_Homography.Width, imageBox_Homography.Height), 1);
                imageBox_Homography.Image = imNewSized;
                m_currentDisplayImage = imNewSized;
            }
            else
            {
                int nWidth = (int)m_VideoCapture.GetCaptureProperty(CapProp.FrameWidth);
                int nHeight = (int)m_VideoCapture.GetCaptureProperty(CapProp.FrameHeight);

                PointF[] ptsDest = new PointF[] { new PointF(0, 0), new PointF(0, nHeight), new PointF(nWidth, nHeight), new PointF(nWidth, 0) };
                Mat imOut = new Mat();
                Mat homography = CvInvoke.FindHomography(m_ptsGridBounds, ptsDest, Emgu.CV.CvEnum.HomographyMethod.Ransac);
                CvInvoke.WarpPerspective(imgeOrigenal, imOut, homography, new Size((int)nWidth, (int)nHeight), Inter.Linear);
                CvInvoke.Resize(imOut, imNewSized, new Size(imageBox_Homography.Width, imageBox_Homography.Height), 1);
                imageBox_Homography.Image = imNewSized.ToImage<Bgr, byte>();
                m_currentDisplayImage = imNewSized;
            }
            if (m_formOrigVideoPlayer != null && m_formOrigVideoPlayer.Visible == true)
                m_formOrigVideoPlayer.UpdateVideoImage(imgeOrigenal);

            UpdateScreenAnnotation();
        }

        private void btnSetGridBound_Click(object sender, EventArgs e)
        {
            if (m_VideoCapture == null)
                return;

            Image<Bgr, Byte> imgeOrigenal = m_VideoCapture.QueryFrame().ToImage<Bgr, Byte>();

            // Get Grid Bounds
            FormGridBound formHomography = new FormGridBound(imgeOrigenal, GRID_WIDTH, GRID_HEIGHT);
            formHomography.ShowDialog();
            m_ptsGridBounds = formHomography.m_ptsGridBounds;
            if (m_ptsGridBounds != null)
            {
                btnStart.Enabled = true;
                btnShowOriginalVideo.Enabled = true;
                ShowVideoFrame();
            }
        }

        private void btnCopyPrevLog_Click(object sender, EventArgs e)
        {
            if (m_nCurrentDataPos == 0)
                return;

            SpatialData.CopyObject(m_listSpatialData[m_nCurrentDataPos - 1], m_listSpatialData[m_nCurrentDataPos]);
            UpdateSpatialDataToViewer(m_listSpatialData[m_nCurrentDataPos]);
        }

        private void btnVideoForward_Click(object sender, EventArgs e)
        {
            foreach (FormSecondVideoPlayer aPlayer in m_hsOtherVideoPlayers)
                aPlayer.Forward(m_nInterval);

            double dTotalDuration = m_VideoCapture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.FrameCount) / m_VideoCapture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.Fps);
            double dCurrentPos = m_VideoCapture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.PosMsec);
            if ((dCurrentPos + m_nInterval) > (dTotalDuration * 1000) )            
                m_VideoCapture.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.PosMsec, (dTotalDuration-1)*1000);
            else
                m_VideoCapture.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.PosMsec, dCurrentPos + m_nInterval);

            UpdateGridDataToSpatialData();
            ShowVideoFrame();

            if (m_bStart == false)
                return;

            if ((m_listSpatialData.Count - 1) != m_nCurrentDataPos) // if this is not the last data
            {
                UpdateSpatialDataToViewer(m_listSpatialData[++m_nCurrentDataPos]);
            }
            else // Add new data
            {
                AddNewSpatialData();
                m_nCurrentDataPos++;
            }
        }

        private void btnVideoPrev_Click(object sender, EventArgs e)
        {
            foreach (FormSecondVideoPlayer aPlayer in m_hsOtherVideoPlayers)
                aPlayer.Prev(m_nInterval);
                               
            if (m_bStart == false)
            {
                double dCurrentPos = m_VideoCapture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.PosMsec);
                if (dCurrentPos - m_nInterval < 0)
                    m_VideoCapture.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.PosMsec, 0);
                else
                    m_VideoCapture.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.PosMsec, dCurrentPos - m_nInterval);
                ShowVideoFrame();
                return;
            }
            else
            {
                if (m_nCurrentDataPos == 0)
                {
                    MessageBox.Show("This is the first record");
                    return;
                }
                UpdateGridDataToSpatialData();
                UpdateSpatialDataToViewer(m_listSpatialData[--m_nCurrentDataPos]);
            }                        
        }

        private void imageBox_Homography_SizeChanged(object sender, EventArgs e)
        {
            if (imageBox_Homography.Width > 0 || imageBox_Homography.Height > 0)
                ShowVideoFrame();
        }

        private void btnShowOriginalVideo_Click(object sender, EventArgs e)
        {
            if (m_formOrigVideoPlayer == null)
                m_formOrigVideoPlayer = new FormOriginalVideoPlayer(m_VideoCapture);
            m_formOrigVideoPlayer.Show();
            m_formOrigVideoPlayer.UpdateVideoImage(m_currentDisplayImage.ToImage<Bgr, Byte>());
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                return;
            }
        }

        private void cbPeripheral_CheckedChanged(object sender, EventArgs e)
        {
            m_bShowTeacherPeripheral = cbPeripheral.Checked;
            UpdateScreenAnnotation();
        }

        private void btnDrawObject_Click(object sender, EventArgs e)
        {
            if (m_VideoCapture == null)
                return;

            m_bDrawingMode = true;
            this.Cursor = Cursors.Cross;
        }

        private void imageBox_Homography_MouseMove(object sender, MouseEventArgs e)
        {
            if (m_bDrawingMode && m_bDrawingObject && m_currentAnnotatedImage != null)
            {
                Image<Bgr, byte> imgCopy = m_currentAnnotatedImage.ToImage<Bgr, byte>();
                int nWidth = e.X - (int)m_ptDrawingStart.X;
                int nHeight = e.Y - (int)m_ptDrawingStart.Y;
                Rectangle rect = new Rectangle((int)m_ptDrawingStart.X, (int)m_ptDrawingStart.Y, nWidth, nHeight);
                CvInvoke.Rectangle(imgCopy, rect, new Bgr(Color.DarkGray).MCvScalar);
                imageBox_Homography.Image = imgCopy;
            }
        }

        private void btnEraseObjects_Click(object sender, EventArgs e)
        {
            if (m_listRect.Count > 0)
            {
                m_listRect.RemoveAt(m_listRect.Count - 1);
                UpdateScreenAnnotation();
            }
        }

        private void CalculateMovingDistance()
        {
            if (m_listSpatialData == null || m_listSpatialData.Count == 0)
                return;

            SpatialData prevData = m_listSpatialData[0];                

            for (int i = 1; i < m_listSpatialData.Count; i++)
            {
                SpatialData currentData = m_listSpatialData[i];
                for (int j = 0; (j < currentData.GetCount()) && (j < prevData.GetCount()); j ++)
                {
                    if (prevData.GetRow(j).nX != -1 && prevData.GetRow(j).nY != -1 && currentData.GetRow(j).nX != -1 && currentData.GetRow(j).nY != -1)
                    {
                        int distanceX = prevData.GetRow(j).nX - currentData.GetRow(j).nX;
                        int distanceY = prevData.GetRow(j).nY - currentData.GetRow(j).nY;
                        currentData.GetRow(j).dDistanceFromPrev = Math.Sqrt(Math.Pow(distanceX, 2.0f) + Math.Pow(distanceY, 2.0f));
                    }
                    else
                    {
                        currentData.GetRow(j).dDistanceFromPrev = 0;
                    }                                        
                }
                prevData = currentData;
            }
        }

        private HashSet<FormSecondVideoPlayer> m_hsOtherVideoPlayers;                
        private void btnSecondAngleVideo_Click(object sender, EventArgs e)
        {                           
            FormSecondVideoPlayer newPlayer = new FormSecondVideoPlayer(m_hsOtherVideoPlayers);
            newPlayer.Show();            
        }
    }

    public class SpatialRowData
    {
        public String strIndividual;
        public int nX;
        public int nY;
        public String strCode = "";
        public String strNote = "";
        
        public double dDistanceFromT1 = 0;
        public double dDistanceFromT2 = 0;        
        public double dDistanceFromPrev = 0;

        public SpatialRowData(String strInd, int x, int y)
        {
            strIndividual = strInd;
            nX = x;
            nY = y;            
        }
    }

    public class SpatialData
    {
        public double dPosition;
        private List<SpatialRowData> listSpatialDataRows;

        public SpatialData()
        {
            listSpatialDataRows = new List<SpatialRowData>();
        }

        public void AddRow(SpatialRowData aRow)
        {
            listSpatialDataRows.Add(aRow);
        }

        public SpatialRowData GetRow(int nIndex)
        {
            if (nIndex > listSpatialDataRows.Count)
                return null;
            return listSpatialDataRows[nIndex];
        }

        public int GetCount()
        {
            return listSpatialDataRows.Count;
        }

        private Point GetIndividualLocation(string strIndividual)
        {
            int nX = -1;
            int nY = -1;
            foreach (SpatialRowData aRow in listSpatialDataRows)
            {
                if (aRow.strIndividual.Equals(strIndividual))
                {
                    nX = aRow.nX;
                    nY = aRow.nY;
                    break;
                }
            }
            return new Point(nX, nY);
        }
        public void UpdateDistanceFromInstructor()
        {            
            // find the Instructor's location
            Point ptT1 = GetIndividualLocation("T1");
            Point ptT2 = GetIndividualLocation("T2");
            
            foreach (SpatialRowData aRow in listSpatialDataRows)
            {
                if (ptT1.X == -1 || ptT1.Y == -1)
                {
                    aRow.dDistanceFromT1 = 0;
                }
                else
                {
                    int nXFromT1 = ptT1.X - aRow.nX;
                    int nYFromT1 = ptT1.Y - aRow.nY;
                    aRow.dDistanceFromT1 = Math.Sqrt(Math.Pow(nXFromT1, 2.0f) + Math.Pow(nYFromT1, 2.0f));
                }
                
                if (ptT2.X == -1 || ptT2.Y == -1)
                {
                    aRow.dDistanceFromT2 = 0;
                }
                else
                {
                    int nXFromT2 = ptT2.X - aRow.nX;
                    int nYFromT2 = ptT2.Y - aRow.nY;
                    aRow.dDistanceFromT2 = Math.Sqrt(Math.Pow(nXFromT2, 2.0f) + Math.Pow(nYFromT2, 2.0f));
                }                
            }
        }

        public static void CopyObject(SpatialData dataSrc, SpatialData dataDest)
        {
            int nMinLength;
            nMinLength = Math.Min(dataSrc.GetCount(), dataDest.GetCount());
            for (int i = 0; i < nMinLength; i++)
            {
                dataDest.GetRow(i).nX = dataSrc.GetRow(i).nX;
                dataDest.GetRow(i).nY = dataSrc.GetRow(i).nY;
                // copy code when click "copy prev" button, but do not copy notes
                dataDest.GetRow(i).strCode = dataSrc.GetRow(i).strCode;
            }
        }        
    }
}
