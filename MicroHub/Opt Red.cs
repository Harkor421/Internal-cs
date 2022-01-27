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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using Shell32;
namespace MicroHub
{
    public partial class UserControl3 : UserControl
    {
        public UserControl3()
        {
            InitializeComponent();
            checkstatus();
            label15.Hide();
            label5.Hide();
            label6.Hide();
        }

        private void checkstatus()
        {
            RegistryKey maxconnectionper1 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Internet Explorer\\Main\\FeatureControl\\FEATURE_MAXCONNECTIONSPER1_0SERVER", true);
            RegistryKey tcp = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\Tcpip\\Parameters", true);
            
            Object maxconnection = maxconnectionper1.GetValue("explorer.exe");

            Object tcpcon = tcp.GetValue("Tcp1323Opts");
            Int32 tcpconn = Convert.ToInt32(tcpcon);

            if(tcpconn.ToString() == "0")
            {
                toggle1.CheckState = CheckState.Checked;
                Console.WriteLine("TCP Optimizado");
            }
            else
            {
                toggle1.CheckState = CheckState.Unchecked;
                Console.WriteLine("TCP No Optimizado");
            }

            if (maxconnection.ToString() != "10")
            {
                rjToggleButton1.CheckState = CheckState.Unchecked;
                Console.WriteLine("No se ha optimizado pc internet");
            }
            else
            {
                rjToggleButton1.CheckState = CheckState.Checked;
                Console.WriteLine("Ya esta optimizado internet");
            }

            if(Properties.Settings.Default.netshconfig == true)
            {
                rjToggleButton2.CheckState = CheckState.Checked;
            }
            else
            {
                rjToggleButton2.CheckState= CheckState.Unchecked;
            }
        }


        private void tcpip()
        {
            RegistryKey tcp = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\Tcpip\\Parameters", true);
            if (tcp != null)
            {
                tcp.CreateSubKey("EnableICMPRedirect");
                tcp.SetValue("EnableICMPRedirect", "1", RegistryValueKind.DWord);
                tcp.CreateSubKey("EnablePMTUDiscover");
                tcp.SetValue("EnablePMTUDiscovery", "1", RegistryValueKind.DWord);
                tcp.CreateSubKey("Tcp1323Opts");
                tcp.SetValue("Tcp1323Opts", "0", RegistryValueKind.DWord);
                tcp.CreateSubKey("TcpMaxDupAcks");
                tcp.SetValue("TcpMaxDupAcks", "2", RegistryValueKind.DWord);
                tcp.CreateSubKey("TcpTimedWaitDelay");
                tcp.SetValue("TcpTimedWaitDelay", "32", RegistryValueKind.DWord);
                tcp.CreateSubKey("GlobalMaxTcpWindowSize");
                tcp.SetValue("GlobalMaxTcpWindowSize", "8760", RegistryValueKind.DWord);
                tcp.CreateSubKey("TcpWindowSize");
                tcp.SetValue("TcpWindowSize", "8760", RegistryValueKind.DWord);
                tcp.CreateSubKey("MaxConnectionsPerServer");
                tcp.SetValue("MaxConnectionsPerServer", "0", RegistryValueKind.DWord);
                tcp.CreateSubKey("MaxUserPort");
                tcp.SetValue("MaxUserPort", "65534", RegistryValueKind.DWord);
                tcp.CreateSubKey("SackOpts");
                tcp.SetValue("SackOpts", "0", RegistryValueKind.DWord);
                tcp.CreateSubKey("DefaultTTL");
                tcp.SetValue("DefaultTTL", "64", RegistryValueKind.DWord);
                tcp.Close();
            }

        }

        
        private void netshcommand(string argument)
        {
            Process proc = new Process();
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.StartInfo.FileName = "netsh.exe";
            proc.StartInfo.Arguments = string.Format(argument);
            proc.StartInfo.RedirectStandardError = false;
            proc.StartInfo.RedirectStandardOutput = false;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.CreateNoWindow = true;
            proc.Start();

        }

        private void netsh()
        {
            netshcommand("int tcp set global autotuninglevel = normal");
            netshcommand("interface 6to4 set state disabled");
            netshcommand("int isatap set state disable");
            netshcommand("int tcp set global timestamps=disabled");
            netshcommand("int tcp set heuristics disabled");
            netshcommand("int tcp set global chimney=disabled");
            netshcommand("int tcp set global ecncapability=disabled");
            netshcommand("int tcp set global rsc=disabled");
            netshcommand("int tcp set global nonsackrttresiliency=disabled");
            netshcommand("int tcp set security profiles=disabled");
            netshcommand("int ip set global icmpredirects=disabled");
            netshcommand("int tcp set security mpp=disabled profiles=disabled");
            netshcommand("int ip set global multicastforwarding=disabled");
            netshcommand("int tcp set supplemental internet congestionprovider=ctcp");
            netshcommand("interface teredo set state disabled");
            netshcommand("winsock reset");
            netshcommand("int isatap set state disable");
            netshcommand("int ip set global taskoffload=disabled");
            netshcommand("int ip set global neighborcachelimit=4096");
            netshcommand("int tcp set global dca=enabled");
            netshcommand("int tcp set global netdma=enabled");
        }

        private void defaultnetsh()
        {
            string strCmdText;
            strCmdText = "ipconfig /release";
            Process.Start("CMD.exe", strCmdText);

            strCmdText = "ipconfig /flushdns";
            Process.Start("CMD.exe", strCmdText);

            strCmdText = "ipconfig /renew";
            Process.Start("CMD.exe", strCmdText);

            netshcommand("int ip reset");
            netshcommand("winsock");
        }

