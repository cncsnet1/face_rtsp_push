using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace screen_window
{
    public partial class Form2 : Form
    {
        private AppConfig config;

        public Form2()
        {
            InitializeComponent();
            LoadConfig();
            
            // 绑定保存按钮事件
            button1.Click += button1_Click;
        }

        private void LoadConfig()
        {
            config = ConfigManager.LoadConfig();
            textBox1.Text = config.StreamUrl;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 保存配置
            config.StreamUrl = textBox1.Text.Trim();
            ConfigManager.SaveConfig(config);
            
            MessageBox.Show("配置已保存！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
