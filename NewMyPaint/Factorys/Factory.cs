namespace NewMyPaint
{
    public abstract class Factory
    {
        public virtual Figure GetShape()
        {
            return null;
        }
    }
}
