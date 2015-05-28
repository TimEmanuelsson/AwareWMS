using AwareServer.Graphic_View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AwareServer
{
    public partial class Menu : ApplicationContext
    {
        private NotifyIcon TrayIcon;
        private ContextMenuStrip TrayIconContextMenu;
        private ToolStripMenuItem CloseMenuItem;
        private ToolStripMenuItem StartServerMenuItem;
        private ToolStripMenuItem StopServerMenuItem;
        private ToolStripMenuItem EditServerMenuItem;
        private Program program;
        private Thread awareThread;
        private bool hasServerBeenStarted = false;
        public Menu()
        {
            Application.ApplicationExit += new EventHandler(this.OnApplicationExit);
            InitializeNotifycon();
            TrayIcon.Visible = true;
            program = new Program();
        }
        private void InitializeNotifycon()
        {
            
            /*
             *Lägg till dessa filerna(Menu) och undermappar
             *resource filen med awareico.ico
             *Start och stopp i async..listener  
             */
            
            TrayIcon = new NotifyIcon();
            TrayIcon.BalloonTipIcon = ToolTipIcon.Info;
            TrayIcon.BalloonTipText = "Server is currently not running";
            TrayIcon.BalloonTipTitle = "Server status";
            TrayIcon.Text = "Aware server";
            

            //Get the icon from resources.
            TrayIcon.Icon = Properties.Resources.AwareIco;
            //Optional - handle doubleclicks on the icon:
            TrayIcon.DoubleClick += TrayIcon_DoubleClick;

            //Optional - Add a context menu to the TrayIcon:
            TrayIconContextMenu = new ContextMenuStrip();
            CloseMenuItem = new ToolStripMenuItem();
            StartServerMenuItem = new ToolStripMenuItem();
            StopServerMenuItem = new ToolStripMenuItem();
            EditServerMenuItem = new ToolStripMenuItem();
            TrayIconContextMenu.SuspendLayout();
            StopServerMenuItem.Visible = false;


            // 
            // TrayIconContextMenu
            // 
            this.TrayIconContextMenu.Items.AddRange(new ToolStripItem[] {
            this.StartServerMenuItem,
            this.StopServerMenuItem,
            this.EditServerMenuItem,
            this.CloseMenuItem});
            this.TrayIconContextMenu.Name = "TrayIconContextMenu";
            this.TrayIconContextMenu.Size = new Size(153, 70);
            // 
            // CloseMenuItem
            // 
            this.CloseMenuItem.Name = "CloseMenuItem";
            this.CloseMenuItem.Size = new Size(152, 22);
            this.CloseMenuItem.Text = "Exit  Aware server";
            this.CloseMenuItem.Click += new EventHandler(this.CloseMenuItem_Click);
            // 
            // StartServer
            // 
            this.StartServerMenuItem.Name = "StartServer";
            this.StartServerMenuItem.Size = new Size(152, 22);
            this.StartServerMenuItem.Text = "Start Aware Server";
            this.StartServerMenuItem.Click += new EventHandler(this.StartAwareMenuItem_Click);
            TrayIconContextMenu.ResumeLayout(false);
            TrayIcon.ContextMenuStrip = TrayIconContextMenu;
            // 
            // StopServer
            // 
            //this.StopServerMenuItem.Name = "StopServer";
            //this.StopServerMenuItem.Size = new Size(152, 22);
            //this.StopServerMenuItem.Text = "Stop Aware Server";
            //this.StopServerMenuItem.Click += new EventHandler(this.StopAwareMenuItem_Click);
            //TrayIconContextMenu.ResumeLayout(false);
            //TrayIcon.ContextMenuStrip = TrayIconContextMenu;
            // 
            // EditServerSettings
            // 
            this.EditServerMenuItem.Name = "EditServerSettings";
            this.EditServerMenuItem.Size = new Size(152, 22);
            this.EditServerMenuItem.Text = "Edit Server Settings";
            this.EditServerMenuItem.Click += new EventHandler(this.EditAwareMenuItem_Click);
            TrayIconContextMenu.ResumeLayout(false);
            TrayIcon.ContextMenuStrip = TrayIconContextMenu;
        }

       
        private void EditAwareMenuItem_Click(object sender, EventArgs e)
        {
            if ((Application.OpenForms["SettingsMenu"] as SettingsMenu) != null)
            {
                //Form is already open
            }
            else
            {
                SettingsMenu settingsMenuForm = new SettingsMenu();
                settingsMenuForm.Show();
            }
        }
        

        private void StartAwareMenuItem_Click(object sender, EventArgs e)
        {
            //START SERVER
            TrayIcon.BalloonTipText = "Server is currently running";
            if (!hasServerBeenStarted)
            {
                hasServerBeenStarted = true;
                StartServerMenuItem.Visible = false;
                //StopServerMenuItem.Visible = true;
                //program.StartServer();
                awareThread = new Thread(new ThreadStart(program.StartServer));
                awareThread.Start();
                TrayIcon.ShowBalloonTip(3, "Aware Status", "Server has started", ToolTipIcon.Info);
            }
        }





        //private void StopAwareMenuItem_Click(object sender, EventArgs e)
        //{
        //    //STOP SERVER
        //    TrayIcon.BalloonTipText = "Server is currently not running";
        //    if (hasServerBeenStarted)
        //    {
        //        hasServerBeenStarted = false;
        //        StartServerMenuItem.Visible = true;
        //        StopServerMenuItem.Visible = false;
        //        //program = new Program();
        //        program.StopServer();
        //        TrayIcon.ShowBalloonTip(3, "Aware Status", "Server has stopped", ToolTipIcon.Info);
        //    }
        //}

        private void OnApplicationExit(object sender, EventArgs e)
        {
            //Cleanup so that the icon will be removed when the application is closed
            TrayIcon.Visible = false;
            Application.Exit();
        }
        private void TrayIcon_DoubleClick(object sender, EventArgs e)
        {
            //Here you can do stuff if the tray icon is doubleclicked
            TrayIcon.ShowBalloonTip(500);
        }

        private void CloseMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to exit Aware server?",
                    "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                //Application.Exit();
                //Application.Exit does not completely remove the process from the system
                System.Environment.Exit(1);
            }
        }
    }
}
