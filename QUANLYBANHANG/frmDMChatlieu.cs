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
    public partial class frmDMChatlieu : Form
    {
        public frmDMChatlieu()
        {
            InitializeComponent();
        }

        DataTable tbl;

        private void frmDMChatlieu_Load(object sender, EventArgs e)
        {
            txtMachatlieu.Enabled = false;
            btnLuu.Enabled = false;
            btnBoqua.Enabled = false;

            Load_DataGridView();
        }

        public void Load_DataGridView()
        {
            string sql = "SELECT * FROM tblChatlieu";
            tbl = FunctionConnect.getdata(sql);
            dataGridView1.DataSource = tbl;
            dataGridView1.Columns[0].HeaderText = "Mã chất liệu";
            dataGridView1.Columns[1].HeaderText = "Tên chất liệu";
            dataGridView1.Columns[0].Width = 200;
            dataGridView1.Columns[1].Width = 200;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMachatlieu.Text = "";
            txtTenchatlieu.Text = "";

            txtMachatlieu.Enabled = true;
            txtMachatlieu.Focus();
            btnLuu.Enabled = true;
            btnBoqua.Enabled = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if(txtMachatlieu.Text.Trim().Length == 0 || txtTenchatlieu.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn chưa nhập dữ liệu", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                // Kiểm tra key Mã Chất Liệu
                sql = "Select MaChatlieu From tblChatlieu where MaChatlieu='" + txtMachatlieu.Text.Trim() + "'";
                if (FunctionConnect.checkkey(sql))
                {
                    MessageBox.Show("Mã chất liệu hiện tại đang tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMachatlieu.Focus();
                    return;
                }

                // Thêm dữ liệu
                if (MessageBox.Show("Bạn có muốn thêm không", "THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    sql = "INSERT INTO tblChatlieu (Machatlieu, Tenchatlieu) VALUES ('" + txtMachatlieu.Text + "','" + txtTenchatlieu.Text + "')";
                    FunctionConnect.runsql(sql);
                    Load_DataGridView();

                    MessageBox.Show("Thêm thành công !");

                    txtMachatlieu.Enabled = false;
                    btnLuu.Enabled = false;
                    btnBoqua.Enabled = false;
                }
            }
        }
        private void dataGridView1_Click(object sender, EventArgs e)
        {
            int id;
            id = dataGridView1.CurrentCell.RowIndex;
            txtMachatlieu.Text = dataGridView1.Rows[id].Cells[0].Value.ToString();
            txtTenchatlieu.Text = dataGridView1.Rows[id].Cells[1].Value.ToString();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMachatlieu.Text.Trim().Length == 0 || txtTenchatlieu.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn chưa nhập dữ liệu", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                sql = "SELECT Machatlieu From tblChatlieu where Machatlieu ='" + txtMachatlieu.Text.Trim() + "'";
                if (FunctionConnect.checkkey(sql))
                {
                    MessageBox.Show("Thông tin bạn cập nhập đã tồn tại !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMachatlieu.Focus();
                    return;
                }
                if (MessageBox.Show("Bạn có muốn sửa không ?", "THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    sql = "UPDATE tblChatlieu Tenchatlieu = '" + txtTenchatlieu.Text + "' WHERE Machatlieu = '" + txtMachatlieu.Text + "'";
                    FunctionConnect.runsql(sql);
                    Load_DataGridView();

                    MessageBox.Show("Sửa thành công !");

                    //txtMachatlieu.Enabled = false;
                    btnLuu.Enabled = false;
                    btnBoqua.Enabled = false;
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xoá không", "THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                string sql = "DELETE KHACHHANG WHERE KHACHHANG.MAKH = '" + txtMachatlieu.Text + "'";
                FunctionConnect.runsql(sql);
                Load_DataGridView();

                MessageBox.Show("Xoá thành công !");

            }
        }

        private void btnBoqua_Click(object sender, EventArgs e)
        {
            txtMachatlieu.Text = "";
            txtTenchatlieu.Text = "";

            txtMachatlieu.Enabled = false;
            btnLuu.Enabled = false;
            btnBoqua.Enabled = false;
        }
    }
}
