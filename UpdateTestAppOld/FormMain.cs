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

namespace UpdateTestAppOld
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            string source = @"C:\Users\lwnwim8\Documents\GitHub\makeupdate\UpdateTestAppNew\bin\Debug";
            string destination = @"C:\Users\lwnwim8\Documents\GitHub\makeupdate\UpdateTestAppOld\bin\Debug";
            string program = "UpdateTestApp.exe";

            string arguments;

            if (checkBoxStartAfterUpdate.Checked)
                arguments = $"appsettings.file.json -source \"{source}\" -destination \"{destination}\" -program \"{program}\" -nozip -start";
            else
                arguments = $"appsettings.file.json -source \"{source}\" -destination \"{destination}\" -program \"{program}\" -nozip";

            using (Process proc = new Process())
            {
                proc.StartInfo = new ProcessStartInfo
                {
                    FileName = @"C:\Users\lwnwim8\Documents\GitHub\makeupdate\MakeUpdate\bin\Debug\netcoreapp3.1\MakeUpdate.exe",
                    Arguments = arguments,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                };

                proc.Start();
            }
        }
    }
}
