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
    public partial class formDanhSachCacKho : Form
    {
        public formDanhSachCacKho()
        {
            InitializeComponent();
            loadDSKho();
        }
        DataProvider proovider = new DataProvider();

        void loadDSKho()
        {
            string query = "select makho as N'Mã Kho', tenkho as N'Tên kho', diachi as N'Địa chỉ', sdt as N'Số điện thoại' from kho";
            dtgvDSKho.DataSource = proovider.layDuLieuDangBang(query);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void reset()
        {
            txtMaKho.Text = string.Empty;
            txtTenKho.Text = string.Empty;
            txtDiaChi.Text = string.Empty;
            txtSoDienThoai.Text = string.Empty;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtMaKho.Text.Trim() != string.Empty)
            {
                if (txtSoDienThoai.Text.Trim().Length < 12)
                {
                    if (MessageBox.Show("Bạn chắc chắn muốn lưu thông tin kho " + txtTenKho.Text.Trim() + " chứ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string query = "update kho set tenkho=N'" + txtTenKho.Text.Trim() + "', diachi=N'" + txtDiaChi.Text.Trim() + "', sdt='" + txtSoDienThoai.Text.Trim() + "' where makho='" + txtMaKho.Text.Trim() + "'";
                        proovider.thucHienCauTruyVan(query);
                        loadDSKho();
                        MessageBox.Show("Đã sửa thông tin kho " + txtTenKho.Text.Trim() + " thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        reset();
                    }
                }
                else
                {
                    MessageBox.Show("Số điện thoại không vượt quá 12 số bạn nhé", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn kho nào cần sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void loadChiTietKho()
        {
            txtMaKho.Text =dtgvDSKho.CurrentRow.Cells[0].Value.ToString();
            txtTenKho.Text = dtgvDSKho.CurrentRow.Cells[1].Value.ToString();
            txtDiaChi.Text = dtgvDSKho.CurrentRow.Cells[2].Value.ToString();
            txtSoDienThoai.Text = dtgvDSKho.CurrentRow.Cells[3].Value.ToString();
        }

        private void dtgvDSKho_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            loadChiTietKho();
        }

        private void txtSoDienThoai_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }
    }
}
