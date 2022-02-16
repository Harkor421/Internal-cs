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
            dataGridView1.RowHeadersVisible = false;
            loaddb();
        }

        private void loaddb()
        {
            dataGridView1.DataSource = null;
            dataTable.Clear();
            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=DatabaseInternal1.accdb");
            con.Open();
            OleDbCommand command = con.CreateCommand();
            command.CommandText = "select user_name,user_pass,user_email, user_ID from Users";
            try
            {
                ds = new OleDbDataAdapter("select user_name,user_pass,user_email, user_ID from Users", con);
                oleCommandBuilder = new OleDbCommandBuilder(ds);
                ds.Fill(dataTable);
                bindingSource = new BindingSource { DataSource = dataTable };
                dataGridView1.DataSource = bindingSource;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.EndEdit(); //very important step
            ds.Update(dataTable);
            MessageBox.Show("Updated");
            loaddb();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
