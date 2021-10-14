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
    public partial class Laboratorians : Form
    {
        public Laboratorians()
        {
            InitializeComponent();
            ShowLaboratorian();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\PC\OneDrive\Documents\MedicalLabDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void ShowLaboratorian()
        {
            Con.Open();
            string Query = "Select * from LaboratorianTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            //LabDGV.DataSource = ds.Tables[0];
            dataGridView1.DataSource = ds.Tables[0];
            Con.Close();
        }
       
        private void Reset()
        {
            LabNameTb.Text = "";
            PhoneTb.Text = "";
            LabAddressTb.Text = "";
            QualCb.SelectedIndex = -1;
            GenCb.SelectedIndex = -1;
            PasswordTb.Text = "";
            Key = 0;
        }
        private void SaveBtn_Click_1(object sender, EventArgs e)
        {
            if (LabNameTb.Text == "" || LabAddressTb.Text == "" || PhoneTb.Text == "" || GenCb.SelectedIndex == -1 || QualCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information !");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into LaboratorianTbl(LabName,LabGender,LabAddress,LabQual,LabPhone,LabDOB,LabPass) values(@LN,@LG,@LA,@LQ,@LP,@LD,@LPa)", Con);
                    cmd.Parameters.AddWithValue("@LN", LabNameTb.Text);
                    cmd.Parameters.AddWithValue("@LG", GenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@LA", LabAddressTb.Text);
                    cmd.Parameters.AddWithValue("@LQ", QualCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@LP", PhoneTb.Text);
                    cmd.Parameters.AddWithValue("@LD", LabDOB.Value.Date);
                    cmd.Parameters.AddWithValue("@LPa", PasswordTb.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Laboratorian Saved");

                    Con.Close();
                    ShowLaboratorian();
                    Reset();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void EditBtn_Click_1(object sender, EventArgs e)
        {
            
                if (LabNameTb.Text == "" || LabAddressTb.Text == "" || PhoneTb.Text == "" || GenCb.SelectedIndex == -1 || QualCb.SelectedIndex == -1)
                {
                    MessageBox.Show("Missing Information !");
                }
                else
                {
                    try
                    {
                        Con.Open();
                        SqlCommand cmd = new SqlCommand("Update LaboratorianTbl Set LabName=@LN, LabGender=@LG,LabAddress=@LA,LabQual=@LQ,LabPhone=@LP,LabDOB=@LD,LabPass=@LPass where LabId=@LKey", Con);
                        cmd.Parameters.AddWithValue("@LN", LabNameTb.Text);
                        cmd.Parameters.AddWithValue("@LG", GenCb.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@LA", LabAddressTb.Text);
                        cmd.Parameters.AddWithValue("@LQ", QualCb.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@LP", PhoneTb.Text);
                        cmd.Parameters.AddWithValue("@LD", LabDOB.Value.Date);
                        cmd.Parameters.AddWithValue("@LPass", PasswordTb.Text);
                        cmd.Parameters.AddWithValue("@LKey", Key);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Laboratorian Updated");

                        Con.Close();
                        ShowLaboratorian();
                    Reset();

                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.Message);
                    }
                }
            }

        int Key = 0;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0) 
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                LabNameTb.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                GenCb.SelectedItem = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                LabAddressTb.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                QualCb.SelectedItem = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                PhoneTb.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                LabDOB.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                PasswordTb.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
               

            }
            if(LabNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            }
           
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if(Key == 0)
            {
                MessageBox.Show("Select the Laboratorian !");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from LaboratorianTbl where LabId=@LKey", Con);
                    cmd.Parameters.AddWithValue("@LKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Laboratorian Deleted!");

                    Con.Close();
                    ShowLaboratorian();
                    Reset();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
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
    }

        
    
    }

