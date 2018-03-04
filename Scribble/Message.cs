using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scribble
{
	public static class Message
	{
		private static Control control;

		public static void init(Control parent)
		{
			control = new Control();
			control.BackColor = System.Drawing.Color.Black;
			control.Visible = false;
			control.Top = 0;
			control.Left = 0;

			parent.Controls.Add(control);
			parent.SizeChanged += Parent_SizeChanged;
		}

		private static void Parent_SizeChanged(object sender, EventArgs e)
		{
			var s = sender as DarkTheme.Form;

			control.Top = s.Height / 3;
			control.Left = s.BorderOffset;
			control.Width = s.Width;
			control.Height = s.Height / 3;
		}

		public static void showInfo(string text)
		{
			control.Visible = true;
			control.BringToFront();
		}

		public static void close()
		{
			control.Visible = false;
		}
	}
}
