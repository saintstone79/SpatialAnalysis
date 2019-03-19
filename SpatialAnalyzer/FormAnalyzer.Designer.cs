namespace SpatialAnalyzer
{
    partial class FormAnalyzer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAnalyzer));
            this.btnLoad = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Individual = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.X = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Y = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Note = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.tbLog = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSecondAngleVideo = new System.Windows.Forms.Button();
            this.btnEraseObjects = new System.Windows.Forms.Button();
            this.btnDrawObjects = new System.Windows.Forms.Button();
            this.btnShowOriginalVideo = new System.Windows.Forms.Button();
            this.btnSetGridBound = new System.Windows.Forms.Button();
            this.btnCopyPrevLog = new System.Windows.Forms.Button();
            this.imageBox_Homography = new Emgu.CV.UI.ImageBox();
            this.btnVideoForward = new System.Windows.Forms.Button();
            this.btnVideoPrev = new System.Windows.Forms.Button();
            this.lbCurrentTimeStamp = new System.Windows.Forms.Label();
            this.lbTotalDuration = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cbPeripheral = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox_Homography)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(155, 5);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(108, 23);
            this.btnLoad.TabIndex = 1;
            this.btnLoad.Text = "Load Video";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Individual,
            this.X,
            this.Y,
            this.Code,
            this.Note});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Right;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridView1.Location = new System.Drawing.Point(780, 0);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(368, 574);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            // 
            // Individual
            // 
            this.Individual.HeaderText = "Individual";
            this.Individual.Name = "Individual";
            this.Individual.Width = 70;
            // 
            // X
            // 
            this.X.HeaderText = "X";
            this.X.Name = "X";
            this.X.Width = 40;
            // 
            // Y
            // 
            this.Y.HeaderText = "Y";
            this.Y.Name = "Y";
            this.Y.Width = 40;
            // 
            // Code
            // 
            this.Code.HeaderText = "Code";
            this.Code.Name = "Code";
            this.Code.Width = 60;
            // 
            // Note
            // 
            this.Note.HeaderText = "Note";
            this.Note.Name = "Note";
            this.Note.Width = 115;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(695, 6);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(614, 6);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tbLog
            // 
            this.tbLog.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.tbLog.Location = new System.Drawing.Point(19, 34);
            this.tbLog.Multiline = true;
            this.tbLog.Name = "tbLog";
            this.tbLog.ReadOnly = true;
            this.tbLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbLog.Size = new System.Drawing.Size(751, 31);
            this.tbLog.TabIndex = 8;
            // 
            // btnStart
            // 
            this.btnStart.Enabled = false;
            this.btnStart.Location = new System.Drawing.Point(111, 6);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(102, 23);
            this.btnStart.TabIndex = 9;
            this.btnStart.Text = "Start Observation";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSecondAngleVideo);
            this.panel1.Controls.Add(this.btnEraseObjects);
            this.panel1.Controls.Add(this.btnDrawObjects);
            this.panel1.Controls.Add(this.btnShowOriginalVideo);
            this.panel1.Controls.Add(this.btnSetGridBound);
            this.panel1.Controls.Add(this.tbLog);
            this.panel1.Controls.Add(this.btnStart);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.btnExit);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 496);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(780, 78);
            this.panel1.TabIndex = 10;
            // 
            // btnSecondAngleVideo
            // 
            this.btnSecondAngleVideo.Location = new System.Drawing.Point(492, 6);
            this.btnSecondAngleVideo.Name = "btnSecondAngleVideo";
            this.btnSecondAngleVideo.Size = new System.Drawing.Size(84, 23);
            this.btnSecondAngleVideo.TabIndex = 16;
            this.btnSecondAngleVideo.Text = "Side Views";
            this.btnSecondAngleVideo.UseVisualStyleBackColor = true;
            this.btnSecondAngleVideo.Click += new System.EventHandler(this.btnSecondAngleVideo_Click);
            // 
            // btnEraseObjects
            // 
            this.btnEraseObjects.Enabled = false;
            this.btnEraseObjects.Location = new System.Drawing.Point(308, 6);
            this.btnEraseObjects.Name = "btnEraseObjects";
            this.btnEraseObjects.Size = new System.Drawing.Size(86, 23);
            this.btnEraseObjects.TabIndex = 15;
            this.btnEraseObjects.Text = "Erase Objects";
            this.btnEraseObjects.UseVisualStyleBackColor = true;
            this.btnEraseObjects.Click += new System.EventHandler(this.btnEraseObjects_Click);
            // 
            // btnDrawObjects
            // 
            this.btnDrawObjects.Enabled = false;
            this.btnDrawObjects.Location = new System.Drawing.Point(221, 6);
            this.btnDrawObjects.Name = "btnDrawObjects";
            this.btnDrawObjects.Size = new System.Drawing.Size(79, 23);
            this.btnDrawObjects.TabIndex = 14;
            this.btnDrawObjects.Text = "Draw Objects";
            this.btnDrawObjects.UseVisualStyleBackColor = true;
            this.btnDrawObjects.Click += new System.EventHandler(this.btnDrawObject_Click);
            // 
            // btnShowOriginalVideo
            // 
            this.btnShowOriginalVideo.Enabled = false;
            this.btnShowOriginalVideo.Location = new System.Drawing.Point(402, 6);
            this.btnShowOriginalVideo.Name = "btnShowOriginalVideo";
            this.btnShowOriginalVideo.Size = new System.Drawing.Size(84, 23);
            this.btnShowOriginalVideo.TabIndex = 13;
            this.btnShowOriginalVideo.Text = "Original Video";
            this.btnShowOriginalVideo.UseVisualStyleBackColor = true;
            this.btnShowOriginalVideo.Click += new System.EventHandler(this.btnShowOriginalVideo_Click);
            // 
            // btnSetGridBound
            // 
            this.btnSetGridBound.Enabled = false;
            this.btnSetGridBound.Location = new System.Drawing.Point(7, 6);
            this.btnSetGridBound.Name = "btnSetGridBound";
            this.btnSetGridBound.Size = new System.Drawing.Size(96, 23);
            this.btnSetGridBound.TabIndex = 12;
            this.btnSetGridBound.Text = "Get Homography";
            this.btnSetGridBound.UseVisualStyleBackColor = true;
            this.btnSetGridBound.Click += new System.EventHandler(this.btnSetGridBound_Click);
            // 
            // btnCopyPrevLog
            // 
            this.btnCopyPrevLog.Enabled = false;
            this.btnCopyPrevLog.Location = new System.Drawing.Point(462, 5);
            this.btnCopyPrevLog.Name = "btnCopyPrevLog";
            this.btnCopyPrevLog.Size = new System.Drawing.Size(97, 23);
            this.btnCopyPrevLog.TabIndex = 13;
            this.btnCopyPrevLog.Text = "Copy prev log";
            this.btnCopyPrevLog.UseVisualStyleBackColor = true;
            this.btnCopyPrevLog.Click += new System.EventHandler(this.btnCopyPrevLog_Click);
            // 
            // imageBox_Homography
            // 
            this.imageBox_Homography.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.imageBox_Homography.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageBox_Homography.Cursor = System.Windows.Forms.Cursors.Cross;
            this.imageBox_Homography.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBox_Homography.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.Minimum;
            this.imageBox_Homography.Location = new System.Drawing.Point(0, 0);
            this.imageBox_Homography.Name = "imageBox_Homography";
            this.imageBox_Homography.Size = new System.Drawing.Size(778, 494);
            this.imageBox_Homography.TabIndex = 2;
            this.imageBox_Homography.TabStop = false;
            this.imageBox_Homography.SizeChanged += new System.EventHandler(this.imageBox_Homography_SizeChanged);
            this.imageBox_Homography.MouseClick += new System.Windows.Forms.MouseEventHandler(this.imageBox_Homography_MouseClick);
            this.imageBox_Homography.MouseMove += new System.Windows.Forms.MouseEventHandler(this.imageBox_Homography_MouseMove);
            // 
            // btnVideoForward
            // 
            this.btnVideoForward.Enabled = false;
            this.btnVideoForward.Location = new System.Drawing.Point(380, 5);
            this.btnVideoForward.Name = "btnVideoForward";
            this.btnVideoForward.Size = new System.Drawing.Size(70, 23);
            this.btnVideoForward.TabIndex = 14;
            this.btnVideoForward.Text = ">>";
            this.btnVideoForward.UseVisualStyleBackColor = true;
            this.btnVideoForward.Click += new System.EventHandler(this.btnVideoForward_Click);
            // 
            // btnVideoPrev
            // 
            this.btnVideoPrev.Enabled = false;
            this.btnVideoPrev.Location = new System.Drawing.Point(289, 5);
            this.btnVideoPrev.Name = "btnVideoPrev";
            this.btnVideoPrev.Size = new System.Drawing.Size(70, 23);
            this.btnVideoPrev.TabIndex = 15;
            this.btnVideoPrev.Text = "<<";
            this.btnVideoPrev.UseVisualStyleBackColor = true;
            this.btnVideoPrev.Click += new System.EventHandler(this.btnVideoPrev_Click);
            // 
            // lbCurrentTimeStamp
            // 
            this.lbCurrentTimeStamp.AutoSize = true;
            this.lbCurrentTimeStamp.Location = new System.Drawing.Point(660, 10);
            this.lbCurrentTimeStamp.Name = "lbCurrentTimeStamp";
            this.lbCurrentTimeStamp.Size = new System.Drawing.Size(43, 13);
            this.lbCurrentTimeStamp.TabIndex = 16;
            this.lbCurrentTimeStamp.Text = "0:00:00";
            // 
            // lbTotalDuration
            // 
            this.lbTotalDuration.AutoSize = true;
            this.lbTotalDuration.Location = new System.Drawing.Point(719, 10);
            this.lbTotalDuration.Name = "lbTotalDuration";
            this.lbTotalDuration.Size = new System.Drawing.Size(43, 13);
            this.lbTotalDuration.TabIndex = 17;
            this.lbTotalDuration.Text = "0:00:00";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(707, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(12, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "/";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.imageBox_Homography);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(780, 496);
            this.panel2.TabIndex = 19;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.cbPeripheral);
            this.panel3.Controls.Add(this.btnVideoPrev);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.btnCopyPrevLog);
            this.panel3.Controls.Add(this.btnVideoForward);
            this.panel3.Controls.Add(this.lbCurrentTimeStamp);
            this.panel3.Controls.Add(this.btnLoad);
            this.panel3.Controls.Add(this.lbTotalDuration);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 463);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(778, 31);
            this.panel3.TabIndex = 16;
            // 
            // cbPeripheral
            // 
            this.cbPeripheral.AutoSize = true;
            this.cbPeripheral.Location = new System.Drawing.Point(3, 5);
            this.cbPeripheral.Name = "cbPeripheral";
            this.cbPeripheral.Size = new System.Drawing.Size(146, 17);
            this.cbPeripheral.TabIndex = 19;
            this.cbPeripheral.Text = "Show Teacher Peripheral";
            this.cbPeripheral.UseVisualStyleBackColor = true;
            this.cbPeripheral.CheckedChanged += new System.EventHandler(this.cbPeripheral_CheckedChanged);
            // 
            // FormAnalyzer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1148, 574);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataGridView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormAnalyzer";
            this.Text = "Spatial Analyzer (Position Locator)";
            this.Load += new System.EventHandler(this.FormAnalyzer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox_Homography)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox tbLog;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSetGridBound;
        private Emgu.CV.UI.ImageBox imageBox_Homography;
        private System.Windows.Forms.Button btnCopyPrevLog;
        private System.Windows.Forms.Button btnVideoForward;
        private System.Windows.Forms.Button btnVideoPrev;
        private System.Windows.Forms.Label lbCurrentTimeStamp;
        private System.Windows.Forms.Label lbTotalDuration;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnShowOriginalVideo;
        private System.Windows.Forms.CheckBox cbPeripheral;
        private System.Windows.Forms.Button btnDrawObjects;
        private System.Windows.Forms.Button btnEraseObjects;
        private System.Windows.Forms.DataGridViewTextBoxColumn Individual;
        private System.Windows.Forms.DataGridViewTextBoxColumn X;
        private System.Windows.Forms.DataGridViewTextBoxColumn Y;
        private System.Windows.Forms.DataGridViewTextBoxColumn Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn Note;
        private System.Windows.Forms.Button btnSecondAngleVideo;
    }
}

