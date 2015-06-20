using Microsoft.Xna.Framework;

namespace Lf2datConverter.dat.Convenient
{
    class FrostWhirlWind: Interaction, IMovementProducingElement, IHittableInteraction, IDamagingElement
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
