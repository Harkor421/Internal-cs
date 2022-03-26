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
            label1.Text = "Welcome Back " + Global.name; // Displays name for the logged user.
            try
            {
            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=DatabaseInternal1.accdb");
            con.Open();
            OleDbCommand cm = new OleDbCommand("select count (user_ID) from Users", con); 
            label3.Text = cm.ExecuteScalar().ToString();//Outputs the amount of users

            cm = new OleDbCommand("SELECT COUNT (subscription_status) FROM Subscriptions WHERE subscription_status = YES", con);
            String prem = cm.ExecuteScalar().ToString();
            label4.Text = prem;
            double monthly = double.Parse(prem) * 9.99;
            label6.Text = "" + monthly + " dollars"; //Outputs monthly income.




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