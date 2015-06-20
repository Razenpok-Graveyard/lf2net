using Microsoft.Xna.Framework;

namespace Lf2datConverter.dat.Convenient
{
    class WeaponStrength: Interaction, IDamagingInteraction, IMovementProducingElement
    {
        public int Fall;
        public int BDefend;
        public int Injury { get; set; }
        public Vector3 VelocityDelta { get; set; }
    }
}
