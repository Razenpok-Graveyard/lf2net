using Microsoft.Xna.Framework;

namespace Lf2datConverter.dat.Convenient
{
    class NormalHit: Interaction, I2DMovableElement
    {
        public int ARest;
        public int VRest;
        public int Fall;
        public int BDefend;
        public int Injury;
        public int ZWidth;
        public int Effect;
        public Point VelocityDelta { get; set; }
    }
}
