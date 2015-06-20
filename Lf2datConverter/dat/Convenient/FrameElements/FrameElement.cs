using Microsoft.Xna.Framework;

namespace Lf2datConverter.dat.Convenient
{
    abstract class FrameElement
    {
        public int X;
        public int Y;
        public string Name;

        public FrameElement()
        {
            Name = GetType().Name;
        }
    }
}
