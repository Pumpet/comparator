using System.Windows.Forms;
using Common;

namespace Sources
{
  partial class SourcePanel : UserControl, IViewSource
  {
    /// <summary> 
    /// Требуется переменная конструктора.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Освободить все используемые ресурсы.
    /// </summary>
    /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Код, автоматически созданный конструктором компонентов

    /// <summary> 
    /// Обязательный метод для поддержки конструктора - не изменяйте 
    /// содержимое данного метода при помощи редактора кода.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SourcePanel));
      this.panelDatabase = new System.Windows.Forms.Panel();
      this.tbConnInfo = new System.Windows.Forms.TextBox();
      this.bSetConn = new System.Windows.Forms.Button();
      this.panelExcel = new System.Windows.Forms.Panel();
      this.bSelectFileXls = new System.Windows.Forms.Button();
      this.bViewDataXls = new System.Windows.Forms.Button();
      this.bShowXls = new System.Windows.Forms.Button();
      this.tbNameXls = new System.Windows.Forms.TextBox();
      this.tbRngEnd = new System.Windows.Forms.TextBox();
      this.tbRngStart = new System.Windows.Forms.TextBox();
      this.label7 = new System.Windows.Forms.Label();
      this.cboxSheets = new System.Windows.Forms.ComboBox();
      this.label6 = new System.Windows.Forms.Label();
      this.label9 = new System.Windows.Forms.Label();
      this.cbFirstLineNamesXls = new System.Windows.Forms.CheckBox();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.ddbOpened = new System.Windows.Forms.ToolStripDropDownButton();
      this.panelCsv = new System.Windows.Forms.Panel();
      this.tbCp = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.bViewDataCsv = new System.Windows.Forms.Button();
      this.label4 = new System.Windows.Forms.Label();
      this.tbDelimiter = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.cbFirstLineNamesCsv = new System.Windows.Forms.CheckBox();
      this.tbNameCsv = new System.Windows.Forms.TextBox();
      this.bSelectFileCsv = new System.Windows.Forms.Button();
      this.panelCommon = new System.Windows.Forms.Panel();
      this.tbSrcName = new System.Windows.Forms.TextBox();
      this.cboxSource = new System.Windows.Forms.ComboBox();
      this.label3 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.ttip = new System.Windows.Forms.ToolTip(this.components);
      this.panelXml = new System.Windows.Forms.Panel();
      this.tbXmlInfo = new System.Windows.Forms.TextBox();
      this.bXmlOptions = new System.Windows.Forms.Button();
      this.panelDatabase.SuspendLayout();
      this.panelExcel.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.panelCsv.SuspendLayout();
      this.panelCommon.SuspendLayout();
      this.panelXml.SuspendLayout();
      this.SuspendLayout();
      // 
      // panelDatabase
      // 
      this.panelDatabase.Controls.Add(this.tbConnInfo);
      this.panelDatabase.Controls.Add(this.bSetConn);
      this.panelDatabase.Location = new System.Drawing.Point(267, 253);
      this.panelDatabase.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
      this.panelDatabase.Name = "panelDatabase";
      this.panelDatabase.Size = new System.Drawing.Size(250, 110);
      this.panelDatabase.TabIndex = 1;
      // 
      // tbConnInfo
      // 
      this.tbConnInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tbConnInfo.BackColor = System.Drawing.SystemColors.Control;
      this.tbConnInfo.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.tbConnInfo.Location = new System.Drawing.Point(8, 27);
      this.tbConnInfo.Multiline = true;
      this.tbConnInfo.Name = "tbConnInfo";
      this.tbConnInfo.ReadOnly = true;
      this.tbConnInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.tbConnInfo.Size = new System.Drawing.Size(234, 76);
      this.tbConnInfo.TabIndex = 1;
      // 
      // bSetConn
      // 
      this.bSetConn.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
      this.bSetConn.FlatAppearance.BorderSize = 0;
      this.bSetConn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.bSetConn.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.bSetConn.Image = global::Sources.Properties.Resources.sql;
      this.bSetConn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.bSetConn.Location = new System.Drawing.Point(4, 2);
      this.bSetConn.Name = "bSetConn";
      this.bSetConn.Size = new System.Drawing.Size(232, 23);
      this.bSetConn.TabIndex = 0;
      this.bSetConn.Text = "Connect and run query";
      this.bSetConn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.bSetConn.UseVisualStyleBackColor = true;
      this.bSetConn.Click += new System.EventHandler(this.bSetConn_Click);
      // 
      // panelExcel
      // 
      this.panelExcel.Controls.Add(this.bSelectFileXls);
      this.panelExcel.Controls.Add(this.bViewDataXls);
      this.panelExcel.Controls.Add(this.bShowXls);
      this.panelExcel.Controls.Add(this.tbNameXls);
      this.panelExcel.Controls.Add(this.tbRngEnd);
      this.panelExcel.Controls.Add(this.tbRngStart);
      this.panelExcel.Controls.Add(this.label7);
      this.panelExcel.Controls.Add(this.cboxSheets);
      this.panelExcel.Controls.Add(this.label6);
      this.panelExcel.Controls.Add(this.label9);
      this.panelExcel.Controls.Add(this.cbFirstLineNamesXls);
      this.panelExcel.Controls.Add(this.toolStrip1);
      this.panelExcel.Location = new System.Drawing.Point(267, 133);
      this.panelExcel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
      this.panelExcel.Name = "panelExcel";
      this.panelExcel.Size = new System.Drawing.Size(250, 110);
      this.panelExcel.TabIndex = 2;
      // 
      // bSelectFileXls
      // 
      this.bSelectFileXls.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
      this.bSelectFileXls.FlatAppearance.BorderSize = 0;
      this.bSelectFileXls.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.bSelectFileXls.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.bSelectFileXls.Image = global::Sources.Properties.Resources.excel;
      this.bSelectFileXls.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.bSelectFileXls.Location = new System.Drawing.Point(4, 2);
      this.bSelectFileXls.Name = "bSelectFileXls";
      this.bSelectFileXls.Size = new System.Drawing.Size(82, 23);
      this.bSelectFileXls.TabIndex = 0;
      this.bSelectFileXls.Text = " Get Excel";
      this.bSelectFileXls.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.bSelectFileXls.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.bSelectFileXls.UseVisualStyleBackColor = true;
      this.bSelectFileXls.Click += new System.EventHandler(this.bSelectFileXls_Click);
      // 
      // bViewDataXls
      // 
      this.bViewDataXls.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
      this.bViewDataXls.FlatAppearance.BorderSize = 0;
      this.bViewDataXls.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.bViewDataXls.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.bViewDataXls.Image = global::Sources.Properties.Resources.viewdata;
      this.bViewDataXls.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.bViewDataXls.Location = new System.Drawing.Point(86, 2);
      this.bViewDataXls.Name = "bViewDataXls";
      this.bViewDataXls.Size = new System.Drawing.Size(85, 23);
      this.bViewDataXls.TabIndex = 1;
      this.bViewDataXls.Text = " View data";
      this.bViewDataXls.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.bViewDataXls.UseVisualStyleBackColor = true;
      this.bViewDataXls.Click += new System.EventHandler(this.bViewData_Click);
      // 
      // bShowXls
      // 
      this.bShowXls.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
      this.bShowXls.FlatAppearance.BorderSize = 0;
      this.bShowXls.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.bShowXls.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.bShowXls.Image = global::Sources.Properties.Resources.showexcel;
      this.bShowXls.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.bShowXls.Location = new System.Drawing.Point(171, 2);
      this.bShowXls.Name = "bShowXls";
      this.bShowXls.Size = new System.Drawing.Size(63, 23);
      this.bShowXls.TabIndex = 2;
      this.bShowXls.Text = " Open";
      this.bShowXls.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.bShowXls.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.bShowXls.UseVisualStyleBackColor = true;
      this.bShowXls.Click += new System.EventHandler(this.bOpenXls_Click);
      // 
      // tbNameXls
      // 
      this.tbNameXls.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tbNameXls.BackColor = System.Drawing.SystemColors.Window;
      this.tbNameXls.Location = new System.Drawing.Point(8, 27);
      this.tbNameXls.Name = "tbNameXls";
      this.tbNameXls.Size = new System.Drawing.Size(235, 22);
      this.tbNameXls.TabIndex = 3;
      this.tbNameXls.TextChanged += new System.EventHandler(this.tbNameXls_TextChanged);
      // 
      // tbRngEnd
      // 
      this.tbRngEnd.Location = new System.Drawing.Point(100, 81);
      this.tbRngEnd.Name = "tbRngEnd";
      this.tbRngEnd.Size = new System.Drawing.Size(44, 22);
      this.tbRngEnd.TabIndex = 8;
      // 
      // tbRngStart
      // 
      this.tbRngStart.Location = new System.Drawing.Point(49, 81);
      this.tbRngStart.Name = "tbRngStart";
      this.tbRngStart.Size = new System.Drawing.Size(44, 22);
      this.tbRngStart.TabIndex = 7;
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(7, 84);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(40, 13);
      this.label7.TabIndex = 6;
      this.label7.Text = "Range";
      // 
      // cboxSheets
      // 
      this.cboxSheets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboxSheets.Location = new System.Drawing.Point(49, 54);
      this.cboxSheets.Name = "cboxSheets";
      this.cboxSheets.Size = new System.Drawing.Size(112, 21);
      this.cboxSheets.TabIndex = 5;
      this.cboxSheets.SelectedIndexChanged += new System.EventHandler(this.cboxSheets_SelectedIndexChanged);
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(7, 57);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(36, 13);
      this.label6.TabIndex = 4;
      this.label6.Text = "Sheet";
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.Location = new System.Drawing.Point(92, 84);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(10, 13);
      this.label9.TabIndex = 25;
      this.label9.Text = ":";
      this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // cbFirstLineNamesXls
      // 
      this.cbFirstLineNamesXls.Location = new System.Drawing.Point(147, 84);
      this.cbFirstLineNamesXls.Name = "cbFirstLineNamesXls";
      this.cbFirstLineNamesXls.Size = new System.Drawing.Size(102, 17);
      this.cbFirstLineNamesXls.TabIndex = 9;
      this.cbFirstLineNamesXls.Text = "First row fields";
      this.cbFirstLineNamesXls.UseVisualStyleBackColor = true;
      // 
      // toolStrip1
      // 
      this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
      this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
      this.toolStrip1.GripMargin = new System.Windows.Forms.Padding(0);
      this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ddbOpened});
      this.toolStrip1.Location = new System.Drawing.Point(9, 2);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Padding = new System.Windows.Forms.Padding(0);
      this.toolStrip1.Size = new System.Drawing.Size(15, 25);
      this.toolStrip1.TabIndex = 5;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // ddbOpened
      // 
      this.ddbOpened.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.None;
      this.ddbOpened.Image = ((System.Drawing.Image)(resources.GetObject("ddbOpened.Image")));
      this.ddbOpened.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.ddbOpened.Name = "ddbOpened";
      this.ddbOpened.Size = new System.Drawing.Size(13, 22);
      this.ddbOpened.DropDownOpening += new System.EventHandler(this.ddbOpened_DropDownOpening);
      this.ddbOpened.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ddbOpened_DropDownItemClicked);
      // 
      // panelCsv
      // 
      this.panelCsv.Controls.Add(this.tbCp);
      this.panelCsv.Controls.Add(this.label5);
      this.panelCsv.Controls.Add(this.bViewDataCsv);
      this.panelCsv.Controls.Add(this.label4);
      this.panelCsv.Controls.Add(this.tbDelimiter);
      this.panelCsv.Controls.Add(this.label1);
      this.panelCsv.Controls.Add(this.cbFirstLineNamesCsv);
      this.panelCsv.Controls.Add(this.tbNameCsv);
      this.panelCsv.Controls.Add(this.bSelectFileCsv);
      this.panelCsv.Location = new System.Drawing.Point(267, 8);
      this.panelCsv.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
      this.panelCsv.Name = "panelCsv";
      this.panelCsv.Size = new System.Drawing.Size(250, 110);
      this.panelCsv.TabIndex = 3;
      // 
      // tbCp
      // 
      this.tbCp.Location = new System.Drawing.Point(67, 81);
      this.tbCp.Name = "tbCp";
      this.tbCp.Size = new System.Drawing.Size(49, 22);
      this.tbCp.TabIndex = 4;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(7, 84);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(60, 13);
      this.label5.TabIndex = 36;
      this.label5.Text = "Codepage";
      // 
      // bViewDataCsv
      // 
      this.bViewDataCsv.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
      this.bViewDataCsv.FlatAppearance.BorderSize = 0;
      this.bViewDataCsv.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.bViewDataCsv.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.bViewDataCsv.Image = global::Sources.Properties.Resources.viewdata;
      this.bViewDataCsv.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.bViewDataCsv.Location = new System.Drawing.Point(86, 2);
      this.bViewDataCsv.Name = "bViewDataCsv";
      this.bViewDataCsv.Size = new System.Drawing.Size(85, 23);
      this.bViewDataCsv.TabIndex = 1;
      this.bViewDataCsv.Text = " View data";
      this.bViewDataCsv.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.bViewDataCsv.UseVisualStyleBackColor = true;
      this.bViewDataCsv.Click += new System.EventHandler(this.bViewData_Click);
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(121, 57);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(84, 13);
      this.label4.TabIndex = 35;
      this.label4.Text = "(empty for TAB)";
      this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // tbDelimiter
      // 
      this.tbDelimiter.Location = new System.Drawing.Point(67, 54);
      this.tbDelimiter.Name = "tbDelimiter";
      this.tbDelimiter.Size = new System.Drawing.Size(49, 22);
      this.tbDelimiter.TabIndex = 3;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(7, 57);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(53, 13);
      this.label1.TabIndex = 33;
      this.label1.Text = "Delimiter";
      // 
      // cbFirstLineNamesCsv
      // 
      this.cbFirstLineNamesCsv.AutoSize = true;
      this.cbFirstLineNamesCsv.Location = new System.Drawing.Point(125, 84);
      this.cbFirstLineNamesCsv.Name = "cbFirstLineNamesCsv";
      this.cbFirstLineNamesCsv.Size = new System.Drawing.Size(102, 17);
      this.cbFirstLineNamesCsv.TabIndex = 5;
      this.cbFirstLineNamesCsv.Text = "First row fields";
      this.cbFirstLineNamesCsv.UseVisualStyleBackColor = true;
      // 
      // tbNameCsv
      // 
      this.tbNameCsv.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tbNameCsv.BackColor = System.Drawing.SystemColors.Window;
      this.tbNameCsv.Location = new System.Drawing.Point(8, 27);
      this.tbNameCsv.Name = "tbNameCsv";
      this.tbNameCsv.Size = new System.Drawing.Size(234, 22);
      this.tbNameCsv.TabIndex = 2;
      this.tbNameCsv.TextChanged += new System.EventHandler(this.tbNameCsv_TextChanged);
      // 
      // bSelectFileCsv
      // 
      this.bSelectFileCsv.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
      this.bSelectFileCsv.FlatAppearance.BorderSize = 0;
      this.bSelectFileCsv.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.bSelectFileCsv.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.bSelectFileCsv.Image = global::Sources.Properties.Resources.selectfile;
      this.bSelectFileCsv.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.bSelectFileCsv.Location = new System.Drawing.Point(4, 2);
      this.bSelectFileCsv.Name = "bSelectFileCsv";
      this.bSelectFileCsv.Size = new System.Drawing.Size(82, 23);
      this.bSelectFileCsv.TabIndex = 0;
      this.bSelectFileCsv.Text = " Load file";
      this.bSelectFileCsv.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.bSelectFileCsv.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.bSelectFileCsv.UseVisualStyleBackColor = true;
      this.bSelectFileCsv.Click += new System.EventHandler(this.bSelectFileCsv_Click);
      // 
      // panelCommon
      // 
      this.panelCommon.Controls.Add(this.tbSrcName);
      this.panelCommon.Controls.Add(this.cboxSource);
      this.panelCommon.Controls.Add(this.label3);
      this.panelCommon.Controls.Add(this.label2);
      this.panelCommon.Location = new System.Drawing.Point(267, 379);
      this.panelCommon.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
      this.panelCommon.Name = "panelCommon";
      this.panelCommon.Size = new System.Drawing.Size(250, 59);
      this.panelCommon.TabIndex = 0;
      // 
      // tbSrcName
      // 
      this.tbSrcName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tbSrcName.Location = new System.Drawing.Point(49, 5);
      this.tbSrcName.Name = "tbSrcName";
      this.tbSrcName.Size = new System.Drawing.Size(194, 22);
      this.tbSrcName.TabIndex = 0;
      // 
      // cboxSource
      // 
      this.cboxSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.cboxSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboxSource.FormattingEnabled = true;
      this.cboxSource.Location = new System.Drawing.Point(49, 33);
      this.cboxSource.Name = "cboxSource";
      this.cboxSource.Size = new System.Drawing.Size(194, 21);
      this.cboxSource.TabIndex = 1;
      this.cboxSource.SelectedIndexChanged += new System.EventHandler(this.cboxSource_SelectedIndexChanged);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(7, 37);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(30, 13);
      this.label3.TabIndex = 16;
      this.label3.Text = "Type";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(7, 10);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(36, 13);
      this.label2.TabIndex = 15;
      this.label2.Text = "Name";
      // 
      // ttip
      // 
      this.ttip.AutomaticDelay = 50;
      this.ttip.AutoPopDelay = 10000;
      this.ttip.InitialDelay = 50;
      this.ttip.ReshowDelay = 10;
      // 
      // panelXml
      // 
      this.panelXml.Controls.Add(this.tbXmlInfo);
      this.panelXml.Controls.Add(this.bXmlOptions);
      this.panelXml.Location = new System.Drawing.Point(267, 453);
      this.panelXml.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
      this.panelXml.Name = "panelXml";
      this.panelXml.Size = new System.Drawing.Size(250, 110);
      this.panelXml.TabIndex = 40;
      // 
      // tbXmlInfo
      // 
      this.tbXmlInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tbXmlInfo.BackColor = System.Drawing.SystemColors.Control;
      this.tbXmlInfo.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.tbXmlInfo.Location = new System.Drawing.Point(8, 27);
      this.tbXmlInfo.Multiline = true;
      this.tbXmlInfo.Name = "tbXmlInfo";
      this.tbXmlInfo.ReadOnly = true;
      this.tbXmlInfo.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.tbXmlInfo.Size = new System.Drawing.Size(234, 76);
      this.tbXmlInfo.TabIndex = 40;
      this.tbXmlInfo.WordWrap = false;
      // 
      // bXmlOptions
      // 
      this.bXmlOptions.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
      this.bXmlOptions.FlatAppearance.BorderSize = 0;
      this.bXmlOptions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.bXmlOptions.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.bXmlOptions.Image = global::Sources.Properties.Resources.viewdata;
      this.bXmlOptions.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.bXmlOptions.Location = new System.Drawing.Point(4, 2);
      this.bXmlOptions.Margin = new System.Windows.Forms.Padding(0);
      this.bXmlOptions.Name = "bXmlOptions";
      this.bXmlOptions.Size = new System.Drawing.Size(233, 22);
      this.bXmlOptions.TabIndex = 37;
      this.bXmlOptions.Text = " Define source and convert options";
      this.bXmlOptions.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.bXmlOptions.UseVisualStyleBackColor = true;
      this.bXmlOptions.Click += new System.EventHandler(this.bXmlOptions_Click);
      // 
      // SourcePanel
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.panelXml);
      this.Controls.Add(this.panelCommon);
      this.Controls.Add(this.panelCsv);
      this.Controls.Add(this.panelExcel);
      this.Controls.Add(this.panelDatabase);
      this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.Name = "SourcePanel";
      this.Size = new System.Drawing.Size(841, 704);
      this.Load += new System.EventHandler(this.SourcePanel_Load);
      this.panelDatabase.ResumeLayout(false);
      this.panelDatabase.PerformLayout();
      this.panelExcel.ResumeLayout(false);
      this.panelExcel.PerformLayout();
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.panelCsv.ResumeLayout(false);
      this.panelCsv.PerformLayout();
      this.panelCommon.ResumeLayout(false);
      this.panelCommon.PerformLayout();
      this.panelXml.ResumeLayout(false);
      this.panelXml.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    public System.Windows.Forms.Panel panelDatabase;
    public System.Windows.Forms.TextBox tbConnInfo;
    public System.Windows.Forms.Button bSetConn;
    public System.Windows.Forms.Panel panelExcel;
    public System.Windows.Forms.CheckBox cbFirstLineNamesXls;
    public System.Windows.Forms.Button bShowXls;
    public System.Windows.Forms.TextBox tbNameXls;
    private System.Windows.Forms.Label label9;
    public System.Windows.Forms.TextBox tbRngEnd;
    public System.Windows.Forms.TextBox tbRngStart;
    private System.Windows.Forms.Label label7;
    public System.Windows.Forms.ComboBox cboxSheets;
    private System.Windows.Forms.Label label6;
    public System.Windows.Forms.Button bSelectFileXls;
    public System.Windows.Forms.Panel panelCsv;
    public System.Windows.Forms.CheckBox cbFirstLineNamesCsv;
    public System.Windows.Forms.TextBox tbNameCsv;
    public System.Windows.Forms.Button bSelectFileCsv;
    public System.Windows.Forms.Panel panelCommon;
    public System.Windows.Forms.TextBox tbSrcName;
    public System.Windows.Forms.ComboBox cboxSource;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label2;
    public System.Windows.Forms.TextBox tbDelimiter;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label4;
    public System.Windows.Forms.Button bViewDataCsv;
    public System.Windows.Forms.TextBox tbCp;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.ToolTip ttip;
    public System.Windows.Forms.Button bViewDataXls;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripDropDownButton ddbOpened;
    public System.Windows.Forms.Panel panelXml;
    public System.Windows.Forms.TextBox tbXmlInfo;
    public System.Windows.Forms.Button bXmlOptions;
  }
}
