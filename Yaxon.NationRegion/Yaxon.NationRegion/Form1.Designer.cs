namespace Yaxon.NationRegion
{
    partial class Form1
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.操作ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.省ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.市级ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.区县ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.镇ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.村ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.其它ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.异常数据处理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.操作ToolStripMenuItem,
            this.其它ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(680, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(0, 28);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(680, 472);
            this.listBox1.TabIndex = 1;
            // 
            // 操作ToolStripMenuItem
            // 
            this.操作ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.省ToolStripMenuItem,
            this.市级ToolStripMenuItem,
            this.区县ToolStripMenuItem,
            this.镇ToolStripMenuItem,
            this.村ToolStripMenuItem});
            this.操作ToolStripMenuItem.Name = "操作ToolStripMenuItem";
            this.操作ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.操作ToolStripMenuItem.Text = "操作";
            // 
            // 省ToolStripMenuItem
            // 
            this.省ToolStripMenuItem.Name = "省ToolStripMenuItem";
            this.省ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.省ToolStripMenuItem.Text = "省";
            this.省ToolStripMenuItem.Click += new System.EventHandler(this.省ToolStripMenuItem_Click);
            // 
            // 市级ToolStripMenuItem
            // 
            this.市级ToolStripMenuItem.Name = "市级ToolStripMenuItem";
            this.市级ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.市级ToolStripMenuItem.Text = "市级";
            this.市级ToolStripMenuItem.Click += new System.EventHandler(this.市级ToolStripMenuItem_Click);
            // 
            // 区县ToolStripMenuItem
            // 
            this.区县ToolStripMenuItem.Name = "区县ToolStripMenuItem";
            this.区县ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.区县ToolStripMenuItem.Text = "区/县";
            this.区县ToolStripMenuItem.Click += new System.EventHandler(this.区县ToolStripMenuItem_Click);
            // 
            // 镇ToolStripMenuItem
            // 
            this.镇ToolStripMenuItem.Name = "镇ToolStripMenuItem";
            this.镇ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.镇ToolStripMenuItem.Text = "镇";
            this.镇ToolStripMenuItem.Click += new System.EventHandler(this.镇ToolStripMenuItem_Click);
            // 
            // 村ToolStripMenuItem
            // 
            this.村ToolStripMenuItem.Name = "村ToolStripMenuItem";
            this.村ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.村ToolStripMenuItem.Text = "村";
            this.村ToolStripMenuItem.Click += new System.EventHandler(this.村ToolStripMenuItem_Click);
            // 
            // 其它ToolStripMenuItem
            // 
            this.其它ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.异常数据处理ToolStripMenuItem});
            this.其它ToolStripMenuItem.Name = "其它ToolStripMenuItem";
            this.其它ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.其它ToolStripMenuItem.Text = "其它";
            // 
            // 异常数据处理ToolStripMenuItem
            // 
            this.异常数据处理ToolStripMenuItem.Name = "异常数据处理ToolStripMenuItem";
            this.异常数据处理ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.异常数据处理ToolStripMenuItem.Text = "异常数据处理";
            this.异常数据处理ToolStripMenuItem.Click += new System.EventHandler(this.异常数据处理ToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 507);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "爬虫全国行政区域数据(国家统计局)";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ToolStripMenuItem 操作ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 省ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 市级ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 区县ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 镇ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 村ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 其它ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 异常数据处理ToolStripMenuItem;
    }
}

