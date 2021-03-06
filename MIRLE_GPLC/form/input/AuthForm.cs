﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MIRLE_GPLC.form
{
    public partial class AuthForm : Form
    {
        public string id;
        public string pass;

        public AuthForm()
        {
            InitializeComponent();
            this.CenterToParent();
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            id = textBox_id.Text;
            pass = textBox_pass.Text;
            if (id.Length < 3)
            {
                label_help.Text = "ID must be at least 3 characters long";
            }
            else if (pass.Length < 3)
            {
                label_help.Text = "Password must be at least 3 characters long";
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
