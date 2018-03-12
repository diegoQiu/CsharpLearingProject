using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _15子游戏
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        const int N = 4;
        Button[,] buttons = new Button[N, N];
        private void Form1_Load(object sender, EventArgs e)
        {
            //产生所有按钮
            GenerateAllButtons();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //打乱顺序
            Shuffle();
        }

        void Shuffle()
        {
            Random rnd = new Random();
            for (int i = 0; i < 100; i++)
            {
                int a = rnd.Next(N);
                int b = rnd.Next(N);
                int c = rnd.Next(N);
                int d = rnd.Next(N);
                Swap(buttons[a, b], buttons[c, d]);
            }
        }

        void Swap(Button btna, Button btnb)
        {
            //交换文本信息
            string str = btna.Text;
            btna.Text = btnb.Text;
            btnb.Text = str;

            //交换可见性
            bool v = btna.Visible;
            btna.Visible = btnb.Visible;
            btnb.Visible = v;
        }

        void GenerateAllButtons()
        {
            int x0 = 100, y0 = 10, w = 45, d = 50;
            for (int r = 0; r < N; r++)
            {
                for (int c = 0; c < N; c++)
                {
                    int num = r * N + c;
                    Button btn = new Button();
                    btn.Text = (num + 1).ToString();
                    btn.Top = y0 + r * d;
                    btn.Left = x0 + c * d;
                    btn.Width = w;
                    btn.Height = w;
                    btn.Visible = true;
                    btn.Tag = r * N + c;//这个数据用来表示他行列位置

                    //注册事件
                    btn.Click += new EventHandler(btn_Click);
                    buttons[r, c] = btn;//放在数组中
                    this.Controls.Add(btn);//加到界面上
                }
            }
          
            buttons[N - 1, N - 1].Visible = false;//最后一个不显示
        }

        private void btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;//当前点中的按钮
            Button blank = findHiddenButton();//空白按钮

            if (IsNeighbor(btn, blank))
            {
                Swap(btn, blank);
                blank.Focus();
            }

            if (ResultIsOk())
            {
                MessageBox.Show("ok");
            }
            int a = (int)btn.Tag;
            
        }

        Button findHiddenButton()
        {
            for (int r = 0; r < N; r++)
            {
                for (int c = 0; c < N; c++)
                {
                    if (!buttons[r, c].Visible)
                        return buttons[r, c];
                }
            }
            return null;
        }

        bool IsNeighbor(Button btnA, Button btnB)
        {
            int a = (int)btnA.Tag;
            int b = (int)btnB.Tag;
            int r1 = a / N, c1 = a % N;
            int r2 = b / N, c2 = b % N;

            if (r1 == r2 && (c1 == c2 - 1 || c1 == c2 + 1) || c1 == c2 && (r1 == r2 - 1 || r1 == r2 + 1))
                return true;
            else return false;
        }

        bool ResultIsOk()
        {
            for (int r = 0; r < N; r++)
            {
                for (int c = 0; c < N; c++)
                {
                    if (buttons[r, c].Text != (r * N + c + 1).ToString())
                        return false;
                }
            }
            return true;
        }
    }
}
