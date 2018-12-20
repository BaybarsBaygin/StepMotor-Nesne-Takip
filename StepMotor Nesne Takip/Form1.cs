using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Imaging.Filters;
using AForge.Imaging;
using AForge.Video;
using AForge.Video.DirectShow;
using AForge.Vision;
using AForge.Vision.Motion;

namespace StepMotor_Nesne_Takip
{
    public partial class Form1 : Form
    {
        private VideoCaptureDevice camera;
        private FilterInfoCollection camfilt;
        public Form1()
        {
            InitializeComponent();
        }
        int X;
        int Y = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;

            for (int i = 0; i < System.IO.Ports.SerialPort.GetPortNames().Length; i++)
            {
                toolStripComboBox2.Items.Add(System.IO.Ports.SerialPort.GetPortNames()[i]);
            }
            camfilt = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            foreach (FilterInfo VideoCaptureDevice in camfilt)
            {
                toolStripComboBox1.Items.Add(VideoCaptureDevice.Name);
                toolStripComboBox1.SelectedIndex = 0;
            }
        }

        private void kameraAc_Click(object sender, EventArgs e)
        {
            try
            {
                camera = new VideoCaptureDevice(camfilt[toolStripComboBox1.SelectedIndex].MonikerString);
                camera.NewFrame += new NewFrameEventHandler(camera_NewFrame);
                camera.DesiredFrameRate = 30;          
                camera.DesiredFrameSize = new Size(640, 480);  
                camera.Start();
            }
            catch (Exception)
            {

                MessageBox.Show("HİÇ KAMERA BULUNAMADI", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void camera_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap image = (Bitmap)eventArgs.Frame.Clone();
            Bitmap image1 = (Bitmap)eventArgs.Frame.Clone();
            pictureBox1.Image = image;

            if(radioButton1.Checked)
            {
                EuclideanColorFiltering filter = new EuclideanColorFiltering();
                filter.CenterColor = new RGB(Color.FromArgb(185, 45, 30));
                filter.Radius = 50;
                filter.ApplyInPlace(image1);
                scala(image1);
            }
            if(radioButton2.Checked)
            {
                EuclideanColorFiltering filter = new EuclideanColorFiltering();
                filter.CenterColor = new RGB(Color.FromArgb(45, 185, 30));
                filter.Radius = 50;
                filter.ApplyInPlace(image1);
                scala(image1);
            }
            if(radioButton3.Checked)
            {
                EuclideanColorFiltering filter = new EuclideanColorFiltering();
                filter.CenterColor = new RGB(Color.FromArgb(47, 30, 185));
                filter.Radius = 50;
                filter.ApplyInPlace(image1);
                scala(image1);
            }

        }

        private void scala(Bitmap image)
        {
            BlobCounter blobCounter = new BlobCounter();
            blobCounter.MinWidth = 2;
            blobCounter.MinHeight = 2;
            blobCounter.FilterBlobs = true;
            blobCounter.ObjectsOrder = ObjectsOrder.Size;

            Grayscale grayFilter = new Grayscale(0.2125, 0.7154, 0.0721);
            Bitmap grayImage = grayFilter.Apply(image);

            blobCounter.ProcessImage(grayImage);
            Rectangle[] rects = blobCounter.GetObjectsRectangles();
            foreach (Rectangle recs in rects)
            {

                if (rects.Length > 0)
                {
                    Rectangle objectRect = rects[0];
                    Graphics g = pictureBox1.CreateGraphics();
                    using (Pen pen = new Pen(Color.FromArgb(252, 3, 26), 2))
                    {

                        g.DrawRectangle(pen, objectRect);
                    }

                    X = objectRect.X + (objectRect.Width / 2); 
                    Y = objectRect.Y + (objectRect.Height / 2);
                    g.DrawString(X.ToString() + "X" + Y.ToString(), new Font("Arial", 12), Brushes.Red, new System.Drawing.Point(250, 1));
                    g.Dispose();
                   
                }
            }
        }

        private void seriPortB_Click(object sender, EventArgs e)
        {
            serialPort1.PortName = toolStripComboBox2.Text;
            label2.Text = "Arduino'ya Bağlandı (" + toolStripComboBox2.Text + ")";
            serialPort1.Open();
        }

        private void seriPortD_Click(object sender, EventArgs e)
        {
            label2.Text = "COM Port Seç !";
            serialPort1.Close();
        }

        private void Basla_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void seriPortB_MouseMove(object sender, MouseEventArgs e)
        {
            seriPortB.BackColor = Color.White;
        }

        private void seriPortB_MouseLeave(object sender, EventArgs e)
        {
            seriPortB.BackColor = Color.FromArgb(192, 64, 0);
        }

        private void kameraDur_Click(object sender, EventArgs e)
        {
            if (camera.IsRunning)
            {
                camera.Stop();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (X >= 512) X = 256;
            else if (X >= 500) X = 250;
            else if (X >= 490) X = 245;
            else if (X >= 480) X = 240;
            else if (X >= 470) X = 235;
            else if (X >= 460) X = 230;
            else if (X >= 450) X = 225;
            else if (X >= 440) X = 220;
            else if (X >= 430) X = 215;
            else if (X >= 420) X = 210;
            else if (X >= 410) X = 205;
            else if (X >= 400) X = 200;
            else if (X >= 390) X = 195;
            else if (X >= 380) X = 190;
            else if (X >= 370) X = 185;
            else if (X >= 360) X = 180;
            else if (X >= 350) X = 175;
            else if (X >= 340) X = 170;
            else if (X >= 330) X = 165;
            else if (X >= 320) X = 160;
            else if (X >= 310) X = 155;
            else if (X >= 300) X = 150;
            else if (X >= 290) X = 145;
            else if (X >= 280) X = 140;
            else if (X >= 270) X = 135;
            else if (X >= 260) X = 130;
            else if (X >= 250) X = 125;
            else if (X >= 240) X = 120;
            else if (X >= 230) X = 115;
            else if (X >= 220) X = 110;
            else if (X >= 210) X = 105;
            else if (X >= 200) X = 100;
            else if (X >= 190) X = 95;
            else if (X >= 180) X = 90;
            else if (X >= 170) X = 85;
            else if (X >= 160) X = 80;
            else if (X >= 150) X = 75;
            else if (X >= 140) X = 70;
            else if (X >= 130) X = 65;
            else if (X >= 120) X = 60;
            else if (X >= 110) X = 55;
            else if (X >= 100) X = 50;
            else if (X >= 90) X = 45;
            else if (X >= 80) X = 40;
            else if (X >= 70) X = 35;
            else if (X >= 60) X = 30;
            else if (X >= 50) X = 25;
            else if (X >= 40) X = 20;
            else if (X >= 30) X = 15;
            else if (X >= 20) X = 10;
            else if (X >= 10) X = 5;
            else X = 0;

            int o = Convert.ToInt32(X);
            byte[] b = BitConverter.GetBytes(o);
            serialPort1.Write(b, 0, 4);
            label4.Text = Convert.ToString(X);
        }

        private void kirmiziToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void yesilToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void maviToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
    
}
