namespace Lf2datConverter.dat.Base
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
