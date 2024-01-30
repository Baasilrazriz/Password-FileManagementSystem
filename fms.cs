using GemBox.Document;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using MySql.Data.MySqlClient;

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;

namespace Pms
{
    public partial class fms : Form
    {
        MySqlConnection conn = null;
        MySqlCommand cmd = null, cmd1 = null;
        MySqlDataReader reader = null;
        //String file;
        public fms()
        {
            InitializeComponent();
            ofd.InitialDirectory = "C:";
            timer1.Start();
            panel2.Hide();
        }


        private string GetFileName(string path)
        {
            string name = path;
            int poz = path.LastIndexOf('.');
            if (poz > 0) name = path.Substring(0, poz);
            return name;
        }

        private string GetFileExt(string path)
        {
            string ext = "";
            int poz = path.LastIndexOf('.');
            if (poz > 0) ext = path.Substring(poz + 1);
            return ext;
        }

        private byte ByteEncrypt(byte b)
        {
            return (byte)(b ^ 128);
        }

        private byte[] StrToByteArray(string st, Encoding enc)
        {
            return enc.GetBytes(st);
        }

        private string ByteArrayToStr(byte[] bstr, Encoding enc)
        {
            return enc.GetString(bstr);
        }

        public bool EncryptFile(string inputFile)
        {
            string name = GetFileName(inputFile);
            string ext = GetFileExt(inputFile);
            byte[] bext = StrToByteArray(ext, new UnicodeEncoding());
            int k = bext.Length;
            try
            {
                FileStream fsRead = new FileStream(inputFile, FileMode.Open);
                FileStream fsWrite = new FileStream(name + ".pk", FileMode.Create);
                fsWrite.Write(BitConverter.GetBytes(k), 0, 4);
                fsWrite.Write(bext, 0, k);
                int data;
                while ((data = fsRead.ReadByte()) != -1) fsWrite.WriteByte(ByteEncrypt((byte)data));
                fsRead.Close();
                fsWrite.Close();
               System.IO.File.Delete(inputFile);
                return true;
            }
            catch { }
            return false;
        }

        public bool DecryptFile(string inputFile)
        {
            try
            {
                FileStream fsRead = new FileStream(inputFile, FileMode.Open);
                string name = GetFileName(inputFile);
                byte[] bint32 = new byte[4];
                int i = fsRead.Read(bint32, 0, 4);
                int k = BitConverter.ToInt32(bint32, 0);
                byte[] bext = new byte[k];
                i = fsRead.Read(bext, 0, k);
                string ext = "." + ByteArrayToStr(bext, new UnicodeEncoding());
                FileStream fsWrite = new FileStream(name + ext, FileMode.Create);
                int data;
                while ((data = fsRead.ReadByte()) != -1) fsWrite.WriteByte(ByteEncrypt((byte)data));
                fsRead.Close();
                fsWrite.Close();
                return true;
            }
            catch { }
            return false;
        }
    
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
            panel2.Height = btn_encrypt.Height;
            panel2.Top = btn_encrypt.Top;
            panel2.Show();
            panel3.Hide();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
           
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
            panel3.Height = image.Height;
            panel3.Top = image.Top;
            panel3.Show();
            panel2.Hide();
        }


        private void btn_encypt_Click_1(object sender, EventArgs e)
        {
            Authentication a = new Authentication();
            a.ShowDialog();

            if (listBox1.SelectedItem == null)
                {
                    MessageBox.Show("Please select a file first");
                }
                else
                {
                    string file = listBox1.SelectedItem.ToString();

                    ofd.Filter = "All Files|*.*";
                    try
                    {

                        if (EncryptFile(file))
                        {
                            MessageBox.Show(file + " has been successfully encrypted" + Environment.NewLine);
                        }


                    }
                    catch (Exception)
                    {

                        MessageBox.Show(file + " could not be decrypted" + Environment.NewLine);
                    }
                }


             
        }

        private void btn_decrypt_Click(object sender, EventArgs e)
        {
            Authentication a = new Authentication();
            a.ShowDialog();
            if (listBox1.SelectedItem == null)
                {
                    MessageBox.Show("Please select a file first");
                }
                else
                {
                    string file = listBox1.SelectedItem.ToString();

                    ofd.Filter = "Encrypted Files|*.pk";
                    try
                    {
                        if (DecryptFile(file))
                        {
                            MessageBox.Show(file + " has been successfully decrypted" + Environment.NewLine);
                            System.IO.File.Delete(file);
                        }

                    }
                    catch (Exception)
                    {

                        MessageBox.Show(file + " could not be decrypted" + Environment.NewLine);
                    }


                }
            
           
}

        private void btn_browse_Click(object sender, EventArgs e)
        {
           
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                foreach (var s in ofd.FileNames)
                {
                    listBox1.Items.Add(s.ToString());
                    //file=s.ToString();
                }

            }
        }

        private void brower_btn1_Click(object sender, EventArgs e)
        {

        }

        private void compress_btn_Click(object sender, EventArgs e)
        {

        }
        
        private void decompress_btn_Click(object sender, EventArgs e)
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
        private void guna2Button3_Click(object sender, EventArgs e)
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

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            string path = string.Empty;
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                path = fbd.SelectedPath;
                DirectoryInfo difo = new DirectoryInfo(path);
                foreach (FileInfo finfo in difo.GetFiles())
                {
                        gz.extract(finfo);
                        File.Delete(finfo.FullName);
                        
             
                }
            }
        }
        GZipCompressor gz=new   GZipCompressor();
        private void compress_btn_Click_1(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd=new FolderBrowserDialog();
            string path = string.Empty;
            if (fbd.ShowDialog()==DialogResult.OK)
            {
                path=fbd.SelectedPath;
                DirectoryInfo difo = new DirectoryInfo(path);
                foreach (FileInfo finfo in difo.GetFiles())
                {
                    if (yesdo)
                    {
                        gz.compress(finfo,listBox2);
                        File.Delete(finfo.FullName);
                    }
                    else
                    {
                        gz.compress(finfo, listBox2);
                    }
                    
                }
            }
        }
        bool yesdo = false;

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                yesdo = true;
            }
        }

        private void txt_fname_p_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void txt_uname_p_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void btn_info_Click(object sender, EventArgs e)
        {
            
        }

        private void btnpi_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 3;
            panel2.Height = btnpi.Height;
            panel2.Top = btnpi.Top;
            panel2.Show();
            panel3.Hide();
            txt_fname_p.Enabled = false;
            txt_lname_p.Enabled = false;
            txt_uname_p.Enabled = false;
            txt_sex.Enabled = false;
            txt_phone_p.Enabled = false;
            txt_address_p.Enabled = false;
            txt_email_p.Enabled = false;
            txt_dob_p.Enabled = false;

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
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0}", ex), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2;
            panel2.Height = guna2Button1.Height;
            panel2.Top = guna2Button1.Top;
            panel2.Show();
            panel3.Hide();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            ChangePassword c = new ChangePassword();
            c.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
                label10.Text = DateTime.Now.ToString("HH:mm");
                label14.Text = DateTime.Now.ToString("ss");
                label15.Text = DateTime.Now.ToString("MMM dd yyyy");
                label16.Text = DateTime.Now.ToString("dddd");
                label14.Location = new Point(label10.Location.X + label10.Width - 5, label14.Location.Y);
            

        }

        private void guna2Button4_Click_1(object sender, EventArgs e)
        {
            ChangePassword c = new ChangePassword();
            c.ShowDialog();
        }

        private void guna2Button2_Click_1(object sender, EventArgs e)
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
    }
}
