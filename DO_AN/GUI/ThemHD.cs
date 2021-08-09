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
using System.Data.SqlClient;
namespace DO_AN.GUI
{
    public partial class ThemHD : Form
    {
        public ThemHD()
        {
            InitializeComponent();
            Ma = new layMaNV(ganTen);
            LoadDuLieuTennVL();
        }

        public delegate void layMaNV(string MaNV);

        public layMaNV Ma;

        public void ganTen(string MaNV)
        {
            txtMaNV.Text = MaNV;
        }
        DataProvider provider = new DataProvider();
        Home ad = new Home();
        DataSet ds_QLSP = new DataSet();
        bool kTraRong()
        {
            if (txtMaNV.Text == string.Empty || txtMaPhieu.Text == string.Empty || cmbTenvl.Text == string.Empty || txtMaNV.Text == string.Empty || dt2.Text == string.Empty || txtThanhtien.Text.Trim() == string.Empty
                || txtSoluong.Text == string.Empty || txtDongia.Text == string.Empty || txtTenkhach.Text == string.Empty || txtSDT.Text == string.Empty)
                return true;
            return false;
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void LoadDuLieuTennVL()
        {
            string query = "select * from vatlieu";
            DataTable table = provider.layDuLieuDangBang(query);
            cmbTenvl.DataSource = table;
            cmbTenvl.DisplayMember = "TenVl";
            cmbTenvl.ValueMember = "maVL";
        }
        private void btnThem_Click_1(object sender, EventArgs e)
        {
            if (!kTraRong())
            {
                if (provider.kTTrungMaSP(txtMaPhieu.Text))
                {
                    MessageBox.Show("Mã phiếu hàng bạn nhập đã bị trùng! Vui lòng nhập mã khác");
                }
                else
                {
                    string query1 = "SET DATEFORMAT DMY INSERT INTO PHIEUHANG VALUES ('" + txtMaNV.Text.Trim() + "','" + txtMaPhieu.Text.Trim() + "','" + cmbTenvl.SelectedValue + "','" + cmbTenvl.Text.Trim() + "','" + dt2.Value.ToString("dd/MM/yyyy") + "','" + txtThanhtien.Text.Trim() + "')";
                    string query2 = "SET DATEFORMAT DMY INSERT INTO CT_PHIEUHANG VALUES ('" + txtMaPhieu.Text.Trim() + "','" + txtMaNV.Text + "','" + txtSoluong.Text.Trim() + "','" + txtDongia.Text.Trim() + "',N'" + txtTenkhach.Text.Trim() + "','" + txtSDT.Text.Trim() + "')";
                    provider.thucHienCauTruyVan(query1);
                    provider.thucHienCauTruyVan(query2);
                    MessageBox.Show("Đã thêm phiếu hàng thành công");
                    ad.Focus();
                }
            }
            else
            {
                MessageBox.Show("Bạn chưa nhập đầy đủ thông tin vật liệu");
            }
        }

        private void btnHuy_Click_1(object sender, EventArgs e)
        {
            txtMaNV.Text = string.Empty;
            txtMaPhieu.Text = string.Empty;
            dt2.Text = string.Empty;
            txtTenkhach.Text = string.Empty;
            txtSoluong.Text = string.Empty;
            txtDongia.Text = string.Empty;
            txtSDT.Text = string.Empty;
            txtThanhtien.Text = string.Empty;
            cmbTenvl.Text = string.Empty;
        }

        private void btnThoat_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ThemHD_Load(object sender, EventArgs e)
        {
            LoadDuLieuTennVL();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int sl = Convert.ToInt32(txtSoluong.Text);
            int dg = Convert.ToInt32(txtDongia.Text);
            int luong = sl * dg;
            txtThanhtien.Text = luong.ToString();
        }


        private void cmbTenvl_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}