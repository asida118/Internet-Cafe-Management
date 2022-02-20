using Internet_Cafe_Management.ADO;
using Internet_Cafe_Management.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Internet_Cafe_Management.frmAccountProfile;

namespace Internet_Cafe_Management
{
    public partial class frmComputerManager : Form
    {
        private Account loginAccount;
        public Account LoginAccount
        {
            get { return loginAccount; }
            set { loginAccount = value; ChangeAccount(loginAccount.Type); }
        }
        public frmComputerManager(Account acc)
        {
            InitializeComponent();
            this.LoginAccount = acc;

            LoadComputer();
            LoadCategory();
            LoadComboboxComputer(cbSwitchComputer);
        }
        #region Method

        //Hiển thị danh sách Computer lên form
        void LoadComputer()
        {
            int newsize = 12;
            Image grey = Image.FromFile(@"D:\Le-Gia-Huy\Nam_3\C# - Winform\Đồ án\Internet Cafe Management\image\grey-Deskstop.png");
            Image white = Image.FromFile(@"D:\Le-Gia-Huy\Nam_3\C# - Winform\Đồ án\Internet Cafe Management\image\white-Deskstop2.png");
            flpComputer.Controls.Clear();
            List<Computer> computerList = ComputerDAO.Instance.LoadComputerList(); // lấy ds computer
            foreach (Computer item in computerList) //Vòng lặp tạo ra button
            {
                //Thiết kế Button cho từng Computer
                Button btn = new Button() { Width = ComputerDAO.ComputerWidth, Height = ComputerDAO.ComputerHeight, };
                btn.Text = item.Name + Environment.NewLine; //+item.status
                btn.ForeColor = Color.White;
                btn.Font = new Font(btn.Font.FontFamily,newsize, FontStyle.Bold);
                btn.Click += btn_Click;
                btn.Tag = item;

                switch (item.Status)
                {
                    case "Trống":
                        btn.BackgroundImage = grey;
                        btn.BackgroundImageLayout = ImageLayout.Zoom;
                        //btn.BackColor = Color.Aqua;
                        break;
                    default:
                        btn.BackgroundImage = white;
                        btn.BackgroundImageLayout = ImageLayout.Zoom;
                        //btn.BackColor = Color.LightPink;
                        break;
                }
                flpComputer.Controls.Add(btn);
            }
        }

        //Hiển thị danh sách danh mục lên form
        void LoadCategory()
        {
            List<Category> listCategory = CategoryDAO.Instance.GetListCategory();
            cbCategory.DataSource = listCategory;
            cbCategory.DisplayMember = "Name";
        }

        //Hiển thị danh sách thức ăn lên form theo category
        void LoadMenuListByCategoryID(int id)
        {
            List<Internet_Cafe_Management.DTO.Menu> listMenu = MenuDAO.Instance.GetMenuByCategoryID(id);
            cbMenu.DataSource = listMenu;
            cbMenu.DisplayMember = "Name";
        }

        //Hiển thị hóa đơn lên list view
        void ShowBill(int id)
        {
            lsvBill.Items.Clear();
            List<Select> listBillInfo = SelectDAO.Instance.GetListSelectByComputer(id);
            float totalPrice = 0;
            foreach (Select item in listBillInfo)
            {
                ListViewItem lsvItem = new ListViewItem(item.FoodName.ToString());      //bắt đầu từ cột foodname
                lsvItem.SubItems.Add(item.Count.ToString());        //thêm item vào cột count
                lsvItem.SubItems.Add(item.Price.ToString());        //thêm item vào cột Price
                lsvItem.SubItems.Add(item.TotalPrice.ToString());   //thêm item vào cột total price
                totalPrice += item.TotalPrice;      //Tính tổng tiền

                lsvBill.Items.Add(lsvItem);
            }
            CultureInfo culture = new CultureInfo("vi-VN"); //Đặt đơn vị tiền là VNĐ
            txtTotalPrice.Text = totalPrice.ToString("c", culture); //Format ra đơn vị tiền 
        }

        void ChangeAccount(int type)
        {
            adminToolStripMenuItem.Enabled = type == 1;
            thôngTinCáNhânToolStripMenuItem.Text += " (" + LoginAccount.DisplayName + ")";
        }
        
