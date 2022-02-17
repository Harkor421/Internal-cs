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
            loaddb();
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[1].HeaderText = "Name";
            dataGridView1.Columns[2].HeaderText = "Password";
            dataGridView1.Columns[3].HeaderText = "Email";
            dataGridView1.Columns[3].HeaderText = "Username";
        }

        private void loaddb()
        {
            dataGridView1.DataSource = null;
            dataTable.Clear();
            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=DatabaseInternal1.accdb");
            con.Open();
            try
            {
                ds = new OleDbDataAdapter("select user_ID,user_name,user_pass,user_email, username from Users", con);
                oleCommandBuilder = new OleDbCommandBuilder(ds);
                ds.Fill(dataTable);
                bindingSource = new BindingSource { DataSource = dataTable };
                dataGridView1.DataSource = bindingSource;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            con.Close();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.EndEdit(); //very important step
            ds.Update(dataTable);
            MessageBox.Show("Changes have been saved");
            loaddb();
        }


        //This subprocedure belongs to the Delete button, which deletes a row from the dataGridView and deletes that same row in the Database.
        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                DialogResult dialogResult = MessageBox.Show("You are about to delete a user permanently from the Database, are you sure?", "WARNING", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    //do something
               

                //Gets the index from the selected row in the datagrid.
                int selectedIndex = dataGridView1.CurrentCell.RowIndex;

                //Turns it into a UserID, so the information can be deleted in the database.
                int rowID = Convert.ToInt32(dataGridView1.CurrentCell.RowIndex);
                OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=DatabaseInternal1.accdb");
                    con.Open();
                    OleDbCommand cmd = new OleDbCommand("DELETE FROM Users WHERE user_ID=" + rowID + "", con);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Deletion attempted");
                    con.Close();
                    MessageBox.Show("deleted");

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
