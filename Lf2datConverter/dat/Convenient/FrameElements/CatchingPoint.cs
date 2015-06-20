using Microsoft.Xna.Framework;

namespace Lf2datConverter.dat.Convenient
{
    class CatchingPoint : FrameElement, IPointFrameElement, IMovementProducingElement
    {
        public int Injury;
        public int VAction;
        public int AAction;
        public int JAction;
        public int TAction;
        public int Hurtable;
        public int ThrowInjury;
        public int Decrease;
        public int DirControl;
        public int Cover;
        public Point Position { get; set; }
        public Vector3 VelocityDelta { get; set; }
    }
}
