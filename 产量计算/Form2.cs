using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 产量计算
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            showAnswer();
        }
        void showAnswer()
        {
           string str= "";
            str += "190吨车单车年产量为：";
            string ans = mineCar190.production.ToString();
            str += ans;
            str += " 吨";
            label1.Text = str;
        }

    }
}
