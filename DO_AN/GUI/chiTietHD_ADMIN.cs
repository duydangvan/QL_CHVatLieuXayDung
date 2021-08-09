using DO_AN.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DO_AN.GUI
{
    public partial class chiTietHD_ADMIN : Form
    { 
        public delegate void nhanMaPhieu(string maPhieu);

        public nhanMaPhieu nhan;


        public chiTietHD_ADMIN()
        {
            InitializeComponent();
            nhan = new nhanMaPhieu(layDL);
        }

        DataProvider provider = new DataProvider();

        void loadChiTietPhieuHang()
        {
            string query1 = "select PHIEUHANG.MAPHIEU, TENVL, ct_Phieuhang.SOLUONG,ct_Phieuhang.DONGIA,hoten_KH, CT_PHIEUHANG.sdt,HOTEN,NGAYTAO, THANHTIEN from CT_PHIEUHANG, PHIEUHANG, VATLIEU,nhanvien where CT_PHIEUHANG.maphieu=PHIEUHANG.MAPHIEU and PHIEUHANG.MAVL=VATLIEU.MAVL and PHIEUHANG.MANV=NHANVIEN.MANV and PHIEUHANG.MAPHIEU='"+txtMaPhieu.Text+"'";
            SqlConnection con = new SqlConnection(provider.connectionSTR);
            con.Open();
            SqlCommand cmd = new SqlCommand(query1, con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                txtTenVL.Text = reader[1].ToString();
                txtSL.Text = reader[2].ToString();
                txtDonGia.Text = reader[3].ToString();
                txtHoTenKhachHang.Text = reader[4].ToString();
                txtSDT.Text = reader[5].ToString();
                txtTenNV.Text = reader[6].ToString();
                txtNgayTao.Text = DateTime.Parse(reader[7].ToString()).ToString("dd/MM/yyyy");
                txtTongTien.Text = reader[8].ToString();
            }
            reader.Close();
            con.Close();
        }

        void layDL(string maPhieu)
        {
            txtMaPhieu.Text = maPhieu;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chiTietHD_ADMIN_Load(object sender, EventArgs e)
        {
            loadChiTietPhieuHang();
        }
    }
}
