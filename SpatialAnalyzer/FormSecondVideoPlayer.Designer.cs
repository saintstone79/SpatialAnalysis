namespace SpatialAnalyzer
{
    partial class FormSecondVideoPlayer
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
            this.imageViewer = new Emgu.CV.UI.ImageBox();
            this.panelBottomControllers = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonPrev10 = new System.Windows.Forms.Button();
            this.lbCurrentTimeStamp = new System.Windows.Forms.Label();
            this.buttonPrev30 = new System.Windows.Forms.Button();
            this.lbTotalDuration = new System.Windows.Forms.Label();
            this.buttonNext30 = new System.Windows.Forms.Button();
            this.buttonNext10 = new System.Windows.Forms.Button();
            this.buttonPlay = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.imageViewer)).BeginInit();
            this.panelBottomControllers.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageViewer
            // 
            this.imageViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageViewer.Location = new System.Drawing.Point(0, 0);
            this.imageViewer.Name = "imageViewer";
            this.imageViewer.Size = new System.Drawing.Size(650, 357);
            this.imageViewer.TabIndex = 2;
            this.imageViewer.TabStop = false;
            this.imageViewer.SizeChanged += new System.EventHandler(this.imageViewer_SizeChanged);
            // 
            // panelBottomControllers
            // 
            this.panelBottomControllers.Controls.Add(this.label3);
            this.panelBottomControllers.Controls.Add(this.buttonPrev10);
            this.panelBottomControllers.Controls.Add(this.lbCurrentTimeStamp);
            this.panelBottomControllers.Controls.Add(this.buttonPrev30);
            this.panelBottomControllers.Controls.Add(this.lbTotalDuration);
            this.panelBottomControllers.Controls.Add(this.buttonNext30);
            this.panelBottomControllers.Controls.Add(this.buttonNext10);
            this.panelBottomControllers.Controls.Add(this.buttonPlay);
            this.panelBottomControllers.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottomControllers.Location = new System.Drawing.Point(0, 325);
            this.panelBottomControllers.Name = "panelBottomControllers";
            this.panelBottomControllers.Size = new System.Drawing.Size(650, 32);
            this.panelBottomControllers.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(592, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(12, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "/";
            // 
            // buttonPrev10
            // 
            this.buttonPrev10.Enabled = false;
            this.buttonPrev10.Location = new System.Drawing.Point(217, 5);
            this.buttonPrev10.Name = "buttonPrev10";
            this.buttonPrev10.Size = new System.Drawing.Size(48, 23);
            this.buttonPrev10.TabIndex = 4;
            this.buttonPrev10.Text = "-10";
            this.buttonPrev10.UseVisualStyleBackColor = true;
            this.buttonPrev10.Click += new System.EventHandler(this.buttonPrev10_Click);
            // 
            // lbCurrentTimeStamp
            // 
            this.lbCurrentTimeStamp.AutoSize = true;
            this.lbCurrentTimeStamp.Location = new System.Drawing.Point(545, 9);
            this.lbCurrentTimeStamp.Name = "lbCurrentTimeStamp";
            this.lbCurrentTimeStamp.Size = new System.Drawing.Size(43, 13);
            this.lbCurrentTimeStamp.TabIndex = 19;
            this.lbCurrentTimeStamp.Text = "0:00:00";
            // 
            // buttonPrev30
            // 
            this.buttonPrev30.Enabled = false;
            this.buttonPrev30.Location = new System.Drawing.Point(157, 5);
            this.buttonPrev30.Name = "buttonPrev30";
            this.buttonPrev30.Size = new System.Drawing.Size(48, 23);
            this.buttonPrev30.TabIndex = 3;
            this.buttonPrev30.Text = "-30";
            this.buttonPrev30.UseVisualStyleBackColor = true;
            this.buttonPrev30.Click += new System.EventHandler(this.buttonPrev30_Click);
            // 
            // lbTotalDuration
            // 
            this.lbTotalDuration.AutoSize = true;
            this.lbTotalDuration.Location = new System.Drawing.Point(604, 9);
            this.lbTotalDuration.Name = "lbTotalDuration";
            this.lbTotalDuration.Size = new System.Drawing.Size(43, 13);
            this.lbTotalDuration.TabIndex = 20;
            this.lbTotalDuration.Text = "0:00:00";
            // 
            // buttonNext30
            // 
            this.buttonNext30.Enabled = false;
            this.buttonNext30.Location = new System.Drawing.Point(397, 5);
            this.buttonNext30.Name = "buttonNext30";
            this.buttonNext30.Size = new System.Drawing.Size(48, 23);
            this.buttonNext30.TabIndex = 2;
            this.buttonNext30.Text = "+30";
            this.buttonNext30.UseVisualStyleBackColor = true;
            this.buttonNext30.Click += new System.EventHandler(this.buttonNext30_Click);
            // 
            // buttonNext10
            // 
            this.buttonNext10.Enabled = false;
            this.buttonNext10.Location = new System.Drawing.Point(337, 5);
            this.buttonNext10.Name = "buttonNext10";
            this.buttonNext10.Size = new System.Drawing.Size(48, 23);
            this.buttonNext10.TabIndex = 1;
            this.buttonNext10.Text = "+10";
            this.buttonNext10.UseVisualStyleBackColor = true;
            this.buttonNext10.Click += new System.EventHandler(this.buttonNext10_Click);
            // 
            // buttonPlay
            // 
            this.buttonPlay.Enabled = false;
            this.buttonPlay.Location = new System.Drawing.Point(277, 5);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(48, 23);
            this.buttonPlay.TabIndex = 0;
            this.buttonPlay.Text = ">";
            this.buttonPlay.UseVisualStyleBackColor = true;
            this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // FormSecondVideoPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 357);
            this.Controls.Add(this.panelBottomControllers);
            this.Controls.Add(this.imageViewer);
            this.Name = "FormSecondVideoPlayer";
            this.Text = "Second Video Player";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSecondVideoPlayer_FormClosing);
            this.Load += new System.EventHandler(this.FormSecondVideoPlayer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imageViewer)).EndInit();
            this.panelBottomControllers.ResumeLayout(false);
            this.panelBottomControllers.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Emgu.CV.UI.ImageBox imageViewer;
        private System.Windows.Forms.Panel panelBottomControllers;
        private System.Windows.Forms.Button buttonPrev10;
        private System.Windows.Forms.Button buttonPrev30;
        private System.Windows.Forms.Button buttonNext30;
        private System.Windows.Forms.Button buttonNext10;
        private System.Windows.Forms.Button buttonPlay;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbCurrentTimeStamp;
        private System.Windows.Forms.Label lbTotalDuration;
    }
}