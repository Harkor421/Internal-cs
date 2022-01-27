using Microsoft.Win32;
using System;
using System.Windows.Forms;


namespace MicroHub
{



    public partial class third : UserControl
    {


        public third()
        {
            InitializeComponent();
            checkcheck();
            label15.Hide();
            Global.progress = Global.progress + 10; 
        }


        //Subprocedimiento para verificar la activacion de los registros...
        public void checkcheck()
        {
            RegistryKey unparkcpu = Registry.LocalMachine.OpenSubKey("SYSTEM\\ControlSet001\\Control\\Power\\PowerSettings\\54533251-82be-4824-96c1-47b60b740d00\\0cc5b647-c1df-4637-891a-dec35c318583", true);
            RegistryKey keyboard = Registry.CurrentUser.OpenSubKey("Control Panel\\Accessibility\\Keyboard Response", true);
            RegistryKey diag = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\DiagTrack", true);
            RegistryKey gpuopt = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\GraphicsDrivers", true);
            RegistryKey app = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Search", true);
            RegistryKey quick = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\Session Manager\\Power", true);
            RegistryKey wingamemodedir = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\GameBar", true);
            RegistryKey transparencia = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize", true);

            string keyboard1 = (string)keyboard.GetValue("AutoRepeatDelay");
            string keyboard2 = (string)keyboard.GetValue("AutoRepeatRate");
            string keyboard3 = (string)keyboard.GetValue("Flags");

            Int32 gamemodecon = Convert.ToInt32(wingamemodedir.GetValue("AutoGameModeEnabled"));


            Object unpark = unparkcpu.GetValue("ValueMax");
            Int32 unparkcon = Convert.ToInt32(unpark);

            Object diagnosis = diag.GetValue("Start");
            string diagnosiscon = (string)diagnosis.ToString();


            Object quickconn = quick.GetValue("HiberbootEnabled");
            Int32 quickcon = Convert.ToInt32(quickconn);

            Object appconn = app.GetValue("BackgroundAppGlobalToggle");
            Int32 appcon = Convert.ToInt32(appconn);



            Object gpuoptconn = gpuopt.GetValue("HwSchMode");
            Int32 gpuoptcon = Convert.ToInt32(gpuoptconn);
            Console.Write(gpuoptconn);


            Object transparenciacon = transparencia.GetValue("EnableTransparency");
            Int32 transparenciaconn = Convert.ToInt32(transparenciacon);


            if (transparenciaconn == 0)
            {
                rjToggleButton1.CheckedState = CheckState.Checked;
            }
            else
            {
                rjToggleButton1.CheckedState = CheckState.Unchecked;
            }


            if (gamemodecon == 1)
            {
                toggle4.CheckState = CheckState.Checked;

            }
            else
            {
                toggle4.CheckState = CheckState.Unchecked;
            }

            if (unparkcon == 0)
            {
                toggle1.CheckState = CheckState.Checked;
                Console.WriteLine("Núcleos desbloqueados");

            }
            else
            {
                toggle1.CheckState = CheckState.Unchecked;
                Console.WriteLine("Núcleos Bloqueados");

            }



            if (keyboard1 == "500" && keyboard2 == "20" && keyboard3 == "122")
            {
                toggle2.CheckState = CheckState.Checked;
                Console.WriteLine("Teclado optimizado");
            }
            else
            {
                toggle2.CheckState = CheckState.Unchecked;
                Console.WriteLine("Teclado no optimizado");
            }




            if (diagnosiscon == "4")
            {
                toggle6.CheckState = CheckState.Checked;
                Console.WriteLine("Servicios de Diagnosis deshabilitados");
            }
            else
            {
                toggle6.CheckState = CheckState.Unchecked;
                Console.WriteLine("Servicios de Diagnosis Habilitados");
                Console.Write(diagnosiscon);
            }


            if (gpuoptcon == 2)
            {
                toggle7.CheckState = CheckState.Checked;
                Console.WriteLine("Aceleración de gráfica activada");
            }
            else
            {
                toggle7.CheckState = CheckState.Unchecked;
                Console.WriteLine("Aceleración de gráfica desactivada");
            }


            if (appcon == 0)
            {
                toggle5.CheckState = CheckState.Checked;
                Console.WriteLine("Aplicaciones en segundo plano desactivadas");
            }
            else
            {
                toggle5.CheckState = CheckState.Unchecked;
                Console.WriteLine("Aplicaciones en segundo plano Activadas");

            }

            if (quickcon == 1)
            {
                toggle3.CheckState = CheckState.Checked;
                Console.WriteLine("Inicio rapido habilitado");
            }
            else
            {
                toggle3.CheckState = CheckState.Unchecked;
                Console.WriteLine("Inicio rapido deshabilitado");

            }

        }


