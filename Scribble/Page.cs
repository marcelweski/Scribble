using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Scribble
{
	public class Page : DarkTheme.Page
	{
		protected new FrmMain Parent { get => base.Parent as FrmMain; }
	}
}
