using System;
using System.Collections.Generic;
using System.ComponentModel;
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
namespace DO_AN.GUI
{
    public partial class ThemSP : Form
    {
        public ThemSP()
        {
            InitializeComponent();
            loadCbbMaKhoa();
        }
        DataProvider provider = new DataProvider();
        Home ad = new Home();
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void loadCbbMaKhoa()
        {
            string query = "select * from kho";
            cbbMaKho.DataSource = provider.layDuLieuDangBang(query);
            cbbMaKho.DisplayMember = "tenkho";
            cbbMaKho.ValueMember = "makho";
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            txtMaVL.Text = string.Empty;
            txtTenVL.Text = string.Empty;
            txtSoluong.Text = string.Empty;
            txtDVT.Text = string.Empty;
            txtDongia.Text = string.Empty;
            cbbMaKho.SelectedIndex = 0;
        }

        bool kTraRong()
        {
            if (txtTenVL.Text == string.Empty || txtDongia.Text == string.Empty || txtMaVL.Text == string.Empty || txtSoluong.Text.Trim() == string.Empty || txtMaVL.Text.Trim() == string.Empty || txtDVT.Text == string.Empty)
                return true;
            return false;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!kTraRong())
            {
                if (provider.kTTrungMaSP(txtMaVL.Text))
                {
                    MessageBox.Show("Mã vật liệu bạn nhập đã bị trùng! Vui lòng nhập mã khác");
                }
                else
                {
                    string query1 = "SET DATEFORMAT DMY INSERT INTO VATLIEU VALUES  ('"+txtMaVL.Text.Trim()+"',N'"+txtTenVL.Text.Trim()+"','"+txtSoluong.Text.Trim()+"',N'"+txtDVT.Text.Trim()+"','"+txtDongia.Text.Trim()+"','"+cbbMaKho.SelectedValue.ToString().Trim()+"',getdate())";
                    provider.thucHienCauTruyVan(query1);
                    MessageBox.Show("Đã thêm vật liệu thành công");
                    this.Close();
                    ad.Focus();
                }
            }
            else
            {
                MessageBox.Show("Bạn chưa nhập đầy đủ thông tin vật liệu");
            }
        }
    }
}
