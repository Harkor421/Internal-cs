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
    public partial class Comments : UserControl
    {
        OleDbDataAdapter ds;
        private BindingSource bindingSource = null;
        private OleDbCommandBuilder oleCommandBuilder = null;
        DataTable dataTable = new DataTable();
        public Comments()
        {
            InitializeComponent();
            getcomments();
        }

        private void getcomments()
        {

            try //Try to see if there's connection with database, and catch any errors such as not typing in the textfields. 
            {



                dataGridView1.DataSource = null;
                dataTable.Clear();
                OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=DatabaseInternal1.accdb");
                con.Open();

                ds = new OleDbDataAdapter("SELECT User_F.feedback_ID, User_F.comment, Subscriptions.subscription_status From User_F, Subscriptions WHERE User_F.sub_ID = Subscriptions.sub_ID", con);
                oleCommandBuilder = new OleDbCommandBuilder(ds);
                ds.Fill(dataTable);
                bindingSource = new BindingSource { DataSource = dataTable };// Binds any changes done in the datagrid with the database
                dataGridView1.DataSource = bindingSource;
                con.Close();
                dataGridView1.RowHeadersVisible = false;
                dataGridView1.Columns[0].Width = 100;
                dataGridView1.Columns[1].Width = 330;
                dataGridView1.Columns[0].HeaderText = "Comment #"; //Setting the names for the columns in the DataGrid
                dataGridView1.Columns[1].HeaderText = "Comment";
                dataGridView1.Columns[2].HeaderText = "Subscription Status";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            getcomments();
        }
    }
}
