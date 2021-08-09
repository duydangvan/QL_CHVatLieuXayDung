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
    public partial class quenMK : Form
    {
        public quenMK()
        {
            InitializeComponent();
        }

        DataProvider provider = new DataProvider();

        private void btnLayMK_Click(object sender, EventArgs e)
        {
            if (txtTDN.Text != string.Empty || txtSDT.Text != string.Empty)
            {
                string query = "select count(*) from nhanvien, taikhoan where nhanvien.manv=taikhoan.manv and TENDANGNHAP='" + txtTDN.Text + "' and SDT='" + txtSDT.Text + "'";
                int kq = (int)provider.layLenGTDauTien(query);
                if (kq >= 1)
                {
                    txtMKMoi.Enabled = true;
                    txtNhapLaiMK.Enabled = true;
                    btnLuu.Enabled = true;
                    txtTDN.Enabled = false;
                    txtSDT.Enabled = false;
                    button1.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập hoặc số điện thoại sai rồi đó bạn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Bạn không được bỏ trống tên đăng nhập hoặc là mật khẩu bạn nhé!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        bool KTraMatKhauTrung()
        {
            if (txtMKMoi.Text.Equals(txtNhapLaiMK.Text))
            {
                return true;
            }
            return false;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!txtMKMoi.Text.Contains("'"))
            {
                if (KTraMatKhauTrung())
                {
                    if (MessageBox.Show("Bạn có chắc chắn muốn thay đổi mật khẩu chứ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        string query = "update taikhoan set MATKHAU='" + txtMKMoi.Text + "' where TENDANGNHAP='" + txtTDN.Text + "'";
                        provider.thucHienCauTruyVan(query);
                        MessageBox.Show("Đã lưu mật khẩu mới thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtMKMoi.Text = string.Empty;
                        txtNhapLaiMK.Text = string.Empty;
                        txtMKMoi.Enabled = false;
                        txtNhapLaiMK.Enabled = false;
                        btnLuu.Enabled = false;
                        txtTDN.Text = string.Empty;
                        txtSDT.Text = string.Empty;
                        txtTDN.Enabled = true;
                        txtSDT.Enabled = true;
                    }
                }
                else
                {
                    MessageBox.Show("Hai mật khẩu không trùng khớp rồi bạn nhé", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Không được nhập mật khẩu có kí tự đặc biệt bạn nhé!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
