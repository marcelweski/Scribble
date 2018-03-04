using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinAdvancedNetworkLibSample
{
	public class ChatControl : RichTextBox
	{
		private System.Drawing.Font infoFont;

		public ChatControl()
		{
			this.BorderStyle = BorderStyle.None;
			this.Font = new System.Drawing.Font("Segoe UI Emoji", 10.0f);
			this.ReadOnly = true;
			this.BackColor = System.Drawing.Color.White;
			this.ScrollBars = RichTextBoxScrollBars.None;

			this.infoFont = new System.Drawing.Font("Segoe UI Emoji", 12.0f, System.Drawing.FontStyle.Bold);
		}

		public void AddMessage(string sender, string text)
		{
			this.SelectionStart = this.TextLength;
			this.SelectionLength = 0;

			this.SelectionColor = System.Drawing.Color.Green;
			this.AppendText($"{sender}> ");
			this.SelectionColor = System.Drawing.Color.Black;
			this.AppendText(text + "\n");

			this.ScrollToCaret();
		}

		public void AddInfo(string text)
		{
			this.SelectionStart = this.TextLength;
			this.SelectionLength = 0;

			this.SelectionColor = System.Drawing.Color.Blue;
			this.SelectionFont = this.infoFont;
			this.AppendText(text + "\n");
			this.SelectionFont = this.Font;

			this.ScrollToCaret();
		}
	}
}
