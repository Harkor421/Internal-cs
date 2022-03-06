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

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" || textBox2.Text != "")
            {
                // If the checkbox remind me is checked, the username and password typed, will be stored on a local config file 

                if (checkBox1.Checked == true)
                {
                    Properties.Settings.Default.username = textBox1.Text;
                    Properties.Settings.Default.password = textBox2.Text;
                    Properties.Settings.Default.Save();
                    Console.WriteLine("Data Saved");
                }
                //  Else the config file will be deleted. 

                if (checkBox1.Checked == false)
                {
                    Properties.Settings.Default.username = "";
                    Properties.Settings.Default.password = "";
                    Properties.Settings.Default.Save();
                    Console.WriteLine("Data deleted");
                }



                try //Try to see if there's connection with database, and catch any errors such as not typing in the textfields. 
                {
                    OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=DatabaseInternal1.accdb");
                    con.Open();
                    OleDbCommand cmd = new OleDbCommand("select user_pass from Users where user_email = '" + textBox1.Text + "'", con); //Query to find the user's password by email.
                    String expected = cmd.ExecuteScalar().ToString();
                    Console.Write("Expected HASH " + expected); // Finds expected hashed password in the database corresponding to that user.
                    Console.WriteLine("Hash encontrado");


                    String enpass = Global.MD5Encode(textBox2.Text, expected); // Hashes the current password typed and compares it.
                    Console.WriteLine(enpass);
                    Console.WriteLine("The password typed matches the password in the database");




                    cmd = new OleDbCommand("select * from Users where user_email='" + textBox1.Text + "' and user_pass='" + enpass + "'", con); //Logs in with the encrypted typed password and email.
                    OleDbDataReader dr = cmd.ExecuteReader();
                    cmd = new OleDbCommand("select user_privileges from Users where user_email='" + textBox1.Text + "' and user_pass='" + enpass + "'", con); //Query to find the user's password by email.
                    String privilege = cmd.ExecuteScalar().ToString();
                    if (dr.Read() == true) // Checks if a row with that query exists
                    {
                        if (privilege == "1")
                        {
                            Global.username = textBox1.Text; // The email is stored in a global variable. 
                            Global.password = textBox2.Text; //The password is stored in a global variable.
                            OleDbCommand cm = new OleDbCommand("select user_name from Users where user_email='" + textBox1.Text + "' and user_pass='" + enpass + "'", con); //Gets name of the user.
                            Global.name = cm.ExecuteScalar().ToString(); //The name of the user gets stored in a global variable.
                            Console.WriteLine(Global.name);
                            Form2 form = new Form2();
                            form.Show();
                            this.Hide();
                        }
                        else
                        {
                            label8.Text = ("The account doesn't have enough privileges");
                        }
                    }
                    else
                    {
                        label8.Text = "Invalid Credentials, Please Re-Enter";
                    }
                }
                catch (Exception ex)
                {
                    label8.Text = "Invalid Credentials, Please Re-Enter";
                }
            }
            else
            {
                label8.Text = "One of the fields is empty";
            }

        }

        private void Login_and_Register_Load(object sender, EventArgs e)
        {
            //Fills the text fields with the saved user_email and password when form loads.
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
    }
}
