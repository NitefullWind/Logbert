﻿namespace Com.Couchcoding.Logbert.Receiver.SyslogUdpReceiver
{
  partial class SyslogUdpReceiverSettings
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.lblNetworkInterface = new System.Windows.Forms.Label();
      this.cmbNetworkInterface = new System.Windows.Forms.ComboBox();
      this.nfoPanel = new Com.Couchcoding.GuiLibrary.Controls.InfoPanel();
      this.chkMulticastGroup = new System.Windows.Forms.CheckBox();
      this.txtMulticastIp = new System.Windows.Forms.TextBox();
      this.nudPort = new System.Windows.Forms.NumericUpDown();
      this.lblPort = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.nudPort)).BeginInit();
      this.SuspendLayout();
      // 
      // lblNetworkInterface
      // 
      this.lblNetworkInterface.AutoSize = true;
      this.lblNetworkInterface.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.lblNetworkInterface.Location = new System.Drawing.Point(0, 0);
      this.lblNetworkInterface.Name = "lblNetworkInterface";
      this.lblNetworkInterface.Size = new System.Drawing.Size(95, 13);
      this.lblNetworkInterface.TabIndex = 0;
      this.lblNetworkInterface.Text = "&Network Interface:";
      // 
      // cmbNetworkInterface
      // 
      this.cmbNetworkInterface.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.cmbNetworkInterface.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbNetworkInterface.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.cmbNetworkInterface.FormattingEnabled = true;
      this.cmbNetworkInterface.Location = new System.Drawing.Point(0, 16);
      this.cmbNetworkInterface.Name = "cmbNetworkInterface";
      this.cmbNetworkInterface.Size = new System.Drawing.Size(400, 21);
      this.cmbNetworkInterface.TabIndex = 1;
      this.cmbNetworkInterface.SelectedIndexChanged += new System.EventHandler(this.CmbNetworkInterfaceSelectedIndexChanged);
      // 
      // nfoPanel
      // 
      this.nfoPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.nfoPanel.BackColor = System.Drawing.Color.WhiteSmoke;
      this.nfoPanel.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
      this.nfoPanel.InfoIcon = global::Com.Couchcoding.Logbert.Properties.Resources.StatusAnnotations_Information_16xLG;
      this.nfoPanel.Location = new System.Drawing.Point(0, 43);
      this.nfoPanel.Name = "nfoPanel";
      this.nfoPanel.ShowInfoIcon = true;
      this.nfoPanel.ShowTitle = false;
      this.nfoPanel.Size = new System.Drawing.Size(400, 64);
      this.nfoPanel.TabIndex = 2;
      this.nfoPanel.TextPadding = new System.Windows.Forms.Padding(0, 6, 0, 0);
      this.nfoPanel.Title = "";
      // 
      // chkMulticastGroup
      // 
      this.chkMulticastGroup.AutoSize = true;
      this.chkMulticastGroup.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.chkMulticastGroup.Location = new System.Drawing.Point(0, 164);
      this.chkMulticastGroup.Name = "chkMulticastGroup";
      this.chkMulticastGroup.Size = new System.Drawing.Size(131, 18);
      this.chkMulticastGroup.TabIndex = 5;
      this.chkMulticastGroup.Text = "Join &Multicast Group:";
      this.chkMulticastGroup.UseVisualStyleBackColor = true;
      this.chkMulticastGroup.CheckedChanged += new System.EventHandler(this.ChkMulticastGroupCheckedChanged);
      // 
      // txtMulticastIp
      // 
      this.txtMulticastIp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtMulticastIp.Enabled = false;
      this.txtMulticastIp.Location = new System.Drawing.Point(134, 163);
      this.txtMulticastIp.Name = "txtMulticastIp";
      this.txtMulticastIp.Size = new System.Drawing.Size(263, 20);
      this.txtMulticastIp.TabIndex = 6;
      // 
      // nudPort
      // 
      this.nudPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.nudPort.Location = new System.Drawing.Point(0, 132);
      this.nudPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
      this.nudPort.Name = "nudPort";
      this.nudPort.Size = new System.Drawing.Size(400, 20);
      this.nudPort.TabIndex = 4;
      this.nudPort.Value = new decimal(new int[] {
            514,
            0,
            0,
            0});
      // 
      // lblPort
      // 
      this.lblPort.AutoSize = true;
      this.lblPort.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.lblPort.Location = new System.Drawing.Point(0, 116);
      this.lblPort.Name = "lblPort";
      this.lblPort.Size = new System.Drawing.Size(29, 13);
      this.lblPort.TabIndex = 3;
      this.lblPort.Text = "&Port:";
      // 
      // SyslogUdpReceiverSettings
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.nudPort);
      this.Controls.Add(this.lblPort);
      this.Controls.Add(this.txtMulticastIp);
      this.Controls.Add(this.chkMulticastGroup);
      this.Controls.Add(this.nfoPanel);
      this.Controls.Add(this.cmbNetworkInterface);
      this.Controls.Add(this.lblNetworkInterface);
      this.Margin = new System.Windows.Forms.Padding(0);
      this.Name = "SyslogUdpReceiverSettings";
      this.Size = new System.Drawing.Size(400, 300);
      ((System.ComponentModel.ISupportInitialize)(this.nudPort)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lblNetworkInterface;
    private System.Windows.Forms.ComboBox cmbNetworkInterface;
    private GuiLibrary.Controls.InfoPanel nfoPanel;
    private System.Windows.Forms.CheckBox chkMulticastGroup;
    private System.Windows.Forms.TextBox txtMulticastIp;
    private System.Windows.Forms.NumericUpDown nudPort;
    private System.Windows.Forms.Label lblPort;
  }
}