        private void transparenciacolor()
        {
            RegistryKey tasks8 = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize", true);
            if (tasks8 != null)
            {
                tasks8.SetValue("EnableTransparency", "0", RegistryValueKind.DWord);
                tasks8.Close();
            }
        }

        private void transparenciacolordefault()
        {
            RegistryKey tasks8 = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize", true);
            if (tasks8 != null)
            {
                tasks8.SetValue("EnableTransparency", "1", RegistryValueKind.DWord);
                tasks8.Close();
            }
        }

        private void gpuopt()
        {
            RegistryKey gpuopt = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\GraphicsDrivers", true);
            gpuopt.SetValue("HwSchMode", "2", RegistryValueKind.DWord);
            gpuopt.Close();

        }

        private void gpuoptdefault()
        {
            RegistryKey gpuopt = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\GraphicsDrivers", true);
            gpuopt.SetValue("HwSchMode", "1", RegistryValueKind.DWord);
            gpuopt.Close();
        }

        private void quickon()
        {

            RegistryKey quick = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\Session Manager\\Power", true);
            quick.SetValue("HiberbootEnabled", "1", RegistryValueKind.DWord);
            quick.Close();
        }


        private void backgroundapps()
        {
            RegistryKey app = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Search", true);
            app.SetValue("BackgroundAppGlobalToggle", "0", RegistryValueKind.DWord);
            app.Close();


            RegistryKey app2 = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\BackgroundAccessApplications", true);
            app2.SetValue("GlobalUserDisabled", "1", RegistryValueKind.DWord);
            app2.Close();
        }

        private void backgroundappsdefault()
        {
            RegistryKey app = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Search", true);
            app.SetValue("BackgroundAppGlobalToggle", "1", RegistryValueKind.DWord);
            app.Close();

            RegistryKey app2 = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\BackgroundAccessApplications", true);
            app2.SetValue("GlobalUserDisabled", "0", RegistryValueKind.DWord);
            app2.Close();
        }


        private void quickondefault()
        {
            RegistryKey quickon = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\Session Manager\\Power", true);
            quickon.SetValue("HiberbootEnabled", "0", RegistryValueKind.DWord);
            quickon.Close();
        }


        private void unparkcpu()
        {
            RegistryKey unparkcpu = Registry.LocalMachine.OpenSubKey("SYSTEM\\ControlSet001\\Control\\Power\\PowerSettings\\54533251-82be-4824-96c1-47b60b740d00\\0cc5b647-c1df-4637-891a-dec35c318583", true);
            if (unparkcpu != null)
            {
                unparkcpu.SetValue("ValueMax", "0", RegistryValueKind.String);
                unparkcpu.Close();
            }

            RegistryKey unparkcpu2 = Registry.LocalMachine.OpenSubKey("SYSTEM\\ControlSet001\\Control\\Power\\PowerSettings\\54533251-82be-4824-96c1-47b60b740d00\\0cc5b647-c1df-4637-891a-dec35c318583", true);
            if (unparkcpu2 != null)
            {

                unparkcpu2.SetValue("ValueMin", "0", RegistryValueKind.String);
                unparkcpu2.Close();
            }
        }


