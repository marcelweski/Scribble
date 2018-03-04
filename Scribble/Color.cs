using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkTheme
{
	public class Color
	{
		public static System.Drawing.Color FormBackground = System.Drawing.Color.FromArgb(16, 16, 16);
		public static System.Drawing.Color TitleBarButtonOver = System.Drawing.Color.FromArgb(64, 64, 64);
		public static System.Drawing.Color TitleBarButtonDown = System.Drawing.Color.FromArgb(128, 128, 128);

		public static System.Drawing.Color ButtonBlue = System.Drawing.Color.FromArgb(64, 128, 255);
		public static System.Drawing.Color ButtonBlueOver = System.Drawing.Color.FromArgb(96, 160, 255);

		public static System.Drawing.Color GetOverBackColor(System.Drawing.Color color)
		{
			if (color == ButtonBlue) return ButtonBlueOver;

			return System.Drawing.Color.FromArgb(128, 128, 128);
		}
	}
}
