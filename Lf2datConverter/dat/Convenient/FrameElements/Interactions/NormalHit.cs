using Microsoft.Xna.Framework;

namespace Lf2datConverter.dat.Convenient
{
    class NormalHit: Interaction, IMovementProducingElement, IHittableInteraction, IDamagingInteraction
    {
        public int ARest;
        public int Fall;
        public int BDefend;
        public int ZWidth;
        public int Effect;
        public Vector3 VelocityDelta { get; set; }
        public int VRest { get; set; }
        public int Injury { get; set; }
    }
}
