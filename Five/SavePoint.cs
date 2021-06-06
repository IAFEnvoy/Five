using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Five
{
    class SavePoint
    {
        public static void SaveNow(PictureBox pic)
        {
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

            //画棋子
            foreach (Control con in pic.Controls)
            {
                if ((string)con.Tag == "")
                {
                    PictureBox p = (PictureBox)con;
                    g.DrawImage(p.Image, p.Location);
                }
            }
            bmp.Save(Application.StartupPath + @"\lostdata" + DateTime.Now.ToFileTimeUtc().ToString() + ".png",ImageFormat.Png);
        }
    }
}
