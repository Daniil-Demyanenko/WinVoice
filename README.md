## Описание
Эта программа добавляет ваши звуки при подключении USB устройств в Windows.

## Инструкция
Создайте файл Settings.txt рядом с исполняемым файлом.  
В файле Settings.txt производится настройка программы.  
Первые строчки файла выглядят следующим образом:
>Width = 250  
Height = 200  
ShowImageTime = 3000  

В них настра ивается максимальная *широта* и *высота* изображения, а так же *время*, которое показывается изображение (в мс).  

Далее попарно вводится адрес картинки и соответствующего ей звука.  
Адрес картинки можно заменить на символ "**#**", тогда картинка не будет отображаться при подключении USB устройств, но адрес .WAV файла обязателен.  

При введении нескольких записей программа будет случайно выбирать нужную.  

Записи можно для удобства разделять пустой строкой.  

Пример записей:
>src/img/1.jpg  
>2.wav  
>  
>voice.wav  
>\#  
