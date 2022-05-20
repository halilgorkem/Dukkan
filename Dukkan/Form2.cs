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
    public partial class Form2 : Form
    {
        NpgsqlConnection connect = new NpgsqlConnection("server=localHost; port=5432; Database=hrkDatabase; user Id=postgres; password=Hh9608494..");
        DataTable table = new DataTable();
        public Form2()
        {
            InitializeComponent();
        }

        public void tum_kayitlar()
        {
            connect.Open();
            NpgsqlDataAdapter getir = new NpgsqlDataAdapter("SELECT * FROM current_card", connect);
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
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
            textBox13.Text = "";
            textBox14.Text = "";
            textBox15.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // kayıt ekleme
            if (textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox11.Text == "" || textBox15.Text == "" || textBox9.Text == "")
            {
                MessageBox.Show("Tüm alanları doldurunuz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            connect.Open();
            NpgsqlCommand add = new NpgsqlCommand("insert into current_card (current_code, commercial_title, current_name, current_surname, current_address, country, city, district, birth_date, tc, email, ctel_number, tel_number_1, tel_number_2, fax, tax_number) values(@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12, @p13, @p14, @p15, @p16)", connect);
            add.Parameters.AddWithValue("@p1", double.Parse(textBox1.Text));
            add.Parameters.AddWithValue("@p2", textBox2.Text);
            add.Parameters.AddWithValue("@p3", textBox3.Text);
            add.Parameters.AddWithValue("@p4", textBox4.Text);
            add.Parameters.AddWithValue("@p5", textBox5.Text);
            add.Parameters.AddWithValue("@p6", textBox6.Text);
            add.Parameters.AddWithValue("@p7", textBox7.Text);
            add.Parameters.AddWithValue("@p8", textBox8.Text);
            add.Parameters.AddWithValue("@p9", dateTimePicker1.Value);
            add.Parameters.AddWithValue("@p10", textBox9.Text);
            add.Parameters.AddWithValue("@p11", textBox10.Text);
            add.Parameters.AddWithValue("@p12", textBox11.Text);
            add.Parameters.AddWithValue("@p13", textBox12.Text);
            add.Parameters.AddWithValue("@p14", textBox13.Text);
            add.Parameters.AddWithValue("@p15", textBox14.Text);
            add.Parameters.AddWithValue("@p16", textBox15.Text);
            add.ExecuteNonQuery();
            connect.Close();
            table.Clear();
            temizle();
            MessageBox.Show("Kayıt İşlemi Başarılı");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //kayıtları listele
            table.Clear();
            tum_kayitlar();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //kayıt silme
            if (textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox11.Text == "" || textBox15.Text == "" || textBox9.Text == "")
            {
                MessageBox.Show("Alanlar zaten boş.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            connect.Open();
            NpgsqlCommand delete = new NpgsqlCommand("DELETE FROM current_card WHERE current_code='" + dataGridView1.CurrentRow.Cells["current_code"].Value.ToString() +
                "' and commercial_title='" + dataGridView1.CurrentRow.Cells["commercial_title"].Value.ToString() +
                "' and current_name='" + dataGridView1.CurrentRow.Cells["current_name"].Value.ToString() +
                "' and current_surname='" + dataGridView1.CurrentRow.Cells["current_surname"].Value.ToString() +
                "' and current_address='" + dataGridView1.CurrentRow.Cells["current_address"].Value.ToString() +
                "' and country='" + dataGridView1.CurrentRow.Cells["country"].Value.ToString() +
                "' and city='" + dataGridView1.CurrentRow.Cells["city"].Value.ToString() +
                "' and district='" + dataGridView1.CurrentRow.Cells["district"].Value.ToString() +
                "' and birth_date='" + dataGridView1.CurrentRow.Cells["birth_date"].Value.ToString() +
                "' and tc='" + dataGridView1.CurrentRow.Cells["tc"].Value.ToString() +
                "' and email='" + dataGridView1.CurrentRow.Cells["email"].Value.ToString() +
                "' and ctel_number='" + dataGridView1.CurrentRow.Cells["ctel_number"].Value.ToString() +
                "' and tel_number_1='" + dataGridView1.CurrentRow.Cells["tel_number_1"].Value.ToString() +
                "' and tel_number_2='" + dataGridView1.CurrentRow.Cells["tel_number_2"].Value.ToString() +
                "' and fax='" + dataGridView1.CurrentRow.Cells["fax"].Value.ToString() +
                "' and tax_number='" + dataGridView1.CurrentRow.Cells["tax_number"].Value.ToString() +
                "'  ", connect);
            delete.ExecuteNonQuery();
            connect.Close();
            table.Clear();
            tum_kayitlar();
            temizle();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells["current_code"].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells["commercial_title"].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells["current_name"].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells["current_surname"].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells["current_address"].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells["country"].Value.ToString();
            textBox7.Text = dataGridView1.CurrentRow.Cells["city"].Value.ToString();
            textBox8.Text = dataGridView1.CurrentRow.Cells["district"].Value.ToString();
            dateTimePicker1.Text = dataGridView1.CurrentRow.Cells["birth_date"].Value.ToString();
            textBox9.Text = dataGridView1.CurrentRow.Cells["tc"].Value.ToString();
            textBox10.Text = dataGridView1.CurrentRow.Cells["email"].Value.ToString();
            textBox11.Text = dataGridView1.CurrentRow.Cells["ctel_number"].Value.ToString();
            textBox12.Text = dataGridView1.CurrentRow.Cells["tel_number_1"].Value.ToString();
            textBox13.Text = dataGridView1.CurrentRow.Cells["tel_number_2"].Value.ToString();
            textBox14.Text = dataGridView1.CurrentRow.Cells["fax"].Value.ToString();
            textBox15.Text = dataGridView1.CurrentRow.Cells["tax_number"].Value.ToString();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //güncelle
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" || textBox7.Text == "")
            {
                MessageBox.Show("Aradığınız ürün bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            connect.Open();
            NpgsqlCommand upgrade = new NpgsqlCommand("UPDATE current_card SET current_code=@p1, commercial_title=@p2, current_name=@p3, current_surname=@p4, current_address=@p5, country=@p6, city=@p7, district=@p8, birth_date=@p9, tc=@p10, email=@p11, ctel_number=@p12, tel_number_1=@p13, tel_number_2=@p14, fax=@p15, tax_number=@p16 WHERE current_code = @p1", connect);
            upgrade.Parameters.AddWithValue("@p1", double.Parse(textBox1.Text));
            upgrade.Parameters.AddWithValue("@p2", textBox2.Text);
            upgrade.Parameters.AddWithValue("@p3", textBox3.Text);
            upgrade.Parameters.AddWithValue("@p4", textBox4.Text);
            upgrade.Parameters.AddWithValue("@p5", textBox5.Text);
            upgrade.Parameters.AddWithValue("@p6", textBox6.Text);
            upgrade.Parameters.AddWithValue("@p7", textBox7.Text);
            upgrade.Parameters.AddWithValue("@p8", textBox8.Text);
            upgrade.Parameters.AddWithValue("@p9", dateTimePicker1.Value);
            upgrade.Parameters.AddWithValue("@p10", textBox9.Text);
            upgrade.Parameters.AddWithValue("@p11", textBox10.Text);
            upgrade.Parameters.AddWithValue("@p12", textBox11.Text);
            upgrade.Parameters.AddWithValue("@p13", textBox12.Text);
            upgrade.Parameters.AddWithValue("@p14", textBox13.Text);
            upgrade.Parameters.AddWithValue("@p15", textBox14.Text);
            upgrade.Parameters.AddWithValue("@p16", textBox15.Text);
            upgrade.ExecuteNonQuery();
            connect.Close();
            table.Clear();
            tum_kayitlar();
            temizle();
            MessageBox.Show("Bilgiler Güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {
            //aratılacak kelime
            connect.Open();
            NpgsqlDataAdapter bring = new NpgsqlDataAdapter("SELECT * FROM current_card WHERE current_name like '" + textBox8.Text + "%' or tc like '" + textBox16.Text + "%' ", connect);
            DataTable table2 = new DataTable();
            bring.Fill(table2);
            dataGridView1.DataSource = table2;
            connect.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result1 = MessageBox.Show("Çıkmak istediğinizden emin misiniz ?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result1 == DialogResult.Yes)
            {
                this.Close();
            }
            else
            {
                
            }
        }

        private void textBox16_Click(object sender, EventArgs e)
        {
            textBox16.Text = "";
        }
    }
}
