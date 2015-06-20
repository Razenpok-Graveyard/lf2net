using Microsoft.Xna.Framework;

namespace Lf2datConverter.dat.Convenient
{
    class CaughtPoint : FrameElement, IPointFrameElement
    {
        public int FrontHurtAct;
        public int BackHurtAct;
        public Point Position { get; set; }
    }
}
