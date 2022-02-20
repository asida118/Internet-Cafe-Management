﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internet_Cafe_Management.DTO
{
    public class Menu
    {
        //Fields
        #region Fields
        private int iD;
        private string name;
        private int categoryID;
        private float price;
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
        public int CategoryID
        {
            get { return categoryID; }
            set { categoryID = value; }
        }
        public float Price
        {
            get { return price; }
            set { price = value; }
        }
        #endregion

        //constructor
        #region constructor
        public Menu(int id, string name, int categoryID, float price)
        {
            this.ID = id;
            this.Name = name;
            this.CategoryID = categoryID;
            this.Price = price;
        }
        //Đưa dữ liệu vào dữ liệu bảng datarow
        public Menu(DataRow row)
        {
            this.ID = (int)row["id"];
            this.Name = row["name"].ToString();
            this.CategoryID = (int)row["idcategory"];
            this.Price = (float)Convert.ToDouble(row["price"].ToString());
        }
        #endregion
    }
}
