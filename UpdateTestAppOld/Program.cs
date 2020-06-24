using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UpdateTestAppOld
{
    static class Program
    {
        const string VERSION = "1.0.0";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                if (args[0] == "version")
                {
                    Console.WriteLine(VERSION);
                    return;
                }
            }
            catch
            {

            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
    }
}
