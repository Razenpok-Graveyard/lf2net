using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace LF2NetCore
{
    public class CharacterFrame
    {
        public int FrameNumber;
        public int Pic;
        public int Wait;
        public int Next;
        public Dictionary<Controls, int> Actions = new Dictionary<Controls, int>();
    }
}