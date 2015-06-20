using System.Collections.Generic;

namespace Lf2datConverter.dat.Convenient
{
    class Character
    {
        public string Name;
        public string Head;
        public string Small;
        public List<SpriteFile> SpriteFiles = new List<SpriteFile>();
        public int WalkingFrameRate;
        public float WalkingSpeed;
        public float WalkingSpeedZ;
        public int RunningFrameRate;
        public float RunningSpeed;
        public float RunningSpeedZ;
        public float HeavyWalkingSpeed;
        public float HeavyWalkingSpeedZ;
        public float HeavyRunningSpeed;
        public float HeavyRunningSpeedZ;
        public float JumpHeight;
        public float JumpDistance;
        public float JumpDistanceZ;
        public float DashHeight;
        public float DashDistance;
        public float DashDistanceZ;
        public float RowingHeight;
        public float RowingDistance; 
        public List<CharacterFrame> Frames = new List<CharacterFrame>(); 
    }
}
