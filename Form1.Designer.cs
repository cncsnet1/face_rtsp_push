namespace screen_window
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnSelectRegion;
        private System.Windows.Forms.Button btnFullScreen;
        private System.Windows.Forms.Button btnToggleStream;
        private System.Windows.Forms.Panel pnlPreview;
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            titleLabel = new Label();
            warningLine = new Panel();
            contextMenuStrip = new ContextMenuStrip(components);
            tsmiShow = new ToolStripMenuItem();
            tsmiExit = new ToolStripMenuItem();
            notifyIcon = new NotifyIcon(components);
            toolPanel = new Panel();
            policeLabel = new Label();
            buttonPanel = new FlowLayoutPanel();
            streamUrlPanel = new Panel();
            btnSelectRegion = new Button();
            button1 = new Button();
            btnFullScreen = new Button();
            btnToggleStream = new Button();
            policeLogo = new PictureBox();
            pnlPreview = new Panel();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            timeLabel = new ToolStripStatusLabel();
            contextMenuStrip.SuspendLayout();
            toolPanel.SuspendLayout();
            buttonPanel.SuspendLayout();
            streamUrlPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)policeLogo).BeginInit();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // titleLabel
            // 
            titleLabel.BackColor = Color.FromArgb(22, 53, 78);
            titleLabel.Dock = DockStyle.Top;
            titleLabel.Font = new Font("微软雅黑", 11F, FontStyle.Bold, GraphicsUnit.Point);
            titleLabel.ForeColor = Color.White;
            titleLabel.Location = new Point(0, 74);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(781, 30);
            titleLabel.TabIndex = 1;
            titleLabel.Text = "公安视频监控推流系统 - 专业版";
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // warningLine
            // 
            warningLine.BackgroundImageLayout = ImageLayout.Stretch;
            warningLine.Dock = DockStyle.Top;
            warningLine.Location = new Point(0, 70);
            warningLine.Name = "warningLine";
            warningLine.Size = new Size(781, 4);
            warningLine.TabIndex = 2;
            // 
            // contextMenuStrip
            // 
            contextMenuStrip.Items.AddRange(new ToolStripItem[] { tsmiShow, tsmiExit });
            contextMenuStrip.Name = "contextMenuStrip";
            contextMenuStrip.Size = new Size(125, 48);
            // 
            // tsmiShow
            // 
            tsmiShow.Name = "tsmiShow";
            tsmiShow.Size = new Size(124, 22);
            tsmiShow.Text = "显示窗口";
            tsmiShow.Click += tsmiShow_Click;
            // 
            // tsmiExit
            // 
            tsmiExit.Name = "tsmiExit";
            tsmiExit.Size = new Size(124, 22);
            tsmiExit.Text = "退出";
            tsmiExit.Click += tsmiExit_Click;
            // 
            // notifyIcon
            // 
            notifyIcon.ContextMenuStrip = contextMenuStrip;
            notifyIcon.Text = "公安视频监控推流系统";
            notifyIcon.Visible = true;
            notifyIcon.DoubleClick += notifyIcon_DoubleClick;
            // 
            // toolPanel
            // 
            toolPanel.BackColor = Color.FromArgb(33, 73, 112);
            toolPanel.Controls.Add(policeLabel);
            toolPanel.Controls.Add(buttonPanel);
            toolPanel.Dock = DockStyle.Top;
            toolPanel.Location = new Point(0, 0);
            toolPanel.Name = "toolPanel";
            toolPanel.Padding = new Padding(20, 10, 20, 10);
            toolPanel.Size = new Size(781, 70);
            toolPanel.TabIndex = 3;
            // 
            // policeLabel
            // 
            policeLabel.Font = new Font("微软雅黑", 10F, FontStyle.Bold, GraphicsUnit.Point);
            policeLabel.ForeColor = Color.White;
            policeLabel.Location = new Point(8, 15);
            policeLabel.Name = "policeLabel";
            policeLabel.Size = new Size(100, 35);
            policeLabel.TabIndex = 0;
            policeLabel.Text = "公安系统专用";
            policeLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // buttonPanel
            // 
            buttonPanel.AutoSize = true;
            buttonPanel.BackColor = Color.Transparent;
            buttonPanel.Controls.Add(streamUrlPanel);
            buttonPanel.Controls.Add(btnToggleStream);
            buttonPanel.Location = new Point(118, 5);
            buttonPanel.Name = "buttonPanel";
            buttonPanel.Padding = new Padding(5);
            buttonPanel.Size = new Size(663, 61);
            buttonPanel.TabIndex = 1;
            buttonPanel.WrapContents = false;
            // 
            // streamUrlPanel
            // 
            streamUrlPanel.BackColor = Color.Transparent;
            streamUrlPanel.Controls.Add(btnSelectRegion);
            streamUrlPanel.Controls.Add(button1);
            streamUrlPanel.Controls.Add(btnFullScreen);
            streamUrlPanel.Location = new Point(8, 8);
            streamUrlPanel.Name = "streamUrlPanel";
            streamUrlPanel.Size = new Size(431, 45);
            streamUrlPanel.TabIndex = 4;
            // 
            // btnSelectRegion
            // 
            btnSelectRegion.BackColor = Color.FromArgb(52, 152, 219);
            btnSelectRegion.Cursor = Cursors.Hand;
            btnSelectRegion.FlatAppearance.BorderSize = 0;
            btnSelectRegion.FlatStyle = FlatStyle.Flat;
            btnSelectRegion.Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnSelectRegion.ForeColor = Color.White;
            btnSelectRegion.Location = new Point(29, 7);
            btnSelectRegion.Name = "btnSelectRegion";
            btnSelectRegion.Size = new Size(100, 27);
            btnSelectRegion.TabIndex = 1;
            btnSelectRegion.Text = "选择区域";
            btnSelectRegion.UseVisualStyleBackColor = false;
            btnSelectRegion.Click += btnSelectRegion_Click;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(192, 192, 0);
            button1.Cursor = Cursors.Hand;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point);
            button1.ForeColor = Color.White;
            button1.Location = new Point(328, 7);
            button1.Name = "button1";
            button1.Size = new Size(100, 27);
            button1.TabIndex = 2;
            button1.Text = "推流地址设置";
            button1.UseVisualStyleBackColor = false;
            // 
            // btnFullScreen
            // 
            btnFullScreen.BackColor = Color.FromArgb(155, 89, 182);
            btnFullScreen.Cursor = Cursors.Hand;
            btnFullScreen.FlatAppearance.BorderSize = 0;
            btnFullScreen.FlatStyle = FlatStyle.Flat;
            btnFullScreen.Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnFullScreen.ForeColor = Color.White;
            btnFullScreen.Location = new Point(135, 7);
            btnFullScreen.Name = "btnFullScreen";
            btnFullScreen.Size = new Size(100, 27);
            btnFullScreen.TabIndex = 2;
            btnFullScreen.Text = "全屏录制";
            btnFullScreen.UseVisualStyleBackColor = false;
            btnFullScreen.Click += btnFullScreen_Click;
            // 
            // btnToggleStream
            // 
            btnToggleStream.BackColor = Color.FromArgb(231, 76, 60);
            btnToggleStream.Cursor = Cursors.Hand;
            btnToggleStream.FlatAppearance.BorderSize = 0;
            btnToggleStream.FlatStyle = FlatStyle.Flat;
            btnToggleStream.Font = new Font("微软雅黑", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnToggleStream.ForeColor = Color.White;
            btnToggleStream.Location = new Point(445, 8);
            btnToggleStream.Name = "btnToggleStream";
            btnToggleStream.Size = new Size(100, 45);
            btnToggleStream.TabIndex = 5;
            btnToggleStream.Text = "开始推流";
            btnToggleStream.UseVisualStyleBackColor = false;
            btnToggleStream.Click += btnToggleStream_Click;
            // 
            // policeLogo
            // 
            policeLogo.Location = new Point(15, 10);
            policeLogo.Name = "policeLogo";
            policeLogo.Size = new Size(45, 45);
            policeLogo.SizeMode = PictureBoxSizeMode.StretchImage;
            policeLogo.TabIndex = 0;
            policeLogo.TabStop = false;
            // 
            // pnlPreview
            // 
            pnlPreview.BackColor = Color.Black;
            pnlPreview.BackgroundImageLayout = ImageLayout.Center;
            pnlPreview.BorderStyle = BorderStyle.FixedSingle;
            pnlPreview.Location = new Point(0, 115);
            pnlPreview.Name = "pnlPreview";
            pnlPreview.Size = new Size(770, 317);
            pnlPreview.TabIndex = 6;
            // 
            // statusStrip1
            // 
            statusStrip1.BackColor = Color.FromArgb(236, 240, 241);
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1, timeLabel });
            statusStrip1.Location = new Point(0, 436);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(781, 22);
            statusStrip1.TabIndex = 7;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point);
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(32, 17);
            toolStripStatusLabel1.Text = "就绪";
            // 
            // timeLabel
            // 
            timeLabel.Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point);
            timeLabel.Name = "timeLabel";
            timeLabel.Size = new Size(734, 17);
            timeLabel.Spring = true;
            timeLabel.Text = "15:07:18";
            timeLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 242, 245);
            ClientSize = new Size(781, 458);
            Controls.Add(titleLabel);
            Controls.Add(warningLine);
            Controls.Add(toolPanel);
            Controls.Add(statusStrip1);
            Controls.Add(pnlPreview);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "公安视频监控推流系统";
            contextMenuStrip.ResumeLayout(false);
            toolPanel.ResumeLayout(false);
            toolPanel.PerformLayout();
            buttonPanel.ResumeLayout(false);
            streamUrlPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)policeLogo).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
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
        private Label titleLabel;
        private Panel warningLine;
        private Panel toolPanel;
        private Label policeLabel;
        private FlowLayoutPanel buttonPanel;
        private Panel streamUrlPanel;
        private PictureBox policeLogo;
        private ToolStripStatusLabel timeLabel;
        private Button button1;
    }

        #endregion
    }

