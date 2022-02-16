using MicroHub;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MicroHub
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            rjProgressBar1.Increment(10);
            Console.WriteLine("Timer");
            if (rjProgressBar1.Value == 100)
            {
                Console.WriteLine("Application Loaded");
                timer1.Enabled = false;
                Form1 form = new Form1();
                form.Show();
                this.Hide();
            }
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            
        }
    }
}
