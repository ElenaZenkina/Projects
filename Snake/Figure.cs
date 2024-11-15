using System.Collections.Generic;

namespace Snake
{
    /// <summary>
    /// Класс Фигура - состоит из набора (списка) точек
    /// </summary>
    class Figure
    {
        // Список точек фигуры
        protected List<Point> pointList;

        /// <summary>
        /// Отображение линии на экране по точкам
        /// </summary>
        public void Draw()
        {
            // Для каждой точки линии
            foreach (Point p in pointList)
            {
                // Отрисовка точки
                p.Draw();
            }
        }

        /// <summary>
        /// Пересекается ли с другой фигурой
        /// </summary>
        /// <param name="figure"></param>
        /// <returns>Да/Нет</returns>
        public bool IsTravers(Figure figure)
        {
            // Для каждой точки фигуры
            foreach (var p in pointList)
            {
                // Пересекается ли точка с фигурой
                if (figure.IsTravers( p )) return true;
            }

            return false;
        }

        /// <summary>
        /// Пересекается ли с точкой
        /// </summary>
        /// <param name="point"></param>
        /// <returns>Да/Нет</returns>
        public bool IsTravers(Point point)
        {
            // Для каждой точки фигуры
            foreach (var p in pointList)
            {
                // Равны ли координаты двух точек
                if (p.IsEquals( point )) return true;
            }

            return false;
        }
    }
}
