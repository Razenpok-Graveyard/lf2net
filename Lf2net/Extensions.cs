using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace LF2Net
{
	public static class Extensions
	{
		public static Vector2 Clamp(this Vector2 v, Rectangle r)
		{
			return new Vector2(v.X.Clamp(r.Left, r.Right), v.Y.Clamp(r.Top, r.Bottom));
		}

		public static float Clamp(this float f, float min, float max)
		{
			return f > max ? max : f < min ? min : f;
		}
	}
}
