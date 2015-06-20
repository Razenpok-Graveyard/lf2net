using Microsoft.Xna.Framework;

namespace Lf2datConverter.dat.Convenient
{
    class WeaponPoint: FrameElement, IPointFrameElement
    {
        public int Kind;
        public int WeaponAct;
        public int Attacking;
        public int Cover;
        public int DVX;
        public int DVY;
        public int DVZ;
        public Point Position { get; set; }
    }
}
