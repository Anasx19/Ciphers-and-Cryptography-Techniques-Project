﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace computer_security_project
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form newform = new Form1();
            newform.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form newform = new Form2();
            newform.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form newform = new Form3();
            newform.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form newform = new Form4();
            newform.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form newform = new Form6();
            newform.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form newform = new Form7();
            newform.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form newform = new Form8();
            newform.Show();
        }
    }
}
