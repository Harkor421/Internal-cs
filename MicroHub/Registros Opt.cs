using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using Shell32;

namespace MicroHub
{

    public partial class UserControl2 : UserControl
    {

        public UserControl2()
        {
            InitializeComponent();
            Global.progress = Global.progress + 10;

        }


        //Bateria

        private void Bateria()
        {
            string strCmdText = "powercfg -duplicatescheme e9a42b02-d5df-448d-aa00-03f14749eb61";
            Process.Start("CMD.exe", strCmdText);
        }



        //Borrar archivos temporales 
        private static void ClearTempData(System.IO.DirectoryInfo di)
        {
            foreach (FileInfo file in di.GetFiles())
            {
                try
                {
                    file.Delete();
                    Console.WriteLine(file.FullName);
                }
                catch
                {
                    continue;
                }
            }

            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                try
                {
                    dir.Delete(true);
                    Console.WriteLine(dir.FullName);
                }
                catch
                {
                    continue;
                }
            }
        }

        [DllImport("Shell32.dll", CharSet = CharSet.Unicode)]
        static extern uint SHEmptyRecycleBin(IntPtr hwnd, string pszRootPath,
        RecycleFlags dwFlags);

        enum RecycleFlags : uint
        {
            SHERB_NOCONFIRMATION = 0x00000001,
            SHERB_NOPROGRESSUI = 0x00000001,
            SHERB_NOSOUND = 0x00000004
        }




        //Subprocedimiento para eliminar archivos temporales...
        private void borrartemp()
        {
            pictureBox1.Hide();
            System.IO.DirectoryInfo di = null;
            //Delete Internet Cache
            string path = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache);
            di = new DirectoryInfo(path);
            Console.WriteLine("****Eliminando Historial de Internet****");
            ClearTempData(di);
            Console.WriteLine("****Terminado de eliminar****");


            //Delete Cookies
            string cookie = Environment.GetFolderPath(Environment.SpecialFolder.Cookies);
            di = new DirectoryInfo(path);
            Console.WriteLine("****Deleting progreso de Cookies****");
            ClearTempData(di);
            Console.WriteLine("****Terminado de eliminar progreso de Cookies****");




            //Delete History
            string history = Environment.GetFolderPath(Environment.SpecialFolder.History);
            di = new DirectoryInfo(path);
            Console.WriteLine("****Deleting History progress****");
            ClearTempData(di);
            Console.WriteLine("****Finished Deleting History****");




            //Delete temporary folder
            di = new DirectoryInfo(@"C:\Windows\Temp");
            Console.WriteLine("****Deleting Temp files progress****");
            ClearTempData(di);
            Console.WriteLine("****Finished Deleting Temp files****");



            di = new DirectoryInfo(@"C:\Windows\Prefetch");
            Console.WriteLine("****Deleting Temp Prefetch files progress****");
            ClearTempData(di);
            Console.WriteLine("****Finished Deleting Temp files****");



            di = new DirectoryInfo(System.IO.Path.GetTempPath());
            Console.WriteLine("****Deleting App Data Temp files progress****");
            ClearTempData(di);
            Console.WriteLine("****Finished Deleting App Data Temp files****");


            //Delete RecycleBin

