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
            label1.Text = "Welcome Back " + Global.name;
        }


    }
}