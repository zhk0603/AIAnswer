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
        private readonly TesseractEngine _engine;
        public Form1()
        {
            InitializeComponent();
            _engine = new TesseractEngine(@"./tessdata", "chi_sim", EngineMode.Default);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "jpg图片文件|*.jpg";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var file = new System.IO.FileStream(openFileDialog1.FileName, System.IO.FileMode.Open);
                var image = ImageHelper.CaptureImage(file, 0, 490, 1080, 400); // 答案。
                var bitmap = new Bitmap(image);
                pictureBox1.Image = bitmap;
                pictureBox1.Image.Save(@"C:\Users\Administrator\Desktop\images\tmp.jpeg");
                Page tmpPage = _engine.Process(bitmap, pageSegMode: _engine.DefaultPageSegMode);
                var txt = tmpPage.GetText();
                MessageBox.Show(txt);
            }
        }
    }
}