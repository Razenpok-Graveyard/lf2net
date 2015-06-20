using Microsoft.Xna.Framework;

namespace Lf2datConverter.dat.Convenient
{
    class SonataOfDeath: Interaction, IHittableInteraction, IDamagingInteraction
    {
        public int ZWidth;
        public int VRest { get; set; }
        public int Injury { get; set; }
    }
}
