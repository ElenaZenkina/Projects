using System.Collections.Generic;

namespace Snake
{
    /// <summary>
    /// Класс, содержащий прямую линию
    /// </summary>
    class Line : Figure
    {
        /// <summary>
        /// Конструктор объекта "линия"
        /// </summary>
        /// <param name="point">Начальная точка линии</param>
        /// <param name="pointCount">Количество точек в линии</param>
        /// <param name="direction">Направление рисования линии</param>
        public Line(Point point, int pointCount, Direction direction)
        {
            // Список точек линии
            pointList = new List<Point>();
            Point p;

            // Для всех точек линии
            for (int i = 0; i < pointCount; i++)
            {
                // Первая точка линии
                p = new Point( point );
                // Смещение относительно первой точки
                p.Move( i, direction );
                // Добавляем в список точек линии
                pointList.Add(p);
            }
        }
    }
}
