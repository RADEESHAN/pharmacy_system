using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pharmacy_system
{
    public partial class AdminLogin : Form
    {
        public AdminLogin()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Logins Obj = new Logins();
            Obj.Show();
            this.Hide();
        }

        private void SaveBtn_Click(object sender, EventArgs e) //login button
        {
            if (AdminPassTb.Text == "")
            {
                
            }else if (AdminPassTb.Text == "Admin")
            {
                Sellers Obj = new Sellers();
                Obj.Show();
                this.Hide();

            }
            else
            {
                MessageBox.Show("Wrong Admin Password");
                AdminPassTb.Text = "";
            }
            
        }
    }
}
