using Internet_Cafe_Management.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internet_Cafe_Management.ADO
{
    public class BillInfoDAO    //Lớp DAO lấy dữ liệu từ csdl
    {
        //Cấu trúc theo design pattern Singleton
        #region Singleton
        private static BillInfoDAO instance;

        public static BillInfoDAO Instance
        {
            get { if (instance == null) instance = new BillInfoDAO(); return BillInfoDAO.instance; }
            private set { BillInfoDAO.instance = value; }
        }
        private BillInfoDAO() { }
        #endregion

        public void DeleteBillInfoByMenuID(int id)
        {
            DataProvider.Instance.ExecuteQuery("DELETE dbo.BillInfo WHERE idMenu= " + id);
        }

        //Chèn bill info
        public void InsertBillInfo(int idBill, int idMenu, int count)
        {
            DataProvider.Instance.ExecuteNonQuery("USP_InsertBillInfo @idBill , @idMenu , @count", new object[] { idBill, idMenu, count });
        }
    }
}
