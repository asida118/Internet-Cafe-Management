using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internet_Cafe_Management.DTO
{
    public class Bill
    {
        //Fields
        #region Fields
        private DateTime? dateCheckIn;
        private DateTime? dateCheckOut;
        private int iD;
        private int status;
        private int discount;
        #endregion
        //Property
        #region Property
        public DateTime? DateCheckIn
        {
            get { return dateCheckIn; }
            set { dateCheckIn = value; }
        }
        public DateTime? DateCheckOut
        {
            get { return DateCheckOut; }
            set { DateCheckOut = value; }
        }
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        public int Status
        {
            get { return status; }
            set { status = value; }
        }
        public int Discount
        {
            get { return discount; }
            set { discount = value; }
        }
        #endregion

        //constructor
        #region constructor
        public Bill(int id, DateTime? dataCheckin, DateTime? dataCheckout, int status,int discount)
        {
            this.ID = id;
            this.dateCheckIn = dataCheckin;
            this.dateCheckOut = dataCheckout;
            this.Status = status;
            this.Discount = discount;
        }
        //Đưa dữ liệu vào dữ liệu bảng datarow
        public Bill(DataRow row)
        {
            this.ID = (int)row["id"];
          
            var dataCheckOutTemp = row["dateCheckOut"];
            if (dataCheckOutTemp.ToString() != "")
                this.DateCheckOut = (DateTime?)dataCheckOutTemp;

            this.Status = (int)row["Status"];

            if (row["discount"].ToString() != "")
                this.Discount = (int)row["discount"];
        }
        #endregion
    }
}
