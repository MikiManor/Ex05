using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace A17_Ex05_MatanMaron_021516083_MikiManor_310962212
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            OtheloUI othello = new OtheloUI();
            othello.RunGame();
        }
    }
}