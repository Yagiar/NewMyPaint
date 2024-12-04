using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NewMyPaint
{
    internal class GroupCommand : Command
    {
        private FiguresCollection collection;
        private List<Figure> figuresToGroup;
        private Figure groupedFigure;

        public GroupCommand(FiguresCollection collection, List<Figure> figuresToGroup)
        {
            this.collection = collection;
            this.figuresToGroup = new List<Figure>(figuresToGroup); // Сохранить копию выбранных фигур
        }

        public void Execute()
        {
            // Группируем выбранные фигуры и сохраняем группу
            groupedFigure = collection.GroupSelected(figuresToGroup);
        }

        public void Undo()
        {
            // Разгруппировать, если groupedFigure существует
            if (groupedFigure != null)
            {
                collection.Ungroup(groupedFigure);
                groupedFigure = null;
            }
        }
    }
}
