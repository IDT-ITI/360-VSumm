using System;
using System.Collections.Generic;

using System.Drawing;

using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

using Emgu.CV;
using Emgu.CV.Structure;

using Emgu.CV.CvEnum;


namespace VideoAnnotator
{


    public partial class Form1 : Form
    {
        int count = 0;
        private List<PictureBox> pictureBoxes = new List<PictureBox>();
        private List<string> scores = new List<string>();

        private List<System.Windows.Forms.Label> labels = new List<System.Windows.Forms.Label>();
        private List<System.Windows.Forms.Label> labelbar = new List<System.Windows.Forms.Label>();
        private Point startPoint;
        private Point endPoint;

        private bool moveUp = false;
        private bool moveDown = false;
        private bool tab = false;
        private bool moveback = false;
        private bool backtab = false;
        private List<Point> subshots = new List<Point>();
        private List<(double, double)> finalfragments = new List<(double, double)>();
        private PictureBox box = new PictureBox();
        int currentFrameCount = -1;
        int currentFrameH = -1;
        int currentFrameW = -1;
        List<Mat> currentFrames;
        List<int> currentXAnnots;
        List<int> currentYAnnots;
        int countAnnotators = 0;
        int countVideos = 0;
        private String vidPath;
        private Mat originalMat; // Replace this with your Emgu.CV.Mat
        private Mat reshapedMat;

        Image<Bgr, byte> doneStatusImg = new Image<Bgr, byte>(new Size(1, 1));

        bool hasLoadedFile = false;
        string loadedFile = "";
        bool flag1 = true;
        private int totalfragments = 0;
        int range1 = 0;


        public Form1()
        {
            InitializeComponent();
            this.MaximizeBox = true;
            panel4.MouseDown += new MouseEventHandler(panel4_MouseDown);
            //panel4.MouseMove += new MouseEventHandler(panel4_MouseMove);
            imageBox1.Enabled = false;
            imageBox1.Visible = false;

            panel4.MouseUp += new MouseEventHandler(panel4_MouseUp);
            this.KeyPreview = true;
            //this.KeyUp += form_KeyDown;
            this.Text = "Annotation Tool";
            check_summary_button.Enabled = false;
            save_txt.Enabled = false;
            save_txt.Visible = false;
            lblDir.Enabled = false;
            lblDir.Visible = false;
            lblFile.Enabled = false;
            lblFile.Visible = false;
            lblMem.Enabled = false;
            lblMem.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            label4.Visible = false;
            label1.Enabled = false;
            label2.Enabled = false;
            label4.Enabled = false;
            label3.Enabled = false;
            label3.Visible = false;
            label5.Visible = false;
            label8.Visible = false;
            label9.Visible = false;
            label10.Visible = false;
            lblFrame.Enabled = false;
            check_summary_button.Visible = false;
            this.Resize += Form1_Resize;
            
            

            //button2.Click += new EventHandler(button2_Click);
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
           
            int range2 = 0;
            int panelHeight = panel4.ClientSize.Height;
            if (pictureBoxes.Count > 0)
            {
                foreach (var pictureBox in pictureBoxes)
                {
                    //pictureBox.Click += PictureBox_Click;
                    pictureBox.Location = new Point(range2 + 1, (int)(panelHeight / 2));
                    range2 += range1;
                }
                range2 = 0;
                foreach (var item in labels)
                {
                    item.Location = new Point(range2, (int)(panelHeight / 2) + 6);
                    range2 += range1;
                }
                    

            }
            show_frame();
            
        }

        public void initDoneBar()
        {
            doneStatusImg = new Image<Bgr, byte>(new Size(currentFrameCount, 1));
        }
        public void setDoneBar()
        {

        }


