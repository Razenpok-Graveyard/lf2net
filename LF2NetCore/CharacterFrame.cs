using System.Collections.Generic;

namespace LF2NetCore
{
    public class CharacterFrame
    {
        public int FrameNumber;
        public int Pic;
        public int Wait;
        public int Next;
        public bool Interruptable;
        public Dictionary<Controls, int> Actions = new Dictionary<Controls, int>();
    }
}