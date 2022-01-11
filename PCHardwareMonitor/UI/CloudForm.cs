﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private void CloudCancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CloudOKButton_Click(object sender, EventArgs e)
        {
            _parent.SetToken(cloudTokenTextBox.Text);
            Close();
        }

        private void CloudForm_Load(object sender, EventArgs e)
        {
            cloudTokenTextBox.Text = _parent.GetToken();
        }
    }
}
