using DO_AN.DAO;
using DO_AN.GUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DO_AN
{
    public partial class ADMIN : Form
    {
        DataProvider provider = new DataProvider();


        public ADMIN()
        {
            InitializeComponent();
            LoadDSNV();
            cbbGioiTinh.DataSource = dsGioiTinh();
            loadDoanhThu();
            tongTien();
            loadHangTrongKho();
            //hienThiTK();
            loadChiTietTK();
            loadHangTrongKho();
        }

        public void LoadDSNV()
        {
            string query = "EXEC HIENTHINV @CHUCVU= N'nhân viên'";

            dtgvNhanVien.DataSource = provider.layDuLieuDangBang(query);
                
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
                if (txtTimKiem.Text != string.Empty)
                {
                    string timNV = "EXEC TIMNV @TENNV=N'" + txtTimKiem.Text + "'";
                    dtgvNhanVien.DataSource = provider.layDuLieuDangBang(timNV);
                    dtgvNhanVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                else
                {
                    MessageBox.Show("Bạn chưa nhập họ và tên mà bạn muốn tìm vào ô kia kìa");
                    txtTimKiem.Focus();
                }
            }

        void hienthiTTNhanVien()
        {
            txtMaNV.Text = dtgvNhanVien.CurrentRow.Cells[0].Value.ToString();
            txtTenNV.Text = dtgvNhanVien.CurrentRow.Cells[1].Value.ToString();
            txtDiaChi.Text = dtgvNhanVien.CurrentRow.Cells[3].Value.ToString();
            txtSDT.Text = dtgvNhanVien.CurrentRow.Cells[4].Value.ToString();
            txtLuongcanBan.Text = dtgvNhanVien.CurrentRow.Cells[7].Value.ToString();
            txtSosGioLam.Text = dtgvNhanVien.CurrentRow.Cells[8].Value.ToString();
        }

        private void txtTimKiem_Click(object sender, EventArgs e)
        {
            txtTimKiem.Text = string.Empty;
            txtTimKiem.Focus();
            txtTimKiem.ForeColor = Color.Black;
        }

        private void load_All_Click(object sender, EventArgs e)
        {
            LoadDSNV();
            dtgvNhanVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            txtMaNV.Enabled = true;
            btnLuu.Enabled = false;
            txtTimKiem.Text = string.Empty;
            reset();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            ThemNV them = new ThemNV();
            them.ShowDialog();
            LoadDSNV();
        }


        void reset()
        {
            txtMaNV.Text = string.Empty;
            txtDiaChi.Text = string.Empty;
            txtLuongcanBan.Text = string.Empty;
            txtSDT.Text = string.Empty;
            txtSosGioLam.Text = string.Empty;
            txtTenNV.Text = string.Empty;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        List<string> GioiTinh = new List<string>();
        public List<string> dsGioiTinh()
        {
            GioiTinh.Add("Hiển thị giới tính");
            GioiTinh.Add("Nam");
            GioiTinh.Add("Nữ");
            return GioiTinh;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMaNV.Text != string.Empty)
            {
                txtMaNV.Enabled = false;
                 btnLuu.Enabled = true;
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn nhân viên cần sửa đó nha");
            }
            
        }

        private void dtgvNhanVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            hienthiTTNhanVien();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtSDT.Text.Trim().Length < 12)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn lưu thông tin nhân viên " + txtTenNV.Text.Trim() + " chưa? ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string query2 = "update nhanvien  set sogiolamchuatinhtien='" + txtSosGioLam.Text + "',HOTEN=N'" + txtTenNV.Text.Trim() + "',DIACHI=N'" + txtDiaChi.Text.Trim() + "',SDT='" + txtSDT.Text.Trim() + "',LUONGCANBAN='" + txtLuongcanBan.Text.Trim() + "' where manv=N'" + txtMaNV.Text.Trim() + "'";
                    provider.thucHienCauTruyVan(query2);
                    LoadDSNV();
                    MessageBox.Show("Đã sửa thông tin nhân viên " + txtTenNV.Text.Trim() + " thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    reset();
                    txtMaNV.Enabled = true;
                    btnLuu.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show("Số điện thoại không vượt quá 12 số bạn nhé","Thông báo",MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnTinhLuong_Click(object sender, EventArgs e)
        {
            if (txtMaNV.Text.Trim() != string.Empty)
            {
                if (int.Parse(txtSosGioLam.Text) != 0)
                {
                    tinhLuongNV t = new tinhLuongNV();
                    t.nhan(dtgvNhanVien.CurrentRow.Cells[0].Value.ToString(), dtgvNhanVien.CurrentRow.Cells[1].Value.ToString(), dtgvNhanVien.CurrentRow.Cells[7].Value.ToString(), dtgvNhanVien.CurrentRow.Cells[8].Value.ToString());
                    t.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Nhân viên "+txtTenNV.Text+" có số giờ làm là 0 nên chưa thể tính lương","Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn nhân viên cần tính lương","Thông báo");
            }
            LoadDSNV();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            string query1 = "EXEC HIENTHIDOANHTHU @NGAYBATDAU='"+thoiGianBatDau.Value+"',@NGAYKETTHUC='"+thoiGianKetThuc.Value+"'";
            dtgvDoanhThu.DataSource = provider.layDuLieuDangBang(query1);
            tongTien();
        }

        void loadDoanhThu()
        {
            string query = "select HOTEN AS N'Họ tên nhân viên',MAPHIEU as 'Mã Phiếu',TENVL as N'Tên vật liệu', NGAYTAO as N'Ngày tạo', THANHTIEN as 'Tiền'  from NHANVIEN,PHIEUHANG,VATLIEU where NHANVIEN.MANV=PHIEUHANG.MANV and PHIEUHANG.MAVL=VATLIEU.MAVL ";
            dtgvDoanhThu.DataSource = provider.layDuLieuDangBang(query);
        }

        private void btnThoat1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void btnHienThiTatCaHD_Click(object sender, EventArgs e)
        {
            loadDoanhThu();
            tongTien();
        }

        void tongTien()
        {
            float tt = 0;
            for (int i = 0; i < dtgvDoanhThu.RowCount; i++)
            {
                tt += float.Parse(dtgvDoanhThu.Rows[i].Cells["Tiền"].Value.ToString());
            }
            txtTongTien.Text = tt.ToString();
        }


        public void loadHangTrongKho()
        {
            string query = "select v.MAVL as N'Mã vật liệu', TENVL as N'Tên vật liệu', SOLUONG as N'Số lượng', DONVITINH as N'Đơn vị tính', DONGIA as N'Đơn giá', TENKHO as N'Tên kho',DIACHI as N'Địa chỉ',NGAYNHAPHANG as N'Ngày nhập hàng', k.MAKHO as N'Mã kho'  from vatlieu v, kho k where v.MAKHO=k.MAKHO and SOLUONG>0";
           dtgvHang.DataSource= provider.layDuLieuDangBang(query);
        }


        private void dtgvHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaKho.Text = dtgvHang.CurrentRow.Cells[8].Value.ToString();
            txtTenVL.Text = dtgvHang.CurrentRow.Cells[1].Value.ToString();
            txtDonGia.Text = dtgvHang.CurrentRow.Cells[4].Value.ToString();
            txtSL.Text = dtgvHang.CurrentRow.Cells[2].Value.ToString();
            txtTenKho.Text = dtgvHang.CurrentRow.Cells[5].Value.ToString();
            txtDiaChi3.Text = dtgvHang.CurrentRow.Cells[6].Value.ToString();
            txtNgayNhapHang.Text = DateTime.Parse(dtgvHang.CurrentRow.Cells[7].Value.ToString()).ToString("dd/MM/yyyy");
        }

        private void btnHienThiTatCa_Click(object sender, EventArgs e)
        {
            loadHangTrongKho();
        }

        private void btnTimKiem3_Click(object sender, EventArgs e)
        {
            string query = "exec TIMVATLIEU @NGAYNHAPBATDAU ='"+TGNHBatDau.Value+"',@NGAYNHAPKETTHUC='"+TGNHKetThuc.Value+"'";
            dtgvHang.DataSource = provider.layDuLieuDangBang(query);
        }

        void hangReset()
        {
            txtTenVL.Text = string.Empty;
            txtSL.Text = string.Empty;
            txtDonGia.Text = string.Empty;
            txtNgayNhapHang.Text = string.Empty;
            txtDiaChi3.Text = string.Empty;
            txtMaKho.Text = string.Empty;
            txtTenKho.Text = string.Empty;
        }

        private void btnSua3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn lưu chứ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string query1 = "update kho set TENKHO=N'" + txtTenKho.Text + "', DIACHI=N'" + txtDiaChi3.Text + "' where makho='" + txtMaKho.Text.Trim() + "'";
                string query2 = "SET DATEFORMAT DMY update VATLIEU set  DONGIA='" + txtDonGia.Text + "', SOLUONG='" + txtSL.Text + "' where MAKHO='" + txtMaKho.Text + "' and NGAYNHAPHANG='" + txtNgayNhapHang.Text + "' and TENVL=N'" + txtTenVL.Text + "'";
                provider.thucHienCauTruyVan(query1);
                provider.thucHienCauTruyVan(query2);
                MessageBox.Show("Đã sửa thành công");
                loadHangTrongKho();
                btnLuu3.Enabled = false;
                hangReset();
            }
        }

        private void btnSua3_Click_1(object sender, EventArgs e)
        {
            if (txtMaKho.Text != string.Empty)
            {
                btnLuu3.Enabled = true;
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn vật liệu cần sửa", "Thông báo", MessageBoxButtons.OK);
            }
        }

        private void btnThem3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Vật liệu sắp thêm đã có trong cửa hàng hay chưa", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                themPhieuNhapHang t = new themPhieuNhapHang();
                t.ShowDialog();
                loadHangTrongKho();
            }
            else
            {
                themVLDaCoTrongCuaHang them = new themVLDaCoTrongCuaHang();
                them.ShowDialog();
                loadHangTrongKho();
            }
        }

        private void btnDSKho_Click(object sender, EventArgs e)
        {
            formDanhSachCacKho f = new formDanhSachCacKho();
            f.ShowDialog();
        }


        private void btnSua4_Click(object sender, EventArgs e)
        {
            txtDiaChi4.Enabled = true;
            txtNgaySinh4.Enabled = true;
            txtSDT4.Enabled = true;
            txtTenNV4.Enabled = true;
            cbbGioiTinh.Enabled = true;
            txtTenDangNhap.Enabled = true;
            txtNhapLaiMatKhau4.Enabled = true;
            txtMatKhauMoi.Enabled = true;
            btnLuu4.Enabled = true;
            txtMatKhauCu.Enabled = true;
        }

        //void hienThiTK()
        //{
        //   string query = "select hoten as N'Họ Tên', phai as N'Giới tính', DIACHI as N'Địa chỉ', SDT as N'Số điện thoại', NGAYSINH as N'Ngày sinh', TENDANGNHAP as N'Tên đăng nhập' from nhanvien n, taikhoan t where n.MANV=t.MANV and n.MANV='ADMIN'";
        //    dtgvTK.DataSource = provider.layDuLieuDangBang(query);
        //}

        void loadChiTietTK()
        {
            //cách 1 load dữ liệu lên
            //txtTenNV4.Text = dtgvTK.Rows[0].Cells[0].Value.ToString();
            //cbbGioiTinh.SelectedItem = dtgvTK.Rows[0].Cells[1].Value.ToString();
            //txtDiaChi4.Text = dtgvTK.Rows[0].Cells[2].Value.ToString();
            //txtSDT4.Text = dtgvTK.Rows[0].Cells[3].Value.ToString();
            //txtNgaySinh4.Text = DateTime.Parse(dtgvTK.Rows[0].Cells[4].Value.ToString()).ToString("dd/MM/yyyy");
            //txtTenDangNhap.Text = dtgvTK.Rows[0].Cells[5].Value.ToString();
            string query = "select hoten as N'Họ Tên', phai as N'Giới tính', DIACHI as N'Địa chỉ', SDT as N'Số điện thoại', NGAYSINH as N'Ngày sinh', TENDANGNHAP as N'Tên đăng nhập' from nhanvien n, taikhoan t where n.MANV=t.MANV and n.MANV='ADMIN'";
            SqlConnection con = new SqlConnection(provider.connectionSTR);
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                txtTenNV4.Text = reader[0].ToString();
                cbbGioiTinh.SelectedItem = reader[1].ToString();
                txtDiaChi4.Text = reader[2].ToString();
                txtSDT4.Text = reader[3].ToString();
                txtNgaySinh4.Text =DateTime.Parse(reader[4].ToString()).ToString("dd/MM/yyyy");
                txtTenDangNhap.Text = reader[5].ToString();
            }
            reader.Close();
            con.Close();
        }

        bool kTraDungMK()
        {
            string query = "select count(*) from taikhoan where matkhau='"+txtMatKhauCu.Text+"' and Manv='ADMIN'";
            int kq = (int)provider.layLenGTDauTien(query);
            if (kq >= 1)
                return true;
            return false;
        }

        bool KtraThongTinADMINRong()
        {
            if (txtTenNV4.Text == string.Empty || cbbGioiTinh.SelectedIndex == 0 || txtDiaChi4.Text == string.Empty || txtSDT4.Text == string.Empty || txtNgaySinh4.Text == string.Empty || txtTenDangNhap.Text == string.Empty || txtMatKhauCu.Text == string.Empty || txtMatKhauMoi.Text == string.Empty || txtNhapLaiMatKhau4.Text == string.Empty)
                return true;
            return false;
        }

        bool KTraTrungMK()
        {
            if (txtMatKhauMoi.Text.Equals(txtNhapLaiMatKhau4.Text))
            {
                return true;
            }
            return false;
        }

        bool KtraTrungTDN()
        {
            string query = "select count(*) from taikhoan where tendangnhap='"+txtTenDangNhap.Text+"'";
            int kq = (int)provider.layLenGTDauTien(query);
            if (kq >= 1)
                return true;
            return false;
        }

        private void btnLuu4_Click(object sender, EventArgs e)
        {
           if(!KtraThongTinADMINRong())
               {
               if(txtSDT4.Text.Trim().Length<12)
                   if (!KtraTrungTDN())
                   {
                       {
                           if (kTraDungMK())
                           {
                               if (KTraTrungMK())
                               {
                                   if (!txtMatKhauMoi.Text.Contains("'"))
                                   {
                                       if (MessageBox.Show("Bạn có chắc chắn muốn lưu chứ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                       {
                                           string query1 = "set dateformat DMY update nhanvien set HOTEN=N'" + txtTenNV4.Text + "', PHAI=N'" + cbbGioiTinh.SelectedItem + "', DIACHI=N'" + txtDiaChi4.Text + "', SDT='" + txtSDT4.Text + "', NGAYSINH='" + txtNgaySinh4.Text + "' where MANV='ADMIN'";
                                           string query2 = "update taikhoan set TENDANGNHAP='" + txtTenDangNhap.Text + "', MATKHAU='" + txtMatKhauMoi.Text + "' where MANV='ADMIN' ";
                                           provider.thucHienCauTruyVan(query1);
                                           provider.thucHienCauTruyVan(query2);
                                           MessageBox.Show("Đã sửa thông tin thành công. Mật khẩu đã được áp dụng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                           txtMatKhauCu.Text = string.Empty;
                                           txtMatKhauMoi.Text = string.Empty;
                                           txtNhapLaiMatKhau4.Text = string.Empty;
                                           btnLuu4.Enabled = false;
                                           txtTenNV4.Enabled = false;
                                           txtDiaChi4.Enabled = false;
                                           txtSDT4.Enabled = false;
                                           txtNgaySinh4.Enabled = false;
                                           txtTenDangNhap.Enabled = false;
                                           txtMatKhauCu.Enabled = false;
                                           txtMatKhauMoi.Enabled = false;
                                           txtNhapLaiMatKhau4.Enabled = false;
                                           cbbGioiTinh.Enabled = false;
                                       }
                                   }
                                   else
                                   {
                                       MessageBox.Show("Mật khẩu không được chứa kí tự đặc biệt bạn nhé", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                   }
                               }
                               else
                               {
                                   MessageBox.Show("Hai mật khẩu chưa trùng khớp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                               }
                           }
                           else
                           {
                               MessageBox.Show("Mật khẩu cũ chưa đúng với tài khoản");
                           }
                       }
                   }
                   else
                   {
                      MessageBox.Show("Tên đăng nhập đã bị trùng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                   }
                   else
                   {
                       MessageBox.Show("Số điện thoại không vượt quá 12 số bạn nhé", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   }
               }
               else
               {
                   MessageBox.Show("Bạn chưa điền đầy đủ thông tin ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
               }
           }

        private void btnThoat4_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Bạn có chắc chắn muốn đăng xuất chứ","Thông báo",MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes)
            this.Close();
        }

        private void btnXemChiTiet_Click(object sender, EventArgs e)
        {
            if (dtgvDoanhThu.Rows.Count != 0)
            {
                chiTietHD_ADMIN c = new chiTietHD_ADMIN();
                c.nhan(dtgvDoanhThu.CurrentRow.Cells[1].Value.ToString());
                c.ShowDialog();
            }
            else
            {
                MessageBox.Show("Chưa có phiếu hàng nào để xem chi tiết","Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoaTatCa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa tất cả chứ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string query = "delete TAIKHOAN where chucvu=N'Nhân viên'";
                provider.thucHienCauTruyVan(query);
                string query2 = "delete chamcong ";
                provider.thucHienCauTruyVan(query2);
                string query3 = "delete CT_PHIEUHANG";
                provider.thucHienCauTruyVan(query3);
                string query4 = "delete PHIEUHANG ";
                provider.thucHienCauTruyVan(query4);
                string query5 = "delete VATLIEU";
                provider.thucHienCauTruyVan(query5);
                string query6 = "delete kho";
                provider.thucHienCauTruyVan(query6);
                string query7 = "delete nhanvien where manv like 'NV%'";
                provider.thucHienCauTruyVan(query7);
                MessageBox.Show("Đã xóa tất cả dữ liệu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        void loadHangTrongkho()
        {
            string query = "select VATLIEU.MAVL as N'Mã vật liệu', TENVL as N'Tên vật liệu', SOLUONG as N'Số lượng', DONVITINH as N'Đơn vị tính', DONGIA as N'Đơn giá', TENKHO as N'Tên kho', NGAYNHAPHANG as N' Ngày nhập hàng' from vatlieu, kho where VATLIEU.MAKHO= kho.MAKHO";
            dtgvVatLieu.DataSource = provider.layDuLieuDangBang(query);
        }

        private void ADMIN_Load(object sender, EventArgs e)
        {
            loadHangTrongkho();
        }

        private void txtTenVL5_TextChanged(object sender, EventArgs e)
        {
            if (txtTenVL5.Text == string.Empty)
            {
                loadHangTrongkho();
            }
            else
            {
                string query = "exec timVl @tenvl=N'" + txtTenVL5.Text + "'";
                dtgvVatLieu.DataSource = provider.layDuLieuDangBang(query);
            }
        }


    }
}
