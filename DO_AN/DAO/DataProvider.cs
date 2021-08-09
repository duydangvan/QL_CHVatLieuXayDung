using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DO_AN.DAO
{

    public class DataProvider
    {

        //private static DataProvider instance;

        //public static DataProvider Instance
        //{
        //    get { if (instance == null) instance = new DataProvider(); return DataProvider.instance; }
        //    private set { DataProvider.instance = value; }
        //}

        //private DataProvider();

        public string connectionSTR = "Data Source=DESKTOP-4DFMQB4\\SQLEXPRESS;Initial Catalog=QL_VLXD;User ID=sa; password=sa2012";

        public int moKetNoi(SqlConnection con)
        {

            if (con.State.ToString() == "Closed")
            {
                con.Open();
                return 1;
            }
            return 0;
        }

        public void dongKetNoi(SqlConnection con)
        {
            con.Close();
        }
        public DataTable layDuLieuDangBang(string query)
        {
            using (SqlConnection con = new SqlConnection(connectionSTR))
            {
                DataTable data = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                adapter.Fill(data);
                return data;
            }
        }

        public void thucHienCauTruyVan(string query)
        {
            using (SqlConnection con = new SqlConnection(connectionSTR))
            {
                moKetNoi(con);
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                dongKetNoi(con);
            }
        }
        public int layLenGTDauTien(string query)// trả về con số đầu tiên khi thực hiện thành công
        {
            int data = 0;
            using (SqlConnection con = new SqlConnection(connectionSTR))
            {
                moKetNoi(con);
                SqlCommand cmd = new SqlCommand(query, con);
                data = (int)cmd.ExecuteScalar();
                dongKetNoi(con);
                return data;
            }
        }

        public string layLenGTDauTienDangChuoi(string query)// trả về con số đầu tiên khi thực hiện thành công
        {
            string data = string.Empty;
            using (SqlConnection con = new SqlConnection(connectionSTR))
            {
                moKetNoi(con);
                SqlCommand cmd = new SqlCommand(query, con);
                data = (string)cmd.ExecuteScalar();
                dongKetNoi(con);
                return data;
            }
        }

        public bool kTTrungMaNV(string maNV)
        {
            string query = "select Count(*) from NhanVien where manv='NV" + maNV + "'";
            int kq = (int)layLenGTDauTien(query);
            if (kq >= 1)
                return true;
            else
                return false;
        }


        public bool kTTrungMaSP(string maSP)
        {
            string query = "select Count(*) from VATLIEU where maVL='" + maSP + "'";
            int kq = (int)layLenGTDauTien(query);
            if (kq >= 1)
                return true;
            else
                return false;
        }

    }
}
