using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DiscordRpcDemo;
using System.Data.OleDb;

namespace MicroHub
{

    public partial class Login_and_Register : Form
    {

        public Login_and_Register()
        {
            InitializeComponent();
            textBox2.PasswordChar = '*';
            textBox2.MaxLength = 10;

        }



        //No Flicker
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleparam = base.CreateParams;
                handleparam.ExStyle = 0x02000000;
                return handleparam;
            }
        }

        int mov, movX, movY;
        private void Login_and_Register_MouseUp(object sender, MouseEventArgs e)
        {
            mov = 0;
        }

        private void Login_and_Register_MouseDown(object sender, MouseEventArgs e)
        {
            mov = 1;
            movX = e.X;
            movY = e.Y;
        }

        private void Login_and_Register_MouseMove(object sender, MouseEventArgs e)
        {
            if (mov == 1)
            {
                this.SetDesktopLocation(MousePosition.X - movX, MousePosition.Y - movY);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.microhubco.com/");
        }

        private void label7_Click(object sender, EventArgs e)
        {
            panel1.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.Show();
            this.Hide();
        }

        private void label4_Click_1(object sender, EventArgs e)
        {
            Process.Start("http://www.microhubco.com/mi-cuenta/");
        }

        private void Login_and_Register_Load(object sender, EventArgs e)
        {
            if (WindowsIdentity.GetCurrent().Owner == WindowsIdentity.GetCurrent().User)   // Check for Admin privileges   
            {
                try
                {
                    this.Visible = false;
                    ProcessStartInfo info = new ProcessStartInfo(Application.ExecutablePath); // my own .exe
                    info.UseShellExecute = true;
                    info.Verb = "runas";   // invoke UAC prompt
                    Process.Start(info);
                }
                catch (Win32Exception ex)
                {
                    if (ex.NativeErrorCode == 1223) //The operation was canceled by the user.
                    {
                        MessageBox.Show("Debes aceptar los permisos de administrador para hacer uso del app");
                        Application.Exit();
                    }
                    else
                        throw new Exception("Something went wrong :-(");
                }
                Application.Exit();
            }
            else
            {
                //    MessageBox.Show("I have admin privileges :-)");
            }




            if (Properties.Settings.Default.username != string.Empty)
            {
                textBox1.Text = Properties.Settings.Default.username;
                textBox2.Text = Properties.Settings.Default.password;
            }
            if (textBox1.Text.Length != 0 && textBox1.Text.Length != 0)
            {
                checkBox1.CheckState = CheckState.Checked;
            }
            else
            {
                checkBox1.CheckState = CheckState.Unchecked;
            }
        }

        private void button_WOC1_Click(object sender, EventArgs e)
        {
          

                if (checkBox1.Checked == true)
                {
                    Properties.Settings.Default.username = textBox1.Text;
                    Properties.Settings.Default.password = textBox2.Text;
                    Properties.Settings.Default.Save();
                    Console.WriteLine("datos guardados");
                }

                if (checkBox1.Checked == false)
                {
                    Properties.Settings.Default.username = "";
                    Properties.Settings.Default.password = "";
                    Properties.Settings.Default.Save();
                    Console.WriteLine("datos eliminados");
                }



                try
                {

                    OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=DatabaseInternal1.accdb");
                    con.Open();
                    OleDbCommand cmd = new OleDbCommand("select * from usuario where email='" + textBox1.Text + "' and password='" + textBox2.Text + "'", con);
                    OleDbDataReader dr = cmd.ExecuteReader();
                    if (dr.Read() == true)
                    {
                        MessageBox.Show("Login Successful");

                        Global.username = textBox1.Text;
                        Global.password = textBox2.Text;

                        Form2 form = new Form2();
                        form.Show();
                        this.Hide();

                    }
                    else
                    {
                        MessageBox.Show("Invalid Credentials, Please Re-Enter");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {

                }

            }
    }

}
