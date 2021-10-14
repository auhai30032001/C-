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
    public partial class Results : Form
    {
        public Results()
        {
            InitializeComponent();
            ShowResult();
            GetPatient();
            GetLab();
            GetTest();
            DateLbl.Text = DateTime.Today.Date.Day.ToString() + "-" + DateTime.Today.Date.Month.ToString() + "-" + DateTime.Today.Date.Year.ToString();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\PC\OneDrive\Documents\MedicalLabDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void ShowResult()
        {
            Con.Open();
            string Query = "Select * from ResultTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            //LabDGV.DataSource = ds.Tables[0];
            ResDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void GetPatientName()
        {
            Con.Open();
            string Query = "Select * from PatientTbl where PatNum=" + PatIdCb.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                PatNameTb.Text = dr["PatName"].ToString();
            }
            Con.Close();
        }

        int Cost;
        private void GetTestName()
        {
            Con.Open();
            string Query = "Select * from TestTbl where TestCode=" + TestId.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                TestNameTb.Text = dr["TestName"].ToString();
                Cost = Convert.ToInt32(dr["TestCost"].ToString());
            }
            Con.Close();
        }

        private void GetLabName()
        {
            Con.Open();
            string Query = "Select * from LaboratorianTbl where LabId=" + LabIdCb.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                LabNameTb.Text = dr["LabName"].ToString();
            }
            Con.Close();
        }
        private void Reset()
        {

        }

        private void GetPatient()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select PatNum from PatientTbl", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("PatNum", typeof(int));
            dt.Load(Rdr);
            PatIdCb.ValueMember = "PatNum";
            PatIdCb.DataSource = dt;
            Con.Close();

        }

        private void GetLab()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select LabId from LaboratorianTbl", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("LabId", typeof(int));
            dt.Load(Rdr);
            LabIdCb.ValueMember = "LabId";
            LabIdCb.DataSource = dt;
            Con.Close();

        }

        private void GetTest()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select TestCode from TestTbl", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("TestCode", typeof(int));
            dt.Load(Rdr);
            TestId.ValueMember = "TestCode";
            TestId.DataSource = dt;
            Con.Close();

        }


        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (TestNameTb.Text == "" || TestTb.Text == "" || PatNameTb.Text == "" || LabIdCb.SelectedIndex == -1 || CostTb.Text == "")
            {
                MessageBox.Show("Missing Information !");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into ResultTbl(PatId,PatName,Laboratorian,LabName,TestDone,ResDate,Test,TestCost) values(@PI,@PN,@Lab,@LN,@TD,@RDate,@TestCode,@TC)", Con);
                    cmd.Parameters.AddWithValue("@PI", PatIdCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@PN", PatNameTb.Text);
                    cmd.Parameters.AddWithValue("@Lab", LabIdCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@LN", LabNameTb.Text);
                    cmd.Parameters.AddWithValue("@TD", TestTb.Text);
                    cmd.Parameters.AddWithValue("@RDate", TDate.Value.Date);
                    cmd.Parameters.AddWithValue("@TestCode",TestId.Text);
                    cmd.Parameters.AddWithValue("@TC", CostTb.Text);


                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Result Saved");

                    Con.Close();
                    ShowResult();
                    Reset();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

            private void PatIdCb_SelectionChangeCommitted(object sender, EventArgs e)
            {
                GetPatientName();
            }

            private void TestId_SelectionChangeCommitted(object sender, EventArgs e)
            {
                GetTestName();
            }

            private void LabIdCb_SelectionChangeCommitted(object sender, EventArgs e)
            {
                GetLabName();
            }

            string tr = "";
            int GrdCost = 0;
            private void button4_Click(object sender, EventArgs e)
            {
                if (LabIdCb.SelectedIndex == -1 || ResCb.SelectedIndex == -1)
                {
                    MessageBox.Show("Select the Test and Result !");
                }
                else
                {
                    tr = tr + TestNameTb.Text + ":" + ResCb.SelectedItem.ToString();
                    TestTb.Text = tr;
                    GrdCost = GrdCost + Cost;
                    CostTb.Text = " " + GrdCost;
                   
                }
            }

            private void pictureBox7_Click(object sender, EventArgs e)
            {
                Application.Exit();
            }

            private void pictureBox3_Click(object sender, EventArgs e)
            {
                Tests obj = new Tests();
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

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Results obj = new Results();
            obj.Show();
            this.Hide();
        }

       
        private void EditBtn_Click(object sender, EventArgs e)
        {

        }

        private void TestTb_TextChanged(object sender, EventArgs e)
        {

        }

        
       
    }
    }

 
