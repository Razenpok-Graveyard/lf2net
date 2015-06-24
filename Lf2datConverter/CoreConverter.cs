using System.IO;
using System.Linq;
using LF2NetCore;
using Newtonsoft.Json;

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
            return new SpriteFile()
            {
                Columns = spriteFile.Columns,
                FinishID = spriteFile.FinishID,
                Height = spriteFile.Height,
                Rows = spriteFile.Rows,
                Filename = ExtractPictureName(spriteFile.Sprite),
                StartID = spriteFile.StartID,
                Width = spriteFile.Width
            };
        }

        private static CharacterFrame ConvertFrame(dat.Convenient.CharacterFrame frame)
        {
            return new CharacterFrame()
            {
                FrameNumber = frame.FrameNumber,
                Next = frame.Next,
                Pic = frame.Pic,
                Wait = frame.Wait
            };
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