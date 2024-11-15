using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    /// <summary>
    /// Класс, содержащий фигуру змейки
    /// </summary>
    class Snake : Figure
    {
        // Направление движения змейки
        Direction direction;

        /// <summary>
        /// Инициализация змейки
        /// </summary>
        /// <param name="tail">Точка хвоста змейки</param>
        /// <param name="length">Длина змейки</param>
        /// <param name="direction">Направление движения</param>
        public Snake(Point tail, int length, Direction direction)
        {
            this.direction = direction;
            pointList = new List<Point>();

            // Для каждой точки змейки, начиная от хвоста
            for (int i = 0; i < length; i++)
            {
                Point p = new Point( tail );
                p.Move( i , direction );
                // Список точек змейки
                pointList.Add( p );
            }
        }

        /// <summary>
        /// Движение змейки: удаляем хвост, добавляем голову
        /// </summary>
        public void Move()
        {
            // Удаление хвоста змейки
            Point tail = pointList.First();
            pointList.Remove( tail );

            // Новая точка головы змейки
            Point head = GetHeadPoint();
            pointList.Add( head );

            // На экране: удаляем точку хвоста, отображаем точку головы
            tail.Clear();
            head.Draw();
        }

        // Получить новую точку головы
        public Point GetHeadPoint()
        {
            Point p = pointList.Last();
            Point head = new Point( p );
            head.Move( 1, direction );
            return head;
        }

        /// <summary>
        /// Встретилась ли голова змейки с едой
        /// </summary>
        /// <param name="food"></param>
        /// <returns>Да/Нет</returns>
        public bool Eat(Point food)
        {
            // Новая точка головы змейки
            Point head = GetHeadPoint();

            // Встретилась ли голова змейки  с едой
            if (head.IsEquals( food ))
            {
                food.symbol = head.symbol;
                food.Draw();
                pointList.Add( food );
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Изменение направления движения змейки
        /// </summary>
        /// <param name="key"></param>
        public void HandleKey( ConsoleKey key )
        {
            if (key == ConsoleKey.LeftArrow)
                direction = Direction.LEFT;
            else if (key == ConsoleKey.RightArrow)
                direction = Direction.RIGHT;
            else if (key == ConsoleKey.UpArrow)
                direction = Direction.UP;
            else if (key == ConsoleKey.DownArrow)
                direction = Direction.DOWN;
        }

        /// <summary>
        /// Не встретилась ли змейка со своим хвостом
        /// </summary>
        /// <returns>Да/Нет</returns>
        public bool IsTraversTail()
        {
            // Точка головы змейки
            Point head = pointList.Last();

            // Для всех точек змейки, кроме головы
            for (int i = 0; i < pointList.Count - 2; i++)
            {
                // Есть ли пересечение тела змейки с головой
                if (head.IsEquals(pointList[ i ])) return true;
            }
            return false;
        }

    }
}
