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
    public partial class FormGridBound : Form
    {
        public Mat _Homography;        
        int m_nHit;
        private PointF m_ptA, m_ptB, m_ptC, m_ptD;
        private PointF m_ptCurrent;
        Image<Bgr, byte> m_img;
        private int m_nOriginalImgWidth;
        private int m_nOriginalImgHeight;
        private int m_nGridWidth;
        private int m_nGridHeight;
        public PointF[] m_ptsGridBounds;

        public FormGridBound(Image<Bgr, byte> img, int nGridWidth, int nGridHeight)
        {
            InitializeComponent();                        
            m_nHit = 0;

            m_nOriginalImgWidth = img.Width;
            m_nOriginalImgHeight = img.Height;
            m_nGridWidth = nGridWidth;
            m_nGridHeight = nGridHeight;

            Mat imNewSized = new Mat();
            CvInvoke.Resize(img, imNewSized, new Size(nGridWidth, nGridHeight), 1);
            imageBox1.Image = imNewSized;
            m_img = imNewSized.ToImage<Bgr, Byte>();
        }

        private void FormHomography_Paint(object sender, PaintEventArgs e)
        {            

        }

        private void imageBox1_MouseMove(object sender, MouseEventArgs e)
        {
            int nX, nY;
            ConvertCoordinates(imageBox1, out nX, out nY, e.X, e.Y);
            m_ptCurrent = new PointF(nX, nY);

            Image<Bgr, byte> imgCopy = m_img.Clone();


            if (m_nHit == 1)
            {                
                CvInvoke.Line(imgCopy, Point.Round(m_ptA), Point.Round(m_ptCurrent), new Bgr(Color.Red).MCvScalar, 3);
                Console.WriteLine("Draw line({0}, {1}) --- ({2}, {3})", m_ptA.X, m_ptA.Y, m_ptCurrent.X, m_ptCurrent.Y);
            } else if(m_nHit == 2) {
                CvInvoke.Line(imgCopy, Point.Round(m_ptA), Point.Round(m_ptB), new Bgr(Color.Red).MCvScalar, 3);
                CvInvoke.Line(imgCopy, Point.Round(m_ptB), Point.Round(m_ptCurrent), new Bgr(Color.Red).MCvScalar, 3);
            } else if(m_nHit == 3) {
                CvInvoke.Line(imgCopy, Point.Round(m_ptA), Point.Round(m_ptB), new Bgr(Color.Red).MCvScalar, 3);
                CvInvoke.Line(imgCopy, Point.Round(m_ptB), Point.Round(m_ptC), new Bgr(Color.Red).MCvScalar, 3);
                CvInvoke.Line(imgCopy, Point.Round(m_ptC), Point.Round(m_ptCurrent), new Bgr(Color.Red).MCvScalar, 3);
            } else if(m_nHit == 4) {
                CvInvoke.Line(imgCopy, Point.Round(m_ptA), Point.Round(m_ptB), new Bgr(Color.Red).MCvScalar, 3);
                CvInvoke.Line(imgCopy, Point.Round(m_ptB), Point.Round(m_ptC), new Bgr(Color.Red).MCvScalar, 3);
                CvInvoke.Line(imgCopy, Point.Round(m_ptC), Point.Round(m_ptD), new Bgr(Color.Red).MCvScalar, 3);
                CvInvoke.Line(imgCopy, Point.Round(m_ptD), Point.Round(m_ptA), new Bgr(Color.Red).MCvScalar, 3);
            }
            imageBox1.Image = imgCopy;
        }

        private void imageBox1_Paint(object sender, PaintEventArgs e)
        {
            /*
            Pen pen = new Pen(Color.FromArgb(255, 0, 0, 0));
            if (m_nHit == 1)
            {
                MCvScalar cvScalar = new MCvScalar(0, 0, 255);                
                CvInvoke.Line(m_img, Point.Round(m_ptA), Point.Round(m_ptCurrent), cvScalar, 3);

                //previousPoint = point;
                //e.Graphics.DrawLine(pen, m_ptA.X, m_ptA.Y, m_ptCurrent.X, m_ptCurrent.Y);
            }

            else if (m_nHit == 2)
            {
                e.Graphics.DrawLine(pen, m_ptA.X, m_ptA.Y, m_ptB.X, m_ptB.Y);
                e.Graphics.DrawLine(pen, m_ptB.X, m_ptB.Y, m_ptCurrent.X, m_ptCurrent.Y);
            }

            else if (m_nHit == 3)
            {
                e.Graphics.DrawLine(pen, m_ptA.X, m_ptA.Y, m_ptB.X, m_ptB.Y);
                e.Graphics.DrawLine(pen, m_ptB.X, m_ptB.Y, m_ptC.X, m_ptC.Y);
                e.Graphics.DrawLine(pen, m_ptC.X, m_ptC.Y, m_ptCurrent.X, m_ptCurrent.Y);
            }
            else if (m_nHit > 3)
            {
                e.Graphics.DrawLine(pen, m_ptA.X, m_ptA.Y, m_ptB.X, m_ptB.Y);
                e.Graphics.DrawLine(pen, m_ptB.X, m_ptB.Y, m_ptC.X, m_ptC.Y);
                e.Graphics.DrawLine(pen, m_ptC.X, m_ptC.Y, m_ptD.X, m_ptD.Y);
                e.Graphics.DrawLine(pen, m_ptD.X, m_ptD.Y, m_ptA.X, m_ptA.Y);
            }
            */
        }

        private void imageBox1_MouseClick(object sender, MouseEventArgs e)
        {
            int nX, nY;
            ConvertCoordinates(imageBox1, out nX, out nY, e.X, e.Y);
            if (m_nHit == 0)
            {
                m_ptA = new PointF(nX, nY);
            }
            else if (m_nHit == 1)
            {
                m_ptB = new PointF(nX, nY);
            }

            else if (m_nHit == 2)
            {
                m_ptC = new PointF(nX, nY);
            }

            else if (m_nHit == 3)
            {
                m_ptD = new PointF(nX, nY);
                this.Cursor = Cursors.Default;
            }
            Console.WriteLine("click ({0}, {1}", nX, nY);
            m_nHit++;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            m_nHit = 0;
        }

        private void btnGetHomography_Click(object sender, EventArgs e)
        {
            m_ptsGridBounds = new PointF[4];
            
            m_ptsGridBounds[0] = new PointF(m_ptA.X * m_nOriginalImgWidth / m_nGridWidth,
                    m_ptA.Y * m_nOriginalImgHeight / m_nGridHeight);
            m_ptsGridBounds[1] = new PointF(m_ptB.X * m_nOriginalImgWidth / m_nGridWidth,
                    m_ptB.Y * m_nOriginalImgHeight / m_nGridHeight);
            m_ptsGridBounds[2] = new PointF(m_ptC.X * m_nOriginalImgWidth / m_nGridWidth,
                    m_ptC.Y * m_nOriginalImgHeight / m_nGridHeight);
            m_ptsGridBounds[3] = new PointF(m_ptD.X * m_nOriginalImgWidth / m_nGridWidth,
                    m_ptD.Y * m_nOriginalImgHeight / m_nGridHeight);

            Close();
        }

        public static void ConvertCoordinates(PictureBox pic,
            out int X0, out int Y0, int x, int y)
        {
            int pic_hgt = pic.ClientSize.Height;
            int pic_wid = pic.ClientSize.Width;
            int img_hgt = pic.Image.Height;
            int img_wid = pic.Image.Width;

            X0 = x;
            Y0 = y;
            switch (pic.SizeMode)
            {
                case PictureBoxSizeMode.AutoSize:
                case PictureBoxSizeMode.Normal:
                    // These are okay. Leave them alone.
                    break;
                case PictureBoxSizeMode.CenterImage:
                    X0 = x - (pic_wid - img_wid) / 2;
                    Y0 = y - (pic_hgt - img_hgt) / 2;
                    break;
                case PictureBoxSizeMode.StretchImage:
                    X0 = (int)(img_wid * x / (float)pic_wid);
                    Y0 = (int)(img_hgt * y / (float)pic_hgt);
                    break;
                case PictureBoxSizeMode.Zoom:
                    float pic_aspect = pic_wid / (float)pic_hgt;
                    float img_aspect = img_wid / (float)img_wid;
                    if (pic_aspect > img_aspect)
                    {
                        // The PictureBox is wider/shorter than the image.
                        Y0 = (int)(img_hgt * y / (float)pic_hgt);

                        // The image fills the height of the PictureBox.
                        // Get its width.
                        float scaled_width = img_wid * pic_hgt / img_hgt;
                        float dx = (pic_wid - scaled_width) / 2;
                        X0 = (int)((x - dx) * img_hgt / (float)pic_hgt);
                    }
                    else
                    {
                        // The PictureBox is taller/thinner than the image.
                        X0 = (int)(img_wid * x / (float)pic_wid);

                        // The image fills the height of the PictureBox.
                        // Get its height.
                        float scaled_height = img_hgt * pic_wid / img_wid;
                        float dy = (pic_hgt - scaled_height) / 2;
                        Y0 = (int)((y - dy) * img_wid / pic_wid);
                    }
                    break;
            }
        }
    }
}
