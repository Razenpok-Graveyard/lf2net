using Microsoft.Xna.Framework;

namespace Lf2datConverter.dat.Convenient
{
    class Falling: Interaction, IHittableInteraction, IDamagingElement, IForceElement
        , IFallPointDealerInteraction, IDefendableInteraction
    {
        public int VRest { get; set; }
        public int Injury { get; set; }
        public Vector3 VelocityDelta { get; set; }
        public int FallPoints { get; set; }
        public int BDefend { get; set; }
    }
}
