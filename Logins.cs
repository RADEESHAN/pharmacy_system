using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pharmacy_system
{
    public partial class Logins : Form
    {
        public Logins()
        {
            InitializeComponent();
        }

        private void Logins_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void CustNameTb_TextChanged(object sender, EventArgs e)
        {

        }
        SqlConnection Con = new SqlConnection(@"Data Source=PRAMUDITHA;Initial Catalog=PharmacyDB;Integrated Security=True");

        private void SaveBtn_Click(object sender, EventArgs e)//login button
        {
            if (UnameTb.Text=="" || PasswordTb.Text == "")
            {
                MessageBox.Show("Enter Both UserName and Password");
            }
            else
            {
                Con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("Seect Count(*) from SellerTb1 where SName='"+UnameTb.Text+"' and SPass='" + PasswordTb.Text + "'", Con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows[0][0].ToString() == "1")
                {
                    Dashboard obj = new Dashboard();
                    obj.Show();
                    this.Hide();
                    Con.Close();

                }
                else
                {
                    MessageBox.Show("wromh userName or Password");
                }
                Con.Close();

            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            AdminLogin obj = new AdminLogin();
            obj.Show();
            this.Hide();
        }
    }
}
