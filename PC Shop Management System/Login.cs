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
    public partial class Login : Form
    {
        bool mouseDown;
        private Point offset;
        public Login()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            offset.X = e.X;
            offset.Y = e.Y;
            mouseDown = true;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                Point currScrPos = PointToScreen(e.Location);
                Location = new Point(currScrPos.X - offset.X, currScrPos.Y - offset.Y);
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (userNameTb.Text != "" && passTb.Text != "")
                {
                    int a = accountValidation();

                    if (a == 1)
                    {
                        this.Hide();
                        clearPreviousDaysTotalSaleInfo();
                        saveLoginDate();
                        Program.adminForm = new Admin();
                        Program.adminForm.Show();
                        userNameTb.Clear();
                        passTb.Clear();
                        GC.Collect();
                    }
                    else if (a == 2)
                    {
                        this.Hide();
                        clearPreviousDaysTotalSaleInfo();
                        saveLoginDate();
                        Program.salesmanForm = new SalesMan();
                        Program.salesmanForm.Show();
                        userNameTb.Clear();
                        passTb.Clear();
                        GC.Collect();
                    }
                    else if (a == 3)
                    {
                        this.Hide();
                        clearPreviousDaysTotalSaleInfo();
                        saveLoginDate();
                        Program.dataEntryOperatorForm = new DataEntryOperator();
                        Program.dataEntryOperatorForm.Show();
                        userNameTb.Clear();
                        passTb.Clear();
                        GC.Collect();
                    }
                    else if (a == 4)
                    {
                        this.Hide();
                        clearPreviousDaysTotalSaleInfo();
                        saveLoginDate();
                        Program.managerForm = new Manager();
                        Program.managerForm.Show();
                        userNameTb.Clear();
                        passTb.Clear();
                        GC.Collect();
                    }
                    else
                    {
                        MessageBox.Show("ID and Password does not Matched", "Login Failed", MessageBoxButtons.OK ,MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    if (userNameTb.Text == "")
                    {
                        errorProvider1.SetError(userNameTb, "Username cannot be blank.");
                        userNameTb.BorderColor = Color.FromArgb(255, 82, 96);
                    }
                    if (passTb.Text == "")
                    {
                        errorProvider2.SetError(passTb, "Password cannot be blank.");
                        passTb.BorderColor = Color.FromArgb(255, 82, 96);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void saveLoginDate()
        {
            SqlConnection con = new SqlConnection(Program.connectionString);
            string query = "update lastLoginDateTB set lastLoginDate=@lastLoginDate where sn=0";
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@lastLoginDate", DateTime.Now.ToString("MM/dd/yyyy"));
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void clearPreviousDaysTotalSaleInfo()
        {
            if (getlastLoginDate() != DateTime.Now.ToString("MM/dd/yyyy"))
            {
                SqlConnection con = new SqlConnection(Program.connectionString);
                string query = "update todaysTotalSalesTB set todaysTotalSales=0 where sn=0";
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        private string getlastLoginDate()
        {
            string date = null;
            SqlConnection con = new SqlConnection(Program.connectionString);
            string query = "select lastLoginDate from lastLoginDateTB";
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader sd = cmd.ExecuteReader();
            if (sd.Read())
            {
                date = sd["lastLoginDate"].ToString();
            }
            con.Close();
            return date;
        }

        private int accountValidation()
        {
            string userType = null;
            SqlConnection con = new SqlConnection(Program.connectionString);
            string query = "select * from loginTB where userName=@username and password=@password";
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@username", userNameTb.Text);
            cmd.Parameters.AddWithValue("@password", passTb.Text);
            SqlDataReader sd = cmd.ExecuteReader();
            if (sd.HasRows)
            {
                if (sd.Read())
                {
                    userType = sd["userType"].ToString();
                }
            }
            con.Close();

            if (userType == "admin")
            {
                return 1;
            }
            else if (userType == "salesman")
            {
                return 2;
            }
            else if (userType == "deo")
            {
                return 3;
            }
            else if (userType == "manager")
            {
                return 4;
            }
            return 0;
        }

        private void forgetPassButton_Click(object sender, EventArgs e)
        {
            forgetPassword fp = new forgetPassword();
            fp.ShowDialog();
        }

        private void infoButton_Click(object sender, EventArgs e)
        {
            info inf = new info();
            inf.ShowDialog();
        }

        private void userNameTb_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            userNameTb.BorderColor = Color.FromArgb(22, 24, 28);
        }

        private void passTb_TextChanged(object sender, EventArgs e)
        {
            errorProvider2.Clear();
            passTb.BorderColor = Color.FromArgb(22, 24, 28);
        }
    }
}
