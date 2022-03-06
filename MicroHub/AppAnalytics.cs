using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MicroHub
{       
    public partial class AppAnalytics : UserControl
    {
        OleDbDataAdapter ds;
        private BindingSource bindingSource = null;
        private OleDbCommandBuilder oleCommandBuilder = null;
        DataTable dataTable = new DataTable();

        public AppAnalytics()
        {
            InitializeComponent();
            loaddb();
        }

        private void loaddb()
        {
            try
            {
                chart1.Series.Clear();
                OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=DatabaseInternal1.accdb");
                con.Open();

                //Query to count the amount of users who have the application downloaded. 
                OleDbCommand cmd = new OleDbCommand("SELECT COUNT (config_1) FROM user_config WHERE config_1 = YES", con);
                int config1count = int.Parse(cmd.ExecuteScalar().ToString());
                cmd = new OleDbCommand("SELECT COUNT (config_2) FROM user_config WHERE config_2 = YES", con);
                int config2count = int.Parse(cmd.ExecuteScalar().ToString());
                cmd = new OleDbCommand("SELECT COUNT (config_3) FROM user_config WHERE config_3 = YES", con);
                int config3count = int.Parse(cmd.ExecuteScalar().ToString());
                cmd = new OleDbCommand("SELECT COUNT (config_4) FROM user_config WHERE config_4 = YES", con);
                int config4count = int.Parse(cmd.ExecuteScalar().ToString());
                cmd = new OleDbCommand("SELECT COUNT (config_5) FROM user_config WHERE config_5 = YES", con);
                int config5count = int.Parse(cmd.ExecuteScalar().ToString());
                cmd = new OleDbCommand("SELECT COUNT (config_6) FROM user_config WHERE config_6 = YES", con);
                int config6count = int.Parse(cmd.ExecuteScalar().ToString());
                cmd = new OleDbCommand("SELECT COUNT (config_7) FROM user_config WHERE config_7 = YES", con);
                int config7count = int.Parse(cmd.ExecuteScalar().ToString());
                cmd = new OleDbCommand("SELECT COUNT (config_8) FROM user_config WHERE config_8 = YES", con);
                int config8count = int.Parse(cmd.ExecuteScalar().ToString());
                cmd = new OleDbCommand("SELECT COUNT (user_id) FROM Users", con);
                int amusers = int.Parse(cmd.ExecuteScalar().ToString());
                con.Close();
                int totaltrigger = config1count + config2count + config3count + config4count + config5count + config6count + config7count + config8count;
                label1.Text = "" + totaltrigger;

             

                var users = chart1.Series.Add("From " + amusers + " users");
                chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
                chart1.ChartAreas[0].AxisY.MinorGrid.Enabled = false;
                chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                chart1.ChartAreas[0].AxisX.MinorGrid.Enabled = false;

                users.Points.AddXY("Config 1", config1count);
                users.Points.AddXY("Config 2", config2count);
                users.Points.AddXY("Config 3", config3count);
                users.Points.AddXY("Config 4", config4count);
                users.Points.AddXY("Config 5", config5count);
                users.Points.AddXY("Config 6", config6count);
                users.Points.AddXY("Config 7", config7count);
                users.Points.AddXY("Config 8", config8count);


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }

        private void button4_Click(object sender, EventArgs e)
        {
            loaddb();
        }
    }
}
