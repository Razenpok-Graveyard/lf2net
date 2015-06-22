using Microsoft.Xna.Framework;

namespace LF2datConverter.dat.Convenient
{
    class ReflectiveShield: Interaction, IHittableInteraction, IDamagingElement, IForceElement
        , IFallPointDealerInteraction
    {
        public int VRest { get; set; }
        public int Injury { get; set; }
        public Vector3 VelocityDelta { get; set; }
        public int FallPoints { get; set; }
    }
}
