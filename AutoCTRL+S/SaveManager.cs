using AutoCTRL_S.Properties;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Timers;
using System.Windows.Forms;

namespace AutoCTRL_S
{
    public partial class SaveManager : Form
    {
        private NotifyIcon trayIcon;
        public System.Timers.Timer saveTimer;

        public SaveManager()
        {
            InitializeComponent();
            startSaveTimer();

            // Initialize Tray Icon
            trayIcon = new NotifyIcon()
            {
                Icon = Resources.SaveMe,
                ContextMenu = new ContextMenu(new MenuItem[] {
                new MenuItem("Settings", settings), new MenuItem("Exit", exit)
            }),
                Visible = true
            };
        }

        void exit(object sender, EventArgs e)
        {
            // Hide tray icon, otherwise it will remain shown until user mouses over it
            trayIcon.Visible = false;
            Environment.Exit(0);
        }

        void settings(object sender, EventArgs e)
        {
            this.Show();
            WindowState = FormWindowState.Normal;
            saveTimer.Stop();
        }

        private void startSaveTimer()
        {
            saveTimer = new System.Timers.Timer((double)numericUpDown1.Value * 60000);
            saveTimer.Elapsed += saveTimer_Elapsed;
            saveTimer.Start();
        }

        private void saveTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            SendKeys.SendWait("^(s)");

            if (chkNotify.Checked)
            {
                trayIcon.BalloonTipText = "Autosave Successful";
                trayIcon.ShowBalloonTip(100);
            }

            if (chkNotification.Checked) {System.Media.SystemSounds.Exclamation.Play();}
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            trayIcon.BalloonTipTitle = "AutoCTRL+S";
            trayIcon.Text = "AutoCTRL+S";

            //this.WindowState = FormWindowState.Minimized;
            //this.ShowInTaskbar = false;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/matthew17754/AutoCTRL-S");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/matthew17754/AutoCTRL-S");
        }

        private void SaveManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            e.Cancel = true;
            startSaveTimer();
        }

        private void SetStartup()
        {
            RegistryKey rk = Registry.CurrentUser.OpenSubKey
                ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            //if (chkStartUp.Checked)
            //    rk.SetValue("AutoCTRL+S", Application.ExecutablePath);
            //else
            //    rk.DeleteValue("AutoCTRL+S", false);
        }

        private void chkStartUp_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Properties.Settings.Default["MinInterval"] = numericUpDown1.Value;
            //Properties.Settings.Default.Save();
            //Properties.Settings.Default["Startup"] = chkStartUp.Checked;
            //Properties.Settings.Default.Save();
            //Properties.Settings.Default["Notification"] = chkNotification.Checked;
            //Properties.Settings.Default.Save(); // Saves settings in application configuration file

            startSaveTimer();
            this.WindowState = FormWindowState.Minimized;
        }

        private void SaveManager_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                trayIcon.Visible = true;
                trayIcon.BalloonTipText = "Minimized To System Tray";
                trayIcon.ShowBalloonTip(100);
            }
            else if (FormWindowState.Normal == this.WindowState)
            { trayIcon.Visible = false; }
        }
    }
}