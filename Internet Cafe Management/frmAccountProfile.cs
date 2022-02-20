using Internet_Cafe_Management.ADO;
using Internet_Cafe_Management.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Internet_Cafe_Management
{
    public partial class frmAccountProfile : Form
    {
        private Account loginAccount;
        public Account LoginAccount
        {
            get { return loginAccount; }
            set { loginAccount = value; ChangeAccount(loginAccount); }
        }
        
        public frmAccountProfile(Account acc)
        {
            InitializeComponent();
            LoginAccount = acc;
        }
        void ChangeAccount(Account acc)
        {
            txtUserName.Text = LoginAccount.UserName;
            txtDisplayName.Text = LoginAccount.DisplayName;
        }
        void UpdateAccountInfo()
        {
            string displayName = txtDisplayName.Text;
            string password = txtPassWord.Text;
            string newpass = txtNewPass.Text;
            string reenterPass = txtReEnterPass.Text;
            string userName = txtUserName.Text;

            if (!newpass.Equals(reenterPass)) MessageBox.Show("Please fill in right password like the new password!");

            else
            {
                if (AccountDAO.Instance.UpdateAccount(userName, displayName, password, newpass))
                {
                    MessageBox.Show("Update successful!");
                    if (updateAccount != null)
                        updateAccount(this, new AccountEvent(AccountDAO.Instance.GetAccountByUserName(userName)));
                }
                else
                    MessageBox.Show("Please fill in right password!");
            }
        }
        private event EventHandler<AccountEvent> updateAccount;
        public event EventHandler<AccountEvent> UpdateAccount
        {
            add { updateAccount += value; }
            remove { updateAccount -= value; }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateAccountInfo();
        }
        public class AccountEvent : EventArgs
        {
            private Account acc;
            public Account Acc
            {
                get { return acc; }
                set { acc = value; }
            }

            public AccountEvent(Account acc)
            {
                this.Acc = acc;
            }
        }

        private void txtPassWord_Enter(object sender, EventArgs e)
        {
            if (txtPassWord.Text == "Enter Password")
            {
                txtPassWord.Text = "";
                txtPassWord.ForeColor = Color.Black;
                txtPassWord.UseSystemPasswordChar = true;
            }
        }
        private void txtPassWord_Leave(object sender, EventArgs e)
        {
            if(txtPassWord.Text == "")
            {
                txtPassWord.UseSystemPasswordChar = false;
                txtPassWord.ForeColor = Color.DarkGray;
                txtPassWord.Text = "Enter Password";
            }
        }
        private void txtNewPass_Enter(object sender, EventArgs e)
        {
            if (txtNewPass.Text == "Enter New Password")
            {
                txtNewPass.Text = "";
                txtNewPass.ForeColor = Color.Black;
                txtNewPass.UseSystemPasswordChar = true;
            }
        }
        private void txtNewPass_Leave(object sender, EventArgs e)
        {
            if (txtNewPass.Text == "")
            {
                txtNewPass.UseSystemPasswordChar = false;
                txtNewPass.ForeColor = Color.DarkGray;
                txtNewPass.Text = "Enter New Password";
            }
        }
        private void txtReEnterPass_Enter(object sender, EventArgs e)
        {
            if (txtReEnterPass.Text == "Re-Enter New Password")
            {
                txtReEnterPass.Text = "";
                txtReEnterPass.ForeColor = Color.Black;
                txtReEnterPass.UseSystemPasswordChar = true;
            }
        }
        private void txtReEnterPass_Leave(object sender, EventArgs e)
        {
            if (txtReEnterPass.Text == "")
            {
                txtReEnterPass.UseSystemPasswordChar = false;
                txtReEnterPass.ForeColor = Color.DarkGray;
                txtReEnterPass.Text = "Re-Enter New Password";
            }
        }
    }
}
