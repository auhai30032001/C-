using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace MedLabTuto
{
    public partial class Tests : Form
    {
        public Tests()
        {
            InitializeComponent();
            ShowTest();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\PC\OneDrive\Documents\MedicalLabDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void ShowTest()
        {
            Con.Open();
            string Query = "Select * from TestTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            TestDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void Reset()
        {
            TestNameTb.Text = "";
            TestCostTb.Text = "";
            Key = 0;
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (TestNameTb.Text == "" || TestCostTb.Text== "")
            {
                MessageBox.Show("Missing Information !");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into TestTbl(TestName,TestCost) values(@TN,@TC)", Con);
                    cmd.Parameters.AddWithValue("@TN",TestNameTb.Text);
                    cmd.Parameters.AddWithValue("@TC",TestCostTb.Text);
                    

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Test Saved");

                    Con.Close();
                    ShowTest();
                    Reset();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (TestNameTb.Text == "" || TestCostTb.Text == "")
            {
                MessageBox.Show("Missing Information !");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update TestTbl Set TestName=@TN,TestCost=@TC where TestCode=@TKey", Con);
                    cmd.Parameters.AddWithValue("@TN", TestNameTb.Text);
                    cmd.Parameters.AddWithValue("@TC", TestCostTb.Text);     
                    cmd.Parameters.AddWithValue("@TKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Test Updated");

                    Con.Close();
                    ShowTest();
                    Reset();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        int Key = 0;
        private void TestDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.TestDGV.Rows[e.RowIndex];
                TestNameTb.Text = TestDGV.Rows[e.RowIndex].Cells[1].Value.ToString();
                TestCostTb.Text = TestDGV.Rows[e.RowIndex].Cells[2].Value.ToString();


            }
            if (TestNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(TestDGV.Rows[e.RowIndex].Cells[0].Value.ToString());
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select the Test !");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from TestTbl where TestCode=@TKey", Con);
                    cmd.Parameters.AddWithValue("@TKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Test Deleted!");

                    Con.Close();
                    ShowTest();
                    Reset();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Results obj = new Results();
            obj.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Patients obj = new Patients();
            obj.Show();
            this.Hide();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Laboratorians obj = new Laboratorians();
            obj.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Tests obj = new Tests();
            obj.Show();
            this.Hide();
        }
    }
}
