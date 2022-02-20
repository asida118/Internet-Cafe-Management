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
    public partial class frmAdmin : Form
    {
        BindingSource menuList = new BindingSource();
        BindingSource accountList = new BindingSource();

        public Account loginAccount;
        public frmAdmin()
        {
            InitializeComponent();
            Load();
        }

    //method
        #region methods
        List<Internet_Cafe_Management.DTO.Menu> SearchMenuByName(string name)
        {
            List<Internet_Cafe_Management.DTO.Menu> listMenu =  MenuDAO.Instance.SearchMenuByName(name);
            return listMenu;
        }
        void Load()
        {
            dtgvMenu.DataSource = menuList;
            dtgvAccount.DataSource = accountList;

            LoadDateTimePickerBill();
            //LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);
            LoadListMenu();
            LoadListCategory();
            LoadListComputer();
            LoadAccount();

            AddFoodBinding();
            AddCategoryBinding();
            AddComputerBinding();
            LoadCategoryIntoCombobox(cbCategoryMenu);
            AddAccountBinding();
        }
        void LoadDateTimePickerBill()
        {
            DateTime today = DateTime.Now;
            dtpkFromDate.Value = new DateTime(today.Year, today.Month, 1);
            dtpkToDate.Value = dtpkFromDate.Value.AddMonths(1).AddDays(-1);
        }
        void LoadListBillByDate(DateTime checkIn, DateTime checkOut)
        {
            dtgvBill.DataSource = BillDAO.Instance.GetBillListByDate(checkIn, checkOut);
        }
        void LoadListMenu()
        {
            menuList.DataSource = MenuDAO.Instance.GetListMenu();
        }
        void LoadListComputer()
        {
            dtgvComputer.DataSource = ComputerDAO.Instance.GetListComputer();
        }
        void LoadListCategory()
        {
            dtgvCategory.DataSource = CategoryDAO.Instance.GetListCategory();
        }
        void AddFoodBinding()
        {
            txtMenuName.DataBindings.Add(new Binding("Text", dtgvMenu.DataSource, "Name",true,DataSourceUpdateMode.Never));
            txtMenuID.DataBindings.Add(new Binding("Text", dtgvMenu.DataSource, "ID", true, DataSourceUpdateMode.Never));
            nmMenuPrice.DataBindings.Add(new Binding("Value", dtgvMenu.DataSource, "Price", true, DataSourceUpdateMode.Never));
        }
        void AddCategoryBinding()
        {
            txtCategoryName.DataBindings.Add(new Binding("Text", dtgvCategory.DataSource, "Name", true, DataSourceUpdateMode.Never));
            txtCategoryID.DataBindings.Add(new Binding("Text", dtgvCategory.DataSource, "ID", true, DataSourceUpdateMode.Never));
        }
        void AddComputerBinding()
        {
            txtComputerID.DataBindings.Add(new Binding("Text", dtgvComputer.DataSource, "ID", true, DataSourceUpdateMode.Never));
            txtComputerName.DataBindings.Add(new Binding("Text", dtgvComputer.DataSource, "Name", true, DataSourceUpdateMode.Never));
            txtComputerStatus.DataBindings.Add(new Binding("Text", dtgvComputer.DataSource, "Status", true, DataSourceUpdateMode.Never));
        }
        //ACCOUNT
        void AddAccountBinding()
        {
            txtUserName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "UserName", true, DataSourceUpdateMode.Never));
            txtDisplayName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "DisplayName", true, DataSourceUpdateMode.Never));
            numericUpDown1.DataBindings.Add(new Binding("Value", dtgvAccount.DataSource, "Type", true, DataSourceUpdateMode.Never));
        }
        void LoadAccount()
        {
            accountList.DataSource = AccountDAO.Instance.GetListAccount(); 
        }
        void AddAccount(string userName, string displayName, int type)
        {
            if(AccountDAO.Instance.InsertAccount(userName, displayName, type))
                MessageBox.Show("Add Account sucessful!");
            else
                MessageBox.Show("Add Account failed!");
            LoadAccount();
        }
        void EditAccount(string userName, string displayName, int type)
        {
            if (AccountDAO.Instance.UpdateAccount(userName, displayName, type))
                MessageBox.Show("Update Account sucessful!");
            else
                MessageBox.Show("Update Account failed!");
            LoadAccount();
        }

        void DeleteAccount(string userName)
        {
            if (loginAccount.UserName.Equals(userName))
            {
                MessageBox.Show("Please Don't delete current account!");
                return;
            }
            if (AccountDAO.Instance.DeleteAccount(userName))
                MessageBox.Show("Delete Account sucessful!");
            else
                MessageBox.Show("Delete Account failed!");
            LoadAccount();
        }
        void ResetPass(string userName)
        {
            if (AccountDAO.Instance.ResetPassword(userName))
                MessageBox.Show("Reset Account sucessful!");
            else
                MessageBox.Show("Reset Account failed!");
        }
        void LoadCategoryIntoCombobox(ComboBox cb)
        {
            cb.DataSource = CategoryDAO.Instance.GetListCategory();
            cb.DisplayMember = "Name";
        }
        #endregion

        //events
        #region events
        private void btnViewBill_Click_1(object sender, EventArgs e)
        {
            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);
        }
        //Category events
        private void btnShowCategory_Click(object sender, EventArgs e)
        {
            LoadListCategory();
        }
        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            string name = txtCategoryName.Text;
            if(CategoryDAO.Instance.InsertCategory(name))
            {
                MessageBox.Show("Add Category sucessful!");
                LoadListCategory();
            }
            else MessageBox.Show("Add Category failed!");
        }
        private void btnUpdateCategory_Click(object sender, EventArgs e)
        {
            string name = txtCategoryName.Text;
            int id = Convert.ToInt32(txtCategoryID.Text);
            if(CategoryDAO.Instance.UpdateCategory(id,name))
            {
                MessageBox.Show("Edit Category sucessful!");
                LoadListCategory();
            }
            else MessageBox.Show("Edit Category failed!");
        }
        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtCategoryID.Text);
            if (CategoryDAO.Instance.DeleteCategory(id))
            {
                MessageBox.Show("Delete Category sucessful!");
                LoadListCategory();
            }
            else MessageBox.Show("Delete Category failed!");
        }
        //Computer events
        private void btnShowComputer_Click(object sender, EventArgs e)
        {
            LoadListComputer();
        }
        private void btnAddComputer_Click(object sender, EventArgs e)
        {
            string name = txtComputerName.Text;
            if (ComputerDAO.Instance.InsertComputer(name))
            {
                MessageBox.Show("Add Computer sucessful!");
                LoadListComputer();
            }
            else MessageBox.Show("Add Computer failed!");
        }
        private void btnEditComputer_Click(object sender, EventArgs e)
        {
            string name = txtComputerName.Text;
            int id = Convert.ToInt32(txtComputerID.Text);
            if (ComputerDAO.Instance.UpdateComputer(id, name))
            {
                MessageBox.Show("Edit Computer sucessful!");
                LoadListComputer();
            }
            else MessageBox.Show("Edit Computer failed!");
        }
        private void btnDeleteComputer_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtComputerID.Text);
            if (ComputerDAO.Instance.DeleteComputer(id))
            {
                MessageBox.Show("Delete Computer sucessful!");
                LoadListComputer();
            }
            else MessageBox.Show("Delete Computer failed!");
        }
        private void btnShowMenu_Click(object sender, EventArgs e)
        {
            LoadListMenu();
        }
        private void txtMenuID_TextChanged(object sender, EventArgs e)
        {
            try { 
                if (dtgvMenu.SelectedCells.Count > 0)
                {
                    int id = (int)dtgvMenu.SelectedCells[0].OwningRow.Cells["CategoryID"].Value;
                    Category cateogory = CategoryDAO.Instance.GetCategoryByID(id);
                    cbCategoryMenu.SelectedItem = cateogory;

                    int index = -1;
                    int i = 0;
                    foreach (Category item in cbCategoryMenu.Items)
                    {
                        if (item.ID == cateogory.ID)
                        {
                            index = i;
                            break;
                        }
                        i++;
                    }
                    cbCategoryMenu.SelectedIndex = index;
                }
            }
            catch { }
        }
        private void btnAddMenu_Click(object sender, EventArgs e)
        {
            string name = txtMenuName.Text;
            int categoryID = (cbCategoryMenu.SelectedItem as Category).ID;
            float price = (float)nmMenuPrice.Value;
            if (MenuDAO.Instance.InsertMenu(name, categoryID, price))
            {
                MessageBox.Show("Add menu sucessful!");
                LoadListMenu();
                if (insertMenu != null)
                    insertMenu(this, new EventArgs());

            }
            else MessageBox.Show("Add menu failed!");
        }

        private void btnEditMenu_Click(object sender, EventArgs e)
        {
            string name = txtMenuName.Text;
            int categoryID = (cbCategoryMenu.SelectedItem as Category).ID;
            float price = (float)nmMenuPrice.Value;
            int id = Convert.ToInt32(txtMenuID.Text);

            if (MenuDAO.Instance.UpdateMenu(id, name, categoryID, price))
            {
                MessageBox.Show("Edit menu sucessful!");
                LoadListMenu();
                if (updateMenu != null)
                    updateMenu(this, new EventArgs());
            }
            else MessageBox.Show("Edit menu failed!");

        }

        private void btnDeleteMenu_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtMenuID.Text);

            if (MenuDAO.Instance.DeleteMenu(id))
            {
                MessageBox.Show("Delete menu sucessful!");
                LoadListMenu();
                if (deleteMenu != null)
                    deleteMenu(this, new EventArgs());
            }
            else MessageBox.Show("Delete menu failed!");
        }
        private void btnSearchMenu_Click(object sender, EventArgs e)
        {
            menuList.DataSource = SearchMenuByName(txtSearchMenu.Text);
        }

        private void btnShowAccount_Click(object sender, EventArgs e)
        {
            LoadAccount();
        }

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text;
            string displayName = txtDisplayName.Text;
            int type = (int)numericUpDown1.Value;

            AddAccount(userName, displayName, type);
        }

        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text;

            DeleteAccount(userName);
        }

        private void btnEditAccount_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text;
            string displayName = txtDisplayName.Text;
            int type = (int)numericUpDown1.Value;

            EditAccount(userName, displayName, type);
        }
        private void btnResetPass_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text;

            ResetPass(userName);
        }
        private event EventHandler insertMenu;
        public event EventHandler InsertMenu
        {
            add { insertMenu += value; }
            remove { insertMenu -= value; }
        }

        private event EventHandler deleteMenu;
        public event EventHandler DeleteMenu
        {
            add { deleteMenu += value; }
            remove { deleteMenu -= value; }
        }

        private event EventHandler updateMenu;
        public event EventHandler UpdateMenu
        {
            add { updateMenu += value; }
            remove { updateMenu -= value; }
        }
        #endregion
    }
}
