using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GS.module.processICarga
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            //Application.Run(new Form1(args[0]));
            Application.Run(new Form1("dZGNmjmb7QDmoXC4p2gygn5N6JwfKq2GolTj/POxncvqy+9clT7f4A=="));
        }
    }
}
