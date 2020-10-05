using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SequenceRename
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var dir = folderBrowserDialog1.ShowDialog();
            tbSourceDir.Text = folderBrowserDialog1.SelectedPath;
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            var inputPath = folderBrowserDialog1.SelectedPath;
            var outputPath = folderBrowserDialog1.SelectedPath + @"\Renamed";
            
            // Create output dir
            Directory.CreateDirectory(outputPath);

            // Input dir and files
            DirectoryInfo dir = new DirectoryInfo(inputPath);

            // Get files, oldest to newest
            FileInfo[] files = dir.GetFiles("*.jpg").OrderBy(f => f.CreationTimeUtc).ToArray();

            int count = 1;
            string newFileName = string.Empty;

            foreach (var file in files)
            {                    
                if (count < 10)
                {
                    newFileName = "000" + count;
                }
                else if (count >= 10 && count < 100)
                {
                    newFileName = "00" + count;
                }
                else if (count >= 100 && count < 1000)
                {
                    newFileName = "0" + count;
                }
                else
                {
                    newFileName = count.ToString();
                }

                file.CopyTo(outputPath + @"\" + newFileName + ".jpg");

                count++;
            }
        }
    }
}
