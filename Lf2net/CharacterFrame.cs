using System.Collections.Generic;
using LF2NetCore;
using Microsoft.Xna.Framework.Graphics;

namespace LF2Net
{
    public class CharacterFrame
    {
        public Texture2D Picture;
        public CharacterFrame NextFrame;
        public int Next;
        public int FrameNumber;
        public int Wait;
        public Dictionary<Controls, int> Actions;

        public CharacterFrame(LF2NetCore.CharacterFrame frame, List<Texture2D> sprites)
        {
            Next = frame.Next;
            Picture = sprites[frame.Pic];
            FrameNumber = frame.FrameNumber;
            Wait = frame.Wait;
            Actions = frame.Actions;
        }
    }
}
