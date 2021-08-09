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

namespace DO_AN.GUI
{
    public partial class tinhLuongNV : Form
    {
        //phương pháp delegate(chuyển đổi dữ liệu từ form admin qua form này)
        public delegate void nhanMaNV(string maNV, string hoTen, string luongCB,string soGioLam);

        public nhanMaNV nhan;

        public void layDL(string maNV, string hoTen, string luongCB, string soGioLam)
        {
            txtMaNV.Text = maNV;
            txtTenNV.Text = hoTen;
            txtLuongCB.Text = luongCB;
            txtSoGioLam.Text = soGioLam;
        }
        //kết thúc phương pháp

        public tinhLuongNV()
        {
            InitializeComponent();
            nhan = new nhanMaNV(layDL);
        }

        DataProvider provider = new DataProvider();


        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();           
        }

        private void btnLichSu_Click(object sender, EventArgs e)
        {
            lichSuNhanLuong l = new lichSuNhanLuong();
            l.ShowDialog();
        }

        bool kTraTrungLSNhanLuong()
        {
            string query = "select count(*) from chamcong , nhanvien where nhanvien.MANV=chamcong.MANV and chamcong.manv='"+txtMaNV.Text+"' and HOTEN=N'"+txtTenNV.Text+"' and LUONGCANBAN='"+txtLuongCB.Text+"' and TIENTHUONG='"+txtTienThuong.Text+"' and SOGIOLAMDATINHTIEN='"+txtSoGioLam.Text+"' and TONGTIEN='"+txtTongTien.Text+"' and NGAYNHANLUONG=getdate()";
            int kq=(int)provider.layLenGTDauTien(query);
            if (kq >= 1)
                return true;
            return false;
        }

        private void btnXuatHD_Click(object sender, EventArgs e)
        {
            if (txtTongTien.Text != string.Empty)
            {
                if (!kTraTrungLSNhanLuong())
                {
                    string query = "set dateformat DMY INSERT INTO CHAMCONG VALUES ('" + txtMaNV.Text + "','" + txtTienThuong.Text + "','"+txtSoGioLam.Text+"',getdate(),'" + txtTongTien.Text + "')";
                    provider.thucHienCauTruyVan(query);
                    MessageBox.Show("Tiền lương của nhân viên " + txtMaNV.Text.Trim() + " có lương cơ bản: " + txtLuongCB.Text + ", số giờ làm: " + txtSoGioLam.Text + " là: " + txtTongTien.Text + " đã được lưu trong lịch sử nhận lương của nhân viên");
                    string query1 = "update nhanvien set sogiolamchuatinhtien='0' where manv='"+txtMaNV.Text+"'";
                    provider.thucHienCauTruyVan(query1);
                    MessageBox.Show("Đã sửa số giờ làm nhân viên bằng 0 thành công");
                    btnXuatHD.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Bạn đã xuất hóa đơn cho nhân viên "+txtTenNV.Text+" rồi");
                }
            }
            else
            {
                MessageBox.Show("Bạn chưa tính tổng tiền cho nhân viên ","Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnTongTien_Click(object sender, EventArgs e)
        {
            if (txtTienThuong.Text != string.Empty)
            {
                txtTongTien.Text = (float.Parse(txtLuongCB.Text) * float.Parse(txtSoGioLam.Text) + float.Parse(txtTienThuong.Text)).ToString();
            }
            else
            {
                MessageBox.Show("Bạn chưa nhập tiền thưởng của  nhân viên","Thông báo", MessageBoxButtons.OK,MessageBoxIcon.Error);
                txtTienThuong.Focus();
            }
        }

        private void txtTienThuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        ADMIN ad = new ADMIN();

        private void tinhLuongNV_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void tinhLuongNV_Load(object sender, EventArgs e)
        {

        }
    }
}
