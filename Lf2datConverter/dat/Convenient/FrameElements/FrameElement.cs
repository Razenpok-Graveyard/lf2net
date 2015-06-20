using Microsoft.Xna.Framework;

namespace Lf2datConverter.dat.Convenient
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
