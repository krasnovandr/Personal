using System;

namespace Program
{
    struct RGB
    {
        public float R;
        public float G;
        public float B;
    }
    public enum Filters
    {
        Median,
        Erosion,
        BuildUp
    }

    class Filter
    {
        public static UInt32[,] matrix_filtration(int W, int H, UInt32[,] pixel, int N, double[,] matryx)
        {
            int gap = (int)(N / 2);
            int tmpH = H + 2 * gap;
            int tmpW = W + 2 * gap;
            double div = 0;
            UInt32[,] newpixel = new UInt32[H, W];

            var tmppixel = FillTempMatrix(W, H, pixel, tmpH, tmpW, gap);
            //применение ядра свертки
            var colorOfPixel = new RGB();
           
            for (int i = gap; i < tmpH - gap; i++)
            {
                for (int j = gap; j < tmpW - gap; j++)
                {
                    colorOfPixel.R = 0;
                    colorOfPixel.G = 0;
                    colorOfPixel.B = 0;
                    div = 0;
                    for (int k = 0; k < N; k++)
                    {
                        for (int m = 0; m < N; m++)
                        {
                            RGB colorOfCell = CalculationOfColor(tmppixel[i - gap + k, j - gap + m], matryx[k, m]);
                            colorOfPixel.R += colorOfCell.R;
                            colorOfPixel.G += colorOfCell.G;
                            colorOfPixel.B += colorOfCell.B;
                            div += matryx[k, m];
                        }

                    }


                    if (div <= 0) div = 1;

                    //контролируем переполнение переменных
                    colorOfPixel.R = (float)(colorOfPixel.R / div);
                    if (colorOfPixel.R < 0) colorOfPixel.R = 0;
                    if (colorOfPixel.R > 255) colorOfPixel.R = 255;

                    colorOfPixel.G = (float)(colorOfPixel.G / div);
                    if (colorOfPixel.G < 0) colorOfPixel.G = 0;
                    if (colorOfPixel.G > 255) colorOfPixel.G = 255;

                    colorOfPixel.B = (float)(colorOfPixel.B / div);
                    if (colorOfPixel.B < 0) colorOfPixel.B = 0;
                    if (colorOfPixel.B > 255) colorOfPixel.B = 255;

                    newpixel[i - gap, j - gap] = Build(colorOfPixel);
                }
            }
             

            return newpixel;
        }

        public static uint[,] MedianFiltration(int W, int H, uint[,] pixel, int N, Filters filter)
        {
            int gap = N / 2;
            int tmpH = H + 2 * gap;
            int tmpW = W + 2 * gap;
            var newpixel = new UInt32[H, W];

            var tmppixel = FillTempMatrix(W, H, pixel, tmpH, tmpW, gap);
            //применение ядра свертки

            for (int i = gap; i < tmpH - gap; i++)
                for (int j = gap; j < tmpW - gap; j++)
                {
                    RGB colorOfPixel;
                    colorOfPixel.R = 0;
                    colorOfPixel.G = 0;
                    colorOfPixel.B = 0;
                    var array = new UInt32[N * N];
                    int index = 0;

                    for (int k = 0; k < N; k++)
                    {
                        for (int m = 0; m < N; m++)
                        {
                            array[index++] = tmppixel[i - gap + k, j - gap + m];

                        }
                    }

                    colorOfPixel = GetResult(array, filter);
                    //контролируем переполнение переменных
                    if (colorOfPixel.R < 0) colorOfPixel.R = 0;
                    if (colorOfPixel.R > 255) colorOfPixel.R = 255;
                    if (colorOfPixel.G < 0) colorOfPixel.G = 0;
                    if (colorOfPixel.G > 255) colorOfPixel.G = 255;
                    if (colorOfPixel.B < 0) colorOfPixel.B = 0;
                    if (colorOfPixel.B > 255) colorOfPixel.B = 255;

                    newpixel[i - gap, j - gap] = Build(colorOfPixel);
                }

            return newpixel;
        }

