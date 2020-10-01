// Unity Framework
using UnityEngine;

namespace FrameLord
{
	public static class ColorEx
	{
		/// <summary>
		/// Convert from Color struct to an integer representation of the color
		/// </summary>
		public static int ConvertFromColor(Color c)
		{
			return (System.Convert.ToInt32(c.r * 255) << 16) + (System.Convert.ToInt32(c.g * 255) << 8) + System.Convert.ToInt32(c.b * 255);
		}

		/// <summary>
		/// Convert from an integer representation of a color to a Color struct
		/// </summary>
		public static Color ConvertFromInt(int c)
		{
			float r = ((c & 0xff0000) >> 16) / 255f;
			float g = ((c & 0xff00) >> 8) / 255f;
			float b = (c & 0xff) / 255f;
			return new Color(r, g, b);
		}
	}
}
