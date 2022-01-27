using MySql.Data.MySqlClient;
using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Security.Principal;
using System.Windows.Forms;



namespace MicroHub
{
    public partial class Form1 : Form
    {
     
        public Form1()
        {
            InitializeComponent();
            firstCustomControl1.BringToFront();
            label3.Text = Global.username;
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

        public void OnProcessExit(object sender, EventArgs e)
        {
            Console.WriteLine("Monitoring ended.");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            firstCustomControl1.BringToFront();
        }

        private void button2_Click(object sender, EventArgs e)
        {   
     
        }

        private void button3_Click(object sender, EventArgs e)
        {
        
        }

        private void button13_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
        
        
        }

        private void button10_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.instagram.com/microhubco/");
        }

        private void button5_Click(object sender, EventArgs e)
        {       

        }


        private void label1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
  
        private void Form1_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            Console.WriteLine("cerrando");
            Application.ExitThread();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }


        int mov, movX, movY;
        private void mySecondCustmControl1_MouseDown(object sender, MouseEventArgs e)
        {
            mov = 1;
            movX = e.X;
            movY = e.Y;
        }

        private void mySecondCustmControl1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mov == 1)
            {
                this.SetDesktopLocation(MousePosition.X - movX, MousePosition.Y - movY);
            }
        }

        private void firstCustomControl1_MouseUp(object sender, MouseEventArgs e)
        {
            mov = 0;
        }

        private void firstCustomControl1_MouseDown(object sender, MouseEventArgs e)
        {
            mov = 1;
            movX = e.X;
            movY = e.Y;
        }

        private void firstCustomControl1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mov == 1)
            {
                this.SetDesktopLocation(MousePosition.X - movX, MousePosition.Y - movY);
            }
        }

        private void cuentaGUI1_MouseUp(object sender, MouseEventArgs e)
        {
            mov = 0;
        }

        private void cuentaGUI1_MouseDown(object sender, MouseEventArgs e)
        {
            mov = 1;
            movX = e.X;
            movY = e.Y;
        }

        private void cuentaGUI1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mov == 1)
            {
                this.SetDesktopLocation(MousePosition.X - movX, MousePosition.Y - movY);
            }
        }

        private void third1_MouseUp(object sender, MouseEventArgs e)
        {
            mov = 0;
        }

        private void third1_MouseDown(object sender, MouseEventArgs e)
        {
            mov = 1;
            movX = e.X;
            movY = e.Y;
        }

        private void third1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mov == 1)
            {
                this.SetDesktopLocation(MousePosition.X - movX, MousePosition.Y - movY);
            }
        }

        private void userControl22_MouseUp(object sender, MouseEventArgs e)
        {
            mov = 0;
        }

        private void userControl22_MouseDown(object sender, MouseEventArgs e)
        {
            mov = 1;
            movX = e.X;
            movY = e.Y;
        }

        private void userControl22_MouseMove(object sender, MouseEventArgs e)
        {
            if (mov == 1)
            {
                this.SetDesktopLocation(MousePosition.X - movX, MousePosition.Y - movY);
            }
        }

        private void userControl31_MouseUp(object sender, MouseEventArgs e)
        {
            mov = 0;
        }

        private void userControl31_MouseDown(object sender, MouseEventArgs e)
        {
            mov = 1;
            movX = e.X;
            movY = e.Y;
        }

        private void userControl31_MouseMove(object sender, MouseEventArgs e)
        {
            if (mov == 1)
            {
                this.SetDesktopLocation(MousePosition.X - movX, MousePosition.Y - movY);
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {

        }


        private void mySecondCustmControl1_MouseUp(object sender, MouseEventArgs e)
        {
            mov = 0;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Restarting");
            Properties.Settings.Default.username = "";
            Properties.Settings.Default.password = "";
            Properties.Settings.Default.Save();

            this.Hide();
            var form2 = new Login_and_Register();
            form2.Closed += (s, args) => this.Close();
            form2.Show();
        }
    }
}
