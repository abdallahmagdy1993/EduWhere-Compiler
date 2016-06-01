using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace CodeEditor.NET
{
	/// <summary>
	/// Summary description for EditorText.
	/// </summary>
	internal class EditorText : System.Windows.Forms.RichTextBox
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		
		private bool isAPaint = false;

		
		//create event for linedrawing
		public  delegate void DrawLineNumberDelegate();
		public event DrawLineNumberDelegate DrawLines;

		
		
		//constants
		public const int WM_KEYUP = 0x101;
		public const int WM_PAINT = 0xF;
		public const int WM_SIZE = 0x5;
		public const int WM_SETFOCUS = 0x7;
		public const int SB_THUMBPOSITION = 0x04;
		public const int SB_THUMBTRACK = 0x05;
		public const int WM_VSCROLL = 0x115;
		public const int SB_LINEUP = 0x00;
		public const int SB_LINEDOWN = 0x01;
		public const int WM_THUMBMOVE = 277;
		public const int WM_SETFONT = 0x30;
		public const int WM_ACTIVATE = 0x6;
		public const int WM_USERO = 0x2111;
		public const int WM_KILLFOCUS = 0x08;
		public const int WM_ERASEBKGND = 0x14;



		
		public EditorText()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call

		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		

		[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name="FullTrust")]
		protected override void WndProc(ref Message m) 
		{
			// Listen for operating system messages.

			//catch message for thumb movements
			int request = m.WParam.ToInt32() & 0xFFFF; //loword

			switch (m.Msg)
			{
				case WM_USERO:
					return;

				case WM_SETFOCUS:
					isAPaint = true;
					break;

				case WM_VSCROLL:

					if(request == SB_THUMBTRACK || request == SB_THUMBPOSITION)
					{
						if(DrawLines != null)
							DrawLines();
					}
					break;
				
				case WM_SIZE:
					//draw lines
					isAPaint = true;
					break;
				case WM_PAINT:
					if(isAPaint)
					{
						if(DrawLines != null)
							DrawLines();

						isAPaint = false;
						
					}
					break;
			}
			base.WndProc(ref m);
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// EditorText
			// 
			this.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
			this.ShowSelectionMargin = true;
			this.TextChanged += new System.EventHandler(this.EditorText_TextChanged);
			this.MouseEnter += new System.EventHandler(this.EditorText_MouseEnter);
			this.VScroll += new System.EventHandler(this.EditorText_VScroll);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.EditorText_KeyUp);

		}
		#endregion

	

		private void EditorText_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{

			if(e.KeyData == Keys.Up || e.KeyData == Keys.Down || e.KeyData == Keys.Return || e.KeyData == Keys.Enter || e.KeyData == Keys.Delete || e.KeyData == Keys.Back)
			{
				if(DrawLines != null)
					DrawLines();
			}

			 
		}
        

		

		private void EditorText_TextChanged(object sender, System.EventArgs e)
		{
			if(this.Text.Length == 1)
			{
				if(DrawLines != null)
					DrawLines();
				
			}
		}

	

		private void EditorText_VScroll(object sender, System.EventArgs e)
		{
			if(DrawLines != null)
				DrawLines();
		}

		

		private void EditorText_MouseEnter(object sender, System.EventArgs e)
		{
			if(DrawLines != null)
				DrawLines();
		}

	

		
	}
}
