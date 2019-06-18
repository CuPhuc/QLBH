using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;

namespace QUANLYBANHANG
{
    class FunctionConnect
    {
        public static SqlConnection Con;
        public static void connect()
        {
            Con = new SqlConnection();
            Con.ConnectionString = "Data Source=DESKTOP-GR6KUTJ\\SQLEXPRESS;Initial Catalog=quanlybanhang.mdf;Integrated Security=True";
            Con.Open();

            if (Con.State == ConnectionState.Open)
            {
                MessageBox.Show("Kết nối dữ liệu thành công !");
            }
            else MessageBox.Show("Kết nối dữ liệu thất bại !");
        }

        public static void disconnect()
        {
            //conn = new SqlConnection("Data Source=DESKTOP-NSOO413\\SQLEXPRESS;Initial Catalog=quanlybanhang;Integrated Security=True");
            //conn.Open();

            if (Con.State == ConnectionState.Open)
            {
                Con.Close();
                Con.Dispose();
                Con = null;
            }
        }

        public static DataTable getdata(String sql)
        {
            SqlDataAdapter my_sql = new SqlDataAdapter(sql,Con);
            //my_sql.SelectCommand = new SqlCommand();
            //my_sql.SelectCommand.Connection = conn;
            //my_sql.SelectCommand.CommandText = sql;
            DataTable table = new DataTable();
            my_sql.Fill(table);

            return table;
        }

        public static void runsql(String sql)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Con;
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
        }

        public static bool checkkey(String sql)
        {
            SqlDataAdapter my_sql = new SqlDataAdapter(sql,Con);
            my_sql.SelectCommand = new SqlCommand();
            my_sql.SelectCommand.Connection = Con;
            my_sql.SelectCommand.CommandText = sql;
            DataTable table = new DataTable();
            my_sql.Fill(table);

            if (table.Rows.Count > 0)
                return true;
            else return false;
        }
    }
}