        private static uint[,] FillTempMatrix(int W, int H, uint[,] pixel, int tmpH, int tmpW, int gap)
        {
            UInt32[,] tmppixel = new UInt32[tmpH, tmpW];

            //заполнение временного расширенного изображения
            //углы
            for (int i = 0; i < gap; i++)
            {
                for (int j = 0; j < gap; j++)
                {
                    tmppixel[i, j] = pixel[0, 0];
                    tmppixel[i, tmpW - 1 - j] = pixel[0, W - 1];
                    tmppixel[tmpH - 1 - i, j] = pixel[H - 1, 0];
                    tmppixel[tmpH - 1 - i, tmpW - 1 - j] = pixel[H - 1, W - 1];
                }
            }

            //крайние левая и правая стороны
            for (int i = gap; i < tmpH - gap; i++)
            {
                for (int j = 0; j < gap; j++)
                {
                    //левая сторона
                    tmppixel[i, j] = pixel[i - gap, j];
                    tmppixel[i, tmpW - 1 - j] = pixel[i - gap, W - 1 - j];
                }
            }

            //крайние верхняя и нижняя стороны
            for (int i = 0; i < gap; i++)
            {
                for (int j = gap; j < tmpW - gap; j++)
                {
                    tmppixel[i, j] = pixel[i, j - gap];
                    tmppixel[tmpH - 1 - i, j] = pixel[H - 1 - i, j - gap];
                }
            }

            //центр
            for (int i = 0; i < H; i++)
            {
                for (int j = 0; j < W; j++)
                {
                    tmppixel[i + gap, j + gap] = pixel[i, j];
                }
            }

            return tmppixel;
        }

        private static RGB GetResult(uint[] array, Filters filter)
        {
            var redArray = new UInt32[array.Length];
            var greenArray = new UInt32[array.Length];
            var blueArray = new UInt32[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                redArray[i] = (array[i] & 0x00FF0000) >> 16;
                greenArray[i] = (array[i] & 0x0000FF00) >> 8;
                blueArray[i] = array[i] & 0x000000FF;
            }
            int index = 0;
            switch (filter)
            {
                case Filters.BuildUp:
                    index = array.Length - 1;
                    break;
                case Filters.Erosion:
                    index = 0;
                    break;
                case Filters.Median:
                    index = array.Length / 2;
                    break;
            }
            Array.Sort(redArray);
            Array.Sort(greenArray);
            Array.Sort(blueArray);

            var color = new RGB
            {
                R = redArray[index],
                G = greenArray[index],
                B = blueArray[index]
            };
            return color;
        }

        //вычисление нового цвета
        public static RGB CalculationOfColor(UInt32 pixel, double coefficient)
        {
            RGB Color = new RGB();
            Color.R = (float)(coefficient * ((pixel & 0x00FF0000) >> 16));
            Color.G = (float)(coefficient * ((pixel & 0x0000FF00) >> 8));
            Color.B = (float)(coefficient * (pixel & 0x000000FF));
            return Color;
        }

        //сборка каналов
        public static UInt32 Build(RGB ColorOfPixel)
        {
            UInt32 Color;
            Color = 0xFF000000 | ((UInt32)ColorOfPixel.R << 16) | ((UInt32)ColorOfPixel.G << 8) | ((UInt32)ColorOfPixel.B);
            return Color;
        }








        //повышение резкости
        public const int N1 = 3;
        public static double[,] sharpness = new double[N1, N1] {{-1, -1, -1},
                                                               {-1,  9, -1},
                                                               {-1, -1, -1}};

        //размытие
        public const int N2 = 5;
        public static double[,] blur = new double[N2, N2] {{0.000789, 0.006581, 0.013347,0.006581,0.000789},
                                                               {0.006581, 0.054901, 0.111345,0.054901,0.006581},
                                                               {0.013347, 0.111345, 0.225821,0.111345,0.013347},
                                                               {0.006581, 0.054901, 0.111345,0.054901,0.006581},
                                                               {0.000789, 0.006581, 0.013347,0.006581,0.000789}};




    }
}