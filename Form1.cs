using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Five
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        const int N = 15;
        int[,] p = new int[N + 2, N + 2]; //0空1黑2白  1●2○ -1▲-2△
        int s = 0, ais = 1, s0;//s是轮到谁下,s=1,2，s=1是ai下，s=2是玩家，s=s0是黑方下，否则是白方下
        bool is_end = false;
        int[] dx = { 1, 1, 0, -1, -1, -1, 0, 1 }; //flat技术
        int[] dy = { 0, 1, 1, 1, 0, -1, -1, -1 };//（dx,dy）是8个方向向量
        int[,] manu = new int[2, 300];
        int manukey = 0;//棋谱

        bool inboard(int row, int col)//判断(row,col)是否在棋盘内
        {
            if (row < 1 || row > N) return false;
            return col >= 1 && col <= N;
        }

        bool same(int row, int col, int key)//判断2个棋子是否同色
        {
            if (!inboard(row, col)) return false;
            return (p[row, col] == key || p[row, col] + key == 0);
        }

        int num(int row, int col, int u)//坐标（row,col），方向向量u，返回该方向有多少连续同色棋子
        {
            int i = row + dx[u], j = col + dy[u], sum = 0, ref1 = p[row, col];
            if (ref1 == 0) return 0;
            while (same(i, j, ref1))
            {
                sum++;
                i += dx[u];
                j += dy[u];
            }
            return sum;
        }

        int live4(int row, int col)//落子成活4的数量
        {
            int sum = 0, i, u;
            for (u = 0; u < 4; u++)//4个方向，判断每个方向是否落子就成活4
            {
                int sumk = 1;
                for (i = 1; same_u_i(row, col, u, i); i++) sumk++;
                if (OutOrNotEmpty(row, col, u, i)) continue;
                for (i = -1; same_u_i(row, col, u, i); i--) sumk++;
                if (OutOrNotEmpty(row, col, u, i)) continue;
                if (sumk == 4) sum++;
            }
            return sum;
        }
        bool same_u_i(int row, int col, int u, int i)
        {
            return same(row + dx[u] * i, col + dy[u] * i, p[row, col]);//u方向i距离的点是否同色
        }
        bool OutOrNotEmpty(int row, int col, int u, int i)
        {
            return (!inboard(row + dx[u] * i, col + dy[u] * i) || p[row + dx[u] * i, col + dy[u] * i] != 0);//出了棋盘或者非空格点
        }
        int cheng5(int row, int col)//成5点的数量
        {
            int sum = 0, i, u;
            for (u = 0; u < 8; u++)//8个成五点的方向
            {
                int sumk = 0;
                bool flag = true;
                for (i = 1; same_u_i(row, col, u, i) || flag; i++)
                {
                    if (!same_u_i(row, col, u, i))//该方向的第一个不同色的点，超出边界或者对方棋子或空格
                    {
                        if (p[row + dx[u] * i, col + dy[u] * i] != 0) sumk -= 10;//该方向的第一个不同色的点是对方棋子,没有成五点
                        flag = false;
                    }
                    sumk++;
                }
                if (!inboard(row + dx[u] * --i, col + dy[u] * i)) continue;//该方向的第一个不同色的点是超出边界,没有成五点
                for (i = -1; same_u_i(row, col, u, i); i--) sumk++;
                if (sumk == 4) sum++;
            }
            return sum;
        }

        int chong4(int row, int col)//冲4的数量
        {
            return cheng5(row, col) - live4(row, col) * 2;
        }

        int live3(int row, int col)//落子成活3的数量
        {
            int key = p[row, col], sum = 0, i, u, flag = 2;
            for (u = 0; u < 4; u++)//三连的活三
            {
                int sumk = 1;
                for (i = 1; same_u_i(row, col, u, i); i++) sumk++;
                if (OutOrNotEmpty(row, col, u, i)) continue;
                i++;
                if (OutOrNotEmpty(row, col, u, i)) flag--;
                for (i = -1; same_u_i(row, col, u, i); i--) sumk++;
                if (OutOrNotEmpty(row, col, u, i)) continue;
                i--;
                if (OutOrNotEmpty(row, col, u, i)) flag--;
                if (sumk == 3 && flag > 0) sum++;
            }
            for (u = 0; u < 8; u++)//8个方向，每个方向最多1个非三连的活三
            {
                int sumk = 0;
                bool flag1 = true;
                for (i = 1; same_u_i(row, col, u, i) || flag1; i++)//成活四点的方向
                {
                    if (!same_u_i(row, col, u, i))
                    {
                        if (flag1 && p[row + dx[u] * i, col + dy[u] * i] != 0) sumk -= 10;
                        flag1 = false;
                    }
                    sumk++;
                }
                if (OutOrNotEmpty(row, col, u, i)) continue; ;
                if (p[row + dx[u] * --i, col + dy[u] * i] == 0) continue;
                for (i = 1; same_u_i(row, col, u, i); i++) sumk++;
                if (OutOrNotEmpty(row, col, u, i)) continue; ;
                if (sumk == 3) sum++;
            }
            return sum;
        }

        bool overline(int row, int col)//长连禁手
        {
            for (int u = 0; u < 4; u++) if (num(row, col, u) + num(row, col, u + 4) > 4) return true;
            return false;
        }

        bool ban(int row, int col)//判断落子后是否成禁手
        {
            if (same(row, col, 2)) return false;//白方无禁手
            return live3(row, col) > 1 || overline(row, col) || live4(row, col) + chong4(row, col) > 1;
        }

        bool end_(int row, int col)//(row,col)处落子之后是否游戏结束
        {
            for (int u = 0; u < 4; u++) if (num(row, col, u) + num(row, col, u + 4) >= 4) is_end = true;
            if (is_end) return true;
            is_end = ban(row, col);
            return is_end;
        }

        void go(int row, int col)//落下一子
        {
            if (s == s0) p[row, col] = -1; //标出最新下的棋
            else p[row, col] = -2;
            for (int i = 0; i <= N; i++) for (int j = 0; j <= N; j++) //取消上一个最新棋的标识
                {
                    if (i == row && j == col) continue;
                    if (p[i, j] < 0) p[i, j] *= -1;
                }
            //DrawBoard();
            if (ban(row, col))
            {
                events.Text = "禁手";
                if (s0 == 1) events.Text = "玩家胜";
                else events.Text = "AI胜";
            }
            if (end_(row, col))
            {
                if (s == ais) events.Text = "AI胜";
                else events.Text = "玩家胜";
            }
            manu[0, manukey] = row; manu[1, manukey++] = col;
        }

        bool ok(int row, int col)//能否落子
        {
            return inboard(row, col) && (p[row, col] == 0);
        }

        int point(int row, int col)//非负分值
        {
            if (ban(row, col)) return 0;//禁手0分
            if (end_(row, col))
            {
                is_end = false;
                return 10000;
            }
            int ret = live4(row, col) * 1000 + (chong4(row, col) + live3(row, col)) * 100, u;
            for (u = 0; u < 8; u++) if (p[row + dx[u], col + dy[u]] != 0) ret++;//无效点0分
            return ret;
        }

        int AI3(int p2)
        {
            int keyp = -100000, tempp;
            for (int i = 1; i <= N; i++) for (int j = 1; j <= N; j++)
                {
                    if (!ok(i, j)) continue;
                    p[i, j] = s0;
                    tempp = point(i, j);
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

        int AI2()
        {
            int keyp = 100000, tempp;
            for (int i = 1; i <= N; i++) for (int j = 1; j <= N; j++)
                {
                    if (!ok(i, j)) continue;
                    p[i, j] = 3 - s0;
                    tempp = point(i, j);
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
        private void Form1_Load(object sender, EventArgs e)
        {
            green.Parent = platform;
            green.Location = new Point(235, 235);
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
            g.DrawRectangle(br, 25, 25, 430, 430);
            platform.Image = bmp;
        }

        private void platform_MouseMove(object sender, MouseEventArgs e)
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
            green.Location = new Point(x1 * 30-5, y1 * 30-5);
        }
        double Distance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
        }

        void AI()
        {
            //DrawBoard();
            events.Text = "  轮到AI下，请稍候： ";
            if (p[8, 8] == 0) { go(8, 8); return; }
            int i, j;
            int keyp = -100000, keyi = new int(), keyj = new int(), tempp;
            for (i = 1; i <= N; i++)
            {
                for (j = 1; j <= N; j++)
                {
                    if (!ok(i, j)) continue;
                    p[i, j] = s0;
                    tempp = point(i, j);
                    if (tempp == 0)
                    {
                        p[i, j] = 0;
                        continue;
                    }//高效剪枝，避开了禁手点和无效点
                    if (tempp == 10000) { go(i, j); return; }
                    tempp = AI2();
                    p[i, j] = 0;
                    if (tempp > keyp) { keyp = tempp; keyi = i; keyj = j; }//第一层取极大
                }
            }
            go(keyi, keyj);
            DrawPoint(keyi, keyj, false);
            return;
        }

        private void platform_MouseClick(object sender, MouseEventArgs e)
        {
            player((green.Location.X + 15) / 30, (green.Location.Y + 15) / 30);
            AI();
        }

        bool player( int row,int col)
        {
            //DrawBoard();
            if (!ok(row, col))//不能下
            {
                return false;
            }
            go(row, col);
            DrawPoint(row, col, true);
            return true;
        }
        void DrawPoint(int x,int y,bool isblack)
        {
            Brush p;
            if (isblack == true) p = Brushes.Black;
            else p = Brushes.White;
            Bitmap bmp = new Bitmap(30, 30);
            Graphics g = Graphics.FromImage(bmp);
            g.FillEllipse(p, 1, 1, 28, 28);
            PictureBox pic = new PictureBox();
            pic.Size = new Size(30, 30);
            pic.BackColor = Color.Transparent;
            pic.Image = bmp;
            pic.Location = new Point(x * 30 - 15, y * 30 - 15);
            platform.Controls.Add(pic);
        }
    }
}