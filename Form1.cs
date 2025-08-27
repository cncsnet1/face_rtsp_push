using System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;


namespace screen_window
{
    public partial class Form1 : Form
    {
        private Process crimAlertProcess = null;

        private bool isCapturing = false;
        private bool isStreaming = false;
        private bool isSelectingRegion = false;
        private Rectangle selectedRegion = Rectangle.Empty;
        private Thread captureThread;
        private Thread streamThread;
        private CancellationTokenSource cts;
        private Icon normalIcon;
        private Icon recordingIcon;
        private string tempImagePath = Path.Combine(Path.GetTempPath(), "screen_capture.jpg");
        private FloatingBorderForm floatingBorder = null;
        private DateTime lastCaptureTime = DateTime.MinValue; // 上次捕获时间
        private const int TARGET_FPS = 25; // 目标帧率
        private const int FRAME_INTERVAL_MS = 1000 / TARGET_FPS; // 帧间隔
        private bool useAlternativeRtsp = false; // 是否使用备用RTSP方案
        private int reconnectAttempts = 0; // 当前重连次数

        public Form1()
        {
            InitializeComponent();
            
            // 启用窗体双缓冲
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer | ControlStyles.ResizeRedraw, true);
            this.UpdateStyles();
            
            // 为预览面板启用双缓冲
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, pnlPreview, new object[] { true });
            
