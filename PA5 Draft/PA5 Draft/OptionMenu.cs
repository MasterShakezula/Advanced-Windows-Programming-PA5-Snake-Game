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
    public partial class OptionMenu : Form
    {
        private int appleNumber;
        
        public OptionMenu()
        {
            InitializeComponent();
            appleNumber = (int)numericUpDown1.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit(); //cancel
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //new MainForm();
            this.Close(); //OK

        }
        public int getApples()
        {
            return appleNumber; //getter method for ease of understanding
        }

        private void OptionMenu_Load(object sender, EventArgs e)
        {
            
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            appleNumber = (int)numericUpDown1.Value;
        }
    }
}
