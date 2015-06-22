namespace LF2datConverter.dat.Base
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
