using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Wpf_GUI_Wrapper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            string strPath = System.IO.Directory.GetCurrentDirectory();//取得当前默认路径
            //System.IO.Directory.SetCurrentDirectory(strPath);//还原默认路径
            openFileDialog1.InitialDirectory = strPath;    //打开对话框后的初始目录
            openFileDialog1.Filter = "文本文件|*.map|所有文件|*.*";
            openFileDialog1.RestoreDirectory = false;    //若为false，则打开对话框后为上次的目录。若为true，则为初始目录
            if (openFileDialog1.ShowDialog() == true)  
            {
                //textBox1.Text = File.ReadAllText(openFileDialog.FileName);
                textBox1.Text = System.IO.Path.GetFullPath(openFileDialog1.FileName);//将选中的文件的路径传递给TextBox “FilePath”
                richTextBox1.AppendText( textBox1.Text);
            }
            //create one thread to run the console and get back the result 

            //paste the result into the textbox 
        }

        private void Run_Click(object sender, RoutedEventArgs e)
        {
            RunPythonScript();
        }

        private void AppendText(string text)
        {
            Console.WriteLine(text);     //此处在控制台输出.py文件print的结果
            //richTextBox1.Text = text; 
        }



        private void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                AppendText(e.Data + Environment.NewLine);
            }
        }


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
                if (string.IsNullOrEmpty(textBox1.Text))
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

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            Button_Click( sender,  e);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            String ver = "0.1";
            string msg = $"GUI Wrapper for Python Control:{ver}";
            MessageBox.Show(msg, "About");
        }
    }
}
