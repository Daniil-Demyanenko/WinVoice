using System;
using System.IO;
using System.Windows.Forms;

namespace WinVoice
{
    class DataParser
    {
        /// <summary>
        /// Массив с изображениями
        /// </summary>
        public string[] img;
        /// <summary>
        /// Массив с звуками, соответствующими изображениям под теми же инжексами
        /// </summary>
        public string[] voice;
        /// <summary>
        /// Максимальная ширина изображения
        /// </summary>
        public int width = 400;
        /// <summary>
        /// Максимальная высота
        /// </summary>
        public int height = 70;
        /// <summary>
        /// Время показа картинки
        /// </summary>
        public int ShowImageTime = 2500;
        /// <summary>
        /// Случайная картинка
        /// </summary>
        public string Random_img = "";
        /// <summary>
        /// Случайный звук, соответсвующий картинке
        /// </summary>
        public string Random_Voice = "";

        /// <summary>
        /// Путь к файлу настроек
        /// </summary>
        private string path = "settings.txt";
        /// <summary>
        /// Счётчик записей 
        /// </summary>
        private int count = 0; 
        

        public DataParser(string path)
        {
            this.path = path;
            var file = File.ReadAllLines(path);

            for (int i = 3; i < file.Length; i++) // инициализация счётчика
                if (!isEmptyLine(file[i])) count++;
            count = (int)Math.Ceiling(count / 2f);

            img = new string[count];
            voice = new string[count];

            parse(path);
        }

        private void parse(string path)
        {
            int c = 0;
            bool complite = false;

            string[] file = File.ReadAllLines(path);
            try
            {
                width = int.Parse(file[0].Split('=')[1]);
                height = int.Parse(file[1].Split('=')[1]);
                ShowImageTime = int.Parse(file[2].Split('=')[1]);
            }
            catch
            {
                parseErr();
            }
            for (int i = 3; i < file.Length; i++)
            {
                if (isEmptyLine(file[i])) continue; //  если строка не пуста
                if(!((file[i].Length > 0 && file[i][0] == '#') || File.Exists(file[i]))) parseErr();// и файл существует и первый символ != '#'
                if (isWAV(file[i])) voice[c] = file[i];
                else img[c] = file[i];

                if(complite) c++;
                complite = !complite;
            }
        }

        /// <summary>
        /// Возвращает true, если строка пуста или содержит только пробелы
        /// </summary>
        private bool isEmptyLine(string line)
        {
            if (line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length == 0)
                return true;

            return false;
        }
        /// <summary>
        /// Возвращает true, если файл является *.wav
        /// </summary>
        private bool isWAV(string line)
        {
            #region Это лучше не видеть...
            int l = line.Length;
            line = line.ToLower();
            if (line[l - 1] != 'v' || line[l - 2] != 'a' || line[l - 3] != 'w' || line[l - 4] != '.') return false;

            return true;
            #endregion
        }

        public void randomize() 
        {
            int i = new Random().Next(0, count);
            Random_img = img[i];
            Random_Voice = voice[i];
        }

        /// <summary>
        /// Выдаёт пользователю сообщение об ошибке и завершает приложение
        /// </summary>
        public void parseErr()
        {
            MessageBox.Show("Проверьте настройки в Settings.txt и перезапустите приложение", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Environment.Exit(0);
        }

    }
}
