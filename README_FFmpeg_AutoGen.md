# FFmpeg.AutoGen 配置指南

本项目已迁移到使用FFmpeg.AutoGen库进行视频推流。以下是配置说明：

## 1. 安装FFmpeg.AutoGen包

项目已添加FFmpeg.AutoGen包引用：
```xml
<PackageReference Include="FFmpeg.AutoGen" Version="5.1.0" />
```

## 2. 配置FFmpeg二进制文件

FFmpeg.AutoGen是FFmpeg的C#绑定库，需要配合实际的FFmpeg二进制文件使用。请按照以下步骤配置：

### 步骤1: 下载FFmpeg二进制文件

1. 访问FFmpeg官方网站: https://ffmpeg.org/download.html
2. 下载适合Windows的静态构建版本(Windows Builds)
3. 建议下载5.1.x版本以匹配FFmpeg.AutoGen 5.1.0

### 步骤2: 解压并配置

1. 解压下载的FFmpeg文件
2. 在应用程序目录(即exe文件所在目录)下创建`ffmpeg`文件夹
3. 将解压后的FFmpeg文件中的以下文件复制到`ffmpeg`文件夹:
   - ffmpeg.exe
   - ffprobe.exe
   - 所有.dll文件

## 3. 运行应用程序

配置完成后，即可正常运行应用程序。FFmpegBinariesHelper类会自动注册FFmpeg二进制文件。

## 4. 代码更改说明

1. 移除了通过Process调用FFmpeg的方式
2. 使用FFmpeg.AutoGen直接调用FFmpeg API
3. 添加了图像格式转换功能
4. 优化了推流逻辑和错误处理

## 5. 可能遇到的问题

### 问题1: 找不到FFmpeg二进制文件
- 确保已按照步骤2正确配置FFmpeg文件
- 检查应用程序目录下是否存在`ffmpeg`文件夹及相关文件

### 问题2: 编码器找不到
- 确保FFmpeg版本与FFmpeg.AutoGen版本匹配
- 尝试使用其他编码器(修改代码中的AV_CODEC_ID_H264)

### 问题3: 推流失败
- 检查推流地址是否正确
- 确保网络连接正常
- 查看调试输出获取详细错误信息

## 6. 依赖项

- .NET 9.0
- Windows Forms
- FFmpeg.AutoGen 5.1.0
- FFmpeg 5.1.x 二进制文件