using Microsoft.Xna.Framework;

namespace Lf2datConverter.dat.Convenient
{
    class ObjectPoint: FrameElement, IPointFrameElement, IForceElement
    {
        public int Kind;
        public int Action;
        public int ObjectID;
        public int Facing;
        public Point Position { get; set; }
        public Vector3 VelocityDelta { get; set; }
    }
}
