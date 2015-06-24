using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
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

        public CharacterFrame(LF2NetCore.CharacterFrame frame, List<Texture2D> sprites)
        {
            Next = frame.Next;
            Picture = sprites[frame.Pic];
            FrameNumber = frame.FrameNumber;
            Wait = frame.Wait;
        }
    }
}
