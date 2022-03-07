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
using System.Windows.Forms.DataVisualization;
using System.Windows.Forms.DataVisualization.Charting;

namespace MicroHub
{
    public partial class User_Analytics : UserControl
    {
        OleDbDataAdapter ds;
        private BindingSource bindingSource = null;
        private OleDbCommandBuilder oleCommandBuilder = null;
        DataTable dataTable = new DataTable();



        public User_Analytics()
        {
            InitializeComponent();
            loaddb();
            loadchart();
            
            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=DatabaseInternal1.accdb");//databaseconnection
            con.Open();
            OleDbCommand cmd = new OleDbCommand("SELECT COUNT (user_ID) FROM Users", con); //Get amount of users
            String users = cmd.ExecuteScalar().ToString();
            cmd = new OleDbCommand("SELECT COUNT (download_status) FROM Downloads WHERE download_status = 1;", con); // Get amount of users who have downloaded the application.
            String userdown = cmd.ExecuteScalar().ToString();
            cmd = new OleDbCommand("SELECT COUNT (download_status) FROM Downloads", con); //Get the amount of total downloads.
            String downloads = cmd.ExecuteScalar().ToString();
            label5.Text = userdown;
            label4.Text = users;
            label6.Text = downloads;
        }


        private void loaddb()
        {
            dataGridView1.DataSource = null;
            dataTable.Clear(); //Clears the datatable for refreshing any changes   
            try
            {
                OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=DatabaseInternal1.accdb");//databaseconnection
                con.Open();
                ds = new OleDbDataAdapter("SELECT * FROM UserGain", con); //Query to display the wanted columns in the DB
                oleCommandBuilder = new OleDbCommandBuilder(ds);
                ds.Fill(dataTable);
                bindingSource = new BindingSource { DataSource = dataTable };// Binds any changes done in the datagrid with the database
                con.Close();
                dataGridView1.RowHeadersVisible = false;
                dataGridView1.DataSource = bindingSource;
                dataGridView1.Columns[0].HeaderText = "Months";
                dataGridView1.Columns[1].HeaderText = "User Gain";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void loadchart() //Subprocedure to load linegraph.
        {
            try
            {
                //Chart settings.
                var users = chart1.Series.Add("Year 1"); 
                users.ChartType = SeriesChartType.Line;
                chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
                chart1.ChartAreas[0].AxisY.MinorGrid.Enabled = false;
                chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                chart1.ChartAreas[0].AxisX.MinorGrid.Enabled = false;

                for (int i = 0; i < dataGridView1.Rows.Count; i++) //Display data from datagrid in the chart
                {
                    double tempx = double.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());
                    double tempy = double.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString());
                    users.Points.AddXY(tempx, tempy);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            loadchart();
        }


    }
}
