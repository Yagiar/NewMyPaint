using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NewMyPaint
{
    internal class UngroupCommand : Command
    {
        private FiguresCollection collection;
        private Figure group;
        private List<Figure> ungroupedFigures;

        public UngroupCommand(FiguresCollection collection, Figure group)
        {
            this.collection = collection;
            this.group = group;
        }

        public void Execute()
        {
            // Разгруппировать выбранную группу
            ungroupedFigures = collection.Ungroup(group);
        }

        public void Undo()
        {
            // Восстановить группу, если ungroupedFigures не пуст
            if (ungroupedFigures != null && ungroupedFigures.Count > 0)
            {
                collection.GroupSelected(ungroupedFigures);
            }
        }
    }
}