            // 注册Resize事件用于最小化到托盘
            this.Resize += Form1_Resize;
            // 注册FormClosing事件用于清理悬浮边框
            this.FormClosing += Form1_FormClosing;
            // 初始化图标
            normalIcon = SystemIcons.Shield;
            recordingIcon = CreateRecordingIcon();
            ManageCrimAlertProcess();

        }
        private void ManageCrimAlertProcess()
        {
            // 结束现有crim_alert进程
            foreach (var proc in Process.GetProcessesByName("crim_alert"))
            {
                try
                {
                    proc.Kill();
                    proc.WaitForExit();
                }
                catch { }
            }

            // 启动新实例（假设路径）
            string crimAlertPath = Path.Combine(Application.StartupPath, "crim_alert.exe");
            if (File.Exists(crimAlertPath))
            {
                crimAlertProcess = new Process();
                crimAlertProcess.StartInfo.FileName = crimAlertPath;
                crimAlertProcess.StartInfo.UseShellExecute = true;
                crimAlertProcess.Start();
            }
            else
            {
              
                MessageBox.Show("找不到 crim_alert.exe，请检查路径。");
                Process.GetCurrentProcess().Kill();
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 停止录制
            if (isCapturing)
            {
                StopScreenCapture();
            }
            
            // 清理预览面板图像
            if (pnlPreview.BackgroundImage != null)
            {
                pnlPreview.BackgroundImage.Dispose();
                pnlPreview.BackgroundImage = null;
            }
            
            // 清理悬浮边框
            HideFloatingBorder();
            if (crimAlertProcess != null && !crimAlertProcess.HasExited)
            {
                try
                {
                    crimAlertProcess.Kill();
                    crimAlertProcess.WaitForExit();
                }
                catch { }
            }
        }

       
        private void btnSelectRegion_Click(object sender, EventArgs e)
        {
            if (!isCapturing && !isStreaming)
            {
                StartRegionSelection();
            }
        }

        private void btnFullScreen_Click(object sender, EventArgs e)
        {
            if (isCapturing)
            {
                MessageBox.Show("请先停止录制再切换模式", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 清除选定区域，设置为全屏模式
            selectedRegion = Rectangle.Empty;
            
            // 隐藏悬浮边框
            HideFloatingBorder();
            
            toolStripStatusLabel1.Text = "已设置为全屏录制模式";
        }

        private void btnToggleStream_Click(object sender, EventArgs e)
        {
          
            if (!isStreaming )
            {

                StartScreenCapture();
                StartStreaming();
                btnToggleStream.Text = "停止推流";
                btnToggleStream.BackColor = Color.FromArgb(39, 174, 96);
            }
            else if (isStreaming)
            {
                StopScreenCapture();
                StopStreaming();
                btnToggleStream.Text = "开始推流";
                btnToggleStream.BackColor = Color.FromArgb(231, 76, 60);
            }
        }

        private void StartScreenCapture()
        {
            isCapturing = true;
            btnSelectRegion.Enabled = false;
            toolStripStatusLabel1.Text = "正在抓取屏幕...";

            // 更新托盘状态
            notifyIcon.Text = "公安视频监控推流工具 - 正在录制";
            notifyIcon.Icon = recordingIcon;
            notifyIcon.ShowBalloonTip(3000, "录制开始", "屏幕录制已开始", ToolTipIcon.Info);

            // 隐藏悬浮边框以避免录制到边框
            if (floatingBorder != null)
            {
                floatingBorder.UpdateRecordingState(true);
                floatingBorder.Hide();
            }

            cts = new CancellationTokenSource();
            captureThread = new Thread(() => CaptureScreenLoop(cts.Token))
            {
                IsBackground = true
            };
            captureThread.Start();
        }

        private void StopScreenCapture()
        {
            if (isStreaming)
            {
                StopStreaming();
            }

            isCapturing = false;
            btnSelectRegion.Enabled = true;
            toolStripStatusLabel1.Text = "就绪";

            // 恢复托盘状态
            notifyIcon.Text = "公安视频监控推流工具";
            notifyIcon.Icon = normalIcon;
            notifyIcon.ShowBalloonTip(3000, "录制停止", "屏幕录制已停止", ToolTipIcon.Info);

            // 显示悬浮边框并更新状态
            if (floatingBorder != null)
            {
                floatingBorder.UpdateRecordingState(false);
                floatingBorder.Show();
            }

            cts?.Cancel();
            captureThread?.Join(1000);
        }

        private void StartRegionSelection()
        {
            isSelectingRegion = true;
            this.Hide();
            Thread.Sleep(500); // 等待窗口隐藏

            using (var regionForm = new RegionSelectionForm())
            {
                if (regionForm.ShowDialog() == DialogResult.OK)
                {
                    selectedRegion = regionForm.SelectedRegion;
                    
                    // 验证并调整区域尺寸
                    int adjustedWidth = selectedRegion.Width;
                    int adjustedHeight = selectedRegion.Height;
                    
                    // 强制宽度必须是4的倍数（H.264编码要求）
                    adjustedWidth = (adjustedWidth / 4) * 4;
                    // 确保高度是偶数
                    if (adjustedHeight % 2 != 0) adjustedHeight--;
                    
                    // 确保最小分辨率
                    if (adjustedWidth < 32) adjustedWidth = 32;
                    if (adjustedHeight < 32) adjustedHeight = 32;
                    
                    // 如果尺寸被调整，更新selectedRegion
                    if (adjustedWidth != selectedRegion.Width || adjustedHeight != selectedRegion.Height)
                    {
                        selectedRegion = new Rectangle(selectedRegion.X, selectedRegion.Y, adjustedWidth, adjustedHeight);
                        toolStripStatusLabel1.Text = $"已选择区域: {selectedRegion.Width}x{selectedRegion.Height} (宽度已调整为4的倍数)";
                    }
                    else
                    {
                        toolStripStatusLabel1.Text = $"已选择区域: {selectedRegion.Width}x{selectedRegion.Height}";
                    }
                    
                    // 显示悬浮边框
                    ShowFloatingBorder();
                }
            }

            this.Show();
            isSelectingRegion = false;
        }

        private void StartStreaming()
        {
            if (string.IsNullOrEmpty(txtStreamUrl.Text))
            {
                MessageBox.Show("请输入推流地址");
                return;
            }

            isStreaming = true;
            toolStripStatusLabel1.Text = "正在推流...";
            notifyIcon.ShowBalloonTip(3000, "推流开始", "视频推流已开始", ToolTipIcon.Info);

            // 重置RTSP方案选择，从标准方案开始
            useAlternativeRtsp = false;
            reconnectAttempts = 0;

            cts = new CancellationTokenSource();
            streamThread = new Thread(() => StreamLoop(cts.Token))
            {
                IsBackground = true
            };
            streamThread.Start();
        }

        private void StopStreaming()
        {
            isStreaming = false;
            toolStripStatusLabel1.Text = "推流已停止";
            notifyIcon.ShowBalloonTip(3000, "推流停止", "视频推流已停止", ToolTipIcon.Info);

            cts?.Cancel();
            streamThread?.Join(1000);
        }

        private void CaptureScreenLoop(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    DateTime currentTime = DateTime.Now;
                    
                    // 精确控制帧率，避免过于频繁的捕获
                    if ((currentTime - lastCaptureTime).TotalMilliseconds >= FRAME_INTERVAL_MS)
                    {
                        lastCaptureTime = currentTime;
                        CaptureScreen();
                    }
                    
                    // 短暂休眠，减少CPU占用
                    Thread.Sleep(5);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"抓取屏幕出错: {ex.Message}");
                    break;
                }
            }
        }

        private void StreamLoop(CancellationToken cancellationToken)
        {
            int reconnectDelay = 1000; // 初始重连延迟(毫秒)
            const int MAX_DELAY = 30000; // 最大延迟30秒
            const int DELAY_MULTIPLIER = 2; // 延迟倍增因子
        
            while (!cancellationToken.IsCancellationRequested && isCapturing)
            {
                Process? ffmpegProcess = null;
                try
                {
                    string streamUrl = txtStreamUrl.Text;
                    if (string.IsNullOrEmpty(streamUrl))
                    {
                        this.Invoke((System.Windows.Forms.MethodInvoker)delegate
                        {
                            MessageBox.Show("请输入推流地址");
                            StopStreaming();
                        });
                        return;
                    }

                    // 查找ffmpeg.exe路径
                    string ffmpegPath = FindFFmpegPath();
                    if (string.IsNullOrEmpty(ffmpegPath))
                    {
                        this.Invoke((System.Windows.Forms.MethodInvoker)delegate
                        {
                            MessageBox.Show("找不到ffmpeg.exe，请确保已正确安装FFmpeg");
                            StopStreaming();
                        });
                        return;
                    }

                    // 构建ffmpeg命令参数 - 根据重试次数选择方案
                    string arguments = BuildFFmpegArguments(streamUrl, useAlternativeRtsp);
                    
                    // 记录推流信息
                    Debug.WriteLine($"开始RTSP TCP推流到: {streamUrl}");

                    // 启动ffmpeg进程
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        FileName = ffmpegPath,
                        Arguments = arguments,
                        UseShellExecute = false,
                        RedirectStandardInput = true,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        CreateNoWindow = true
                    };

                    ffmpegProcess = new Process { StartInfo = startInfo };

                    // 设置输出数据接收事件
                    ffmpegProcess.OutputDataReceived += (sender, e) =>
                    {
                        if (!string.IsNullOrEmpty(e.Data))
                        {
                            Debug.WriteLine($"FFmpeg输出: {e.Data}");
                        }
                    };

                    ffmpegProcess.ErrorDataReceived += (sender, e) =>
                    {
                        if (!string.IsNullOrEmpty(e.Data))
                        {
                            Debug.WriteLine($"FFmpeg错误: {e.Data}");
                        }
                    };

                    // 记录录制信息
                    Rectangle bounds = selectedRegion.IsEmpty ? Screen.PrimaryScreen.Bounds : selectedRegion;
                    int recordWidth = bounds.Width;
                    int recordHeight = bounds.Height;
                    recordWidth = (recordWidth / 4) * 4; // 强制宽度4的倍数
                    if (recordHeight % 2 != 0) recordHeight--; // 确保高度偶数
                    if (recordWidth < 32) recordWidth = 32;
                    if (recordHeight < 32) recordHeight = 32;

                    Debug.WriteLine($"录制模式: {(selectedRegion.IsEmpty ? "全屏" : "区域")}");
                    Debug.WriteLine($"原始尺寸: {bounds.Width}x{bounds.Height}");
                    Debug.WriteLine($"调整后尺寸: {recordWidth}x{recordHeight}");
                    Debug.WriteLine($"区域位置: X={bounds.X}, Y={bounds.Y}");
                    Debug.WriteLine($"启动FFmpeg，参数: {arguments}");

                    ffmpegProcess.Start();

                    // 开始异步读取输出
                    ffmpegProcess.BeginOutputReadLine();
                    ffmpegProcess.BeginErrorReadLine();

                    Debug.WriteLine($"FFmpeg进程已启动，PID: {ffmpegProcess.Id}");

                    // 推流循环 - 通过管道向ffmpeg发送图像数据
                    Debug.WriteLine("开始推流循环");
                    int frameCount = 0;
                    while (!cancellationToken.IsCancellationRequested && isCapturing && !ffmpegProcess.HasExited)
                    {
                        try
                        {
                            // 捕获屏幕
                            Rectangle captureBounds = selectedRegion.IsEmpty ? Screen.PrimaryScreen.Bounds : selectedRegion;

                            // 强制宽度必须是4的倍数，高度是偶数（H.264编码要求）
                            int width = captureBounds.Width;
                            int height = captureBounds.Height;
                            width = (width / 4) * 4; // 强制4的倍数
                            if (height % 2 != 0) height--; // 确保偶数

                            // 确保最小分辨率
                            if (width < 32) width = 32;
                            if (height < 32) height = 32;

                            using (Bitmap bitmap = new Bitmap(width, height))
                            using (Graphics graphics = Graphics.FromImage(bitmap))
                            {
                                graphics.CopyFromScreen(captureBounds.Location, Point.Empty, new Size(width, height));

                                // 将图像转换为原始RGB数据并发送给ffmpeg
                                BitmapData bitmapData = bitmap.LockBits(
                                    new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                                    ImageLockMode.ReadOnly,
                                    PixelFormat.Format24bppRgb);

                                try
                                {
                                    // 计算图像数据大小
                                    int stride = bitmapData.Stride;
                                    int bitmapHeight = bitmap.Height;
                                    byte[] imageData = new byte[stride * bitmapHeight];

                                    // 复制图像数据
                                    System.Runtime.InteropServices.Marshal.Copy(
                                        bitmapData.Scan0, imageData, 0, imageData.Length);

                                    // 发送数据到ffmpeg的标准输入
                                    ffmpegProcess.StandardInput.BaseStream.Write(imageData, 0, imageData.Length);
                                    ffmpegProcess.StandardInput.BaseStream.Flush();

                                    frameCount++;
                                    if (frameCount % 100 == 0) // 每100帧输出一次日志
                                    {
                                        Debug.WriteLine($"已发送 {frameCount} 帧数据到FFmpeg");
                                    }
                                }
                                finally
                                {
                                    bitmap.UnlockBits(bitmapData);
                                }
                            }

                            Thread.Sleep(40); // 控制帧率约25fps
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine($"捕获屏幕异常: {ex.Message}");
                            Debug.WriteLine($"异常堆栈: {ex.StackTrace}");
                            break;
                        }
                    }

                    Debug.WriteLine($"推流循环结束，总共发送了 {frameCount} 帧数据");
                }
                catch (Exception ex)
                {
                    this.Invoke((System.Windows.Forms.MethodInvoker)delegate
                    {
                        MessageBox.Show($"推流异常: {ex.Message}");
                        StopStreaming();
                    });
                }
                finally
                {
                    // 清理ffmpeg进程
                    try
                    {
                        if (ffmpegProcess != null)
                        {
                            if (!ffmpegProcess.HasExited)
                            {
                                Debug.WriteLine($"正在终止FFmpeg进程，PID:{ffmpegProcess.Id}");
                                ffmpegProcess.Kill();
                                ffmpegProcess.WaitForExit(3000);
                            }

                            Debug.WriteLine($"FFmpeg进程已结束，退出代码:{ffmpegProcess.ExitCode}");
                            ffmpegProcess.Dispose();
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"清理ffmpeg进程异常:{ex.Message}");
                    }
                }

                // 无限重连逻辑 with 指数退避
                if (isCapturing && !cancellationToken.IsCancellationRequested)
                {
                    Debug.WriteLine($"推流断开，正在等待 {reconnectDelay/1000} 秒后重连...");
                    this.Invoke((System.Windows.Forms.MethodInvoker)delegate
                    {
                        toolStripStatusLabel1.Text = $"推流断开，等待 {reconnectDelay/1000}s 后重连...";
                    });
                    Thread.Sleep(reconnectDelay);
                
                    // 增加延迟以优化资源
                    reconnectDelay = Math.Min(reconnectDelay * DELAY_MULTIPLIER, MAX_DELAY);
                }
            }
        }

        private string FindFFmpegPath()
        {
            // 首先检查应用程序目录下的ffmpeg文件夹
            string appDir = AppDomain.CurrentDomain.BaseDirectory;
            string localFFmpegPath = Path.Combine(appDir, "ffmpeg", "ffmpeg.exe");
            if (File.Exists(localFFmpegPath))
            {
                return localFFmpegPath;
            }

            // 检查系统PATH中的ffmpeg
            string[] paths = Environment.GetEnvironmentVariable("PATH")?.Split(';') ?? new string[0];
            foreach (string path in paths)
            {
                string ffmpegPath = Path.Combine(path, "ffmpeg.exe");
                if (File.Exists(ffmpegPath))
                {
                    return ffmpegPath;
                }
            }

            return string.Empty;
        }

        private string BuildFFmpegArguments(string streamUrl, bool useAlternativeRtsp = false)
        {
            Rectangle bounds = selectedRegion.IsEmpty ? Screen.PrimaryScreen.Bounds : selectedRegion;
            
            // 强制宽度必须是4的倍数，高度是偶数（H.264编码要求）
            int width = bounds.Width;
            int height = bounds.Height;
            width = (width / 4) * 4; // 强制4的倍数
            if (height % 2 != 0) height--; // 确保偶数
            
            // 确保最小分辨率
            if (width < 32) width = 32;
            if (height < 32) height = 32;
            
            StringBuilder args = new StringBuilder();
            
            // 输入参数 - 从标准输入读取原始视频数据
            args.Append("-f rawvideo ");
            args.Append("-pix_fmt bgr24 "); // Windows位图格式是BGR
            args.Append($"-s {width}x{height} ");
            args.Append("-r 25 ");
            args.Append("-i - ");
            
            // 统一使用RTMP推流配置
            args.Append("-c:v libx264 ");
            args.Append("-preset ultrafast ");
            args.Append("-tune zerolatency ");
            args.Append("-profile:v baseline ");
            args.Append("-level 3.0 ");
            args.Append("-pix_fmt yuv420p ");
            args.Append("-b:v 1000k ");
            args.Append("-g 25 ");
            args.Append("-bf 0 ");
            args.Append("-an "); // 禁用音频
            
            // RTSP TCP推流配置
            args.Append("-f rtsp ");
            args.Append("-rtsp_transport tcp ");
            args.Append($"\"{streamUrl}\"");
            
            return args.ToString();
        }
        

        private void CaptureScreen()
        {
            try
            {
                Rectangle bounds;

                if (selectedRegion.IsEmpty)
                {
                    // 抓取整个屏幕
                    bounds = Screen.PrimaryScreen.Bounds;
                }
                else
                {
                    // 抓取选定区域
                    bounds = selectedRegion;
                }

                using (var bitmap = new Bitmap(bounds.Width, bounds.Height))
                {
                    using (var graphics = Graphics.FromImage(bitmap))
                    {
                        graphics.CopyFromScreen(bounds.X, bounds.Y, 0, 0, bounds.Size);
                    }

                    // 保存到临时文件（用于推流）
                    bitmap.Save(tempImagePath, ImageFormat.Jpeg);

                    // 简化的UI更新逻辑
                    if (this.InvokeRequired)
                    {
                        this.Invoke((System.Windows.Forms.MethodInvoker)delegate
                        {
                            UpdatePreview(bitmap);
                        });
                    }
                    else
                    {
                        UpdatePreview(bitmap);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"屏幕捕获错误: {ex.Message}");
            }
        }

        private void UpdatePreview(Bitmap originalBitmap)
        {
            try
            {
                // 释放之前的图像
                var oldImage = pnlPreview.BackgroundImage;
                oldImage?.Dispose();

                // 计算缩放比例
                Size panelSize = pnlPreview.ClientSize;
                if (panelSize.Width <= 0 || panelSize.Height <= 0)
                    return;

                float scaleX = (float)panelSize.Width / originalBitmap.Width;
                float scaleY = (float)panelSize.Height / originalBitmap.Height;
                float scale = Math.Min(scaleX, scaleY);

                int newWidth = (int)(originalBitmap.Width * scale);
                int newHeight = (int)(originalBitmap.Height * scale);

                // 创建缩放后的图像
                var scaledBitmap = new Bitmap(newWidth, newHeight);
                using (var g = Graphics.FromImage(scaledBitmap))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
                    g.DrawImage(originalBitmap, 0, 0, newWidth, newHeight);
                }

                // 更新预览面板
                pnlPreview.BackgroundImage = scaledBitmap;
                pnlPreview.BackgroundImageLayout = ImageLayout.Center;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"预览更新错误: {ex.Message}");
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // 确认退出
            DialogResult result = MessageBox.Show("确定要退出程序吗？", "确认退出", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }

            StopScreenCapture();
            if (File.Exists(tempImagePath))
            {
                try
                {
                    File.Delete(tempImagePath);
                }
                catch { }
            }
            notifyIcon.Visible = false;
            base.OnFormClosing(e);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            // 最小化到托盘
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                notifyIcon.ShowBalloonTip(3000, "程序最小化", "程序已最小化到系统托盘", ToolTipIcon.Info);
                
                // 如果有选择的区域且悬浮边框存在，确保悬浮边框仍然显示
                if (!selectedRegion.IsEmpty && floatingBorder != null)
                {
                    floatingBorder.TopMost = true;
                }
            }
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            
            // 确保悬浮边框在主窗口恢复时保持正确状态
            if (!selectedRegion.IsEmpty && floatingBorder != null)
            {
                floatingBorder.TopMost = true;
                floatingBorder.BringToFront();
            }
        }

        private void tsmiShow_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            
            // 确保悬浮边框在主窗口恢复时保持正确状态
            if (!selectedRegion.IsEmpty && floatingBorder != null)
            {
                floatingBorder.TopMost = true;
                floatingBorder.BringToFront();
            }
        }

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private Icon CreateRecordingIcon()
        {
            // 创建一个带有红色圆点的录制图标
            Bitmap bmp = new Bitmap(32, 32);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                // 绘制基础图标
                g.DrawIcon(normalIcon, 0, 0);
                // 绘制红色录制点
                using (Brush brush = new SolidBrush(Color.Red))
                {
                    g.FillEllipse(brush, 20, 20, 10, 10);
                }
            }
            return Icon.FromHandle(bmp.GetHicon());
        }

        private void ShowFloatingBorder()
        {
            if (!selectedRegion.IsEmpty)
            {
                HideFloatingBorder(); // 先隐藏之前的边框
                
                floatingBorder = new FloatingBorderForm(selectedRegion, this);
                floatingBorder.StartStopClicked += FloatingBorder_StartStopClicked;
                floatingBorder.MinimizeClicked += FloatingBorder_MinimizeClicked;
                floatingBorder.CloseClicked += FloatingBorder_CloseClicked;
                floatingBorder.Show();
            }
        }

        private void HideFloatingBorder()
        {
            if (floatingBorder != null)
            {
                floatingBorder.StartStopClicked -= FloatingBorder_StartStopClicked;
                floatingBorder.MinimizeClicked -= FloatingBorder_MinimizeClicked;
                floatingBorder.CloseClicked -= FloatingBorder_CloseClicked;
                floatingBorder.Close();
                floatingBorder.Dispose();
                floatingBorder = null;
            }
        }

        private void FloatingBorder_StartStopClicked(object sender, EventArgs e)
        {
            // 触发主窗口的录制开关
            btnToggleStream_Click(sender, e);
        }

        private void FloatingBorder_MinimizeClicked(object sender, EventArgs e)
        {
            // 最小化主窗口到托盘
            this.WindowState = FormWindowState.Minimized;
        }

        private void FloatingBorder_CloseClicked(object sender, EventArgs e)
        {
            // 停止录制并隐藏悬浮边框
            if (isCapturing)
            {
                StopScreenCapture();
            }
            HideFloatingBorder();
            selectedRegion = Rectangle.Empty;
            toolStripStatusLabel1.Text = "就绪";
        }
    }

    // 区域选择表单
    public class RegionSelectionForm : Form
    {
        private bool isSelecting = false;
        private Point startPoint;
        private Rectangle selectionRect;

        public Rectangle SelectedRegion { get; private set; }

        public RegionSelectionForm()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.Black;
            this.Opacity = 0.5;
            this.Cursor = Cursors.Cross;
            this.MouseDown += RegionSelectionForm_MouseDown;
            this.MouseMove += RegionSelectionForm_MouseMove;
            this.MouseUp += RegionSelectionForm_MouseUp;
            this.KeyDown += RegionSelectionForm_KeyDown;
        }

        private void RegionSelectionForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isSelecting = true;
                startPoint = e.Location;
                selectionRect = new Rectangle(e.Location, Size.Empty);
                this.Invalidate();
            }
        }

        private void RegionSelectionForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (isSelecting)
            {
                // 更新选择矩形
                int x = Math.Min(startPoint.X, e.X);
                int y = Math.Min(startPoint.Y, e.Y);
                int width = Math.Abs(startPoint.X - e.X);
                int height = Math.Abs(startPoint.Y - e.Y);

                selectionRect = new Rectangle(x, y, width, height);
                this.Invalidate();
            }
        }

        private void RegionSelectionForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (isSelecting)
            {
                isSelecting = false;
                if (selectionRect.Width > 0 && selectionRect.Height > 0)
                {
                    SelectedRegion = selectionRect;
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    this.DialogResult = DialogResult.Cancel;
                }
                this.Close();
            }
        }

        private void RegionSelectionForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (isSelecting)
            {
                using (var pen = new Pen(Color.Red, 2))
                {
                    e.Graphics.DrawRectangle(pen, selectionRect);
                    // 填充选择区域的透明度
                    using (var brush = new SolidBrush(Color.FromArgb(50, 255, 0, 0)))
                    {
                        e.Graphics.FillRectangle(brush, selectionRect);
                    }
                }
            }
        }
       
    }
}
