using DO_AN.DAO;
using DO_AN.GUI;
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_DangNhap_Click(object sender, EventArgs e)
        {
            if (!txtMatKhau.Text.Contains("'"))
            {
                if (kTraDangNhap())
                {
                    if (KTraChu())
                    {
                        MessageBox.Show("Bạn đã đăng nhập thành công bằng quyền ADMIN","Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        ADMIN a = new ADMIN();
                        a.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Bạn đã đăng nhập thành công bằng quyền USER", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        Home h = new Home();
                        h.nhan(txt_TENDN.Text);
                        h.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("Tài khoản hoặc mật khẩu không đúng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Mật khẩu đã chứa kí tự đặc biệt! \nVui lòng nhập lại","Thông báo", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            txt_TENDN.Text = string.Empty;
            txtMatKhau.Text = string.Empty;
            txt_TENDN.Focus();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult r;
            r = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (r == DialogResult.Yes)
                this.Close();
        }

        bool KTraChu()
        {
            string query = "select chucvu  from TAIKHOAN where TENDANGNHAP='"+txt_TENDN.Text+"' and MATKHAU='"+txtMatKhau.Text+"'";
            if (provider.layLenGTDauTienDangChuoi(query) == "Chủ")
                return true;
            return false;
        }

        DataProvider provider = new DataProvider();

        bool kTraDangNhap()
        {
            string query = "EXEC LOGGIN @TENDANGNHAP='"+txt_TENDN.Text+"', @matkhau='"+txtMatKhau.Text+"'";
            int kq=(int)provider.layLenGTDauTien(query);
            if (kq >= 1)
                return true;
            return false;
        }



        private void btnDN_ADMIN_Click_1(object sender, EventArgs e)
        {
            ADMIN a = new ADMIN();
            a.ShowDialog();
        }

        private void lbQuenMK_Click(object sender, EventArgs e)
        {
            quenMK quen = new quenMK();
            quen.ShowDialog();
        }

    }
}
