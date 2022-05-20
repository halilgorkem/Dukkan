using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace Dukkan
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        NpgsqlConnection connect = new NpgsqlConnection("server=localHost; port=5432; Database=hrkDatabase; user Id=postgres; password=Hh9608494..");
        DataTable table = new DataTable();
        private void button1_Click(object sender, EventArgs e)
        {
            string sql = "SELECT op_code, datee, commentt, incomee, exponsee FROM day_report WHERE datee BETWEEN @tarih1 and @tarih2";
            NpgsqlDataAdapter data = new NpgsqlDataAdapter(sql, connect);
            data.SelectCommand.Parameters.AddWithValue("@tarih1", dateTimePicker1.Value);
            data.SelectCommand.Parameters.AddWithValue("@tarih2", dateTimePicker2.Value);
            connect.Open();
            data.Fill(table);
            dataGridView1.DataSource = data;
            connect.Close();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            connect.Open();
            NpgsqlDataAdapter getir = new NpgsqlDataAdapter("Select * From day_report", connect);
            getir.Fill(table);
            dataGridView1.DataSource = table;
            connect.Close();
        }
    }
}
