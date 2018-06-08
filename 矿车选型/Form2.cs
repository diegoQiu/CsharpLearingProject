using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Math.Ceiling()向上取整，Math.Floor()向下取整 

namespace 矿车选型
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
                case "泥土碎石混合物": material.fillFactor = 1.1;material.density = 1895; break;
                case "爆炸碎石": material.fillFactor = 0.875; material.density = 2490; break;
                case "干土":material.fillFactor = 1.05;material.density=1600 ;break;
                case "湿土":material.fillFactor = 1.05;material.density = 1780;break;
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }


    //类，每一个车即为一个类，170吨，190吨，240吨各为一个类
    public class mineCar190
    {

        
        //int[] numbers = new int[3]{1,2,3};//定长 
        static double[,] loader = new double[7,7] { 
            {10, 16.5, 18,0,0,0,0 },{10,16.5,18,22,26,0,0 },
            {  16.5, 18,22,26,34,0,0},{16.5, 18,22,26,34,0,0 },
            { 18,22,26,34,45,51.2,62.7 },{ 26,34,45,51.2,62.7,0,0},{ 26,34,45,51.2,62.7,0,0}
        };//铲的斗容
        public static int tonPick;
        public static double carWedth = 6.95;
        public static double Volume = 108;
        public static double EVW = 130;
        public static int tonnage = 190;
        public static double RollR = 1.633;
        public static double Power = 590;
        public static double RedRatio = 50.1;
        public static double MaEff = 0.94;
        public static double volumeOfLoader;
        public static double loadWeight =getLoad();//难点
        public static double haulWeight = EVW+loadWeight;
        public static double production = getPro();
        public static double cycletime0;
        public static double cycletime;
        public static double numOfCar=Math.Ceiling( mineInput.exPro*10000/production);//矿车数
        public static double numOfShovel = Math.Ceiling(numOfCar * numOfLoader * 39 / ( cycletime * 60));//电铲数
        public static double numOfLoader;//铲数
        public static double priceOfCar=1900;//单位万元
        public static double priceOfLoader=volumeOfLoader*40*6.3;
        public static double totalAmount=(numOfCar*priceOfCar+numOfShovel*priceOfLoader)/10000;


        //计算铲数和实际装载质量
        static double getLoad()
        {
            double maxLoad = 0;//动态更新最大装载容积
            //numOfLoader随着maxLoad更新时记录j的值，即为铲数。
            switch (tonnage)
            {
                case 108: tonPick = 0; break;
                case 109: tonPick = 0; break;
                case 136: tonPick = 1; break;
                case 154: tonPick = 2; break;
                case 172: tonPick = 2; break;
                case 186: tonPick = 3; break;
                case 190: tonPick = 3; break;
                case 217: tonPick = 4; break;
                case 220: tonPick = 4; break;
                case 326: tonPick = 5; break;
                case 363: tonPick = 6; break;
            }
            for (int i = 0; i < 7; i++)
            {
                if (loader[tonPick, i] == 0)
                {
                    continue;
                }
                for (int j = 1; j * material.fillFactor * loader[tonPick,i] <= Volume; j++)
                {
                    if (j * material.fillFactor * loader[tonPick,i] > maxLoad)
                    {
                        maxLoad = j * material.fillFactor * loader[tonPick,i];
                        mineCar190.numOfLoader = j;
                        volumeOfLoader = loader[tonPick,i];
                    }
                }
            }
           return maxLoad * material.density/1000;
        }

        //计算单车年产量
        static double getPro()
        {
            double loadtime = numOfLoader * 39 / 60;//铲数*周期
            double effctiveDegree = mineInput.exDre - 0.02;
            double TractionH = effctiveDegree * 10 * haulWeight;
            double TorqueW = TractionH * mineCar190.RollR;
            double TorqueE = TorqueW / 2 / mineCar190.RedRatio/mineCar190.MaEff;
            double Espeed = 590 * 9550 / (TorqueE * 1000);
            double Cspeed = 2 * 3.14 * mineCar190.RollR * Espeed / mineCar190.RedRatio / 60 ;//米每秒
            double haultime = mineInput.exDis *1000/ Cspeed /60;
            double dumptime = 1;
            double reDegree = mineInput.exDre + 0.02;
            double reTration = reDegree * 9.8 * mineCar190.EVW;
            double reTorqueW = reTration * mineCar190.RollR;
            double reTorqueE = reTorqueW / 2 / mineCar190.RedRatio /mineCar190.MaEff;
            double reEspeed = 590 * 9550 / (reTorqueE * 1000);
            double reCspeed = 2 * 3.14 * mineCar190.RollR * reEspeed / mineCar190.RedRatio / 60 ;
            double retime = mineInput.exDis*1000/ reCspeed/60 ;
            mineCar190.cycletime0= loadtime + haultime + dumptime + retime;
            double matchPoint = Math.Ceiling(cycletime0 / loadtime);
            double waitTime = matchPoint * loadtime - cycletime0;
            cycletime = cycletime0 + waitTime;
            double ans=365*mineInput.exAvi*24*55/cycletime*loadWeight;
            return ans;  
        }
    }

    public class mineCar170
    {
        static double[,] loader = new double[7, 7] {
            {10, 16.5, 18,0,0,0,0 },{10,16.5,18,22,26,0,0 },
            {  16.5, 18,22,26,34,0,0},{16.5, 18,22,26,34,0,0 },
            { 18,22,26,34,0,0,0 },{ 26,34,45,51.2,62.7,0,0},{ 26,34,45,51.2,62.7,0,0}
        };//铲的斗容
        public static int tonPick;
        public static double carWedth = 6.42;
        public static double Volume = 93.5;
        public static double EVW = 110;
        public static int tonnage = 170;
        public static double RollR = 1.536;
        public static double Power = 590;
        public static double RedRatio = 30.36;
        public static double MaEff = 0.948;
        public static double volumeOfLoader;
        public static double loadWeight = getLoad();//难点
        public static double haulWeight = 110 + loadWeight;
        public static double production = getPro();
        public static double cycletime;
        public static double cycletime0;
        public static double numOfCar = Math.Ceiling(mineInput.exPro*10000 / production);//矿车数
        public static double numOfShovel = Math.Ceiling(numOfCar * numOfLoader * 39 / ( cycletime * 60));//电铲数
        public static double numOfLoader;//铲数
        public static double priceOfCar = 1700;//单位万元
        public static double priceOfLoader = volumeOfLoader * 40 * 6.3;
        public static double totalAmount = (numOfCar * priceOfCar + numOfShovel * priceOfLoader)/10000;


        //计算铲数和实际装载质量
        static double getLoad()
        {
            double maxLoad = 0;//动态更新最大装载容积
                               //numOfLoader随着maxLoad更新时记录j的值，即为铲数。
            switch (tonnage)
            {
                case 108: tonPick = 0; break;
                case 109: tonPick = 0; break;
                case 136: tonPick = 1; break;
                case 154: tonPick = 2; break;
                case 170: tonPick = 2; break;
                case 186: tonPick = 3; break;
                case 190: tonPick = 3; break;
                case 217: tonPick = 4; break;
                case 220: tonPick = 4; break;
                case 326: tonPick = 5; break;
                case 363: tonPick = 6; break;
            }
            for (int i = 0; i < 7; i++)
            {
                if (loader[tonPick, i] == 0)
                {
                    continue;
                }
                for (int j = 1; j * material.fillFactor * loader[tonPick,i] <= Volume; j++)
                {
                    if (j * material.fillFactor * loader[tonPick,i] > maxLoad)
                    {
                        maxLoad = j * material.fillFactor * loader[tonPick,i];
                        mineCar170.numOfLoader = j;
                        volumeOfLoader = loader[tonPick,i];
                    }
                }
            }
            return maxLoad * material.density / 1000;
        }

        //计算单车年产量
        static double getPro()
        {
            double loadtime = numOfLoader * 39 / 60;//铲数*周期
            double effctiveDegree = mineInput.exDre - 0.02;
            double TractionH = effctiveDegree * 9.8 * haulWeight;
            double TorqueW = TractionH * mineCar170.RollR;
            double TorqueE = TorqueW / 2 / mineCar170.RedRatio / mineCar170.MaEff;
            double Espeed = 590 * 9550 / (TorqueE * 1000);
            double Cspeed = 2 * 3.14 * mineCar170.RollR * Espeed / mineCar170.RedRatio / 60 * 3.6;//km每小时
            double haultime = mineInput.exDis / Cspeed * 60;
            double dumptime = 1;
            double reDegree = mineInput.exDre + 0.02;
            double reTration = reDegree * 9.8 * mineCar170.EVW;
            double reTorqueW = reTration * mineCar170.RollR;
            double reTorqueE = reTorqueW / 2 / mineCar170.RedRatio / mineCar170.MaEff;
            double reEspeed = 590 * 9550 / (reTorqueE * 1000);
            double reCspeed = 2 * 3.14 * mineCar170.RollR * reEspeed / mineCar170.RedRatio / 60 * 3.6;
            double retime = mineInput.exDis / reCspeed * 60;
            mineCar170.cycletime0 = loadtime + haultime + dumptime + retime;
            double matchPoint = cycletime0 / loadtime;
            double waitTime = matchPoint * loadtime - cycletime0;
            cycletime = cycletime0 + waitTime;
            double ans = 365 * mineInput.exAvi * 24 * 55 / cycletime * loadWeight;
            return ans;
        }
    }

    public class mineCar240
    {
        static double[,] loader = new double[7, 7] {
            {10, 16.5, 18,0,0,0,0 },{10,16.5,18,22,26,0,0 },
            {  16.5, 18,22,26,34,0,0},{16.5, 18,22,26,34,0,0 },
            { 18,22,26,34,0,0,0 },{ 26,34,45,51.2,62.7,0,0},{ 26,34,45,51.2,62.7,0,0}
        };//铲的斗容
        public static int tonPick;
        public static double carWedth = 8;
        public static double Volume = 138;
        public static double EVW = 160;
        public static int tonnage = 240;
        public static double RollR = 1.743;
        public static double Power = 764;
        public static double RedRatio = 40.47;
        public static double MaEff = 0.94;
        public static double volumeOfLoader;
        public static double loadWeight = getLoad();//难点
        public static double haulWeight = 160 + loadWeight;
        public static double production = getPro();
        public static double cycletime;
        public static double cycletime0;
        public static double numOfCar = Math.Ceiling(mineInput.exPro*10000 / production);//矿车数
        public static double numOfShovel = Math.Ceiling(numOfCar * numOfLoader * 39 / ( cycletime * 60));//电铲数
        public static double numOfLoader;//铲数
        public static double priceOfCar = 2400;//单位万元
        public static double priceOfLoader = volumeOfLoader * 40 * 6.3;
        public static double totalAmount = (numOfCar * priceOfCar + numOfShovel * priceOfLoader)/10000;


        //计算铲数和实际装载质量
        static double getLoad()
        {
            double maxLoad = 0;//动态更新最大装载容积
                               //numOfLoader随着maxLoad更新时记录j的值，即为铲数。
            switch (tonnage)
            {
                case 108: tonPick = 0; break;
                case 109: tonPick = 0; break;
                case 136: tonPick = 1; break;
                case 154: tonPick = 2; break;
                case 170: tonPick = 2; break;
                case 186: tonPick = 3; break;
                case 190: tonPick = 3; break;
                case 217: tonPick = 4; break;
                case 220: tonPick = 4; break;
                case 240: tonPick = 4; break;
                case 326: tonPick = 5; break;
                case 363: tonPick = 6; break;
            }
            for (int i = 0; i < 7; i++)
            {
                if (loader[tonPick, i] == 0)
                {
                    continue;
                }
                for (int j = 1; j * material.fillFactor * loader[tonPick,i] <= Volume; j++)
                {
                    if (j * material.fillFactor * loader[tonPick,i] > maxLoad)
                    {
                        maxLoad = j * material.fillFactor * loader[tonPick,i];
                        mineCar240.numOfLoader = j;
                        volumeOfLoader = loader[tonPick,i];
                    }
                }
            }
            return maxLoad * material.density / 1000;
        }

        //计算单车年产量
        static double getPro()
        {
            double loadtime = numOfLoader * 39 / 60;//铲数*周期
            double effctiveDegree = mineInput.exDre - 0.02;
            double TractionH = effctiveDegree * 10 * haulWeight;
            double TorqueW = TractionH * mineCar240.RollR;
            double TorqueE = TorqueW / 2 / mineCar240.RedRatio / mineCar240.MaEff;
            double Espeed = 764 * 9550 / (TorqueE * 1000);
            double Cspeed = 2 * 3.14 * mineCar240.RollR * Espeed / mineCar240.RedRatio / 60 * 3.6;//km每小时
            double haultime = mineInput.exDis / Cspeed * 60;
            double dumptime = 1;
            double reDegree = mineInput.exDre + 0.02;
            double reTration = reDegree * 9.8 * mineCar240.EVW;
            double reTorqueW = reTration * mineCar240.RollR;
            double reTorqueE = reTorqueW / 2 / mineCar240.RedRatio / mineCar240.MaEff;
            double reEspeed = 764 * 9550 / (reTorqueE * 1000);
            double reCspeed = 2 * 3.14 * mineCar240.RollR * reEspeed / mineCar240.RedRatio / 60 * 3.6;
            double retime = mineInput.exDis / reCspeed * 60;
            mineCar240.cycletime0 = loadtime + haultime + dumptime + retime;
            double matchPoint = cycletime0 / loadtime;
            double waitTime = matchPoint * loadtime - cycletime0;
            cycletime = cycletime0 + waitTime;
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
