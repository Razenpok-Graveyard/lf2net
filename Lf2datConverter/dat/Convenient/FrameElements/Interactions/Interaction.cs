using Microsoft.Xna.Framework;

namespace LF2datConverter.dat.Convenient
{
    abstract class Interaction: FrameElement, IRectangularFrameElement
    {
        public Rectangle Area { get; set; }
    }
}
