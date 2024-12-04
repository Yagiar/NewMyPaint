namespace NewMyPaint
{
    internal class TriangleFactory : Factory
    {
        public override Figure GetShape()
        {
            return new MyTriangle();
        }
    }
}
