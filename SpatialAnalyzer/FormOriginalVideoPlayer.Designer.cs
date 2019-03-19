namespace SpatialAnalyzer
{
    partial class FormOriginalVideoPlayer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormOriginalVideoPlayer));
            this.imageBoxOrigVideo = new Emgu.CV.UI.ImageBox();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxOrigVideo)).BeginInit();
            this.SuspendLayout();
            // 
            // imageBoxOrigVideo
            // 
            this.imageBoxOrigVideo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBoxOrigVideo.Location = new System.Drawing.Point(0, 0);
            this.imageBoxOrigVideo.Name = "imageBoxOrigVideo";
            this.imageBoxOrigVideo.Size = new System.Drawing.Size(800, 450);
            this.imageBoxOrigVideo.TabIndex = 2;
            this.imageBoxOrigVideo.TabStop = false;
            this.imageBoxOrigVideo.SizeChanged += new System.EventHandler(this.imageBoxOrigVideo_SizeChanged);
            // 
            // FormOriginalVideoPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.imageBoxOrigVideo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormOriginalVideoPlayer";
            this.Text = "FormOriginalVideoPlayer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormOriginalVideoPlayer_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxOrigVideo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Emgu.CV.UI.ImageBox imageBoxOrigVideo;
    }
}