using Microsoft.Xna.Framework;

namespace LF2datConverter.dat.Convenient
{
    class WhirlWind: Interaction, IForceElement, IHittableInteraction, IDamagingElement
        , IFallPointDealerInteraction, IZVerticalInteraction, IDefendableInteraction
    {
        public Vector3 VelocityDelta { get; set; }
        public int VRest { get; set; }
        public int Injury { get; set; }
        public int FallPoints { get; set; }
        public int ZWidth { get; set; }
        public int BDefend { get; set; }
    }
}