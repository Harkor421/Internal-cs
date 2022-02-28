using MicroHub;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using System.Data.OleDb;

namespace MicroHub
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {


            try
            {

                OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=DatabaseInternal1.accdb");



                Console.WriteLine("Conected to Database");

                string user = Properties.Settings.Default.username;
                string pass = Properties.Settings.Default.password;
    
        
                Console.WriteLine("EXECUTING LOG IN DATA");
                Application.Run(new Login_and_Register());

            }
            catch
            {
                Application.Run(new Form4());
            }

        }


    }
}
