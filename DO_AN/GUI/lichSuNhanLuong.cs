using DO_AN.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DO_AN
{
    public partial class lichSuNhanLuong : Form
    {


        public lichSuNhanLuong()
        {
            InitializeComponent();
            loadLichSuTL();
        }

        DataProvider provider = new DataProvider();

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void loadLichSuTL()
        {
                string query = "select nhanvien.MANV AS N'Mã Nhân Viên', HOTEN as N'Họ tên',LUONGCANBAN as N'Lương căn bản', SOGIOLAMDATINHTIEN as N'Số giờ làm', TIENTHUONG as N'Tiền Thưởng',tongtien as N'Tổng tiền', NGAYNHANLUONG as N'Ngày nhận lương' from chamcong, nhanvien where chamcong.MANV=NHANVIEN.MANV and sogiolamDATINHTIEN>0";
                dtgvLichSu.DataSource = provider.layDuLieuDangBang(query);
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string query = "EXEC LICHSUNHANLUONG @tenNV=N'"+txtTenNV.Text+"'";
            DataTable table =provider.layDuLieuDangBang(query);
            dtgvLichSu.DataSource = table;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            loadLichSuTL();
            txtTenNV.Text = string.Empty;
        }
    }
}
