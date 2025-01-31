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
    public partial class Sellers : Form
    {
        public Sellers()
        {
            InitializeComponent();
            ShowSeller();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=PRAMUDITHA;Initial Catalog=PharmacyDB;Integrated Security=True");
        private void ShowSeller()
        {
            Con.Open();
            string Query = "Select * from SellerTb1";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            SellersDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        private void Reset()
        {
            SNameTb.Text = "";
            SPhoneTb.Text = "";
            SAddTb.Text = "";
            SPasswordTb.Text = "";
            SGenCb.SelectedIndex= 0;
            key = 0;
        }

        private void SaveBtn_Click(object sender, EventArgs e)// save button

        {
            if (SNameTb.Text == "" || SPhoneTb.Text == "" || SPasswordTb.Text == "" || SGenCb.SelectedIndex == -1 || SAddTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into SellerTb1(SName,SDOB,Sphone,SAdd,SGen,SPass)values(@SN,@SD,@SP,@SA,@SG,@SPA)", Con);
                    cmd.Parameters.AddWithValue("@SN", SNameTb.Text);
                    cmd.Parameters.AddWithValue("@SD", SDOB.Value.Date);
                    cmd.Parameters.AddWithValue("@SP", SPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@SA", SAddTb.Text);
                    cmd.Parameters.AddWithValue("@SG", SGenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@SPA", SPasswordTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Seller Added");
                    Con.Close();
                    ShowSeller();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }

        private void EditBtn_Click(object sender, EventArgs e)//edit button
        {
            if (SNameTb.Text == "" || SPhoneTb.Text == "" || SPasswordTb.Text == "" || SGenCb.SelectedIndex == -1 || SAddTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update SellerTb1 Set SName=@SN,SDOB=@SD,Sphone=@SP,SAdd=@SA,SGen=@SG,SPass=@SPA where SNum =@SKey", Con);
                    cmd.Parameters.AddWithValue("@SN", SNameTb.Text);
                    cmd.Parameters.AddWithValue("@SD", SDOB.Value.Date);
                    cmd.Parameters.AddWithValue("@SP", SPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@SA", SAddTb.Text);
                    cmd.Parameters.AddWithValue("@SG", SGenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@SPA", SPasswordTb.Text);
                    cmd.Parameters.AddWithValue("@SKey", key);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Seller Updated");
                    Con.Close();
                    ShowSeller();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }

        }

        private void DeleteBtn_Click(object sender, EventArgs e)// delete button
        {
            if (key == 0)
            {
                MessageBox.Show("Select the Seller");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from SellerTb1 where SNum=@SKey", Con);
                    cmd.Parameters.AddWithValue("@SKey", key);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Seller Deleted");
                    Con.Close();
                    ShowSeller();
                    Reset();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }
        int key = 0;
        private void SellersDGV_CellContentClick(object sender, DataGridViewCellEventArgs e) //grid view
        {
            SNameTb.Text = SellersDGV.SelectedRows[0].Cells[1].Value.ToString();
            SDOB.Text = SellersDGV.SelectedRows[0].Cells[2].Value.ToString();
            SPhoneTb.Text = SellersDGV.SelectedRows[0].Cells[3].Value.ToString();
            SAddTb.Text = SellersDGV.SelectedRows[0].Cells[4].Value.ToString();
            SGenCb.SelectedValue = SellersDGV.SelectedRows[0].Cells[5].Value.ToString();
            SPasswordTb.Text = SellersDGV.SelectedRows[0].Cells[6].Value.ToString();
            if (SNameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(SellersDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }
    }
}
