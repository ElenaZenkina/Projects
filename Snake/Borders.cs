using System.Collections.Generic;

namespace Snake
{
    /// <summary>
    /// Класс линий, составляющих границу (рамку) экрана
    /// </summary>
    class Borders
    {
        // Список линий, входящих в рамку
        List<Figure> lineList;

        public Borders(int mapWidth, int mapHeight)
        {
            // Здесь хранится список линий
            lineList = new List<Figure>();
            
            // Создаем 4 линии по краям экрана
            Line line;
            line = new Line(new Point(0, 0, '+'), mapWidth - 2, Direction.RIGHT);
            lineList.Add(line);
            line = new Line(new Point(0, 0, '+'), mapHeight - 1, Direction.DOWN);
            lineList.Add(line);
            line = new Line(new Point(0, mapHeight - 1, '+'), mapWidth - 2, Direction.RIGHT);
            lineList.Add(line);
            line = new Line(new Point(mapWidth - 2, 0, '+'), mapHeight - 1, Direction.DOWN);
            lineList.Add(line);            
        }

        /// <summary>
        /// Отрисовка линий рамки
        /// </summary>
        public void Draw()
        {
            // Для всего списка линий
            foreach (var line in lineList)
            {
                // Отрисовка линии
                line.Draw();
            }
        }

        /// <summary>
        /// Пересекается ли рамка с фигурой
        /// </summary>
        /// <param name="figure"></param>
        /// <returns>Да/Нет</returns>
        public bool IsTravers(Figure figure)
        {
            // Для каждой линии в рамке
            foreach (var line in lineList)
            {
                // Пересекается ли линия с фигурой
                if (line.IsTravers( figure )) return true;
            }

            return false;
        }
    }
}
