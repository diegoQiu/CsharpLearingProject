using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int a, b;
        string op;
        int result;
        Random rnd = new Random();
        private void btnNew_Click(object sender, EventArgs e)
        {
            a = rnd.Next(9) + 1;
            b = rnd.Next(9) + 1;
            int c = rnd.Next(4);
            switch (c)
            {
                case 0: op = "+"; result = a + b; break;
                case 1: op = "-"; result = a - b; break;
                case 2: op = "*"; result = a * b; break;
                case 3: op = "/"; result = a / b; break;
            }
            lblA.Text = a.ToString();
            lblB.Text = b.ToString();
            lblOp.Text = op;
            txtAnswer.Text = "";
        }

        private void btnJudge_Click(object sender, EventArgs e)
        {
            string str = txtAnswer.Text;
            double d = double.Parse(str);
            string disp = "" + a + op + b + "=" + str + " ";
            if(d==result)
                disp+= "☆";
            else
                disp+= "╳";
            lstDisp.Items.Add(disp);
        }
    }
}
