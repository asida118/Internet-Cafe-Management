using Internet_Cafe_Management.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internet_Cafe_Management.ADO
{
    public class ComputerDAO //Lớp DAO lấy dữ liệu từ csdl
    {
        public static int ComputerWidth = 110;
        public static int ComputerHeight = 110;
        //Cấu trúc theo design pattern Singleton
        #region Singleton
        private static ComputerDAO instance;
        public static ComputerDAO Instance
        {
            get { if (instance == null) instance = new ComputerDAO(); return ComputerDAO.instance; }
            private set { ComputerDAO.instance = value; }
        }
        private ComputerDAO() { }
        #endregion
        
        //Lấy danh sách máy tính
        public List<Computer> LoadComputerList()
        {
            List<Computer> computerList = new List<Computer>();     //Tạo danh sách computer
            DataTable data = DataProvider.Instance.ExecuteQuery("USP_GetComputerList");     //Lấy dữ liệu danh sách computer từ csdl
            foreach (DataRow item in data.Rows) //duyệt item trong bảng dữ liệu lấy từ csdl
            {
                Computer computer = new Computer(item);
                computerList.Add(computer);     //thêm từng computer vào ds
            }
            return computerList;
        }
        public List<Computer> GetListComputer()
        {
            List<Computer> list = new List<Computer>();
            string query = "select * from dbo.ComputerFood";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach(DataRow item in data.Rows)
            {
                Computer computer = new Computer(item);
                list.Add(computer);
            }

            return list;
        }
        public void SwitchComputer(int id1, int id2)
        {
            DataProvider.Instance.ExecuteQuery("USP_SwitchComputer @idComputer1 , @idComputer2", new object[] { id1, id2 });
        }
        #region CRUD
        public bool InsertComputer(string name)
        {
            string query = string.Format("INSERT dbo.ComputerFood(name) VALUES  ( N'{0}')", name);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
        public bool UpdateComputer(int idComputer, string name)
        {
            string query = string.Format("UPDATE dbo.ComputerFood SET name = N'{0}' WHERE id = {1}", name, idComputer);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
        public bool DeleteComputer(int idComputer)
        {
            string query = string.Format("DELETE dbo.ComputerFood WHERE id = {0}",idComputer);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
        #endregion
    }
}