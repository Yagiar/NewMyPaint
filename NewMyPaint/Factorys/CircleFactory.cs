namespace NewMyPaint
{
    internal class CircleFactory : Factory
    {
        public override Figure GetShape()
        {
            return new MyCircle();
        }
    }
}
