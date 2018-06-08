using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 矿车选型
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        void showAnswer()
        {
            string str170_1 = "", str170_2 = "", str170_3 = "", str170_4 = "";
            string str190_1 = "", str190_2 = "", str190_3 = "", str190_4 = "";
            string str240_1 = "", str240_2 = "", str240_3 = "", str240_4 = "";
            string strAcommed = "";
            str170_1 += "一：170吨的车应需要:";
            str170_1 += mineCar170.numOfCar.ToString();
            str170_1 += "辆，并配";
            str170_2 += "斗容:";
            str170_2 += mineCar170.volumeOfLoader.ToString();
            str170_2 += "立方米的铲:";
            str170_2 += mineCar170.numOfShovel.ToString();
            str170_2 += "个,能带来年产量:";
            str170_3 += (mineCar170.numOfCar * mineCar170.production/10000).ToString("f2");
            str170_3 += "万吨,总金额为:";
            str170_3 += mineCar170.totalAmount.ToString("f2");
            str170_3 += "亿元";
            str170_4 += "推荐最小路宽值:";
            str170_4 += (3.5 * mineCar170.carWedth).ToString("f2");
            str170_4 += "米";
            str190_1 += "二：190吨的车应需要:";
            str190_1 +=mineCar190.numOfCar.ToString();
            str190_1 += "辆，并配";
            str190_2 += "斗容:";
            str190_2+= mineCar190.volumeOfLoader.ToString();
            str190_2 += "立方米的铲:";
            str190_2+= mineCar190.numOfShovel.ToString();
            str190_2 += "个,能带来年产量:";
            str190_3 +=( mineCar190.numOfCar * mineCar190.production/10000).ToString("f2");
            str190_3 += "万吨,总金额为:";
            str190_3 += mineCar190.totalAmount.ToString("f2");
            str190_3 += "亿元";
            str190_4 += "推荐最小路宽值:";
            str190_4 += (3.5 * mineCar190.carWedth).ToString("f2");
            str190_4 += "米";
            str240_1 += "三：240吨的车应需要:";
            str240_1 += mineCar240.numOfCar.ToString();
            str240_1 += "辆，并配";
            str240_2 += "斗容:";
            str240_2 += mineCar240.volumeOfLoader.ToString();
            str240_2 += "立方米的铲:";
            str240_2 += mineCar240.numOfShovel.ToString();
            str240_2 += "个,能带来年产量:";
            str240_3 += (mineCar240.numOfCar * mineCar240.production/10000).ToString("f2");
            str240_3 += "万吨,总金额为:";
            str240_3 += mineCar240.totalAmount.ToString("f2");
            str240_3 += "亿元";
            str240_4 += "推荐最小路宽值:";
            str240_4 += (3.5 * mineCar240.carWedth).ToString("f2");
            str240_4 += "米";
            listBox1.Items.Add(str170_1);
            listBox1.Items.Add(str170_2);
            listBox1.Items.Add(str170_3);
            listBox1.Items.Add(str170_4);
            listBox2.Items.Add(str190_1);
            listBox2.Items.Add(str190_2);
            listBox2.Items.Add(str190_3);
            listBox2.Items.Add(str190_4);
            listBox3.Items.Add(str240_1);
            listBox3.Items.Add(str240_2);
            listBox3.Items.Add(str240_3);
            listBox3.Items.Add(str240_4);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            showAnswer();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
