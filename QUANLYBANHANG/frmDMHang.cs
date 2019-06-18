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
    public partial class frmDMHang : Form
    {
        public frmDMHang()
        {
            InitializeComponent();
        }

        DataTable tblHang;
        private void frmDMHang_Load(object sender, EventArgs e)
        {
            Load_DataGridView();
            Disable_Enitity();

            string sql2 = "Select Machatlieu From tblChatlieu";
            cboMachatlieu.DataSource = FunctionConnect.getdata(sql2);
            cboMachatlieu.DisplayMember = "Machatlieu";
        }

        private void Enable_Entity()
        {
            txtTenhang.Enabled = true;
            cboMachatlieu.Enabled = true;
            txtSoluong.Enabled = true;
            txtDongianhap.Enabled = true;
            txtDongiaban.Enabled = true;
            txtAnh.Enabled = true;
            txtGhichu.Enabled = true;
        }
        private void Disable_Enitity()
        {
            txtMahang.Enabled = false;
            txtTenhang.Enabled = false;
            cboMachatlieu.Enabled = false;
            txtSoluong.Enabled = false;
            txtDongianhap.Enabled = false;
            txtDongiaban.Enabled = false;
            txtAnh.Enabled = false;
            txtGhichu.Enabled = false;

            btnOpenLocalImage.Enabled = false;
            btnSua.Enabled = false;
            btnBoqua.Enabled = false;
            btnLuu.Enabled = false;
        }
        private void Clear_WriteText()
        {
            txtMahang.Clear();
            txtTenhang.Clear();
            cboMachatlieu.Text = "";
            txtSoluong.Clear();
            txtDongianhap.Clear();
            txtDongiaban.Clear();
            txtAnh.Clear();
            txtGhichu.Clear();
            picAnh.Image = null;
        }

        private void Load_DataGridView()
        {
            //string sql = "Select Mahang, Tenhang, Tenchatlieu, SoLuong, Dongianhap, Dongiaban, Anh, Ghichu " +
            //            "from tblHang Inner Join tblChatlieu on tblHang.Machatlieu = tblChatlieu.Machatlieu";
            string sql = "Select * from tblHang";
            tblHang = FunctionConnect.getdata(sql);
            DataGridView.DataSource = tblHang;

            DataGridView.Columns[0].HeaderText = "Mã Hàng";
            DataGridView.Columns[0].Width = 80;

            DataGridView.Columns[1].HeaderText = "Tên Hàng";
            DataGridView.Columns[1].Width = 120;

            DataGridView.Columns[2].HeaderText = "Mã Chất Liệu";
            DataGridView.Columns[2].Width = 100;

            DataGridView.Columns[3].HeaderText = "Số Lượng";
            DataGridView.Columns[3].Width = 80;

            DataGridView.Columns[4].HeaderText = "Đơn Giá Nhập";
            DataGridView.Columns[4].Width = 100;

            DataGridView.Columns[5].HeaderText = "Đơn Giá Bán";

            DataGridView.Columns[6].HeaderText = "Ảnh";

            DataGridView.Columns[7].HeaderText = "Ghi Chú";
        }

        private void DataGridView_Click(object sender, EventArgs e)
        {
            Enable_Entity();
            if(btnLuu.Enabled == true)
            {
                MessageBox.Show("Bạn không thể Sửa khi đang ở chế độ thêm !");
                btnSua.Enabled = false; 
            }
            else
            {
                btnSua.Enabled = true;
                btnOpenLocalImage.Enabled = true;

                txtMahang.Text = DataGridView.CurrentRow.Cells[0].Value.ToString();

                txtTenhang.Text = DataGridView.CurrentRow.Cells[1].Value.ToString();

                cboMachatlieu.Text = DataGridView.CurrentRow.Cells[2].Value.ToString();

                txtSoluong.Text = DataGridView.CurrentRow.Cells[3].Value.ToString();

                txtDongianhap.Text = DataGridView.CurrentRow.Cells[4].Value.ToString();

                txtDongiaban.Text = DataGridView.CurrentRow.Cells[5].Value.ToString();

                txtAnh.Text = DataGridView.CurrentRow.Cells[6].Value.ToString();
                if (txtAnh.Text != null)
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = openFileDialog.Filter = "JPG file(*.jpg)|*.jpg|All file(*.*)|*.*";
                    openFileDialog.FileName = txtAnh.Text;
                    picAnh.ImageLocation = openFileDialog.FileName;
                }

                txtGhichu.Text = DataGridView.CurrentRow.Cells[7].Value.ToString();
            }

            btnBoqua.Enabled = true;
        }

        private void btnOpenLocalImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = openFileDialog.Filter = "JPG file(*.jpg)|*.jpg|All file(*.*)|*.*";

            openFileDialog.FilterIndex = 1;

            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                picAnh.ImageLocation = openFileDialog.FileName;

                txtAnh.Text = openFileDialog.FileName;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMahang.Enabled = true;

            btnOpenLocalImage.Enabled = true;
            btnSua.Enabled = false;
            btnLuu.Enabled = true;

            Enable_Entity();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xoá: " + DataGridView.CurrentRow.Cells[1].Value.ToString() + " không ?", "THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sql = "DELETE tblHang WHERE MaHang = '" + txtMahang.Text + "'";
                FunctionConnect.runsql(sql);
                Load_DataGridView();

                MessageBox.Show("Xoá thành công !");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Bạn có muốn sửa dữ liều hàng này không ?", "THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string sql = "UPDATE tblHang " +
                        "SET Tenhang = '" + txtTenhang.Text + "', Machatlieu = '" + cboMachatlieu.Text + "', Soluong = '" + txtSoluong.Text + "'," +
                        "Dongianhap = '" + txtDongianhap.Text + "', Dongiaban = '" + txtDongiaban.Text + "', Anh = '" + txtAnh.Text + "', Ghichu = '" + txtGhichu.Text + "'" +
                        "WHERE MaHang = '" + txtMahang.Text + "'";

                    FunctionConnect.runsql(sql);

                    Load_DataGridView();

                    MessageBox.Show("Sửa thành công !");
                }
                Disable_Enitity();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi trong quá trình sửa ! \nError: " + ex.Message);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                string sql;
                if (txtMahang.Text.Trim().Length == 0 || txtTenhang.Text.Trim().Length == 0 ||
                    txtDongianhap.Text.Trim().Length == 0 || txtDongiaban.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn cần điền đầy đủ thông tin \nĐể có thể lưu", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    sql = "SELECT Mahang FROM tblHang WHERE Mahang ='" + txtMahang.Text.Trim() + "'";
                    if (FunctionConnect.checkkey(sql))
                    {
                        MessageBox.Show("Mã bạn muốn lưu đã tồn tại ! \nVui lòng chọn mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMahang.Focus();
                        return;
                    }
                    if (MessageBox.Show("Bạn có muốn lưu không ?", "THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        sql = "INSERT INTO " +
                        "tblHang (Mahang, Tenhang, Machatlieu, Soluong, Dongianhap, Dongiaban, Anh, Ghichu) " +
                        "VALUES ('" + txtMahang.Text + "','" + txtTenhang.Text + "','" + cboMachatlieu.Text + "','" + txtSoluong.Text + "','" + txtDongianhap.Text + "','" + txtDongiaban.Text + "','" + txtAnh.Text + "','" + txtGhichu.Text + "')";
                        FunctionConnect.runsql(sql);
                        Load_DataGridView();
                        MessageBox.Show("Thêm thành công !");
                    }
                    Disable_Enitity();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi trong quá trình lưu lưu phát sinh lỗi ! \nError: " + ex.Message);
            }
        }

        private void btnBoqua_Click(object sender, EventArgs e)
        {
            Clear_WriteText();
            Disable_Enitity();
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng tìm kiếm chưa được khởi tạo [Comming Soon !]");
        }

        private void btnHienthi_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng hiển thị chưa được khởi tạo [Comming Soon !]");
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
