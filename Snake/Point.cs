using System;

namespace Snake
{
    class Point
    {
        public int x;
        public int y;
        public char symbol;

        public Point()
        {
        }

        /// <summary>
        /// Создание точки по кординатам и символу
        /// </summary>
        /// <param name="x">Координата Х</param>
        /// <param name="y">Координата Y</param>
        /// <param name="symbol">Символ для отображения точки</param>
        public Point( int x, int y, char symbol )
        {
            this.x = x;
            this.y = y;
            this.symbol = symbol;
        }

        /// <summary>
        /// Создание точки от другой точки
        /// </summary>
        /// <param name="point">Объект точки</param>
        public Point( Point point )
        {
            this.x = point.x;
            this.y = point.y;
            this.symbol = point.symbol;
        }

        /// <summary>
        /// Перемещение (сдвиг) точки 
        /// </summary>
        /// <param name="offset">Количество точек сдвига</param>
        /// <param name="direction">Направление перемещения</param>
        public void Move(int offset, Direction direction)
        {
            switch (direction)
            {
                case Direction.LEFT:
                    x -= offset;
                    break;
                case Direction.RIGHT:
                    x += offset;
                    break;
                case Direction.UP:
                    y -= offset;
                    break;
                case Direction.DOWN:
                    y += offset;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Отображение точки на экране
        /// </summary>
        public void Draw()
        {
            try
            {
                Console.SetCursorPosition(x, y);
                Console.Write(symbol);
            }
            catch (Exception e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Удаление точки с экрана
        /// </summary>
        public void Clear()
        {
            symbol = ' ';
            Draw();
        }

        /// <summary>
        /// Находятся ли две точки в одном месте на экране
        /// </summary>
        /// <param name="p"></param>
        /// <returns>Да/Нет</returns>
        public bool IsEquals( Point p )
        {
            return p.x == this.x && p.y == this.y;
        }

    }
}
