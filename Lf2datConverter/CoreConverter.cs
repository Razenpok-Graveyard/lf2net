using System.Collections.Generic;
using System.Linq;
using LF2NetCore;

namespace LF2datConverter
{
    static class CoreConverter
    {
        public static Character ConvertConvenient(dat.Convenient.Character character)
        {
            return new Character
            {
                Name = character.Name,
                HeadPicture = ExtractPictureName(character.HeadPicture),
                SmallPicture = ExtractPictureName(character.SmallPicture),
                SpriteFiles = character.SpriteFiles.Select(ConvertSpriteFile).ToList(),
                Frames = character.Frames.Select(ConvertFrame).ToList()
            };
        }

        private static SpriteFile ConvertSpriteFile(dat.Convenient.SpriteFile spriteFile)
        {
            return new SpriteFile
            {
                Columns = spriteFile.PicturesInRow,
                FrameHeight = spriteFile.Height + 1,
                Rows = spriteFile.PicturesInColumn,
                Filename = ExtractPictureName(spriteFile.Sprite),
                StartingFrame = spriteFile.StartID,
                FrameCount = spriteFile.FinishID - spriteFile.StartID + 1,
                FrameWidth = spriteFile.Width + 1
            };
        }

        private static CharacterFrame ConvertFrame(dat.Convenient.CharacterFrame frame)
        {
            return new CharacterFrame
            {
                FrameNumber = frame.FrameNumber,
                Next = frame.Next,
                Pic = frame.Pic,
                Wait = (frame.Wait + 1)*2,
                Actions = GetFrameActions(frame),
                Interruptable = frame.State == 0,
                Center = frame.Center
            };
        }

        private static Dictionary<Controls, int> GetFrameActions(dat.Convenient.CharacterFrame frame)
        {
            var actions = new Dictionary<Controls, int>();
            if (frame.State == 1 || frame.State == 0)
            {
                var nextFrame = 5;
                switch (frame.FrameNumber)
                {
                    case 5:
                    case 6:
                    case 7:
                    {
                        nextFrame = frame.FrameNumber+1;
                        break;
                    }
                }
                actions.Add(Controls.Down, nextFrame);
                actions.Add(Controls.Left, nextFrame);
                actions.Add(Controls.Right, nextFrame);
                actions.Add(Controls.Up, nextFrame);
                actions.Add(Controls.Jump, 210);
                actions.Add(Controls.Attack, 60);
            }
            return actions;
        }

        private static string ExtractPictureName(string oldPath)
        {
            return oldPath.Split('\\').Last().Split('.').First() + ".png";
        }

        private static string ConvertSound(string oldPath)
        {
            return oldPath.Split('\\').Last().Split('.').First() + ".wav";
        }
    }
}