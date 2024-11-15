using System;
using System.Threading;

namespace Snake
{
    class Program
    {
        // Ширина и высота экрана
        const byte MAP_WIDTH = 80;
        const byte MAP_HEIGHT = 25;

        static void Main(string[] args)
        {
            // Устанавливаем размер экрана
            //Console.SetBufferSize( MAP_WIDTH, MAP_HEIGHT );

            // Рисуем границы экрана
            Borders borders = new Borders( MAP_WIDTH, MAP_HEIGHT );
            borders.Draw();

            // Создание и отрисовка змейки, начиная с хвоста
            Point tail = new Point( 4, 5, '*');
            Snake snake = new Snake( tail, 4, Direction.RIGHT );
            snake.Draw();

            // Создание и отрисовка еды
            Point food = CreateFood( snake );
            
            while (true)
            {
                // Если змейка встретилась со своим хвостом или 
                // с границей экрана - конец игры
                if (borders.IsTravers(snake) || snake.IsTraversTail())
                    break;

                // Если змейка встретилась с едой
                if (snake.Eat(food))
                    food = CreateFood(snake);
                else snake.Move();

                // Нажатие клавиш для управления змейкой
                if (Console.KeyAvailable)
                    snake.HandleKey(Console.ReadKey().Key);

                // Задержка в 200 мс
                Thread.Sleep(200);
            }
        }

        /// <summary>
        /// Создание и отрисовка еды
        /// </summary>
        /// <param name="snake"></param>
        /// <returns>Точку с едой</returns>
        private static Point CreateFood(Figure snake)
        {
            Point food = FoodCreator.CreateFood(MAP_WIDTH, MAP_HEIGHT, '$');
            // Еда не должна создаться в теле змейки
            while (snake.IsTravers(food))
            {
                food = FoodCreator.CreateFood(MAP_WIDTH, MAP_HEIGHT, '$');
            }

            food.Draw();
            return food;
        }
    }
}
