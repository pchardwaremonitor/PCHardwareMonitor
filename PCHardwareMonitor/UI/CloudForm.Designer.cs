namespace PCHardwareMonitor.UI
{
    partial class CloudForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CloudForm));
            this.cloudTokenTextBox = new System.Windows.Forms.TextBox();
            this.cloudTokenLabel = new System.Windows.Forms.Label();
            this.cloudOKButton = new System.Windows.Forms.Button();
            this.webLinkLabel = new System.Windows.Forms.LinkLabel();
            this.loginTextLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cloudTokenTextBox
            // 
            this.cloudTokenTextBox.Location = new System.Drawing.Point(86, 12);
            this.cloudTokenTextBox.Name = "cloudTokenTextBox";
            this.cloudTokenTextBox.Size = new System.Drawing.Size(256, 20);
            this.cloudTokenTextBox.TabIndex = 0;
            // 
            // cloudTokenLabel
            // 
            this.cloudTokenLabel.AutoSize = true;
            this.cloudTokenLabel.Location = new System.Drawing.Point(12, 15);
            this.cloudTokenLabel.Name = "cloudTokenLabel";
            this.cloudTokenLabel.Size = new System.Drawing.Size(68, 13);
            this.cloudTokenLabel.TabIndex = 1;
            this.cloudTokenLabel.Text = "Cloud Token";
            // 
            // cloudOKButton
            // 
            this.cloudOKButton.Location = new System.Drawing.Point(146, 86);
            this.cloudOKButton.Name = "cloudOKButton";
            this.cloudOKButton.Size = new System.Drawing.Size(75, 23);
            this.cloudOKButton.TabIndex = 2;
            this.cloudOKButton.Text = "OK";
            this.cloudOKButton.UseVisualStyleBackColor = true;
            this.cloudOKButton.Click += new System.EventHandler(this.CloudOKButton_Click);
            // 
            // webLinkLabel
            // 
            this.webLinkLabel.AutoSize = true;
            this.webLinkLabel.Location = new System.Drawing.Point(12, 64);
            this.webLinkLabel.Name = "webLinkLabel";
            this.webLinkLabel.Size = new System.Drawing.Size(126, 13);
            this.webLinkLabel.TabIndex = 8;
            this.webLinkLabel.TabStop = true;
            this.webLinkLabel.Text = "https://pchwmonitor.com";
            this.webLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.WebLinkLabel_LinkClicked);
            // 
            // loginTextLabel
            // 
            this.loginTextLabel.AutoSize = true;
            this.loginTextLabel.Location = new System.Drawing.Point(11, 41);
            this.loginTextLabel.Name = "loginTextLabel";
            this.loginTextLabel.Size = new System.Drawing.Size(321, 13);
            this.loginTextLabel.TabIndex = 9;
            this.loginTextLabel.Text = "Login to the PC Hardware Monitor website to get your cloud token.";
            // 
            // CloudForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 121);
            this.Controls.Add(this.loginTextLabel);
            this.Controls.Add(this.webLinkLabel);
            this.Controls.Add(this.cloudOKButton);
            this.Controls.Add(this.cloudTokenLabel);
            this.Controls.Add(this.cloudTokenTextBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CloudForm";
            this.Text = "Set Cloud Token";
            this.Load += new System.EventHandler(this.CloudForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox cloudTokenTextBox;
        private System.Windows.Forms.Label cloudTokenLabel;
        private System.Windows.Forms.Button cloudOKButton;
        private System.Windows.Forms.LinkLabel webLinkLabel;
        private System.Windows.Forms.Label loginTextLabel;
    }
}
