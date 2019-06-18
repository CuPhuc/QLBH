using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QUANLYBANHANG
{
    public partial class frmDMKhachhang : Form
    {
        public frmDMKhachhang()
        {
            InitializeComponent();
        }

        DataTable tblKhach;
        private void frmDMKhachhang_Load(object sender, EventArgs e)
        {
            Load_DataGridView();
            Disable_Button();
        }

        private void Load_DataGridView()
        {
            string sql = "SELECT * FROM tblKhach";
            tblKhach = FunctionConnect.getdata(sql);
            DataGridView.DataSource = tblKhach;

            DataGridView.Columns[0].HeaderText = "Mã nhân viên";
            DataGridView.Columns[0].Width = 50;

            DataGridView.Columns[1].HeaderText = "Tên nhân viên";
            DataGridView.Columns[1].Width = 209;

            DataGridView.Columns[2].HeaderText = "Địa chỉ";
            DataGridView.Columns[2].Width = 210;

            DataGridView.Columns[3].HeaderText = "Điện thoại";
            DataGridView.Columns[3].Width = 100;
        }

        private void DataGridView_Click(object sender, EventArgs e)
        {
            if (btnLuu.Enabled == true)
            {
                MessageBox.Show("Bạn không thể Sửa khi đang ở chế độ thêm !");
                btnSua.Enabled = false;
            }
            else
            {
                btnSua.Enabled = true;
                Enable_Button();

                txtMakhach.Text = DataGridView.CurrentRow.Cells[0].Value.ToString();
                txtTenkhach.Text = DataGridView.CurrentRow.Cells[1].Value.ToString();
                txtDiachi.Text = DataGridView.CurrentRow.Cells[2].Value.ToString();
                mskDienthoai.Text = DataGridView.CurrentRow.Cells[3].Value.ToString();
            }
        }

        public void Enable_Button()
        {
            txtTenkhach.Enabled = true;
            txtDiachi.Enabled = true;
            mskDienthoai.Enabled = true;

            btnBoqua.Enabled = true;
        }

        public void Disable_Button()
        {
            txtMakhach.Enabled = false;
            txtTenkhach.Enabled = false;
            txtDiachi.Enabled = false;
            mskDienthoai.Enabled = false;

            btnSua.Enabled = false;
            btnLuu.Enabled = false;
            btnBoqua.Enabled = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMakhach.Text = "";
            txtTenkhach.Text = "";
            txtDiachi.Text = "";
            mskDienthoai.Text = "";

            txtMakhach.Enabled = true;
            btnLuu.Enabled = true;
            Enable_Button();

            //DataGridView.Enabled = false;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                string sql;
                if (txtMakhach.Text.Trim().Length == 0 || txtTenkhach.Text.Trim().Length == 0 ||
                    txtDiachi.Text.Trim().Length == 0 || mskDienthoai.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn cần điền đầy đủ thông tin \nĐể có thể lưu", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    sql = "SELECT Makhach FROM tblKhach WHERE Makhach ='" + txtMakhach.Text.Trim() + "'";
                    if (FunctionConnect.checkkey(sql))
                    {
                        MessageBox.Show("Mã khách bạn muốn lưu đã tồn tại ! \nVui lòng chọn mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMakhach.Focus();
                        return;
                    }
                    if (MessageBox.Show("Bạn có muốn lưu không ?", "THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        sql = "INSERT INTO " +
                        "tblKhach (Makhach, Tenkhach, Diachi, Dienthoai) " +
                        "VALUES ('" + txtMakhach.Text + "','" + txtTenkhach.Text + "','" + txtDiachi.Text + "','" + mskDienthoai.Text + "')";
                        FunctionConnect.runsql(sql);
                        Load_DataGridView();
                        MessageBox.Show("Thêm thành công !");
                    }
                }
                Disable_Button();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi: ", ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (tblKhach.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu !");
                return;
            }
            else if (MessageBox.Show("Bạn có muốn xoá \nKhách Hàng: " + txtTenkhach.Text + " không ?", "THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sql = "DELETE tblKhach WHERE Makhach = '" + txtMakhach.Text + "'";
                FunctionConnect.runsql(sql);
                Load_DataGridView();

                MessageBox.Show("Xoá thành công !");
            }
            Disable_Button();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Bạn có muốn sửa không", "THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string sql = "UPDATE tblKhach " +
                        "SET Tenkhach = '" + txtTenkhach.Text + "', Diachi = '" + txtDiachi.Text + "', Dienthoai = '" + mskDienthoai.Text + "'" +
                        "WHERE Makhach = '" + txtMakhach.Text + "'";

                    FunctionConnect.runsql(sql);

                    Load_DataGridView();

                    MessageBox.Show("Sửa thành công !");
                }
                Disable_Button();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi: ", ex.Message);
            }
        }

        private void btnBoqua_Click(object sender, EventArgs e)
        {
            txtMakhach.Text = "";
            txtTenkhach.Text = "";
            txtDiachi.Text = "";
            mskDienthoai.Text = "";

            Disable_Button();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
