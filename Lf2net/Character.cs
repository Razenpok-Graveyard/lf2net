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
		private bool alreadyCalculatedUpdate = false;
		public Vector3 WalkingSpeed;
		private Vector2 position = new Vector2(100, 0);

		public Character(CharacterFrame startingFrame)
		{
			AssignCurrentFrame(startingFrame);
		}

		public void Update()
		{
			tillNextFrame-= alreadyCalculatedUpdate ? 0 : 1;
			alreadyCalculatedUpdate = false;
			if (tillNextFrame > 0) return;
			AssignCurrentFrame(CurrentFrame.NextFrame);
		}

		private void AssignCurrentFrame(CharacterFrame frame)
		{
			CurrentFrame = frame;
			tillNextFrame = frame.Wait;
		}

		public void HandleControls(List<Controls> pressedControls)
		{
			tillNextFrame--;
			alreadyCalculatedUpdate = true;
			var nextFrames = pressedControls
				.Where(CurrentFrame.Actions.ContainsKey)
				.Select(control => CurrentFrame.Actions[control])
				.ToList();
			if ((tillNextFrame == 0 || CurrentFrame.Interruptable)&& nextFrames.Any())
				AssignCurrentFrame(nextFrames.First());
			var direction = Vector2.Zero;
			if (pressedControls.Contains(Controls.Down))
				direction += Vector2.UnitY;
			if (pressedControls.Contains(Controls.Up))
				direction += -Vector2.UnitY;
			if (pressedControls.Contains(Controls.Right))
				direction += Vector2.UnitX;
			if (pressedControls.Contains(Controls.Left))
				direction += -Vector2.UnitX;
			position += direction * new Vector2(WalkingSpeed.X/2, WalkingSpeed.Y/2);
			if (pressedControls.Contains(Controls.Right))
				facingRight = true;
			if (pressedControls.Contains(Controls.Left))
				facingRight = false;
		}

		public void Draw(SpriteBatch spriteBatch, Vector2 floorOrigin)
		{
			var reversed = facingRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			spriteBatch.Begin();
			var offset = -CurrentFrame.Center;
			if (!facingRight)
				offset = new Vector2(CurrentFrame.Center.X - CurrentFrame.Picture.Width, -CurrentFrame.Center.Y);
			spriteBatch.Draw(CurrentFrame.Picture, floorOrigin + position + offset, effects:reversed);
			spriteBatch.End();
		}
	}
}