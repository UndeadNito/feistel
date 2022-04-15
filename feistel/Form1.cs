using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace feistel
{
    public partial class Form1 : Form
    {
        Feistel feistelInstance;
        public Form1()
        {
            InitializeComponent();

            feistelInstance = new Feistel(4);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox3.Text = feistelInstance.Encode(textBox1.Text, textBox2.Text);
        }
    }
}
