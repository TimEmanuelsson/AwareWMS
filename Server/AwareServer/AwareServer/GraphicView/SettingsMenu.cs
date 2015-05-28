using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AwareServer.Graphic_View
{
    public partial class SettingsMenu : Form
    {
        private Settings set;
        public SettingsMenu()
        {

            InitializeComponent();
            //Get default settings for awareserver
            set  = Settings.Default;
            
            //Assigning old settings to appropriate textboxes
            Username.Text = set.Username;
            Password.Text = set.Password;
            IP.Text = set.IpAddress;
            Port.Text = set.Port.ToString();
        }


        private void okbutton_Click(object sender, EventArgs e)
        {

            set.Username = Username.Text;
            set.Password = Password.Text;
            set.IpAddress = IP.Text;
            set.Port = Convert.ToInt32(Port.Text);

            foreach (Control control in Controls)
            {
                if (control.GetType().ToString() == "System.Windows.Forms.TextBox")
                {
                    if(control.Text ==""){
                        control.BackColor = Color.Red;
                        MessageBox.Show(control.Name + " Cannot be empty");
                    }
                    else
                    {
                        control.BackColor = Color.White;
                        
                    }
                }
            }
            if (set.Username != "" && set.Password != "" && set.IpAddress != "" && set.Port.ToString() != "")
            {
                set.Save();
                Application.Restart();    
            }
        }
    }
}
