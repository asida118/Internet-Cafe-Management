using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internet_Cafe_Management.DTO
{
    public class BillInfo
    {
        //Fields
        #region Fields
        private int iD;
        private int billID;
        private int menuID;
        private int count;
        #endregion

        //Property
        #region Property
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        public int BillID
        {
            get { return billID; }
            set { billID = value; }
        }
        public int MenuID
        {
            get { return menuID; }
            set { menuID = value; }
        }
        public int Count
        {
            get { return count; }
            set { count = value; }
        }
        #endregion

        //constructor
        #region constructor
        public BillInfo(int id, int billID, int menuID, int count)
        {
            this.ID = id;
            this.BillID = billID;
            this.MenuID = menuID;
            this.Count = count;
        }
        //Đưa dữ liệu vào dữ liệu bảng datarow
        public BillInfo(DataRow row)
        {
            this.ID = (int)row["id"];
            this.BillID = (int)row["idbill"];
            this.MenuID = (int)row["idmenu"];
            this.Count = (int)row["count"];
        }
        #endregion
    }
}
