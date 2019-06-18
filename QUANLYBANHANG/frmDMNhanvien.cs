using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QUANLYBANHANG
{
    public partial class frmDMNhanvien : Form
    {
        public frmDMNhanvien()
        {
            InitializeComponent();
        }

        DataTable tblNhanvien;
        string gioitinh;

        private void frmDMNhanvien_Load(object sender, EventArgs e)
        {
            Disable_Entity();
            Load_DataGridView();
        }

        public void Enable_Entity()
        {
            txtTennhanvien.Enabled = true;
            txtDiachi.Enabled = true;

            mskDienthoai.Enabled = true;
            mskNgaysinh.Enabled = true;
        }

        public void Disable_Entity()
        {
            btnXoa.Enabled = false;
            btnSua.Enabled = false;

            btnLuu.Enabled = false;
            btnBoqua.Enabled = false;

            txtManhanvien.Enabled = false;
            txtTennhanvien.Enabled = false;
            txtDiachi.Enabled = false;

            mskDienthoai.Enabled = false;
            mskNgaysinh.Enabled = false;
        }

        private void Load_DataGridView()
        {
            string sql = "SELECT * FROM tblNhanvien";
            tblNhanvien = FunctionConnect.getdata(sql);
            dataGridView1.DataSource = tblNhanvien;

            dataGridView1.Columns[0].HeaderText = "Mã nhân viên";
            dataGridView1.Columns[0].Width = 50;

            dataGridView1.Columns[1].HeaderText = "Tên nhân viên";
            dataGridView1.Columns[1].Width = 209;

            dataGridView1.Columns[2].HeaderText = "Giới tính";
            dataGridView1.Columns[2].Width = 40;

            dataGridView1.Columns[3].HeaderText = "Địa chỉ";
            dataGridView1.Columns[3].Width = 210;

            dataGridView1.Columns[4].HeaderText = "Điện thoại";
            dataGridView1.Columns[4].Width = 100;

            dataGridView1.Columns[5].HeaderText = "Ngày sinh";
            dataGridView1.Columns[5].Width = 80;
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            if (btnLuu.Enabled == true)
            {
                MessageBox.Show("Bạn không thể Sửa khi đang ở chế độ thêm !");
                btnSua.Enabled = false;
            }
            else
            {
                btnXoa.Enabled = true;
                btnSua.Enabled = true;
                btnBoqua.Enabled = true;

                Enable_Entity();

                txtManhanvien.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();

                txtTennhanvien.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();

                if (dataGridView1.CurrentRow.Cells[2].Value.ToString() == "Nam")
                {
                    chkGioitinh.Checked = true;
                }
                else chkGioitinh.Checked = false;

                txtDiachi.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();

                mskDienthoai.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();

                mskNgaysinh.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            }
                
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Enable_Entity();

            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            btnLuu.Enabled = true;
            btnBoqua.Enabled = true;

            txtManhanvien.Enabled = true;

            txtManhanvien.Text = "";
            txtTennhanvien.Text = "";
            txtDiachi.Text = "";
            chkGioitinh.Checked = false;
            mskDienthoai.Text = "";

            txtManhanvien.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (tblNhanvien.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu !");
                return;
            }
            else if(MessageBox.Show("Bạn có muốn xoá \nNhân viên: " + txtTennhanvien.Text + " không ?", "THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sql = "DELETE tblNhanvien WHERE Manhanvien = '" + txtManhanvien.Text + "'";
                FunctionConnect.runsql(sql);
                Load_DataGridView();

                MessageBox.Show("Xoá thành công !");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkGioitinh.CheckState == CheckState.Checked) gioitinh = "Nam";
                else if (chkGioitinh.CheckState == CheckState.Unchecked) gioitinh = "Nữ";

                if (MessageBox.Show("Bạn có muốn sửa không", "THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string sql = "UPDATE tblNhanvien " +
                        "SET Tennhanvien = '" + txtTennhanvien.Text + "', Gioitinh = '" + gioitinh + "', " +
                        "Diachi = '" + txtDiachi.Text + "', Dienthoai = '" + mskDienthoai.Text + "', " +
                        "Ngaysinh = '" + mskNgaysinh.Value.ToString("yyyy/MM/dd") + "' " +
                        "WHERE Manhanvien = '" + txtManhanvien.Text + "'";

                    FunctionConnect.runsql(sql);

                    Load_DataGridView();

                    MessageBox.Show("Sửa thành công !");
                }
                Disable_Entity();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi: ", ex.Message);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkGioitinh.CheckState == CheckState.Checked) gioitinh = "Nam";
                else if (chkGioitinh.CheckState == CheckState.Unchecked) gioitinh = "Nữ";

                string sql;
                if (txtManhanvien.Text.Trim().Length == 0 || txtTennhanvien.Text.Trim().Length == 0 || 
                    txtDiachi.Text.Trim().Length == 0 || mskDienthoai.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn cần điền đầy đủ thông tin \nĐể có thể lưu", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    sql = "SELECT Manhanvien FROM tblNhanvien WHERE Manhanvien ='" + txtManhanvien.Text.Trim() + "'";
                    if (FunctionConnect.checkkey(sql))
                    {
                        MessageBox.Show("Mã bạn muốn lưu đã tồn tại ! \nVui lòng chọn mã khácƯ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtManhanvien.Focus();
                        return;
                    }
                    if (MessageBox.Show("Bạn có muốn lưu không ?", "THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        sql = "INSERT INTO " +
                        "tblNhanvien (Manhanvien, Tennhanvien, Gioitinh, Diachi, Dienthoai, Ngaysinh) " +
                        "VALUES ('" + txtManhanvien.Text + "','" + txtTennhanvien.Text + "','" + gioitinh + "','" + txtDiachi.Text + "','" + mskDienthoai.Text + "','" + mskNgaysinh.Value.ToString("yyyy/MM/dd") + "')";
                        FunctionConnect.runsql(sql);
                        Load_DataGridView();
                        MessageBox.Show("Thêm thành công !");

                        Disable_Entity();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi: ", ex.Message);
            }
        }

        private void btnBoqua_Click(object sender, EventArgs e)
        {
            txtManhanvien.Text = "";
            txtTennhanvien.Text = "";
            chkGioitinh.Checked = false;
            txtDiachi.Text = "";
            mskDienthoai.Text = "";
            Disable_Entity();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mskNgaysinh_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
