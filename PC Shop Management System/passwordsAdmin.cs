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
    public partial class passwordsAdmin : Form
    {
        public passwordsAdmin()
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

        private void passwordsAdmin_Load(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(Program.connectionString);
                string query = "select * from loginTB";
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader sd = cmd.ExecuteReader();
                while (sd.Read())
                {
                    if (sd["userType"].ToString() == "admin")
                    {
                        guna2TextBox7.Text = sd["userName"].ToString();
                        guna2TextBox8.Text = sd["password"].ToString();
                    }
                    else if (sd["userType"].ToString() == "manager")
                    {
                        guna2TextBox5.Text = sd["userName"].ToString();
                        guna2TextBox6.Text = sd["password"].ToString();
                    }
                    else if (sd["userType"].ToString() == "deo")
                    {
                        guna2TextBox2.Text = sd["userName"].ToString();
                        guna2TextBox4.Text = sd["password"].ToString();
                    }
                    else if (sd["userType"].ToString() == "salesman")
                    {
                        guna2TextBox3.Text = sd["userName"].ToString();
                        guna2TextBox1.Text = sd["password"].ToString();
                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
