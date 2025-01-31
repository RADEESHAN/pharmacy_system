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
    public partial class Customer : Form
    {
        public Customer()
        {
            InitializeComponent();
            ShowCust();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=PRAMUDITHA;Initial Catalog=PharmacyDB;Integrated Security=True");
        private void ShowCust()
        {
            Con.Open();
            string Query = "Select * from CustomerTb1";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            CustomerDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuThinButton22_Click(object sender, EventArgs e) //Cus Edit button
        {
            if (CustNameTb.Text == "" || CusPhoneTb.Text == "" || CusPhoneTb.Text == "" || CusGenCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update CustomerTb1 Set CustName=@CN,CustPhone=@CP,CustAdd=@CA,CustDOB=@CD,CustGen=@CG where CustNum=@CKey", Con);
                    cmd.Parameters.AddWithValue("@CN", CustNameTb.Text);
                    cmd.Parameters.AddWithValue("@CP", CusPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@CA", CustAddTb.Text);
                    cmd.Parameters.AddWithValue("@CD", CusDOB.Value.Date);
                    cmd.Parameters.AddWithValue("@CG", CusGenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@CKey", key);


                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Manufacturer Updated");
                    Con.Close();
                    ShowCust();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }

        private void CustAddTb_TextChanged(object sender, EventArgs e)//CustAddTb
        {

        }

        private void CusGenCb_SelectedIndexChanged(object sender, EventArgs e)//CusGenCb
        {

        }

        private void CusDOB_onValueChanged(object sender, EventArgs e)//CusDOB
        {

        }
        private void Reset()
        {
            CustNameTb.Text = "";
            CusPhoneTb.Text = "";
            CusGenCb.SelectedIndex = 0;
            CustAddTb.Text = "";

        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)// customer save button
        {
            if (CustNameTb.Text == "" || CusPhoneTb.Text == "" || CusPhoneTb.Text == ""||CusGenCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into CustomerTb1(CustName,CustPhone,CustAdd,CustDOB,CustGen)values(@CN,@CP,@CA,@CD,@CG)", Con);
                    cmd.Parameters.AddWithValue("@CN", CustNameTb.Text);
                    cmd.Parameters.AddWithValue("@CP", CusPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@CA", CustAddTb.Text);
                    cmd.Parameters.AddWithValue("@CD", CusDOB.Value.Date);
                    cmd.Parameters.AddWithValue("@CG", CusGenCb.SelectedItem.ToString());

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Manufacturer Added");
                    Con.Close();
                    ShowCust();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }
        int key = 0;
        private void CustomerDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CustNameTb.Text = CustomerDGV.SelectedRows[0].Cells[1].Value.ToString();
            CusPhoneTb.Text = CustomerDGV.SelectedRows[0].Cells[2].Value.ToString();
            CustAddTb.Text = CustomerDGV.SelectedRows[0].Cells[3].Value.ToString();
            CusDOB.Text = CustomerDGV.SelectedRows[0].Cells[4].Value.ToString();
            CusGenCb.SelectedItem = CustomerDGV.SelectedRows[0].Cells[5].Value.ToString();

            if (CustNameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(CustomerDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void bunifuThinButton23_Click(object sender, EventArgs e) // cutomer delete button
        {
            if (key == 0)
            {
                MessageBox.Show("Select the Customer");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from CustomerTb1 where CustNum=@CKey", Con);
                    cmd.Parameters.AddWithValue("@CKey", key);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Deleted");
                    Con.Close();
                    ShowCust();
                    Reset();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }
    }
}
