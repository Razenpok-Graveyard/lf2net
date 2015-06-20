using Microsoft.Xna.Framework;

namespace Lf2datConverter.dat.Convenient
{
    class WeaponStrength: Interaction, IDamagingElement, IMovementProducingElement, IFallPointDealerInteraction
        , IDefendableInteraction
    {
        public int Injury { get; set; }
        public Vector3 VelocityDelta { get; set; }
        public int FallPoints { get; set; }
        public int BDefend { get; set; }
    }
}
