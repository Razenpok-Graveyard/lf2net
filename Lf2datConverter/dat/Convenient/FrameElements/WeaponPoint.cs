using Microsoft.Xna.Framework;

namespace Lf2datConverter.dat.Convenient
{
    class WeaponPoint: FrameElement, IPointFrameElement, IMovementProducingElement
    {
        public int Kind;
        public int WeaponAct;
        public int Attacking;
        public int Cover;
        public int DVZ;
        public Point Position { get; set; }
        public Vector3 VelocityDelta { get; set; }
    }
}
