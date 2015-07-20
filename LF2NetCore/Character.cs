using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace LF2NetCore
{
    public class Character
    {
        public string Name;
        public string HeadPicture;
        public string SmallPicture;
        public List<SpriteFile> SpriteFiles = new List<SpriteFile>();
        public List<CharacterFrame> Frames = new List<CharacterFrame>();
	    public Vector3 WalkingSpeed;
    }
}