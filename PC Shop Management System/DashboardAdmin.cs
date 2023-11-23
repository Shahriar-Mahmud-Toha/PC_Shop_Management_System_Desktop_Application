using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PC_Shop_Management_System
{
    public partial class DashboardAdmin : Form
    {
        public DashboardAdmin()
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

        private void exitAdminBt_Click(object sender, EventArgs e)
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

        private void noticeAdminBt_Click(object sender, EventArgs e)
        {
            try
            {
                noticeAdmin notAdmin = new noticeAdmin();
                notAdmin.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            accountSettingsAdmin sa = new accountSettingsAdmin();
            sa.ShowDialog();
        }

        private void DashboardAdmin_Load(object sender, EventArgs e)
        {
            try
            {
                welcomeText();
                bindTotalNumberOfProductSold();
                bindTotalSalesTB();
                bindTodaysTotalSalesTB();
                bindTotalNumberOfProductSoldWithDateTB();
                bindTotalSalesWithDateTB();
                bindTodaysTotalSalesWithDateTB();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bindTodaysTotalSalesWithDateTB()
        {
            try
            {
                List<string> axisName = new List<string>();
                List<double> values = new List<double>();

                SqlConnection con = new SqlConnection(Program.connectionString);
                string query = "select * from todaysTotalSalesWithDateTB";
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader sd = cmd.ExecuteReader();
                while (sd.Read())
                {
                    axisName.Add(sd["dates"].ToString());
                    values.Add((int)sd["todaysTotalSales"]);
                }
                con.Close();

                cartesianChart3.AxisX.Add(new Axis
                {
                    Title = "\nDates",
                    Labels = axisName
                });

                SeriesCollection series = new SeriesCollection();
                series.Add(new LineSeries() { Title = "Todays Total Sales BDT", Values = new ChartValues<double>(values) });
                cartesianChart3.Series = series;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bindTotalSalesWithDateTB()
        {
            try
            {
                List<string> axisName = new List<string>();
                List<double> values = new List<double>();

                SqlConnection con = new SqlConnection(Program.connectionString);
                string query = "select * from totalSalesWithDateTB";
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader sd = cmd.ExecuteReader();
                while (sd.Read())
                {
                    axisName.Add(sd["dates"].ToString());
                    values.Add((int)sd["totalSales"]);
                }
                con.Close();

                cartesianChart2.AxisX.Add(new Axis
                {
                    Title = "\nDates",
                    Labels = axisName
                });

                SeriesCollection series = new SeriesCollection();
                series.Add(new LineSeries() { Title = "Total Sales BDT", Values = new ChartValues<double>(values) });
                cartesianChart2.Series = series;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bindTotalNumberOfProductSoldWithDateTB()
        {
            try
            {
                List<string> axisName = new List<string>();
                List<double> values = new List<double>();

                SqlConnection con = new SqlConnection(Program.connectionString);
                string query = "select * from totalNumberOfProductSoldWithDateTB";
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader sd = cmd.ExecuteReader();
                while (sd.Read())
                {
                    axisName.Add(sd["dates"].ToString());
                    values.Add((int)sd["totalSold"]);
                }
                con.Close();

                cartesianChart1.AxisX.Add(new Axis
                {
                    Title = "\nDates",
                    Labels = axisName
                });

                SeriesCollection series = new SeriesCollection();
                series.Add(new LineSeries() { Title = "Total Number of Product Sold", Values = new ChartValues<double>(values) });
                cartesianChart1.Series = series;
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
                string query = "select userName from loginTB where userType='admin'";
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
        private void bindTodaysTotalSalesTB()
        {
            try
            {
                SqlConnection con = new SqlConnection(Program.connectionString);
                string query = "select * from todaysTotalSalesTB";
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader sd = cmd.ExecuteReader();
                if (sd.Read())
                {
                    label8.Text = sd["todaysTotalSales"].ToString();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bindTotalNumberOfProductSold()
        {
            try
            {
                SqlConnection con = new SqlConnection(Program.connectionString);
                string query = "select * from totalNumberOfProductSoldTB";
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader sd = cmd.ExecuteReader();
                if (sd.Read())
                {
                    label4.Text = sd["totalSold"].ToString();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bindTotalSalesTB()
        {
            try
            {
                SqlConnection con = new SqlConnection(Program.connectionString);
                string query = "select * from totalSalesTB";
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader sd = cmd.ExecuteReader();
                if (sd.Read())
                {
                    label5.Text = sd["totalSales"].ToString();
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
