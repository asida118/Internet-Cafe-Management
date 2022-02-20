using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internet_Cafe_Management.DTO
{
    public class Category
    {
        //Fields
        #region Fields
        private string name;
        private int iD;
        #endregion

        //Property
        #region Property
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        #endregion

        //constructor
        #region constructor
        public Category(int id, string name)
        {
            this.ID = id;
            this.Name = name;
        }
        //Đưa dữ liệu vào dữ liệu bảng datarow
        public Category(DataRow row)
        {
            this.ID = (int)row["id"];
            this.Name = row["name"].ToString();
        }
        #endregion
    }
}
