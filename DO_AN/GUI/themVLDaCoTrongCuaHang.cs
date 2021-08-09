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
    public partial class themVLDaCoTrongCuaHang : Form
    {
        public themVLDaCoTrongCuaHang()
        {
            InitializeComponent();
            loadCbbTenVL();
            loadTenKho();
        }

        DataProvider provider = new DataProvider();
        void loadCbbTenVL()
        {
            string query = "select tenvl from VATLIEU";
            DataTable table = provider.layDuLieuDangBang(query);
            cbbTenVL.DataSource = table;
            cbbTenVL.DisplayMember = "tenvl";
            cbbTenVL.ValueMember = "tenvl";
        }

        void loadTenKho()
        {
            string query = "select * from kho";
            DataTable table = provider.layDuLieuDangBang(query);
            cbbTenKho.DataSource = table;
            cbbTenKho.DisplayMember = "tenkho";
            cbbTenKho.ValueMember = "makho";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn đã chắc chắn muốn lưu chưa", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string query1 = "select soluong from vatlieu where tenvl=N'" + cbbTenVL.SelectedValue + "'";
                int SoLuongCu = (int)provider.layLenGTDauTien(query1);
                int soLuongMoi = SoLuongCu + int.Parse(txtSL.Text);
                string query = "update VATLIEU set SOLUONG='" + soLuongMoi + "',DONGIA='" + txtDonGia.Text + "', DONVITINH=N'" + txtDonViTinh.Text + "',NGAYNHAPHANG=getdate() where TENVL=N'" + cbbTenVL.SelectedValue + "' ";
                provider.thucHienCauTruyVan(query);
                MessageBox.Show("Đã thêm  vật liệu " + cbbTenVL.SelectedValue + " thành công ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtDonGia.Text = string.Empty;
            txtDonViTinh.Text = string.Empty;
            txtNgayNhapHang.Text = string.Empty;
            txtSL.Text = string.Empty;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
