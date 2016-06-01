using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EduWhere_Studio
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox2.SelectAll();
            richTextBox2.SelectionColor = Color.Black;   
            string[] input=richTextBox1.Lines;
            Process p = new Process();
            p.StartInfo.FileName = "EduWhere.exe";
            //p.StartInfo.WorkingDirectory = Path.GetDirectoryName(@"C:\Users\Yans\Desktop");
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            //p.StartInfo.Arguments = "< input.txt";
            p.Start(); // start the process (the python program)
            StreamWriter myStreamWriter = p.StandardInput;
            foreach(string line in input)
                myStreamWriter.WriteLine(line);
            myStreamWriter.Close();

            string output = p.StandardOutput.ReadToEnd();
            // Wait for the sort process to write the sorted text lines.
            p.WaitForExit();
            p.Close();
            richTextBox2.Text = output;
            for (int i = 0; i < richTextBox2.Lines.Count(); i++)
                highlightLineContaining(richTextBox2, i, "error", Color.Red);

            string symURL = "symbolTable.txt";
            StreamReader reader = new StreamReader(symURL);
            string symTable = reader.ReadToEnd();
            reader.Close();
            richTextBox3.Text = symTable;
        }

        void highlightLineContaining(RichTextBox rtb, int line, string search, Color color)
        {
            int c0 = rtb.GetFirstCharIndexFromLine(line);
            int c1 = rtb.GetFirstCharIndexFromLine(line + 1);
            if (c1 < 0) c1 = rtb.Text.Length;
            rtb.SelectionStart = c0;
            rtb.SelectionLength = c1 - c0;
            if (rtb.SelectedText.Contains(search))
                rtb.SelectionColor = color;
            rtb.SelectionLength = 0;
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            string fileURL = System.IO.Path.GetDirectoryName(openFileDialog1.FileName) + "/" + System.IO.Path.GetFileName(openFileDialog1.FileName);
            StreamReader reader = new StreamReader(fileURL);
            string input = reader.ReadToEnd();
            reader.Close();
            richTextBox1.setText(input);
            button1_Click(sender,e);
        }

        private void compileFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }


    }
}
