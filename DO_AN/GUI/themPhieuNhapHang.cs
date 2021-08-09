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
    public partial class themPhieuNhapHang : Form
    {
        ADMIN ad = new ADMIN();
        DataProvider provider = new DataProvider();
        public themPhieuNhapHang()
        {
            InitializeComponent();
            loadMaKhoa();
        }

        private void themPhieuNhapHang_Load(object sender, EventArgs e)
        {


        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        void loadMaKhoa()
        {
            string query = "select * from kho";
            DataTable table= provider.layDuLieuDangBang(query);
            cbbTenKhoa.DataSource = table;
            cbbTenKhoa.DisplayMember = "tenkho";
            cbbTenKhoa.ValueMember = "makho";
        }

        void resetVL()
        {
            
            txtDonGia.Text = string.Empty;
            txtDonViTinh.Text = string.Empty;           
            txtMaVL.Text = string.Empty;
            txtNgayNhapHang.Text = string.Empty;
            txtSL.Text = string.Empty;            
            txtTenVL.Text = string.Empty;
            cbbTenKhoa.SelectedIndex = 0;
        }
        void resetKho()
        {
            txtMaKho1.Text = string.Empty;
            txtTenKho.Text = string.Empty;
            txtDiaChi.Text = string.Empty;
            txtSDT.Text = string.Empty;
        }

        bool ktTrung()
        {
            string query = "select count(*) from vatlieu where mavl='"+txtMaVL.Text+"'";
            int kq=(int)provider.layLenGTDauTien(query);
            if (kq >= 1)
            {
                return true;
            }
            return false;

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            
            if(txtMaVL.Text!=string.Empty || txtTenVL.Text!=string.Empty || txtSL.Text!=string.Empty || txtDonViTinh.Text!=string.Empty || txtDonGia.Text!=string.Empty)
            {
                string  query1 = "select count(*)  from vatlieu where mavl='"+txtMaVL.Text+"' ";
                 int soCu = (int)provider.layLenGTDauTien(query1);
                 int soLuongMoi = int.Parse(txtSL.Text) + soCu;
                if (!ktTrung())
                {
                    if (MessageBox.Show("Bạn có chắc chắn muốn thêm vật liệu " + txtTenVL.Text.Trim() + " vào chứ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        string query = "SET DATEFORMAT DMY INSERT INTO VATLIEU VALUES  ('" + txtMaVL.Text + "',N'" + txtTenVL.Text + "','" + txtSL.Text + "',N'" + txtDonViTinh.Text + "','" + txtDonGia.Text + "','" + cbbTenKhoa.SelectedValue + "','" + DateTime.Parse(txtNgayNhapHang.Value.ToString()).ToString("dd/MM/yyyy") + "')";
                        string query2 = "update vatlieu set soluong='"+soLuongMoi+"' where mavl='"+txtMaVL.Text+"'";
                        provider.thucHienCauTruyVan(query);
                        provider.thucHienCauTruyVan(query2);
                        MessageBox.Show("Đã thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        resetVL();
                    }
                }
                else
                {
                    MessageBox.Show("Mã vật liệu đã bị trùng");
                }
            }
            else
            {
                MessageBox.Show("Bạn chưa đầy đủ thông tin cần thêm phiếu nhập hàng","Thông báo",MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtSL_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        bool KTKhoTrung()
        {
            string query="select count(*) from kho where tenkho='"+txtTenKho.Text+"' and diachi=N'"+txtDiaChi.Text+"'";
            int kq= (int)provider.layLenGTDauTien(query);
            if(kq>=1)
                return true;
            return false;
        }

        bool kTraTrungMaKho()
        {
            string query = "select count(*) from kho where makho='"+txtMaKho1.Text+"'";
            int kq = (int)provider.layLenGTDauTien(query);
            if (kq >= 1)
                return true;
            return false;
        }

        private void btnThemKho_Click(object sender, EventArgs e)
        {
            if(txtMaKho1.Text!=string.Empty || txtTenKho.Text!=string.Empty || txtDiaChi.Text!=string.Empty || txtSDT.Text!=string.Empty) 
            {
                if(!KTKhoTrung())
                {
                    if(!kTraTrungMaKho())
                    {
                        if (MessageBox.Show("Bạn có chắc chắn thêm " + txtTenKho.Text.Trim() + " vào chứ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            string query = "INSERT INTO KHO VALUES ('" + txtMaKho1.Text + "',N'" + txtTenKho.Text + "',N'" + txtDiaChi.Text + "','" + txtSDT.Text + "')";
                            provider.thucHienCauTruyVan(query);
                            MessageBox.Show("Đã thêm kho thành công");
                            resetKho();
                            loadMaKhoa();
                        }
                    }                
                    else
                    {
                        MessageBox.Show("Mã kho đã bị trùng ","Thông báo",MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                 }
                 else
                    {
                        MessageBox.Show("Kho đã tồn tại trong danh sách", "Thông báo",MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
            }
            else
            {
                MessageBox.Show("Bạn chưa điền đầy đủ thông tin cần thêm kho","Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }
}
