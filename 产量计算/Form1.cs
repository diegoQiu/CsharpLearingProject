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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            mineInput.exPro = double.Parse(textBox1.Text);
            mineInput.exDis = double.Parse(textBox2.Text);
            mineInput.exDre = double.Parse(textBox3.Text);
            mineInput.exDre /= 100;
            mineInput.exAvi = double.Parse(textBox4.Text);
            mineInput.exAvi /= 100;
            string value = this.comboBox1.SelectedItem.ToString();
            switch (value)
            {
                case "泥土碎石混合物": material.fillFactor = 1.1; material.density = 1895; break;
                case "铁矿": material.fillFactor = 1.00; material.density = 1.00; break;
                case "煤": material.fillFactor = 1.00; material.density = 1.00; break;
            }
            Form2 f2 = new Form2();
            f2.Show();
            this.Hide();
        }

    }


    public class mineCar190
    {
        //int[] numbers = new int[3]{1,2,3};//定长 
        static double[] loader = new double[5] { 16.5, 18, 22, 26, 34 };//五个铲的斗容
        public static double carWedth = 6.95;
        public static double Volume = 108;
        public static double EVW = 130;
        public static double GVW = 190;
        public static double RollR = 1.633;
        public static double Power = 590;
        public static double RedRatio = 50.1;
        public static double MaEff = 0.94;
        public static double numOfLoader;
        public static double loadWeight = getLoad();//难点
        public static double haulWeight = 130 + loadWeight;
        public static double production = getPro();
        public static double numCar;
        public static double numLoader;
        public static double priceOfCar;
        public static double priceOfLoader;
        public static double totalAmount = numCar * priceOfCar + numLoader * priceOfLoader;


        //计算铲数和实际装载质量
        static double getLoad()
        {
            double maxLoad = 0;//动态更新最大装载容积
            for (int i = 0; i <= 4; i++)
            {
                for (int j = 1; j * material.fillFactor * loader[i] <= 108; j++)
                {
                    if (j * material.fillFactor * loader[i] > maxLoad)
                    {
                        maxLoad = j * material.fillFactor * loader[i];
                        mineCar190.numOfLoader = j;
                    }
                }
            }
            return maxLoad * material.density / 1000;
        }

        //计算单车年产量
        static double getPro()
        {
            double loadtime = numOfLoader * 39 / 60;
            double effctiveDegree = mineInput.exDre - 0.02;
            double TractionH = effctiveDegree * 9.8 * haulWeight;
            double TorqueW = TractionH * mineCar190.RollR;
            double TorqueE = TorqueW / 2 / mineCar190.RedRatio / mineCar190.MaEff;
            double Espeed = 590 * 9550 / (TorqueE * 1000);
            double Cspeed = 2 * 3.14 * mineCar190.RollR * Espeed / mineCar190.RedRatio / 60 * 3.6;//km每小时
            double haultime = mineInput.exDis / Cspeed * 60;
            double dumptime = 2;
            double reDegree = mineInput.exDre + 0.02;
            double reTration = reDegree * 9.8 * mineCar190.EVW;
            double reTorqueW = reTration * mineCar190.RollR;
            double reTorqueE = reTorqueW / 2 / mineCar190.RedRatio / mineCar190.MaEff;
            double reEspeed = 590 * 9550 / (reTorqueE * 1000);
            double reCspeed = 2 * 3.14 * mineCar190.RollR * reEspeed / mineCar190.RedRatio / 60 * 3.6;
            double retime = mineInput.exDis / reCspeed * 60;
            double cycletime = loadtime + haultime + dumptime + retime;
            double ans = 365 * mineInput.exAvi * 24 * 55 / cycletime * loadWeight;
            return ans;
        }
    }

    public class mineInput
    {
        public static double exPro;
        public static double exDis;
        public static double exDre;
        public static double exAvi;

    }
    public class material
    {
        public static double fillFactor;
        public static double density;
    }
}
