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
                dataGridView1.DataSource = bindingSource;
                con.Close();
                dataGridView1.RowHeadersVisible = false;
                dataGridView1.DataSource = new List<Temperature>();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }

        private void button4_Click(object sender, EventArgs e)
        {
            loaddb();

            try
            {


                var objChart = chart1.ChartAreas[0];
                objChart.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
                //month 1-12
                objChart.AxisX.Minimum = 1;
                objChart.AxisX.Maximum = 12;
                //temperature
                objChart.AxisY.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
                objChart.AxisY.Minimum = -50;
                objChart.AxisY.Maximum = 50;
                //clear
                chart1.Series.Clear();
                //random color
                Random random = new Random();
                //loop rows to draw multi line chart c#
                foreach (Temperature t in dataGridView1.DataSource as List<Temperature>)
                {
                    chart1.Series.Add(t.Location);
                    chart1.Series[t.Location].Color = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
                    chart1.Series[t.Location].Legend = "Legend1";
                    chart1.Series[t.Location].ChartArea = "ChartArea1";
                    chart1.Series[t.Location].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                    //adding data
                    for (int i = 1; i <= 12; i++)
                        chart1.Series[t.Location].Points.AddXY(i, Convert.ToInt32(t[$"M{i}"]));
                }
            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.Message);
            }
        }
    }
}
