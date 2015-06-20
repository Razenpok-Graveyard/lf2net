using Microsoft.Xna.Framework;

namespace Lf2datConverter.dat.Convenient
{
    class Body: FrameElement, IRectangularFrameElement
    {
        public int Kind;
        public Rectangle Area { get; set; }
    }
}
