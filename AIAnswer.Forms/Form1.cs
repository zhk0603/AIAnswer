using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tesseract;

namespace AIAnswer.Forms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "全部文件|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var file = new System.IO.FileStream(openFileDialog1.FileName, System.IO.FileMode.Open);
                var image = ImageHelper.CaptureImage(file, 0, 490, 1080, 1280); // 答案。
                var bitmap = new Bitmap(image);
                bitmap.Save(@"C:\Users\Administrator\Desktop\images\tmp.jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
                pictureBox1.Image = bitmap;
                var engine = new TesseractEngine(@"./tessdata", "chi_sim", EngineMode.Default);
                Page tmpPage = engine.Process(bitmap, engine.DefaultPageSegMode);
                var txt = tmpPage.GetText();
                MessageBox.Show(txt);
            }
        }
    }
}