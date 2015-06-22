using Microsoft.Xna.Framework;

namespace LF2datConverter.dat.Convenient
{
    class HealBall: Interaction, IDamagingElement, IForceElement
    {
        public int Injury { get; set; }
        public Vector3 VelocityDelta { get; set; }
    }
}
