using System.Collections.Generic;
using System.IO;
using System.Linq;
using LF2NetCore;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;

namespace LF2Net
{
    public class Character
    {
        private List<CharacterFrame> frames;
        public CharacterFrame CurrentFrame;
        private int tillNextFrame;
        private bool facingRight = true;

        public Character(CharacterFrame startingFrame)
        {
            AssignCurrentFrame(startingFrame);
        }

        public void Update()
        {
            tillNextFrame--;
            if (tillNextFrame > 0) return;
            AssignCurrentFrame(CurrentFrame.NextFrame);
        }

        private void AssignCurrentFrame(CharacterFrame frame)
        {
            CurrentFrame = frame;
            tillNextFrame = (frame.Wait + 1) * 2;
        }

        public void HandleControls(List<Controls> pressedControls)
        {
            var nextFrames =
                pressedControls.Where(control => CurrentFrame.Actions.ContainsKey(control))
                    .Select(control => CurrentFrame.Actions[control]);
            var nextFrameNumber = nextFrames.First();
            var nextFrame = frames.First(frame => frame.FrameNumber == nextFrameNumber);
            AssignCurrentFrame(nextFrame);
            if (pressedControls.Contains(Controls.Right))
                facingRight = true;
            if (pressedControls.Contains(Controls.Left))
                facingRight = false;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            var reversed = facingRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            spriteBatch.Begin();
            spriteBatch.Draw(CurrentFrame.Picture, position, effects:reversed);
            spriteBatch.End();
        }
    }
}