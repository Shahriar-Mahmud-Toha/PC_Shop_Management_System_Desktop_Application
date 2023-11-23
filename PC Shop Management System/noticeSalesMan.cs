using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PC_Shop_Management_System
{
    public partial class noticeSalesMan : Form
    {
        public noticeSalesMan()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
                GC.Collect();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void noticeSalesMan_Load(object sender, EventArgs e)
        {
            bindNoticeTB();
        }
        private void bindNoticeTB()
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Notice Load Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
