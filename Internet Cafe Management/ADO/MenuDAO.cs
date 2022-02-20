using Internet_Cafe_Management.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internet_Cafe_Management.ADO
{
    public class MenuDAO        //Lớp DAO lấy dữ liệu từ csdl
    {

        //Cấu trúc theo design pattern Singleton
        #region Singleton
        private static MenuDAO instance;
        public static MenuDAO Instance
        {
            get { if (instance == null) instance = new MenuDAO(); return MenuDAO.instance; }
            private set { MenuDAO.instance = value; }
        }
        private MenuDAO() { }
        #endregion
        public void DeleteMenuByCategoryID(int id)
        {
            DataProvider.Instance.ExecuteQuery("delete dbo.Menu where idCategory = " + id);
        }

        //Lấy danh sách menu theo category
        public List<Menu> GetMenuByCategoryID(int id)
        {
            List<Menu> list = new List<Menu>();
            string query = "select * from Menu where idCategory = " + id;

            DataTable data = DataProvider.Instance.ExecuteQuery(query);     //Lấy dữ liệu từ csdl
            foreach (DataRow item in data.Rows)
            {
                Menu menu = new Menu(item);
                list.Add(menu);
            }
            return list;
        }

        public List<Menu> GetListMenu()
        {
            List<Menu> list = new List<Menu>();
            string query = "select * from dbo.Menu";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Menu menu = new Menu(item);
                list.Add(menu);
            }
            return list;
        }

        public List<Menu> SearchMenuByName(string name)
        {
            List<Menu> list = new List<Menu>();
            string query = string.Format("SELECT * FROM dbo.Menu WHERE name LIKE N'%{0}%'", name);
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Menu menu = new Menu(item);
                list.Add(menu);
            }
            return list;
        }
        #region CRUD
        public bool InsertMenu(string name, int id, float price)
        {
            string query = string.Format("INSERT dbo.Menu ( name, idCategory, price )VALUES  ( N'{0}', {1}, {2})", name, id, price);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        public bool UpdateMenu(int idMenu, string name, int id, float price)
        {
            string query = string.Format("UPDATE dbo.Menu SET name = N'{0}', idCategory = {1}, price = {2} WHERE id = {3}", name, id, price, idMenu);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        public bool DeleteMenu(int idMenu)
        {
            BillInfoDAO.Instance.DeleteBillInfoByMenuID(idMenu);

            string query = string.Format(" DELETE Menu WHERE id = {0}", idMenu);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        #endregion
    }
}
