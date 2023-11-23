using Guna.UI2.WinForms;
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
    public partial class discountsOrCuponnAdmin : Form
    {
        public discountsOrCuponnAdmin()
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

        private void discountsOrCuponnAdmin_Load(object sender, EventArgs e)
        {
            try
            {
                reset();
                bindDiscountsAndCouponsData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void reset()
        {
            try
            {
                guna2TextBox1.Clear();
                guna2TextBox2.Clear();
                guna2TextBox5.Clear();
                guna2TextBox6.Clear();
                guna2TextBox4.Clear();
                guna2TextBox3.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void bindDiscountsAndCouponsData()
        {
            try
            {
                SqlConnection con = new SqlConnection(Program.connectionString);
                string query = "select * from discountsTB";
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader sd = cmd.ExecuteReader();
                if (sd.Read())
                {
                    guna2TextBox1.Text = sd["discounts"].ToString();
                }
                con.Close();

                SqlConnection con1 = new SqlConnection(Program.connectionString);
                string query1 = "select * from couponsTB";
                con1.Open();
                SqlCommand cmd1 = new SqlCommand(query1, con1);
                SqlDataReader sd1 = cmd1.ExecuteReader();
                if (sd1.Read())
                {
                    guna2TextBox6.Text = sd1["couponDiscount"].ToString();
                    guna2TextBox5.Text = sd1["coupon"].ToString();
                }
                con1.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool binarySearch(string str, string item, int start, int end)
        {
            bool flag = false;
            try
            {
                int mid;
                while (start <= end)
                {
                    mid = (start + end) / 2;

                    if (str[mid].ToString() == item)
                    {
                        flag = true;
                        break;
                    }
                    else if (char.Parse(item) < char.Parse(str[mid].ToString()))
                    {
                        end = mid - 1;
                    }
                    else
                    {
                        start = mid + 1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flag;
        }
        private bool discountFormatChecker(string discount)
        {
            bool flag = false;
            try
            {
                string errorMessage = null;
                bool onlyNumbers = false;
                string ch = "0123456789";
                for (int i = 0; i < discount.Length; i++)
                {
                    if (!binarySearch(ch, discount[i].ToString(), 0, ch.Length - 1))
                    {
                        onlyNumbers = false;
                        errorMessage += "* Discount can be consists only Integer Numbers (0-9)\n";
                        break;
                    }
                    else
                    {
                        onlyNumbers = true;
                    }
                }

                if (onlyNumbers)
                {
                    flag = true;
                }
                else
                {
                    MessageBox.Show(errorMessage, "Invalid Discount", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flag;
        }
        private void setDiscountBt_Click(object sender, EventArgs e)
        {
            try
            {
                if (guna2TextBox2.Text != "")
                {
                    if (discountFormatChecker(guna2TextBox2.Text))
                    {
                        SqlConnection con = new SqlConnection(Program.connectionString);
                        string query = "update discountsTB set discounts=@discount where sn=0";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@discount", Int32.Parse(guna2TextBox2.Text));
                        con.Open();
                        int a = cmd.ExecuteNonQuery();
                        if (a > 0)
                        {
                            MessageBox.Show("Discount Added Successfully", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        con.Close();
                        reset();
                        bindDiscountsAndCouponsData();
                    }
                }
                else
                {
                    errorProvider1.SetError(guna2TextBox2, "Discount cannot be blank.");
                    guna2TextBox2.BorderColor = Color.FromArgb(255, 82, 96);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void setCouponBt_Click(object sender, EventArgs e)
        {
            try
            {
                if (guna2TextBox4.Text != "" && guna2TextBox3.Text != "")
                {
                    if (discountFormatChecker(guna2TextBox3.Text))
                    {
                        SqlConnection con = new SqlConnection(Program.connectionString);
                        string query = "update couponsTB set coupon=@coupon, couponDiscount=@couponDiscount where sn=0";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@coupon", guna2TextBox4.Text);
                        cmd.Parameters.AddWithValue("@couponDiscount", Int32.Parse(guna2TextBox3.Text));
                        con.Open();
                        int a = cmd.ExecuteNonQuery();
                        if (a > 0)
                        {
                            MessageBox.Show("Coupon Added Successfully", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        con.Close();
                        reset();
                        bindDiscountsAndCouponsData();
                    }
                }
                else
                {
                    if (guna2TextBox4.Text == "")
                    {
                        errorProvider2.SetError(guna2TextBox4, "Coupon Code cannot be blank.");
                        guna2TextBox4.BorderColor = Color.FromArgb(255, 82, 96);
                    }
                    if (guna2TextBox3.Text == "")
                    {
                        errorProvider3.SetError(guna2TextBox3, "Coupon Discount cannot be blank.");
                        guna2TextBox3.BorderColor = Color.FromArgb(255, 82, 96);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void removeDiscountBt_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(Program.connectionString);
                string query = "update discountsTB set discounts=0 where sn=0";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                int a = cmd.ExecuteNonQuery();
                if (a > 0)
                {
                    MessageBox.Show("Discount Removed Successfully", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                con.Close();
                reset();
                bindDiscountsAndCouponsData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void removeCouponsBt_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(Program.connectionString);
                string query = "update couponsTB set coupon='', couponDiscount=0 where sn=0";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                int a = cmd.ExecuteNonQuery();
                if (a > 0)
                {
                    MessageBox.Show("Coupon Removed Successfully", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                con.Close();
                reset();
                bindDiscountsAndCouponsData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                errorProvider1.Clear();
                guna2TextBox2.BorderColor = Color.FromArgb(22, 24, 28);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2TextBox4_TextChanged(object sender, EventArgs e)
        {
            try
            {
                errorProvider2.Clear();
                guna2TextBox4.BorderColor = Color.FromArgb(22, 24, 28);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                errorProvider3.Clear();
                guna2TextBox3.BorderColor = Color.FromArgb(22, 24, 28);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
