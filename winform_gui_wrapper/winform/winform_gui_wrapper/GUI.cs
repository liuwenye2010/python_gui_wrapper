using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace winform_gui_wrapper
{
    public partial class GUI : Form
    {
        public GUI()
        {
            InitializeComponent();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String ver = "0.1";
            string msg = $"GUI Wrapper for Python Control:{ver}";
            MessageBox.Show(msg, "About");

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Browse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            string strPath = System.IO.Directory.GetCurrentDirectory();//取得当前默认路径
            //System.IO.Directory.SetCurrentDirectory(strPath);//还原默认路径
            openFileDialog1.InitialDirectory = strPath;    //打开对话框后的初始目录
            openFileDialog1.Filter = "文本文件|*.map|所有文件|*.*";
            openFileDialog1.RestoreDirectory = false;    //若为false，则打开对话框后为上次的目录。若为true，则为初始目录
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = System.IO.Path.GetFullPath(openFileDialog1.FileName);//将选中的文件的路径传递给TextBox “FilePath”
                richTextBox1.Text = textBox1.Text;
            }
            //create one thread to run the console and get back the result 

            //paste the result into the textbox 
        }



        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Run_Click(object sender, EventArgs e)
        {
            RunPythonScript();
        }

        private void AppendText(string text)
        {
            Console.WriteLine(text);     //此处在控制台输出.py文件print的结果
            //richTextBox1.Text = text; 
        }


        //输出打印的信息
        private void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                AppendText(e.Data + Environment.NewLine);
            }
        }

        //调用python核心代码
        private void RunPythonScript()
        {
            try
            {
                Process p = new Process();
                //string path = System.IO.Directory.GetCurrentDirectory(); // System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                string strPath = System.IO.Directory.GetCurrentDirectory();
                p.StartInfo.FileName = strPath + "\\check_mcu_map.exe";
                string sArguments = "-i";
                /*
                           foreach (string sigstr in teps)
                           {
                               sArguments += " " + sigstr;//传递参数
                           }

                           sArguments += " " + args;
               */
                sArguments += " ";
                if(string.IsNullOrEmpty(textBox1.Text))
                    throw new System.InvalidOperationException("Input map file cannot be empty");
                sArguments += textBox1.Text;
                p.StartInfo.Arguments = sArguments;

                p.StartInfo.UseShellExecute = false;

                p.StartInfo.RedirectStandardOutput = true;

                p.StartInfo.RedirectStandardInput = true;

                p.StartInfo.RedirectStandardError = true;

                p.StartInfo.CreateNoWindow = true;

                p.Start();
                p.BeginOutputReadLine();
                p.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);
                // Console.ReadLine();
                p.WaitForExit();
                MessageBox.Show("DONE!", "INFO");
            }
            catch (Exception e)
            {
                string msg = $"There are something wrong:{e.Message}";
                MessageBox.Show(msg, "ERROR");
            }
            
            
        }


    }
}
