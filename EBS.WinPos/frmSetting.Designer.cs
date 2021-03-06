﻿namespace EBS.WinPos
{
    partial class frmSetting
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSavePosId = new System.Windows.Forms.Button();
            this.btnSaveStoreID = new System.Windows.Forms.Button();
            this.txtPosId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbbStores = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnUpdateProduct = new System.Windows.Forms.Button();
            this.btnDownload = new System.Windows.Forms.Button();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.btnSaleSync = new System.Windows.Forms.Button();
            this.lblMsg = new System.Windows.Forms.Label();
            this.btnCommand = new System.Windows.Forms.Button();
            this.txtInfo = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSavePosId);
            this.groupBox1.Controls.Add(this.btnSaveStoreID);
            this.groupBox1.Controls.Add(this.txtPosId);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbbStores);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(567, 147);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "门店设置";
            // 
            // btnSavePosId
            // 
            this.btnSavePosId.Location = new System.Drawing.Point(357, 84);
            this.btnSavePosId.Name = "btnSavePosId";
            this.btnSavePosId.Size = new System.Drawing.Size(75, 35);
            this.btnSavePosId.TabIndex = 3;
            this.btnSavePosId.Text = "保存";
            this.btnSavePosId.UseVisualStyleBackColor = true;
            this.btnSavePosId.Click += new System.EventHandler(this.btnSavePosId_Click);
            // 
            // btnSaveStoreID
            // 
            this.btnSaveStoreID.Location = new System.Drawing.Point(357, 41);
            this.btnSaveStoreID.Name = "btnSaveStoreID";
            this.btnSaveStoreID.Size = new System.Drawing.Size(75, 35);
            this.btnSaveStoreID.TabIndex = 3;
            this.btnSaveStoreID.Text = "保存";
            this.btnSaveStoreID.UseVisualStyleBackColor = true;
            this.btnSaveStoreID.Click += new System.EventHandler(this.btnSaveStoreID_Click);
            // 
            // txtPosId
            // 
            this.txtPosId.Location = new System.Drawing.Point(138, 88);
            this.txtPosId.Name = "txtPosId";
            this.txtPosId.Size = new System.Drawing.Size(172, 29);
            this.txtPosId.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 21);
            this.label2.TabIndex = 1;
            this.label2.Text = "pos机编号：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(74, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "门店：";
            // 
            // cbbStores
            // 
            this.cbbStores.FormattingEnabled = true;
            this.cbbStores.Location = new System.Drawing.Point(138, 40);
            this.cbbStores.Name = "cbbStores";
            this.cbbStores.Size = new System.Drawing.Size(172, 29);
            this.cbbStores.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblMsg);
            this.groupBox2.Controls.Add(this.btnUpdateProduct);
            this.groupBox2.Controls.Add(this.btnDownload);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 147);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(567, 82);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "数据下载";
            // 
            // btnUpdateProduct
            // 
            this.btnUpdateProduct.Location = new System.Drawing.Point(193, 28);
            this.btnUpdateProduct.Name = "btnUpdateProduct";
            this.btnUpdateProduct.Size = new System.Drawing.Size(128, 39);
            this.btnUpdateProduct.TabIndex = 2;
            this.btnUpdateProduct.Text = "下载商品";
            this.btnUpdateProduct.UseVisualStyleBackColor = true;
            this.btnUpdateProduct.Click += new System.EventHandler(this.btnUpdateProduct_Click);
            // 
            // btnDownload
            // 
            this.btnDownload.Location = new System.Drawing.Point(35, 28);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(128, 39);
            this.btnDownload.TabIndex = 2;
            this.btnDownload.Text = "下载数据";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // dtpDate
            // 
            this.dtpDate.Location = new System.Drawing.Point(35, 246);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(200, 29);
            this.dtpDate.TabIndex = 2;
            // 
            // btnSaleSync
            // 
            this.btnSaleSync.Location = new System.Drawing.Point(268, 243);
            this.btnSaleSync.Name = "btnSaleSync";
            this.btnSaleSync.Size = new System.Drawing.Size(128, 39);
            this.btnSaleSync.TabIndex = 2;
            this.btnSaleSync.Text = "上传销售数据";
            this.btnSaleSync.UseVisualStyleBackColor = true;
            this.btnSaleSync.Click += new System.EventHandler(this.btnSaleSync_Click);
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.ForeColor = System.Drawing.Color.Red;
            this.lblMsg.Location = new System.Drawing.Point(336, 37);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(86, 21);
            this.lblMsg.TabIndex = 3;
            this.lblMsg.Text = "下载数据...";
            // 
            // btnCommand
            // 
            this.btnCommand.Location = new System.Drawing.Point(428, 298);
            this.btnCommand.Name = "btnCommand";
            this.btnCommand.Size = new System.Drawing.Size(89, 32);
            this.btnCommand.TabIndex = 4;
            this.btnCommand.Text = "执行命令";
            this.btnCommand.UseVisualStyleBackColor = true;
            this.btnCommand.Click += new System.EventHandler(this.btnCommand_Click);
            // 
            // txtInfo
            // 
            this.txtInfo.Location = new System.Drawing.Point(24, 288);
            this.txtInfo.Multiline = true;
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.Size = new System.Drawing.Size(387, 108);
            this.txtInfo.TabIndex = 5;
            // 
            // frmSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 421);
            this.Controls.Add(this.txtInfo);
            this.Controls.Add(this.btnCommand);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnSaleSync);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "frmSetting";
            this.Text = "系统设置";
            this.Load += new System.EventHandler(this.frmSetting_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtPosId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbbStores;
        private System.Windows.Forms.Button btnSavePosId;
        private System.Windows.Forms.Button btnSaveStoreID;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Button btnUpdateProduct;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Button btnSaleSync;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Button btnCommand;
        private System.Windows.Forms.TextBox txtInfo;
    }
}