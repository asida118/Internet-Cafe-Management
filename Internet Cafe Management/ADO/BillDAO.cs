using Internet_Cafe_Management.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internet_Cafe_Management.ADO
{
    public class BillDAO    //Lớp DAO lấy dữ liệu từ csdl
    {
        //Cấu trúc theo design pattern Singleton
        #region Singleton
        private static BillDAO instance;
        public static BillDAO Instance
        {
            get { if (instance == null) instance = new BillDAO(); return instance; }
            private set { instance = value; }
        }
        private BillDAO() { }
        #endregion

        public int GetUncheckBillIDByComputerID(int id)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM dbo.Bill WHERE idComputer = " + id + " AND status = 0"); //Lấy bảng dữ liệu trong csdl
            if (data.Rows.Count > 0)        //nếu có dữ liệu
            {
                Bill bill = new Bill(data.Rows[0]); // Lấy trường ID của bảng Bill
                return bill.ID; // thành công trả về: bill ID
            }
            return -1; // thất bại
        }

        //Lưu ngày checkout
        public void CheckOut(int id, int discount, float totalPrice)
        {
            string query = "UPDATE dbo.Bill SET dateCheckOut = GETDATE(), status = 1, " + "discount = " + discount + ", totalPrice = " + totalPrice + " WHERE id = " + id;
            DataProvider.Instance.ExecuteNonQuery(query);
        }

        //Thêm hóa đơn
        public void InsertBill(int id)
        {
            DataProvider.Instance.ExecuteNonQuery("EXEC USP_InsertBill @idComputer", new object[] { id });
        }

        //Lấy hóa đơn theo ngày
        public DataTable GetBillListByDate(DateTime checkIn, DateTime checkOut)
        {
            return DataProvider.Instance.ExecuteQuery("exec USP_GetListBillByDate @checkIn , @checkOut", new object[] { checkIn, checkOut });
        }

        public int GetMaxIDBill()
        {
            try
            {
                return (int)DataProvider.Instance.ExecuteScalar("SELECT MAX(id) FROM dbo.Bill");
            }
            catch
            {
                return 1;
            }
        }
    }
}
    