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
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            FunctionConnect.connect();
        }

        private void mnuChatlieu_Click(object sender, EventArgs e)
        {
            frmDMChatlieu Open_Form1 = new frmDMChatlieu();
            Open_Form1.Show();
        }

        private void mnuNhanvien_Click(object sender, EventArgs e)
        {
            frmDMNhanvien Open_Form2 = new frmDMNhanvien();
            Open_Form2.Show();
        }

        private void mnuKhachhang_Click(object sender, EventArgs e)
        {
            frmDMKhachhang Open_Form3 = new frmDMKhachhang();
            Open_Form3.Show();
        }

        private void mnuHanghoa_Click(object sender, EventArgs e)
        {
            frmDMHang Open_Form4 = new frmDMHang();
            Open_Form4.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
