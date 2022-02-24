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
            dataGridView1.DataSource = new List<Dates>();
            loaddb();
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }

        private void button4_Click(object sender, EventArgs e)
        {
         

            try
            {
                var objChart = chart1.ChartAreas[0];
                objChart.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
                //month 1-12
                objChart.AxisX.Minimum = 1;
                objChart.AxisX.Maximum = 12;
                //temperature
                objChart.AxisY.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
                objChart.AxisY.Minimum = 0;
                objChart.AxisY.Maximum = 1000;
                //clear
                chart1.Series.Clear();
                //random color
                Random random = new Random();
                //loop rows to draw multi line chart c#
                foreach (Dates t in dataGridView1.DataSource as List<Dates>)
                {
                    chart1.Series.Add(t.years);
                    chart1.Series[t.years].Color = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
                    chart1.Series[t.years].Legend = "Legend1";
                    chart1.Series[t.years].ChartArea = "ChartArea1";
                    chart1.Series[t.years].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                    //adding data
                    for (int i = 1; i <= 12; i++)
                        chart1.Series[t.years].Points.AddXY(i, Convert.ToInt32(t[$"M{i}"]));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