        private void tcipdefault()
        {
            RegistryKey tcp = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\Tcpip\\Parameters", true);
            if (tcp != null)
            {
                tcp.DeleteValue("EnableICMPRedirect");
                tcp.DeleteValue("EnablePMTUDiscovery");
                tcp.DeleteValue("Tcp1323Opts");
                tcp.DeleteValue("TcpMaxDupAcks");
                tcp.DeleteValue("TcpTimedWaitDelay");
                tcp.DeleteValue("GlobalMaxTcpWindowSize");
                tcp.DeleteValue("TcpWindowSize");
                tcp.DeleteValue("MaxConnectionsPerServer");
                tcp.DeleteValue("MaxUserPort");
                tcp.DeleteValue("SackOpts");
                tcp.DeleteValue("DefaultTTL");
                tcp.Close();
            }
        }
        
      
        private void optimizacionred()
        {
            RegistryKey maxconnectionper1 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Internet Explorer\\Main\\FeatureControl\\FEATURE_MAXCONNECTIONSPER1_0SERVER", true);
            if (maxconnectionper1 != null)
            {
                maxconnectionper1.SetValue("explorer.exe", "10", RegistryValueKind.String);
                maxconnectionper1.Close();
            }
      
         

            RegistryKey maxconnectionper12 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Internet Explorer\\Main\\FeatureControl\\FEATURE_MAXCONNECTIONSPERSERVER", true);
            if (maxconnectionper12 != null)
            {
                maxconnectionper12.SetValue("explorer.exe", "10", RegistryValueKind.String);
                maxconnectionper12.Close();
            }
          

            RegistryKey maxconnectionperserver = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\Tcpip\\ServiceProvider", true);
            if (maxconnectionperserver != null)
            {
                maxconnectionperserver.SetValue("LocalPriority", "4", RegistryValueKind.String);
                maxconnectionperserver.SetValue("HostsPriority", "5", RegistryValueKind.String);
                maxconnectionperserver.SetValue("DnsPriority", "6", RegistryValueKind.String);
                maxconnectionperserver.SetValue("NetbtPriority", "7", RegistryValueKind.String);
                maxconnectionperserver.Close();
            }

         


            RegistryKey NetworkThrottlingIndex = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Multimedia\\SystemProfile", true);
            if (NetworkThrottlingIndex != null)
            {
                NetworkThrottlingIndex.SetValue("NetworkThrottlingIndex", "ffffffff", RegistryValueKind.String);
                NetworkThrottlingIndex.SetValue("SystemResponsiveness", "0", RegistryValueKind.String);
                NetworkThrottlingIndex.Close();
            }
           

        }

        private void optdefault()
        {

            RegistryKey maxconnectionper1 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Internet Explorer\\Main\\FeatureControl\\FEATURE_MAXCONNECTIONSPER1_0SERVER", true);
            if (maxconnectionper1 != null)
            {
                maxconnectionper1.SetValue("explorer.exe", "4", RegistryValueKind.String);
                maxconnectionper1.Close();
            }

          


            RegistryKey maxconnectionper12 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Internet Explorer\\Main\\FeatureControl\\FEATURE_MAXCONNECTIONSPERSERVER", true);
            if (maxconnectionper12 != null)
            {
                maxconnectionper12.SetValue("explorer.exe", "2", RegistryValueKind.String);
                maxconnectionper12.Close();
            }

       


            RegistryKey maxconnectionperserver = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\Tcpip\\ServiceProvider", true);
            if (maxconnectionperserver != null)
            {
                maxconnectionperserver.SetValue("LocalPriority", "1f3", RegistryValueKind.String);
                maxconnectionperserver.SetValue("HostsPriority", "1f4", RegistryValueKind.String);
                maxconnectionperserver.SetValue("DnsPriority", "7d0", RegistryValueKind.String);
                maxconnectionperserver.SetValue("NetbtPriority", "7d1", RegistryValueKind.String);
                maxconnectionperserver.Close();
            }
           



            RegistryKey NetworkThrottlingIndex = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Multimedia\\SystemProfile", true);
            if (NetworkThrottlingIndex != null)
            {
                NetworkThrottlingIndex.SetValue("NetworkThrottlingIndex", "ffffffff", RegistryValueKind.String);
                NetworkThrottlingIndex.SetValue("SystemResponsiveness", "14", RegistryValueKind.String);
                NetworkThrottlingIndex.Close();
            }
        }

        private void toggle1_CheckedChanged(object sender, EventArgs e)
        {
            if (toggle1.Checked)
            {
                tcpip();
                label15.Show();
            }
            else
            {
                tcipdefault();
                label15.Show();
            }
        }

        private void rjToggleButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (rjToggleButton1.Checked)
            {
                optimizacionred();
                label5.Show();
            }
            else
            {
                optdefault();
                label5.Show();
            }
        }

        private void rjToggleButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (rjToggleButton2.Checked)
            {                
                netsh();
                label6.Show();
                Properties.Settings.Default.netshconfig = true;
                Properties.Settings.Default.Save();
                Console.WriteLine("netsh config active");

            }
            else
            {
                defaultnetsh();
                label6.Show();
                Properties.Settings.Default.netshconfig = false;
                Properties.Settings.Default.Save();
                Console.WriteLine("netsh config not active");
            }

        }
    }
}
