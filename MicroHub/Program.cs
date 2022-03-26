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

        [STAThread]
        static void Main()
        {

            //Tries connection with database before the application loads
            try
            {

                OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=DatabaseInternal1.accdb");

                Console.WriteLine("Conected to Database");

                string user = Properties.Settings.Default.username;
                string pass = Properties.Settings.Default.password;
    
        
                Console.WriteLine("EXECUTING LOG IN DATA");
                Application.Run(new Login_and_Register()); //Opens Login form

            }
            catch
            {
                Application.Run(new Form4()); //if unsuccessfull it will display the following forms 
            }

        }


    }
}