        void LoadComboboxComputer(ComboBox cb)
        {
            cb.DataSource = ComputerDAO.Instance.LoadComputerList();
            cb.DisplayMember = "Name";
        }
        #endregion
        #region Events
        void btn_Click(object sender, EventArgs e)
        {
            int computerID = ((sender as Button).Tag as Computer).ID;
            string computerName = ((sender as Button).Tag as Computer).Name;
            lsvBill.Tag = (sender as Button).Tag;
            ShowBill(computerID);   //Hiển thị hóa đơn theo ID của computer
            lblComputerID.Text = computerName;  //hiển thị tên của computer
        }
    
        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAdmin f = new frmAdmin(); //Chuyển qua form Admin
            f.loginAccount = LoginAccount;
            f.InsertMenu += f_InsertMenu;
            f.DeleteMenu += f_DeleteMenu;
            f.UpdateMenu += f_UpdateMenu;
            f.ShowDialog();
        }       
        void f_UpdateMenu(object sender, EventArgs e)
        {
            LoadMenuListByCategoryID((cbCategory.SelectedItem as Category).ID);
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Computer).ID);
        }
        void f_DeleteMenu(object sender, EventArgs e)
        {
            LoadMenuListByCategoryID((cbCategory.SelectedItem as Category).ID);
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Computer).ID);
            LoadComputer();
        }
        void f_InsertMenu(object sender, EventArgs e)
        {
            LoadMenuListByCategoryID((cbCategory.SelectedItem as Category).ID);
            if(lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Computer).ID);
        }

        //Chạy danh sách menu theo category hiển thị lên combobox
        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedItem == null)
                return;
            Category selected = cb.SelectedItem as Category;
            id = selected.ID;

            LoadMenuListByCategoryID(id);
        }

        //Thêm bớt lựa cho cho menu
        private void btxAddMenu_Click(object sender, EventArgs e)
        {
            Computer computer = lsvBill.Tag as Computer;

            if (computer == null)
            {
                MessageBox.Show("Choose the computer !!");
                return;
            }    

            int idBill = BillDAO.Instance.GetUncheckBillIDByComputerID(computer.ID);
            int menuID = (cbMenu.SelectedItem as Internet_Cafe_Management.DTO.Menu).ID;
            int count = count = (int)nmMenuCount.Value;
            if (idBill == -1)
            {
                BillDAO.Instance.InsertBill(computer.ID);
                BillInfoDAO.Instance.InsertBillInfo(BillDAO.Instance.GetMaxIDBill(), menuID, count);
            }
            else
            {
                BillInfoDAO.Instance.InsertBillInfo(idBill, menuID, count);
            }
            ShowBill(computer.ID);
            LoadComputer();
        }
        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            Computer computer = lsvBill.Tag as Computer;
            int idBill = BillDAO.Instance.GetUncheckBillIDByComputerID(computer.ID);
            int discount = (int)nmDiscount.Value;

            double totalPrice = Convert.ToDouble(txtTotalPrice.Text.Split(',')[0]);
            double finalTotalPrice = totalPrice - (totalPrice / 100) * discount;
            if (idBill != -1)
            {
                if (MessageBox.Show(string.Format("Are you sure to make payment for computer{0}\nTotal - (Total / 100) x Discount\n=> {1} - ({1} / 100) x {2} = {3}",
                    computer.Name, totalPrice*1000, discount, finalTotalPrice*1000), "Notification", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    BillDAO.Instance.CheckOut(idBill,discount,(float)finalTotalPrice);
                    ShowBill(computer.ID);
                    LoadComputer();
                }
            }
        }
        private void btnSwitchComputer_Click(object sender, EventArgs e)
        {
            int id1 = (lsvBill.Tag as Computer).ID;
            int id2 = (cbSwitchComputer.SelectedItem as Computer).ID;
            if (MessageBox.Show(string.Format("Do you really want to switch from Computer{0} to Computer {1}", 
                (lsvBill.Tag as Computer).Name, (cbSwitchComputer.SelectedItem as Computer).Name),"Notification", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                ComputerDAO.Instance.SwitchComputer(id1, id2);
                LoadComputer();
            }        
        }

        //Hiển thị loại tài khoản đang dùng
        void f_UpdateAccount(object sender, AccountEvent e)
        {
            thôngToolStripMenuItem.Text = "Account Information (" + e.Acc.DisplayName + ")";
        }

        //Chuyển qua form thông tin tài khoản
        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAccountProfile f = new frmAccountProfile(LoginAccount);
            f.UpdateAccount += f_UpdateAccount;
            f.ShowDialog();
        }

        //Đóng form khi đăng xuất
        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
