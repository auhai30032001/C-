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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\PC\OneDrive\Documents\MedicalLabDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void label4_Click(object sender, EventArgs e)
        {
            AdmLogin obj = new AdmLogin();
            obj.Show();
            this.Hide();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            if(UnameTb.Text == "" || PasswordTb.Text == "")
            {
                MessageBox.Show("Enter username and password !"  );
            }
            else
            {
                Con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("Select count(*) from LaboratorianTbl where LabName='" + UnameTb.Text + "' and LabPass='" + PasswordTb.Text + "'", Con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if(dt.Rows[0][0].ToString() == "1")
                {
                    Laboratorians obj = new Laboratorians();
                    obj.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Wrong username or password !");
                }
                Con.Close();
            }
        }

        private void AppearBtn_Click(object sender, EventArgs e)
        {
            
            if (PasswordTb.PasswordChar == '\0')
            {
                HideBtn.BringToFront();
                PasswordTb.PasswordChar = '*';
            }
        }

        private void HideBtn_Click(object sender, EventArgs e)
        {
            if (PasswordTb.PasswordChar == '*')
            {
                AppearBtn.BringToFront();
                PasswordTb.PasswordChar = '\0';
            }
        }
    }
}
