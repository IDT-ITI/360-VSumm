using Emgu.CV;
using System;
using System.Collections.Generic;

using System.Drawing;

using System.Windows.Forms;

namespace VideoAnnotator
{
    public partial class Form2 : Form
    {
        private List<Mat> summaryFrames = new List<Mat>();
        private List<PictureBox> greenboxes = new List<PictureBox>();
        //private List<System.Windows.Forms.Label> labels1 = new List<System.Windows.Forms.Label>();
        private List<string> labels1 = new List<string>();
        private List<string> ints = new List<string>();
        public Form2(List<PictureBox> pictureBoxes, List<Mat> currentFrames, List<System.Windows.Forms.Label> labels)
        {

            InitializeComponent();
            this.Text = "Summary Video";
            //timer1.Enabled = false;
            this.DialogResult = DialogResult.Cancel;
            int counter = 0;
            int counter2 = 0;
            foreach (PictureBox box in pictureBoxes)
            {
                if (box.BackColor == Color.Green)
                {
                    string labelText = labels[counter].Text;

                    string[] parts = labelText.Split(' ');


                    int value1 = int.Parse(parts[0]);
                    int value2 = int.Parse(parts[1]);
                    //MessageBox.Show(labels[counter].Text.ToString());

                    greenboxes.Add(pictureBoxes[counter]);
                    labels1.Add(labels[counter].Text);
                    int dif = value2 - value1;

                    ints.Add($"{counter2} {counter2 + dif}");
                    counter2 += dif + 1;
                    for (int i = value1; i <= value2; i++)
                    {
                        summaryFrames.Add(currentFrames[i]);
                    }
                }
                counter++;

            }
            this.Width = 480;
            buttonPlay.Enabled = true;
            label1.Enabled = true;
            hScrollBar1.Minimum = 0;
            hScrollBar1.Maximum = summaryFrames.Count - 1;
            //MessageBox.Show(hScrollBar1.Maximum.ToString());
            hScrollBar1.Value = 0;
            timer1.Interval = Convert.ToInt32(0.75 * (1000.0 / 30));
            timer1.Tick += new EventHandler(timer1_Tick);
            show_frame();

        }
        public void show_frame()
        {

            if (summaryFrames.Count != null)
            {
                imageBox1.Image = summaryFrames[hScrollBar1.Value];
                imageBox1.Invalidate();
                for (int i = 0; i < greenboxes.Count; i++)
                {

                    string labelText = ints[i];

                    string[] parts = labelText.Split(' ');
                    if (hScrollBar1.Value >= int.Parse(parts[0]) && hScrollBar1.Value <= int.Parse(parts[1]))
                    {

                        label1.Text = $"Fragment: '{i + 1}' Frames: " + labels1[i].ToString();
                        //label1.ControlAdded()
                        label1.Refresh();
                        break;

                    }


                }
            }
            // MessageBox.Show($"yees {hScrollBar1.Value}");



        }
        public void loadSummary()
        {
            imageBox1.Image = summaryFrames[hScrollBar1.Value];
            timer1.Enabled = true;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {

            if (hScrollBar1.Value == hScrollBar1.Maximum)
            {
                buttonPlay.Text = "▶";
                timer1.Enabled = false;

                return;
            }
            if (hScrollBar1.Value + 1 >= hScrollBar1.Maximum)
            {

                buttonPlay.Text = "▶";
                timer1.Enabled = false;
            }
            else
            {
                hScrollBar1.Value += 1;
            }

            show_frame();


        }
        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)

        {

            show_frame();
        }
        
      

        private void buttonPlay_Click_1(object sender, EventArgs e)
        {
            if (timer1.Enabled && hScrollBar1.Value < hScrollBar1.Maximum)
            {
                buttonPlay.Text = "▶";
                //hScrollBar1.Value = 0;
                timer1.Enabled = false;
            }
            else if (hScrollBar1.Value + 1 == hScrollBar1.Maximum)
            {
                buttonPlay.Text = "▶";
                hScrollBar1.Value = 0;
                timer1.Enabled = false;
                show_frame();


            }
            else
            {

                buttonPlay.Text = "⏸";
                //hScrollBar1.Value = 0;
                timer1.Enabled = true;
                show_frame();


            }
        }
    }
}
