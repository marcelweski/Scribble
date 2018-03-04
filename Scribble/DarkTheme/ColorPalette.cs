using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DarkTheme
{
	public delegate void ColorChangedDelegate(object sender, System.Drawing.Color color);

	class ColorPalette : System.Windows.Forms.Control
	{
		//public System.Drawing.Color Color { get; set; }

		private int selectionIndex = 1;
		private System.Drawing.Color[] colors;

		public System.Drawing.Color SelectedColor { get => this.colors[this.selectionIndex]; }

		public event ColorChangedDelegate ColorChanged;

		public ColorPalette()
		{
			this.Size = new System.Drawing.Size(300, 30);
			//this.Color = System.Drawing.Color.Red;

			this.colors = new System.Drawing.Color[]
			{
				System.Drawing.Color.White,
				System.Drawing.Color.Black,
				System.Drawing.Color.Red,
				System.Drawing.Color.Orange,
				System.Drawing.Color.Yellow,
				System.Drawing.Color.Green,
				System.Drawing.Color.Blue,
				System.Drawing.Color.Purple
			};

			this.MouseClick += ColorPalette_MouseClick;
		}

		private void ColorPalette_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.X <= this.Height * this.colors.Length)
			{
				this.selectionIndex = (e.X + 1) / this.Height;
				this.Refresh();

				this.ColorChanged?.Invoke(this, this.SelectedColor);
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			var g = e.Graphics;
			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

			for (int i = 0; i < this.colors.Length; i++)
			{
				g.FillEllipse(new System.Drawing.SolidBrush(this.colors[i]), this.Height * i, 0, this.Height - 1, this.Height - 1);
				
				if (i == this.selectionIndex)
				{
					var pen = new System.Drawing.Pen(System.Drawing.Color.LightGray, 2.0f);
					g.DrawEllipse(pen, this.Height * i, 0, this.Height - 2, this.Height - 2);
				}
			}

			//var pen = new System.Drawing.Pen(System.Drawing.Color.White, 2.0f);
			//g.DrawEllipse(pen, 0, 0, this.Width-2, this.Height-2);

		}
	}
}