        private void wingamemodeon()
        {
            RegistryKey wingamemodedir = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\GameBar", true);
            if (wingamemodedir != null)
            {
                wingamemodedir.CreateSubKey("AutoGameModeEnabled");
                wingamemodedir.SetValue("AutoGameModeEnabled", "1", RegistryValueKind.DWord);
            }

        }
        private void wingamemodeoff()
        {
            RegistryKey wingamemodedir = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\GameBar", true);
            wingamemodedir.SetValue("AutoGameModeEnabled", "0", RegistryValueKind.DWord);


        }

        private void unparkcpudefault()
        {
            RegistryKey unparkcpu = Registry.LocalMachine.OpenSubKey("SYSTEM\\ControlSet001\\Control\\Power\\PowerSettings\\54533251-82be-4824-96c1-47b60b740d00\\0cc5b647-c1df-4637-891a-dec35c318583", true);
            unparkcpu.SetValue("ValueMax", "64", RegistryValueKind.String);
            unparkcpu.Close();

        }


        private void keyboardopt()
        {


            RegistryKey keyboard = Registry.CurrentUser.OpenSubKey("Control Panel\\Accessibility\\Keyboard Response", true);
            keyboard.SetValue("AutoRepeatDelay", "500", RegistryValueKind.String);
            keyboard.SetValue("AutoRepeatRate", "20", RegistryValueKind.String);
            keyboard.SetValue("DelayBeforeAcceptance", "0", RegistryValueKind.String);
            keyboard.SetValue("Flags", "122", RegistryValueKind.String);
            keyboard.Close();

            RegistryKey keyboard2 = Registry.CurrentUser.OpenSubKey("Control Panel\\Accessibility\\StickyKeys", true);
            keyboard2.SetValue("Flags", "506", RegistryValueKind.String);
            keyboard2.Close();

            RegistryKey keyboard3 = Registry.CurrentUser.OpenSubKey("Control Panel\\Accessibility\\ToggleKeys", true);
            keyboard3.SetValue("Flags", "58", RegistryValueKind.String);
            keyboard3.Close();


            RegistryKey keyboard4 = Registry.CurrentUser.OpenSubKey("Control Panel\\Keyboard", true);
            keyboard4.SetValue("KeyboardDelay", "0", RegistryValueKind.String);
            keyboard4.SetValue("KeyboardSpeed", "31", RegistryValueKind.String);
            keyboard4.Close();


        }


        private void keyboarddefaults()
        {
            RegistryKey keyboard = Registry.CurrentUser.OpenSubKey("Control Panel\\Accessibility\\Keyboard Response", true);
            keyboard.SetValue("AutoRepeatDelay", "1000", RegistryValueKind.String);
            keyboard.SetValue("AutoRepeatRate", "500", RegistryValueKind.String);
            keyboard.SetValue("DelayBeforeAcceptance", "1000", RegistryValueKind.String);
            keyboard.SetValue("Flags", "126", RegistryValueKind.String);
            keyboard.Close();


            RegistryKey keyboard2 = Registry.CurrentUser.OpenSubKey("Control Panel\\Accessibility\\StickyKeys", true);
            keyboard2.SetValue("Flags", "510", RegistryValueKind.String);
            keyboard2.Close();

            RegistryKey keyboard3 = Registry.CurrentUser.OpenSubKey("Control Panel\\Accessibility\\ToggleKeys", true);
            keyboard3.SetValue("Flags", "62", RegistryValueKind.String);
            keyboard3.Close();


            RegistryKey keyboard4 = Registry.CurrentUser.OpenSubKey("Control Panel\\Keyboard", true);
            keyboard4.SetValue("KeyboardDelay", "1", RegistryValueKind.String);
            keyboard4.Close();
        }

