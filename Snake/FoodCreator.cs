using System;

namespace Snake
{
    /// <summary>
    /// Класс, создающий еду в случайном месте экрана
    /// </summary>
    static class FoodCreator
    {
        private static Random random = new Random();

        public static Point CreateFood( int mapWidth, int mapHeight, char symbol )
        {
            int x = random.Next( 2, mapWidth - 2 );
            int y = random.Next( 2, mapHeight - 2 );
            //Point food = new Point(x, y, symbol);
            //food.Draw();
            //return food;
            return new Point( x, y, symbol );
        }
    }
}