        public void updateGUI(bool active)
        {


            if (active)
            {
                check_summary_button.Enabled = false;
                save_txt.Enabled = false;
                save_txt.Visible = true;
                lblDir.Enabled = false;
                lblDir.Visible = true;
                lblFile.Enabled = false;
                lblFile.Visible = true;
                lblMem.Visible = true;
                lblMem.Enabled = false;

                label1.Visible = true;
                label2.Visible = true;
                label4.Visible = true;

                button1.TabStop = false;
                button2.TabStop = false;
                button3.TabStop = false;
                button4.TabStop = false;
                //buttSave.TabStop = false;
                //buttonReset.TabStop = false;
                buttonPlay.TabStop = false;
                buttonNext.TabStop = false;
                buttonPrev.TabStop = false;
                buttonLoad.TabStop = false;
                //textBox1.TabStop = false;
                save_txt.Enabled = false;
                save_txt.TabStop = false;
                lblFrame.TabStop = false;
                txtUser.TabStop = false;
                lblFile.TabStop = false;
                label5.Visible = true;
                label8.Visible = true;
                label9.Visible = true;
                label10.Visible = true;
                imageBox1.Visible = true;
                check_summary_button.Enabled = true;
                check_summary_button.Visible = true;
                save_txt.Enabled = false;
                save_txt.Visible = true;
                label3.Visible = true;



                lblDir.TabStop = false;
                check_summary_button.Enabled = true;
                lblFile.KeyDown += (sender, e) =>
                {
                    if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
                    {
                        e.Handled = true; // Prevent the default behavior of the arrow keys
                    }
                };
                hScrollBar1.Enabled = true;
                hScrollBar1.KeyDown += (sender, e) =>
                {
                    if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
                    {
                        e.Handled = true; // Prevent the default behavior of the arrow keys
                    }
                };
                panel4.Enabled = true;
                //buttSave.Enabled = true;
                //buttonReset.Enabled = true;
                buttonNext.Enabled = true;
                buttonPrev.Enabled = true;
                buttonPlay.Enabled = true;
                buttonLoad.Enabled = true;
                buttonLoad.Visible = true;
                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
                save_txt.Enabled = true;
                button1.Enabled = true;
                button1.Visible = true;
                //lblBox.Enabled = true;
                lblFrame.Enabled = true;
                lblMem.Enabled = true;
                imageBox1.Enabled = true;

                button1.Visible = true;
                label3.Enabled = true;
                //imageBox1.Height = currentFrameH ;
                //imageBox1.Width = currentFrameW ;

                lblFile.KeyDown += (sender, e) =>
                {
                    if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
                    {
                        e.SuppressKeyPress = true; // Prevent up and down arrow keys from navigating the text
                    }
                };


                hScrollBar1.Minimum = 0;
                hScrollBar1.Maximum = currentFrameCount - 1;
                hScrollBar1.Value = 0;
                hScrollBar1.TabStop = false;
                if (flag1 == true)
                {

                    this.Size = Screen.PrimaryScreen.WorkingArea.Size;

                    this.Width = Screen.PrimaryScreen.WorkingArea.Width;
                    panel2.Height = (int)Math.Round(this.Height * 0.7);
                    panel2.Width = this.Width;
                    this.Height = Screen.PrimaryScreen.WorkingArea.Height - (int)(this.Height / 10);
       

                    panel4.Width = this.Width + (int)((16/this.Width)*this.Width);

                    imageBox1.Location = new Point((int)(this.Width / 4), 0);
                    label3.Location = new Point((int)(this.Width * 0.01), (int)((this.Width / 2) * 3 / 4) - (int)(this.Height * 0.04));



                }
                buttonLoad.Enabled = true;
                buttonLoad.Visible = true;
                imageBox1.Height = (int)((this.Width / 2) * 3 / 4);
                imageBox1.Width = (int)(this.Width / 2);



                flag1 = false;

            }
            else
            {
                hScrollBar1.Enabled = false;
                buttonNext.Enabled = false;
                buttonPrev.Enabled = false;
                buttonPlay.Enabled = false;
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                save_txt.Enabled = false;
                label3.Enabled = false;
                check_summary_button.Enabled = true;
                button1.Visible = true;
                buttonLoad.Enabled = true;
                buttonLoad.Visible = true;
                label3.Text = "0";
                lblFile.KeyDown += (sender, e) =>
                {
                    if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
                    {
                        e.Handled = true;
                    }
                };
                hScrollBar1.KeyDown += (sender, e) =>
                {
                    if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
                    {
                        e.Handled = true;
                    }
                };
                //lblBox.Text = "";
                lblDir.Text = "";
                //lblMode.Text = "";
                lblFile.Text = "No file loaded";
                lblFrame.Text = "0/0";
                lblMem.Text = "0 MB";
                //.Enabled = false;
                lblFrame.Enabled = false;
                lblMem.Enabled = false;
                hScrollBar1.Minimum = 0;
                hScrollBar1.Maximum = 1;
                hScrollBar1.Value = 0;
            }
            System.Windows.Forms.Application.DoEvents();
        }

