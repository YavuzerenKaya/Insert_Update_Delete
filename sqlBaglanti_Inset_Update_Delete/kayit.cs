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

namespace sqlBaglanti_Inset_Update_Delete
{
    public partial class kayit : Form
    {

        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        public kayit()
        {
            InitializeComponent();
        }

        private void kayit_Load(object sender, EventArgs e)
        {
            con.Close();
        }

        private void openConnectionButton_Click(object sender, EventArgs e)
        {
            ConnectionControl(con);
        }

        private void insertButon_Click(object sender, EventArgs e)
        {
            SqlInsert(cmd, reader);
        }

        private void updateButon_Click(object sender, EventArgs e)
        {
            UpdateThem(cmd, kayitNoNumerik);
        }

        private void deleteButon_Click(object sender, EventArgs e)
        {
            DeleteShipper(cmd, kayitNoNumerik);
        }

        private void showListButon_Click(object sender, EventArgs e)
        {

        }

        public void UpdateThem(SqlCommand command,NumericUpDown numerik )
        {
            try
            {
                SqlCon(con, "Server =.; database = Northwind; Trusted_connection = True");
                SqlCom(command, "select * from shippers wehere ShipperID=@ID");
                command.Parameters.AddWithValue("@ID", Math.Floor(numerik.Value));
                ConnectionControl(con);
                SqlReader(command, reader);
                if (reader.HasRows)
                {
                    command.CommandText = "Update shippers set ('@CompanyName','@Phone')";
                    command.Parameters.AddWithValue("@CompanyName", yaziBox1.Text);
                    command.Parameters.AddWithValue("@Phone",yaziBox2.Text);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Rows affected {0}", command.ExecuteNonQuery().ToString());
                    MessageBox.Show("Istenilen oge silinmiştir.");
                }
                else
                {
                    MessageBox.Show("Verilen  numaraya ait veri bulunamdi");
                }
            }
            finally
            {
                reader.Close();
                SqlConnectionClose(con);

            }
        }
        public void ConnectionControl(SqlConnection con)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
                MessageBox.Show("baglanti kapaliydi fakat acildi");
            }
            else if (con.State == ConnectionState.Open)
            {
                MessageBox.Show("baglanti zaten acik");
            }
            else
            {
                con.Close();
                MessageBox.Show("baglantida yasanan bir karisiklilk sebebiyle isteginiz yapilamamsitir.Lutfen tekrar deneyiniz");

            }
        }
        public void DeleteShipper(SqlCommand command,NumericUpDown numerik)
        {
            try
            {
                SqlCon(con, "Server =.; database = Northwind; Trusted_connection = True;");
                SqlCom(command, "select * from shippers wehere ShipperID=@ID");
                command.Parameters.AddWithValue("@ID", Math.Floor(numerik.Value));
                ConnectionControl(con);
                SqlReader(command, reader);
                if (reader.HasRows)
                {
                    command.CommandText = "delete from shippers where ShipperID=@ID";
                    command.Parameters.AddWithValue("@ID", Math.Floor(numerik.Value));
                    command.ExecuteNonQuery();
                    MessageBox.Show("Rows affected {0}", command.ExecuteNonQuery().ToString());
                    MessageBox.Show("Istenilen oge silinmiştir.");
                }
                else
                {
                    MessageBox.Show("Verilen  numaraya ait veri bulunamdi");
                }
            }
            finally
            {
                reader.Close();
                SqlConnectionClose(con);
            }
            
        }
        public void ListThem(SqlCommand command,SqlDataReader reader)
        {
            
        }
        void SqlInsert(SqlCommand command, SqlDataReader reader)
        {
            try
            {
                SqlCon(con, "Server=.; Database=Northwind; Trusted_Connection=True;");
                SqlCom(command, "Select * From Shippers where CompanyName = @CompanyName");
                command.Parameters.AddWithValue("@CompanyName", yaziBox1.Text);
                ConnectionControl(con);
                SqlDataReader rdr = command.ExecuteReader();
                if (reader.HasRows)
                {
                    MessageBox.Show("Bu isme ait bir şirket bulunmaktadır. Ekleme işlemi başarısız");

                }
                else
                {
                    SqlCom(command, "Insert into Shippers values ('@CompanyName','@Phone')");
                    command.Parameters.AddWithValue("@CompanyName", yaziBox1.Text);
                    command.Parameters.AddWithValue("@Phone", yaziBox2.Text);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Rows affected" + command.ExecuteNonQuery());
                }
            }
            finally
            {

                reader.Close();
                con.Close();
            }
            
        }

        void SqlCom(SqlCommand command,String commandString)
        {
            command.CommandText = commandString;
        }
        void SqlCon(SqlConnection connection,string connectionString)
        {
            connection.ConnectionString = connectionString;
        }
        void SqlReader(SqlCommand command, SqlDataReader reader)
        {
            reader = command.ExecuteReader();
        }
        void SqlConnectionClose(SqlConnection connection)
        {
            connection.Close();
        }

    }
}
