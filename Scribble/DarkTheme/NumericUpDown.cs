using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace DarkTheme
{
	public class NumericUpDown : Control
	{
		private System.Windows.Forms.NumericUpDown nup;

		public decimal Value { get => this.nup.Value; set => this.nup.Value = value; }
		public event EventHandler ValueChanged { add => this.nup.ValueChanged += value; remove => this.nup.ValueChanged -= null; }

		public NumericUpDown()
		{
			this.nup = new System.Windows.Forms.NumericUpDown();
			this.nup.Left = 10;
			this.nup.Top = 5;
			this.nup.BorderStyle = BorderStyle.None;
			this.nup.Font = new Font("Consolas", 12.0f);
			this.nup.Increment = 1;
			this.nup.Minimum = 1;
			this.nup.Maximum = 100;
			this.Controls.Add(this.nup);

			this.Size = new Size(100, 29);
			this.BackColor = System.Drawing.Color.White;
		}

		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);

			this.nup.Width = this.Width - (this.nup.Left * 2);
		}
	}
}
