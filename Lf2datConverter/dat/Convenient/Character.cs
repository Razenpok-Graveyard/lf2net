using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Lf2datConverter.dat.Convenient
{
    public class Character
    {
        public string Name;
        public string HeadPicture;
        public string SmallPicture;
        public List<SpriteFile> SpriteFiles = new List<SpriteFile>();
        public int WalkingFrameRate;
        public Vector3 WalkingSpeed;
        public int RunningFrameRate;
        public Vector3 RunningSpeed;
        public Vector3 HeavyWalkingSpeed;
        public Vector3 HeavyRunningSpeed;
        public Vector3 JumpDistance;
        public Vector3 DashDistance;
        public float RowingHeight;
        public float RowingDistance; 
        public List<CharacterFrame> Frames = new List<CharacterFrame>(); 
    }
}
