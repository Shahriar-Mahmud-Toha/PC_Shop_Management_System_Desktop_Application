using Guna.UI2.AnimatorNS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Markup;

namespace PC_Shop_Management_System
{
    public partial class SalesMan : Form
    {
        public bool salesmanFrmClosed = false;
        public SalesMan()
        {
            try
            {
                InitializeComponent();
                guna2DataGridView1.RowTemplate.Height = 40;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void exitButton_Click(object sender, EventArgs e)
        {
            try
            {
                GC.Collect();
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SalesMan_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (!salesmanFrmClosed)
                {
                    Program.loginForm.Close();
                }
                GC.Collect();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void logoutSalesmanBt_Click(object sender, EventArgs e)
        {
            try
            {
                salesmanFrmClosed = true;
                this.Close();
                Program.loginForm.Show();
                GC.Collect();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void noticeSMBt_Click(object sender, EventArgs e)
        {
            try
            {
                noticeSalesMan ns = new noticeSalesMan();
                ns.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2Button12_Click(object sender, EventArgs e)
        {
            try
            {
                accountSettingsSalesman ss = new accountSettingsSalesman();
                ss.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        DateTime start;
        DateTime end;
        private void SalesMan_Load(object sender, EventArgs e)
        {
            try
            {
                welcomeText();
                bindProductData();
                guna2DataGridView1.Rows[0].Selected = true;
                guna2TextBox1.Text = guna2DataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                stopwatch = new Stopwatch();
                stopwatch.Start();
                start = DateTime.Now;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void bindProductData()
        {
            try
            {
                SqlConnection con = new SqlConnection(Program.connectionString);
                string query = "select * from ProductTB";
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
        private void welcomeText()
        {
            try
            {
                SqlConnection con = new SqlConnection(Program.connectionString);
                string query = "select userName from loginTB where userType='salesman'";
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader sd = cmd.ExecuteReader();
                if (sd.Read())
                {
                    welcomeUserIdLabel.Text = sd["userName"].ToString();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        Stopwatch stopwatch;
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                label3.Text = String.Format("{0:hh\\:mm\\:ss}", stopwatch.Elapsed);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SalesMan_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (!clickedStatus)
                {
                    stopwatch.Stop();
                    end = DateTime.Now;
                    saveWorkingDuration();
                    //watchSaveWDData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //private void watchSaveWDData()
        //{
        //    int duration = 0;
        //    SqlConnection con = new SqlConnection(Program.connectionString);
        //    string query = "select * from RegularWorkingDurationTB where post=@post";
        //    con.Open();
        //    SqlCommand cmd = new SqlCommand(query, con);
        //    cmd.Parameters.AddWithValue("@post", "Salesman");
        //    SqlDataReader sd = cmd.ExecuteReader();
        //    if (sd.Read())
        //    {
        //        duration = (int)sd["duration"];
        //    }
        //    con.Close();
        //    MessageBox.Show(duration.ToString());
        //}
        private void saveWorkingDuration()
        {
            try
            {
                int duration = 0;
                TimeSpan ts = end - start;

                SqlConnection con = new SqlConnection(Program.connectionString);
                string query = "select * from RegularWorkingDurationTB where post=@post";
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@post", "Salesman");
                SqlDataReader sd = cmd.ExecuteReader();
                if (sd.Read())
                {
                    duration = (int)sd["duration"];
                }
                con.Close();
                query = "update RegularWorkingDurationTB set duration=@duration where post=@post";
                con.Open();
                cmd = new SqlCommand(query, con);
                duration += Convert.ToInt32(ts.TotalSeconds);
                cmd.Parameters.AddWithValue("@duration", duration);
                cmd.Parameters.AddWithValue("@post", "Salesman");
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool clickedStatus = false;

        private void breakBt_Click(object sender, EventArgs e)
        {
            try
            {
                if (!clickedStatus)
                {
                    guna2Panel2.FillColor = Color.FromArgb(255, 82, 96);
                    clickedStatus = true;
                    stopwatch.Stop();
                    end = DateTime.Now;
                    saveWorkingDuration();
                }
                else
                {
                    guna2Panel2.FillColor = Color.FromArgb(14, 180, 138);
                    clickedStatus = false;
                    stopwatch.Start();
                    start = DateTime.Now;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void ResetControl()
        {
            try
            {
                guna2TextBox4.Clear();
                guna2TextBox5.Clear();
                guna2TextBox2.Clear();
                guna2TextBox1.Clear();
                discountBt.Enabled = true;
                applyCouponBt.Enabled = true;
                guna2DataGridView1.Rows[0].Selected = true;
                guna2TextBox1.Text = guna2DataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void discountBt_Click(object sender, EventArgs e)
        {
            try
            {
                if (guna2DataGridView1.SelectedRows.Count > 0)
                {
                    int finalPrice = Int32.Parse(guna2TextBox1.Text) - getDiscount();
                    if (finalPrice >= 0)
                    {
                        guna2TextBox1.Text = finalPrice.ToString();
                    }
                    else
                    {
                        guna2TextBox1.Text = "0";
                    }
                    discountBt.Enabled = false;
                }
                else
                {
                    MessageBox.Show("You have to select a row first");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //insertIdIntoAppliedDiscountList(guna2DataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            //if (!checkDiscountAppliedOrNot(guna2DataGridView1.SelectedRows[0].Cells[0].Value.ToString()))
            //{
            //}
            //else
            //{
            //    MessageBox.Show("Discount already applied in this product");
            //}
        }
        //private int getProductPrice(string pdId)
        //{
        //    int price = -1;
        //    SqlConnection con = new SqlConnection(Program.connectionString);
        //    string query = "select price from ProductTB where pdId=@pdId";
        //    con.Open();
        //    SqlCommand cmd = new SqlCommand(query, con);
        //    cmd.Parameters.AddWithValue("@pdId", pdId);
        //    SqlDataReader sd = cmd.ExecuteReader();
        //    if (sd.HasRows)
        //    {
        //        if (sd.Read())
        //        {
        //            price = (int)sd["price"];
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Product ID Not found");
        //    }
        //    con.Close();
        //    return price;
        //}

        //private bool checkDiscountAppliedOrNot(string pdId)
        //{
        //    bool flag = false;
        //    SqlConnection con = new SqlConnection(Program.connectionString);
        //    string query = "select * from appliedDiscountsPdListTB where pdId=@pdId";
        //    con.Open();
        //    SqlCommand cmd = new SqlCommand(query, con);
        //    cmd.Parameters.AddWithValue("@pdId", pdId);
        //    SqlDataReader sd = cmd.ExecuteReader();
        //    if (sd.HasRows)
        //    {
        //        flag = true;
        //    }
        //    con.Close();
        //    return flag;
        //}

        //private void insertIdIntoAppliedDiscountList(string pdId)
        //{
        //    SqlConnection con = new SqlConnection(Program.connectionString);
        //    string query = "insert into appliedDiscountsPdListTB values(@pdId)";
        //    con.Open();
        //    SqlCommand cmd = new SqlCommand(query, con);
        //    cmd.Parameters.AddWithValue("@pdId", pdId);
        //    cmd.ExecuteNonQuery();
        //    con.Close();
        //}

        private int getDiscount()
        {
            int discount = 0;
            try
            {
                SqlConnection con = new SqlConnection(Program.connectionString);
                string query = "select * from discountsTB";
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader sd = cmd.ExecuteReader();
                if (sd.Read())
                {
                    discount = (int)sd["discounts"];
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return discount;
        }
        //private void resetAppliedDiscountList()
        //{
        //    SqlConnection con = new SqlConnection(Program.connectionString);
        //    string query = "delete from appliedDiscountsPdListTB";
        //    con.Open();
        //    SqlCommand cmd = new SqlCommand(query, con);
        //    cmd.ExecuteNonQuery();
        //    con.Close();
        //}
        private void refreshBt_Click(object sender, EventArgs e)
        {
            try
            {
                ResetControl();
                bindProductData();
                //resetAppliedDiscountList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2DataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                guna2TextBox1.Text = guna2DataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void applyCouponBt_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkValidCoupon(guna2TextBox5.Text))
                {
                    int finalPrice = Int32.Parse(guna2TextBox1.Text) - getCouponDiscount(guna2TextBox5.Text);
                    if (finalPrice >= 0)
                    {
                        guna2TextBox1.Text = finalPrice.ToString();
                    }
                    else
                    {
                        guna2TextBox1.Text = "0";
                    }
                    applyCouponBt.Enabled = false;
                }
                else
                {
                    if (guna2TextBox5.Text == "")
                    {
                        errorProvider2.SetError(guna2TextBox5, "Coupon cannot be blank.");
                        guna2TextBox5.BorderColor = Color.FromArgb(255, 82, 96);
                    }
                    else
                    {
                        MessageBox.Show("This Coupon code is not Correct", "Invalid Coupon", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool checkValidCoupon(string coupon)
        {
            bool flag = false;
            try
            {
                SqlConnection con = new SqlConnection(Program.connectionString);
                string query = "select coupon from couponsTB where coupon=@coupon";
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@coupon", coupon);
                SqlDataReader sd = cmd.ExecuteReader();
                if (sd.HasRows)
                {
                    flag = true;
                }
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flag;
        }
        private int getCouponDiscount(string coupon)
        {
            int discount = 0;
            try
            {
                SqlConnection con = new SqlConnection(Program.connectionString);
                string query = "select couponDiscount from couponsTB where coupon=@coupon";
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@coupon", coupon);
                SqlDataReader sd = cmd.ExecuteReader();
                if (sd.HasRows)
                {
                    if (sd.Read())
                    {
                        discount = (int)sd["couponDiscount"];
                    }
                }
                else
                {
                    MessageBox.Show("Coupon Not Found");
                }
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return discount;
        }
        private void guna2DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                discountBt.Enabled = true;
                applyCouponBt.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void soldBt_Click(object sender, EventArgs e)
        {
            try
            {
                if (guna2TextBox2.Text != "")
                {
                    if (Int32.Parse(guna2TextBox2.Text) > 0)
                    {
                        if (Int32.Parse(guna2TextBox2.Text) <= Int32.Parse(guna2DataGridView1.SelectedRows[0].Cells[5].Value.ToString()))
                        {
                            SqlConnection con = new SqlConnection(Program.connectionString);
                            string query = "update ProductTB set availableQuantity=@availableQuantity where pdId=@pdId";
                            con.Open();
                            SqlCommand cmd = new SqlCommand(query, con);
                            cmd.Parameters.AddWithValue("@pdId", guna2DataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                            cmd.Parameters.AddWithValue("@availableQuantity", Int32.Parse(guna2DataGridView1.SelectedRows[0].Cells[5].Value.ToString()) - Int32.Parse(guna2TextBox2.Text));
                            cmd.ExecuteNonQuery();
                            con.Close();

                            saveTotalNumberOfProductSoldWithDateTB();
                            saveTotalSalesWithDateTB();
                            saveToTodaysTotalSalesWithDateTB();
                            saveToTotalNumberOfProductSoldTB();
                            saveToTotalSalesTB();
                            saveToTodaysTotalSalesTB();
                            MessageBox.Show("Product has been Sold Successfully", "Sold Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            bindProductData();
                        }
                        else
                        {
                            MessageBox.Show("Sold Quantity Must be less than Available Quantity", "Sold Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Sold Quantity Must be greater than 0", "Sold Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    errorProvider1.SetError(guna2TextBox2, "Quantity cannot be blank.");
                    guna2TextBox2.BorderColor = Color.FromArgb(255, 82, 96);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Sold Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void saveToTodaysTotalSalesWithDateTB()
        {
            try
            {
                string date = DateTime.Now.ToString("dd MMM");
                if (findToTodaysTotalSalesWithDateTB(date))
                {
                    SqlConnection con = new SqlConnection(Program.connectionString);
                    string query = "update todaysTotalSalesWithDateTB set todaysTotalSales=@todaysTotalSales where dates=@date";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@todaysTotalSales", Int32.Parse(guna2TextBox1.Text) * Int32.Parse(guna2TextBox2.Text) + getCurrentTodaysTotalSalesWithDateTB(date));
                    cmd.Parameters.AddWithValue("@date", date);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                else
                {
                    SqlConnection con = new SqlConnection(Program.connectionString);
                    string query = "insert into todaysTotalSalesWithDateTB values(@dates,@todaysTotalSales)";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@dates", date);
                    cmd.Parameters.AddWithValue("@todaysTotalSales", Int32.Parse(guna2TextBox1.Text) * Int32.Parse(guna2TextBox2.Text));
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private int getCurrentTodaysTotalSalesWithDateTB(string date)
        {
            int data = 0;
            try
            {
                SqlConnection con = new SqlConnection(Program.connectionString);
                string query = "select todaysTotalSales from todaysTotalSalesWithDateTB where dates=@dates";
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@dates", date);
                SqlDataReader sd = cmd.ExecuteReader();
                if (sd.Read())
                {
                    data = (int)sd["todaysTotalSales"];
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return data;
        }
        private bool findToTodaysTotalSalesWithDateTB(string date) //returns true if date found.
        {
            bool flag = false;
            try
            {
                SqlConnection con = new SqlConnection(Program.connectionString);
                string query = "select dates from todaysTotalSalesWithDateTB where dates=@dates";
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@dates", date);
                SqlDataReader sd = cmd.ExecuteReader();
                if (sd.HasRows)
                {
                    flag = true;
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flag;
        }
        private void saveTotalSalesWithDateTB()
        {
            try
            {
                string date = DateTime.Now.ToString("dd MMM");
                if (findDateTotalSalesWithDateTB(date))
                {
                    SqlConnection con = new SqlConnection(Program.connectionString);
                    string query = "update totalSalesWithDateTB set totalSales=@totalSales where dates=@date";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@totalSales", Int32.Parse(guna2TextBox1.Text) * Int32.Parse(guna2TextBox2.Text) + getCurrentTotalSalesWithDateTB(date));
                    cmd.Parameters.AddWithValue("@date", date);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                else
                {
                    SqlConnection con = new SqlConnection(Program.connectionString);
                    string query = "insert into totalSalesWithDateTB values(@dates,@totalSales)";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@dates", date);
                    cmd.Parameters.AddWithValue("@totalSales", Int32.Parse(guna2TextBox1.Text) * Int32.Parse(guna2TextBox2.Text) + getCurrentTotalSales());
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool findDateTotalSalesWithDateTB(string date) //returns true if date found.
        {
            bool flag = false;
            try
            {
                SqlConnection con = new SqlConnection(Program.connectionString);
                string query = "select dates from totalSalesWithDateTB where dates=@dates";
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@dates", date);
                SqlDataReader sd = cmd.ExecuteReader();
                if (sd.HasRows)
                {
                    flag = true;
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flag;
        }
        private int getCurrentTotalSalesWithDateTB(string date)
        {
            int data = 0;
            try
            {
                SqlConnection con = new SqlConnection(Program.connectionString);
                string query = "select totalSales from totalSalesWithDateTB where dates=@dates";
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@dates", date);
                SqlDataReader sd = cmd.ExecuteReader();
                if (sd.Read())
                {
                    data = (int)sd["totalSales"];
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return data;
        }

        private bool findDateInSaveTotalNumberOfProductSoldWithDateTB(string date) //returns true if date found.
        {
            bool flag = false;
            try
            {
                SqlConnection con = new SqlConnection(Program.connectionString);
                string query = "select dates from totalNumberOfProductSoldWithDateTB where dates=@dates";
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@dates", date);
                SqlDataReader sd = cmd.ExecuteReader();
                if (sd.HasRows)
                {
                    flag = true;
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flag;
        }
        private int getCurrentTotalNumberOfProductSoldWithDateTB(string date)
        {
            int data = 0;
            try
            {
                SqlConnection con = new SqlConnection(Program.connectionString);
                string query = "select totalSold from totalNumberOfProductSoldWithDateTB where dates=@dates";
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@dates", date);
                SqlDataReader sd = cmd.ExecuteReader();
                if (sd.Read())
                {
                    data = (int)sd["totalSold"];
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return data;
        }
        private void saveTotalNumberOfProductSoldWithDateTB()
        {
            try
            {
                string date = DateTime.Now.ToString("dd MMM");
                if (findDateInSaveTotalNumberOfProductSoldWithDateTB(date))
                {
                    SqlConnection con = new SqlConnection(Program.connectionString);
                    string query = "update totalNumberOfProductSoldWithDateTB set totalSold=@totalSold where dates=@dates";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@totalSold", Int32.Parse(guna2TextBox2.Text) + getCurrentTotalNumberOfProductSoldWithDateTB(date));
                    cmd.Parameters.AddWithValue("@dates", date);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                else
                {
                    SqlConnection con = new SqlConnection(Program.connectionString);
                    string query = "insert into totalNumberOfProductSoldWithDateTB values(@dates,@totalSold)";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@dates", date);
                    cmd.Parameters.AddWithValue("@totalSold", Int32.Parse(guna2TextBox2.Text) + getCurrentTotalNumberofProductSold());
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void saveToTodaysTotalSalesTB()
        {
            try
            {
                SqlConnection con = new SqlConnection(Program.connectionString);
                string query = "update todaysTotalSalesTB set todaysTotalSales=@todaysTotalSales where sn=0";
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@todaysTotalSales", Int32.Parse(guna2TextBox1.Text) * Int32.Parse(guna2TextBox2.Text) + getCurrentTodaysTotalTotalSales());
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void saveToTotalSalesTB()
        {
            try
            {
                SqlConnection con = new SqlConnection(Program.connectionString);
                string query = "update totalSalesTB set totalSales=@totalSales where sn=0";
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@totalSales", Int32.Parse(guna2TextBox1.Text) * Int32.Parse(guna2TextBox2.Text) + getCurrentTotalSales());
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void saveToTotalNumberOfProductSoldTB()
        {
            try
            {
                SqlConnection con = new SqlConnection(Program.connectionString);
                string query = "update totalNumberOfProductSoldTB set totalSold=@totalSold where sn=0";
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@totalSold", Int32.Parse(guna2TextBox2.Text) + getCurrentTotalNumberofProductSold());
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int getCurrentTodaysTotalTotalSales()
        {
            int data = 0;
            try
            {
                SqlConnection con = new SqlConnection(Program.connectionString);
                string query = "select todaysTotalSales from todaysTotalSalesTB where sn=0";
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader sd = cmd.ExecuteReader();
                if (sd.Read())
                {
                    data = (int)sd["todaysTotalSales"];
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return data;
        }

        private int getCurrentTotalSales()
        {
            int data = 0;
            try
            {
                SqlConnection con = new SqlConnection(Program.connectionString);
                string query = "select totalSales from totalSalesTB where sn=0";
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader sd = cmd.ExecuteReader();
                if (sd.Read())
                {
                    data = (int)sd["totalSales"];
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return data;
        }

        private int getCurrentTotalNumberofProductSold()
        {
            int data = 0;
            try
            {
                SqlConnection con = new SqlConnection(Program.connectionString);
                string query = "select totalSold from totalNumberOfProductSoldTB where sn=0";
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader sd = cmd.ExecuteReader();
                if (sd.Read())
                {
                    data = (int)sd["totalSold"];
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return data;
        }

        private void searchNameBt_Click(object sender, EventArgs e)
        {
            try
            {
                if (guna2TextBox4.Text != "")
                {
                    SqlConnection con = new SqlConnection(Program.connectionString);
                    string query = "select * from ProductTB where pdName=@searchedName or companyName=@searchedName or modelName=@searchedName";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@searchedName", guna2TextBox4.Text);
                    SqlDataReader sd = cmd.ExecuteReader();
                    if (sd.HasRows)
                    {
                        DataTable dt = new DataTable();
                        dt.Load(sd);
                        guna2DataGridView1.DataSource = dt;
                    }
                    else
                    {
                        MessageBox.Show("No Product Found", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    con.Close();
                }
                else
                {
                    errorProvider3.SetError(guna2TextBox4, "You have to enter a Name or ID first");
                    guna2TextBox4.BorderColor = Color.FromArgb(255, 82, 96);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void searchIdBt_Click(object sender, EventArgs e)
        {
            try
            {
                if (guna2TextBox4.Text != "")
                {
                    SqlConnection con = new SqlConnection(Program.connectionString);
                    string query = "select * from ProductTB where pdId=@pdId";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@pdId", guna2TextBox4.Text);
                    SqlDataReader sd = cmd.ExecuteReader();
                    if (sd.HasRows)
                    {
                        DataTable dt = new DataTable();
                        dt.Load(sd);
                        guna2DataGridView1.DataSource = dt;
                    }
                    else
                    {
                        MessageBox.Show("No Product Found", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    con.Close();
                }
                else
                {
                    errorProvider3.SetError(guna2TextBox4, "You have to enter a Name or ID first");
                    guna2TextBox4.BorderColor = Color.FromArgb(255, 82, 96);
                }
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

        private void guna2TextBox5_TextChanged(object sender, EventArgs e)
        {
            try
            {
                errorProvider2.Clear();
                guna2TextBox5.BorderColor = Color.FromArgb(22, 24, 28);
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
                errorProvider3.Clear();
                guna2TextBox4.BorderColor = Color.FromArgb(22, 24, 28);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
