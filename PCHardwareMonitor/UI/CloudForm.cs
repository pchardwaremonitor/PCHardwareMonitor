using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace PCHardwareMonitor.UI
{
    public partial class CloudForm : Form
    {
        private readonly MainForm _parent;

        public CloudForm(MainForm m)
        {
            InitializeComponent();
            _parent = m;
        }

        private void CloudOKButton_Click(object sender, EventArgs e)
        {
            _parent.SetToken(cloudTokenTextBox.Text);
            Close();
        }

        private void CloudForm_Load(object sender, EventArgs e)
        {
            cloudTokenTextBox.Text = _parent.GetToken();

            string url = "https://pchwmonitor.com/";
            webLinkLabel.Text = url;
            webLinkLabel.Links.Remove(webLinkLabel.Links[0]);
            webLinkLabel.Links.Add(0, webLinkLabel.Text.Length, url);
        }

        private void WebLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo(e.Link.LinkData.ToString()));
            }
            catch { }
        }
    }
}
