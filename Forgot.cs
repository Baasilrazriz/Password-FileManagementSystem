using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace Pms
{
    public partial class Forgot : Form
    {
        System.Timers.Timer t;
        int h, s, m;
        string phone, emailaddress, username, otp_code;
        public Forgot()
        {
            InitializeComponent();
            txt_email_f.Hide();
            txt_email_f.Enabled = false;
            resend_btn.Enabled = false;
        }
     
        public void time()
        {
            t=new System.Timers.Timer();
            t.Interval = 1000;
            t.Elapsed += OnTimeEvent;
            t.Start();
         
        }

        private void OnTimeEvent(object sender,System.Timers.ElapsedEventArgs e)
        {
            Invoke(new Action(() =>
            {
                s++;
                if (s == 60)
                {
                    t.Stop();
                    resend_btn.ForeColor = Color.Black;
                    resend_btn.Enabled = true;
                    guna2GradientButton3.Enabled = false;
                    label15.Text = "00:00";
                }
                label15.Text = m.ToString().PadLeft(2, '0') + ":" + s.ToString().PadLeft(2, '0');
              
            }));
            t.AutoReset = true;

        }

        public string otp()
        {
            int len = 4;
            const string ValidChar = "1234567890";
            StringBuilder result = new StringBuilder();
            Random rand = new Random();
            while (0 < len--)
            {
                result.Append(ValidChar[rand.Next(ValidChar.Length)]);

            }
            return result.ToString();
        }

        public void email(string to)
        {
            try
            {
                otp_code = otp();
                MailMessage mm = new MailMessage();
                SmtpClient sc = new SmtpClient("smtp.gmail.com");
                mm.From = new MailAddress("baasil86805@gmail.com");
                mm.To.Add(to);
                mm.Subject = "PFMS-RESET YOUR PASSWORD";
                mm.Body = "here is your otp:" + otp_code + ".Now you can reset your password easily\nif any query contact us on bah@gmail.com";
                sc.Port = 587;
                sc.Credentials = new System.Net.NetworkCredential("baasil86805@gmail.com", "fohl fwjq mmsa qsyj");
                sc.EnableSsl = true;
                sc.Send(mm);
                MessageBox.Show("sent successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void resend_btn_Click(object sender, EventArgs e)
        {
            tabControl_f.SelectedIndex = 2;
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            txt_email_f.Show();
        }

        private void radio_email_CheckedChanged(object sender, EventArgs e)
        {
            txt_email_f.Hide();
        }

        private void guna2GradientButton5_Click(object sender, EventArgs e)
        {
            Login l = new Login();

            this.Hide();
            l.Show();

            Forgot s = new Forgot();
            s.Close();

        }

        private void signin_btn_Click(object sender, EventArgs e)
        { 
 
            if (txt_username_f.Text == "")
            {
                MessageBox.Show("please enter the username first!!");
            }
            else
            {
                username = txt_username_f.Text;
                MySqlConnection con = new MySqlConnection(@"datasource= localhost; database=pms;port=3306; username = root; password= Zxc121216");
                con.Open();
                string query = "SELECT * FROM admin WHERE username='" + txt_username_f.Text + "'";

                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    emailaddress = reader.GetString("email");
                    string num = reader.GetString("phonenumber");
                    phone = num.ToString().Substring(1);
                    tabControl_f.SelectedIndex = 1;
                    
                       
                }
                else
                {
                    MessageBox.Show("Username not found!", "VINSMOKE MJ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                txt_username_f.Text = string.Empty;

                reader.Close();

                con.Close();
            }
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            if (combo_forgot.SelectedIndex == -1)
            {
                MessageBox.Show("select a medium first");
            }
            else

            {

                if (combo_forgot.SelectedIndex == 0)
                {
                    radio_email.Text = emailaddress;
                    radioButton2.Text = "Enter another email";
                    tabControl_f.SelectedIndex = 2;


                }
                else if (combo_forgot.SelectedItem.ToString().Equals("WHATSAPP"))
                {
                    radio_email.Text = "+92"+phone;
                    radioButton2.Text = "Enter another number";
                    tabControl_f.SelectedIndex = 2;

                }

            }
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            if (radio_email.Checked == false && radioButton2.Checked == false)
            {
                MessageBox.Show("please select any option");
            
            }
            else
            {
            
                otp_code = otp();

                if (combo_forgot.SelectedItem.ToString().Equals("EMAIL"))
                {
                    if (radio_email.Checked == true)
                    {
                        txt_email_f.Enabled = false;
                        email(emailaddress);
                        tabControl_f.SelectedIndex = 3;
                    }
                    else if (radioButton2.Checked == true)
                    {
                        txt_email_f.Enabled=true;
                        if (txt_email_f.Text == "")
                        {
                            MessageBox.Show("enter email address to get otp!");
                        }
                        else
                        {
                            email(txt_email_f.Text);
                            tabControl_f.SelectedIndex = 3;
                        }
                    }

                }
                else if (combo_forgot.SelectedItem.ToString() == "WHATSAPP")
                {
                    radio_email.Text = phone;
                    radioButton2.Text = "Enter another number";
                    if (radio_email.Checked == true)
                    {
                        txt_email_f.Enabled = false;
                        watsapp.send(otp_code, "+92"+phone);
                        tabControl_f.SelectedIndex = 3;
                    }
                    else if (radioButton2.Checked == true)
                    {
                        txt_email_f.Enabled = true;
                        if (txt_email_f.Text == "")
                        {
                            MessageBox.Show("enter email address to get otp!");
                        }

                        watsapp.send(otp_code, txt_email_f.Text);

                        tabControl_f.SelectedIndex = 3;

                    }
                }
                time();
              
            }
        }

        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            if(txt_otp.Text=="")
            {
                MessageBox.Show("please enter the otp u recieved");
            }
            if (txt_otp.Text.Equals(otp_code))
            {
                tabControl_f.SelectedIndex = 4;
            }
            else
            {
                MessageBox.Show("otp is not valid");
            }
        }

        private void guna2GradientButton4_Click(object sender, EventArgs e)
        {
            if (txt_newpass.Text == "" || txt_confirmpass.Text == "")
            {
                MessageBox.Show("kindly enter your new password");
            }
            else
            {
                MySqlConnection conn = new MySqlConnection(@"datasource= localhost; database=pms;port=3306; username = root; password= Zxc121216");
                if (txt_newpass.Text.Equals(txt_confirmpass.Text))
                {
                    try
                    {



                        conn.Open();

                        try
                        {

                            string query2 = "UPDATE admin  SET pass='" + txt_newpass.Text + "' WHERE username='" + username + "'";
                            MySqlCommand cmd = new MySqlCommand(query2, conn);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("data is updated");
                            tabControl_f.SelectedIndex = 5;
                        }

                        catch (Exception ex)
                        {
                            MessageBox.Show(string.Format("data is not deleted {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(string.Format("database not connected {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("password doesnt match");
                }
            }
        }


    }
}
