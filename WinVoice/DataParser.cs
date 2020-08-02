using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// Смещение вверх
        /// </summary>
        public int shiftUp = 0;

        /// <summary>
        /// Путь к файлу настроек
        /// </summary>
        private string path = "settings.txt";

        public DataParser(string path)
        {
            this.path = path;
        }

    }
}
