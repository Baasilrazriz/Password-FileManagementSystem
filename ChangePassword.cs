using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pms
{
    public partial class ChangePassword : Form
    {
        public ChangePassword()
        {
            InitializeComponent();
        }

        private void bunifuCustomLabel3_Click(object sender, EventArgs e)
        {

        }

        private void signin_btn_Click(object sender, EventArgs e)
        {
            if (txt_Password_A.Text == ""||txt_newpassword.Text.Equals("")||txt_confirmpass.Text.Equals(""))
            {
                MessageBox.Show("please enter the password ");
            }
            if (txt_newpassword.Text != txt_confirmpass.Text)
            {
                MessageBox.Show("password doesnt match");
            }
            else
            {
                MySqlConnection con = new MySqlConnection(@"datasource= localhost; database=pms;port=3306; username = root; password= Zxc121216");
                con.Open();
                string query = "SELECT * FROM admin WHERE username='" + Login.admin + "'AND pass='" + txt_Password_A.Text + "'";

                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    reader.Close();
                    string query1 = "UPDATE admin SET pass= '" + txt_newpassword.Text + "' WHERE username='" + Login.admin + "'";
                    
                    MySqlCommand cmd1 = new MySqlCommand(query1, con);
                    MySqlDataReader reader1 = cmd1.ExecuteReader();
                    
                    if (reader1.Read())
                    {
                    }
                    MessageBox.Show("Password is successfully changed");
                    this.Hide();
                    ChangePassword a = new ChangePassword();
                    a.Close();


                    reader1.Close();
                }

                else
                {
                    MessageBox.Show("Old password is wrong");
                }
                txt_Password_A.Text = string.Empty;
                con.Close();
            }
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            this.Hide();  
            ChangePassword a = new ChangePassword();
            a.Close();


        }
    }
}
