﻿using System;
namespace MIRLE_GPLC.form
{
    partial class ProjectDataView
    {
        /// <summary> 
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            Utility.modbusWorkerPool.stopViewWorker();
            base.Dispose(disposing);
        }

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.listView_tag = new System.Windows.Forms.ListView();
            this.item = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.value = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listView_plc = new System.Windows.Forms.ListView();
            this.name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ip = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.port = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonNext = new System.Windows.Forms.Button();
            this.buttonBack = new System.Windows.Forms.Button();
            this.labelCounter = new System.Windows.Forms.Label();
            this.labelText = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.tagInputControl1 = new MIRLE_GPLC.form.TagInputControl();
            this.plcInputControl = new MIRLE_GPLC.form.PLCInputControl();
            this.SuspendLayout();
            // 
            // listView_tag
            // 
            this.listView_tag.Alignment = System.Windows.Forms.ListViewAlignment.Default;
            this.listView_tag.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.item,
            this.value});
            this.listView_tag.FullRowSelect = true;
            this.listView_tag.HideSelection = false;
            this.listView_tag.Location = new System.Drawing.Point(261, 48);
            this.listView_tag.Name = "listView_tag";
            this.listView_tag.Size = new System.Drawing.Size(184, 255);
            this.listView_tag.TabIndex = 13;
            this.listView_tag.UseCompatibleStateImageBehavior = false;
            this.listView_tag.View = System.Windows.Forms.View.Details;
            this.listView_tag.DoubleClick += new System.EventHandler(this.listView_tag_DoubleClick);
            this.listView_tag.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView_tag_KeyDown);
            this.listView_tag.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listView_data_MouseDown);
            // 
            // item
            // 
            this.item.Tag = "1";
            this.item.Text = "項目";
            this.item.Width = 155;
            // 
            // value
            // 
            this.value.Tag = "1";
            this.value.Text = "值";
            this.value.Width = 80;
            // 
            // listView_plc
            // 
            this.listView_plc.Alignment = System.Windows.Forms.ListViewAlignment.Default;
            this.listView_plc.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.name,
            this.id,
            this.ip,
            this.port});
            this.listView_plc.FullRowSelect = true;
            this.listView_plc.HideSelection = false;
            this.listView_plc.Location = new System.Drawing.Point(1, 48);
            this.listView_plc.Name = "listView_plc";
            this.listView_plc.Size = new System.Drawing.Size(254, 255);
            this.listView_plc.TabIndex = 12;
            this.listView_plc.UseCompatibleStateImageBehavior = false;
            this.listView_plc.View = System.Windows.Forms.View.Details;
            this.listView_plc.SelectedIndexChanged += new System.EventHandler(this.listView_plc_SelectedIndexChanged);
            this.listView_plc.DoubleClick += new System.EventHandler(this.listView_plc_DoubleClick);
            this.listView_plc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView_plc_KeyDown);
            this.listView_plc.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listView_plc_MouseDown);
            this.listView_plc.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listView_plc_MouseUp);
            // 
            // name
            // 
            this.name.Text = "名稱";
            this.name.Width = 100;
            // 
            // id
            // 
            this.id.Text = "ID";
            this.id.Width = 30;
            // 
            // ip
            // 
            this.ip.Text = "IP";
            this.ip.Width = 110;
            // 
            // port
            // 
            this.port.Text = "Port";
            this.port.Width = 50;
            // 
            // buttonNext
            // 
            this.buttonNext.Location = new System.Drawing.Point(419, 19);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(26, 23);
            this.buttonNext.TabIndex = 11;
            this.buttonNext.Text = ">";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // buttonBack
            // 
            this.buttonBack.Location = new System.Drawing.Point(387, 19);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(26, 23);
            this.buttonBack.TabIndex = 10;
            this.buttonBack.Text = "<";
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // labelCounter
            // 
            this.labelCounter.AutoSize = true;
            this.labelCounter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelCounter.Location = new System.Drawing.Point(402, 1);
            this.labelCounter.Name = "labelCounter";
            this.labelCounter.Size = new System.Drawing.Size(27, 12);
            this.labelCounter.TabIndex = 9;
            this.labelCounter.Text = "page";
            // 
            // labelText
            // 
            this.labelText.AutoSize = true;
            this.labelText.Location = new System.Drawing.Point(3, 24);
            this.labelText.MaximumSize = new System.Drawing.Size(400, 0);
            this.labelText.Name = "labelText";
            this.labelText.Size = new System.Drawing.Size(22, 12);
            this.labelText.TabIndex = 8;
            this.labelText.Text = "text";
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelTitle.Location = new System.Drawing.Point(3, 0);
            this.labelTitle.MaximumSize = new System.Drawing.Size(400, 0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(43, 19);
            this.labelTitle.TabIndex = 7;
            this.labelTitle.Text = "title";
            // 
            // tagInputControl1
            // 
            this.tagInputControl1.Location = new System.Drawing.Point(1, 48);
            this.tagInputControl1.Name = "tagInputControl1";
            this.tagInputControl1.Size = new System.Drawing.Size(254, 258);
            this.tagInputControl1.TabIndex = 15;
            this.tagInputControl1.Visible = false;
            // 
            // plcInputControl
            // 
            this.plcInputControl.Location = new System.Drawing.Point(261, 48);
            this.plcInputControl.Name = "plcInputControl";
            this.plcInputControl.Size = new System.Drawing.Size(198, 171);
            this.plcInputControl.TabIndex = 14;
            this.plcInputControl.Visible = false;
            // 
            // ProjectDataView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tagInputControl1);
            this.Controls.Add(this.plcInputControl);
            this.Controls.Add(this.listView_tag);
            this.Controls.Add(this.buttonNext);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.labelCounter);
            this.Controls.Add(this.labelText);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.listView_plc);
            this.Name = "ProjectDataView";
            this.Size = new System.Drawing.Size(458, 306);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView_tag;
        private System.Windows.Forms.ColumnHeader item;
        private System.Windows.Forms.ColumnHeader value;
        private System.Windows.Forms.ListView listView_plc;
        private System.Windows.Forms.ColumnHeader name;
        private System.Windows.Forms.ColumnHeader id;
        private System.Windows.Forms.ColumnHeader ip;
        private System.Windows.Forms.ColumnHeader port;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Label labelCounter;
        private System.Windows.Forms.Label labelText;
        private System.Windows.Forms.Label labelTitle;
        private PLCInputControl plcInputControl;
        private TagInputControl tagInputControl1;

    }
}
