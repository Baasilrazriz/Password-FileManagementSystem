using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pms
{
    public partial class signup : Form
    {
        MySqlConnection conn = null;
        MySqlCommand cmd = null;
        MySqlDataReader reader = null;
        string img_loc, gender;
        public signup()
        {
            InitializeComponent();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void signup_Load(object sender, EventArgs e)
        {

        }




        private void signin_btn_Click(object sender, EventArgs e)
        {
            if (fname_c.Text.Equals("") || lname_c.Text.Equals("") || email_c.Text.Equals("") || address_c.Text.Equals("") || phone_c.Text.Equals(""))
            {
                MessageBox.Show("Please fill all the fields");
            }
            else
            {
                tabControl1.SelectedIndex = 1;
            }

        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2;
        }

        private void bunifuGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            Login l = new Login();

            this.Hide();
            l.Show();

            signup s = new signup();
            s.Close();

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void register_btn_Click_1(object sender, EventArgs e)
        {
            if (uname_c.Text.Equals("") || password_c.Text.Equals("") || confirmpass_c.Text.Equals(""))
            {
                MessageBox.Show("Please fill all the fields!!");
            }
            if (confirmpass_c.Text.Equals("") && password_c.Text.Equals(""))
            {
                MessageBox.Show("Password doesnt match!!");
            }
            else
            {
                char gender='M' ;
                if (checkedListBox1.CheckedItems.Equals("Male"))
                {
                    gender='M';
                }
                else if (checkedListBox1.CheckedItems.Equals("Female"))
                {
                    gender = 'F';
                }
                else if (checkedListBox1.CheckedItems.Equals("others"))
                {
                    gender = 'O';
                }
                try
                {
                    conn = new MySqlConnection(@"datasource= localhost; database=pms;port=3306; username = root; password= Zxc121216");
                    conn.Open();
                    string query1 = "INSERT INTO admin(admin.username," +
                        "admin.pass,admin.firstname,admin.lastname,admin.address,admin.sex,admin.email,admin.phonenumber,admin.dob" +
                        ",admin.image) VALUES  ('" + uname_c.Text + "','" + confirmpass_c.Text + "','" + fname_c.Text + "','" + lname_c.Text + "'," +
                        "'" + address_c.Text + "','" + gender + "','" + email_c.Text + "','" + phone_c.Text + "','" + dob_c.Text.ToString() + "','" + null + "')";
                    cmd = new MySqlCommand(query1, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("data is inserted");
                    fname_c.Text = "";
                    lname_c.Text = "";
                    email_c.Text = "";
                    dob_c.ResetText();
                
                    address_c.Text = "";
                    conn.Close();
                    Login l = new Login();

                    this.Hide();
                    l.Show();

                    signup s = new signup();
                    s.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("data is not inserted {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


    }
}
