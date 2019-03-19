namespace SpatialAnalyzer
{
    partial class FormGridBound
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGridBound));
            this.imageBox1 = new Emgu.CV.UI.ImageBox();
            this.btnGetHomography = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // imageBox1
            // 
            this.imageBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageBox1.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bicubic;
            this.imageBox1.Location = new System.Drawing.Point(25, 3);
            this.imageBox1.Name = "imageBox1";
            this.imageBox1.Size = new System.Drawing.Size(1000, 730);
            this.imageBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.imageBox1.TabIndex = 2;
            this.imageBox1.TabStop = false;
            this.imageBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.imageBox1_Paint);
            this.imageBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.imageBox1_MouseClick);
            this.imageBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.imageBox1_MouseMove);
            // 
            // btnGetHomography
            // 
            this.btnGetHomography.Location = new System.Drawing.Point(567, 750);
            this.btnGetHomography.Name = "btnGetHomography";
            this.btnGetHomography.Size = new System.Drawing.Size(120, 23);
            this.btnGetHomography.TabIndex = 3;
            this.btnGetHomography.Text = "Get Homography";
            this.btnGetHomography.UseVisualStyleBackColor = true;
            this.btnGetHomography.Click += new System.EventHandler(this.btnGetHomography_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(335, 750);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(120, 23);
            this.btnReset.TabIndex = 4;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // FormGridBound
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1049, 778);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnGetHomography);
            this.Controls.Add(this.imageBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormGridBound";
            this.Text = "Get Grid Bounds";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FormHomography_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Emgu.CV.UI.ImageBox imageBox1;
        private System.Windows.Forms.Button btnGetHomography;
        private System.Windows.Forms.Button btnReset;
    }
}