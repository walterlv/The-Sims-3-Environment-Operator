namespace Seo
{
    partial class SimsDirForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TitleText = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.DescriptionText = new System.Windows.Forms.Label();
            this.OkButton = new System.Windows.Forms.Button();
            this.ClButton = new System.Windows.Forms.Button();
            this.ManualPanel = new System.Windows.Forms.Panel();
            this.ErrorMsg = new System.Windows.Forms.Label();
            this.FolderButton = new System.Windows.Forms.Button();
            this.FolderText = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.ManualPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // TitleText
            // 
            this.TitleText.AutoSize = true;
            this.TitleText.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TitleText.ForeColor = System.Drawing.Color.White;
            this.TitleText.Location = new System.Drawing.Point(12, 9);
            this.TitleText.Name = "TitleText";
            this.TitleText.Size = new System.Drawing.Size(97, 21);
            this.TitleText.TabIndex = 0;
            this.TitleText.Text = "Security Up";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.SteelBlue;
            this.panel1.Controls.Add(this.TitleText);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(422, 42);
            this.panel1.TabIndex = 1;
            // 
            // DescriptionText
            // 
            this.DescriptionText.AutoSize = true;
            this.DescriptionText.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DescriptionText.Location = new System.Drawing.Point(12, 54);
            this.DescriptionText.Name = "DescriptionText";
            this.DescriptionText.Size = new System.Drawing.Size(300, 38);
            this.DescriptionText.TabIndex = 2;
            this.DescriptionText.Text = "Sorry, but we cannot get the The Sims 3 folder.\r\nWe need Security Up.";
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.OkButton.Location = new System.Drawing.Point(236, 158);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(84, 32);
            this.OkButton.TabIndex = 3;
            this.OkButton.Text = "OK";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // ClButton
            // 
            this.ClButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ClButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ClButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ClButton.Location = new System.Drawing.Point(326, 158);
            this.ClButton.Name = "ClButton";
            this.ClButton.Size = new System.Drawing.Size(84, 32);
            this.ClButton.TabIndex = 4;
            this.ClButton.Text = "Cancel";
            this.ClButton.UseVisualStyleBackColor = true;
            this.ClButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // ManualPanel
            // 
            this.ManualPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ManualPanel.Controls.Add(this.ErrorMsg);
            this.ManualPanel.Controls.Add(this.FolderButton);
            this.ManualPanel.Controls.Add(this.FolderText);
            this.ManualPanel.Location = new System.Drawing.Point(13, 96);
            this.ManualPanel.Name = "ManualPanel";
            this.ManualPanel.Size = new System.Drawing.Size(397, 56);
            this.ManualPanel.TabIndex = 5;
            this.ManualPanel.Visible = false;
            // 
            // ErrorMsg
            // 
            this.ErrorMsg.AutoSize = true;
            this.ErrorMsg.ForeColor = System.Drawing.Color.Red;
            this.ErrorMsg.Location = new System.Drawing.Point(4, 34);
            this.ErrorMsg.Name = "ErrorMsg";
            this.ErrorMsg.Size = new System.Drawing.Size(0, 17);
            this.ErrorMsg.TabIndex = 2;
            // 
            // FolderButton
            // 
            this.FolderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FolderButton.Location = new System.Drawing.Point(363, 4);
            this.FolderButton.Name = "FolderButton";
            this.FolderButton.Size = new System.Drawing.Size(31, 23);
            this.FolderButton.TabIndex = 1;
            this.FolderButton.Text = "...";
            this.FolderButton.UseVisualStyleBackColor = true;
            this.FolderButton.Click += new System.EventHandler(this.FolderButton_Click);
            // 
            // FolderText
            // 
            this.FolderText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FolderText.Location = new System.Drawing.Point(4, 4);
            this.FolderText.Name = "FolderText";
            this.FolderText.Size = new System.Drawing.Size(353, 23);
            this.FolderText.TabIndex = 0;
            this.FolderText.TextChanged += new System.EventHandler(this.FolderText_TextChanged);
            // 
            // SimsDirForm
            // 
            this.AcceptButton = this.OkButton;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(422, 202);
            this.Controls.Add(this.ManualPanel);
            this.Controls.Add(this.ClButton);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.DescriptionText);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SimsDirForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sims Folder";
            this.Load += new System.EventHandler(this.SimsDirForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ManualPanel.ResumeLayout(false);
            this.ManualPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label TitleText;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label DescriptionText;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Button ClButton;
        private System.Windows.Forms.Panel ManualPanel;
        private System.Windows.Forms.Button FolderButton;
        private System.Windows.Forms.TextBox FolderText;
        private System.Windows.Forms.Label ErrorMsg;
    }
}