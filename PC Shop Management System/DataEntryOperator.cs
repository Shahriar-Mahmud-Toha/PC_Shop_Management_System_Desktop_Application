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
using System.Diagnostics;
using Guna.UI2.WinForms;

namespace PC_Shop_Management_System
{
    public partial class DataEntryOperator : Form
    {
        public bool deoClosed = false;
        public DataEntryOperator()
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

        private void logoutDeoBt_Click(object sender, EventArgs e)
        {
            try
            {
                deoClosed = true;
                stopwatch.Stop();
                this.Close();
                Program.loginForm.Show();
                GC.Collect();
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
                stopwatch.Stop();
                GC.Collect();
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DataEntryOperator_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (!deoClosed)
                {
                    Application.Exit();
                }
                stopwatch.Stop();
                GC.Collect();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void deoNoticeBt_Click(object sender, EventArgs e)
        {
            try
            {
                noticeDEO nd = new noticeDEO();
                nd.ShowDialog();
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
                accountSettingsDataEntryOperator sd = new accountSettingsDataEntryOperator();
                sd.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        DateTime start;
        DateTime end;
        private void DataEntryOperator_Load(object sender, EventArgs e)
        {
            try
            {
                welcomeText();
                bindProductData();
                stopwatch = new Stopwatch();
                stopwatch.Start();
                start = DateTime.Now;
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
                string query = "select userName from loginTB where userType='deo'";
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
        public string productIdGeneraotor()
        {
            int sn = 0;
            try
            {
                SqlConnection con = new SqlConnection(Program.connectionString);
                string query = "select * from productSNcounterTB";
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader sd = cmd.ExecuteReader();
                if (sd.Read())
                {
                    sn = (int)sd["serial"];
                }
                con.Close();
                query = "update productSNcounterTB set serial=@serial where sn=0";
                con.Open();
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@serial", sn + 1);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return "PD-" + sn;
        }

        public void insertPrductInfoToDatabase()
        {
            try
            {
                SqlConnection con = new SqlConnection(Program.connectionString);
                string query = "insert into ProductTB values(@pdId, @pdName, @companyName, @modelName, @price, @availableQuantity)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@pdId", productIdGeneraotor());
                cmd.Parameters.AddWithValue("@pdName", guna2ComboBox1.Text);
                cmd.Parameters.AddWithValue("@companyName", guna2TextBox8.Text);
                cmd.Parameters.AddWithValue("@modelName", guna2TextBox1.Text);
                cmd.Parameters.AddWithValue("@price", guna2TextBox9.Text);
                cmd.Parameters.AddWithValue("@availableQuantity", guna2TextBox10.Text);
                con.Open();
                int a = cmd.ExecuteNonQuery();
                if (a > 0)
                {
                    MessageBox.Show("Data Inserted Successfully", "Data Inserion Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Data not Inserted", "Data Inserion Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void resetProductForm()
        {
            try
            {
                guna2TextBox8.Clear();
                guna2TextBox1.Clear();
                guna2TextBox9.Clear();
                guna2TextBox10.Clear();
                guna2TextBox4.Clear();
                guna2TextBox4.Clear();
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
        private bool priceFormatChecker(string price)
        {
            bool flag = false;
            try
            {
                string errorMessage = null;
                bool onlyNumbers = false;
                string ch = "0123456789";
                for (int i = 0; i < price.Length; i++)
                {
                    if (!binarySearch(ch, price[i].ToString(), 0, ch.Length - 1))
                    {
                        onlyNumbers = false;
                        errorMessage += "* Price can be consists only Integer Numbers (0-9)\n";
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
                    MessageBox.Show(errorMessage, "Invalid Price", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flag;
        }
        private bool quantityFormatChecker(string quantity)
        {
            bool flag = false;
            try
            {
                string errorMessage = null;
                bool onlyNumbers = false;
                string ch = "-0123456789";
                for (int i = 0; i < quantity.Length; i++)
                {
                    if (!binarySearch(ch, quantity[i].ToString(), 0, ch.Length - 1))
                    {
                        onlyNumbers = false;
                        errorMessage += "* Quantity can be consists only Integer Numbers (0-9)\n";
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
                    MessageBox.Show(errorMessage, "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flag;
        }
        private bool quantityFormatCheckerForUpdate(string quantity)
        {
            bool flag = false;
            try
            {
                string errorMessage = null;
                bool onlyNumbers = false;
                string ch = "-0123456789";
                for (int i = 0; i < quantity.Length; i++)
                {
                    if (!binarySearch(ch, quantity[i].ToString(), 0, ch.Length - 1))
                    {
                        onlyNumbers = false;
                        errorMessage += "* Quantity can be consists only Integer Numbers (0-9)\n";
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
                    MessageBox.Show(errorMessage, "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flag;
        }
        private void insertBt_Click(object sender, EventArgs e)
        {
            try
            {
                if (guna2TextBox8.Text != "" && guna2TextBox1.Text != "" && guna2TextBox9.Text != "" && guna2TextBox10.Text != "")
                {
                    if (priceFormatChecker(guna2TextBox9.Text) && quantityFormatChecker(guna2TextBox10.Text))
                    {
                        if (Int32.Parse(guna2TextBox10.Text) > 0)
                        {
                            insertPrductInfoToDatabase();
                            bindProductData();
                            resetProductForm();
                        }
                        else
                        {
                            MessageBox.Show("* Quantity must be greater than 0", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
                else
                {
                    if (guna2TextBox8.Text == "")
                    {
                        errorProvider1.SetError(guna2TextBox8, "Company Name cannot be blank.");
                        guna2TextBox8.BorderColor = Color.FromArgb(255, 82, 96);
                    }
                    if (guna2TextBox1.Text == "")
                    {
                        errorProvider2.SetError(guna2TextBox1, "Model Info cannot be blank.");
                        guna2TextBox1.BorderColor = Color.FromArgb(255, 82, 96);
                    }
                    if (guna2TextBox9.Text == "")
                    {
                        errorProvider3.SetError(guna2TextBox9, "Price cannot be blank.");
                        guna2TextBox9.BorderColor = Color.FromArgb(255, 82, 96);
                    }
                    if (guna2TextBox10.Text == "")
                    {
                        errorProvider4.SetError(guna2TextBox10, "Quantity cannot be blank.");
                        guna2TextBox10.BorderColor = Color.FromArgb(255, 82, 96);
                    }
                }
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
                guna2ComboBox1.Text = guna2DataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                guna2TextBox8.Text = guna2DataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                guna2TextBox1.Text = guna2DataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                guna2TextBox9.Text = guna2DataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                guna2TextBox10.Text = guna2DataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void deleteBt_Click(object sender, EventArgs e)
        {
            try
            {
                if (guna2DataGridView1.SelectedRows.Count > 0)
                {
                    SqlConnection con = new SqlConnection(Program.connectionString);
                    string query = "delete from ProductTB where pdId = @pdId";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@pdId", guna2DataGridView1.SelectedRows[0].Cells[0].Value);
                    int a = cmd.ExecuteNonQuery();
                    if (a > 0)
                    {
                        MessageBox.Show("Data Deleted Successfully", "Delete Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Data Not Deleted", "Deletion Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    con.Close();
                    bindProductData();
                    resetProductForm();
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
        private int setAvailableQuantity()
        {
            int data = 0;
            try
            {
                if (((int)(guna2DataGridView1.SelectedRows[0].Cells[5].Value) + Int32.Parse(guna2TextBox10.Text)) < 0)
                {
                    data = 0;
                }
                else
                {
                    data = Int32.Parse(guna2TextBox10.Text) + (int)guna2DataGridView1.SelectedRows[0].Cells[5].Value;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return data;
        }
        private void updateBt_Click(object sender, EventArgs e)
        {
            try
            {
                if (guna2TextBox8.Text != "" && guna2TextBox1.Text != "" && guna2TextBox9.Text != "" && guna2TextBox10.Text != "")
                {
                    if (priceFormatChecker(guna2TextBox9.Text) && quantityFormatCheckerForUpdate(guna2TextBox10.Text))
                    {
                        SqlConnection con = new SqlConnection(Program.connectionString);
                        string query = "update ProductTB set pdName=@pdName, companyName=@companyName, modelName=@modelName, price=@price, availableQuantity=@availableQuantity where pdId=@pdId";
                        con.Open();
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@pdId", guna2DataGridView1.SelectedRows[0].Cells[0].Value);
                        cmd.Parameters.AddWithValue("@pdName", guna2ComboBox1.Text);
                        cmd.Parameters.AddWithValue("@companyName", guna2TextBox8.Text);
                        cmd.Parameters.AddWithValue("@modelName", guna2TextBox1.Text);
                        cmd.Parameters.AddWithValue("@price", guna2TextBox9.Text);
                        cmd.Parameters.AddWithValue("@availableQuantity", setAvailableQuantity());
                        int a = cmd.ExecuteNonQuery();
                        if (a > 0)
                        {
                            MessageBox.Show("Data updated Successfully", "Data Update Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Data Not Updated", "Data Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        con.Close();
                        bindProductData();
                        resetProductForm();
                    }
                }
                else
                {
                    if (guna2TextBox8.Text == "")
                    {
                        errorProvider1.SetError(guna2TextBox8, "Company Name cannot be blank.");
                        guna2TextBox8.BorderColor = Color.FromArgb(255, 82, 96);
                    }
                    if (guna2TextBox1.Text == "")
                    {
                        errorProvider2.SetError(guna2TextBox1, "Model Info cannot be blank.");
                        guna2TextBox1.BorderColor = Color.FromArgb(255, 82, 96);
                    }
                    if (guna2TextBox9.Text == "")
                    {
                        errorProvider3.SetError(guna2TextBox9, "Price cannot be blank.");
                        guna2TextBox9.BorderColor = Color.FromArgb(255, 82, 96);
                    }
                    if (guna2TextBox10.Text == "")
                    {
                        errorProvider4.SetError(guna2TextBox10, "Quantity cannot be blank.");
                        guna2TextBox10.BorderColor = Color.FromArgb(255, 82, 96);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void searchIDBt_Click(object sender, EventArgs e)
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
                    errorProvider5.SetError(guna2TextBox4, "You have to enter a Name or ID first");
                    guna2TextBox4.BorderColor = Color.FromArgb(255, 82, 96);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void refreshBt_Click(object sender, EventArgs e)
        {
            try
            {
                bindProductData();
                resetProductForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                    errorProvider5.SetError(guna2TextBox4, "You have to enter a Name or ID first");
                    guna2TextBox4.BorderColor = Color.FromArgb(255, 82, 96);
                }
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

        private void DataEntryOperator_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (!clickedStatus)
                {
                    stopwatch.Stop();
                    end = DateTime.Now;
                    saveWorkingDuration();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //watchSaveWDData();
        }
        //private void watchSaveWDData()
        //{
        //    int duration = 0;
        //    SqlConnection con = new SqlConnection(Program.connectionString);
        //    string query = "select * from RegularWorkingDurationTB where post=@post";
        //    con.Open();
        //    SqlCommand cmd = new SqlCommand(query, con);
        //    cmd.Parameters.AddWithValue("@post", "Data Entry Operator");
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
            int duration = 0;
            try
            {
                TimeSpan ts = end - start;

                SqlConnection con = new SqlConnection(Program.connectionString);
                string query = "select * from RegularWorkingDurationTB where post=@post";
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@post", "Data Entry Operator");
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
                cmd.Parameters.AddWithValue("@post", "Data Entry Operator");
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

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                errorProvider2.Clear();
                guna2TextBox1.BorderColor = Color.FromArgb(22, 24, 28);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2TextBox9_TextChanged(object sender, EventArgs e)
        {
            try
            {
                errorProvider3.Clear();
                guna2TextBox9.BorderColor = Color.FromArgb(22, 24, 28);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2TextBox10_TextChanged(object sender, EventArgs e)
        {
            try
            {
                errorProvider4.Clear();
                guna2TextBox10.BorderColor = Color.FromArgb(22, 24, 28);
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
                errorProvider5.Clear();
                guna2TextBox4.BorderColor = Color.FromArgb(22, 24, 28);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
