using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internet_Cafe_Management.ADO
{
    public class DataProvider
    {
        #region design pattern Singleton
        //Dùng design pattern Singleton đảm bảo chỉ có 1 kết nối trong 1 lần thực thi truy vấn
        private static DataProvider instance; //Tạo đối tượng static kiếu DataProvider
        //Đóng gói đối tượng
        public static DataProvider Instance
        {
            get { if (instance == null) instance = new DataProvider(); return DataProvider.instance; } //Nếu = null thì tạo mới đối tượng
            private set { DataProvider.instance = value; } //Chỉ set dữ liệu nội bộ trong class DataProvider    
        }
        private DataProvider() { } //Tránh bị lấy lớp DataProvider ra ngoài
        #endregion

        //Tạo chuỗi kết nối database
        private string connectionSTR = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=QuanLyInternetCafe;Integrated Security=True";

        //Tạo truy vấn ExecuteQuery lấy bảng dữ liệu
        public DataTable ExecuteQuery(string query, object[] parameter = null) //ExcuteQuery dùng để hiển thị bảng lên gridview (tạo mảng parameter để thực hiện n parameter)
        {
            DataTable data = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionSTR)) //Tạo kết nối tới database và tự giải phóng khi ngừng kết nối
            {
                connection.Open(); // mở kết nối csdl
                SqlCommand command = new SqlCommand(query, connection); //tạo command thi hành các lệnh sql tương tác csdl
                if (parameter != null)
                {
                    string[] listPara = query.Split(' '); //tách chuỗi lấy ds parameter
                    int i = 0;
                    foreach (string item in listPara) //duyệt ds parameter
                    {
                        if (item.Contains('@')) //nếu item chứ @ thì nó là 1 parameter 
                        {
                            command.Parameters.AddWithValue(item, parameter[i]); //thêm giá trị parameter đó vào
                            i++;
                        }
                    }
                }
                SqlDataAdapter adapter = new SqlDataAdapter(command); //khởi tạo đối tượng adapter và cung cấp câu lệnh Sql
                adapter.Fill(data); //đổ dữ liệu từ datasource tới datatable
                connection.Close(); //đóng kết nối khi thực hiện xong câu lệnh
            }
            return data;
        }

        //Tạo truy vấn ExecuteNonQuery để chỉnh sửa dữ liệu
        public int ExecuteNonQuery(string query, object[] parameter = null) //ExecuteNonQuery dùng thi hành truy vấn update, insert, delete trả về số dòng bị ảnh hường
        {
            int data = 0;
            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }
                data = command.ExecuteNonQuery(); //trả về số dòng thực thi thành công
                connection.Close();
            }
            return data;
        }

        //Tạo truy vấn ExecuteScalar lấy giá trị
        public object ExecuteScalar(string query, object[] parameter = null) //ExecuteScalar trả về 1 giá trị của dòng đầu tiên, cột đầu tiên thường dùng để lấy 1 kết quả của count
        {
            object data = 0;
            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }
                data = command.ExecuteScalar();
                connection.Close();
            }
            return data;
        }
    }
}
