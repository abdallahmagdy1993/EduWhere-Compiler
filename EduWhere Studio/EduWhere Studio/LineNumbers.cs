using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;

namespace CodeEditor.NET
{
	/// <summary>
	/// Summary description for LineNumbers.
	/// </summary>
	internal class LineNumbers : System.Windows.Forms.RichTextBox
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		//create event for linedrawing
		public  delegate void DrawLineNumberDelegate();
		public event DrawLineNumberDelegate DrawLines;


		private bool isAPaint = false;


		public LineNumbers(System.ComponentModel.IContainer container)
		{
			///
			/// Required for Windows.Forms Class Composition Designer support
			///
			container.Add(this);
			InitializeComponent();

		}

	
		
		[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name="FullTrust")]
		protected override void WndProc(ref System.Windows.Forms.Message m)
		{
			
			System.Diagnostics.Debug.WriteLine(m.Msg.ToString());
			
									/*160
						1082
						1092
						8270
						8465
						15
						20
						8465
						*/
				switch(m.Msg)
				{
					case EditorText.WM_USERO:
						
						
						return;
					
					case EditorText.WM_KILLFOCUS:
						if(DrawLines != null)
							DrawLines();
						break;

					case EditorText.WM_PAINT:
						if(isAPaint)
						{
							if(DrawLines != null)
								DrawLines();
							isAPaint = false;
						}
						break;
					case EditorText.WM_SIZE:
						isAPaint = true;
						break;


			
						
				}

				base.WndProc (ref m);
			
		}


		public LineNumbers()
		{
			///
			/// Required for Windows.Forms Class Composition Designer support
			///
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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


		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// LineNumbers
			// 
			this.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.DetectUrls = false;
			this.ForeColor = System.Drawing.Color.Teal;
			this.ReadOnly = true;
			this.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
			this.TabStop = false;
			this.WordWrap = false;
			this.ForeColorChanged += new System.EventHandler(this.LineNumbers_ForeColorChanged);
			this.BackColorChanged += new System.EventHandler(this.LineNumbers_BackColorChanged);
			this.MouseEnter += new System.EventHandler(this.LineNumbers_MouseEnter);

		}
		#endregion

		private void LineNumbers_MouseEnter(object sender, System.EventArgs e)
		{
			if(DrawLines != null)
				DrawLines();
		}

		private void LineNumbers_ForeColorChanged(object sender, System.EventArgs e)
		{
			if(DrawLines != null)
				DrawLines();
		}

		private void LineNumbers_BackColorChanged(object sender, System.EventArgs e)
		{
			if(DrawLines != null)
				DrawLines();
		}
	}
}
