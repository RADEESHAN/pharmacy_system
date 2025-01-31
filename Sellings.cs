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
    public partial class Sellings : Form
    {
        public Sellings()
        {
            InitializeComponent();
            ShowMed();
            ShowBill();
         

        }
        SqlConnection Con = new SqlConnection(@"Data Source=PRAMUDITHA;Initial Catalog=PharmacyDB;Integrated Security=True");
        private void UpdateQty() //update medicine stock while billing
        {
          
                try
                {   
                    int NewQty = Stock - Convert.ToInt32(MedQtyTb.Text);
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update MedicineTb1 Set MedQty=@MQ where MedNum=@MKey", Con);
                    cmd.Parameters.AddWithValue("@MQ", NewQty);
                
                    cmd.Parameters.AddWithValue("@MKey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Medicine Updated");
                    Con.Close();
                    ShowMed();
                  //  Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        private void insertBill()
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("insert into BillTb1(SName,CustNum,CustName,BDate,BAmount)values(@UN,@CN,@CNA,@BD,@BA)", Con);
                cmd.Parameters.AddWithValue("@UN", SnameLbl.Text);
                cmd.Parameters.AddWithValue("@CN", CustIdcb.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@CNA", CustNameTb.Text);
                cmd.Parameters.AddWithValue("@BD", DateTime.Today.Date);
                cmd.Parameters.AddWithValue("@BA", GrdTotal);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Bill Saved");
                Con.Close();
                ShowBill();
              
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        private void ShowBill()
        {
            Con.Open();
            string Query = "Select * from BillTb1 where Sname='"+SnameLbl.Text+"'";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            TransactionDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void ShowMed()
        {
            Con.Open();
            string Query = "Select * from MedicineTb1";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            MedicinesDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void pictureBox21_Click(object sender, EventArgs e)
        {

        }
        int n = 0,GrdTotal=0;
        private void SaveBtn_Click(object sender, EventArgs e) // Add to 
        {
            if(MedQtyTb.Text == ""|| Convert.ToInt32(MedQtyTb.Text)> Stock)
            {
                MessageBox.Show("Enter Correct Quantity");
            }else
            {
                int total = Convert.ToInt32(MedQtyTb.Text) *  Convert.ToInt32(MedPriceTb.Text);
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(BillDGV);
                newRow.Cells[0].Value = n+1;
                newRow.Cells[1].Value = MedNameTb.Text;
                newRow.Cells[2].Value = MedQtyTb.Text;
                newRow.Cells[3].Value = MedPriceTb.Text;
                newRow.Cells[4].Value = total;
               BillDGV.Rows.Add(newRow);
                GrdTotal = GrdTotal + total;
   

                TotalLbl.Text = "RS  " + GrdTotal;

                n++;
                UpdateQty();
               // 

            }
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }
        int key = 0,Stock;

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        int MedId, Medprice, MedQty, MedTot;
        int pos = 60;

        string MedName;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)////////////////////////////
        {
            e.Graphics.DrawString("CSpase Pharmacy", new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Red, new Point(80));
            e.Graphics.DrawString("ID MEDICINE PRICE QUANTITY TOTAL", new Font("Century Gothic", 10, FontStyle.Bold), Brushes.Red, new Point(26, 40));
                     foreach (DataGridViewRow row in BillDGV.Rows)
            {
                MedId = Convert.ToInt32(row.Cells["Column1"].Value);
                MedName = "" + row.Cells["Column2"].Value;
                Medprice = Convert.ToInt32(row.Cells["Column3"].Value);
                MedQty = Convert.ToInt32(row.Cells["Column4"].Value);
                MedTot = Convert.ToInt32(row.Cells["Column5"].Value);

                e.Graphics.DrawString("" + MedId, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(26, pos));
                e.Graphics.DrawString("" + MedName, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(45, pos));
                e.Graphics.DrawString("" + Medprice, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(126, pos));
                e.Graphics.DrawString("" + MedQty, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(170, pos));
                e.Graphics.DrawString("" + MedTot, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(235, pos));
                pos = pos + 20;
            }
            e.Graphics.DrawString("Grand Total:Rs " + GrdTotal, new Font("Century Gothic", 10, FontStyle.Bold), Brushes.Crimson, new Point(50, pos + 50));

            e.Graphics.DrawString("***********MCS Pharmacy**********", new Font("Century Gothic", 10, FontStyle.Bold), Brushes.Crimson, new Point(10, pos + 85));

           // BillGDV.Rows.Clear(); ///////////////////////////////////////////////////////////////////////////////////////////
           // BillGDV.Rows.Refresh();//////////////////////////// error here i need to add these two codes
            pos = 100;
            GrdTotal = 0;
            n = 0;



        }

        private void EditBtn_Click(object sender, EventArgs e)//print btn
        {
            

        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }

        private void PrintBtn_Click(object sender, EventArgs e)// print button
        {
           
            printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 285, 600);
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
            insertBill();
        }

        private void BillDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void MedicinesDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            MedNameTb.Text = MedicinesDGV.SelectedRows[0].Cells[1].Value.ToString();
           // MedTypeCb.SelectedItem = MedicinesDGV.SelectedRows[0].Cells[2].Value.ToString();
            Stock = Convert.ToInt32(MedicinesDGV.SelectedRows[0].Cells[3].Value.ToString());
            MedPriceTb.Text = MedicinesDGV.SelectedRows[0].Cells[4].Value.ToString();
           // MedManCb.SelectedValue = MedicinesDGV.SelectedRows[0].Cells[5].Value.ToString();
          //  MedManTb.Text = MedicinesDGV.SelectedRows[0].Cells[6].Value.ToString();
            if (MedNameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(MedicinesDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }
    }
}
