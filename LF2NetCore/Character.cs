using System.Collections.Generic;

namespace LF2NetCore
{
    public class Character
    {
        public string Name;
        public string HeadPicture;
        public string SmallPicture;
        public List<SpriteFile> SpriteFiles = new List<SpriteFile>();
        public List<CharacterFrame> Frames = new List<CharacterFrame>(); 
    }
}