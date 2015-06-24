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
        public Dictionary<Keys, Controls> Controls = new Dictionary<Keys, Controls>(); 
        private List<CharacterFrame> frames;
        public CharacterFrame CurrentFrame;
        private int tillNextFrame;
        private bool facingRight = true;

        public Character(string path, string name, ContentManager contentManager)
        {
            var convCharacter =
                JsonConvert.DeserializeObject<LF2NetCore.Character>(File.ReadAllText("Content/" + path + name + ".json"));
            var sprites = new List<Texture2D>();
            foreach (var spriteFile in convCharacter.SpriteFiles)
            {
                var spriteSheet = contentManager.Load<Texture2D>(path + spriteFile.Filename.Split('.').First());
                sprites.InsertRange(spriteFile.StartID,
                    SplitSpriteSheet(spriteSheet, spriteFile.Width, spriteFile.Height, spriteFile.Rows,
                        spriteFile.Columns, spriteFile.FinishID - spriteFile.StartID + 1));
            }
            frames = convCharacter.Frames.Select(frame => new CharacterFrame(frame, sprites)).ToList();
            foreach (var frame in frames)
            {
                if (frame.Next == 999)
                    frame.Next = 0;
                frame.NextFrame = frames.Find(f => f.FrameNumber == frame.Next);
            }
            AssignCurrentFrame(frames.First());
        }

        public void Update()
        {
            tillNextFrame--;
            if (tillNextFrame > 0) return;
            if (!HandleControls())
                AssignCurrentFrame(CurrentFrame.NextFrame);
        }

        private void AssignCurrentFrame(CharacterFrame frame)
        {
            CurrentFrame = frame;
            tillNextFrame = (frame.Wait + 1) * 2;
        }

        private bool HandleControls()
        {
            var pressed =
                Controls.Where(key => Keyboard.GetState().IsKeyDown(key.Key)).ToList();
            if (!pressed.Any()) return false;
            var pressedControls = pressed.Select(key => key.Value).ToList();
            var pressedKeys = pressed.Select(key => key.Key).ToList();
            var nextFrames =
                pressedControls.Where(control => CurrentFrame.Actions.ContainsKey(control))
                    .Select(control => CurrentFrame.Actions[control]);
            var nextFrameNumber = nextFrames.First();
            var nextFrame = frames.First(frame => frame.FrameNumber == nextFrameNumber);
            AssignCurrentFrame(nextFrame);
            if (pressedKeys.Contains(Keys.Right))
                facingRight = true;
            if (pressedKeys.Contains(Keys.Left))
                facingRight = false;
            return true;
        }

        private IEnumerable<Texture2D> SplitSpriteSheet(Texture2D origin, int width, int height, int rows, int columns, int count)
        {
            width++;
            height++;
            return
                Enumerable.Range(0, count)
                    .Select(frameNumber => GetFrame(origin, width, height, frameNumber/columns, frameNumber%columns));
        }

        private Texture2D GetFrame(Texture2D origin, int width, int height, int row, int column)
        {
            var sourceRectangle = new Rectangle(width * column, height * row, width, height);
            var frame = new Texture2D(origin.GraphicsDevice, sourceRectangle.Width, sourceRectangle.Height);
            var data = new Color[sourceRectangle.Width * sourceRectangle.Height];
            origin.GetData(0, sourceRectangle, data, 0, data.Length);
            frame.SetData(data);
            return frame;
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