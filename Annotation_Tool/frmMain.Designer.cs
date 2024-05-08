
namespace VideoAnnotator
{
    public partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            buttonLoad = new System.Windows.Forms.Button();
            imageList1 = new System.Windows.Forms.ImageList(components);
            openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            lblMem = new System.Windows.Forms.Label();
            panel1 = new System.Windows.Forms.Panel();
            label10 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            check_summary_button = new System.Windows.Forms.Button();
            button4 = new System.Windows.Forms.Button();
            button3 = new System.Windows.Forms.Button();
            button2 = new System.Windows.Forms.Button();
            button1 = new System.Windows.Forms.Button();
            lblFrame = new System.Windows.Forms.TextBox();
            buttonPrev = new System.Windows.Forms.Button();
            buttonNext = new System.Windows.Forms.Button();
            buttonPlay = new System.Windows.Forms.Button();
            hScrollBar1 = new System.Windows.Forms.HScrollBar();
            label3 = new System.Windows.Forms.Label();
            panel2 = new System.Windows.Forms.Panel();
            imageBox1 = new Emgu.CV.UI.ImageBox();
            save_txt = new System.Windows.Forms.Button();
            lblFile = new System.Windows.Forms.TextBox();
            lblDir = new System.Windows.Forms.TextBox();
            txtUser = new System.Windows.Forms.TextBox();
            label7 = new System.Windows.Forms.Label();
            timer1 = new System.Windows.Forms.Timer(components);
            panel4 = new System.Windows.Forms.Panel();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)imageBox1).BeginInit();
            SuspendLayout();
            // 
            // buttonLoad
            // 
            resources.ApplyResources(buttonLoad, "buttonLoad");
            buttonLoad.Name = "buttonLoad";
            buttonLoad.UseVisualStyleBackColor = true;
            buttonLoad.Click += button1_Click;
            // 
            // imageList1
            // 
            imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            resources.ApplyResources(imageList1, "imageList1");
            imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            //label2.Click += label2_Click;
            // 
            // label4
            // 
            resources.ApplyResources(label4, "label4");
            label4.Name = "label4";
            
            // 
            // lblMem
            // 
            resources.ApplyResources(lblMem, "lblMem");
            lblMem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            lblMem.Name = "lblMem";
           
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.SystemColors.ButtonFace;
            panel1.Controls.Add(label10);
            panel1.Controls.Add(label9);
            panel1.Controls.Add(label8);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(check_summary_button);
            panel1.Controls.Add(button4);
            panel1.Controls.Add(button3);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(lblFrame);
            panel1.Controls.Add(buttonPrev);
            panel1.Controls.Add(buttonNext);
            panel1.Controls.Add(buttonPlay);
            panel1.Controls.Add(hScrollBar1);
            resources.ApplyResources(panel1, "panel1");
            panel1.Name = "panel1";
           
            // 
            // label10
            // 
            resources.ApplyResources(label10, "label10");
            label10.Name = "label10";
            // 
            // label9
            // 
            resources.ApplyResources(label9, "label9");
            label9.Name = "label9";
            // 
            // label8
            // 
            resources.ApplyResources(label8, "label8");
            label8.Name = "label8";
            // 
            // label5
            // 
            resources.ApplyResources(label5, "label5");
            label5.Name = "label5";
            // 
            // button5
            // 
            resources.ApplyResources(check_summary_button, "check_summary_button");
            check_summary_button.Name = "check_summary_button";
            check_summary_button.UseVisualStyleBackColor = true;
            check_summary_button.Click += check_summary_button_Click_1;
            // 
            // button4
            // 
            resources.ApplyResources(button4, "button4");
            button4.Name = "button4";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button3
            // 
            resources.ApplyResources(button3, "button3");
            button3.Name = "button3";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button2
            // 
            resources.ApplyResources(button2, "button2");
            button2.Name = "button2";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            resources.ApplyResources(button1, "button1");
            button1.Name = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click_1;
            // 
            // lblFrame
            // 
            resources.ApplyResources(lblFrame, "lblFrame");
            lblFrame.BackColor = System.Drawing.SystemColors.ButtonFace;
            lblFrame.BorderStyle = System.Windows.Forms.BorderStyle.None;
            lblFrame.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            lblFrame.Name = "lblFrame";
           
            // 
            // buttonPrev
            // 
            resources.ApplyResources(buttonPrev, "buttonPrev");
            buttonPrev.Name = "buttonPrev";
            buttonPrev.UseVisualStyleBackColor = true;
            buttonPrev.Click += buttonPrev_Click;
            // 
            // buttonNext
            // 
            resources.ApplyResources(buttonNext, "buttonNext");
            buttonNext.Name = "buttonNext";
            buttonNext.UseVisualStyleBackColor = true;
            buttonNext.Click += buttonNext_Click;
            // 
            // buttonPlay
            // 
            resources.ApplyResources(buttonPlay, "buttonPlay");
            buttonPlay.Name = "buttonPlay";
            buttonPlay.UseVisualStyleBackColor = true;
            buttonPlay.Click += buttonPlay_Click;
            // 
            // hScrollBar1
            // 
            resources.ApplyResources(hScrollBar1, "hScrollBar1");
            hScrollBar1.LargeChange = 1;
            hScrollBar1.Maximum = 0;
            hScrollBar1.Name = "hScrollBar1";
            hScrollBar1.Scroll += hScrollBar1_Scroll;
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            label3.Name = "label3";
            // 
            // panel2
            // 
            panel2.Controls.Add(label3);
            panel2.Controls.Add(imageBox1);
            panel2.Controls.Add(save_txt);
            panel2.Controls.Add(lblFile);
            panel2.Controls.Add(lblDir);
            panel2.Controls.Add(txtUser);
            panel2.Controls.Add(label7);
            panel2.Controls.Add(buttonLoad);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(lblMem);
            resources.ApplyResources(panel2, "panel2");
            panel2.Name = "panel2";
            // 
            // imageBox1
            // 
            resources.ApplyResources(imageBox1, "imageBox1");
            imageBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            imageBox1.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.Minimum;
            imageBox1.Name = "imageBox1";
            imageBox1.TabStop = false;
            // 
            // save_txt
            // 
            resources.ApplyResources(save_txt, "save_txt");
            save_txt.Name = "save_txt";
            save_txt.UseVisualStyleBackColor = true;
            save_txt.Click += save_txt_Click;
            // 
            // lblFile
            // 
            resources.ApplyResources(lblFile, "lblFile");
            lblFile.BackColor = System.Drawing.SystemColors.ControlLight;
            lblFile.Name = "lblFile";
            lblFile.ReadOnly = true;
            
            // 
            // lblDir
            // 
            resources.ApplyResources(lblDir, "lblDir");
            lblDir.BackColor = System.Drawing.SystemColors.ControlLight;
            lblDir.Name = "lblDir";
            lblDir.ReadOnly = true;
            
            // 
            // txtUser
            // 
            resources.ApplyResources(txtUser, "txtUser");
            txtUser.Name = "txtUser";
            
            // 
            // label7
            // 
            resources.ApplyResources(label7, "label7");
            label7.Name = "label7";
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // panel4
            // 
            panel4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            resources.ApplyResources(panel4, "panel4");
            panel4.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            panel4.Name = "panel4";
        
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.SystemColors.ControlLight;
            Controls.Add(panel4);
            Controls.Add(panel2);
            Controls.Add(panel1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            Load += Form1_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)imageBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Button buttonLoad;

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblMem;
        //private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonPrev;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Button buttonPlay;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox lblFrame;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox lblFile;
        private System.Windows.Forms.TextBox lblDir;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button save_txt;
        private System.Windows.Forms.Panel panel4;
        private Emgu.CV.UI.ImageBox imageBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button check_summary_button;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
    }
}

