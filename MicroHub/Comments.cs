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
        DataObject collection = new DataObject();
        public Comments()
        {
            InitializeComponent();
            /*/
            dataGridView1.Columns[0].HeaderText = "Comment #"; //Setting the names for the columns in the DataGrid
            dataGridView1.Columns[1].HeaderText = "Comment";
            dataGridView1.Columns[2].HeaderText = "Member Rank";
            DataTable dataTable = new DataTable();
            getcomments();
            /**/
        }

        private void getcomments()
        {

            try //Try to see if there's connection with database, and catch any errors such as not typing in the textfields. 
            {
                OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=DatabaseInternal1.accdb");
                con.Open();



                int i = 0;
                OleDbCommand cmd = new OleDbCommand("select feedback_ID, comment, sub_ID from User_F", con); 
                OleDbDataReader dr = cmd.ExecuteReader();              
                
         
                while (dr.Read())
                {
             

                }
                dr.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
