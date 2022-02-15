using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace MicroHub
{
    public partial class FirstCustomControl : UserControl
    {

        public FirstCustomControl()
        {
            InitializeComponent();
            label1.Text = "Welcome Back " + Global.name;
            try
            {
            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=DatabaseInternal1.accdb");
            con.Open();
            OleDbCommand cm = new OleDbCommand("select count (user_id) from userdata", con);
            label3.Text = cm.ExecuteScalar().ToString();
            }
            catch
            {
                Console.WriteLine("Error");
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}