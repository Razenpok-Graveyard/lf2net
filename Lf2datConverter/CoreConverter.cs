using System.Collections.Generic;
using System.Linq;
using LF2NetCore;
using Microsoft.Xna.Framework;

namespace LF2datConverter
{
	static class CoreConverter
	{
		const int Offset = 63340;

		public static Character ConvertConvenient(dat.Convenient.Character character)
		{
			var c = new Character
			{
				Name = character.Name,
				HeadPicture = ExtractPictureName(character.HeadPicture),
				SmallPicture = ExtractPictureName(character.SmallPicture),
				SpriteFiles = character.SpriteFiles.Select(ConvertSpriteFile).ToList(),
				Frames = character.Frames.Select(ConvertFrame).ToList(),
				WalkingSpeed = character.WalkingSpeed.Swap()
			};
			AddFrames(c.Frames);
			RearrangeFrames(c.Frames);
			return c;
		}

		public static Vector3 Swap(this Vector3 v)
		{
			return new Vector3(v.X, v.Z, v.Y);
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
					case 8:
					{
						nextFrame = frame.FrameNumber + Offset;
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

		private static void AddFrames(List<CharacterFrame> frames)
		{
			var frameLibrary = frames.ToDictionary(f => f.FrameNumber);
			var frame = frameLibrary[7];
			var newFrame = frame.Copy();
			newFrame.Actions[Controls.Down] = Offset + 9;
			newFrame.Actions[Controls.Left] = Offset + 9;
			newFrame.Actions[Controls.Right] = Offset + 9;
			newFrame.Actions[Controls.Up] = Offset + 9;
			newFrame.FrameNumber = Offset + 8;
			frames.Add(newFrame);
			frame = frameLibrary[6];
			newFrame = frame.Copy();
			newFrame.Actions[Controls.Down] = 5;
			newFrame.Actions[Controls.Left] = 5;
			newFrame.Actions[Controls.Right] = 5;
			newFrame.Actions[Controls.Up] = 5;
			newFrame.FrameNumber = Offset + 9;
			frames.Add(newFrame);   
		}

		private static CharacterFrame Copy(this CharacterFrame frame)
		{
			return new CharacterFrame
			{
				Actions = new Dictionary<Controls, int>(frame.Actions),
				Center = frame.Center,
				FrameNumber = frame.FrameNumber,
				Interruptable = frame.Interruptable,
				Next = frame.Next,
				Pic = frame.Pic,
				Wait = frame.Wait
			};
		}

		private static void RearrangeFrames(List<CharacterFrame> frames)
		{
			
		}

		private static void SwapFrames(List<CharacterFrame> frames, int first, int second)
		{
			
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