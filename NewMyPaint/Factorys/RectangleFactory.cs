namespace NewMyPaint
{
    internal class RectangleFactory : Factory
    {
        public override Figure GetShape()
        {
            return new MyRectangle();
        }
    }
}
