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
    public partial class Patients : Form
    {
        public Patients()
        {
            InitializeComponent();
            ShowPatients();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\PC\OneDrive\Documents\MedicalLabDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void ShowPatients()
        {
            Con.Open();
            string Query = "Select * from PatientTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            //LabDGV.DataSource = ds.Tables[0];
            PatientsDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void Reset()
        {
            PatNameTb.Text = "";
            PatPhoneTb.Text = "";
            PatAddressTb.Text = "";
            GenCb.SelectedIndex = -1;
            Key = 0;
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (PatNameTb.Text == "" || PatPhoneTb.Text == "" || PatAddressTb.Text == "" || GenCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information !");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into PatientTbl(PatName,PatGen,PatAdd,PatPhone,PatDOB) values(@PN,@PG,@PA,@PP,@PD)", Con);
                    cmd.Parameters.AddWithValue("@PN",PatNameTb.Text);
                    cmd.Parameters.AddWithValue("@PG", GenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@PA",PatAddressTb.Text);
                    cmd.Parameters.AddWithValue("@PP", PatPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@PD", PatDOB.Value.Date);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show( "Patient Saved");

                    Con.Close();
                    ShowPatients();
                    Reset();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        int Key = 0;
        private void PatientsDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.PatientsDGV.Rows[e.RowIndex];
                PatNameTb.Text = PatientsDGV.Rows[e.RowIndex].Cells[1].Value.ToString();
                GenCb.SelectedItem = PatientsDGV.Rows[e.RowIndex].Cells[2].Value.ToString();
                PatAddressTb.Text = PatientsDGV.Rows[e.RowIndex].Cells[3].Value.ToString();
                PatPhoneTb.Text = PatientsDGV.Rows[e.RowIndex].Cells[4].Value.ToString();
                PatDOB.Text = PatientsDGV.Rows[e.RowIndex].Cells[5].Value.ToString();

            }
            if (PatNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(PatientsDGV.Rows[e.RowIndex].Cells[0].Value.ToString());
            }

        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (PatNameTb.Text == "" || PatPhoneTb.Text == "" || PatAddressTb.Text == "" || GenCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information !");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update PatientTbl Set PatName=@PN,PatGen=@PG,PatAdd=@PA,PatPhone=@PP,PatDOB=@PD where PatNum=@PKey", Con);
                    cmd.Parameters.AddWithValue("@PN", PatNameTb.Text);
                    cmd.Parameters.AddWithValue("@PG", GenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@PA", PatAddressTb.Text);
                    cmd.Parameters.AddWithValue("@PP", PatPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@PD", PatDOB.Value.Date);
                    cmd.Parameters.AddWithValue("@PKey", Key);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show( "Patient Updated");

                    Con.Close();
                    ShowPatients();
                    Reset();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select the Patient !");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from PatientTbl where PatNum=@PKey", Con);
                    cmd.Parameters.AddWithValue("@PKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show( "Patient Deleted!");

                    Con.Close();
                    ShowPatients();
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

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Results obj = new Results();
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
    }
    }

