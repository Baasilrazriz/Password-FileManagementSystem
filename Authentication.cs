using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Pms
{
    public partial class Authentication : Form
    {
        public Authentication()
        {
            InitializeComponent();
        }
        public bool val;
        public string auth;
        private void signin_btn_Click(object sender, EventArgs e)
        {
            if (txt_Password_A.Text == "")
            {
                MessageBox.Show("please enter the password ");
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


  
                    this.Hide();
                    Authentication a = new Authentication();
                    a.Close();

                }
                else
                {
                    MessageBox.Show("failed");
                }
                txt_Password_A.Text = string.Empty;
                reader.Close();
                con.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            show_btn.Visible = true;
            button1.Visible = false;

            show_btn.BringToFront();
            txt_Password_A.isPassword = true;

        }

        private void show_btn_Click(object sender, EventArgs e)
        {

            button1.Visible = true;
            show_btn.Visible = false;

            button1.BringToFront();
            txt_Password_A.isPassword = false;


        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Authentication a = new Authentication();
            a.Close();

        }
    }
}
