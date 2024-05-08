
namespace VideoAnnotator
{
    partial class Form2
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
            components = new System.ComponentModel.Container();
            hScrollBar1 = new System.Windows.Forms.HScrollBar();
            timer1 = new System.Windows.Forms.Timer(components);
            imageBox1 = new Emgu.CV.UI.ImageBox();
            buttonPlay = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)imageBox1).BeginInit();
            SuspendLayout();
            // 
            // hScrollBar1
            // 
            hScrollBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            hScrollBar1.Location = new System.Drawing.Point(0, 448);
            hScrollBar1.Name = "hScrollBar1";
            hScrollBar1.Size = new System.Drawing.Size(708, 28);
            hScrollBar1.TabIndex = 0;
            hScrollBar1.Scroll += hScrollBar1_Scroll;
            // 
            // imageBox1
            // 
            imageBox1.Dock = System.Windows.Forms.DockStyle.Top;
            imageBox1.Location = new System.Drawing.Point(0, 0);
            imageBox1.Name = "imageBox1";
            imageBox1.Size = new System.Drawing.Size(708, 379);
            imageBox1.TabIndex = 2;
            imageBox1.TabStop = false;
            // 
            // buttonPlay
            // 
            buttonPlay.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            buttonPlay.Enabled = false;
            buttonPlay.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            buttonPlay.Location = new System.Drawing.Point(326, 418);
            buttonPlay.Name = "buttonPlay";
            buttonPlay.Size = new System.Drawing.Size(45, 27);
            buttonPlay.TabIndex = 30;
            buttonPlay.Text = "▶";
            buttonPlay.UseVisualStyleBackColor = true;
            buttonPlay.Click += buttonPlay_Click_1;
            // 
            // label1
            // 
            label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(12, 382);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(13, 15);
            label1.TabIndex = 31;
            label1.Text = "0";
           
            // 
            // Form2
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(708, 476);
            Controls.Add(label1);
            Controls.Add(buttonPlay);
            Controls.Add(imageBox1);
            Controls.Add(hScrollBar1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form2";
            TopMost = true;
           
            ((System.ComponentModel.ISupportInitialize)imageBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.Timer timer1;
        private Emgu.CV.UI.ImageBox imageBox1;
        private System.Windows.Forms.Button buttonPlay;
        private System.Windows.Forms.Label label1;
    }
}