using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 矿车选型v1._0
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mineInput.exPro = double.Parse(textBox1.Text);
            mineInput.exDis = double.Parse(textBox2.Text);
            mineInput.exDre = double.Parse(textBox3.Text);
            mineInput.exDre /= 100;
            mineInput.exAvi = double.Parse(textBox4.Text);
            mineInput.exAvi /= 100;
            Form3 f3 = new Form3();
            f3.Show();
            this.Hide();
            string value = this.comboBox1.SelectedItem.ToString();

            switch (value)
            {
                case "泥土碎石混合物": material.fillFactor = 1.1; material.density = 1895; break;
                case "干土": material.fillFactor = 1.00; material.density = 1.00; break;
                case  "湿土": material.fillFactor = 1.00; material.density = 1.00; break;
                case "爆炸碎石": material.fillFactor = 1.00; material.density = 1.00; break;
                case "石灰石": material.fillFactor = 1.00; material.density = 1.00; break;
            }
        }
    }

    public class mineCar190
    {
       
        private double EVW;
        private double GVW;
        private double Width;
        private double RollR;
        private double Power;
        private double Volume;
        private double Reduction;
        private double MaEff;


        public  void setParameter(double evw,double gvw,double width,double rollr,double volume,double red,double mae)
        {
            EVW = evw;
            GVW = gvw;
            Width = width;
            RollR = rollr;
            Volume = volume;
            Reduction = red;
            MaEff = mae;
        }


        //int[] numbers = new int[3]{1,2,3};//定长 
        double[] loader = new double[5] { 16.5, 18, 22, 26, 34 };//五个铲的斗容
       
        
       
        public  double volumeOfLoader;
        public  double loadWeight;//难点
       
        public  double haulWeight = 130 + loadWeight;
        public  double production = getPro();
        public double cycletime;
        public  double numOfCar = Math.Ceiling(mineInput.exPro / loadWeight);//矿车数
        public  double numOfShovel = Math.Ceiling(numOfCar * numOfLoader * 39 / (numOfLoader * 39 + cycletime * 60));//电铲数
        public  double numOfLoader;//铲数
        public  double priceOfCar = 1900;//单位万元
        public  double priceOfLoader = volumeOfLoader * 40 * 6.3;
        public  double totalAmount = numOfCar * priceOfCar + numOfShovel * priceOfLoader;


        //计算铲数和实际装载质量
        public double getLoad()
        {
            double maxLoad = 0;//动态更新最大装载容积
            //numOfLoader随着maxLoad更新时记录j的值，即为铲数。
            for (int i = 0; i < 5; i++)
            {
                for (int j = 1; j * material.fillFactor * loader[i] <= Volume; j++)
                {
                    if (j * material.fillFactor * loader[i] > maxLoad)
                    {
                        maxLoad = j * material.fillFactor * loader[i];
                        numOfLoader = j;
                        volumeOfLoader = loader[i];
                    }
                }
                
            }
            return maxLoad * material.density / 1000;
        }

        //计算单车年产量
        public double getPro()
        {
            double loadtime = numOfLoader * 39 / 60;//铲数*周期
            double effctiveDegree = mineInput.exDre - 0.02;
            double TractionH = effctiveDegree * 9.8 * haulWeight;
            double TorqueW = TractionH * RollR;
            double TorqueE = TorqueW / 2 / Reduction / MaEff;
            double Espeed = 590 * 9550 / (TorqueE * 1000);
            double Cspeed = 2 * 3.14 * RollR * Espeed / Reduction / 60 * 3.6;//km每小时
            double haultime = mineInput.exDis / Cspeed * 60;
            double dumptime = 2;
            double reDegree = mineInput.exDre + 0.02;
            double reTration = reDegree * 9.8 * EVW;
            double reTorqueW = reTration * RollR;
            double reTorqueE = reTorqueW / 2 / Reduction / MaEff;
            double reEspeed = 590 * 9550 / (reTorqueE * 1000);
            double reCspeed = 2 * 3.14 * RollR * reEspeed / Reduction/ 60 * 3.6;
            double retime = mineInput.exDis / reCspeed * 60;
            cycletime = loadtime + haultime + dumptime + retime;
            double ans = 365 * mineInput.exAvi * 24 * 55 / cycletime * loadWeight;
            return ans;
        }
    }
    

    struct  mineInput
    {
       public  static double exPro;
       public static double exDis;
       public  static double exDre;
       public static double exAvi;

    }
    struct material
    {
        public static double fillFactor;
        public static double density;
    }
}
