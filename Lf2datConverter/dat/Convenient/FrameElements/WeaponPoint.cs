using Microsoft.Xna.Framework;

namespace Lf2datConverter.dat.Convenient
{
    class WeaponPoint: FrameElement, IPointFrameElement, IForceElement, ICoveringElement
    {
        public int Kind;
        public int WeaponAct;
        public int Attacking;
        public Point Position { get; set; }
        public Vector3 VelocityDelta { get; set; }
        public int Cover { get; set; }
    }
}
