using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PC_Shop_Management_System
{
    public partial class productsAdmin : Form
    {
        public productsAdmin()
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
                            bindAllProductData();
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
        public void insertPrductInfoToDatabase()
        {
            try
            {
                SqlConnection con = new SqlConnection(Program.connectionString);
                string query = "insert into ProductTB values(@pdId, @pdName, @companyName, @modelName, @price, @availableQuantity)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("pdId", productIdGeneraotor());
                cmd.Parameters.AddWithValue("@pdName", guna2ComboBox1.Text);
                cmd.Parameters.AddWithValue("@companyName", guna2TextBox8.Text);
                cmd.Parameters.AddWithValue("@modelName", guna2TextBox1.Text);
                cmd.Parameters.AddWithValue("@price", guna2TextBox9.Text);
                cmd.Parameters.AddWithValue("@availableQuantity ", guna2TextBox10.Text);
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
        public string productIdGeneraotor()
        {
            int sn = 0;
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);
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
        public void bindAllProductData()
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
            //SqlDataAdapter sda = new SqlDataAdapter(query, con);
            //DataTable data = new DataTable();
            //sda.Fill(data);
            //guna2DataGridView1.DataSource = data;
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
                guna2ComboBox2.SelectedIndex = 0;
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
                        bindAllProductData();
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
                    bindAllProductData();
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

        private void refreshBt_Click(object sender, EventArgs e)
        {
            try
            {
                bindAllProductData();
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

        private void searchIDbt_Click(object sender, EventArgs e)
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

        private void productsAdmin_Load(object sender, EventArgs e)
        {
            try
            {
                bindAllProductData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (guna2ComboBox2.SelectedIndex == 1)
                {
                    bindAllInStockProductData();
                }
                else if (guna2ComboBox2.SelectedIndex == 2)
                {
                    bindAllOutOfStockProductData();
                }
                else
                {
                    bindAllProductData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bindAllOutOfStockProductData()
        {
            try
            {
                SqlConnection con = new SqlConnection(Program.connectionString);
                string query = "select * from ProductTB where availableQuantity <= 0";
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

        public void bindAllInStockProductData()
        {
            try
            {
                SqlConnection con = new SqlConnection(Program.connectionString);
                string query = "select * from ProductTB where availableQuantity > 0";
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
