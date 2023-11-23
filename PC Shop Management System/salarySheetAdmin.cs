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
using System.IO;

namespace PC_Shop_Management_System
{
    public partial class salarySheetAdmin : Form
    {
        public salarySheetAdmin()
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

        private void salarySheetAdmin_Load(object sender, EventArgs e)
        {
            try
            {
                loadPathFromDb();
                if (checkAllEmpSalIsZeroOrNot())
                {
                    bindRegularWDtoEmpWD();
                    bindReceivableSalaryToEmpTB();
                    bindEmployeeTB();
                }
                else
                {
                    MessageBox.Show("Reminder: There are some Employees whose salary has not been set yet.\n\nTo view Salary Sheet You have to set all the Employees Salaries first from \"Employees\" option.\n\nAfter set salaries, you have to re-login for effect.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool checkAllEmpSalIsZeroOrNot()
        {
            bool flag = false;
            try
            {
                SqlConnection con = new SqlConnection(Program.connectionString);
                string query = "select * from EmployeeTB where salary=0";
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader sd = cmd.ExecuteReader();
                if (sd.HasRows)
                {
                    flag = false;
                }
                else
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

        private void bindReceivableSalaryToEmpTB()
        {
            try
            {
                SqlConnection con = new SqlConnection(Program.connectionString);
                string query = "select empNId, salary, workingDuration from EmployeeTB where salary!=0";
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader sd = cmd.ExecuteReader();
                if (sd.HasRows)
                {
                    while (sd.Read())
                    {
                        double sal = Math.Round(((int)sd["workingDuration"] / 3600.0) * (double)(((int)sd["salary"]) / 240.0));
                        string qr = "update EmployeeTB set receivableSalary=@receivableSalary where empNId=@empNId";
                        SqlConnection cn = new SqlConnection(Program.connectionString);
                        cn.Open();
                        SqlCommand cmd1 = new SqlCommand(qr, cn);
                        cmd1.Parameters.AddWithValue("@receivableSalary", (int)sal);
                        cmd1.Parameters.AddWithValue("@empNId", sd["empNId"].ToString());
                        cmd1.ExecuteNonQuery();
                        cn.Close();
                    }
                }
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bindEmployeeTB()
        {
            try
            {
                SqlConnection con = new SqlConnection(Program.connectionString);
                string query = "select empNId, empName, cast(workingDuration/3600 as decimal(10,1)) workingDuration, receivableSalary from EmployeeTB";
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

        private void bindRegularWDtoEmpWD()
        {
            try
            {
                SqlConnection con = new SqlConnection(Program.connectionString);
                string query = "select * from RegularWorkingDurationTB";
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader sd = cmd.ExecuteReader();
                int count = 0;
                while (sd.Read())
                {
                    if (count == 0)
                    {
                        string qr = "update EmployeeTB set workingDuration=@duration where post=@post";
                        SqlConnection cn = new SqlConnection(Program.connectionString);
                        cn.Open();
                        SqlCommand cmd1 = new SqlCommand(qr, cn);
                        cmd1.Parameters.AddWithValue("@duration", (int)sd["duration"]);
                        cmd1.Parameters.AddWithValue("@post", "Data Entry Operator");
                        cmd1.ExecuteNonQuery();
                        cn.Close();
                        count++;
                    }
                    else if (count == 1)
                    {
                        string qr = "update EmployeeTB set workingDuration=@duration where post=@post";
                        SqlConnection cn = new SqlConnection(Program.connectionString);
                        cn.Open();
                        SqlCommand cmd1 = new SqlCommand(qr, cn);
                        cmd1.Parameters.AddWithValue("@duration", (int)sd["duration"]);
                        cmd1.Parameters.AddWithValue("@post", "Manager");
                        cmd1.ExecuteNonQuery();
                        cn.Close();
                        count++;
                    }
                    else if (count == 2)
                    {
                        string qr = "update EmployeeTB set workingDuration=@duration where post=@post";
                        SqlConnection cn = new SqlConnection(Program.connectionString);
                        cn.Open();
                        SqlCommand cmd1 = new SqlCommand(qr, cn);
                        cmd1.Parameters.AddWithValue("@duration", (int)sd["duration"]);
                        cmd1.Parameters.AddWithValue("@post", "Salesman");
                        cmd1.ExecuteNonQuery();
                        cn.Close();
                        count++;
                    }
                }
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void browseBt_Click(object sender, EventArgs e)
        {
            try
            {
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    exportPath.Text = folderBrowserDialog1.SelectedPath + @"\Salary-Sheet.txt";
                    savePathToDB();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        private void savePathToDB()
        {
            try
            {
                string query = "update defaultSalExportPathTB set pathLoc=@pathLoc where sn=0";
                SqlConnection con = new SqlConnection(Program.connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@pathLoc", exportPath.Text);
                cmd.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void loadPathFromDb()
        {
            try
            {
                string query = "select * from defaultSalExportPathTB";
                SqlConnection con = new SqlConnection(Program.connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader sd = cmd.ExecuteReader();
                if (sd.Read())
                {
                    exportPath.Text = sd["pathLoc"].ToString();
                }
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exportBt_Click(object sender, EventArgs e)
        {
            //DataTable table = new DataTable();
            //table.Columns.Add("NID", typeof(string));
            //table.Columns.Add("Name", typeof(string));
            //table.Columns.Add("Working Duration (Hours)", typeof(int));
            //table.Columns.Add("Salary", typeof(int));
            //table.Rows.Add()
            try
            {
                TextWriter writer = new StreamWriter(exportPath.Text);
                writer.Write("      -------------------- Salary Sheet ---------------------\n      -------------------------------------------------------\n");
                for (int i = 0; i < guna2DataGridView1.Rows.Count; i++)
                {
                    for (int j = 0; j < guna2DataGridView1.Columns.Count; j++)
                    {
                        writer.Write("\t" + guna2DataGridView1.Rows[i].Cells[j].Value.ToString() + "\t" + "|");
                    }
                    writer.WriteLine("");
                    writer.WriteLine("      -------------------------------------------------------");
                }
                writer.WriteLine("\n\n\tExported Date and Time: " + DateTime.Now);
                writer.Close();
                MessageBox.Show("Data Exported", "Export Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception name: " + ex.Message, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

