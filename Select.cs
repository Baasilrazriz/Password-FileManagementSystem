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
    public partial class Select : Form
    {
        public Select()
        {
            InitializeComponent();
        }

      
        private void guna2GradientButton2_Click_1(object sender, EventArgs e)
        {
            Dashboard ds = new Dashboard();

            this.Hide();
            ds.Show();

            Select s = new Select();
            s.Close();

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            Dashboard d = new Dashboard();
            if (d.Confirm("do you want to  exit the system? ") == true)
            {
                System.Environment.Exit(1);

            }
            System.Environment.Exit(1);
        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {
            Login l = new Login();
            this.Hide();
            l.Show();

            Select s = new Select();
            s.Close();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            fms f=new fms();
            this.Hide();
            f.Show();

            Select s = new Select();
            s.Close();

        }

        private void Select_Load(object sender, EventArgs e)
        {

        }
    }
}
