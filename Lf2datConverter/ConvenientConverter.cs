using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lf2datConverter.dat.Convenient;
using Microsoft.Xna.Framework;

namespace Lf2datConverter
{
    static class ConvenientConverter
    {
        public static Character ConvertToConvenientCharacter(dat.Base.Character character)
        {
            var convCharacter = new Character
            {
                Name = character.Name,
                Head = character.Head,
                Small = character.Small,
                SpriteFiles = character.SpriteFiles.Select(ConvertSpriteFile).ToList(),
                WalkingFrameRate = character.WalkingFrameRate,
                WalkingSpeed = new Vector3(character.WalkingSpeed, 0, character.WalkingSpeedZ),
                RunningFrameRate = character.RunningFrameRate,
                RunningSpeed = new Vector3(character.RunningSpeed, 0, character.RunningSpeedZ),
                HeavyWalkingSpeed = new Vector3(character.HeavyWalkingSpeed, 0, character.HeavyWalkingSpeedZ),
                HeavyRunningSpeed = new Vector3(character.HeavyRunningSpeed, 0, character.HeavyRunningSpeedZ),
                JumpHeight = character.JumpHeight,
                JumpDistance = new Vector3(character.JumpDistance, 0, character.JumpDistanceZ),
                DashHeight = character.DashHeight,
                DashDistance = new Vector3(character.DashDistance, 0, character.DashDistanceZ),
                RowingHeight = character.RowingHeight,
                RowingDistance = character.RowingDistance,
                Frames = character.Frames.Select(ConvertCharacterFrame).ToList()
            };

            return convCharacter;
        }

        private static SpriteFile ConvertSpriteFile(dat.Base.SpriteFile file)
        {
            var convFile = new SpriteFile()
            {
                Columns = file.Col,
                FinishID = file.FinishID,
                Height = file.H,
                Rows = file.Row,
                Sprite = file.Sprite,
                StartID = file.StartID,
                Width = file.W
            };
            return convFile;
        }

        private static CharacterFrame ConvertCharacterFrame(dat.Base.CharacterFrame frame)
        {
            var convFrame = new CharacterFrame()
            {
                FrameNumber = frame.FrameNumber,
                Name = frame.Name,
                Pic = frame.Pic,
                State = frame.State,
                Wait = frame.Wait,
                Next = frame.Next,
                DV = new Vector3(frame.DVX, frame.DVY, frame.DVZ),
                Center = new Vector2(frame.CenterX, frame.CenterY),
                HitA = frame.HitA,
                HitD = frame.HitD,
                HitJ = frame.HitJ,
                HitDA = frame.HitDA,
                HitDJ = frame.HitDJ,
                HitFA = frame.HitFA,
                HitFJ = frame.HitFJ,
                HitJA = frame.HitJA,
                HitUA = frame.HitUA,
                HitUJ = frame.HitUJ,
                MP = frame.MP,
                Sound = frame.Sound,
                FrameElements = frame.FrameElements.Select(ConvertFrameElement).ToList()
            };
            return convFrame;
        }

        private static FrameElement ConvertFrameElement(dat.Base.FrameElement frameElement)
        {
            return null;
        }
    }
}
