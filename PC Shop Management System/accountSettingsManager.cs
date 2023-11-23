﻿using System;
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
    public partial class accountSettingsManager : Form
    {
        public accountSettingsManager()
        {
            InitializeComponent();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            if (userNameTb.Text != "" && passTb.Text != "")
            {
                if (userNameTb.Text.Length <= 28)
                {
                    if (passTb.Text.Length <= 36)
                    {
                        SqlConnection con = new SqlConnection(Program.connectionString);
                        string query = "update LoginTB set userName=@userName, password=@password where userType=@userType";
                        con.Open();
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@username", userNameTb.Text);
                        cmd.Parameters.AddWithValue("@password", passTb.Text);
                        cmd.Parameters.AddWithValue("@userType", "manager");
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Account Settings Saved Successfully", "Save Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Account Settings Not Saved", "Save Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        con.Close();
                        this.Close();
                        GC.Collect();
                    }
                    else
                    {
                        errorProvider2.SetError(passTb, "Password Length must be Less than or Equal to 36 Digit");
                        passTb.BorderColor = Color.FromArgb(255, 82, 96);
                    }
                }
                else
                {
                    errorProvider1.SetError(userNameTb, "Username Length must be Less than or Equal to 28 Digit");
                    userNameTb.BorderColor = Color.FromArgb(255, 82, 96);
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
