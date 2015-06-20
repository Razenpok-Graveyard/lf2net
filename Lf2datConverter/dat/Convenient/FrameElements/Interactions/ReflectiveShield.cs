using Microsoft.Xna.Framework;

namespace Lf2datConverter.dat.Convenient
{
    class ReflectiveShield: Interaction, IHittableInteraction, IDamagingElement, IMovementProducingElement
        , IFallPointDealerInteraction
    {
        public int VRest { get; set; }
        public int Injury { get; set; }
        public Vector3 VelocityDelta { get; set; }
        public int FallPoints { get; set; }
    }
}
