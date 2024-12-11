using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace paint
{
    internal class CollectionFig
    {
        internal List<Fig> collection = new List<Fig>();
        public void Add(Fig fig)
        {
            collection.Add(fig);
        }
        public void clear()
        {
            collection.Clear();
        }
        public void Remove(Fig fig)
        {
            if(collection.Contains(fig))
                collection.Remove(fig);
        }
        public void Draw(Canvas canvas)
        {
            foreach (Fig fig in collection)
            {
                if(!canvas.Children.Contains(fig.GetFigure()))
                    fig.Show(canvas);
            }
        }
        public Fig FindFigure(Shape findfig)
        {
            foreach (var child in collection)
            {
                if (findfig == child.GetFigure())
                {
                    return child;
                }
            }
            return null;
        }
    }
}
