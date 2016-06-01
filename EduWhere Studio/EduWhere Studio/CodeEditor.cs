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
	/// The Source Code Editor for CodeEditor.NET
	/// </summary>
	/// 
	[Description("Code Editor")]
	public class Editor : System.Windows.Forms.UserControl
	{
		private System.ComponentModel.IContainer components;

		

		private const int WM_VSCROLL = 0x115;
		private const int SB_LINEUP = 0x00;
		private const int SB_LINEDOWN = 0x01;
		private const int EM_LINEINDEX =0xBB;

		private const  int EM_SETMARGINS = 0xD3;
		private const int EC_LEFTMARGIN = 0x0001 ;
		private CodeEditor.NET.LineNumbers rtbLineNumbers;
		private CodeEditor.NET.EditorText rtbScript;
		private const int EM_GETFIRSTVISIBLELINE =  0x00CE;
		
		//defines that the line numbers are shown
		private bool bShowlineNumbers = true;

		[DllImport ("user32.dll")]
		public static extern int SendMessage(IntPtr hWnd, int Msg, int
			wParam, int lParam);
		

		/// <summary>
		/// Initializes the Component
		/// </summary>
		public Editor()
		{
			
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			this.ResizeRedraw = true;
			
			
			//messages to draw lines from EditorText
			rtbScript.DrawLines += new CodeEditor.NET.EditorText.DrawLineNumberDelegate(rtbScript_DrawLines);
			//tatty but subscribe to the Line Number draw line event also
			rtbLineNumbers.DrawLines +=new CodeEditor.NET.LineNumbers.DrawLineNumberDelegate(rtbScript_DrawLines);

						
		}
        public void setText(string s) { 
        rtbScript.Text=s;
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
			this.components = new System.ComponentModel.Container();
			this.rtbLineNumbers = new CodeEditor.NET.LineNumbers(this.components);
			this.rtbScript = new CodeEditor.NET.EditorText();
			this.SuspendLayout();
			// 
			// rtbLineNumbers
			// 
			this.rtbLineNumbers.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.rtbLineNumbers.DetectUrls = false;
			this.rtbLineNumbers.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.rtbLineNumbers.ForeColor = System.Drawing.Color.Teal;
			this.rtbLineNumbers.Location = new System.Drawing.Point(0, 0);
			this.rtbLineNumbers.Name = "rtbLineNumbers";
			this.rtbLineNumbers.ReadOnly = true;
			this.rtbLineNumbers.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
			this.rtbLineNumbers.Size = new System.Drawing.Size(40, 280);
			this.rtbLineNumbers.TabIndex = 1;
			this.rtbLineNumbers.TabStop = false;
			this.rtbLineNumbers.Text = "";
			this.rtbLineNumbers.WordWrap = false;
			this.rtbLineNumbers.BackColorChanged += new System.EventHandler(this.rtbLineNumbers_BackColorChanged);
			this.rtbLineNumbers.ForeColorChanged += new System.EventHandler(this.rtbLineNumbers_ForeColorChanged);
			this.rtbLineNumbers.MouseMove += new System.Windows.Forms.MouseEventHandler(this.rtbLineNumbers_MouseMove);
			// 
			// rtbScript
			// 
			this.rtbScript.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.rtbScript.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.rtbScript.ForeColor = System.Drawing.Color.Black;
			this.rtbScript.Location = new System.Drawing.Point(32, 0);
			this.rtbScript.Name = "rtbScript";
			this.rtbScript.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
			this.rtbScript.ShowSelectionMargin = true;
			this.rtbScript.Size = new System.Drawing.Size(328, 280);
			this.rtbScript.TabIndex = 0;
			this.rtbScript.Text = " ";
			this.rtbScript.WordWrap = false;
			this.rtbScript.MouseMove += new System.Windows.Forms.MouseEventHandler(this.rtbLineNumbers_MouseMove);
			// 
			// Editor
			// 
			this.Controls.Add(this.rtbLineNumbers);
			this.Controls.Add(this.rtbScript);
			this.Name = "Editor";
			this.Size = new System.Drawing.Size(360, 280);
			this.SizeChanged += new System.EventHandler(this.CodeEditor_SizeChanged);
			this.ResumeLayout(false);

		}
		#endregion


		/// <summary>
		/// Sets or Gets the visible attribute of the Line Numbers
		/// </summary>
		[EditorBrowsable(EditorBrowsableState.Always),Description("Sets or Gets the visible attribute of the Line Numbers"),Category("Editor Specific")]
		public bool ShowLineNumbers
		{
			get
			{
				return bShowlineNumbers;
			}

			set
			{
				bShowlineNumbers = value;
				this.RedrawObjects();
			}
		}

		
		/// <summary>
		/// Returns the lines in the Editor as a string[]
		/// </summary>
		[EditorBrowsable(EditorBrowsableState.Always),Description("Sets or Gets the Lines in the Editor"),Category("Editor Specific")]
		public string[] Lines
		{
			get
			{
				return rtbScript.Lines;
			}

			set
			{
				rtbScript.Lines = value;
				rtbScript.Refresh();
			}
		}


		
		/// <summary>
		/// Sets the Font of the editor
		/// </summary>
		[Browsable(true),Description("Sets or Gets the Code Editors Font"),Category("Editor Specific"), DefaultValue(typeof(Font),"Courier New, 10")]
		public override Font Font
		{
			set
			{
				
				rtbScript.Font = value;
				//rtbScript.SelectionFont = value;

				//now select all text and set font
				RedrawObjects();

			}

			get
			{
					return rtbScript.Font;
			}
		}

	

		/// <summary>
		/// Sets the Font Color of Script Editor
		/// </summary>
		/// 
		[Description("Sets or Gets the ForeColor of the Editor area"),DefaultValue(typeof(Color),"Black"),Category("Editor Specific")]
		public Color SelectionColor
		{
			get
			{
				return rtbScript.SelectionColor;
			}
			set
			{
				rtbScript.SelectionColor = value;
			}
		}

		/// <summary>
		/// Sets the Line Number Color
		/// </summary>
		[Description("Sets or Gets the ForeColor of the Line Numbers"),Category("Editor Specific")]
		public Color LineNumberForeColor
		{
			get
			{
				return rtbLineNumbers.ForeColor; //clrLineNumber;;
			}
			set
			{
				rtbLineNumbers.ForeColor = value;
				
				
			}
		}

		/// <summary>
		/// Sets the editor backcolor
		/// </summary>
		/// 
		[Description("Sets or Gets the Editor area BackColor"),Category("Editor Specific")]
		public Color EditorBackColor
		{
			get
			{
				return rtbScript.BackColor;
			}

			set
			{
				rtbScript.BackColor = value;

			}
		}

		/// <summary>
		/// Sets the Line Number backcolor
		/// </summary>
		/// 
		[Description("Sets or Gets the Line Number area BackColor"),Category("Editor Specific")]
		public Color LineNumberBackColor
		{
			get
			{
				return rtbLineNumbers.BackColor;
			}

			set
			{
				rtbLineNumbers.BackColor = value;
				

			}
		}


		
		/// <summary>
		/// Sets the line numbers that show in the second rtbox
		/// </summary>
		private void rtbScript_DrawLines()
		{
			//only draw numbers if client wishes numbers
			if(bShowlineNumbers)
			{
				//now draw the line numbers
				Graphics g = rtbLineNumbers.CreateGraphics();
				SizeF sizF  = g.MeasureString("8888",new Font(rtbScript.Font.FontFamily,rtbScript.Font.Size));
			
				int x = 2;
				g.Clear(this.LineNumberBackColor);
				//get top line as we don't want to draw too many line numbers
				int intTopLine = SendMessage(rtbScript.Handle,EM_GETFIRSTVISIBLELINE,0,0);
				int intLinesToDraw = (int)(rtbScript.Height/sizF.Height) + intTopLine + 5;
				Color clrFore = rtbLineNumbers.ForeColor;

				//create stringformat so we can put text right aligned
				StringFormat sf = new StringFormat();
				sf.FormatFlags = StringFormatFlags.DirectionRightToLeft;

				//TODO: Take the highest hieght in the line to draw the number of and use that font size for setting
				for(int i = intTopLine;i < intLinesToDraw;i++)
				{
					int intLine = i + 1;

					if(i < rtbScript.Lines.Length)
					{
						//get char index of each line
						int intLineCharIndex = SendMessage(rtbScript.Handle,EM_LINEINDEX,i,0);
						//TODO: use the highest font in the line to set the hight of the line etc..
						//WORRY: How to make a word wrap ?
						//get position where to print line number
						Point pntLinePos = rtbScript.GetPositionFromCharIndex(intLineCharIndex);

						RectangleF rectF = new RectangleF(x,pntLinePos.Y,sizF.Width,sizF.Height);
						
						g.DrawString(intLine.ToString(),new Font(rtbScript.Font.FontFamily,rtbScript.Font.Size), new SolidBrush(clrFore),rectF,sf);
						
					}

				
					else
						break;
				}

				g.Dispose();
			}
			
		}



		private void CodeEditor_SizeChanged(object sender, System.EventArgs e)
		{
			RedrawObjects();	

		}

		
		

		/// <summary>
		/// Draws out the 2 RichTextBox's in correct posisitons
		/// </summary>
		private void RedrawObjects()
		{
			
			if(bShowlineNumbers)
			{
				rtbLineNumbers.Visible = true;
			
				//set font width at most point
				int intFontWidth = (int)rtbScript.CreateGraphics().MeasureString("8888",new Font(rtbScript.Font.FontFamily,rtbScript.Font.Size)).Width + 5;
			
				//set height in comparison to the font height
				//float intThisHeight = 20 * rtbScript.Font.Height;
				rtbLineNumbers.Location = new Point(0,0);
				rtbLineNumbers.Size = new Size(intFontWidth ,this.Height - SystemInformation.HorizontalScrollBarHeight);
		

				//now set the rtbScript
				rtbScript.Location = new Point(intFontWidth - 3,0);
				rtbScript.Size = new Size(this.Width-(intFontWidth),this.Height);
			}
			else //no numbers to show...
			{
				rtbLineNumbers.Visible = false;
				//now set the rtbScript
				rtbScript.Location = new Point(0,0);
				rtbScript.Size = new Size(this.Width,this.Height);
			}
			
		}

		private void rtbLineNumbers_BackColorChanged(object sender, System.EventArgs e)
		{
			this.rtbScript_DrawLines();
		
		}

		private void rtbLineNumbers_ForeColorChanged(object sender, System.EventArgs e)
		{
			this.rtbScript_DrawLines();
		}

		private void rtbLineNumbers_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			
		}

		

	
	}
}
