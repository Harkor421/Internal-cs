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
    public partial class Estimated_Income : UserControl
    {

        OleDbDataAdapter ds;
        private BindingSource bindingSource = null;
        private OleDbCommandBuilder oleCommandBuilder = null;
        DataTable dataTable = new DataTable();
        public Estimated_Income()
        {
            InitializeComponent();
            try
            {
                OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=DatabaseInternal1.accdb");//databaseconnection
                con.Open();
                //Query to count the amount of users who have the application downloaded. 
                OleDbCommand cmd = new OleDbCommand("SELECT COUNT (download_status) FROM Downloads WHERE download_status = 1", con);
                String userdown = cmd.ExecuteScalar().ToString();


                //Loads database.
                String f = "";
                loaddb(f);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void loaddb(String search)
        {
            dataGridView1.DataSource = null;
            dataTable.Clear(); //Clears the datatable for refreshing any changes

            try
            {
                OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=DatabaseInternal1.accdb");//databaseconnection
                con.Open();
                ds = new OleDbDataAdapter("SELECT sub_ID, subscription_status, subscription_count FROM Subscriptions WHERE sub_ID LIKE '%%' AND (subscription_status LIKE  '%" + search + "%' OR subscription_count LIKE '%" + search +  "%');", con); //Query to display the wanted columns in the DB
                oleCommandBuilder = new OleDbCommandBuilder(ds);
                ds.Fill(dataTable);
                bindingSource = new BindingSource { DataSource = dataTable };// Binds any changes done in the datagrid with the database
                dataGridView1.DataSource = bindingSource;
                con.Close();
                dataGridView1.RowHeadersVisible = false;
                dataGridView1.Columns[0].HeaderText = "ID"; //Setting the names for the columns in the DataGrid
                dataGridView1.Columns[1].HeaderText = "Subscription Status";
                dataGridView1.Columns[2].HeaderText = "Subscription Count";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {

            String search = textBox1.Text;
            loaddb(search);



        }

        private void button4_Click(object sender, EventArgs e)
        {
            String search = textBox1.Text;
            dataGridView1.EndEdit();
            ds.Update(dataTable); //Updates the information in the Database
            MessageBox.Show("Changes have been saved");
            loaddb(search);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            {

                try
                {
                    DialogResult dialogResult = MessageBox.Show("You are about to delete a user permanently from the Database, are you sure?", "WARNING", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {

                        //Gets the index from the selected row in the datagrid.
                        int selectedIndex = dataGridView1.CurrentCell.RowIndex;

                        //Turns it into a UserID, so the information can be deleted in the database.
                        int rowID = Convert.ToInt32(dataGridView1.CurrentCell.RowIndex);
                        try
                        {

                            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=DatabaseInternal1.accdb");
                            con.Open();
                            OleDbCommand cmd = new OleDbCommand("DELETE FROM Users WHERE user_ID=" + rowID + "", con);
                            cmd.ExecuteNonQuery();
                            Console.WriteLine("Deletion attempted");
                            con.Close();
                            MessageBox.Show("deleted");

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        //Deletion of the row in the dataGridView
                        dataGridView1.Rows.RemoveAt(selectedIndex);

                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        //do something else
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }
}
