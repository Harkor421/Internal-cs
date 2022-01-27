using MicroHub;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Timers;
using System.Windows.Forms;
using static MicroHub.Global;

namespace MicroHub
{
    public partial class MySecondCustmControl : UserControl
    {
        List <String> listdirs = new List <String>();

        List<String> list = new List<String>();

        bool Minecraft, Valorant, Lol, osu, Fortnite;
        int count1;

        public MySecondCustmControl()
        {
            InitializeComponent();
            gpu();
            cpuinfo();
            ram();
            specsdb();
            detectgames();
            detectgamefiles();


            label1.AutoSize = true;
            label2.AutoSize = true;
            label7.AutoSize = true;
            label10.AutoSize = true;
            label11.AutoSize = true;

            Global.progress = Global.progress + 10;
        }


        private void detectgamefiles()
        {


        }


        private void detectgames()
        {
            try
            {
                string registry_key = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(registry_key))
                {
                    foreach (string subkey_name in key.GetSubKeyNames())
                    {

                        using (RegistryKey subkey = key.OpenSubKey(subkey_name))
                        {

                            Console.WriteLine(subkey.GetValue("DisplayName"));
                            list.Add("" + subkey.GetValue("DisplayName"));
                            object temp = subkey.GetValue("ApplicationPath");
                            string path = subkey.ToString();
                            listdirs.Add(path);

                        }
                    }
                }
            
                

                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(registry_key))
                {
                    foreach (string subkey_name in key.GetSubKeyNames())
                    {
                        using (RegistryKey subkey = key.OpenSubKey(subkey_name))
                        {
                            Console.WriteLine(subkey.GetValue("DisplayName"));
                            list.Add("" + subkey.GetValue("DisplayName"));
                            object temp = subkey.GetValue("ApplicationPath");
                            string path = subkey.ToString();
                            listdirs.Add(path);
                        }
                    }
                }

                while (count1 < list.Count)
                {
                    switch (list[count1])
                    {
                        case "VALORANT":
                            {
                                string valpath = listdirs[count1]; 
                                Valorant = true;
                                Console.WriteLine(valpath);
                                break;
                            }
                        case "League of Legends":
                            {
                                Lol = true;
                                break;
                            }
                        case "osu!":
                            {
                                osu = true;
                                break;

                            }
                        case "Minecraft Launcher":
                            {
                                Minecraft = true;
                                break;
                            }

                        case "Fortnite":
                            {
                                Fortnite = true;
                                break;
                            }
                    }

                    count1 = count1 + 1;
                }


                count1 = 0;


                ImageList imgs = new ImageList();
                imgs.ImageSize = new Size(50, 50);

                //load images from file 

