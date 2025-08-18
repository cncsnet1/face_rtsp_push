namespace screen_window
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnToggleCapture;
        private System.Windows.Forms.Button btnSelectRegion;
        private System.Windows.Forms.Button btnFullScreen;
        private System.Windows.Forms.Button btnToggleStream;
        private System.Windows.Forms.Panel pnlPreview;
        private System.Windows.Forms.TextBox txtStreamUrl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem tsmiShow;
        private System.Windows.Forms.ToolStripMenuItem tsmiExit;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1050, 650); // 增加窗口宽度和高度
            this.Text = "公安视频监控推流系统"; // 更专业的系统名称
            this.BackColor = System.Drawing.Color.FromArgb(240, 242, 245); // 浅灰色背景，减少视觉疲劳
            this.Icon = System.Drawing.Icon.ExtractAssociatedIcon(System.Reflection.Assembly.GetExecutingAssembly().Location);
            this.MinimizeBox = true;
            this.MaximizeBox = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            // 创建标题栏标签
            System.Windows.Forms.Label titleLabel = new System.Windows.Forms.Label();
            titleLabel.Dock = System.Windows.Forms.DockStyle.Top;
            titleLabel.Height = 30;
            titleLabel.BackColor = System.Drawing.Color.FromArgb(22, 53, 78); // 公安深蓝色
            titleLabel.ForeColor = System.Drawing.Color.White;
            titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            titleLabel.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            titleLabel.Text = "公安视频监控推流系统 - 专业版";
            this.Controls.Add(titleLabel);

            // 创建警戒线装饰
            System.Windows.Forms.Panel warningLine = new System.Windows.Forms.Panel();
            warningLine.Dock = System.Windows.Forms.DockStyle.Top;
            warningLine.Height = 4;
            warningLine.BackgroundImage = CreateWarningLinePattern();
            warningLine.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(warningLine);

            // 创建托盘菜单
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiShow = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {this.tsmiShow, this.tsmiExit});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(101, 48);

            // tsmiShow
            this.tsmiShow.Name = "tsmiShow";
            this.tsmiShow.Size = new System.Drawing.Size(100, 22);
            this.tsmiShow.Text = "显示窗口";
            this.tsmiShow.Click += new System.EventHandler(this.tsmiShow_Click);

            // tsmiExit
            this.tsmiExit.Name = "tsmiExit";
            this.tsmiExit.Size = new System.Drawing.Size(100, 22);
            this.tsmiExit.Text = "退出";
            this.tsmiExit.Click += new System.EventHandler(this.tsmiExit_Click);

            // notifyIcon
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
            this.notifyIcon.Text = "公安视频监控推流系统";
            this.notifyIcon.Visible = true;
            this.notifyIcon.DoubleClick += new System.EventHandler(this.notifyIcon_DoubleClick);

            // 创建一个面板作为顶部工具栏
            System.Windows.Forms.Panel toolPanel = new System.Windows.Forms.Panel();
            toolPanel.Dock = System.Windows.Forms.DockStyle.Top;
            toolPanel.Height = 70;
            toolPanel.BackColor = System.Drawing.Color.FromArgb(33, 73, 112); // 公安蓝主色调
            toolPanel.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);

            // 创建公安徽标
            System.Windows.Forms.PictureBox policeLogo = new System.Windows.Forms.PictureBox();
            policeLogo.Location = new System.Drawing.Point(15, 10);
            policeLogo.Name = "policeLogo";
            policeLogo.Size = new System.Drawing.Size(45, 45);
            policeLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 使用盾形图标作为临时公安标志
            using (System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(45, 45))
            using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bmp))
            {
                // 绘制蓝色盾牌
                g.FillEllipse(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(0, 51, 102)), 5, 5, 35, 35);
                // 绘制五角星
                DrawStar(g, 22, 22, 15, System.Drawing.Color.Yellow);
                policeLogo.Image = (System.Drawing.Image)bmp.Clone();
            }
            toolPanel.Controls.Add(policeLogo);

            // 添加公安标识文字
            System.Windows.Forms.Label policeLabel = new System.Windows.Forms.Label();
            policeLabel.Location = new System.Drawing.Point(70, 15);
            policeLabel.Name = "policeLabel";
            policeLabel.Size = new System.Drawing.Size(100, 35);
            policeLabel.ForeColor = System.Drawing.Color.White;
            policeLabel.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            policeLabel.Text = "公安系统专用";
            policeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            toolPanel.Controls.Add(policeLabel);

            // 按钮容器 - 使用FlowLayoutPanel使按钮布局更灵活
            System.Windows.Forms.FlowLayoutPanel buttonPanel = new System.Windows.Forms.FlowLayoutPanel();
            buttonPanel.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            buttonPanel.Location = new System.Drawing.Point(180, 5);
            buttonPanel.Name = "buttonPanel";
            buttonPanel.Size = new System.Drawing.Size(600, 55);
            buttonPanel.BackColor = System.Drawing.Color.Transparent;
            buttonPanel.Padding = new System.Windows.Forms.Padding(5);
            buttonPanel.AutoSize = false;
            buttonPanel.WrapContents = false;

            // btnToggleCapture - 重新设计为主要操作按钮
            this.btnToggleCapture = new System.Windows.Forms.Button();
            this.btnToggleCapture.Name = "btnToggleCapture";
            this.btnToggleCapture.Size = new System.Drawing.Size(140, 45);
            this.btnToggleCapture.TabIndex = 0;
            this.btnToggleCapture.Text = "开始抓取屏幕";
            this.btnToggleCapture.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.btnToggleCapture.ForeColor = System.Drawing.Color.White;
            this.btnToggleCapture.BackColor = System.Drawing.Color.FromArgb(39, 174, 96); // 成功绿
            this.btnToggleCapture.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToggleCapture.FlatAppearance.BorderSize = 0;
            this.btnToggleCapture.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnToggleCapture.Click += new System.EventHandler(this.btnToggleCapture_Click);
            // 添加悬停效果
            this.btnToggleCapture.MouseEnter += (sender, e) => this.btnToggleCapture.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnToggleCapture.MouseLeave += (sender, e) => this.btnToggleCapture.BackColor = System.Drawing.Color.FromArgb(39, 174, 96);
            buttonPanel.Controls.Add(this.btnToggleCapture);

            // 添加分隔符
            System.Windows.Forms.Panel separator1 = new System.Windows.Forms.Panel();
            separator1.Width = 15;
            separator1.Height = 45;
            separator1.BackColor = System.Drawing.Color.Transparent;
            buttonPanel.Controls.Add(separator1);

            // btnSelectRegion
            this.btnSelectRegion = new System.Windows.Forms.Button();
            this.btnSelectRegion.Name = "btnSelectRegion";
            this.btnSelectRegion.Size = new System.Drawing.Size(100, 45);
            this.btnSelectRegion.TabIndex = 1;
            this.btnSelectRegion.Text = "选择区域";
            this.btnSelectRegion.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btnSelectRegion.ForeColor = System.Drawing.Color.White;
            this.btnSelectRegion.BackColor = System.Drawing.Color.FromArgb(52, 152, 219); // 信息蓝
            this.btnSelectRegion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectRegion.FlatAppearance.BorderSize = 0;
            this.btnSelectRegion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelectRegion.Click += new System.EventHandler(this.btnSelectRegion_Click);
            // 添加悬停效果
            this.btnSelectRegion.MouseEnter += (sender, e) => this.btnSelectRegion.BackColor = System.Drawing.Color.FromArgb(41, 128, 185);
            this.btnSelectRegion.MouseLeave += (sender, e) => this.btnSelectRegion.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            buttonPanel.Controls.Add(this.btnSelectRegion);

            // 添加分隔符
            System.Windows.Forms.Panel separator2 = new System.Windows.Forms.Panel();
            separator2.Width = 15;
            separator2.Height = 45;
            separator2.BackColor = System.Drawing.Color.Transparent;
            buttonPanel.Controls.Add(separator2);

            // btnFullScreen
            this.btnFullScreen = new System.Windows.Forms.Button();
            this.btnFullScreen.Name = "btnFullScreen";
            this.btnFullScreen.Size = new System.Drawing.Size(100, 45);
            this.btnFullScreen.TabIndex = 2;
            this.btnFullScreen.Text = "全屏录制";
            this.btnFullScreen.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btnFullScreen.ForeColor = System.Drawing.Color.White;
            this.btnFullScreen.BackColor = System.Drawing.Color.FromArgb(155, 89, 182); // 紫色
            this.btnFullScreen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFullScreen.FlatAppearance.BorderSize = 0;
            this.btnFullScreen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFullScreen.Click += new System.EventHandler(this.btnFullScreen_Click);
            // 添加悬停效果
            this.btnFullScreen.MouseEnter += (sender, e) => this.btnFullScreen.BackColor = System.Drawing.Color.FromArgb(142, 68, 173);
            this.btnFullScreen.MouseLeave += (sender, e) => this.btnFullScreen.BackColor = System.Drawing.Color.FromArgb(155, 89, 182);
            buttonPanel.Controls.Add(this.btnFullScreen);

            // 添加分隔符
            System.Windows.Forms.Panel separator3 = new System.Windows.Forms.Panel();
            separator3.Width = 15;
            separator3.Height = 45;
            separator3.BackColor = System.Drawing.Color.Transparent;
            buttonPanel.Controls.Add(separator3);

            // 推流地址容器
            System.Windows.Forms.Panel streamUrlPanel = new System.Windows.Forms.Panel();
            streamUrlPanel.Width = 280;
            streamUrlPanel.Height = 45;
            streamUrlPanel.BackColor = System.Drawing.Color.Transparent;

            // label1
            this.label1 = new System.Windows.Forms.Label();
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "推流地址：";
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F);
            streamUrlPanel.Controls.Add(this.label1);

            // txtStreamUrl
            this.txtStreamUrl = new System.Windows.Forms.TextBox();
            this.txtStreamUrl.Location = new System.Drawing.Point(70, 7);
            this.txtStreamUrl.Name = "txtStreamUrl";
            this.txtStreamUrl.Size = new System.Drawing.Size(200, 23);
            this.txtStreamUrl.TabIndex = 4;
            this.txtStreamUrl.Text = "rtsp://localhost:8554/live/stream";
            this.txtStreamUrl.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.txtStreamUrl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtStreamUrl.BackColor = System.Drawing.Color.White;
            this.txtStreamUrl.ForeColor = System.Drawing.Color.FromArgb(51, 51, 51);
            streamUrlPanel.Controls.Add(this.txtStreamUrl);
            buttonPanel.Controls.Add(streamUrlPanel);

            // 添加分隔符
            System.Windows.Forms.Panel separator4 = new System.Windows.Forms.Panel();
            separator4.Width = 15;
            separator4.Height = 45;
            separator4.BackColor = System.Drawing.Color.Transparent;
            buttonPanel.Controls.Add(separator4);

            // btnToggleStream
            this.btnToggleStream = new System.Windows.Forms.Button();
            this.btnToggleStream.Name = "btnToggleStream";
            this.btnToggleStream.Size = new System.Drawing.Size(100, 45);
            this.btnToggleStream.TabIndex = 5;
            this.btnToggleStream.Text = "开始推流";
            this.btnToggleStream.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.btnToggleStream.ForeColor = System.Drawing.Color.White;
            this.btnToggleStream.BackColor = System.Drawing.Color.FromArgb(231, 76, 60); // 危险红
            this.btnToggleStream.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToggleStream.FlatAppearance.BorderSize = 0;
            this.btnToggleStream.Enabled = false;
            this.btnToggleStream.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnToggleStream.Click += new System.EventHandler(this.btnToggleStream_Click);
            // 添加悬停效果
            this.btnToggleStream.MouseEnter += (sender, e) => this.btnToggleStream.BackColor = System.Drawing.Color.FromArgb(192, 57, 43);
            this.btnToggleStream.MouseLeave += (sender, e) => this.btnToggleStream.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            buttonPanel.Controls.Add(this.btnToggleStream);

            // 设置FlowLayoutPanel自动调整大小
            buttonPanel.AutoSize = true;
            buttonPanel.WrapContents = false;
            buttonPanel.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;

            // 将按钮容器添加到工具面板
            toolPanel.Controls.Add(buttonPanel);

            // 确保按钮面板不会覆盖公安徽标
            buttonPanel.Location = new System.Drawing.Point(180, 5);
            buttonPanel.Width = toolPanel.Width - 200;  // 留出公安徽标和文字的空间

            // 创建左侧控制面板
            System.Windows.Forms.Panel controlPanel = new System.Windows.Forms.Panel();
            controlPanel.Location = new System.Drawing.Point(20, 115);
            controlPanel.Name = "controlPanel";
            controlPanel.Size = new System.Drawing.Size(220, 430);
            controlPanel.BackColor = System.Drawing.Color.White;
            controlPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 添加阴影效果
            controlPanel.BackColor = System.Drawing.Color.White;
            controlPanel.Paint += (sender, e) =>
            {
                System.Windows.Forms.Panel p = sender as System.Windows.Forms.Panel;
                e.Graphics.Clear(System.Drawing.Color.White);
                // 添加左侧蓝色边框
                e.Graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(33, 73, 112)), 0, 0, 4, p.Height);
            };

            // 添加控制面板标题
            System.Windows.Forms.Label controlPanelTitle = new System.Windows.Forms.Label();
            controlPanelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            controlPanelTitle.Height = 30;
            controlPanelTitle.BackColor = System.Drawing.Color.FromArgb(240, 242, 245);
            controlPanelTitle.ForeColor = System.Drawing.Color.FromArgb(51, 51, 51);
            controlPanelTitle.Text = "系统状态";
            controlPanelTitle.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            controlPanelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            controlPanelTitle.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            controlPanel.Controls.Add(controlPanelTitle);

            // 添加状态信息
            System.Windows.Forms.Label statusLabel = new System.Windows.Forms.Label();
            statusLabel.Location = new System.Drawing.Point(10, 40);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new System.Drawing.Size(180, 20);
            statusLabel.Text = "当前状态: 就绪";
            statusLabel.Font = new System.Drawing.Font("微软雅黑", 9F);
            statusLabel.ForeColor = System.Drawing.Color.FromArgb(51, 51, 51);
            controlPanel.Controls.Add(statusLabel);

            // 添加录制状态指示灯
            System.Windows.Forms.Label recordingStatusLabel = new System.Windows.Forms.Label();
            recordingStatusLabel.Location = new System.Drawing.Point(10, 70);
            recordingStatusLabel.Name = "recordingStatusLabel";
            recordingStatusLabel.Size = new System.Drawing.Size(100, 20);
            recordingStatusLabel.Text = "录制状态:";
            recordingStatusLabel.Font = new System.Drawing.Font("微软雅黑", 9F);
            recordingStatusLabel.ForeColor = System.Drawing.Color.FromArgb(51, 51, 51);
            controlPanel.Controls.Add(recordingStatusLabel);

            System.Windows.Forms.Panel recordingIndicator = new System.Windows.Forms.Panel();
            recordingIndicator.Location = new System.Drawing.Point(110, 70);
            recordingIndicator.Name = "recordingIndicator";
            recordingIndicator.Size = new System.Drawing.Size(12, 12);
            recordingIndicator.BackColor = System.Drawing.Color.Gray;
            recordingIndicator.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            recordingIndicator.Tag = "recordingIndicator";
            controlPanel.Controls.Add(recordingIndicator);

            // 添加推流状态指示灯
            System.Windows.Forms.Label streamingStatusLabel = new System.Windows.Forms.Label();
            streamingStatusLabel.Location = new System.Drawing.Point(10, 100);
            streamingStatusLabel.Name = "streamingStatusLabel";
            streamingStatusLabel.Size = new System.Drawing.Size(100, 20);
            streamingStatusLabel.Text = "推流状态:";
            streamingStatusLabel.Font = new System.Drawing.Font("微软雅黑", 9F);
            streamingStatusLabel.ForeColor = System.Drawing.Color.FromArgb(51, 51, 51);
            controlPanel.Controls.Add(streamingStatusLabel);

            System.Windows.Forms.Panel streamingIndicator = new System.Windows.Forms.Panel();
            streamingIndicator.Location = new System.Drawing.Point(110, 100);
            streamingIndicator.Name = "streamingIndicator";
            streamingIndicator.Size = new System.Drawing.Size(12, 12);
            streamingIndicator.BackColor = System.Drawing.Color.Gray;
            streamingIndicator.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            streamingIndicator.Tag = "streamingIndicator";
            controlPanel.Controls.Add(streamingIndicator);

            // 添加FPS显示
            System.Windows.Forms.Label fpsLabel = new System.Windows.Forms.Label();
            fpsLabel.Location = new System.Drawing.Point(10, 130);
            fpsLabel.Name = "fpsLabel";
            fpsLabel.Size = new System.Drawing.Size(100, 20);
            fpsLabel.Text = "帧率: 0 FPS";
            fpsLabel.Font = new System.Drawing.Font("微软雅黑", 9F);
            fpsLabel.ForeColor = System.Drawing.Color.FromArgb(51, 51, 51);
            controlPanel.Controls.Add(fpsLabel);

            // 添加公安提示信息
            System.Windows.Forms.Label policeInfoLabel = new System.Windows.Forms.Label();
            policeInfoLabel.Location = new System.Drawing.Point(10, 170);
            policeInfoLabel.Name = "policeInfoLabel";
            policeInfoLabel.Size = new System.Drawing.Size(180, 100);
            policeInfoLabel.Text = "公安系统安全提示:\n\n1. 本系统仅用于合法执法活动\n2. 视频数据受法律保护\n3. 严禁未授权访问和传播\n4. 操作需符合警务规范";
            policeInfoLabel.Font = new System.Drawing.Font("微软雅黑", 8.5F);
            policeInfoLabel.ForeColor = System.Drawing.Color.FromArgb(192, 57, 43);
            policeInfoLabel.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            policeInfoLabel.AutoSize = true;
            controlPanel.Controls.Add(policeInfoLabel);

            // pnlPreview - 视频预览区域
            this.pnlPreview = new System.Windows.Forms.Panel();
            this.pnlPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPreview.Location = new System.Drawing.Point(250, 115);
            this.pnlPreview.Name = "pnlPreview";
            this.pnlPreview.Size = new System.Drawing.Size(780, 430);
            this.pnlPreview.TabIndex = 6;
            this.pnlPreview.BackColor = System.Drawing.Color.Black;
            this.pnlPreview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            // 添加警戒线边框
            this.pnlPreview.Paint += (sender, e) =>
            {
                System.Windows.Forms.Panel p = sender as System.Windows.Forms.Panel;
                // 绘制警戒线风格边框
                using (System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.Yellow, 2))
                {
                    e.Graphics.DrawRectangle(pen, 5, 5, p.Width - 10, p.Height - 10);
                    // 添加对角线警戒线
                    e.Graphics.DrawLine(pen, 5, 5, p.Width - 10, p.Height - 10);
                    e.Graphics.DrawLine(pen, p.Width - 10, 5, 5, p.Height - 10);
                }
                // 添加监控中水印
                using (System.Drawing.Font font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold))
                using (System.Drawing.SolidBrush brush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(50, 255, 0, 0)))
                {
                    string watermark = "公安监控中...";
                    System.Drawing.SizeF size = e.Graphics.MeasureString(watermark, font);
                    e.Graphics.DrawString(watermark, font, brush, p.Width - size.Width - 20, p.Height - size.Height - 20);
                }
            };

            // statusStrip1 - 状态栏
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusStrip1.Location = new System.Drawing.Point(0, 560);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1050, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);

            // toolStripStatusLabel1 - 主状态标签
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(39, 17);
            this.toolStripStatusLabel1.Text = "就绪";
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {this.toolStripStatusLabel1});

            // 添加系统时间显示
            System.Windows.Forms.ToolStripStatusLabel timeLabel = new System.Windows.Forms.ToolStripStatusLabel();
            timeLabel.Name = "timeLabel";
            timeLabel.Size = new System.Drawing.Size(100, 17);
            timeLabel.Text = DateTime.Now.ToString("HH:mm:ss");
            timeLabel.Font = new System.Drawing.Font("微软雅黑", 9F);
            timeLabel.Spring = true;
            timeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.statusStrip1.Items.Add(timeLabel);
            // 更新时间的计时器
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += (sender, e) => timeLabel.Text = DateTime.Now.ToString("HH:mm:ss");
            timer.Start();

            // Form1 - 添加所有控件
            this.Controls.Add(toolPanel);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(controlPanel);
            this.Controls.Add(this.pnlPreview);
        }

        // 创建警戒线图案
        private System.Drawing.Image CreateWarningLinePattern()
        {
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(20, 4);
            using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bmp))
            {
                g.Clear(System.Drawing.Color.Red);
                g.FillRectangle(System.Drawing.Brushes.White, 0, 0, 10, 4);
            }
            return bmp;
        }

        // 绘制五角星
        private void DrawStar(System.Drawing.Graphics g, int x, int y, int radius, System.Drawing.Color color)
        {
            System.Drawing.PointF[] points = new System.Drawing.PointF[10];
            float outerRadius = radius;
            float innerRadius = radius * 0.45f;
            float angle = (float)(Math.PI / 2); // 从顶部开始
            float step = (float)(2 * Math.PI / 5);

            for (int i = 0; i < 10; i += 2)
            {
                points[i] = new System.Drawing.PointF(
                    x + outerRadius * (float)Math.Cos(angle),
                    y - outerRadius * (float)Math.Sin(angle));
                angle += step;
                points[i + 1] = new System.Drawing.PointF(
                    x + innerRadius * (float)Math.Cos(angle),
                    y - innerRadius * (float)Math.Sin(angle));
                angle += step;
            }

            using (System.Drawing.SolidBrush brush = new System.Drawing.SolidBrush(color))
            {
                g.FillPolygon(brush, points);
            }
        }
        }

        #endregion
    }

