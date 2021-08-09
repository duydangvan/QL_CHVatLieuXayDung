using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using DO_AN.DAO;
using DO_AN.GUI;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace DO_AN
{
    public partial class Home : Form
    {
        DataProvider provider = new DataProvider();

        //Phương pháp delegate Tên đăng nhập

        public delegate void nhanTDN(string TDN);
        public nhanTDN nhan;

        void layTDN(string TDN)
        {
            txtTenDangNhap.Text = TDN;
        }

        public Home()
        {
            InitializeComponent();
            LoadDSSP();
            LoadDSPHIEUHANG();
            dataGVMH.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgvPhieuhang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            nhan = new nhanTDN(layTDN);
            cbbGioiTinh.DataSource = dsGioiTinh();
        }


        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text != string.Empty)
            {
                string timVLXD = "EXEC TIMVLXD @TENVL=N'" + textBox1.Text + "'";
                dataGVMH.DataSource = provider.layDuLieuDangBang(timVLXD);
                dataGVMH.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            else
            {
                MessageBox.Show("Bạn chưa nhập tên vật liệu cần tìm");
                textBox1.Focus();
            }
        }
        public void LoadDSSP()
        {
            string query = "EXEC HIENTHIVATLIEU1 @SANPHAM=N'SẢN PHẨM'";

            dataGVMH.DataSource = provider.layDuLieuDangBang(query);

        }
        public void LoadDSPHIEUHANG()
        {
            string query = "select PHIEUHANG.MAPHIEU as N'Mã Phiếu',nhanvien.manv as N'Mã nhân viên',TENVATLIEU as N'Tên vật liệu',NGAYTAO as N'Ngày tạo', THANHTIEN as N'Thành tiền',CT_PHIEUHANG.SOLUONG as N'Số lượng',CT_PHIEUHANG.DONGIA as N'Đơn giá', CT_PHIEUHANG.HOTEN_KH as N'Tên khách hàng',CT_PHIEUHANG.sdt as N'Số điện thoại' from VATLIEU, PHIEUHANG, CT_PHIEUHANG, NHANVIEN where VATLIEU.MAVL=PHIEUHANG.MAVL and PHIEUHANG.MAPHIEU=CT_PHIEUHANG.maPhieu and PHIEUHANG.MANV=NHANVIEN.MANV ";
            dtgvPhieuhang.DataSource = provider.layDuLieuDangBang(query);

        }

        void reset()
        {
            txtMaVL.Text = string.Empty;
            txtMaNV.Text = string.Empty;
            txtSoluong.Text = string.Empty;
            txtDVT.Text = string.Empty;
            txtDongia.Text = string.Empty;
            txtMakho.Text = string.Empty;
            txtTenVL.Text = string.Empty;
        }
        void resetPH()
        {
            txtMaPhieu.Text = string.Empty;
            txtMaNV.Text = string.Empty;
            txtMaVL.Text = string.Empty;
            txtNgaytao.Text = string.Empty;
            txtThanhtien.Text = string.Empty;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadDSSP();
            dtgvPhieuhang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            txtMaVL.Enabled = true;
            txtTimKiemMP.Text = string.Empty;
            reset();
        }
        void hienthiVatlieu()
        {
            txtMaVL.Text = dataGVMH.CurrentRow.Cells[0].Value.ToString();
            txtTenVL.Text = dataGVMH.CurrentRow.Cells[1].Value.ToString();
            txtSoluong.Text = dataGVMH.CurrentRow.Cells[2].Value.ToString();
            txtDVT.Text = dataGVMH.CurrentRow.Cells[3].Value.ToString();
            txtDongia.Text = dataGVMH.CurrentRow.Cells[4].Value.ToString();
            txtMakho.Text = dataGVMH.CurrentRow.Cells[5].Value.ToString();
        }
        void hienthiPhieuhang()
        {
            txtMaPhieu.Text = dtgvPhieuhang.CurrentRow.Cells[0].Value.ToString();
            txtMaNV.Text = dtgvPhieuhang.CurrentRow.Cells[1].Value.ToString();
            txtDG.Text = dtgvPhieuhang.CurrentRow.Cells[6].Value.ToString();
            txtSL.Text = dtgvPhieuhang.CurrentRow.Cells[5].Value.ToString();
            textBox2.Text = dtgvPhieuhang.CurrentRow.Cells[2].Value.ToString().Trim();
            txtSDT.Text = dtgvPhieuhang.CurrentRow.Cells[8].Value.ToString();
            txtTenKH.Text = dtgvPhieuhang.CurrentRow.Cells[7].Value.ToString();
            txtThanhtien.Text = dtgvPhieuhang.CurrentRow.Cells[4].Value.ToString();
            txtNgaytao.Text = DateTime.Parse(dtgvPhieuhang.CurrentRow.Cells[3].Value.ToString()).ToString("dd/MM/yyyy");
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            textBox1.Focus();
            textBox1.ForeColor = Color.Black;
        }

        private void dataGridViewmh_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            hienthiVatlieu();

        }
        private void load_All_Click(object sender, EventArgs e)
        {
            LoadDSSP();
            LoadDSPHIEUHANG();
            dataGVMH.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            txtMaVL.Enabled = true;
            txtTimKiemMP.Text = string.Empty;
            reset();
            resetPH();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXoasp_Click(object sender, EventArgs e)
        {
            if (txtMaVL.Text != string.Empty)
            {
                if (MessageBox.Show("Bạn có chắn chắn muốn xóa vật liệu " + txtTenVL.Text.Trim() + " chưa? ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string query3 = "delete from VATLIEU where MAVL='" + txtMaVL.Text.Trim() + "'";

                    provider.thucHienCauTruyVan(query3);
                    LoadDSSP();
                    MessageBox.Show("Đã xóa thành công ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    reset();
                }
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn hóa đơn cần xóa ");
            }
        }

        private void btnSuasp_Click(object sender, EventArgs e)
        {
            if (txtMaVL.Text != string.Empty)
            {
                txtMaVL.Enabled = false;
                txtDongia.Enabled = false;
                btnLuusp.Enabled = true;
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn vật liệu cần sửa đó");
            }

        }

        private void btnLuusp_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn lưu thông tin vật liệu này " + txtTenVL.Text.Trim() + " chưa? ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string query2 = "update VATLIEU  set TENVL=N'" + txtTenVL.Text.Trim() + "',SOLUONG=N'" + txtSoluong.Text.Trim() + "',DONVITINH='" + txtDVT.Text.Trim() + "',DONGIA='" + txtDongia.Text.Trim() + "' ,MAKHO='" + txtMakho.Text.Trim() + "' where MAVL=N'" + txtMaVL.Text.Trim() + "'";
                provider.thucHienCauTruyVan(query2);
                LoadDSSP();
                MessageBox.Show("Đã sửa thông tin vật liệu " + txtTenVL.Text.Trim() + " thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                reset();
                txtMaVL.Enabled = true;
            }
        }

        private void button4sp_Click(object sender, EventArgs e)
        {
            ThemSP them = new ThemSP();
            them.ShowDialog();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            ThemHD them = new ThemHD();
            them.ganTen(txtMaNV4.Text);
            them.ShowDialog();
            LoadDSPHIEUHANG();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            LoadDSPHIEUHANG();
            dtgvPhieuhang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            txtMaNV.Enabled = true;
            txtTimKiemMP.Text = string.Empty;
            resetPH();
        }

        private void txtTimKiem_Click(object sender, EventArgs e)
        {
            txtTimKiemMP.Text = string.Empty;
            txtTimKiemMP.Focus();
            txtTimKiemMP.ForeColor = Color.Black;
        }

        private void button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dtgvPhieuhang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            hienthiPhieuhang();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaNV.Text != string.Empty)
            {
                if (MessageBox.Show("Bạn có chắn chắn muốn xóa phiếu hàng này " + txtMaPhieu.Text.Trim() + " chưa? ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string query3 = "delete from PHIEUHANG where MAPHIEU='" + txtMaPhieu.Text.Trim() + "'";

                    provider.thucHienCauTruyVan(query3);
                    LoadDSSP();
                    MessageBox.Show("Đã xóa thành công ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    reset();
                }
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn phiều hàng cần xóa ");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMaNV.Text != string.Empty)
            {
                txtMaPhieu.Enabled = false;
                txtMaNV.Enabled = false;
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn phiếu hàng cần sửa");
            }

        }


        private void btnLuu_Click(object sender, EventArgs e)
        {
            //if (MessageBox.Show("Bạn có chắc chắn muốn lưu thông tin phiếu hàng này " + txtMaPhieu.Text.Trim() + " chưa? ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //{
            //    string query2 = "set dateformat DMY update PHIEUHANG  set MANV=N'""',TENVATLIEU=N'" + cbbTenVL.SelectedText.Trim() + "',NGAYTAO='" + txtNgaytao.Text.Trim() + "',THANHTIEN='" + txtThanhtien.Text.Trim() + "' where MAPHIEU=N'" + txtMaPhieu.Text.Trim() + "'";
            //    provider.thucHienCauTruyVan(query2);
            //    LoadDSPHIEUHANG();
            //    MessageBox.Show("Đã sửa thông tin phiếu hàng " + txtMaPhieu.Text.Trim() + " thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    reset();
            //    txtMaPhieu.Enabled = true;
            //}
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void button4_Click(object sender, EventArgs e)
        {
            txtTimKiemMP.Text = string.Empty;
            reset();
        }


        bool KTraTrungTDN()
        {
            string query = "select count(*) from taikhoan where TENDANGNHAP='" + txtTenDangNhap.Text + "'";
            int kq = (int)provider.layLenGTDauTien(query);
            if (kq >= 1)
                return true;
            return false;
        }

        bool kTraRong()
        {
            if (txtTenNV4.Text == string.Empty || txtDiaChi4.Text == string.Empty || txtTenDangNhap.Text == string.Empty || txtMatKhauCu.Text == string.Empty || txtMatKhauMoi.Text == string.Empty || txtNhapLaiMatKhau4.Text == string.Empty || txtSDT4.Text == string.Empty || cbbGioiTinh.SelectedIndex == 0)
                return true;
            return false;
        }

        bool KTraDungMatKhauTaiKhoan()
        {
            string query = "select count(*) from taikhoan, nhanvien where taikhoan.MANV = nhanvien.MANV and nhanvien.manv='" + txtMaNV4.Text.Trim() + "' and MATKHAU='" + txtMatKhauCu.Text + "'";
            int kq = (int)provider.layLenGTDauTien(query);
            if (kq >= 1)
                return true;
            return false;
        }

        private void btnLuu4_Click(object sender, System.EventArgs e)
        {
            if (!kTraRong())
            {
                if (!KTraTrungTDN())
                {
                    if (KTraDungMatKhauTaiKhoan())
                    {
                        if (!txtMatKhauMoi.Text.Contains("'"))
                        {
                            if (txtMatKhauMoi.Text.Equals(txtNhapLaiMatKhau4.Text))
                            {
                                if (MessageBox.Show("Bạn có chắc chắn muốn lưu chứ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                                {
                                    string query1 = "update nhanvien set HOTEN=N'" + txtTenNV4.Text + "', phai=N'" + cbbGioiTinh.SelectedItem + "', DIACHI=N'" + txtDiaChi4.Text + "',SDT='" + txtSDT4.Text + "',NGAYSINH='" + txtNgaySinh4.Value + "' where manv='" + txtMaNV4.Text + "'";
                                    provider.thucHienCauTruyVan(query1);
                                    string query2 = "update taikhoan set TENDANGNHAP='" + txtTenDangNhap.Text + "', MATKHAU='" + txtMatKhauMoi.Text + "' where MANV='" + txtMaNV4.Text + "'";
                                    provider.thucHienCauTruyVan(query2);
                                    MessageBox.Show("Đã sửa thông tin " + txtTenNV4.Text.Trim() + " thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                                MessageBox.Show("Hai mật khẩu chưa trùng khớp với nhau", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Mật khẩu mới không chứa kí tự đặc biệt bạn nhé", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Mật khẩu cũ chưa đúng với tài khoản", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập đã bị trùng. Vui lòng nhập tên đăng nhập khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Bạn chưa nhập thông tin của mình đầy đủ vào ô", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        void loadChiTietTK()
        {
            string query = "select taikhoan.manv, HOTEN,PHAI, DIACHI, SDT, NGAYSINH from nhanvien, taikhoan where nhanvien.MANV=TAIKHOAN.MANV and TENDANGNHAP='" + txtTenDangNhap.Text + "'";
            SqlConnection con = new SqlConnection(provider.connectionSTR);
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                txtMaNV4.Text = reader[0].ToString();
                txtTenNV4.Text = reader[1].ToString();
                cbbGioiTinh.SelectedItem = reader[2].ToString();
                txtDiaChi4.Text = reader[3].ToString();
                txtSDT4.Text = reader[4].ToString();
                txtNgaySinh4.Value = DateTime.Parse(reader[5].ToString());
            }
            reader.Close();
            con.Close();
        }

        private void btnSua4_Click(object sender, System.EventArgs e)
        {
            btnLuu4.Enabled = true;
        }

        private void btnSua4_Click_1(object sender, System.EventArgs e)
        {
            txtDiaChi4.Enabled = true;
            txtMatKhauCu.Enabled = true;
            txtMatKhauMoi.Enabled = true;
            txtNgaySinh4.Enabled = true;
            txtNhapLaiMatKhau4.Enabled = true;
            txtSDT4.Enabled = true;
            txtTenNV4.Enabled = true;
            txtTenDangNhap.Enabled = true;
            cbbGioiTinh.Enabled = true;
            btnLuu4.Enabled = true;
        }

        List<string> GioiTinh = new List<string>();
        public List<string> dsGioiTinh()
        {
            GioiTinh.Add("Hiển thị giới tính");
            GioiTinh.Add("Nam");
            GioiTinh.Add("Nữ");
            return GioiTinh;
        }

        private void Home_Load(object sender, System.EventArgs e)
        {
            loadChiTietTK();
        }

        private void txtSDT4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void btnThoat4_Click(object sender, System.EventArgs e)
        {
            if(MessageBox.Show("Bạn có chắc chắn muốn thoát chứ","Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes)
            this.Close();
        }

        private void textBox1_Click_1(object sender, System.EventArgs e)
        {
            textBox1.Text = string.Empty;
            textBox1.Focus();
            textBox1.ForeColor = Color.Black;
        }

        private void txtTimKiemMP_Click(object sender, System.EventArgs e)
        {
            txtTimKiemMP.Text = string.Empty;
            txtTimKiemMP.Focus();
            txtTimKiemMP.ForeColor = Color.Black;
        }

        private void btnTim_Click(object sender, System.EventArgs e)
        {
            if (txtTimKiemMP.Text != string.Empty)
            {
                string timPH = "EXEC TIMPH @MAPHIEU=N'" + txtTimKiemMP.Text + "'";
                dtgvPhieuhang.DataSource = provider.layDuLieuDangBang(timPH);
                dtgvPhieuhang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            else
            {
                MessageBox.Show("Bạn chưa nhập mã phiếu cần tìm");
                txtTimKiemMP.Focus();
            }
        }


        private void button5_Click(object sender, EventArgs e)
        {
            //MyCrystalReport rpt = new MyCrystalReport();
            //crystalReportViewer1.ReportSource = rpt;
            ////không cần nhập lại pass sql
            //rpt.SetDatabaseLogon("sa", "sa2012", "DESKTOP-4DFMQB4\\SQLEXPRESS", "QL_VLXD");
            //crystalReportViewer1.DisplayToolbar = true;
            //crystalReportViewer1.DisplayStatusBar = false;
            //crystalReportViewer1.Refresh();
        }

        private void btnHienThiTatCa_Click(object sender, EventArgs e)
        {
            LoadDSPHIEUHANG();
        }

        private void textBox1_Click_2(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            textBox1.ForeColor = Color.Black;
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
                txtTimKiemMP.Text = string.Empty;
                            txtTimKiemMP.ForeColor = Color.Black;
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            
        }

        private void txtTimKiemMP_ChangeUICues(object sender, UICuesEventArgs e)
        {

        }

        private void txtTimKiemMP_Click_1(object sender, EventArgs e)
        {
            txtTimKiemMP.Text = string.Empty;
            txtTimKiemMP.ForeColor = Color.Black;
        }








    }
}
