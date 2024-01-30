using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pms
{
    public partial class Login : Form
    {
        public static string admin,id;
        public Login()
        {
            InitializeComponent();
            txtUsername.Text = string.Empty;
            txt_Password.Text = string.Empty;
        }
        public void email(string to)
        {
            try
            {
                string time = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");
                MailMessage mm = new MailMessage();
                SmtpClient sc = new SmtpClient("smtp.gmail.com");
                mm.From = new MailAddress("baasil86805@gmail.com");
                mm.To.Add(to);
                mm.Subject = "PFMS-Login";
                mm.Body = "you have logged in your account at "+time+".\nHope you get the best experience.";
                sc.Port = 587;
                sc.Credentials = new System.Net.NetworkCredential("baasil86805@gmail.com", "fohl fwjq mmsa qsyj");
                sc.EnableSsl = true;
                sc.Send(mm);
             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bunifuCustomLabel4_MouseEnter(object sender, EventArgs e)
        {
            forgot_lbl.ForeColor = Color.Red;
        }
        private void bunifuCustomLabel4_MouseLeave(object sender, EventArgs e)
        {
            forgot_lbl.ForeColor = Color.Black;
        }

        public void signin_btn_Click(object sender, EventArgs e)
        {
            string emailaddress;
            MySqlConnection con = new MySqlConnection(@"datasource= localhost; database=pms;port=3306; username = root; password= Zxc121216");
            con.Open();
            string query = "SELECT * FROM admin WHERE username='"+txtUsername.Text+"'AND pass='"+txt_Password.Text+"'";
            
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                admin = txtUsername.Text;
                Select ds = new Select();
                emailaddress = reader.GetString("email");
                id = reader.GetString("admin_id");
                this.Hide();
                ds.Show();
                email(emailaddress);
                Login l = new Login();
                l.Close();

            }
            else
            {
                MessageBox.Show("Username And Password Not Match!", "VINSMOKE MJ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            txtUsername.Text = string.Empty;
            txt_Password.Text = string.Empty;
            reader.Close();
     
            con.Close(); // always close connection }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void txtUsername_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void forgot_lbl_Click(object sender, EventArgs e)
        {
            Forgot f = new Forgot();

            this.Hide();
            f.Show();

            Login l = new Login();
            l.Close();

        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            signup f = new signup();

            this.Hide();
            f.Show();

            Login l = new Login();
            l.Close();

        }

        private void hide_btn_Click(object sender, EventArgs e)
        {
            show_btn.Visible = true;
            hide_btn.Visible = false;
            
                show_btn.BringToFront();
                txt_Password.isPassword = true;

            
        }

        private void show_btn_Click(object sender, EventArgs e)
        {
            
            hide_btn.Visible = true;
            show_btn.Visible = false;
            
                hide_btn.BringToFront();
                txt_Password.isPassword = false;

            
        }
    }
}
