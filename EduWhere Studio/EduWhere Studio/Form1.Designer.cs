namespace EduWhere_Studio
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Compile = new System.Windows.Forms.Button();
            this.richTextBox1 = new CodeEditor.NET.Editor();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.Input = new System.Windows.Forms.Label();
            this.Output = new System.Windows.Forms.Label();
            this.compileFile = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.richTextBox3 = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Compile
            // 
            this.Compile.Location = new System.Drawing.Point(176, 11);
            this.Compile.Margin = new System.Windows.Forms.Padding(2);
            this.Compile.Name = "Compile";
            this.Compile.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Compile.Size = new System.Drawing.Size(82, 25);
            this.Compile.TabIndex = 0;
            this.Compile.Text = "Compile";
            this.Compile.UseVisualStyleBackColor = true;
            this.Compile.Click += new System.EventHandler(this.button1_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.EditorBackColor = System.Drawing.SystemColors.Window;
            this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.LineNumberBackColor = System.Drawing.SystemColors.Control;
            this.richTextBox1.LineNumberForeColor = System.Drawing.Color.Teal;
            this.richTextBox1.Lines = new string[] {
        " "};
            this.richTextBox1.Location = new System.Drawing.Point(47, 71);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(2);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ShowLineNumbers = true;
            this.richTextBox1.Size = new System.Drawing.Size(400, 600);
            this.richTextBox1.TabIndex = 1;
            // 
            // richTextBox2
            // 
            this.richTextBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox2.Location = new System.Drawing.Point(476, 71);
            this.richTextBox2.Margin = new System.Windows.Forms.Padding(2);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(400, 600);
            this.richTextBox2.TabIndex = 2;
            this.richTextBox2.Text = "";
            this.richTextBox2.WordWrap = false;
            // 
            // Input
            // 
            this.Input.AutoSize = true;
            this.Input.Location = new System.Drawing.Point(44, 56);
            this.Input.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Input.Name = "Input";
            this.Input.Size = new System.Drawing.Size(31, 13);
            this.Input.TabIndex = 3;
            this.Input.Text = "Input";
            // 
            // Output
            // 
            this.Output.AutoSize = true;
            this.Output.Location = new System.Drawing.Point(473, 56);
            this.Output.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Output.Name = "Output";
            this.Output.Size = new System.Drawing.Size(86, 13);
            this.Output.TabIndex = 4;
            this.Output.Text = "Output Assembly";
            // 
            // compileFile
            // 
            this.compileFile.Location = new System.Drawing.Point(47, 11);
            this.compileFile.Margin = new System.Windows.Forms.Padding(2);
            this.compileFile.Name = "compileFile";
            this.compileFile.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.compileFile.Size = new System.Drawing.Size(125, 25);
            this.compileFile.TabIndex = 5;
            this.compileFile.Text = "Compile from file";
            this.compileFile.UseVisualStyleBackColor = true;
            this.compileFile.Click += new System.EventHandler(this.compileFile_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // richTextBox3
            // 
            this.richTextBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox3.Location = new System.Drawing.Point(901, 71);
            this.richTextBox3.Margin = new System.Windows.Forms.Padding(2);
            this.richTextBox3.Name = "richTextBox3";
            this.richTextBox3.Size = new System.Drawing.Size(400, 600);
            this.richTextBox3.TabIndex = 6;
            this.richTextBox3.Text = "";
            this.richTextBox3.WordWrap = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(898, 56);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Symbol Table";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 609);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBox3);
            this.Controls.Add(this.compileFile);
            this.Controls.Add(this.Output);
            this.Controls.Add(this.Input);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.Compile);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EduWhere Studio";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Compile;
        private CodeEditor.NET.Editor richTextBox1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.Label Input;
        private System.Windows.Forms.Label Output;
        private System.Windows.Forms.Button compileFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.RichTextBox richTextBox3;
        private System.Windows.Forms.Label label1;
    }
}

