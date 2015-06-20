using Microsoft.Xna.Framework;

namespace Lf2datConverter.dat.Convenient
{
    class ObjectPoint: FrameElement, IPointFrameElement
    {
        public int Kind;
        public int Action;
        public int DVX;
        public int DVY;
        public int OID;
        public int Facing;
        public Point Position { get; set; }
    }
}
