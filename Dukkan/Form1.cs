using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using ZXing;

namespace Dukkan
{
    public partial class Form1 : Form
    {
        PrintDocument pDoc = new PrintDocument();
        NpgsqlConnection connect = new NpgsqlConnection("server=localHost; port=5432; Database=hrkDatabase; user Id=postgres; password=Hh9608494..");
        DataTable table = new DataTable();
        public Form1()
        {
            InitializeComponent();
        }
        public void tum_kayitlar()
        {
            connect.Open();
            NpgsqlDataAdapter getir = new NpgsqlDataAdapter("Select * From products", connect);
            getir.Fill(table);
            dataGridView1.DataSource = table;
            connect.Close();
        }
        public void temizle()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox8.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //kayıt ekleme
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" || textBox7.Text == "" || comboBox1.Text == "")
            {
                MessageBox.Show("Tüm alanları doldurunuz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            connect.Open();
            NpgsqlCommand add = new NpgsqlCommand("insert into products (stock_name, brand, model, barcode_type, barcode, seller, purchase_price, sell_price, reg_date, stock_quantity) values(@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10)", connect);
            add.Parameters.AddWithValue("@p1", textBox1.Text);
            add.Parameters.AddWithValue("@p2", textBox5.Text);
            add.Parameters.AddWithValue("@p3", textBox2.Text);
            add.Parameters.AddWithValue("@p4", comboBox1.Text);
            add.Parameters.AddWithValue("@p5", textBox4.Text);
            add.Parameters.AddWithValue("@p6", textBox3.Text);
            add.Parameters.AddWithValue("@p7", double.Parse(textBox6.Text));
            add.Parameters.AddWithValue("@p8", double.Parse(textBox7.Text));
            add.Parameters.AddWithValue("@p9", dateTimePicker1.Value);
            add.Parameters.AddWithValue("@p10", int.Parse(numericUpDown1.Value.ToString()));
            add.ExecuteNonQuery();
            connect.Close();
            table.Clear();
            tum_kayitlar();
            temizle();
            MessageBox.Show("Kayıt İşlemi Başarılı");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //kayıtları listele
            string query = "SELECT * FROM products";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, connect);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //kayıt silme
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" || textBox7.Text == "" || comboBox1.Text == "")
            {
                MessageBox.Show("Alanlar zaten boş.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            connect.Open();
            NpgsqlCommand delete = new NpgsqlCommand("DELETE FROM products WHERE stock_id='" + dataGridView1.CurrentRow.Cells["stock_id"].Value.ToString() +
                "' and stock_name='" + dataGridView1.CurrentRow.Cells["stock_name"].Value.ToString() +
                "' and brand='" + dataGridView1.CurrentRow.Cells["brand"].Value.ToString() +
                "' and model='" + dataGridView1.CurrentRow.Cells["model"].Value.ToString() +
                //"' and barcode_type='" + dataGridView1.CurrentRow.Cells["barcode_type"].Value.ToString() +
                "' and barcode='" + dataGridView1.CurrentRow.Cells["barcode"].Value.ToString() +
                "' and seller='" + dataGridView1.CurrentRow.Cells["seller"].Value.ToString() +
                "' and purchase_price='" + dataGridView1.CurrentRow.Cells["purchase_price"].Value.ToString() +
                //"' and sell_price='" + dataGridView1.CurrentRow.Cells["sell_price"].Value.ToString() +
                //"' and reg_date='" + dataGridView1.CurrentRow.Cells["reg_date"].Value.ToString() +
                //"' and stock_quantity='" + dataGridView1.CurrentRow.Cells["stock_quantity"].Value.ToString() +
                "'  ", connect);
            delete.ExecuteNonQuery();
            connect.Close();
            table.Clear();
            tum_kayitlar();
            temizle();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells["stock_name"].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells["model"].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells["seller"].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells["barcode"].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells["brand"].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells["purchase_price"].Value.ToString();
            textBox7.Text = dataGridView1.CurrentRow.Cells["sell_price"].Value.ToString();
            dateTimePicker1.Text = dataGridView1.CurrentRow.Cells["reg_date"].Value.ToString();
            numericUpDown1.Text = dataGridView1.CurrentRow.Cells["stock_quantity"].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells["barcode_type"].Value.ToString();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //güncelle
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" || textBox7.Text == "")
            {
                MessageBox.Show("Aradığınız ürün bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            connect.Open();
            NpgsqlCommand upgrade = new NpgsqlCommand("UPDATE products SET stock_name=@p1, brand=@p2, model=@p3, barcode_type=@p4, barcode=@p5, seller=@p6, purchase_price=@p7, sell_price=@p8, reg_date=@p9, stock_quantity=@p10 WHERE stock_name=@p1", connect);
            upgrade.Parameters.AddWithValue("@p1", textBox1.Text);
            upgrade.Parameters.AddWithValue("@p2", textBox5.Text);
            upgrade.Parameters.AddWithValue("@p3", textBox2.Text);
            upgrade.Parameters.AddWithValue("@p4", comboBox1.Text);
            upgrade.Parameters.AddWithValue("@p5", textBox4.Text);
            upgrade.Parameters.AddWithValue("@p6", textBox3.Text);
            upgrade.Parameters.AddWithValue("@p7", double.Parse(textBox6.Text));
            upgrade.Parameters.AddWithValue("@p8", double.Parse(textBox7.Text));
            upgrade.Parameters.AddWithValue("@p9", dateTimePicker1.Value);
            upgrade.Parameters.AddWithValue("@p10", int.Parse(numericUpDown1.Value.ToString()));
            upgrade.ExecuteNonQuery();
            connect.Close();
            table.Clear();
            tum_kayitlar();
            temizle();
            MessageBox.Show("Bilgiler Güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            //aratılacak kelime
            connect.Open();
            NpgsqlDataAdapter bring = new NpgsqlDataAdapter("SELECT * FROM products WHERE stock_name like '" + textBox8.Text + "%' or brand like '" + textBox8.Text + "%' ", connect);
            DataTable table2 = new DataTable();
            bring.Fill(table2);
            dataGridView1.DataSource = table2;
            connect.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //ara
            connect.Open();
            NpgsqlDataAdapter bring = new NpgsqlDataAdapter("SELECT * FROM products WHERE stock_name like '" + textBox8.Text + "%' or brand like '" + textBox8.Text + "%' ", connect);
            DataTable table2 = new DataTable();
            bring.Fill(table2);
            dataGridView1.DataSource = table2;
            connect.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //string selectedcombobox1 = comboBox1.SelectedItem.ToString();
            //if( selectedcombobox1 == "EAN-13")
            //{
            //    ZXing.BarcodeFormat.EAN_13
            //}
            
            //pDoc.PrintPage += new PrintPageEventHandler(PrintPage);
            //pDoc.Print();
        }
        /*private void PrintPage(object sender, PrintPageEventArgs e)
        {
            Font drawFont = new Font("Arial", 16);

            SolidBrush drawBrush = new SolidBrush(Color.Black);

            Pen blackPen = new Pen(Color.Black);

            StringFormat drawFormat = new StringFormat();

            drawFormat.Alignment = StringAlignment.Center;

            float x = 150.0F;

            float y = 150.0F;

            float width = 200.0F;

            float height = 50.0F;

            RectangleF drawRect = new RectangleF(x, y, width, height);

            e.Graphics.DrawString(textBox4.Text, drawFont, drawBrush, drawRect, drawFormat);
            
        }*/
    }
}
