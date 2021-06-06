using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Five
{
    public partial class TestBox : Form
    {
        Label[,] tb = new Label[17, 17];
        public TestBox()
        {
            InitializeComponent();

            this.BackColor = Color.Gray;

            //画棋盘
            Bitmap bmp = new Bitmap(480, 480);
            Graphics g = Graphics.FromImage(bmp);
            Pen b = new Pen(Color.White);
            for (int i = 1; i <= 15; i++)
            {
                g.DrawLine(b, i * 30, 30, i * 30, 450);
            }
            for (int i = 1; i <= 15; i++)
            {
                g.DrawLine(b, 30, i * 30, 450, i * 30);
            }
            g.FillEllipse(Brushes.White, 115, 115, 10, 10);//画点，下面四个也是画点
            g.FillEllipse(Brushes.White, 115, 355, 10, 10);
            g.FillEllipse(Brushes.White, 355, 115, 10, 10);
            g.FillEllipse(Brushes.White, 355, 355, 10, 10);
            g.FillEllipse(Brushes.White, 235, 235, 10, 10);
            Pen br = new Pen(Color.White, 3);
            g.DrawRectangle(br, 25, 25, 430, 430);//画边框
            platform.Image = bmp;

            for (int i=1;i<=15;i++) for(int j = 1; j <= 15; j++)
                {
                    tb[i, j] = new Label
                    {
                        BackColor = Color.Transparent,
                        Location = new Point(i * 30 - 14, j * 30 - 15),
                        Size = new Size(30, 30),
                        TextAlign = ContentAlignment.MiddleCenter,
                        Font = new Font("宋体", 8, FontStyle.Bold)
                    };
                    platform.Controls.Add(tb[i, j]);
                }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 1; i <= 15; i++) for (int j = 1; j <= 15; j++)
                {
                    switch(MainForm.point[i, j])
                    {
                        case 0: { tb[i, j].ForeColor = Color.Black;break; }
                        case 1: { tb[i, j].ForeColor = Color.Purple; break; }
                        case 2: { tb[i, j].ForeColor = Color.Blue; break; }
                        case 3: { tb[i, j].ForeColor = Color.SkyBlue; break; }
                        case 4: { tb[i, j].ForeColor = Color.DarkGreen; break; }
                        case 5: { tb[i, j].ForeColor = Color.Green; break; }
                        case 6: { tb[i, j].ForeColor = Color.SpringGreen; break; }
                        case 7: { tb[i, j].ForeColor = Color.YellowGreen; break; }
                        case 8: { tb[i, j].ForeColor = Color.GreenYellow; break; }
                        case 9: { tb[i, j].ForeColor = Color.Yellow; break; }
                        default:
                            {
                                if (MainForm.point[i, j] >= 1000) tb[i, j].ForeColor = Color.Red;
                                else if (MainForm.point[i, j] >= 100) tb[i, j].ForeColor = Color.Orange;
                                else if (MainForm.point[i, j] >= 10) tb[i, j].ForeColor = Color.LightGoldenrodYellow;
                                break;
                            }
                    }
                    tb[i, j].Text = MainForm.point[i, j].ToString();
                }
        }
    }
}
