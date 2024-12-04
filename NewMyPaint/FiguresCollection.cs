using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace NewMyPaint
{
    internal class FiguresCollection
    {
        private List<Figure> figures;
        public FiguresCollection()
        {
            figures = new List<Figure>();
        }
        public void Add(Figure f)
        {
            figures.Add(f);
        }
        public void Remove(Figure f)
        {
            figures.Remove(f);
        }
        public void Clear()
        {
            figures.Clear();
        }
        public void Draw(Canvas can)
        {
            foreach (Figure f in figures)
            {
                f.Show(can);
            }
        }
        public List<Figure> GetFigures()
        {
            return figures;
        }
        public Figure GroupSelected(List<Figure> selectedFigures)
        {
            GroupShape group = new GroupShape();
            foreach (var figure in selectedFigures)
            {
                group.Add(figure);
                figures.Remove(figure);  // Удаляем фигуры из основной коллекции
            }

            figures.Add(group); // Добавляем группу в коллекцию
            return group;
        }

        public List<Figure> Ungroup(Figure group)
        {
            if (group is GroupShape grp)
            {
                // Удаляем группу из коллекции
                figures.Remove(group);

                // Добавляем все фигуры из группы обратно в коллекцию
                List<Figure> ungroupedFigures = grp.Ungroup();
                figures.AddRange(ungroupedFigures);

                // Возвращаем список разгруппированных фигур
                return ungroupedFigures;
            }

            return new List<Figure>(); // Возвращаем пустой список, если переданный объект не является группой
        }

    }
}
