using Microsoft.Xna.Framework;

namespace Lf2datConverter.dat.Convenient
{
    class FrostWhirlWind: Interaction, I2DMovableElement
    {
        public int VRest;
        public int Fall;
        public int BDefend;
        public int Injury;
        public int ZWidth;
        public Point VelocityDelta { get; set; }
    }
}
