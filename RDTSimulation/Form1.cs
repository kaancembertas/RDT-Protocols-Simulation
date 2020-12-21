using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RDTSimulation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormRdt10 form = new FormRdt10();
            form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           FormRdt20 form = new FormRdt20();
            form.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
           FormRdt21 form = new FormRdt21();
            form.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormRdt22 form = new FormRdt22();
            form.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FormRdt30GoBN form = new FormRdt30GoBN();
            form.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<Point> unreliableChannelNodes = Helper.unreliableChannelNodes;
            unreliableChannelNodes.Add(new Point(250, 443));
            unreliableChannelNodes.Add(new Point(250, 600));
            unreliableChannelNodes.Add(new Point(764, 600));
            unreliableChannelNodes.Add(new Point(764, 443));
        }
    }
}
