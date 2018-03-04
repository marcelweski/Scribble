using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DarkTheme
{
	public class Button : System.Windows.Forms.Button
	{
		public Button()
		{
			this.Size = new Size(200, 50);
			this.ForeColor = System.Drawing.Color.White;
			this.BackColor = Color.ButtonBlue;
			this.FlatStyle = FlatStyle.Flat;
			this.FlatAppearance.BorderSize = 0;
			this.FlatAppearance.MouseOverBackColor = Color.ButtonBlueOver;
			this.FlatAppearance.MouseDownBackColor = Color.ButtonBlueOver;

			this.Font = new Font("Consolas", 12);

			this.EnabledChanged += Button_EnabledChanged;
		}

		private void Button_EnabledChanged(object sender, EventArgs e)
		{
			//this.ForeColor = this.Enabled ? System.Drawing.Color.Red : System.Drawing.Color.White;
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			if (base.Enabled)
			{
				base.OnPaint(pe);
			}
			else
			{
				// Calling the base class OnPaint
				base.OnPaint(pe);
				// Setup the Formatting for the text
				StringFormat formatText = new StringFormat(StringFormatFlags.NoClip);
				formatText.LineAlignment = StringAlignment.Center;
				formatText.Alignment = StringAlignment.Center;
				// Drawing the button yoursel. The background is gray
				pe.Graphics.FillRectangle(new SolidBrush(System.Drawing.Color.Gray), pe.ClipRectangle);
				// Draw the line around the button
				pe.Graphics.DrawRectangle(new Pen(System.Drawing.Color.Black, 1), 0, 0, base.Width - 1, base.Height - 1);
				// Draw the text in the button in red
				pe.Graphics.DrawString(base.Text, base.Font, new SolidBrush(System.Drawing.Color.White),
					new RectangleF(0F, 0F, base.Width, base.Height), formatText);
			}
		}
	}
}
