using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace MicroHub
{
    public partial class FirstCustomControl : UserControl
    {

        public FirstCustomControl()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://discord.gg/aTvaybnp");
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.microhubco.com/");
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.instagram.com/microhubco/");
        }
    }
}