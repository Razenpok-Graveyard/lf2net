using Microsoft.Xna.Framework;

namespace Lf2datConverter.dat.Convenient
{
    class CatchingPoint : FrameElement, IPointFrameElement
    {
        public int Injury;
        public int VAction;
        public int AAction;
        public int JAction;
        public int TAction;
        public int ThrowVX;
        public int ThrowVY;
        public int ThrowVZ;
        public int Hurtable;
        public int ThrowInjury;
        public int Decrease;
        public int DirControl;
        public int Cover;
        public Point Position { get; set; }
    }
}
