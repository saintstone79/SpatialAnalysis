using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpatialAnalyzer
{
    public partial class FormInputParam : Form
    {
        //public 
        public FormInputParam(FormAnalyzer formAnalyzer)
        {
            InitializeComponent();
            _formAnalyzer = formAnalyzer;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            int nInterval = 0;
            int nInstructor = 0;
            int nStudent = 0;
            int nWidth = 0;
            int nHeight = 0;
            
            if (!Int32.TryParse(tbInterval.Text.ToString(), out nInterval))
            {
                MessageBox.Show("Invalid Interval");
                return;
            }
            if (!Int32.TryParse(tbInstructor.Text.ToString(), out nInstructor))
            {
                MessageBox.Show("Invalid Instructor Number");
                return;
            }
            if (!Int32.TryParse(tbStudents.Text.ToString(), out nStudent))
            {
                MessageBox.Show("Invalid Student Number");
                return;
            }
            if (!Int32.TryParse(tbWidth.Text.ToString(), out nWidth))
            {
                MessageBox.Show("Invalid Class size width");
                return;
            }
            if (!Int32.TryParse(tbHeight.Text.ToString(), out nHeight))
            {
                MessageBox.Show("Invalid Class size height");
                return;
            }
            _formAnalyzer.m_nInterval = nInterval * 1000; // milliseconds
            _formAnalyzer.m_nInstructor = nInstructor;
            _formAnalyzer.m_nStudent = nStudent;
            _formAnalyzer.m_nClassWidth = nWidth;
            _formAnalyzer.m_nClassHeight = nHeight;
            this.DialogResult = DialogResult.OK;
            Close();
        }
      
        public FormAnalyzer _formAnalyzer;

        private void FormInputParam_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                //this.Close();
                System.Windows.Forms.Application.Exit();
            }
        }

        private void FormInputParam_FormClosing(object sender, FormClosingEventArgs e)
        {
//            if (this.DialogResult == DialogResult.Cancel)
            //{
              //  System.Windows.Forms.Application.Exit();
            //}                
        }
    }
}
