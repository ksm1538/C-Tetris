using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public partial class Form3 : Form
    {
        
        public Form3()
        {
            
            
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            Rank rank = new Rank()
            {
                score = Form1.NScore,
                name = textBox1.Text

            };
            DataManager.Ranks.Add(rank);
            DataManager.Save();
            new Form2().ShowDialog();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
