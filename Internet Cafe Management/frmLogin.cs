using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Internet_Cafe_Management.ADO;

namespace Internet_Cafe_Management
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text;
            string passWord = txtPassword.Text;
            if (Login(userName,passWord))
            {
                Internet_Cafe_Management.DTO.Account loginAccount = AccountDAO.Instance.GetAccountByUserName(userName);
                frmComputerManager f = new frmComputerManager(loginAccount); //Chuyển tới form chính
                this.Hide();
                f.ShowDialog();
                this.Show();
            }
            else MessageBox.Show("Incorrect username or password", "Notification");
        }
        bool Login(string userName, string passWord)
        {
            return AccountDAO.Instance.Login(userName, passWord);
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void fLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Do you really want to exit", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                e.Cancel = true;
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }
    }

}
