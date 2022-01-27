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

namespace MicroHub
{

    public partial class Login_and_Register : Form
    {
        private DiscordRpc.EventHandlers handlers;
        private DiscordRpc.RichPresence presence;
        public Login_and_Register()
        {
            InitializeComponent();
            label14.Text = Global.version;
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

        public void conexiondb()
        {
            try
            {
                string conexions = "Server = 173.201.179.107; database = MicroHub; UID =salgui; password =salgui123";
                MySqlConnection conexion = new MySqlConnection(conexions);
                conexion.Open();

                if (conexion.State == ConnectionState.Open)
                {
                    Console.WriteLine("Conectado a base de datos");
                }
                else
                {
                    Console.WriteLine("No hay conexion con la base de datos");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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


            this.handlers = default(DiscordRpc.EventHandlers);
            DiscordRpc.Initialize("871957507256483860", ref this.handlers, true, null);
            this.handlers = default(DiscordRpc.EventHandlers);
            DiscordRpc.Initialize("871957507256483860", ref this.handlers, true, null);
            this.presence.details = "Aplicacion para optimizar tu PC!";
            this.presence.state = "http://www.microhubco.com/ ";
            this.presence.largeImageKey = "discord_rpc";
            this.presence.smallImageKey = "discord_rpc";
            this.presence.largeImageText = "Micro Hub";
            this.presence.smallImageText = "Micro Hub";
            this.presence.joinSecret = "http://www.microhubco.com/";
            DiscordRpc.UpdatePresence(ref this.presence);

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
            try {

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



                string user = textBox1.Text;
                string pass = textBox2.Text;



                string conexions = "Server = 173.201.179.107; database = i7679659_wp1; UID =salgui; password =salgui123";
                MySqlConnection conexion = new MySqlConnection(conexions);
                conexion.Open();

                if (conexion.State == ConnectionState.Open)
                {
                    Console.WriteLine("Conectado a base de datos");
                }
                else
                {
                    label8.Text = "*No hay conexión con internet, verifica tu red e intenta de nuevo*";
                    Console.WriteLine("No hay conexion con la base de datos");
                }





                MySqlCommand comm = conexion.CreateCommand();
                comm.CommandText = "select user_pass from wp_users where user_email = '" + user + "'";
                Console.Write("encontrando contra");
                String expected = comm.ExecuteScalar().ToString();
                Console.Write("Expected HASH " + expected);
                Console.WriteLine("Hash encontrado");


                String enpass = Global.MD5Encode(pass, expected);
                Console.WriteLine(enpass);
                Console.WriteLine("Contra descifrada");





                MySqlDataAdapter sda = new MySqlDataAdapter("select count(*) from wp_users where user_email ='" + user + "' and user_pass = '" + enpass + "'", conexion);
                DataTable dt = new DataTable();
                sda.Fill(dt);





                if (dt.Rows[0][0].ToString() == "1")
                {
                    Global.username = textBox1.Text;
                    Global.password = textBox2.Text;


                    string conexionss = "Server = 173.201.179.107; database = MicroHub; UID =salgui; password =salgui123";
                    MySqlConnection conexion1 = new MySqlConnection(conexionss);
                    conexion1.Open();
                    MySqlCommand commm = conexion1.CreateCommand();
                    commm.CommandText = "select status from status where version = '" + Global.version + "'";
                    String ver = commm.ExecuteScalar().ToString();
                    Console.Write(ver);
                    
                    if (ver == "1")
                    {
                        DialogResult dialogResult = MessageBox.Show("¡Hay una nueva actualización disponible! ¿Deseas actualizar? Ten en cuenta que nuestro software funcionará de manera más óptima si está en su última versión ", "ALERTA DE ACTUALIZACIÓN", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            MySqlCommand comm1 = conexion.CreateCommand();
                            comm1.CommandText = "select verlink from status where version = '" + Global.version + "'";
                            String vers = comm1.ExecuteScalar().ToString();
                            Console.Write(vers);
                            System.Diagnostics.Process.Start(vers);
                            Application.Exit();
                        }
                        else if (dialogResult == DialogResult.No)
                        {
                            //do something else
                        }
                    }


                    Form2 form = new Form2();
                    form.Show();
                    this.Hide();



                }

                else
                {
                    label8.Text = "*Usuario y/o contraseña incorrectos*";
                }
            }
            catch (Exception)
            {
                label8.Text = "*Usuario y/o contraseña incorrectos*";
            }
            finally
            {
                label8.Text = "*Usuario y/o contraseña incorrectos*";
            }
        }

    }
}
