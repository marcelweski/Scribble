using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DarkTheme
{
	public class Page : UserControl
	{
		public Form Form { get => (this.FindForm() as Form); }

		public event EventHandler Closed;

		public Page()
		{
			this.Left = 1;
			this.Top = 24 + 1;
			this.Width = 1098 - (1 * 2);
			this.Height = 600 - (24 + (1 * 2));

			this.BackColor = System.Drawing.Color.Transparent;
			//this.DoubleBuffered = true;
			this.Load += Page_Load;
		}

		public void Close()
		{
			this.Enabled = false;
			this.Hide();
			this.Closed?.Invoke(this, new EventArgs());
		}

		private void Page_Load(object sender, EventArgs e)
		{
			try
			{
				this.UpdateSize(null, null);
			}
			catch { }
		}

		public void UpdateSize(object sender, EventArgs e)
		{
			this.Left = this.Form.BorderOffset;
			this.Top = 24 + this.Form.BorderOffset;
			this.Width = this.Form.Width - (this.Form.BorderOffset * 2);
			this.Height = this.Form.Height - (24 + (this.Form.BorderOffset * 2));
		}

		
	}
}
