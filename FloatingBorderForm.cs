using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace screen_window
{
    public partial class FloatingBorderForm : Form
    {
        private Rectangle recordingRegion;
        private Panel controlPanel;
        private Button btnStartStop;
        private Button btnMinimize;
        private Button btnClose;
        private bool isRecording = false;
        private Form1 parentForm;
        
        // Windows API for making window stay on top and transparent to mouse clicks
        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        
        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        
        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_LAYERED = 0x80000;
        private const int WS_EX_TRANSPARENT = 0x20;
        private const int WS_EX_TOPMOST = 0x8;
        
        public event EventHandler StartStopClicked;
        public event EventHandler MinimizeClicked;
        public event EventHandler CloseClicked;
        
        public FloatingBorderForm(Rectangle region, Form1 parent)
        {
            recordingRegion = region;
            parentForm = parent;
            InitializeComponent();
            SetupWindow();
            CreateControls();
        }
        
        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            // Form properties
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.Lime; // 用于透明度键控
            this.TransparencyKey = Color.Lime;
            this.FormBorderStyle = FormBorderStyle.None;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.StartPosition = FormStartPosition.Manual;
            
            // 启用双缓冲以减少闪烁
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | 
                         ControlStyles.UserPaint | 
                         ControlStyles.DoubleBuffer | 
                         ControlStyles.ResizeRedraw, true);
            
            // 设置窗口位置和大小
            this.Location = recordingRegion.Location;
            this.Size = recordingRegion.Size;
            
            this.ResumeLayout(false);
        }
        
        private void SetupWindow()
        {
            // 使窗口保持在最顶层
            this.TopMost = true;
            
            // 设置窗口样式为分层窗口，但不设置透明，保持按钮可点击
            int exStyle = GetWindowLong(this.Handle, GWL_EXSTYLE);
            SetWindowLong(this.Handle, GWL_EXSTYLE, exStyle | WS_EX_LAYERED | WS_EX_TOPMOST);
        }
        
        private void CreateControls()
        {
            // 创建控制面板
            controlPanel = new Panel()
            {
                Size = new Size(200, 40),
                BackColor = Color.FromArgb(200, 52, 73, 94), // 半透明深色背景
                Location = new Point(recordingRegion.Width - 210, -45), // 位于边框上方
                BorderStyle = BorderStyle.FixedSingle
            };
            
            // 开始/停止按钮
            btnStartStop = new Button()
            {
                Text = "开始录制",
                Size = new Size(60, 30),
                Location = new Point(5, 5),
                BackColor = Color.FromArgb(39, 174, 96),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("微软雅黑", 8F, FontStyle.Bold)
            };
            btnStartStop.FlatAppearance.BorderSize = 0;
            btnStartStop.Click += BtnStartStop_Click;
            
            // 最小化按钮
            btnMinimize = new Button()
            {
                Text = "最小化",
                Size = new Size(60, 30),
                Location = new Point(70, 5),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("微软雅黑", 8F)
            };
            btnMinimize.FlatAppearance.BorderSize = 0;
            btnMinimize.Click += BtnMinimize_Click;
            
            // 关闭按钮
            btnClose = new Button()
            {
                Text = "关闭",
                Size = new Size(60, 30),
                Location = new Point(135, 5),
                BackColor = Color.FromArgb(231, 76, 60),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("微软雅黑", 8F)
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += BtnClose_Click;
            
            // 添加按钮到控制面板
            controlPanel.Controls.Add(btnStartStop);
            controlPanel.Controls.Add(btnMinimize);
            controlPanel.Controls.Add(btnClose);
            
            // 添加控制面板到窗体
            this.Controls.Add(controlPanel);
        }
        
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // 不绘制背景，避免闪烁
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            // 设置高质量渲染
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            
            // 先用透明键颜色填充整个区域，确保中间透明
            e.Graphics.Clear(this.TransparencyKey);
            
            // 绘制边框（只绘制边框线条，不填充内部）
            using (Pen borderPen = new Pen(Color.Red, 3))
            {
                // 绘制外边框
                Rectangle borderRect = new Rectangle(1, 1, this.Width - 3, this.Height - 3);
                e.Graphics.DrawRectangle(borderPen, borderRect);
                
                // 绘制内边框（虚线）
                using (Pen innerPen = new Pen(Color.Yellow, 1))
                {
                    innerPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    Rectangle innerRect = new Rectangle(4, 4, this.Width - 9, this.Height - 9);
                    e.Graphics.DrawRectangle(innerPen, innerRect);
                }
            }
            
            // 在左上角显示录制区域信息
            string regionInfo = $"{recordingRegion.Width} × {recordingRegion.Height}";
            using (Font font = new Font("微软雅黑", 10F, FontStyle.Bold))
            using (Brush textBrush = new SolidBrush(Color.White))
            using (Brush bgBrush = new SolidBrush(Color.FromArgb(180, 0, 0, 0)))
            {
                SizeF textSize = e.Graphics.MeasureString(regionInfo, font);
                Rectangle textRect = new Rectangle(8, 8, (int)textSize.Width + 8, (int)textSize.Height + 4);
                e.Graphics.FillRectangle(bgBrush, textRect);
                e.Graphics.DrawString(regionInfo, font, textBrush, 12, 10);
            }
        }
        
        private void BtnStartStop_Click(object sender, EventArgs e)
        {
            isRecording = !isRecording;
            btnStartStop.Text = isRecording ? "停止录制" : "开始录制";
            btnStartStop.BackColor = isRecording ? Color.FromArgb(231, 76, 60) : Color.FromArgb(39, 174, 96);
            StartStopClicked?.Invoke(this, EventArgs.Empty);
        }
        
        private void BtnMinimize_Click(object sender, EventArgs e)
        {
            MinimizeClicked?.Invoke(this, EventArgs.Empty);
        }
        
        private void BtnClose_Click(object sender, EventArgs e)
        {
            CloseClicked?.Invoke(this, EventArgs.Empty);
        }
        
        public void UpdateRecordingState(bool recording)
        {
            isRecording = recording;
            btnStartStop.Text = isRecording ? "停止录制" : "开始录制";
            btnStartStop.BackColor = isRecording ? Color.FromArgb(231, 76, 60) : Color.FromArgb(39, 174, 96);
        }
        
        public void UpdateRegion(Rectangle newRegion)
        {
            if (recordingRegion != newRegion)
            {
                recordingRegion = newRegion;
                this.Location = newRegion.Location;
                this.Size = newRegion.Size;
                
                // 更新控制面板位置
                controlPanel.Location = new Point(newRegion.Width - 210, -45);
                
                // 只在区域真正改变时才重绘
                this.Invalidate();
            }
        }
        
        protected override void SetVisibleCore(bool value)
        {
            base.SetVisibleCore(value);
            if (value)
            {
                this.TopMost = true;
            }
        }
        
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= WS_EX_TOPMOST;
                return cp;
            }
        }
    }
}