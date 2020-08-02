using System;
using System.Drawing;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinVoice
{
    public partial class Form1 : Form
    {
        private DataParser DP = new DataParser("Settings.txt"); 
        

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

        // Настройка положения окна
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

        // Корректировка размеров Bitmap
        private Bitmap setBitmapSize(string path) 
        {
            Bitmap picture = new Bitmap(path);
            float kW = picture.Width / (float)DP.width; // Коэфициент для расчёта уменьшения picture по ширине
            if (picture.Height / kW <= DP.height)
                return new Bitmap(picture, new Size((int)(picture.Width / kW), (int)(picture.Height / kW)));
            else
            {
                float kH = picture.Height / (float)DP.height; // Коэфициент для расчёта уменьшения picture по высоте
                return new Bitmap(picture, new Size((int)(picture.Width / kH), (int)(picture.Height / kH)));
            }
        }

        
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == 0x219) // Если событие -- это подключение USB
            {
                if (m.WParam.ToInt32() == 0x8000) 
                {
                    try
                    {
                        DP.randomize();
                        bool show = !(DP.Random_img[0] == '#');
                        if (show) pictureBox1.Image = setBitmapSize(DP.Random_img);
                        new SoundPlayer(DP.Random_Voice).Play();

                        if (show) setWindowPosition();
                        this.TopMost = true;
                        if (show) this.Show();
                        timer();
                        this.TopMost = false;
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                        DP.parseErr();
                    }
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
            await Task.Delay(DP.ShowImageTime);
            this.Hide();
        }
    }
}
