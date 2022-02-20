using Internet_Cafe_Management.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internet_Cafe_Management.ADO
{
    public class SelectDAO
    {
        private static SelectDAO instance;

        public static SelectDAO Instance
        {
            get { if (instance == null) instance = new SelectDAO(); return SelectDAO.instance; }
            private set { SelectDAO.instance = value; }
        }
        private SelectDAO() { }
        public List<Select> GetListSelectByComputer(int id)
        {
            List<Select> listSelect = new List<Select>();
            string query = "SELECT f.name, bi.count, f.price, f.price*bi.count AS totalPrice FROM dbo.BillInfo AS bi, dbo.Bill AS b, dbo.Menu AS f " +
                "WHERE bi.idBill = b.id AND bi.idMenu = f.id AND b.status = 0 AND b.idComputer =" + id;
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Select select = new Select(item);
                listSelect.Add(select);
            }
            return listSelect;
        }

    }
}
