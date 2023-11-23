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
using System.Configuration;

namespace PC_Shop_Management_System
{
    public partial class noticeDEO : Form
    {
        public noticeDEO()
        {
            InitializeComponent();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            this.Close();
            GC.Collect();
        }

        private void noticeDEO_Load(object sender, EventArgs e)
        {
            bindNoticeTB();
        }

        private void bindNoticeTB()
        {
            SqlConnection con = new SqlConnection(Program.connectionString);
            string query = "select * from noticeTB";
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader sd = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(sd);
            guna2DataGridView1.DataSource = dt;
            con.Close();
        }
    }
}
