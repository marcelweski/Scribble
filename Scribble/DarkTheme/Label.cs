using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DarkTheme
{
	public class Label : System.Windows.Forms.Label
	{
		public Label()
		{
			this.Font = new System.Drawing.Font("Segoe UI", 12.0f);

			this.ForeColor = System.Drawing.Color.White;
			this.BackColor = System.Drawing.Color.Transparent;
		}
	}
}
