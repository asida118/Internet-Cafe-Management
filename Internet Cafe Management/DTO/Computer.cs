using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internet_Cafe_Management.DTO
{
    //Class lấy ra từ csdl
    public class Computer
    {
        //Fields
        #region Fields
        private int iD;
        private string name;
        private string status;
        #endregion

        //Property
        #region Property
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        #endregion

        //constructor
        #region constructor
        public Computer(int id, string name, string status)
        {
            this.ID = id;
            this.Name = name;
            this.Status = status;
        }
        //Đưa dữ liệu vào dữ liệu bảng datarow
        public Computer(DataRow row) //chuyển thành tường trường dữ liệu
        {
            this.ID = (int)row["id"];   //Lấy ra trường id 
            this.Name = row["name"].ToString();     //Lấy ra trường name 
            this.Status = row["status"].ToString();     //Lấy ra trường status 
        }
        #endregion
    }
}
