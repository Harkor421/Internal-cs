using MicroHub;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

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

            SetProcessDPIAware();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
            string conexions = "Server = 173.201.179.107; database = MicroHub; UID =salgui; password =salgui123";
            MySqlConnection conexion = new MySqlConnection(conexions);
            conexion.Open();

            string user = Properties.Settings.Default.username;
            string pass = Properties.Settings.Default.password;
    
        
                Console.WriteLine("EJECUTANDO DATOS DE INICIO DE SESIÓN");
                Application.Run(new Login_and_Register());

            }
            catch
            {
                Application.Run(new Form4());
            }

        }
        
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();


    }
}
