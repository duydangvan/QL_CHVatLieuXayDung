using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DO_AN.DAO;

namespace DO_AN
{
    public partial class ThemNV : Form
    {
        public ThemNV()
        {
            InitializeComponent();
            cbbGioiTinh.DataSource = dSGioiTinh();
        }
        DataProvider provider = new DataProvider();
        ADMIN ad = new ADMIN();

        List<string> gioiTinh = new List<string>();
        public List<string> dSGioiTinh()
        {
            gioiTinh.Add("Chọn giới tính");
            gioiTinh.Add("Nam");
            gioiTinh.Add("Nữ");
            return gioiTinh;
        }

        bool kiemTraTDN()
        {
            string query ="select count(*) from taikhoan where tendangnhap='"+txtTDN.Text.Trim()+"'";
           int kq= provider.layLenGTDauTien(query);
           if (kq == 1)
               return true;
           else
               return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        bool kTraRong()
        {
            if (txtDiaChi.Text == string.Empty || txtLuongcanBan.Text == string.Empty || txtMaNV.Text == string.Empty || txtMatKhau.Text == string.Empty || txtNgaySinh.Text.Trim() == string.Empty || txtNgayVaoLam.Text.Trim() == string.Empty || txtNhapLaiMatKhau.Text == string.Empty || txtSDT.Text == string.Empty || txtTDN.Text == string.Empty || txtTenNV.Text == string.Empty || cbbGioiTinh.SelectedIndex == 0)
                return true;
            return false;
        }

        private void btnThem_Click_1(object sender, EventArgs e)
        {
            if (!kTraRong())
            {
                    if (provider.kTTrungMaNV(txtMaNV.Text.Trim()))
                    {
                        MessageBox.Show("Mã nhân viên bạn nhập đã bị trùng! Vui lòng nhập mã nhân viên khác");
                        txtMaNV.Focus();
                    }
                    else
                    {
                        if(txtSDT.Text.Trim().Length<12)
                        {
                            if (!kiemTraTDN())
                            {
                                if (!txtMatKhau.Text.Contains("'"))
                                {
                                    if (txtMatKhau.Text.Equals(txtNhapLaiMatKhau.Text))
                                    {
                                        if (MessageBox.Show("Bạn có chắc chắn muốn thêm nhân viên " + txtTenNV.Text.Trim() + " vào cửa hàng chứ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                                        {
                                            string query1 = "SET DATEFORMAT DMY INSERT INTO NHANVIEN VALUES ('NV" + txtMaNV.Text.Trim() + "',N'" + txtTenNV.Text.Trim() + "',N'" + cbbGioiTinh.SelectedItem.ToString() + "',N'" + txtDiaChi.Text.Trim() + "','" + txtSDT.Text.Trim() + "','" + txtNgaySinh.Text.Trim() + "','" + txtNgayVaoLam.Text + "','" + txtLuongcanBan.Text.Trim() + "','')";
                                            provider.thucHienCauTruyVan(query1);
                                            string quey2 = "INSERT INTO TAIKHOAN VALUES ('NV" + txtMaNV.Text.Trim() + "','" + txtTDN.Text.Trim() + "','" + txtMatKhau.Text.Trim() + "',N'Nhân viên')";
                                            provider.thucHienCauTruyVan(quey2);
                                            MessageBox.Show("Đã thêm nhân viên thành công");
                                            this.Close();
                                            ad.Focus();
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Hai mật khẩu chưa trùng khớp", "Thông báo");
                                        txtMatKhau.Focus();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Mật khẩu không được chứa kí tự đặc biệt bạn nhé","Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Tên đăng nhập đã tồn tại rồi nha! Mời bạn nhập tên đăng nhập khác ","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
                                txtTDN.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Số điện thoại không vượt quá 12 số bạn nhé", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
            
            }
            else
            {
                MessageBox.Show("Bạn chưa nhập đầy đủ thông tin nhân viên");
            }
            
        }

        private void txtLuongcanBan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }

        private void txtTDN_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            txtDiaChi.Text = string.Empty;
            txtLuongcanBan.Text = string.Empty;
            txtMaNV.Text = string.Empty;
            txtMatKhau.Text = string.Empty;
            txtNgaySinh.Text = string.Empty;
            txtNgayVaoLam.Text = string.Empty;
            txtNhapLaiMatKhau.Text = string.Empty;
            txtSDT.Text = string.Empty;
            txtTDN.Text = string.Empty;
            txtTenNV.Text = string.Empty;
            cbbGioiTinh.SelectedIndex = 0;
        }
    }
}
