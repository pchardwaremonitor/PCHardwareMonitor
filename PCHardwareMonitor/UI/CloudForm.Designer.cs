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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.cloudKeyLabel = new System.Windows.Forms.Label();
            this.cloudOKButton = new System.Windows.Forms.Button();
            this.cloudCancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(80, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(209, 20);
            this.textBox1.TabIndex = 0;
            // 
            // cloudKeyLabel
            // 
            this.cloudKeyLabel.AutoSize = true;
            this.cloudKeyLabel.Location = new System.Drawing.Point(19, 15);
            this.cloudKeyLabel.Name = "cloudKeyLabel";
            this.cloudKeyLabel.Size = new System.Drawing.Size(55, 13);
            this.cloudKeyLabel.TabIndex = 1;
            this.cloudKeyLabel.Text = "Cloud Key";
            // 
            // cloudOKButton
            // 
            this.cloudOKButton.Location = new System.Drawing.Point(157, 84);
            this.cloudOKButton.Name = "cloudOKButton";
            this.cloudOKButton.Size = new System.Drawing.Size(75, 23);
            this.cloudOKButton.TabIndex = 2;
            this.cloudOKButton.Text = "OK";
            this.cloudOKButton.UseVisualStyleBackColor = true;
            this.cloudOKButton.Click += new System.EventHandler(this.CloudOKButton_Click);
            // 
            // cloudCancelButton
            // 
            this.cloudCancelButton.Location = new System.Drawing.Point(67, 84);
            this.cloudCancelButton.Name = "cloudCancelButton";
            this.cloudCancelButton.Size = new System.Drawing.Size(75, 23);
            this.cloudCancelButton.TabIndex = 3;
            this.cloudCancelButton.Text = "Cancel";
            this.cloudCancelButton.UseVisualStyleBackColor = true;
            this.cloudCancelButton.Click += new System.EventHandler(this.CloudCancelButton_Click);
            // 
            // CloudForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(302, 119);
            this.Controls.Add(this.cloudCancelButton);
            this.Controls.Add(this.cloudOKButton);
            this.Controls.Add(this.cloudKeyLabel);
            this.Controls.Add(this.textBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CloudForm";
            this.Text = "Set Cloud Token";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label cloudKeyLabel;
        private System.Windows.Forms.Button cloudOKButton;
        private System.Windows.Forms.Button cloudCancelButton;
    }
}