                String[] paths = { };
                paths = Directory.GetFiles(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"images"));
                try
                {
                    foreach (String path in paths)
                    {
                        imgs.Images.Add(Image.FromFile(path));
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }

                listView1.SmallImageList = imgs;


                if (Minecraft == true)
                {
                    listView1.Items.Add("Minecraft", 0);
                }
                else
                {

                }


                if (osu == true)
                {
                    listView1.Items.Add("Osu!", 1);
                }
                if (Lol == true)
                {
                    listView1.Items.Add("League of Legends", 3);
                }

                if (Valorant == true)
                {
                    listView1.Items.Add("Valorant", 2);
                }

                if (Fortnite == true)
                {
                    listView1.Items.Add("Fortnite", 4);
                }
            }
            catch
            {
                Console.WriteLine("Unable to detect games");
            }
        }

       
        private void cpuinfo()
        {
            var cpu =
              new ManagementObjectSearcher("select * from Win32_Processor")
              .Get()
              .Cast<ManagementObject>()
              .First();

            string name = (string)cpu["Name"];
            label10.Text = "CPU " + name;
            Console.WriteLine("CPU " + name);
            uint cpucores = (uint)cpu["NumberOfCores"];
            uint cputhreads = (uint)cpu["NumberOfLogicalProcessors"];
            label11.Text = "Nucleos: " + cpucores + "  Hilos: " + cputhreads;
            Console.WriteLine("Nucleos: " + cpucores + "  Hilos: " + cputhreads);
        }

        

        private void specsdb()
        {

            var cpu =new ManagementObjectSearcher("select * from Win32_Processor").Get().Cast<ManagementObject>().First();

            string namecpu = (string)cpu["Name"];

            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_DisplayConfiguration");
            string graphicsCard = string.Empty;
            foreach (ManagementObject mo in searcher.Get())
            {
                foreach (PropertyData property in mo.Properties)
                {
                    if (property.Name == "Description")
                    {
                        graphicsCard = property.Value.ToString();
                        label7.Text = "GPU:" + graphicsCard;
                    }
                }
            }

            UInt64 total = 0;
            ManagementObjectSearcher ramsearch = new ManagementObjectSearcher("Select * From Win32_PhysicalMemory");
            foreach (ManagementObject ram in ramsearch.Get())
            {
                total += (UInt64)ram.GetPropertyValue("Capacity");
               
            
            }

                string conexions = "Server = 173.201.179.107; database = MicroHub; UID =salgui; password =salgui123";
            MySqlConnection conexion = new MySqlConnection(conexions);
            conexion.Open();


            String specs = "CPU: " + namecpu + " GPU: " + graphicsCard + " RAM " + total / 1073741824;
     

            MySqlCommand comm = conexion.CreateCommand();
            comm.CommandText = "UPDATE Usuarios SET cpu= '"+ namecpu + "' WHERE username='"+ Global.username+ "'";
            comm.ExecuteNonQuery();

            MySqlCommand comm2 = conexion.CreateCommand();
            comm2.CommandText = "UPDATE Usuarios SET gpu= '" + graphicsCard + "' WHERE username='" + Global.username + "'";
            comm2.ExecuteNonQuery();

            MySqlCommand comm3 = conexion.CreateCommand();
            comm3.CommandText = "UPDATE Usuarios SET ram= '" + total/ 1073741824 + "' WHERE username='" + Global.username + "'";
            comm3.ExecuteNonQuery();
        }


        private void gpu()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_DisplayConfiguration");

            string graphicsCard = string.Empty;
            foreach (ManagementObject mo in searcher.Get())
            {
                foreach (PropertyData property in mo.Properties)
                {
                    if (property.Name == "Description")
                    {
                        graphicsCard = property.Value.ToString();
                        label7.Text = "GPU " + graphicsCard;
                        Console.WriteLine("GPU: " + graphicsCard);
                    }
                }
            }
        }

        private void ram()
        {

            uint freq = 0;
            UInt64 total = 0;
            UInt32 hz = 0;
            ManagementObjectSearcher ramsearch = new ManagementObjectSearcher("Select * From Win32_PhysicalMemory");
              foreach (ManagementObject ram in ramsearch.Get())
            {
                total += (UInt64)ram.GetPropertyValue("Capacity");
                hz += (UInt32)ram.GetPropertyValue("Speed");


                if (hz < 3600)
                {
                    freq = hz / 2;
                }
                else
                {
                    if (hz > 3600 && hz < 7200)
                    {
                        freq = hz / 2;
                    }
                    else
                    {
                        if (hz > 7200 && hz < 14400)
                        {
                            freq = hz / 4;
                        }
                    }
                }
            }
            label1.Text = "RAM " + total / 1073741824 + " GB" + " - Frecuencia " + freq + " mhz";
            Console.WriteLine("RAM: " + total / 1073741824 + " GB" + " Frecuencia " + freq + " mhz");
        }

        private void MySecondCustmControl_Load(object sender, EventArgs e)
        {
            //LV PROPERTIES 
            listView1.View = View.Details;

            //Construir columnas

            listView1.Columns.Add("Juegos", 150);
            listView1.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.HeaderSize);


        }

    }
}
