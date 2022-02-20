using Internet_Cafe_Management.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internet_Cafe_Management.ADO
{
    public class CategoryDAO        //Lớp DAO lấy dữ liệu từ csdl
    {
        //Cấu trúc theo design pattern Singleton
        #region Singleton
        private static CategoryDAO instance;
        public static CategoryDAO Instance
        {
            get { if (instance == null) instance = new CategoryDAO(); return CategoryDAO.instance; }
            private set { CategoryDAO.instance = value; }
        }
        private CategoryDAO() { }
        #endregion

        //Lấy dữ liệu danh sách category
        public List<Category> GetListCategory()
        {
            List<Category> list = new List<Category>();
            string query = "select * from MenuCategory";
            DataTable data = DataProvider.Instance.ExecuteQuery(query); //Lấy dữ liệu danh sách category từ csdl

            //Thêm category vào trong list đã tạo
            foreach (DataRow item in data.Rows)
            {
                Category category = new Category(item);
                list.Add(category);
            }
            return list;
        }
        public Category GetCategoryByID(int id)
        {
            Category category = null;
            string query = "select * from MenuCategory where id = " + id;
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                category = new Category(item);
                return category;
            }
            return category;
        }

        #region CRUD
        public bool InsertCategory(string name)
        {
            string query = string.Format("INSERT dbo.MenuCategory ( name)VALUES  ( N'{0}')", name);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        public bool UpdateCategory(int idCategory, string name)
        {
            string query = string.Format("UPDATE dbo.MenuCategory SET name = N'{0}' WHERE id = {1}", name, idCategory);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        public bool DeleteCategory(int idCategory)
        {
            MenuDAO.Instance.DeleteMenuByCategoryID(idCategory);
            string query = string.Format("delete dbo.MenuCategory where id ={0}",idCategory);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        #endregion
    }
}
