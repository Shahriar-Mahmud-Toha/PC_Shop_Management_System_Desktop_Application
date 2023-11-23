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
    public partial class noticeAdmin : Form
    {
        public noticeAdmin()
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

        private void noticeAdmin_Load(object sender, EventArgs e)
        {
            try
            {
                bindNoticeTB();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2DataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                guna2TextBox8.Text = guna2DataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (guna2TextBox8.Text != "")
                {

                    SqlConnection con = new SqlConnection(Program.connectionString);
                    string query = "insert into noticeTB values(@time, @message)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@time ", DateTime.Now.ToString());
                    cmd.Parameters.AddWithValue("@message ", guna2TextBox8.Text);
                    con.Open();
                    int a = cmd.ExecuteNonQuery();
                    if (a > 0)
                    {
                        MessageBox.Show("Notice Published Successfully", "Publish Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    con.Close();
                    bindNoticeTB();
                    guna2TextBox8.Clear();
                }
                else
                {
                    errorProvider1.SetError(guna2TextBox8, "You have to type some Message.");
                    guna2TextBox8.BorderColor = Color.FromArgb(255, 82, 96);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void noticeUpdateBt_Click(object sender, EventArgs e)
        {
            try
            {
                if (guna2TextBox8.Text != "")
                {
                    SqlConnection con = new SqlConnection(Program.connectionString);
                    string query = "update noticeTB set message=@message where dateTime=@time";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@message ", guna2TextBox8.Text);
                    cmd.Parameters.AddWithValue("@time", guna2DataGridView1.SelectedRows[0].Cells[0].Value);
                    int a = cmd.ExecuteNonQuery();
                    if (a > 0)
                    {
                        MessageBox.Show("Notice Updated Successfully", "Update Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Data Not Updated", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    con.Close();
                    bindNoticeTB();
                    guna2TextBox8.Clear();
                }
                else
                {
                    errorProvider1.SetError(guna2TextBox8, "You have to type some Message.");
                    guna2TextBox8.BorderColor = Color.FromArgb(255, 82, 96);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void noticeDeleteBt_Click(object sender, EventArgs e)
        {
            try
            {
                if (guna2DataGridView1.SelectedRows.Count > 0)
                {
                    SqlConnection con = new SqlConnection(Program.connectionString);
                    string query = "delete from noticeTB where dateTime=@time";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@time", guna2DataGridView1.SelectedRows[0].Cells[0].Value);
                    int a = cmd.ExecuteNonQuery();
                    if (a > 0)
                    {
                        MessageBox.Show("Notice Deleted Successfully", "Delete Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Notice Not Deleted", "Delete Unsuccessful", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    con.Close();
                    bindNoticeTB();
                    guna2TextBox8.Clear();
                }
                else
                {
                    MessageBox.Show("No Row is Selected. Delete not Possible.", "Deletion Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2TextBox8_TextChanged(object sender, EventArgs e)
        {
            try
            {
                errorProvider1.Clear();
                guna2TextBox8.BorderColor = Color.FromArgb(22, 24, 28);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
