using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internet_Cafe_Management.DTO
{
    public class Select
    {
        //Fields
        #region Fields
        private string foodName;
        private int count;
        private float price;
        private float totalPrice;
        #endregion

        //Property
        #region Property
        public string FoodName
        {
            get { return foodName; }
            set { foodName = value; }
        }
        public int Count
        {
            get { return count; }
            set { count = value; }
        }
        public float Price
        {
            get { return price; }
            set { price = value; }
        }
        public float TotalPrice
        {
            get { return totalPrice; }
            set { totalPrice = value; }
        }
        #endregion

        //constructor
        #region constructor
        public Select(string foodName, int count, float price, float totalPrice = 0)
        {
            this.FoodName = foodName;
            this.Count = count;
            this.Price = price;
            this.TotalPrice = totalPrice;
        }
        //Đưa dữ liệu vào dữ liệu bảng datarow
        public Select(DataRow row)
        {
            this.FoodName = row["Name"].ToString();
            this.Count = (int)row["count"];
            this.Price = (float)Convert.ToDouble(row["price"].ToString());
            this.TotalPrice = (float)Convert.ToDouble(row["totalPrice"].ToString());
        }
        #endregion
    }
}
