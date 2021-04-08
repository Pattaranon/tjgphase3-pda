using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using SPACE.TJG.Barcode.UI.Main;

namespace SPACE.TJG.Barcode.UI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            Application.Run(new frmMainMenu("LIFETIME"));
        }
    }
}