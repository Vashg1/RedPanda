using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Collections;
using System.Threading;
using System.Text.RegularExpressions;


namespace HappyCustomer
{
    static class Program
    {
        public static System.Timers.Timer _timer = new System.Timers.Timer();
        public static Hashtable hashtable = new Hashtable();
        public static int happiestDay = 0;
        public static Queue CustomerQue = new Queue();
        public static string[] lines = System.IO.File.ReadAllLines(@"C:\StoreSimulation.csv");
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

    }

    


}
