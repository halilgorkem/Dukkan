using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dukkan
{
    public partial class Form0 : Form
    {
        public Form0()
        {
            InitializeComponent();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            AcceptButton = button8;
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Başarıyla giriş yapıldı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Form1 form1 = new Form1();
                form1.Show();
                this.Hide();
            }
            else
                MessageBox.Show("Lütfen doğru kullanıcı adı ve şifre giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            AcceptButton = button8;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            AcceptButton = button8;
        }
    }
}
