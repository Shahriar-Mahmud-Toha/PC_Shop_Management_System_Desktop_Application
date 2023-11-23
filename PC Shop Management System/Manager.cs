using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace PC_Shop_Management_System
{
    public partial class Manager : Form
    {
        public bool managerFrm = false;
        private bool clickedStatus;
        private DateTime end;
        private DateTime start;
        public Manager()
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

        private void Manager_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (!managerFrm)
                {
                    Application.Exit();
                }
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
                GC.Collect();
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void logoutManagerBt_Click(object sender, EventArgs e)
        {
            try
            {
                managerFrm = true;
                this.Close();
                Program.loginForm.Show();
                GC.Collect();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void managerNoticeBt_Click(object sender, EventArgs e)
        {
            try
            {
                noticeManager nm = new noticeManager();
                nm.ShowDialog();
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
                accountSettingsManager sm = new accountSettingsManager();
                sm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Manager_Load(object sender, EventArgs e)
        {
            try
            {
                welcomeText();
                stopwatch = new Stopwatch();
                stopwatch.Start();
                start = DateTime.Now;
                BindGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        Stopwatch stopwatch;
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
                cmd.Parameters.AddWithValue("@post", "Manager");
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
                cmd.Parameters.AddWithValue("@post", "Manager");
                cmd.ExecuteNonQuery();
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
                string query = "select userName from loginTB where userType='manager'";
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

        private void guna2Button9_Click(object sender, EventArgs e)
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
        void BindGridView()
        {
            try
            {
                DataGridViewImageColumn dvg = new DataGridViewImageColumn();
                dvg = (DataGridViewImageColumn)guna2DataGridView1.Columns[6];
                dvg.ImageLayout = DataGridViewImageCellLayout.Stretch;
                guna2DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                guna2DataGridView1.RowTemplate.Height = 100;

                SqlConnection con = new SqlConnection(Program.connectionString);
                string query = "select empNId,empName,post,empAddress,phoneNumber,email,picture from EmployeeTB";
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader sd = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(sd);
                guna2DataGridView1.DataSource = dt;
                con.Close();
                ResetControl();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Manager_FormClosing(object sender, FormClosingEventArgs e)
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
        }
        private int getSalary(string nid)
        {
            int Salary = 0;
            try
            {
                SqlConnection con = new SqlConnection(Program.connectionString);
                string query = "select salary from EmployeeTB where empNId=@nid";
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@nid", nid);
                SqlDataReader sd = cmd.ExecuteReader();
                if (sd.HasRows)
                {
                    if (sd.Read())
                    {
                        Salary = (int)sd["salary"];
                    }
                }
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Salary;
        }
        private int getReceivableSalary(string nid)
        {
            int Salary = 0;
            try
            {
                SqlConnection con = new SqlConnection(Program.connectionString);
                string query = "select receivableSalary from EmployeeTB where empNId=@nid";
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@nid", nid);
                SqlDataReader sd = cmd.ExecuteReader();
                if (sd.HasRows)
                {
                    if (sd.Read())
                    {
                        Salary = (int)sd["receivableSalary"];
                    }
                }
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Salary;
        }
        private int getWorkingDuration(string nid)
        {
            int duration = 0;
            try
            {
                SqlConnection con = new SqlConnection(Program.connectionString);
                string query = "select workingDuration from EmployeeTB where empNId=@nid";
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@nid", nid);
                SqlDataReader sd = cmd.ExecuteReader();
                if (sd.HasRows)
                {
                    if (sd.Read())
                    {
                        duration = (int)sd["workingDuration"];
                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return duration;
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
        private bool nameFormatChecker(string name, int maxLength)
        {
            bool flag = false;
            try
            {
                string errorMessage = null;
                bool lengthCheck = false, firstLetterCapitalCheck = false, onlyLetters = false;
                if (name.Length <= maxLength)
                {
                    lengthCheck = true;
                }
                else
                {
                    errorMessage += "* Maximum Length of the Name Exceeded\n";
                }
                if (name[0].ToString() == name[0].ToString().ToUpper() && name[0].ToString() != " ")
                {
                    firstLetterCapitalCheck = true;
                }
                else
                {
                    errorMessage += "* First Letter of the Name must be Capital\n";
                }
                if (firstLetterCapitalCheck)
                {
                    string ch = " ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
                    for (int i = 0; i < name.Length; i++)
                    {
                        if (!binarySearch(ch, name[i].ToString(), 0, ch.Length - 1))
                        {
                            onlyLetters = false;
                            errorMessage += "* Name can be consists only Alphanumeric Characters (A-Z,a-z)\n";
                            break;
                        }
                        else
                        {
                            onlyLetters = true;
                        }
                    }
                }

                if (lengthCheck && firstLetterCapitalCheck && onlyLetters)
                {
                    flag = true;
                }
                else
                {
                    MessageBox.Show(errorMessage, "Invalid Name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return flag;
        }
        private bool phoneNumberFormatChecker(string number)
        {
            bool flag = false;
            try
            {
                string errorMessage = null;
                bool lengthCheck = false, onlyNumbers = false;
                if (number.Length == 11)
                {
                    lengthCheck = true;
                }
                else
                {
                    errorMessage += "* Phone Number Length must be 11 Digit\n";
                }

                string ch = "0123456789";
                for (int i = 0; i < number.Length; i++)
                {
                    if (!binarySearch(ch, number[i].ToString(), 0, ch.Length - 1))
                    {
                        onlyNumbers = false;
                        errorMessage += "* Phone Number can be consists only Numbers (0-9)\n";
                        break;
                    }
                    else
                    {
                        onlyNumbers = true;
                    }
                }

                if (lengthCheck && onlyNumbers)
                {
                    flag = true;
                }
                else
                {
                    MessageBox.Show(errorMessage, "Invalid Phone Number", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flag;
        }
        private bool nidFormatChecker(string nid)
        {
            bool flag = false;
            try
            {
                string errorMessage = null;
                bool onlyNumbers = false;
                if (!(nid.Length <= 32))
                {
                    errorProvider2.SetError(guna2TextBox7, "NID Length must be Less than or Equal to 32 Digit");
                    guna2TextBox7.BorderColor = Color.FromArgb(255, 82, 96);
                    return false;
                }
                string ch = "0123456789";
                for (int i = 0; i < nid.Length; i++)
                {
                    if (!binarySearch(ch, nid[i].ToString(), 0, ch.Length - 1))
                    {
                        onlyNumbers = false;
                        errorMessage += "* NID can be consists only Numbers (0-9)\n";
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
                    MessageBox.Show(errorMessage, "Invalid NID", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return flag;
        }
        private bool emailFormatChecker(string email)
        {
            bool flag = false;
            try
            {
                string errorMessage = null;
                bool firstLetterCheck = false, allowedLettersCheck = false;
                if (!(email.Length <= 32))
                {
                    errorProvider5.SetError(guna2TextBox11, "Email Length must be Less than or Equal to 32 Digit");
                    guna2TextBox11.BorderColor = Color.FromArgb(255, 82, 96);
                    return false;
                }
                string ch = "abcdefghijklmnopqrstuvwxyz";

                if (binarySearch(ch, email[0].ToString(), 0, ch.Length - 1))
                {
                    firstLetterCheck = true;
                }
                else
                {
                    errorMessage += "* First Letter of the Email must be a Alphanumeric Character (a-z)\n";
                }
                if (firstLetterCheck)
                {
                    int atCount = 0, atPos = -1, dotPos = -1;
                    string ch1 = "-.0123456789@_abcdefghijklmnopqrstuvwxyz";
                    for (int i = 0; i < email.Length; i++)
                    {
                        if (!binarySearch(ch1, email[i].ToString(), 0, ch1.Length - 1))
                        {
                            allowedLettersCheck = false;
                            errorMessage += "* Email can be consists Letters (a-z), numbers, hyphen, ., and dashes (a-z),(0-9),@\n";
                            break;
                        }
                        if (email[i].ToString() == "@")
                        {
                            atCount++;
                            atPos = i;
                            if (email[i - 1].ToString() == "." || email[i - 1].ToString() == "-" || email[i - 1].ToString() == "_")
                            {
                                errorMessage += "* Email can not consists . or - or _ symbol immediate before or after @ symbol\n";
                                allowedLettersCheck = false;
                                break;
                            }
                        }
                        if (atCount > 1)
                        {
                            errorMessage += "* Email can be consists only one @ symbol\n";
                            allowedLettersCheck = false;
                            break;
                        }
                        if (email[i].ToString() == ".")
                        {
                            dotPos = i;
                            if (email[i - 1].ToString() == "." || email[i - 1].ToString() == "-" || email[i - 1].ToString() == "_")
                            {
                                errorMessage += "* Email can not consists . or - or _ symbol immediate before or after . symbol\n";
                                allowedLettersCheck = false;
                                break;
                            }
                        }
                        allowedLettersCheck = true;
                    }
                    if (atPos == -1 && allowedLettersCheck)
                    {
                        errorMessage += "* Email must consists @ symbol in Valid Position\n";
                        allowedLettersCheck = false;
                    }
                    if (dotPos == -1 && allowedLettersCheck)
                    {
                        errorMessage += "* Email must consists . symbol in Valid Position\n";
                        allowedLettersCheck = false;
                    }
                    if (atPos != -1 && allowedLettersCheck)
                    {
                        if (email[atPos + 1].ToString() == "." || email[atPos + 1].ToString() == "-" || email[atPos + 1].ToString() == "_")
                        {
                            errorMessage += "* Email can not consists . or - or _ symbol immediate before or after @ symbol\n";
                            allowedLettersCheck = false;
                        }
                    }
                    if (dotPos != -1 && allowedLettersCheck && (dotPos != email.Length - 1))
                    {
                        if (email[dotPos + 1].ToString() == "." || email[dotPos + 1].ToString() == "-" || email[dotPos + 1].ToString() == "_")
                        {
                            errorMessage += "* Email can not consists . or - or _ symbol immediate before or after . symbol\n";
                            allowedLettersCheck = false;
                        }
                    }
                    if (!(dotPos != -1) && allowedLettersCheck && !(dotPos != email.Length - 1) && !(dotPos > atPos))
                    {
                        errorMessage += "* Email can consists . symbol only in Valid position (example@mail.com)\n";
                        allowedLettersCheck = false;
                    }
                    if (allowedLettersCheck && !(((email.Length - 1) - dotPos) >= 2))
                    {
                        errorMessage += "* Email domain must be greater than or equal to 2 digit\n";
                        allowedLettersCheck = false;
                    }
                    for (int i = dotPos + 1; i < email.Length && allowedLettersCheck; i++)
                    {
                        if (!binarySearch(ch, email[i].ToString(), 0, ch.Length - 1))
                        {
                            errorMessage += "* In Email after . symbol which is after @ symbol, only Lowercase Letters (a-z) can consists\n";
                            allowedLettersCheck = false;
                            break;
                        }
                        else
                        {
                            allowedLettersCheck = true;
                        }
                    }
                }

                if (firstLetterCheck && allowedLettersCheck)
                {
                    flag = true;
                }
                else
                {
                    MessageBox.Show(errorMessage, "Invalid Email", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            return flag;
        }
        private bool addressLengthVerify()
        {
            if (guna2TextBox9.Text.Length <= 32)
            {
                return true;
            }
            errorProvider3.SetError(guna2TextBox9, "Address Length must be Less than or Equal to 32 Digit");
            guna2TextBox9.BorderColor = Color.FromArgb(255, 82, 96);
            return false;
        }
        private void insertBt_Click(object sender, EventArgs e)
        {
            try
            {
                if (guna2TextBox8.Text != "" && guna2TextBox7.Text != "" && guna2TextBox9.Text != "" && guna2TextBox10.Text != "" && guna2TextBox11.Text != "")
                {
                    if (!NIDMatchedOrNot(guna2TextBox7.Text))
                    {
                        if (!postMatchedOrNot(guna2ComboBox1.Text))
                        {
                            if (nameFormatChecker(guna2TextBox8.Text, 32) && nidFormatChecker(guna2TextBox7.Text) && addressLengthVerify() && phoneNumberFormatChecker(guna2TextBox10.Text) && emailFormatChecker(guna2TextBox11.Text))
                            {
                                SqlConnection con = new SqlConnection(Program.connectionString);
                                string query = "insert into EmployeeTB values(@empNId, @empName, @post, @salary, @receivableSalary, @workingDuration, @empAddress, @phoneNumber, @email, @picture)";
                                SqlCommand cmd = new SqlCommand(query, con);
                                con.Open();
                                cmd.Parameters.AddWithValue("@empNId", guna2TextBox7.Text);
                                cmd.Parameters.AddWithValue("@empName", guna2TextBox8.Text);
                                cmd.Parameters.AddWithValue("@post", guna2ComboBox1.Text);
                                cmd.Parameters.AddWithValue("@salary", getSalary(guna2TextBox7.Text));
                                cmd.Parameters.AddWithValue("@receivableSalary", getReceivableSalary(guna2TextBox7.Text));
                                cmd.Parameters.AddWithValue("@workingDuration", getWorkingDuration(guna2TextBox7.Text));
                                cmd.Parameters.AddWithValue("@empAddress", guna2TextBox9.Text);
                                cmd.Parameters.AddWithValue("@phoneNumber", guna2TextBox10.Text);
                                cmd.Parameters.AddWithValue("@email", guna2TextBox11.Text);
                                cmd.Parameters.AddWithValue("@picture", SavePhoto());

                                int a = cmd.ExecuteNonQuery();
                                if (a > 0)
                                {
                                    MessageBox.Show("Data Inserted Successfully", "Data Inserion Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    BindGridView();
                                    ResetControl();
                                }
                                else
                                {
                                    MessageBox.Show("Data not Inserted", "Data Inserion Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                }
                                con.Close();
                            }
                        }
                        else
                        {
                            errorProvider7.SetError(guna2ComboBox1, "An Employee with Same Post already Exists. Choose a different Post for this Employee.");
                            guna2ComboBox1.BorderColor = Color.FromArgb(255, 82, 96);
                        }
                    }
                    else
                    {
                        errorProvider2.SetError(guna2TextBox7, "Duplicate NID Found. NID must be Unique.");
                        guna2TextBox7.BorderColor = Color.FromArgb(255, 82, 96);
                    }
                }
                else
                {
                    if (guna2TextBox8.Text == "")
                    {
                        errorProvider1.SetError(guna2TextBox8, "Name cannot be blank.");
                        guna2TextBox8.BorderColor = Color.FromArgb(255, 82, 96);
                    }
                    if (guna2TextBox7.Text == "")
                    {
                        errorProvider2.SetError(guna2TextBox7, "NID Number cannot be blank.");
                        guna2TextBox7.BorderColor = Color.FromArgb(255, 82, 96);
                    }
                    if (guna2TextBox9.Text == "")
                    {
                        errorProvider3.SetError(guna2TextBox9, "Address cannot be blank.");
                        guna2TextBox9.BorderColor = Color.FromArgb(255, 82, 96);
                    }
                    if (guna2TextBox10.Text == "")
                    {
                        errorProvider4.SetError(guna2TextBox10, "Phone Number cannot be blank.");
                        guna2TextBox10.BorderColor = Color.FromArgb(255, 82, 96);
                    }
                    if (guna2TextBox11.Text == "")
                    {
                        errorProvider5.SetError(guna2TextBox11, "Email cannot be blank.");
                        guna2TextBox11.BorderColor = Color.FromArgb(255, 82, 96);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Data Insertion Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool NIDMatchedOrNot(string nid)
        {
            bool flag = false;
            try
            {
                SqlConnection con = new SqlConnection(Program.connectionString);
                string query = "select empNId from EmployeeTB where empNId=@nid";
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@nid", nid);
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
        private bool postWithThisNidHaveOrNot(string nid, string post)
        {
            bool flag = false;
            try
            {
                SqlConnection con = new SqlConnection(Program.connectionString);
                string query = "select post from EmployeeTB where empNId!=@nid and post=@post";
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@nid", nid);
                cmd.Parameters.AddWithValue("@post", post);
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
        private bool postMatchedOrNot(string post)
        {
            bool flag = false;
            try
            {
                SqlConnection con = new SqlConnection(Program.connectionString);
                string query = "select post from EmployeeTB where post=@post";
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@post", post);
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
        private byte[] SavePhoto()
        {
            MemoryStream ms = new MemoryStream();
            guna2PictureBox1.Image.Save(ms, guna2PictureBox1.Image.RawFormat);
            return ms.GetBuffer();
        }
        void ResetControl()
        {
            try
            {
                guna2TextBox11.Clear();
                guna2TextBox4.Clear();
                guna2TextBox10.Clear();
                guna2TextBox9.Clear();
                guna2TextBox8.Clear();
                guna2TextBox7.Clear();
                errorProvider1.Clear();
                errorProvider2.Clear();
                errorProvider3.Clear();
                errorProvider4.Clear();
                errorProvider5.Clear();
                errorProvider6.Clear();
                errorProvider7.Clear();
                guna2TextBox8.BorderColor = Color.FromArgb(22, 24, 28);
                guna2TextBox7.BorderColor = Color.FromArgb(22, 24, 28);
                guna2TextBox9.BorderColor = Color.FromArgb(22, 24, 28);
                guna2TextBox10.BorderColor = Color.FromArgb(22, 24, 28);
                guna2TextBox11.BorderColor = Color.FromArgb(22, 24, 28);
                guna2TextBox4.BorderColor = Color.FromArgb(22, 24, 28);
                guna2ComboBox1.BorderColor = Color.FromArgb(217, 221, 226);
                guna2PictureBox1.Image = Properties.Resources.user_white1;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private Image GetPhoto(byte[] photo)
        {
            MemoryStream ms = new MemoryStream(photo);
            return Image.FromStream(ms);
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "Please select image.";
                ofd.Filter = "Image File (*.png, *.jpg, *.jpeg;) | *.png;*.jpg;*.jpeg;";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    guna2PictureBox1.Image = new Bitmap(ofd.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void updateBt_Click(object sender, EventArgs e)
        {
            try
            {
                if (NIDMatchedOrNot(guna2TextBox7.Text))
                {
                    if (!postWithThisNidHaveOrNot(guna2TextBox7.Text, guna2ComboBox1.Text))
                    {
                        if (nameFormatChecker(guna2TextBox8.Text, 32) && nidFormatChecker(guna2TextBox7.Text) && addressLengthVerify() && phoneNumberFormatChecker(guna2TextBox10.Text) && emailFormatChecker(guna2TextBox11.Text))
                        {
                            SqlConnection con = new SqlConnection(Program.connectionString);
                            string query = "update EmployeeTB set empName=@empName, post=@post, empAddress=@empAddress, phoneNumber=@phoneNumber, email=@email, picture=@picture where empNId=@empNId";
                            con.Open();
                            SqlCommand cmd = new SqlCommand(query, con);
                            cmd.Parameters.AddWithValue("@empNId", guna2TextBox7.Text);
                            cmd.Parameters.AddWithValue("@empName", guna2TextBox8.Text);
                            cmd.Parameters.AddWithValue("@post", guna2ComboBox1.Text);
                            cmd.Parameters.AddWithValue("@phoneNumber", guna2TextBox10.Text);
                            cmd.Parameters.AddWithValue("@email", guna2TextBox11.Text);
                            cmd.Parameters.AddWithValue("@empAddress", guna2TextBox9.Text);
                            cmd.Parameters.AddWithValue("@picture", SavePhoto());
                            int a = cmd.ExecuteNonQuery();
                            if (a > 0)
                            {
                                MessageBox.Show("Data updated Successfully", "Data Update Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                ResetControl();
                                BindGridView();
                            }
                            else
                            {
                                MessageBox.Show("Data Not Updated", "Data Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            con.Close();
                        }
                    }
                    else
                    {
                        errorProvider7.SetError(guna2ComboBox1, "An Employee with Same Post already Exists. Choose a different Post for this Employee.");
                        guna2ComboBox1.BorderColor = Color.FromArgb(255, 82, 96);
                    }
                }
                else
                {
                    errorProvider2.SetError(guna2TextBox7, "No Employee Found with this NID. Select any Existing Employee.");
                    guna2TextBox7.BorderColor = Color.FromArgb(255, 82, 96);
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
                guna2TextBox7.Text = guna2DataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                guna2TextBox8.Text = guna2DataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                guna2ComboBox1.Text = guna2DataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                guna2TextBox9.Text = guna2DataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                guna2TextBox10.Text = guna2DataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                guna2TextBox11.Text = guna2DataGridView1.SelectedRows[0].Cells[5].Value.ToString();
                guna2PictureBox1.Image = GetPhoto((byte[])guna2DataGridView1.SelectedRows[0].Cells[6].Value);

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
                    string query = "select empNId,empName,post,empAddress,phoneNumber,email,picture from EmployeeTB where empName=@searchedName";
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
                        MessageBox.Show("No Employee Found", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    con.Close();
                }
                else
                {
                    errorProvider6.SetError(guna2TextBox4, "You have to enter a Name or ID first");
                    guna2TextBox4.BorderColor = Color.FromArgb(255, 82, 96);
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
                    string query = "select empNId,empName,post,empAddress,phoneNumber,email,picture from EmployeeTB where empNId=@searchedNId";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@searchedNId", guna2TextBox4.Text);
                    SqlDataReader sd = cmd.ExecuteReader();
                    if (sd.HasRows)
                    {
                        DataTable dt = new DataTable();
                        dt.Load(sd);
                        guna2DataGridView1.DataSource = dt;
                    }
                    else
                    {
                        MessageBox.Show("No Employee Found", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    con.Close();
                }
                else
                {
                    errorProvider6.SetError(guna2TextBox4, "You have to enter a Name or ID first");
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
                ResetControl();
                BindGridView();
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

        private void guna2TextBox7_TextChanged(object sender, EventArgs e)
        {
            try
            {
                errorProvider2.Clear();
                guna2TextBox7.BorderColor = Color.FromArgb(22, 24, 28);
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

        private void guna2TextBox11_TextChanged(object sender, EventArgs e)
        {
            try
            {
                errorProvider5.Clear();
                guna2TextBox11.BorderColor = Color.FromArgb(22, 24, 28);
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
                errorProvider6.Clear();
                guna2TextBox4.BorderColor = Color.FromArgb(22, 24, 28);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                errorProvider7.Clear();
                guna2ComboBox1.BorderColor = Color.FromArgb(217, 221, 226);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
