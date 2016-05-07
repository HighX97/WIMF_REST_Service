using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WIMF_ClassLibrary;

namespace WIMF_server_app_form
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
