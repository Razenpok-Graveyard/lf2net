using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lf2net
{
	class BattleField
	{
		private List<Character> characters = new List<Character>();
		private Texture2D floor;

		public int Width
		{
			get { return floor.Width; }
		}

		public int Height
		{
			get { return floor.Height; }
		}

		public BattleField(Texture2D floor)
		{
			this.floor = floor;
		}

		public void AddCharacter(Character character)
		{
			characters.Add(character);
			character.Position = new Vector2(100, 100);
		}

		public void Update()
		{
			foreach (var character in characters)
			{
				character.Update();
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			var floorOrigin = new Vector2(0, spriteBatch.GraphicsDevice.Viewport.Height - Height);
			spriteBatch.Begin();
			spriteBatch.Draw(floor, floorOrigin);
			spriteBatch.End();
			foreach (var character in characters)
			{
				character.Draw(spriteBatch, floorOrigin);
			}
		}
	}
}
