namespace Five
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.platform = new System.Windows.Forms.PictureBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.locate = new System.Windows.Forms.ToolStripStatusLabel();
            this.process = new System.Windows.Forms.ToolStripProgressBar();
            this.events = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新棋局ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.棋局ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.使用禁手规则ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.打开调试窗口ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.red = new System.Windows.Forms.PictureBox();
            this.green = new System.Windows.Forms.PictureBox();
            this.black = new System.Windows.Forms.PictureBox();
            this.white = new System.Windows.Forms.PictureBox();
            this.btime = new System.Windows.Forms.Label();
            this.wtime = new System.Windows.Forms.Label();
            this.timerb = new System.Windows.Forms.Timer(this.components);
            this.timerw = new System.Windows.Forms.Timer(this.components);
            this.arrayp = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.platform)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.red)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.green)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.black)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.white)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.arrayp)).BeginInit();
            this.SuspendLayout();
            // 
            // platform
            // 
            this.platform.Location = new System.Drawing.Point(0, 31);
            this.platform.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.platform.Name = "platform";
            this.platform.Size = new System.Drawing.Size(640, 600);
            this.platform.TabIndex = 0;
            this.platform.TabStop = false;
            this.platform.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Platform_MouseClick);
            this.platform.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Platform_MouseMove);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.locate,
            this.process,
            this.events});
            this.statusStrip1.Location = new System.Drawing.Point(0, 633);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 13, 0);
            this.statusStrip1.Size = new System.Drawing.Size(861, 26);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // locate
            // 
            this.locate.BackColor = System.Drawing.SystemColors.Control;
            this.locate.Name = "locate";
            this.locate.Size = new System.Drawing.Size(99, 20);
            this.locate.Text = "等待中。。。";
            // 
            // process
            // 
            this.process.Name = "process";
            this.process.Size = new System.Drawing.Size(100, 18);
            // 
            // events
            // 
            this.events.BackColor = System.Drawing.SystemColors.Control;
            this.events.Name = "events";
            this.events.Size = new System.Drawing.Size(99, 20);
            this.events.Text = "等待中。。。";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.棋局ToolStripMenuItem,
            this.打开调试窗口ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(861, 28);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新棋局ToolStripMenuItem,
            this.退出ToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 新棋局ToolStripMenuItem
            // 
            this.新棋局ToolStripMenuItem.Name = "新棋局ToolStripMenuItem";
            this.新棋局ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.新棋局ToolStripMenuItem.Size = new System.Drawing.Size(195, 26);
            this.新棋局ToolStripMenuItem.Text = "新棋局";
            this.新棋局ToolStripMenuItem.Click += new System.EventHandler(this.新棋局ToolStripMenuItem_Click);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(195, 26);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // 棋局ToolStripMenuItem
            // 
            this.棋局ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.使用禁手规则ToolStripMenuItem,
            this.toolStripSeparator1,
            this.toolStripMenuItem1,
            this.toolStripMenuItem2});
            this.棋局ToolStripMenuItem.Name = "棋局ToolStripMenuItem";
            this.棋局ToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.棋局ToolStripMenuItem.Text = "棋局";
            // 
            // 使用禁手规则ToolStripMenuItem
            // 
            this.使用禁手规则ToolStripMenuItem.Name = "使用禁手规则ToolStripMenuItem";
            this.使用禁手规则ToolStripMenuItem.Size = new System.Drawing.Size(182, 26);
            this.使用禁手规则ToolStripMenuItem.Text = "使用禁手规则";
            this.使用禁手规则ToolStripMenuItem.Click += new System.EventHandler(this.使用禁手规则ToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(179, 6);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Checked = true;
            this.toolStripMenuItem1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(182, 26);
            this.toolStripMenuItem1.Text = "人机对战";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.ToolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(182, 26);
            this.toolStripMenuItem2.Text = "人人对战";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.ToolStripMenuItem2_Click);
            // 
            // 打开调试窗口ToolStripMenuItem
            // 
            this.打开调试窗口ToolStripMenuItem.Name = "打开调试窗口ToolStripMenuItem";
            this.打开调试窗口ToolStripMenuItem.Size = new System.Drawing.Size(113, 24);
            this.打开调试窗口ToolStripMenuItem.Text = "打开调试窗口";
            this.打开调试窗口ToolStripMenuItem.Click += new System.EventHandler(this.打开调试窗口ToolStripMenuItem_Click);
            // 
            // red
            // 
            this.red.BackColor = System.Drawing.Color.Red;
            this.red.Location = new System.Drawing.Point(445, 392);
            this.red.Margin = new System.Windows.Forms.Padding(4);
            this.red.Name = "red";
            this.red.Size = new System.Drawing.Size(13, 12);
            this.red.TabIndex = 6;
            this.red.TabStop = false;
            this.red.Tag = "Last";
            // 
            // green
            // 
            this.green.BackColor = System.Drawing.Color.Lime;
            this.green.Location = new System.Drawing.Point(445, 225);
            this.green.Margin = new System.Windows.Forms.Padding(4);
            this.green.Name = "green";
            this.green.Size = new System.Drawing.Size(13, 12);
            this.green.TabIndex = 5;
            this.green.TabStop = false;
            this.green.Tag = "Now";
            this.green.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Green_MouseClick);
            this.green.Move += new System.EventHandler(this.Green_Move);
            // 
            // black
            // 
            this.black.Location = new System.Drawing.Point(720, 65);
            this.black.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.black.Name = "black";
            this.black.Size = new System.Drawing.Size(40, 38);
            this.black.TabIndex = 7;
            this.black.TabStop = false;
            // 
            // white
            // 
            this.white.Location = new System.Drawing.Point(720, 108);
            this.white.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.white.Name = "white";
            this.white.Size = new System.Drawing.Size(40, 38);
            this.white.TabIndex = 8;
            this.white.TabStop = false;
            // 
            // btime
            // 
            this.btime.AutoSize = true;
            this.btime.Location = new System.Drawing.Point(767, 88);
            this.btime.Name = "btime";
            this.btime.Size = new System.Drawing.Size(71, 15);
            this.btime.TabIndex = 9;
            this.btime.Text = "00:00:00";
            // 
            // wtime
            // 
            this.wtime.AutoSize = true;
            this.wtime.Location = new System.Drawing.Point(767, 130);
            this.wtime.Name = "wtime";
            this.wtime.Size = new System.Drawing.Size(71, 15);
            this.wtime.TabIndex = 10;
            this.wtime.Text = "00:00:00";
            // 
            // timerb
            // 
            this.timerb.Tick += new System.EventHandler(this.Timerb_Tick);
            // 
            // timerw
            // 
            this.timerw.Tick += new System.EventHandler(this.Timerw_Tick);
            // 
            // arrayp
            // 
            this.arrayp.Location = new System.Drawing.Point(648, 65);
            this.arrayp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.arrayp.Name = "arrayp";
            this.arrayp.Size = new System.Drawing.Size(67, 38);
            this.arrayp.TabIndex = 11;
            this.arrayp.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(861, 659);
            this.Controls.Add(this.arrayp);
            this.Controls.Add(this.wtime);
            this.Controls.Add(this.btime);
            this.Controls.Add(this.white);
            this.Controls.Add(this.black);
            this.Controls.Add(this.red);
            this.Controls.Add(this.green);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.platform);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "五子棋";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.platform)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.red)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.green)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.black)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.white)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.arrayp)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox platform;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripProgressBar process;
        private System.Windows.Forms.ToolStripStatusLabel events;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新棋局ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.PictureBox red;
        private System.Windows.Forms.PictureBox green;
        private System.Windows.Forms.ToolStripStatusLabel locate;
        private System.Windows.Forms.ToolStripMenuItem 棋局ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 使用禁手规则ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.PictureBox black;
        private System.Windows.Forms.PictureBox white;
        private System.Windows.Forms.Label btime;
        private System.Windows.Forms.Label wtime;
        private System.Windows.Forms.Timer timerb;
        private System.Windows.Forms.Timer timerw;
        private System.Windows.Forms.PictureBox arrayp;
        private System.Windows.Forms.ToolStripMenuItem 打开调试窗口ToolStripMenuItem;
    }
}

