using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pms
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Dashboard());
            //SqlConnection conn = new SqlConnection("Data Source=IRFAN\\RAZRIZ;Initial Catalog=SABBANK;Persist Security Info=True;User ID=admin;Password=Zxc121216");
            //conn.Open();
            //MessageBox.Show("open");

        }
    }
}
