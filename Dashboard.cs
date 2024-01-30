using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Pms
{
	
    public partial class Dashboard : Form
    {
        MySqlConnection conn = null;
        MySqlCommand cmd = null, cmd1 = null;
        MySqlDataReader reader = null;
        
        public Dashboard()
        {
           
            InitializeComponent();
            tabControl.SelectedIndex = 0;
            timer1.Start();
            panel2.Hide();
        }
        string hash = "";
        public string encryption(string x)
        {
            string encrypt;
            byte[] data = UTF8Encoding.UTF8.GetBytes(x);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));//Get hash key
                //Encrypt data by hash key
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateEncryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    encrypt = Convert.ToBase64String(results, 0, results.Length);
                }
            }
            return encrypt;
        }
        public string decryption(string y)
        {
            string decrypt;
            //Convert a string to byte array
            byte[] data = Convert.FromBase64String(y);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));//Get hash key
                //Decrypt data by hash key
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateDecryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    decrypt = UTF8Encoding.UTF8.GetString(results);
                }
            }
            return decrypt;
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            txtAddURL.Text = "";
            txtAddWeb.Text = "";
            txtAddUserName.Text = "";
            txt_email.Text = "";
            txt_pass.Text = "";
            txt_notes.Text = "";
            category.SelectedIndex = -1;
            tabControl.SelectedIndex = 1;
            panel2.Height = btnadd.Height;
            panel2.Top = btnadd.Top;
            panel2.Show();
            panel3.Hide();
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            txt_accname_u.Enabled = false;
            txt_uname_u.Enabled = false;

            txt_email_u.Enabled = false;
            txt_notes_u.Enabled = false;
            txt_cat_u.Enabled = false;

            txt_pass_u.Enabled = false;
            txt_url_u.Enabled = false;

            txt_accname_u.Text = "";
            txt_uname_u.Text = "";
            txt_acc_u.Text = "";
            txt_email_u.Text = "";
            txt_notes_u.Text = "";
            txt_cat_u.Text = "";
            txt_username_u.Text = "";
            txt_pass_u.Text = "";
            txt_url_u.Text = "";
            cat_u.SelectedIndex = -1;
            list_u.Items.Clear();

            tabControl.SelectedIndex = 3;
            panel2.Height = btnupdate.Height;
            panel2.Top = btnupdate.Top;
            panel2.Show();
            panel3.Hide();
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            txt_acc_d.Enabled = false;
            txt_email_d.Enabled = false;
            txt_notes_d.Enabled = false;
            txt_cat_d.Enabled = false;
            txt_username_d.Enabled = false;
            txt_pass_d.Enabled = false;
            txt_url_d.Enabled = false;

            txt_accname_d.Text = "";
            txt_uname_d.Text = "";
            txt_acc_d.Text = "";
            txt_email_d.Text = "";
            txt_notes_d.Text = "";
            txt_cat_d.Text = "";
            txt_username_d.Text = "";
            txt_pass_d.Text = "";
            txt_url_d.Text = "";
            cat_d.SelectedIndex = -1;

            listBox_d.Items.Clear();
            tabControl.SelectedIndex = 2;
            panel2.Height = btndelete.Height;
            panel2.Top = btndelete.Top;
            panel2.Show();
            panel3.Hide();
        }

        private void btnview_Click(object sender, EventArgs e)
        {
            //dataGridView1.Enabled = false;
            tabControl.SelectedIndex = 5;
            panel2.Height = btnview.Height;
            panel2.Top = btnview.Top;
            panel2.Show();
            panel3.Hide();
            dataGridView1.Rows.Clear();
            try
            {
                string query = "SELECT * FROM passwords WHERE admin_id='"+Login.id+"'";
                conn = new MySqlConnection(@"datasource= localhost; database=pms;port=3306; username = root; password= Zxc121216");
                cmd = new MySqlCommand(query, conn);

                conn.Open();

                reader = cmd.ExecuteReader();
                //if (reader.Read())
                //    {
                //    DataTable table = new DataTable();
                //    table.Load(reader);
                //    dataGridView1.DataSource = table;
                //    conn.Close();


                //}
                while (reader.Read())
                {
                    dataGridView1.Rows.Add(reader["p_id"].ToString(), reader["username"].ToString(), reader["pass"].ToString(), reader["url"].ToString(), reader["acc_name"].ToString(), reader["category"].ToString(), reader["email"].ToString(), reader["p_date"].ToString(), reader["notes"].ToString());
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void btnpi_Click(object sender, EventArgs e)
        {
            txt_fname_p.Enabled = false;
            txt_lname_p.Enabled = false;
            txt_uname_p.Enabled = false;
            txt_sex.Enabled = false;
            txt_phone_p.Enabled = false;
            txt_address_p.Enabled = false;
            txt_email_p.Enabled = false;
            txt_dob_p.Enabled = false;

            tabControl.SelectedIndex = 4;
            panel2.Height = btnpi.Height;
            panel2.Top = btnpi.Top;
            panel2.Show();
            panel3.Hide();
            string id = null;
            try
            {

                string query = "SELECT * FROM admin WHERE username='" + Login.admin + "'";
                conn = new MySqlConnection(@"datasource= localhost; database=pms;port=3306; username = root; password= Zxc121216");
                cmd = new MySqlCommand(query, conn);

                conn.Open();

                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    id = reader.GetString("admin_id");
                    txt_fname_p.Text = reader.GetString("firstname");
                    txt_lname_p.Text = reader.GetString("lastname");
                    txt_uname_p.Text = reader.GetString("username");
                    txt_sex.Text = reader.GetString("sex");
                    txt_phone_p.Text = reader.GetString("phonenumber");
                    txt_address_p.Text = reader.GetString("address");
                    txt_email_p.Text = reader.GetString("email");
                    txt_dob_p.Text = reader.GetString("dob");

                }
                reader.Close();
                //string selectSql = "SELECT * FROM images WHERE id = 1";

                //using (MySqlCommand command = new MySqlCommand(selectSql, conn))
                //{
                //    using (MySqlDataReader reader1 = command.ExecuteReader())
                //    {
                //        if (reader1.Read())
                //        {

                //        }

                //    }
                //}
                //reader.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0}", ex), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void image_Click(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 0;
            panel3.Height = image.Height;
            panel3.Top = image.Top;
            panel3.Show();
            panel2.Hide();
        }






        private void timer1_Tick(object sender, EventArgs e)
        {
            label10.Text = DateTime.Now.ToString("HH:mm");
            label14.Text = DateTime.Now.ToString("ss");
            label15.Text = DateTime.Now.ToString("MMM dd yyyy");
            label16.Text = DateTime.Now.ToString("dddd");
            label14.Location = new Point(label10.Location.X + label10.Width - 5, label14.Location.Y);
        }




        private void delete_btn_Click_1(object sender, EventArgs e)
        {


            try
            {


                conn = new MySqlConnection(@"datasource= localhost; database=pms;port=3306; username = root; password= Zxc121216");

                conn.Open();

                try
                {
                    string query2 = "DELETE FROM passwords WHERE username='" + listBox_d.SelectedItem.ToString() + "'";
                    cmd = new MySqlCommand(query2, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("data is deleted");
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


        private void btn_search1_Click(object sender, EventArgs e)
        {
            listBox_d.Items.Clear();

            if (cat_d.SelectedIndex == -1)
            {
                MessageBox.Show("please enter the value first");
            }
            else
            {
                try
                {


                    conn = new MySqlConnection(@"datasource= localhost; database=pms;port=3306; username = root; password= Zxc121216");
                    string query2 = "SELECT * FROM passwords WHERE category='" + cat_d.SelectedItem.ToString() + "'AND admin_id='"+Login.id+"'";
                    cmd = new MySqlCommand(query2, conn);
                    conn.Open();
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string user = reader.GetString("username");
                        listBox_d.Items.Add(user);
                    }

                    reader.Close();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("database not connected {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btn_search2_Click(object sender, EventArgs e)
        {
            listBox_d.Items.Clear();

            if (cat_d.SelectedIndex == -1)
            {
                MessageBox.Show("please enter the value first");
            }
            else
            {
                try
                {


                    conn = new MySqlConnection(@"datasource= localhost; database=pms;port=3306; username = root; password= Zxc121216");
                    string query2 = "SELECT * FROM passwords WHERE acc_name='" + txt_accname_d.Text + "'AND admin_id='"+ Login.id +"'";

                    cmd = new MySqlCommand(query2, conn);
                    conn.Open();
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string user = reader.GetString("username");
                        listBox_d.Items.Add(user);
                    }

                    reader.Close();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("database not connected {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btn_search3_Click(object sender, EventArgs e)
        {
            listBox_d.Items.Clear();


            if (cat_d.SelectedIndex == -1)
            {
                MessageBox.Show("please enter the value first");
            }
            else
            {

                try
                {


                    conn = new MySqlConnection(@"datasource= localhost; database=pms;port=3306; username = root; password= Zxc121216");
                    string query2 = "SELECT * FROM passwords WHERE username='" + txt_uname_d.Text + "'AND admin_id='"+ Login.id + "' ";
                    cmd = new MySqlCommand(query2, conn);
                    conn.Open();
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string user = reader.GetString("username");
                        listBox_d.Items.Add(user);
                        txt_url_d.Text = reader.GetString("url");
                        txt_acc_d.Text = reader.GetString("acc_name");
                        txt_cat_d.Text = reader.GetString("category");
                        txt_username_d.Text = reader.GetString("username");
                        txt_pass_d.Text =decryption( reader.GetString("pass"));
                        txt_email_d.Text = reader.GetString("email");
                        txt_notes_d.Text = reader.GetString("notes");
                    }

                    reader.Close();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("database not connected {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void listBox_d_SelectedIndexChanged(object sender, EventArgs e)
        {

            conn = new MySqlConnection(@"datasource= localhost; database=pms;port=3306; username = root; password= Zxc121216");


            try
            {
                string query3 = "SELECT * FROM passwords WHERE username='" + listBox_d.SelectedItem.ToString() + "' ";
                cmd = new MySqlCommand(query3, conn);
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    txt_url_d.Text = reader.GetString("url");
                    txt_acc_d.Text = reader.GetString("acc_name");
                    txt_cat_d.Text = reader.GetString("category");
                    txt_username_d.Text = reader.GetString("username");
                    txt_pass_d.Text =decryption( reader.GetString("pass"));
                    txt_email_d.Text = reader.GetString("email");
                    txt_notes_d.Text = reader.GetString("notes");
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            reader.Close();
            conn.Close();
        }

        private void btn_search1_u_Click(object sender, EventArgs e)
        {
            list_u.Items.Clear();
            txt_accname_u.Enabled = false;
            txt_uname_u.Enabled = false;

            txt_email_u.Enabled = false;
            txt_notes_u.Enabled = false;
            txt_cat_u.Enabled = false;

            txt_pass_u.Enabled = false;
            txt_url_u.Enabled = false;
            txt_accname_u.Text = "";
            txt_uname_u.Text = "";

            txt_email_u.Text = "";
            txt_notes_u.Text = "";
            txt_cat_u.Text = "";

            txt_pass_u.Text = "";
            txt_url_u.Text = "";


            if (cat_u.SelectedIndex == -1)
            {
                MessageBox.Show("please enter the value first");
            }
            else
            {
                try
                {


                    conn = new MySqlConnection(@"datasource= localhost; database=pms;port=3306; username = root; password= Zxc121216");
                    string query2 = "SELECT * FROM passwords WHERE category='" + cat_u.SelectedItem.ToString() + "'AND admin_id='"+ Login.id + "' ";
                    cmd = new MySqlCommand(query2, conn);
                    conn.Open();
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string user = reader.GetString("username");
                        list_u.Items.Add(user);
                    }

                    reader.Close();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("database not connected {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btn_search2_u_Click(object sender, EventArgs e)
        {
            list_u.Items.Clear();
            txt_accname_u.Enabled = false;
            txt_uname_u.Enabled = false;

            txt_email_u.Enabled = false;
            txt_notes_u.Enabled = false;
            txt_cat_u.Enabled = false;

            txt_pass_u.Enabled = false;
            txt_url_u.Enabled = false;
            txt_accname_u.Text = "";
            txt_uname_u.Text = "";

            txt_email_u.Text = "";
            txt_notes_u.Text = "";
            txt_cat_u.Text = "";

            txt_pass_u.Text = "";
            txt_url_u.Text = "";
            if (cat_u.SelectedIndex == -1)
            {
                MessageBox.Show("please enter the value first");
            }
            else
            {
                try
                {


                    conn = new MySqlConnection(@"datasource= localhost; database=pms;port=3306; username = root; password= Zxc121216");
                    string query2 = "SELECT * FROM passwords WHERE acc_name='" + txt_acc_u.Text + "'AND admin_id='"+ Login.id + "' ";
                    cmd = new MySqlCommand(query2, conn);
                    conn.Open();
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string user = reader.GetString("username");
                        list_u.Items.Add(user);
                    }

                    reader.Close();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("database not connected {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btn_search3_u_Click(object sender, EventArgs e)
        {
            list_u.Items.Clear();
            txt_accname_u.Enabled = false;
            txt_uname_u.Enabled = false;

            txt_email_u.Enabled = false;
            txt_notes_u.Enabled = false;
            txt_cat_u.Enabled = false;

            txt_pass_u.Enabled = false;
            txt_url_u.Enabled = false;
            txt_accname_u.Text = "";
            txt_uname_u.Text = "";

            txt_email_u.Text = "";
            txt_notes_u.Text = "";
            txt_cat_u.Text = "";

            txt_pass_u.Text = "";
            txt_url_u.Text = "";
            if (cat_u.SelectedIndex == -1)
            {
                MessageBox.Show("please enter the value first");
            }
            else
            {

                try
                {


                    conn = new MySqlConnection(@"datasource= localhost; database=pms;port=3306; username = root; password= Zxc121216");
                    string query2 = "SELECT * FROM passwords WHERE username='" + txt_username_u.Text + "'AND admin_id='"+ Login.id + "' ";
                    cmd = new MySqlCommand(query2, conn);
                    conn.Open();
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string user = reader.GetString("username");
                        list_u.Items.Add(user);
                        txt_url_u.Text = reader.GetString("url");
                        txt_accname_u.Text = reader.GetString("acc_name");
                        txt_cat_u.Text = reader.GetString("category");
                        txt_uname_u.Text = reader.GetString("username");
                        txt_pass_u.Text = decryption(reader.GetString("pass"));
                        txt_email_u.Text = reader.GetString("email");
                        txt_notes_u.Text = reader.GetString("notes");
                    }

                    reader.Close();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("database not connected {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void list_u_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (list_u.SelectedIndex == -1)
            {
                txt_accname_u.Enabled = false;
                txt_uname_u.Enabled = false;

                txt_email_u.Enabled = false;
                txt_notes_u.Enabled = false;
                txt_cat_u.Enabled = false;

                txt_pass_u.Enabled = false;
                txt_url_u.Enabled = false;
            }
            else
            {
                txt_accname_u.Enabled = true;
                txt_uname_u.Enabled = true;

                txt_email_u.Enabled = true;
                txt_notes_u.Enabled = true;
                txt_cat_u.Enabled = true;

                txt_pass_u.Enabled = true;
                txt_url_u.Enabled = true;
                conn = new MySqlConnection(@"datasource= localhost; database=pms;port=3306; username = root; password= Zxc121216");


                try
                {
                    string query3 = "SELECT * FROM passwords WHERE username='" + list_u.SelectedItem.ToString() + "' ";
                    cmd = new MySqlCommand(query3, conn);
                    conn.Open();
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        txt_url_u.Text = reader.GetString("url");
                        txt_accname_u.Text = reader.GetString("acc_name");
                        txt_cat_u.Text = reader.GetString("category");
                        txt_uname_u.Text = reader.GetString("username");
                        txt_pass_u.Text =decryption( reader.GetString("pass"));
                        txt_email_u.Text = reader.GetString("email");
                        txt_notes_u.Text = reader.GetString("notes");
                    }
                }
                catch (Exception ex)
                {

                    throw;
                }
                reader.Close();
                conn.Close();
            }
        }


        private void update_Click(object sender, EventArgs e)
        {
            if (listBox_d.SelectedIndex == -1)
            {
                MessageBox.Show("please select any username first");
            }
            else
            {
                try
                {


                    conn = new MySqlConnection(@"datasource= localhost; database=pms;port=3306; username = root; password= Zxc121216");

                    conn.Open();

                    try
                    {
                        string query2 = "UPDATE passwords  SET url= '" + txt_url_u.Text + "',acc_name='" + txt_accname_u.Text + "',category= '" + txt_cat_u.Text + "',username='" + txt_uname_u.Text + "',pass='" + txt_pass_u.Text + "',email='" + txt_email_u.Text + "',notes='" + txt_notes_u.Text + "' WHERE username='" + list_u.SelectedItem.ToString() + "'";
                        cmd = new MySqlCommand(query2, conn);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("data is updated");
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(string.Format("data is not updated {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("database not connected {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                txt_accname_u.Text = "";
                txt_uname_u.Text = "";
                txt_acc_u.Text = "";
                txt_email_u.Text = "";
                txt_notes_u.Text = "";
                txt_cat_u.Text = "";
                txt_username_u.Text = "";
                txt_pass_u.Text = "";
                txt_url_u.Text = "";
                cat_u.SelectedIndex = -1;
                list_u.Items.Clear();
            }
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            int len = 8;
            const string ValidChar = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder result = new StringBuilder();
            Random rand = new Random();
            while (0 < len--)
            {
                result.Append(ValidChar[rand.Next(ValidChar.Length)]);

            }
            txt_pass.Text = result.ToString();
        }

        private void DECRYPT_btn_Click(object sender, EventArgs e)
        {
            Authentication a = new Authentication();
            a.ShowDialog();
            dataGridView1.Rows.Clear();
            try
            {
                string query = "SELECT * FROM passwords WHERE admin_id='"+ Login.id + "'";
                conn = new MySqlConnection(@"datasource= localhost; database=pms;port=3306; username = root; password= Zxc121216");
                cmd = new MySqlCommand(query, conn);

                conn.Open();

                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    dataGridView1.Rows.Add(reader["p_id"].ToString(), reader["username"].ToString(), decryption(reader["pass"].ToString()), reader["url"].ToString(), reader["acc_name"].ToString(), reader["category"].ToString(), reader["email"].ToString(), reader["p_date"].ToString(), reader["notes"].ToString());
                }
                DECRYPT_btn.SendToBack();
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void delete_btn_Click(object sender, EventArgs e)
        {
            if (listBox_d.SelectedIndex == -1)
            {
                MessageBox.Show("please select any username first");
            }
            else
            {
                try
                {


                    conn = new MySqlConnection(@"datasource= localhost; database=pms;port=3306; username = root; password= Zxc121216");

                    conn.Open();

                    try
                    {
                        string query2 = "DELETE FROM passwords WHERE username='" + listBox_d.SelectedItem.ToString() + "'";
                        cmd = new MySqlCommand(query2, conn);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("data is DELETED");
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
                txt_accname_d.Text = "";
                txt_uname_d.Text = "";
                txt_acc_d.Text = "";
                txt_email_d.Text = "";
                txt_notes_d.Text = "";
                txt_cat_d.Text = "";
                txt_username_d.Text = "";
                txt_pass_d.Text = "";
                txt_url_d.Text = "";
                cat_d.SelectedIndex = -1;
                listBox_d.Items.Clear();
            }
        }

        private void ENCRYPT_btn_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            try
            {
                string query = "SELECT * FROM passwords WHERE admin_id='"+ Login.id + "'";
                conn = new MySqlConnection(@"datasource= localhost; database=pms;port=3306; username = root; password= Zxc121216");
                cmd = new MySqlCommand(query, conn);

                conn.Open();

                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    dataGridView1.Rows.Add(reader["p_id"].ToString(), reader["username"].ToString(), reader["pass"].ToString(), reader["url"].ToString(), reader["acc_name"].ToString(), reader["category"].ToString(), reader["email"].ToString(), reader["p_date"].ToString(), reader["notes"].ToString());
                }
                ENCRYPT_btn.SendToBack();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Edit_Paint(object sender, PaintEventArgs e)
        {

        }
        public bool Confirm(string Message)
        {
            if (MessageBox.Show(Message, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                return true;
            }
            else return false;
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
        }

        private void guna2GradientButton1_Click_1(object sender, EventArgs e)
        {

            int len = 8;
            const string ValidChar = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder result = new StringBuilder();
            Random rand = new Random();
            while (0 < len--)
            {
                result.Append(ValidChar[rand.Next(ValidChar.Length)]);

            }

            txt_pass.Text = result.ToString();

        }

        private void add_btn_Click_1(object sender, EventArgs e)
        {
            if (txtAddWeb.Text.Equals("") || txtAddUserName.Text.Equals("") || txt_pass.Text.Equals("") || txt_email.Text.Equals("") || category.SelectedIndex == -1)
            {
                MessageBox.Show("kindly fill all the * information!!");
            }
            else
            {
                



                try
                {
                    int admin = 0;
                    string query = "SELECT admin_id FROM admin WHERE username='" + Login.admin + "'";
                    conn = new MySqlConnection(@"datasource= localhost; database=pms;port=3306; username = root; password= Zxc121216");
                    cmd = new MySqlCommand(query, conn);

                    conn.Open();

                    reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        admin = Convert.ToInt32(reader.GetString("admin_id"));
                    }
                    reader.Close();
                    try
                    {
                        string query2 = "SELECT * FROM passwords WHERE  category='" + category.SelectedItem.ToString() + "'AND pass='" + txt_pass.Text + "'";
                        cmd = new MySqlCommand(query2, conn);
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            MessageBox.Show("this password is taken please try another password ");
                            txt_pass.Text = "";

                        }

                        else
                        {
                            reader.Close();
                            string query1 = "INSERT INTO passwords(passwords.admin_id,passwords.acc_name," +
                            "passwords.category,passwords.username,passwords.pass,passwords.url,passwords.email," +
                            "passwords.p_date,passwords.notes) VALUES  ('" + admin + "','" + txtAddWeb.Text + "','" + category.SelectedItem.ToString() + "','" + txtAddUserName.Text + "'," +
                            "'" + encryption(txt_pass.Text) + "','" + txtAddURL.Text + "','" + txt_email.Text + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + txt_notes.Text + "')";
                            cmd = new MySqlCommand(query1, conn);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("data is inserted");
                            txtAddURL.Text = "";
                            txtAddWeb.Text = "";
                            txtAddUserName.Text = "";
                            txt_email.Text = "";
                            txt_pass.Text = "";
                            txt_notes.Text = "";
                            category.SelectedIndex = -1;
                        }
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(string.Format("data is not inserted {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }


                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("database not connected {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            ChangePassword c = new ChangePassword();
            c.ShowDialog();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (txt_fname_p.Text.Equals("") || txt_lname_p.Text.Equals("") || txt_dob_p.Text.Equals("") || txt_address_p.Text.Equals("") || txt_email_p.Text.Equals("") || txt_sex.Text.Equals("") || txt_phone_p.Text.Equals("") || txt_uname_p.Text.Equals(""))
            {
                MessageBox.Show("You cannot leave any field empty");
            }
            else
            {
                try
                {


                    conn = new MySqlConnection(@"datasource= localhost; database=pms;port=3306; username = root; password= Zxc121216");

                    conn.Open();

                    try
                    {
                        string query2 = "UPDATE admin  SET firstname= '" + txt_fname_p.Text + "',lastname='" + txt_lname_p.Text + "',username= '" + txt_uname_p.Text + "',address='" + txt_address_p.Text + "',sex='" + txt_sex.Text + "',email='" + txt_email_p.Text + "',dob='" + txt_dob_p.Text + "',phonenumber='" + txt_phone_p.Text + "' WHERE username='" + Login.admin + "'";
                        cmd = new MySqlCommand(query2, conn);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("data is updated");
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(string.Format("data is not updated {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("database not connected {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                txt_fname_p.Text.Equals("");
                txt_lname_p.Text.Equals("");
                txt_dob_p.Text.Equals("");
                txt_address_p.Text.Equals("");
                txt_email_p.Text.Equals("");
                txt_sex.Text.Equals("");
                txt_phone_p.Text.Equals("");
                txt_uname_p.Text.Equals("");
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked = true)
            {
                txt_fname_p.Enabled = true;
                txt_lname_p.Enabled = true;
                txt_uname_p.Enabled = true;
                txt_sex.Enabled = true;
                txt_phone_p.Enabled = true;
                txt_address_p.Enabled = true;
                txt_email_p.Enabled = true;
                txt_dob_p.Enabled = true;
            }
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                txt_fname_p.Enabled = true;
                txt_lname_p.Enabled = true;
                txt_uname_p.Enabled = true;
                txt_sex.Enabled = true;
                txt_phone_p.Enabled = true;
                txt_address_p.Enabled = true;
                txt_email_p.Enabled = true;
                txt_dob_p.Enabled = true;
            }
        }

        private void signout_btn_Click_1(object sender, EventArgs e)
        {
            panel2.Height = signout_btn.Height;
            panel2.Top = signout_btn.Top;
            panel2.Show();
            panel3.Hide();

            if (Confirm("do you really want to exit? \nyou will be directed to login ") == true)
            {
                this.Hide();
                Dashboard d = new Dashboard();
                d.Close();
                Login L = new Login();
                L.Show();
                if (Confirm("do you want to  or exit the system?\napplication will be closed ") == true)
                {
                    System.Environment.Exit(1);

                }
            }
        }


    }
}