        // load video
        public void loadVideo(string vidPath)
        {
            // reset info
            currentFrames = new List<Mat>();


            // open video
            //MessageBox.Show($"{vidPath}");
            VideoCapture vidCap = new VideoCapture(vidPath);
            int currentFPS = Convert.ToInt32(vidCap.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.Fps));
            currentFrameCount = Convert.ToInt32(vidCap.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.FrameCount));
            currentFrameH = Convert.ToInt32(vidCap.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.FrameHeight));
            currentFrameW = Convert.ToInt32(vidCap.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.FrameWidth));
            //double durationInSeconds = currentFrameCount / currentFPS;

            // read video
            currentFrameCount = 0;
            bool Reading = true;
            //int count11 = 0;
            while (Reading)
            {
                Mat frame = vidCap.QueryFrame();
                if (frame != null)
                {

                    CvInvoke.Resize(frame, frame, new Size(320, 240));
                    currentFrameCount++;
                    currentFrames.Add(frame.Clone());
                    //MessageBox.Show(frame.Width.ToString());

                    lblFrame.Text = "loading " + Convert.ToString(currentFrameCount);
                    double ls = ((currentFrameCount * 3.0 * currentFrameW * currentFrameH * 2.0) / 1024.0) / 1024.0;
                    lblMem.Text = Convert.ToString(Convert.ToInt32(ls)) + "MB";

                    if (currentFrameCount % 50 == 0)
                    {
                        lblFrame.Refresh();
                        lblMem.Refresh();
                    }

                }
                else
                {
                    Reading = false;
                }
            }


            // create done image
            initDoneBar();
            setDoneBar();

            // update GUI
            timer1.Interval = Convert.ToInt32(0.75 * (1000.0 / currentFPS));
            updateGUI(true);
            System.Windows.Forms.Application.DoEvents();
            show_frame();
            System.Windows.Forms.Application.DoEvents();
            imageBox1.Invalidate();
            System.Windows.Forms.Application.DoEvents();

            string filePath1 = vidPath.ToString();

            int underscoreIndex = filePath1.LastIndexOf('_');
            int periodIndex = filePath1.LastIndexOf('.');
            string numberString = filePath1.Substring(underscoreIndex + 1, periodIndex - underscoreIndex - 1);

            int panelWidth = this.Width - 8;
            int panelHeight = panel4.ClientSize.Height;

            string newFileName = $"subshots_video_{numberString}.txt";

            // Find the last occurrence of the directory separator character ('/') or ('\')
            int lastSeparatorIndex = filePath1.LastIndexOf('/') != -1 ? filePath1.LastIndexOf('/') : filePath1.LastIndexOf('\\');


            // Extract the directory part and concatenate the new file name
            string directoryPart = filePath1.Substring(0, lastSeparatorIndex + 1);
            string newPath = directoryPart + newFileName;

            int a = 0;
            foreach (string line in File.ReadLines(newPath))
            {
                string[] items = line.Split(' ');
                Point points = new Point(int.Parse(items[0]), int.Parse(items[1]));
                subshots.Add(points);
                a = int.Parse(items[1]);
            }
            if (a > 6000)
            {
                a = (int)Math.Round(this.Width*(0.00208));
            }
            else
            {
                a = (int)Math.Round(this.Width * (0.00315));
            }
        
            int count1 = 0;
            int count = 0;
            for (int i = 0; i < subshots.Count; i++)
            {

                double x = subshots[i].Y - subshots[i].X;
                double y = x / 60;
                double z = (int)(y) - 1;
                double decimals = y - z;
                if (decimals > 1.5)
                {

                    count += 1;

                }
                count += (int)y;

                finalfragments.Add((z, decimals));

            }

            //MessageBox.Show(count.ToString());
            double range11 = panelWidth / count;

            range1 = (int)Math.Round(range11);
            //MessageBox.Show(range1.ToString());
            int range2 = 0;
            float count2 = 0;
            count = 0;
            // Read the subshots txt file of the video to create 2 sec (60 frames) fragments-buttons as picturebox
            for (int i = 0; i < subshots.Count; i++)
            {

                float x = subshots[i].Y - subshots[i].X;
                float y = x / 60;
                float z = (int)(y) - 1;
                float decimals = y - z;
                if (decimals > 1.5)
                    count += 1;

                count += (int)y;

                for (int j = 0; j < count; j++)
                {

                    if (j + 1 < count)
                    {
                        PictureBox pictureBox = new PictureBox();

                        pictureBox.Width = range1 - 2; // Set the width of the picture box

                        pictureBox.Height = 5; // Set the height of the picture box
                        pictureBox.Location = new Point(range2 + 1, (int)(panelHeight / 2));


                        pictureBox.BackColor = Color.Gray;
                        //pictureBox.ForeColor = Color.Black;
                        panel4.Controls.Add(pictureBox);
                        pictureBoxes.Add(pictureBox);
                        //Console.WriteLine($"Video Duration:{count2}");
                        System.Windows.Forms.Label newlabel = new System.Windows.Forms.Label();
                        newlabel.Width = range1;
                        newlabel.Height = 30;

                        newlabel.Location = new Point(range2, (int)(panelHeight / 2) + 6);
                        newlabel.Text = $"{(int)Math.Round(count2)} {(int)Math.Round(count2 + 59)}";

                        newlabel.TextAlign = ContentAlignment.TopCenter;
                        newlabel.Font = new Font(label1.Font.FontFamily, a, label1.Font.Style);

                        newlabel.ForeColor = Color.Black;
                        panel4.Controls.Add(newlabel);
                        labels.Add(newlabel);
                        count2 += 60;
                    }
                    else
                    {
                        if (decimals * 60 <= 90)
                        {
                            PictureBox pictureBox = new PictureBox();

                            pictureBox.Width = range1 - 2; // Set the width of the picture box

                            pictureBox.Height = 5; // Set the height of the picture box
                            pictureBox.Location = new Point(range2 + 1, (int)(panelHeight / 2));


                            pictureBox.BackColor = Color.Gray;
                            panel4.Controls.Add(pictureBox);
                            pictureBoxes.Add(pictureBox);
                            System.Windows.Forms.Label newlabel = new System.Windows.Forms.Label();
                            newlabel.Width = range1;
                            newlabel.Height = 30;
                            newlabel.Location = new Point(range2, (int)(panelHeight / 2) + 6);
                            newlabel.Text = $"{(int)Math.Round(count2)} {(int)Math.Round((count2 + (decimals * 60)))}";
                            newlabel.TextAlign = ContentAlignment.TopCenter;
                            newlabel.Font = new Font(label1.Font.FontFamily, a, label1.Font.Style);
                            newlabel.ForeColor = Color.Black;
                            //newlabel.Enabled = false;
                            panel4.Controls.Add(newlabel);
                            labels.Add(newlabel);
                            count2 = count2 + (decimals * 60) + 1;

                        }
                        else
                        {

                            PictureBox pictureBox1 = new PictureBox();

                            pictureBox1.Width = range1 - 2; // Set the width of the picture box

                            pictureBox1.Height = 5; // Set the height of the picture box
                            pictureBox1.Location = new Point(range2 + 1, (int)(panelHeight / 2));


                            pictureBox1.BackColor = Color.Gray;
                            panel4.Controls.Add(pictureBox1);
                            pictureBoxes.Add(pictureBox1);

                            System.Windows.Forms.Label newlabel1 = new System.Windows.Forms.Label();
                            newlabel1.Width = range1;
                            newlabel1.Height = 30;
                            newlabel1.Location = new Point(range2, (int)(panelHeight / 2) + 6);
                            newlabel1.Text = $"{count2} {(count2 + (((decimals - 1) * 60)))}";
                            newlabel1.TextAlign = ContentAlignment.TopCenter;
                            newlabel1.Font = new Font(label1.Font.FontFamily, a, label1.Font.Style);
                            newlabel1.ForeColor = Color.Black;
                            //newlabel.Enabled = false;
                            panel4.Controls.Add(newlabel1);
                            labels.Add(newlabel1);


                            count2 = count2 + ((decimals - 1) * 60) + 1;


                        }
                        //Console.WriteLine($"Video New:{decimals}");

                    }
                    range2 += range1;

                }

                finalfragments.Add((z, decimals));
                count = 0;

            }
            int panelheight = (int)(panel4.Height / 2) + 4 * (int)(panel4.Height / 9);

            // add to each fragment-button the click function
            foreach (var pictureBox in pictureBoxes)
            {
                pictureBox.Click += PictureBox_Click;
            }
          
            totalfragments = (int)Math.Round((currentFrameCount * 0.15) / 60);
                

            label3.Text = $"Choose {totalfragments.ToString()} fragments / Selected: 0";
            label3.Font = new Font(label3.Font.FontFamily,(int)(this.Height*0.015), label3.Font.Style);
            label3.ForeColor = SystemColors.ControlText;
            
            show_frame();

        }
        private void PictureBox_Click(object sender, EventArgs e)
        {
            // Perform the action you want when a PictureBox is clicked
            PictureBox clickedPictureBox = (PictureBox)sender;

            // Example: Change the background color of the clicked PictureBox
            int counter = 0;
            if (clickedPictureBox.BackColor == Color.Gray)
            {
                clickedPictureBox.BackColor = System.Drawing.Color.Green;
                clickedPictureBox.Height = 8;

                clickedPictureBox.BackColor = Color.Green;
                clickedPictureBox.Height = 8;

                foreach (PictureBox item in pictureBoxes)
                {
                    if (item.Location == clickedPictureBox.Location)
                    {
                        break;
                    }
                    counter++;
                }
                string labelText = labels[counter].Text;

                string[] parts = labelText.Split(' ');


                foreach (string part in parts)
                {

                    int value = int.Parse(part);
                    if (value == 0)
                    {
                        hScrollBar1.Value = 0;
                    }
                    else if (value > hScrollBar1.Maximum)
                    {
                        hScrollBar1.Value = hScrollBar1.Maximum;
                    }
                    else
                    {
                        hScrollBar1.Value = value;
                    }

                    break;

                }
                show_frame();



            }
            else
            {
                clickedPictureBox.BackColor = System.Drawing.Color.Gray;
                clickedPictureBox.Height = 5;
            }
            counter = 0;
            foreach (PictureBox item in pictureBoxes)
            {
                if (item.BackColor == Color.Green)
                {
                    counter++;
                }
            }
            label3.Text = $"Choose {totalfragments.ToString()} fragments / Selected: {counter}";
            if (counter == totalfragments)
            {
                label3.Font = new Font(label3.Font.FontFamily, (int)(this.Height * 0.015)+1, label3.Font.Style);
                label3.ForeColor = Color.Green;
            }
            else if (counter > totalfragments)
            {
                label3.Font = new Font(label3.Font.FontFamily, (int)(this.Height * 0.015), label3.Font.Style);
                label3.ForeColor = Color.Red;

            }
            else
            {
                label3.Font = new Font(label3.Font.FontFamily, (int)(this.Height * 0.015), label3.Font.Style);
                label3.ForeColor = SystemColors.ControlText;

            }
            label3.Refresh();

        }
        // load video button
        private void buttonLoad_Click(object sender, EventArgs e)

        {
        }
        private void button1_Click(object sender, EventArgs e)

        {
            timer1.Enabled = false;
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Title = "Select video file";
            ofd.Filter = "mp4 Video file |*.mp4";
            ofd.Multiselect = false;

            string root = Directory.GetCurrentDirectory();


            if (Directory.Exists(root))
            {
                Debug.WriteLine(" Found!");
                ofd.InitialDirectory = root;

            }
            if (vidPath != null)
            {
                ofd.InitialDirectory = Directory.GetParent(vidPath).ToString();

            }
            root = Directory.GetParent(root).ToString();


            //updateGUI(false);


            // show open file dialog
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (ofd.FileName.Length > 0)
                {
                    currentFrameCount = -1;
                    currentFrameH = -1;
                    currentFrameW = -1;
                    currentFrames = new List<Mat>();
                    vidPath = ofd.FileName;


                    // show file info
                    lblFile.Text = "" + ofd.SafeFileName + "";
                    lblDir.Text = "" + Path.GetDirectoryName(vidPath) + "";


                    pictureBoxes.Clear();
                    labels.Clear();
                    subshots.Clear();
                    panel4.Controls.Clear();
                    hasLoadedFile = false;
                    loadedFile = "";

                    loadVideo(vidPath);
                }

            }

        }

        // show frame
        int xy = 0;
        public void show_frame()
        {

            if (currentFrames != null && currentFrameCount > 0)
            {
                lblFrame.Text = Convert.ToString(hScrollBar1.Value) + "/" + Convert.ToString(currentFrameCount - 1);
                lblFrame.Refresh();
                Mat reshapedMat = new Mat();
                CvInvoke.Resize(currentFrames[hScrollBar1.Value], reshapedMat, new System.Drawing.Size((int)(this.Width / 2), (int)((this.Width / 2) * 3 / 4)), interpolation: Inter.Linear);

                imageBox1.Image = reshapedMat;
                for (int i = 0; i < pictureBoxes.Count; i++)
                {
                    string labelText = labels[i].Text;

                    string[] parts = labelText.Split(' ');
                    if (hScrollBar1.Value >= int.Parse(parts[0]) && hScrollBar1.Value <= int.Parse(parts[1]))
                    {
                       
                        int locy = pictureBoxes[i].Location.Y;

                        int locx = pictureBoxes[i].Location.X;

                        box.Location = new Point(locx - 1 + (int)pictureBoxes[i].Width / 2, locy - 8);
                        box.Width = 3; box.Height = 10;
                        box.ForeColor = Color.Black;
                        box.BackColor = Color.Black;
                        panel4.Controls.Add(box);
                        panel4.Refresh();

                    }


                }

            }





        }



        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)

        {


            show_frame();
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                int index = userName.LastIndexOf(@"\");
                userName = userName.Substring(index + 1);
                txtUser.Text = userName;

            }
            catch (Exception err)
            {
                Debug.WriteLine(err.ToString());
            }
        }



        private void buttonPlay_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                buttonPlay.Text = "▶";
                buttonNext.Enabled = true;
                buttonPrev.Enabled = true;
                timer1.Enabled = false;
            }
            else
            {
                buttonPlay.Text = "⏸";
                buttonNext.Enabled = false;
                buttonPrev.Enabled = false;
                timer1.Enabled = true;


            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (hScrollBar1.Value == hScrollBar1.Maximum)
            {
                buttonPlay.Text = "▶";
                buttonNext.Enabled = true;
                buttonPrev.Enabled = true;
                timer1.Enabled = false;
                return;
            }
            if (hScrollBar1.Value + 1 >= hScrollBar1.Maximum)
            {
                buttonPlay.Text = "▶";
                buttonNext.Enabled = true;
                buttonPrev.Enabled = true;
                timer1.Enabled = false;
            }
            if (buttonPlay.Text != "▶")
            {
                hScrollBar1.Value += 1;
            }



            show_frame();
            if (hScrollBar1.Value == hScrollBar1.Maximum)
            {
                timer1.Enabled = false;
            }

        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            hScrollBar1.Value = Math.Min(hScrollBar1.Value + 10, hScrollBar1.Maximum);
            show_frame();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            hScrollBar1.Value = Math.Min(hScrollBar1.Value + 1, hScrollBar1.Maximum);
            show_frame();
        }
        private void buttonPrev_Click(object sender, EventArgs e)
        {
            hScrollBar1.Value = Math.Max(hScrollBar1.Value - 10, hScrollBar1.Minimum);
            show_frame();
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            hScrollBar1.Value = Math.Max(hScrollBar1.Value - 1, hScrollBar1.Minimum);
            show_frame();
        }

 

        private void button4_Click(object sender, EventArgs e)
        {
            hScrollBar1.Value = Math.Max(hScrollBar1.Value - 20, hScrollBar1.Minimum);
            show_frame();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            hScrollBar1.Value = Math.Min(hScrollBar1.Value + 20, hScrollBar1.Maximum);
            show_frame();
        }

        
        private void panel4_MouseDown(object sender, MouseEventArgs e)
        {
           
            int counter1 = 0;
            foreach (var item in pictureBoxes)
            {

                if (Math.Pow((Math.Pow(e.X - (int)(2 * item.Location.X + item.Width) / 2, 2) + Math.Pow((e.Y - item.Location.Y), 2)), 0.5) <= 13.0 || Math.Pow((Math.Pow(e.X - (int)(2 * item.Location.X + item.Width) / 2, 2) + Math.Pow((e.Y - item.Location.Y - 5), 2)), 0.5) <= 13.0)
                {
                    if (item.BackColor == Color.Gray)
                    {
                        item.BackColor = Color.Green;
                        item.Height = 8;
                        string labelText = labels[counter1].Text;

                        string[] parts = labelText.Split(' ');


                        foreach (string part in parts)
                        {

                            int value = int.Parse(part);
                            if (value == 0)
                            {
                                hScrollBar1.Value = 0;
                            }
                            else if (value > hScrollBar1.Maximum)
                            {
                                hScrollBar1.Value = hScrollBar1.Maximum;
                            }
                            else
                            {
                                hScrollBar1.Value = value;
                            }
                            //setDoneBar();
                            show_frame();
                            //updateGUI(true);
                            break;

                        }
                    }
                    else
                    {
                        item.BackColor = Color.Gray;
                        item.Height = 5;
                    }

                }
                counter1 += 1;
            }

        }

       
        private void panel4_MouseUp(object sender, MouseEventArgs e)
        {
            //update the selected total fragments text
            int counter = 0;
            for (int i = 0; i < pictureBoxes.Count; i++)

            {

                if (pictureBoxes[i].BackColor == Color.Green)
                {
                    counter++;
                }

            }
            label3.Text = $"Choose {totalfragments.ToString()} fragments / Selected: {counter}";
            if (counter == totalfragments)
            {
                label3.Font = new Font(label3.Font.FontFamily, (int)(this.Height * 0.015)+1, label3.Font.Style);
                label3.ForeColor = Color.Green;
            }
            else if (counter > totalfragments)
            {
                label3.Font = new Font(label3.Font.FontFamily, (int)(this.Height * 0.015), label3.Font.Style);
                label3.ForeColor = Color.Red;

            }
            else
            {
                label3.Font = new Font(label3.Font.FontFamily, (int)(this.Height * 0.015), label3.Font.Style);
                label3.ForeColor = SystemColors.ControlText;

            }
            label3.Refresh();

        }

        private void SaveLabelsTextToFile(string filePath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (System.Windows.Forms.Label label in labels)
                    {
                        writer.WriteLine(label.Text);
                    }
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void save_txt_Click(object sender, EventArgs e)
        {
            string id = txtUser.Text;
            string filePath1 = vidPath.ToString();
            int height1 = panel4.Height;
            scores.Clear();
            int underscoreIndex = filePath1.LastIndexOf('_');
            int periodIndex = filePath1.LastIndexOf('.');
            string numberString = filePath1.Substring(underscoreIndex + 1, periodIndex - underscoreIndex - 1);

            string annots_dir = Directory.GetCurrentDirectory() + @"\annotations_" + txtUser.Text + @"\";
            Directory.CreateDirectory(annots_dir);

            if (!Directory.Exists(annots_dir))
            {
                Directory.CreateDirectory(annots_dir);
                Console.WriteLine("Folder created successfully.");
            }
            else
            {
                Console.WriteLine("Folder already exists.");
            }
            string filePath = annots_dir + $"scores_user_{id}_video_{numberString}.txt";

            int counter = 0;
            foreach (PictureBox item in pictureBoxes)
            {
                if (item.BackColor == Color.Green)
                {
                    counter++;
                }
            }
            if (counter == totalfragments)
            {
                for (int i = 0; i < pictureBoxes.Count; i++)
                {
                    if (pictureBoxes[i].BackColor == Color.Green)
                    {
                        scores.Add(labels[i].Text.ToString());

                    }
                }
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    // Write each value to the file
                    foreach (string value in scores)
                    {
                        writer.WriteLine(value);
                    }

                }
                MessageBox.Show($"The scores have saved to {filePath}");
            }
            else if (counter > totalfragments)
            {
                MessageBox.Show("The number of selected fragments is higher than the suggested one");
            }
            else
            {
                MessageBox.Show("The number of selected fragments is lower than the suggested one");
            }
            string filePath2 = annots_dir + $"subshots_video_{numberString}.txt";

            // Call the method to save labels' text to the file
            SaveLabelsTextToFile(filePath2);


        }
       
  
        //check summary button and load a new form if the selected fragments are correct
        private void check_summary_button_Click_1(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            int counter = 0;
            foreach (PictureBox item in pictureBoxes)
            {
                if (item.BackColor == Color.Green)
                {
                    counter += 1;
                }
            }
            if (counter == totalfragments)
            {
                Form2 frm = new Form2(pictureBoxes, currentFrames, labels);
                DialogResult qrwts = frm.ShowDialog();
            }
            else if (counter > totalfragments)
            {
                int dif = counter - totalfragments;
                MessageBox.Show($"The number of selected fragments is higher than the suggested one");
            }
            else
            {
                int dif = totalfragments - counter;
                MessageBox.Show($"The number of selected fragments is lower than the suggested one");
            }

  


        }


    }
}