            Console.WriteLine("****Deleting RecyleBin****");
            uint result = SHEmptyRecycleBin(IntPtr.Zero, null, 0);
            Console.WriteLine("****Finished Deleting RecyleBin****");

        }

        
        private void UserControl2_Load(object sender, EventArgs e)
        {
            try
            {

                RegistryKey meltdown = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\Session Manager\\Memory Management", true);
                RegistryKey dvrcon = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\GameDVR", true);
                RegistryKey dvrcon2 = Registry.CurrentUser.OpenSubKey("System\\GameConfigStore", true);
                RegistryKey cortana = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\Windows Search", true);

                Object meltdowncon = meltdown.GetValue("FeatureSettings");
                Int32 meltdownconn = Convert.ToInt32(meltdowncon);


                Object cortanacon = cortana.GetValue("AllowCortana");
                Int32 cortanaconn = Convert.ToInt32(cortanacon);

                Object obj = dvrcon.GetValue("AppCaptureEnabled");
                Int32 dvr1 = Convert.ToInt32(obj);
                obj = dvrcon2.GetValue("GameDVR_Enabled");
                Int32 dvr2 = Convert.ToInt32(obj);


                if (meltdownconn == 0)
                {
                    toggle1.CheckState = CheckState.Checked;
                }
                else
                {
                    toggle1.CheckState = CheckState.Unchecked;
                }

                if (dvr1 == 0 && dvr2 == 0)
                {
                    rjToggleButton1.CheckState = CheckState.Checked;
                }
                else
                {
                    rjToggleButton1.CheckState = CheckState.Unchecked;
                }

                if (cortanaconn == 0)
                {
                    rjToggleButton2.CheckState = CheckState.Checked;
                }
                else
                {
                    rjToggleButton2.CheckState = CheckState.Unchecked;
                }
                label14.Hide();
            }
            catch (Exception)
            {

            }

        }

        private void DVR()
        {
            RegistryKey tasks1 = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\GameDVR", true);
            if (tasks1 != null)
            {
                tasks1.SetValue("AppCaptureEnabled", "0", RegistryValueKind.DWord);
            }

            RegistryKey tasks2 = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\GameDVR", true);
            if (tasks2 != null)
            {
                tasks2.SetValue("GameDVR_Enabled", "0", RegistryValueKind.DWord);
            }
        }

        private void DVRdef()
        {
            RegistryKey tasks1 = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\GameDVR", true);
            if (tasks1 != null)
            {
                tasks1.SetValue("AppCaptureEnabled", "1", RegistryValueKind.DWord);
            }

            RegistryKey tasks2 = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\GameDVR", true);
            if (tasks2 != null)
            {
                tasks2.SetValue("GameDVR_Enabled", "1", RegistryValueKind.DWord);
            }
        }

        private void meltdown()
        {
            RegistryKey tasks8 = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\Session Manager\\Memory Management", true);
            if (tasks8 != null)
            {
                tasks8.SetValue("FeatureSettings", "0", RegistryValueKind.DWord);
                tasks8.CreateSubKey("FeatureSettingsOverride");
                tasks8.SetValue("FeatureSettingsOverride", "3", RegistryValueKind.DWord);
                tasks8.CreateSubKey("FeatureSettingsOverrideMask");
                tasks8.SetValue("FeatureSettingsOverrideMask", "3", RegistryValueKind.DWord);
                tasks8.Close();
            }
        }

        private void meltdowndefault()
        {
            RegistryKey tasks8 = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\Session Manager\\Memory Management", true);
            if (tasks8 != null)
            {
                tasks8.SetValue("FeatureSettings", "1", RegistryValueKind.DWord);
                tasks8.DeleteValue("FeatureSettingsOverride");
                tasks8.DeleteValue("FeatureSettingsOverrideMask");
                tasks8.Close();
            }
        }


        private void toggle1_CheckedChanged(object sender, EventArgs e)
        {
            if (toggle1.Checked)
            {
                meltdown();
            }
            else
            {
                meltdowndefault();
            }
        }

        private void rjToggleButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (rjToggleButton1.Checked)
            {
                DVR();
            }
            else
            {
                DVRdef();
            }
        }

        private void circularProgressBar1_Click(object sender, EventArgs e)
        {
            borrartemp();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            borrartemp();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

        }

        private void rjToggleButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (rjToggleButton2.Checked)
            {
                RegistryKey subKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\Windows Search");
                if (!checkIfKeyExists(subKey))
                {
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\Windows Search");
                }
                RegistryKey tasks8 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\Windows Search", true);
                if (tasks8 != null)
                {
                    tasks8.SetValue("AllowCortana", "0", RegistryValueKind.DWord);
                    tasks8.Close();
                    label14.Show();
                }

            }
            else
            {
                RegistryKey subKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\Windows Search");
                    if (!checkIfKeyExists(subKey))
                {
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\Windows Search");
                }

                RegistryKey tasks2 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\Windows Search", true);
                if (tasks2 != null)
                {
                    tasks2.SetValue("AllowCortana", "1", RegistryValueKind.DWord);
                    tasks2.Close();
                    label14.Show();
                }
            }
    }

        private static bool checkIfKeyExists(RegistryKey subKey)
        {
            bool status = true;
            if (subKey == null)
            {
                status = false;
            }
            return status;
        }

    }
}
