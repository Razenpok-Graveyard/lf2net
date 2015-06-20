using Microsoft.Xna.Framework;

namespace Lf2datConverter.dat.Convenient
{
    class SonataOfDeath: Interaction, IHittableInteraction, IDamagingElement, IZVerticalInteraction
    {
        public int VRest { get; set; }
        public int Injury { get; set; }
        public int ZWidth { get; set; }
    }
}
