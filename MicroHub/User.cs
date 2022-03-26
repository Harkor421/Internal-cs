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
    public partial class User : UserControl
    {
        OleDbDataAdapter ds;
        private BindingSource bindingSource = null;
        private OleDbCommandBuilder oleCommandBuilder = null;
        DataTable dataTable = new DataTable();
        public User()
        {
            InitializeComponent();
            try
            {
            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=DatabaseInternal1.accdb");//databaseconnection
            con.Open();
            //Query to count the amount of users who have the application downloaded. 
            OleDbCommand cmd = new OleDbCommand("SELECT COUNT (download_status) FROM Downloads WHERE download_status = 1", con);
            String userdown = cmd.ExecuteScalar().ToString();
            
            //Query to count the amount of users.
            OleDbCommand cm = new OleDbCommand("select count (user_ID) from Users", con);
            label4.Text = cm.ExecuteScalar().ToString();
            label5.Text = userdown;

            //Loads database.
            String f = "";
            loaddb(f);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //Subprocedure that belongs to the display of the database in the datagrid and bind any changes to the database.
        private void loaddb(String search)
        {
            dataGridView1.DataSource = null;
            dataTable.Clear(); //Clears the datatable for refreshing any changes
           
            try
            {
                OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=DatabaseInternal1.accdb");//databaseconnection
                con.Open();
                ds = new OleDbDataAdapter("SELECT user_ID, user_name, user_pass, user_email, username FROM Users WHERE User_ID LIKE '%%' AND (user_email LIKE  '%"+ search + "%' OR user_name LIKE '%"+ search + "%' OR user_pass LIKE '%"+ search + "%');", con); //Query to display the wanted columns in the DB
                oleCommandBuilder = new OleDbCommandBuilder(ds);
                ds.Fill(dataTable);
                bindingSource = new BindingSource { DataSource = dataTable };// Binds any changes done in the datagrid with the database
                dataGridView1.DataSource = bindingSource;
                con.Close();
                dataGridView1.RowHeadersVisible = false;
                dataGridView1.Columns[0].HeaderText = "ID"; //Setting the names for the columns in the DataGrid
                dataGridView1.Columns[1].HeaderText = "Name";
                dataGridView1.Columns[2].HeaderText = "Password";
                dataGridView1.Columns[3].HeaderText = "Email";
                dataGridView1.Columns[4].HeaderText = "Username";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          

        }

        //Subprodcedure that belongs to the button for saving any changes done in the datagrid.
        private void button4_Click(object sender, EventArgs e)
        {
            String search = textBox1.Text;
            dataGridView1.EndEdit(); //Stops receiving client edits in datagrid.
            ds.Update(dataTable); //Updates the information in the Database.
            MessageBox.Show("Changes have been saved");
            loaddb(search);
        }


        //This subprocedure belongs to the Delete button, which deletes a row from the dataGridView and deletes that same row in the Database.
        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                DialogResult dialogResult = MessageBox.Show("You are about to delete a user permanently from the Database, are you sure?", "WARNING", MessageBoxButtons.YesNo); //dialog box
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
                    OleDbCommand cmd = new OleDbCommand("DELETE FROM Users WHERE user_ID=" + rowID + "", con); //deletes selected user from database
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Deletion attempted");
                    con.Close();
                    MessageBox.Show("deleted");

                    }
                    catch(Exception ex)
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

        private void button2_Click(object sender, EventArgs e)
        {
            String search = textBox1.Text;
            loaddb(search); //Queries with the inputted parameter from search bar.
        }
    }
}
