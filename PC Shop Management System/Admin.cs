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
    public partial class Admin : Form
    {
        public bool adminFrmClose = false;
        public Admin()
        {
            InitializeComponent();
        }

        private void guna2CircleButton1_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Logout", logoutAdminBt);
        }

        private void logoutAdminBt_Click(object sender, EventArgs e)
        {
            adminFrmClose = true;
            this.Close();
            Program.loginForm.Show();
        }
        private void Admin_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!adminFrmClose)
            {
                Application.Exit();
            }
            GC.Collect();
        }

        private void productsButtonAdmin_Click(object sender, EventArgs e)
        {
            dashboardButtonAdmin.ForeColor = Color.White;
            dashboardButtonAdmin.IconColor = Color.White;
            overviewButtonLBorderAdmin.BackColor = Color.Transparent;

            productsButtonAdmin.ForeColor = Color.FromArgb(45, 156, 252);
            productsButtonAdmin.IconColor = Color.FromArgb(45, 156, 252);
            productsButtonLBorderAdmin.BackColor = Color.FromArgb(45, 156, 252);

            employeesButtonAdmin.ForeColor = Color.White;
            employeesButtonAdmin.IconColor = Color.White;
            employeeButtonLBorderAdmin.BackColor = Color.Transparent;

            salarySheetButtonAdmin.ForeColor = Color.White;
            salarySheetButtonAdmin.IconColor = Color.White;
            salarySheetButtonLBorderAdmin.BackColor = Color.Transparent;

            discountsButtonAdmin.ForeColor = Color.White;
            discountsButtonAdmin.IconColor = Color.White;
            discountsButtonLBorderAdmin.BackColor = Color.Transparent;

            passwordButtonAdmin.ForeColor = Color.White;
            passwordButtonAdmin.IconColor = Color.White;
            passwordsButtonLBorderAdmin.BackColor = Color.Transparent;

            da.Visible = false;
            sa.Visible = false;
            pa.Visible = true;
            dis.Visible = false;
            ea.Visible = false;
            pas.Visible = false;
        }

        private void employeesButtonAdmin_Click(object sender, EventArgs e)
        {
            dashboardButtonAdmin.ForeColor = Color.White;
            dashboardButtonAdmin.IconColor = Color.White;
            overviewButtonLBorderAdmin.BackColor = Color.Transparent;

            productsButtonAdmin.ForeColor = Color.White;
            productsButtonAdmin.IconColor = Color.White;
            productsButtonLBorderAdmin.BackColor = Color.Transparent;

            employeesButtonAdmin.ForeColor = Color.FromArgb(45, 156, 252);
            employeesButtonAdmin.IconColor = Color.FromArgb(45, 156, 252);
            employeeButtonLBorderAdmin.BackColor = Color.FromArgb(45, 156, 252);

            salarySheetButtonAdmin.ForeColor = Color.White;
            salarySheetButtonAdmin.IconColor = Color.White;
            salarySheetButtonLBorderAdmin.BackColor = Color.Transparent;

            discountsButtonAdmin.ForeColor = Color.White;
            discountsButtonAdmin.IconColor = Color.White;
            discountsButtonLBorderAdmin.BackColor = Color.Transparent;

            passwordButtonAdmin.ForeColor = Color.White;
            passwordButtonAdmin.IconColor = Color.White;
            passwordsButtonLBorderAdmin.BackColor = Color.Transparent;

            da.Visible = false;
            sa.Visible = false;
            pa.Visible = false;
            dis.Visible = false;
            ea.Visible = true;
            pas.Visible = false;
        }


        private void discountsButtonAdmin_Click(object sender, EventArgs e)
        {
            dashboardButtonAdmin.ForeColor = Color.White;
            dashboardButtonAdmin.IconColor = Color.White;
            overviewButtonLBorderAdmin.BackColor = Color.Transparent;

            productsButtonAdmin.ForeColor = Color.White;
            productsButtonAdmin.IconColor = Color.White;
            productsButtonLBorderAdmin.BackColor = Color.Transparent;

            employeesButtonAdmin.ForeColor = Color.White;
            employeesButtonAdmin.IconColor = Color.White;
            employeeButtonLBorderAdmin.BackColor = Color.Transparent;

            salarySheetButtonAdmin.ForeColor = Color.White;
            salarySheetButtonAdmin.IconColor = Color.White;
            salarySheetButtonLBorderAdmin.BackColor = Color.Transparent;

            discountsButtonAdmin.ForeColor = Color.FromArgb(45, 156, 252);
            discountsButtonAdmin.IconColor = Color.FromArgb(45, 156, 252);
            discountsButtonLBorderAdmin.BackColor = Color.FromArgb(45, 156, 252);

            passwordButtonAdmin.ForeColor = Color.White;
            passwordButtonAdmin.IconColor = Color.White;
            passwordsButtonLBorderAdmin.BackColor = Color.Transparent;

            da.Visible = false;
            sa.Visible = false;
            pa.Visible = false;
            dis.Visible = true;
            ea.Visible = false;
            pas.Visible = false;
        }

        private void passwordButtonAdmin_Click(object sender, EventArgs e)
        {
            dashboardButtonAdmin.ForeColor = Color.White;
            dashboardButtonAdmin.IconColor = Color.White;
            overviewButtonLBorderAdmin.BackColor = Color.Transparent;

            productsButtonAdmin.ForeColor = Color.White;
            productsButtonAdmin.IconColor = Color.White;
            productsButtonLBorderAdmin.BackColor = Color.Transparent;

            employeesButtonAdmin.ForeColor = Color.White;
            employeesButtonAdmin.IconColor = Color.White;
            employeeButtonLBorderAdmin.BackColor = Color.Transparent;

            salarySheetButtonAdmin.ForeColor = Color.White;
            salarySheetButtonAdmin.IconColor = Color.White;
            salarySheetButtonLBorderAdmin.BackColor = Color.Transparent;

            discountsButtonAdmin.ForeColor = Color.White;
            discountsButtonAdmin.IconColor = Color.White;
            discountsButtonLBorderAdmin.BackColor = Color.Transparent;

            passwordButtonAdmin.ForeColor = Color.FromArgb(45, 156, 252);
            passwordButtonAdmin.IconColor = Color.FromArgb(45, 156, 252);
            passwordsButtonLBorderAdmin.BackColor = Color.FromArgb(45, 156, 252);

            da.Visible = false;
            sa.Visible = false;
            pa.Visible = false;
            dis.Visible = false;
            ea.Visible = false;
            pas.Visible = true;
        }

        private void overviewButtonAdmin_Click(object sender, EventArgs e)
        {
            dashboardButtonAdmin.ForeColor = Color.FromArgb(45, 156, 252);
            dashboardButtonAdmin.IconColor = Color.FromArgb(45, 156, 252);
            overviewButtonLBorderAdmin.BackColor = Color.FromArgb(45, 156, 252);

            productsButtonAdmin.ForeColor = Color.White;
            productsButtonAdmin.IconColor = Color.White;
            productsButtonLBorderAdmin.BackColor = Color.Transparent;

            employeesButtonAdmin.ForeColor = Color.White;
            employeesButtonAdmin.IconColor = Color.White;
            employeeButtonLBorderAdmin.BackColor = Color.Transparent;

            salarySheetButtonAdmin.ForeColor = Color.White;
            salarySheetButtonAdmin.IconColor = Color.White;
            salarySheetButtonLBorderAdmin.BackColor = Color.Transparent;

            discountsButtonAdmin.ForeColor = Color.White;
            discountsButtonAdmin.IconColor = Color.White;
            discountsButtonLBorderAdmin.BackColor = Color.Transparent;

            passwordButtonAdmin.ForeColor = Color.White;
            passwordButtonAdmin.IconColor = Color.White;
            passwordsButtonLBorderAdmin.BackColor = Color.Transparent;

            da.Visible = true;
            sa.Visible = false;
            pa.Visible = false;
            dis.Visible = false;
            ea.Visible = false;
            pas.Visible = false;
        }

        private void salarySheetButtonAdmin_Click(object sender, EventArgs e)
        {
            dashboardButtonAdmin.ForeColor = Color.White;
            dashboardButtonAdmin.IconColor = Color.White;
            overviewButtonLBorderAdmin.BackColor = Color.Transparent;

            productsButtonAdmin.ForeColor = Color.White;
            productsButtonAdmin.IconColor = Color.White;
            productsButtonLBorderAdmin.BackColor = Color.Transparent;

            employeesButtonAdmin.ForeColor = Color.White;
            employeesButtonAdmin.IconColor = Color.White;
            employeeButtonLBorderAdmin.BackColor = Color.Transparent;

            salarySheetButtonAdmin.ForeColor = Color.FromArgb(45, 156, 252);
            salarySheetButtonAdmin.IconColor = Color.FromArgb(45, 156, 252);
            salarySheetButtonLBorderAdmin.BackColor = Color.FromArgb(45, 156, 252);

            discountsButtonAdmin.ForeColor = Color.White;
            discountsButtonAdmin.IconColor = Color.White;
            discountsButtonLBorderAdmin.BackColor = Color.Transparent;

            passwordButtonAdmin.ForeColor = Color.White;
            passwordButtonAdmin.IconColor = Color.White;
            passwordsButtonLBorderAdmin.BackColor = Color.Transparent;

            da.Visible = false;
            sa.Visible = true;
            pa.Visible = false;
            dis.Visible = false;
            ea.Visible = false;
            pas.Visible = false;
        }

        private DashboardAdmin da = new DashboardAdmin();
        private employeeAdmin ea = new employeeAdmin();
        private productsAdmin pa = new productsAdmin();
        private salarySheetAdmin sa = new salarySheetAdmin();
        private discountsOrCuponnAdmin dis = new discountsOrCuponnAdmin();
        private passwordsAdmin pas = new passwordsAdmin();
        private void Admin_Load(object sender, EventArgs e)
        {
            da.TopLevel = false;
            OptionContainer.Controls.Add(da);
            da.Dock = DockStyle.Fill;
            da.BringToFront();
            da.Show();

            ea.TopLevel = false;
            OptionContainer.Controls.Add(ea);
            ea.Dock = DockStyle.Fill;
            da.Show();

            pa.TopLevel = false;
            OptionContainer.Controls.Add(pa);
            pa.Dock = DockStyle.Fill;
            pa.BringToFront();
            pa.Show();

            sa.TopLevel = false;
            OptionContainer.Controls.Add(sa);
            sa.Dock = DockStyle.Fill;
            sa.BringToFront();
            sa.Show();

            dis.TopLevel = false;
            OptionContainer.Controls.Add(dis);
            dis.Dock = DockStyle.Fill;
            dis.BringToFront();
            dis.Show();

            pas.TopLevel = false;
            OptionContainer.Controls.Add(pas);
            pas.Dock = DockStyle.Fill;
            pas.BringToFront();
            pas.Show();

            dashboardButtonAdmin.ForeColor = Color.FromArgb(45, 156, 252);
            dashboardButtonAdmin.IconColor = Color.FromArgb(45, 156, 252);
            overviewButtonLBorderAdmin.BackColor = Color.FromArgb(45, 156, 252);

            productsButtonAdmin.ForeColor = Color.White;
            productsButtonAdmin.IconColor = Color.White;
            productsButtonLBorderAdmin.BackColor = Color.Transparent;

            employeesButtonAdmin.ForeColor = Color.White;
            employeesButtonAdmin.IconColor = Color.White;
            employeeButtonLBorderAdmin.BackColor = Color.Transparent;

            salarySheetButtonAdmin.ForeColor = Color.White;
            salarySheetButtonAdmin.IconColor = Color.White;
            salarySheetButtonLBorderAdmin.BackColor = Color.Transparent;

            discountsButtonAdmin.ForeColor = Color.White;
            discountsButtonAdmin.IconColor = Color.White;
            discountsButtonLBorderAdmin.BackColor = Color.Transparent;

            passwordButtonAdmin.ForeColor = Color.White;
            passwordButtonAdmin.IconColor = Color.White;
            passwordsButtonLBorderAdmin.BackColor = Color.Transparent;

            da.Visible = true;
            sa.Visible = false;
            pa.Visible = false;
            dis.Visible = false;
            ea.Visible = false;
            pas.Visible = false;
        }
    }
}
