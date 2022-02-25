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
        public Comments()
        {
            InitializeComponent();
            getcomments();
        }

        private void getcomments()
        {

            try //Try to see if there's connection with database, and catch any errors such as not typing in the textfields. 
            {
                OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=DatabaseInternal1.accdb");
                con.Open();
              



                OleDbCommand cmd = new OleDbCommand("select * from Users", con); //Logs in with the encrypted typed password and email.
                OleDbDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ListViewItem lvItem = new ListViewItem();
                    lvItem.SubItems[0].Text = dr[0].ToString();
                    lvItem.SubItems.Add(dr[0].ToString());
                    listView1.Items.Add(lvItem);
                }
                dr.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
