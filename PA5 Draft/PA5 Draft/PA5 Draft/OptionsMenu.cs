using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PA5_Draft
{
    public partial class OptionsMenu : Form
    {
        private int appleNumber;
        public OptionsMenu()
        {
            InitializeComponent();
            appleNumber = (int)numericUpDown1.Value;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit(); // cancel
        }

        public int getApples()
        {
            return appleNumber;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            appleNumber = (int)numericUpDown1.Value;
        }
    }
}
