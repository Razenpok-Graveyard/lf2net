using System.Collections.Generic;
using LF2NetCore;
using Microsoft.Xna.Framework.Graphics;

namespace LF2Net
{
    public class CharacterFrame
    {
        public Texture2D Picture;
        public CharacterFrame NextFrame;
        public int Wait;
        public Dictionary<Controls, CharacterFrame> Actions = new Dictionary<Controls, CharacterFrame>();

        public CharacterFrame(){}
    }
}
