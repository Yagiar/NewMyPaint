using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NewMyPaint
{
    internal class CreateCommand : Command
    {
        private Figure figure;
        private Factory factory;
        private FiguresCollection collection;
        private Point start;
        private Point end;

        public CreateCommand(FiguresCollection collection, Factory factory, Point start, Point end)
        {
            this.collection = collection;
            this.factory = factory;
            this.start = start;
            this.end = end;
        }
        public void Execute()
        {
            figure = factory.GetShape();
            figure.startPoint = start;
            figure.endPoint = end;
            collection.Add(figure);
        }

        public void Undo()
        {
            if (figure != null)
            {
                collection.Remove(figure);
            }
        }
    }
}