        private void diagnosis()
        {
            RegistryKey diag = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\DiagTrack", true);
            diag.SetValue("Start", "4", RegistryValueKind.String);
            diag.Close();

            RegistryKey diag2 = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\dmwappushservice", true);
            diag2.SetValue("Start", "4", RegistryValueKind.String);
            diag2.Close();


            RegistryKey diag3 = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\diagsvc", true);
            diag3.SetValue("Start", "4", RegistryValueKind.String);
            diag3.Close();

            RegistryKey diag5 = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\diagnosticshub.standardcollector.service", true);
            diag5.SetValue("Start", "4", RegistryValueKind.String);
            diag5.Close();

        }

        private void diagnosisdefault()
        {
            RegistryKey diag = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\DiagTrack", true);
            diag.SetValue("Start", "2", RegistryValueKind.String);
            diag.Close();

            RegistryKey diag2 = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\dmwappushservice", true);
            diag2.SetValue("Start", "2", RegistryValueKind.String);
            diag2.Close();


            RegistryKey diag3 = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\diagsvc", true);
            diag3.SetValue("Start", "2", RegistryValueKind.String);
            diag3.Close();

            RegistryKey diag5 = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\diagnosticshub.standardcollector.service", true);
            diag5.SetValue("Start", "2", RegistryValueKind.String);
            diag5.Close();
        }

        //Toggle 2 Latencia de teclado 
        private void rjToggleButton1_CheckedChanged(object sender, EventArgs e)
        {

            if (toggle2.Checked)
            {
                keyboardopt();
            }
            else
            {
                keyboarddefaults();
            }
        }

        //Toggle 3 Inicio Rapido
        private void rjToggleButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (toggle3.Checked)
            {
                quickon();
            }
            else
            {
                quickondefault();
            }


        }


        //Toggle 1 Activar nucleos
        private void toggle1_CheckedChanged(object sender, EventArgs e)
        {
            if (toggle1.Checked)
            {
                unparkcpu();
            }
            else
            {
                unparkcpudefault();
            }

        }

        //Toggle 4 Modo de Juego


        private void toggle4_CheckedChanged(object sender, EventArgs e)
        {
            if (toggle4.Checked)
            {
                wingamemodeon();
            }
            else
            {
                wingamemodeoff();
            }
        }

        //toggle 5 Segundo Plano
        private void toggle5_CheckedChanged(object sender, EventArgs e)
        {
            if (toggle5.Checked)
            {
                backgroundapps();
            }
            else
            {
                backgroundappsdefault();
            }
        }

        //Toggle 6 Deshabilitar Diagnosis
        private void toggle6_CheckedChanged(object sender, EventArgs e)
        {
            if (toggle6.Checked)
            {
                diagnosis();
            }
            else
            {
                diagnosisdefault();
            }
        }
        //Toggle 7 GPU Opt
        private void toggle7_CheckedChanged(object sender, EventArgs e)
        {
            if (toggle7.Checked)
            {
                gpuopt();
                label15.Show();
            }
            else
            {
                gpuoptdefault();
                label15.Show();
            }
        }


        //Restablecer ajustes predeterminados
        private void rjButton1_Click(object sender, EventArgs e)
        {

            toggle1.CheckState = CheckState.Unchecked;
            toggle2.CheckState = CheckState.Unchecked;
            toggle3.CheckState = CheckState.Unchecked;
            toggle4.CheckState = CheckState.Unchecked;
            toggle5.CheckState = CheckState.Unchecked;
            toggle6.CheckState = CheckState.Unchecked;
            toggle7.CheckState = CheckState.Unchecked;
            rjToggleButton1.CheckState = CheckState.Unchecked;

        }

        private void rjToggleButton1_CheckedChanged_1(object sender, EventArgs e)
        {
           if (rjToggleButton1.Checked)
            {
                transparenciacolor();
            }
            else
            {
                transparenciacolordefault();
            }
        }
    }
}
