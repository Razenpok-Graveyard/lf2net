using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;

namespace LF2Net.Loaders
{
    public static class CharacterLoader
    {
        //MonoGame requires that Content folder must not be in path
        private static Func<string, string> pathWithoutRoot =
            (path) => path.Substring(path.IndexOf(Path.DirectorySeparatorChar) + 1);

        public static Character LoadFromPath(string path, ContentManager contentManager)
        {
            var name = Path.GetFileName(path);
            var json = File.ReadAllText(Path.Combine(path, name + ".json"));
            var coreCharacter = JsonConvert.DeserializeObject<LF2NetCore.Character>(json);
            return LoadFromCore(coreCharacter, contentManager, path);
        }

        private static Character LoadFromCore(LF2NetCore.Character coreCharacter, ContentManager contentManager, string path)
        {
            path = pathWithoutRoot(path);
            var sprites = new List<Texture2D>();
            foreach (var spriteFile in coreCharacter.SpriteFiles)
            {
                var fileName = spriteFile.Filename.Split('.').First();
                var spriteSheet = contentManager.Load<Texture2D>(Path.Combine(path, fileName));
                sprites.InsertRange(spriteFile.StartID,
                    SplitSpriteSheet(spriteSheet, spriteFile.Width, spriteFile.Height, spriteFile.Rows,
                        spriteFile.Columns, spriteFile.FinishID - spriteFile.StartID + 1));
            }
            var frames = coreCharacter.Frames.Select(frame => new CharacterFrame(frame, sprites)).ToList();
            foreach (var frame in frames)
            {
                if (frame.Next == 999)
                    frame.Next = 0;
                frame.NextFrame = frames.Find(f => f.FrameNumber == frame.Next);
            }
            return new Character(frames.First());
        }

        private static IEnumerable<Texture2D> SplitSpriteSheet(Texture2D origin, int width, int height, int rows, int columns, int count)
        {
            width++;
            height++;
            return
                Enumerable.Range(0, count)
                    .Select(frameNumber => GetFrame(origin, width, height, frameNumber / columns, frameNumber % columns));
        }

        private static Texture2D GetFrame(Texture2D origin, int width, int height, int row, int column)
        {
            var sourceRectangle = new Rectangle(width * column, height * row, width, height);
            var frame = new Texture2D(origin.GraphicsDevice, sourceRectangle.Width, sourceRectangle.Height);
            var data = new Color[sourceRectangle.Width * sourceRectangle.Height];
            origin.GetData(0, sourceRectangle, data, 0, data.Length);
            frame.SetData(data);
            return frame;
        }
    }
}