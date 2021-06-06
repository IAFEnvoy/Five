using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Five
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        public static int[,] point = new int[N + 2, N + 2];//存储评分

        double whitet = 0, blackt = 0;
        bool pvp = false, is2 = false, ending = false, banopen = false;
        const int N = 15;
        int[,] p = new int[N + 2, N + 2]; //0空1黑2白  1●2○ -1▲-2△
        int s0 = 0;//s是轮到谁下,s=1,2，s=1是ai下，s=2是玩家，s=s0是黑方下，否则是白方下
        bool is_end = false;
        readonly int[] dx = { 1, 1, 0, -1, -1, -1, 0, 1 }; //flat技术
        readonly int[] dy = { 0, 1, 1, 1, 0, -1, -1, -1 };//（dx,dy）是8个方向向量
        int[,] manu = new int[2, 300];
        int manukey = 0;//棋谱
        /// <summary>
        /// 游戏开局初始化
        /// </summary>
        void init()
        {
            for (int i = 0; i <= N + 1; i++)
                for (int j = 0; j <= N + 1; j++)
                    p[i, j] = 0;//以空格包围棋盘	
            for (int j = 0; j < 300; j++)
                manu[0, j] = manu[1, j] = 0;
            events.Text = "现在轮到你了";
        }
        /// <summary>
        /// 判断(row,col)是否在棋盘内
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        bool Inboard(int row, int col)
        {
            if (row < 1 || row > N) return false;
            return col >= 1 && col <= N;
        }
        /// <summary>
        ///判断2个棋子是否同色
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Same(int row, int col, int key)
        {
            if (!Inboard(row, col)) return false;
            return (p[row, col] == key || p[row, col] + key == 0);
        }
        /// <summary>
        ///某个方向有多少连续同色棋子
        /// </summary>
        /// <param name="row">坐标x</param>
        /// <param name="col">坐标u</param>
        /// <param name="u">方向向量</param>
        /// <returns>返回该方向有多少连续同色棋子</returns>
        int Num(int row, int col, int u)
        {
            int i = row + dx[u], j = col + dy[u], sum = 0, ref1 = p[row, col];
            if (ref1 == 0) return 0;
            while (Same(i, j, ref1)) { sum++; i += dx[u]; j += dy[u]; }
            return sum;
        }
        /// <summary>
        /// 落子成活4的数量
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        int Live4(int row, int col)
        {
            int sum = 0, i, u;
            for (u = 0; u < 4; u++)//4个方向，判断每个方向是否落子就成活4
            {
                int sumk = 1;
                for (i = 1; Same(row + dx[u] * i, col + dy[u] * i, p[row, col]); i++) sumk++;
                if ((!Inboard(row + dx[u] * i, col + dy[u] * i) || p[row + dx[u] * i, col + dy[u] * i] != 0)) continue;
                for (i = -1; Same(row + dx[u] * i, col + dy[u] * i, p[row, col]); i--) sumk++;
                if ((!Inboard(row + dx[u] * i, col + dy[u] * i) || p[row + dx[u] * i, col + dy[u] * i] != 0)) continue;
                if (sumk == 4) sum++;
            }
            return sum;
        }
        /// <summary>
        /// 成5点的数量
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        int Cheng5(int row, int col)
        {
            int sum = 0, i, u;
            for (u = 0; u < 8; u++)//8个成五点的方向
            {
                int sumk = 0;
                bool flag = true;
                for (i = 1; Same(row + dx[u] * i, col + dy[u] * i, p[row, col]) || flag; i++)
                {
                    if (!Same(row + dx[u] * i, col + dy[u] * i, p[row, col]))//该方向的第一个不同色的点，超出边界或者对方棋子或空格
                    {
                        if (p[row + dx[u] * i, col + dy[u] * i] != 0) sumk -= 10;//该方向的第一个不同色的点是对方棋子,没有成五点
                        flag = false;
                    }
                    sumk++;
                }
                if (!Inboard(row + dx[u] * --i, col + dy[u] * i)) continue;//该方向的第一个不同色的点是超出边界,没有成五点
                for (i = -1; Same(row + dx[u] * i, col + dy[u] * i, p[row, col]); i--) sumk++;
                if (sumk == 4) sum++;
            }
            return sum;
        }
        /// <summary>
        /// 冲4的数量
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        int Chong4(int row, int col)
        {
            return Cheng5(row, col) - Live4(row, col) * 2;
        }
        /// <summary>
        /// 落子成活3的数量
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        int Live3(int row, int col)
        {
            int sum = 0, i, u, flag = 2;
            for (u = 0; u < 4; u++)//三连的活三
            {
                int sumk = 1;
                for (i = 1; Same(row + dx[u] * i, col + dy[u] * i, p[row, col]); i++) sumk++;
                if ((!Inboard(row + dx[u] * i, col + dy[u] * i) || p[row + dx[u] * i, col + dy[u] * i] != 0)) continue;
                i++;
                if ((!Inboard(row + dx[u] * i, col + dy[u] * i) || p[row + dx[u] * i, col + dy[u] * i] != 0)) flag--;
                for (i = -1; Same(row + dx[u] * i, col + dy[u] * i, p[row, col]); i--) sumk++;
                if ((!Inboard(row + dx[u] * i, col + dy[u] * i) || p[row + dx[u] * i, col + dy[u] * i] != 0)) continue;
                i--;
                if ((!Inboard(row + dx[u] * i, col + dy[u] * i) || p[row + dx[u] * i, col + dy[u] * i] != 0)) flag--;
                if (sumk == 3 && flag > 0) sum++;
            }
            for (u = 0; u < 8; u++)//8个方向，每个方向最多1个非三连的活三
            {
                int sumk = 0;
                bool flag1 = true;
                for (i = 1; Same(row + dx[u] * i, col + dy[u] * i, p[row, col]) || flag1; i++)//成活四点的方向
                {
                    if (!Same(row + dx[u] * i, col + dy[u] * i, p[row, col]))
                    {
                        if (flag1 && p[row + dx[u] * i, col + dy[u] * i] != 0) sumk -= 10;
                        flag1 = false;
                    }
                    sumk++;
                }
                if ((!Inboard(row + dx[u] * i, col + dy[u] * i) || p[row + dx[u] * i, col + dy[u] * i] != 0)) continue; ;
                if (p[row + dx[u] * --i, col + dy[u] * i] == 0) continue;
                for (i = 1; Same(row + dx[u] * i, col + dy[u] * i, p[row, col]); i++) sumk++;
                if ((!Inboard(row + dx[u] * i, col + dy[u] * i) || p[row + dx[u] * i, col + dy[u] * i] != 0)) continue; ;
                if (sumk == 3) sum++;
            }
            return sum;
        }
        /// <summary>
        /// 长连禁手
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        bool Overline(int row, int col)
        {
            for (int u = 0; u < 4; u++) if (Num(row, col, u) + Num(row, col, u + 4) > 4) return true;
            return false;
        }
        /// <summary>
        /// 判断落子后是否成禁手
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        bool Ban(int row, int col)
        {
            if (Same(row, col, 2)) return false;//白方无禁手
            return Live3(row, col) > 1 || Overline(row, col) || Live4(row, col) + Chong4(row, col) > 1;
        }
        /// <summary>
        /// (row,col)处落子之后是否游戏结束
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        bool End_(int row, int col)
        {
            for (int u = 0; u < 4; u++) if (Num(row, col, u) + Num(row, col, u + 4) >= 4) is_end = true;
            if (is_end) return true;
            if(banopen==true) is_end = Ban(row, col);
            return is_end;
        }
        /// <summary>
        /// 落下一子
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        void Go(int row, int col, bool flag)
        {
            if (flag == true) p[row, col] = 1;
            else p[row, col] = 2;
            DrawPoint(row, col, flag);
            if (Ban(row, col)&&banopen==true)
            {
                ending = true;
                timerb.Enabled = false;
                timerw.Enabled = false;
                if (s0 == 1) { 
                    events.Text = "禁手,玩家胜"; 
                    MessageBox.Show("禁手\n玩家胜"); 
                }
                else { 
                    events.Text = "禁手,AI胜"; 
                    MessageBox.Show("禁手\nAI胜"); 
                }
                return;
            }
            if (End_(row, col))
            {
                ending = true;
                timerb.Enabled = false;
                timerw.Enabled = false;
                if (flag == false) { 
                    events.Text = "AI胜"; 
                    MessageBox.Show("AI胜"); 
                }
                else { 
                    SavePoint.SaveNow(platform); 
                    events.Text = "玩家胜"; 
                    MessageBox.Show("玩家胜");
                }
                return;
            }
            manu[0, manukey] = row; manu[1, manukey++] = col;
        }
        /// <summary>
        /// 能否落子
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        bool Ok(int row, int col)
        {
            return Inboard(row, col) && (p[row, col] == 0);
        }
        /// <summary>
        /// 非负分值
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        int Point(int row, int col)
        {
            if (Ban(row, col)&&banopen==true) return 0;//禁手0分
            if (End_(row, col))
            {
                is_end = false;
                return 10000;
            }
            int ret = Live4(row, col) * 1000 + (Chong4(row, col) + Live3(row, col)) * 100, u;
            for (u = 0; u < 8; u++) if (p[row + dx[u], col + dy[u]] != 0) ret++;//无效点0分
            return ret;
        }
        /// <summary>
        /// AI第三层
        /// </summary>
        /// <param name="p2"></param>
        /// <returns></returns>
        int AI3(int p2)
        {
            int keyp = -100000, tempp;
            for (int i = 1; i <= N; i++) for (int j = 1; j <= N; j++)
                {
                    if (!Ok(i, j)) continue;
                    p[i, j] = s0;
                    tempp = Point(i, j);
                    if (tempp == 0)
                    {
                        p[i, j] = 0;
                        continue;
                    }
                    if (tempp == 10000)
                    {
                        p[i, j] = 0;
                        return 10000;
                    }
                    p[i, j] = 0;
                    if (tempp - p2 * 2 > keyp) keyp = tempp - p2 * 2;//第三层取极大
                }
            return keyp;
        }
        /// <summary>
        /// AI第二层
        /// </summary>
        /// <returns></returns>
        int AI2()
        {
            int keyp = 100000, tempp;
            for (int i = 1; i <= N; i++) for (int j = 1; j <= N; j++)
                {
                    if (!Ok(i, j)) continue;
                    p[i, j] = 3 - s0;
                    tempp = Point(i, j);
                    if (tempp == 0)
                    {
                        p[i, j] = 0;
                        continue;
                    }
                    if (tempp == 10000)
                    {
                        p[i, j] = 0;
                        return -10000;
                    }
                    tempp = AI3(tempp);
                    p[i, j] = 0;
                    if (tempp < keyp) keyp = tempp;//第二层取极小
                }
            return keyp;
        }
        /// <summary>
        /// AI第一层
        /// </summary>
        void AI()
        {
            for (int i = 1; i <= N; i++) for (int j = 1; j <= N; j++) point[i, j] = 0;
                    timerw.Enabled = true;
            if (p[8, 8] == 0) { Go(8, 8, false); timerw.Enabled = false; return; }
            int keyp = -100000, keyi = 0, keyj = 0, tempp;
            for (int i = 1; i <= N; i++)
            {
                for (int j = 1; j <= N; j++)
                {
                    process.Value++;
                    Application.DoEvents();
                    if (!Ok(i, j)) continue;
                    p[i, j] = s0;
                    tempp = Point(i, j);
                    point[i, j] = tempp;
                    if (tempp == 0)
                    {
                        p[i, j] = 0;
                        continue;
                    }//高效剪枝，避开了禁手点和无效点
                    if (tempp == 10000) { Go(i, j, false); timerw.Enabled = false; return; }
                    tempp = AI2();
                    p[i, j] = 0;
                    if (tempp > keyp) { keyp = tempp; keyi = i; keyj = j; }//第一层取极大
                }
            }
            Go(keyi, keyj, false);
            timerw.Enabled = false;
            return;
        }
        bool Player(int row, int col)
        {
            timerb.Enabled = true;
            if (!Ok(row, col))
            {
                events.Text = "此处不能下";
                MessageBox.Show("此处不能下");
                timerb.Enabled = false;
                return false;
            }
            Go(row, col, true);
            timerb.Enabled = false;
            return true;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            process.Maximum = N * N;
            green.Parent = platform;
            green.Location = new Point(235, 235);
            red.Parent = platform;
            red.Visible = false;
            this.BackColor = Color.FromArgb(231, 177, 77);
            //画棋盘
            Bitmap bmp = new Bitmap(480, 480);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.FromArgb(231, 177, 77));
            Pen b = new Pen(Color.Black);
            for (int i = 1; i <= 15; i++)
            {
                g.DrawLine(b, i * 30, 30, i * 30, 450);
            }
            for (int i = 1; i <= 15; i++)
            {
                g.DrawLine(b, 30, i * 30, 450, i * 30);
            }
            g.FillEllipse(Brushes.Black, 115, 115, 10, 10);//画点，下面四个也是画点
            g.FillEllipse(Brushes.Black, 115, 355, 10, 10);
            g.FillEllipse(Brushes.Black, 355, 115, 10, 10);
            g.FillEllipse(Brushes.Black, 355, 355, 10, 10);
            g.FillEllipse(Brushes.Black, 235, 235, 10, 10);
            Pen br = new Pen(Color.Black, 3);
            g.DrawRectangle(br, 25, 25, 430, 430);//画边框
            platform.Image = bmp;

            init();//初始化棋盘
            s0 = 2;//1为AI先，2为玩家先
            ending = false;
            is_end = false;

            //<-------画旁边的内容------->
            //棋子图标
            Brush p;
            p = Brushes.Black;
            Bitmap bmp1 = new Bitmap(30, 30);
            Graphics g2 = Graphics.FromImage(bmp1);
            g2.FillEllipse(p, 1, 1, 28, 28);
            black.Image = bmp1;
            p = Brushes.White;
            Bitmap bmp2 = new Bitmap(30, 30);
            Graphics g3 = Graphics.FromImage(bmp2);
            g3.FillEllipse(p, 1, 1, 28, 28);
            white.Image = bmp2;

            //箭头
            Bitmap bmp3 = new Bitmap(50, 30);
            Graphics arr = Graphics.FromImage(bmp3);
            arr.DrawLine(new Pen(Color.Red, 3), 5, 15, 45, 15);
            arr.DrawLine(new Pen(Color.Red, 3), 33, 5, 43, 15);
            arr.DrawLine(new Pen(Color.Red, 3), 33, 25, 43, 15);
            arrayp.Image = bmp3;
            //<-------画旁边的内容------->

            timerb.Enabled = true;
        }

        private void Platform_MouseMove(object sender, MouseEventArgs e)
        {
            int x = e.X - platform.Location.X, y = e.Y - platform.Location.Y + menuStrip1.Height;
            int x1 = 0, y1 = 0;
            double d = 10000, d2;
            for (int i = 1; i <= 15; i++)
            {
                for (int j = 1; j <= 15; j++)
                {
                    d2 = Distance(i * 30, j * 30, x, y);
                    if (d2 < d)
                    {
                        x1 = i;
                        y1 = j;
                        d = d2;
                    }
                }
            }
            green.Location = new Point(x1 * 30 - 5, y1 * 30 - 5);
        }
        double Distance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
        }

        private void Green_Move(object sender, EventArgs e)
        {
            locate.Text = "位置：" + ((green.Location.X + 5) / 30).ToString() + "," + ((green.Location.Y + 5) / 30).ToString();
        }
        void Clickforget()
        {
            timerb.Enabled = false;
            red.Visible = true;
            if (pvp == true)
            {
                is2 = !is2;
                if (is2 == false)
                {
                    Player((green.Location.X + 15) / 30, (green.Location.Y + 15) / 30);
                    arrayp.Location = new Point(486, 86);
                }
                else
                {
                    Player2((green.Location.X + 15) / 30, (green.Location.Y + 15) / 30);
                    arrayp.Location = new Point(486, 52);
                }
            }
            else if (Player((green.Location.X + 15) / 30, (green.Location.Y + 15) / 30) == true)
            {
                Application.DoEvents();
                if (ending == false)
                {
                    arrayp.Location = new Point(486, 86);
                    events.Text = "  轮到AI下，请稍候。";
                    Application.DoEvents();
                    AI();
                    Application.DoEvents();
                    arrayp.Location = new Point(486, 52);
                    events.Text = "现在轮到你了";
                }
                process.Value = 0;
            }
            timerb.Enabled = true;
        }
        private void Platform_MouseClick(object sender, MouseEventArgs e)
        {
            Clickforget();
        }
        void DrawPoint(int x, int y, bool isblack)
        {
            Brush p;
            if (isblack == true) p = Brushes.Black;
            else p = Brushes.White;
            Bitmap bmp = new Bitmap(30, 30);
            Graphics g = Graphics.FromImage(bmp);
            g.FillEllipse(p, 1, 1, 28, 28);
            PictureBox pic = new PictureBox
            {
                Size = new Size(30, 30),
                BackColor = Color.Transparent,
                Image = bmp,
                Location = new Point(x * 30 - 15, y * 30 - 15),
                Tag=""
            };
            platform.Controls.Add(pic);
            red.Location = new Point(x * 30 - 5, y * 30 - 5);
            red.BringToFront();
        }

        private void Green_MouseClick(object sender, MouseEventArgs e)
        {
            Clickforget();
        }
        private void 新棋局ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            green.Parent = this;
            red.Parent = this;
            red.Visible = false;
            platform.Controls.Clear();
            green.Parent = platform;
            red.Parent = platform;
            init();//初始化棋盘
            s0 = 2;//1为AI先，2为玩家先
            ending = false;
            is_end = false;

            timerb.Enabled = true;
            blackt = 0;
            whitet = 0;
            btime.Text = ToTime(blackt, true);
            wtime.Text = ToTime(whitet, true);
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0); 
        }
        /// <summary>
        /// 填充前位0，如大于则直接返回
        /// </summary>
        /// <param name="input">输入</param>
        /// <param name="digits">目标位数</param>
        /// <returns></returns>
        string Filldigit(double input,int digits)
        {
            string s = input.ToString();
            for(int i= ((int)input).ToString().Length+1; i <= digits; i++)
                s = "0" + s;
            return s;
        }
        /// <summary>
        /// 把秒转成时:分:秒.毫秒
        /// </summary>
        /// <param name="second">秒数</param>
        /// <param name="HaveMillisecond">是否保留毫秒</param>
        /// <returns></returns>
        string ToTime(double second,bool HaveMillisecond)
        {
            double hour=0, minute=0;
            while (second >= 3600)
            {
                second -= 3600;
                hour++;
            }
            while (second >= 60)
            {
                second -= 60;
                minute++;
            }
            if (HaveMillisecond == false) second = Math.Round(second);
            else second = Math.Round(second, 1);
            return Filldigit(hour,2) + ":" + Filldigit(minute, 2) + ":" + Filldigit(second, 2);
        }
        private void Timerb_Tick(object sender, EventArgs e)
        {
            blackt += 0.1;
            btime.Text = ToTime(blackt, true);
        }

        private void Timerw_Tick(object sender, EventArgs e)
        {
            whitet += 0.1;
            wtime.Text = ToTime(whitet, true);
        }

        private void 打开调试窗口ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new TestBox();
            form.Show();
        }

        private void 使用禁手规则ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            banopen = !banopen;
            使用禁手规则ToolStripMenuItem.Checked = !使用禁手规则ToolStripMenuItem.Checked;
        }
        bool Player2(int row,int col)
        {
            timerw.Enabled = true;
            if (!Ok(row, col))
            {
                events.Text = "此处不能下";
                MessageBox.Show("此处不能下");
                return false;
            }
            Go(row, col, false);
            timerw.Enabled = false;
            return true;
        }

        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            pvp = false;
            新棋局ToolStripMenuItem_Click(sender, e);
            toolStripMenuItem1.Checked = true;
            toolStripMenuItem2.Checked = false;
        }

        private void ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            pvp = true;
            新棋局ToolStripMenuItem_Click(sender, e);
            toolStripMenuItem1.Checked = false;
            toolStripMenuItem2.Checked = true;
            is2 = true;
        }
    }
}