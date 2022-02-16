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
        public User()
        {
            InitializeComponent();
            loaddb();
        }

        private void loaddb()
        {
            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=DatabaseInternal1.accdb");
            con.Open();
            OleDbDataAdapter dataadapter = new OleDbDataAdapter("select user_name,user_pass,user_email, user_ID from Users", con);
            DataSet ds = new DataSet();
            dataadapter.Fill(ds, "Users");
            con.Close();

            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "Users";
        }

    }
}
