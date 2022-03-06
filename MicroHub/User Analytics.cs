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
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {

                    //chartBpComplaince.Series.Clear();
                    Series S = chart1.Series.Add(row.Cells[2].Value.ToString());

                    S.Points.AddXY(row.Cells[4].Value.ToString(), row.Cells[3].Value.ToString());
                    S.ChartType = SeriesChartType.Column;
                    S.IsValueShownAsLabel = true;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
