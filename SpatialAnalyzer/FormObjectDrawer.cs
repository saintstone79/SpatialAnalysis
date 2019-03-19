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
    public partial class FormObjectDrawer : Form
    {
        bool m_bDrawing = false;
        private PointF m_ptStart;
        private PointF m_ptCurrent;
        Image<Bgr, byte> m_img;
        private int m_nOriginalImgWidth;
        private int m_nOriginalImgHeight;
        private int m_nGridWidth;
        private int m_nGridHeight;
        public PointF[] m_ptsGridBounds;
        private List<Rectangle> m_listRect;

        public FormObjectDrawer(Image<Bgr, byte> img, int nGridWidth, int nGridHeight)
        {
            InitializeComponent();

            m_nOriginalImgWidth = img.Width;
            m_nOriginalImgHeight = img.Height;
            m_nGridWidth = nGridWidth;
            m_nGridHeight = nGridHeight;

            Mat imNewSized = new Mat();
            CvInvoke.Resize(img, imNewSized, new Size(nGridWidth, nGridHeight), 1);
            imageBox1.Image = imNewSized;
            m_img = imNewSized.ToImage<Bgr, Byte>();
            m_listRect = new List<Rectangle>();
        }

        private void imageBox1_MouseClick(object sender, MouseEventArgs e)
        {
            int nX, nY;
            SpatialAnalyzer.FormGridBound.ConvertCoordinates(imageBox1, out nX, out nY, e.X, e.Y);
            if (m_bDrawing == false)
            {
                m_ptStart = new PointF(nX, nY);
                m_bDrawing = true;
            }
            else
            {
                int nWidth = nX - (int)m_ptStart.X;
                int nHeight = nY - (int)m_ptStart.Y;
                Rectangle newRect = new Rectangle((int)m_ptStart.X, (int)m_ptStart.Y, nWidth, nHeight);
                m_listRect.Add(newRect);
                CvInvoke.Rectangle(m_img, newRect, new Bgr(Color.Black).MCvScalar);
                imageBox1.Image = m_img;
                this.Cursor = Cursors.Default;
                m_bDrawing = false;
            }
        }

        private void buttonAddRectangle_Click(object sender, EventArgs e)
        {

        }

        private void imageBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (m_bDrawing == false)
                return;

            int nX, nY;
            SpatialAnalyzer.FormGridBound.ConvertCoordinates(imageBox1, out nX, out nY, e.X, e.Y);
            m_ptCurrent = new PointF(nX, nY);

            Image<Bgr, byte> imgCopy = m_img.Clone();
            int nWidth = nX - (int)m_ptStart.X;
            int nHeight = nY - (int)m_ptStart.Y;
            Rectangle rect = new Rectangle((int)m_ptStart.X, (int)m_ptStart.Y, nWidth, nHeight);
            CvInvoke.Rectangle(imgCopy, rect, new Bgr(Color.DarkGray).MCvScalar);
            imageBox1.Image = imgCopy;
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {

        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {

        }
    }
}
