using Microsoft.Xna.Framework;

namespace Lf2datConverter.dat.Convenient
{
    class WeaponPoint: FrameElement, IPointFrameElement, I2DMovableElement
    {
        public int Kind;
        public int WeaponAct;
        public int Attacking;
        public int Cover;
        public int DVZ;
        public Point Position { get; set; }
        public Point VelocityDelta { get; set; }
    }
}
