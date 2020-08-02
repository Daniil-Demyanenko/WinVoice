using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinVoice
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            //установка положения окна внизу справа
            Point pt = Screen.PrimaryScreen.WorkingArea.Location;
            pt.Offset(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            pt.Offset(-this.Width, -this.Height);
            this.Location = pt;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void setWindowPosition()
        {
            //настройка размеров pictureBox
            pictureBox1.Width = pictureBox1.Image.Width;
            pictureBox1.Height = pictureBox1.Image.Height;
            //настройка размеров окна равных pictureBox
            this.Height = pictureBox1.Height;
            this.Width = pictureBox1.Width;
            //установка положения окна внизу справа
            Point pt = Screen.PrimaryScreen.WorkingArea.Location;
            pt.Offset(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            pt.Offset(-this.Width, -this.Height);
            this.Location = pt;
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == 0x219)
            {
                if (m.WParam.ToInt32() == 0x8000)
                {
                    pictureBox1.Image = new Bitmap(@"src\img\2.jpg");
                    setWindowPosition();
                    new SoundPlayer(@"src\voice\11.wav").Play();
                    this.TopMost = true;
                    this.Show();
                    timer();
                    this.TopMost = false;
                }

            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            this.Hide();
        }

        //Таймер для скрытия формы через время
        private async void timer() 
        {
            await Task.Delay(2500);
            this.Hide();
        }
    }
}
