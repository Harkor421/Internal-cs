using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace MicroHub
{
    public partial class CuentaGUI : UserControl
    {
        public CuentaGUI()
        {
            InitializeComponent();
            if (Global.username == null)
            {
                Global.username = Properties.Settings.Default.username;
            }
            label5.Text = Global.username;
            string conexions = "Server = 173.201.179.107; database = i7679659_wp1; UID =salgui; password =salgui123";
            MySqlConnection conexion = new MySqlConnection(conexions);
            conexion.Open();
            MySqlCommand comm = conexion.CreateCommand();
            comm.CommandText = "select user_login from wp_users where user_email = '" + Global.username + "'";
            label7.Text = (string)comm.ExecuteScalar();
            Console.WriteLine(Properties.Settings.Default.netshconfig);
        }

        private void rjButton1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.microhubco.com/mi-cuenta/edit-account/");



        }
    }
}
