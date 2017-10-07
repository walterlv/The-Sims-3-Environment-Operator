using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Seo
{
    public partial class SimsDirForm : Form
    {
        public SimsDirForm()
        {
            InitializeComponent();
        }

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(HandleRef hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        ///////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 将需要的Button赋予UAC请求标示
        /// </summary>
        /// <param name="ThisButton"></param>
        ///////////////////////////////////////////////////////////////////////
        private void EnableElevateIcon_BCM_SETSHIELD(Button ThisButton)
        {
            if (ThisButton == null) return;
            // 定义本地 BCM_SETSHIELD
            uint BCM_SETSHIELD = 0x0000160C;
            // 设置按钮样式
            ThisButton.FlatStyle = FlatStyle.System;
            // 将 BCM_SETSHIELD 消息发送至按钮
            SendMessage(new HandleRef(ThisButton, ThisButton.Handle), BCM_SETSHIELD, new IntPtr(0), new IntPtr(1));
        }

        private void SimsDirForm_Load(object sender, EventArgs e)
        {
            if (IsManual) ChangeToManual();
            else EnableElevateIcon_BCM_SETSHIELD(OkButton);
        }

        public bool SecurityUp = true;
        public string SimsDir { get; private set; }
        public bool IsManual = false;

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (IsManual)
            {
                if (Directory.Exists(FolderText.Text + EnvironmentOperator.SimsDirectoryTail))
                {
                    SimsDir = FolderText.Text;
                    this.Close();
                }
            }
            else
            {
                try { SimsDir = FilesDirs.GetSimsDirectoryByRegistry(); this.Close(); }
                catch { ChangeToManual(); }
            }
        }

        private void ChangeToManual()
        {
            TitleText.Text = "Manually set the folder";
            DescriptionText.Text = "We still cannot get The Sims 3 folder." + Environment.NewLine + "Please select the correct folder.";
            ManualPanel.Visible = true;
            OkButton.Enabled = false;
            IsManual = true;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FolderButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            folder.ShowNewFolderButton = false;
            folder.SelectedPath = FolderText.Text;
            if (folder.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FolderText.Text = folder.SelectedPath;
            }
        }

        private void FolderText_TextChanged(object sender, EventArgs e)
        {
            if (Directory.Exists(FolderText.Text + EnvironmentOperator.SimsDirectoryTail))
            {
                OkButton.Enabled = true;
                ErrorMsg.Text = String.Empty;
            }
            else
            {
                OkButton.Enabled = false;
                ErrorMsg.Text = "Not the correct folder";
            }
        }
    }
}
