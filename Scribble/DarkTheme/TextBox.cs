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
	class TextBox : UserControl
	{
		public System.Windows.Forms.TextBox textBox;

		[Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		[Description("Test text displayed in the textbox"), Category("Data")]
		public override string Text { get => this.textBox.Text; set => this.textBox.Text = value; }

		public bool ReadOnly { get => this.textBox.ReadOnly; set => this.textBox.ReadOnly = value; }

		public TextBox()
		{
			//this.BackColor = Color.FromArgb(240, 240, 240);
			////this.BorderStyle = BorderStyle.FixedSingle;
			//this.Font = new Font("Consolas", 12.0f);
			//this.ForeColor = Color.Black;
			//this.Location = new Point(608, 216);
			//this.Size = new Size(198, 50);
			//this.BorderStyle = BorderStyle.FixedSingle;

			this.textBox = new System.Windows.Forms.TextBox();
			this.textBox.Left = 10;
			this.textBox.Top = 5;
			this.textBox.BorderStyle = BorderStyle.None;
			this.textBox.Font = new Font("Consolas", 12.0f);
			this.Controls.Add(this.textBox);

			this.Width = 200;
			this.Height = 29;
			this.BackColor = System.Drawing.Color.White;

			this.EnabledChanged += TextBox_EnabledChanged;

			this.textBox.ReadOnlyChanged += TextBox_ReadOnlyChanged;

			this.textBox.KeyDown += (s, e) => this.OnKeyDown(e);
		}

		private void TextBox_EnabledChanged(object sender, EventArgs e)
		{
			this.BackColor = this.Enabled ? System.Drawing.Color.White : System.Drawing.Color.FromArgb(240, 240, 240);
		}

		private void TextBox_ReadOnlyChanged(object sender, EventArgs e)
		{
			if (this.textBox.ReadOnly)
			{
				this.BackColor = System.Drawing.Color.LightGray;
				this.textBox.BackColor = System.Drawing.Color.LightGray;
			}
			else
			{
				this.BackColor = System.Drawing.Color.White;
				this.textBox.BackColor = System.Drawing.Color.White;
			}
		}

		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);

			this.textBox.Width = this.Width - (this.textBox.Left*2);
		}
	}
}
