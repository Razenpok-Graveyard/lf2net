namespace Lf2datConverter
{
    abstract class FrameElement
    {
        public string Name;

        public FrameElement()
        {
            Name = GetType().Name;
        }
    }
}
