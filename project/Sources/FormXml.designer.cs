namespace Sources
{
  partial class FormXml
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
      this.components = new System.ComponentModel.Container();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.panel1 = new System.Windows.Forms.Panel();
      this.cbUseFieldsMap = new System.Windows.Forms.CheckBox();
      this.cbFromAttrib = new System.Windows.Forms.CheckBox();
      this.tbPathRow = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.tbCodepage = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.bConvert = new System.Windows.Forms.Button();
      this.tbNameXml = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.bSelectFileXml = new System.Windows.Forms.Button();
      this.cbFromTag = new System.Windows.Forms.CheckBox();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.label4 = new System.Windows.Forms.Label();
      this.rbXsltNotUse = new System.Windows.Forms.RadioButton();
      this.tbNameXslt = new System.Windows.Forms.TextBox();
      this.bSelectFileXslt = new System.Windows.Forms.Button();
      this.rbXsltScript = new System.Windows.Forms.RadioButton();
      this.rbXsltFile = new System.Windows.Forms.RadioButton();
      this.tabs = new System.Windows.Forms.TabControl();
      this.tabPageXslt = new System.Windows.Forms.TabPage();
      this.tbXslt = new FastColoredTextBoxNS.FastColoredTextBox();
      this.tabPageXml = new System.Windows.Forms.TabPage();
      this.tbResult = new FastColoredTextBoxNS.FastColoredTextBox();
      this.tabPageResult = new System.Windows.Forms.TabPage();
      this.dgResult = new System.Windows.Forms.DataGridView();
      this.tabPageMap = new System.Windows.Forms.TabPage();
      this.dgFields = new System.Windows.Forms.DataGridView();
      this.colFieldName = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colDefault = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.cmsFields = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.miAdd = new System.Windows.Forms.ToolStripMenuItem();
      this.miAddFromResult = new System.Windows.Forms.ToolStripMenuItem();
      this.miDelete = new System.Windows.Forms.ToolStripMenuItem();
      this.miDeleteAll = new System.Windows.Forms.ToolStripMenuItem();
      this.status = new System.Windows.Forms.StatusStrip();
      this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
      this.cmsResult = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.miExcel = new System.Windows.Forms.ToolStripMenuItem();
      this.tableLayoutPanel1.SuspendLayout();
      this.panel1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.tabs.SuspendLayout();
      this.tabPageXslt.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.tbXslt)).BeginInit();
      this.tabPageXml.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.tbResult)).BeginInit();
      this.tabPageResult.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dgResult)).BeginInit();
      this.tabPageMap.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dgFields)).BeginInit();
      this.cmsFields.SuspendLayout();
      this.status.SuspendLayout();
      this.cmsResult.SuspendLayout();
      this.SuspendLayout();
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 1;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
      this.tableLayoutPanel1.Controls.Add(this.tabs, 0, 1);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 2;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 190F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tableLayoutPanel1.Size = new System.Drawing.Size(672, 540);
      this.tableLayoutPanel1.TabIndex = 0;
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.cbUseFieldsMap);
      this.panel1.Controls.Add(this.cbFromAttrib);
      this.panel1.Controls.Add(this.tbPathRow);
      this.panel1.Controls.Add(this.label3);
      this.panel1.Controls.Add(this.tbCodepage);
      this.panel1.Controls.Add(this.label2);
      this.panel1.Controls.Add(this.groupBox1);
      this.panel1.Controls.Add(this.bConvert);
      this.panel1.Controls.Add(this.tbNameXml);
      this.panel1.Controls.Add(this.label1);
      this.panel1.Controls.Add(this.bSelectFileXml);
      this.panel1.Controls.Add(this.cbFromTag);
      this.panel1.Controls.Add(this.groupBox2);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(3, 3);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(666, 184);
      this.panel1.TabIndex = 0;
      // 
      // cbUseFieldsMap
      // 
      this.cbUseFieldsMap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.cbUseFieldsMap.AutoSize = true;
      this.cbUseFieldsMap.Location = new System.Drawing.Point(554, 134);
      this.cbUseFieldsMap.Name = "cbUseFieldsMap";
      this.cbUseFieldsMap.Size = new System.Drawing.Size(101, 17);
      this.cbUseFieldsMap.TabIndex = 10;
      this.cbUseFieldsMap.Text = "Use fields map";
      this.cbUseFieldsMap.UseVisualStyleBackColor = true;
      this.cbUseFieldsMap.CheckedChanged += new System.EventHandler(this.cbUseFieldsMap_CheckedChanged);
      // 
      // cbFromAttrib
      // 
      this.cbFromAttrib.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.cbFromAttrib.AutoSize = true;
      this.cbFromAttrib.Checked = true;
      this.cbFromAttrib.CheckState = System.Windows.Forms.CheckState.Checked;
      this.cbFromAttrib.Location = new System.Drawing.Point(419, 134);
      this.cbFromAttrib.Name = "cbFromAttrib";
      this.cbFromAttrib.Size = new System.Drawing.Size(130, 17);
      this.cbFromAttrib.TabIndex = 9;
      this.cbFromAttrib.Text = "Data from attributes";
      this.cbFromAttrib.UseVisualStyleBackColor = true;
      this.cbFromAttrib.CheckedChanged += new System.EventHandler(this.cbFrom_CheckedChanged);
      // 
      // tbPathRow
      // 
      this.tbPathRow.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tbPathRow.BackColor = System.Drawing.SystemColors.Window;
      this.tbPathRow.Location = new System.Drawing.Point(112, 131);
      this.tbPathRow.Name = "tbPathRow";
      this.tbPathRow.Size = new System.Drawing.Size(191, 22);
      this.tbPathRow.TabIndex = 7;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(3, 134);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(105, 13);
      this.label3.TabIndex = 6;
      this.label3.Text = "Path to tag for row";
      // 
      // tbCodepage
      // 
      this.tbCodepage.Location = new System.Drawing.Point(117, 36);
      this.tbCodepage.Name = "tbCodepage";
      this.tbCodepage.Size = new System.Drawing.Size(52, 22);
      this.tbCodepage.TabIndex = 4;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(55, 39);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(60, 13);
      this.label2.TabIndex = 3;
      this.label2.Text = "Codepage";
      // 
      // groupBox1
      // 
      this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.groupBox1.Location = new System.Drawing.Point(0, 182);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(666, 2);
      this.groupBox1.TabIndex = 12;
      this.groupBox1.TabStop = false;
      // 
      // bConvert
      // 
      this.bConvert.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
      this.bConvert.FlatAppearance.BorderSize = 0;
      this.bConvert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.bConvert.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.bConvert.Image = global::Sources.Properties.Resources.Check;
      this.bConvert.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.bConvert.Location = new System.Drawing.Point(6, 153);
      this.bConvert.Name = "bConvert";
      this.bConvert.Size = new System.Drawing.Size(84, 26);
      this.bConvert.TabIndex = 11;
      this.bConvert.Text = " Convert";
      this.bConvert.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.bConvert.UseVisualStyleBackColor = true;
      this.bConvert.Click += new System.EventHandler(this.bConvert_Click);
      // 
      // tbNameXml
      // 
      this.tbNameXml.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tbNameXml.BackColor = System.Drawing.SystemColors.Window;
      this.tbNameXml.Location = new System.Drawing.Point(58, 9);
      this.tbNameXml.Name = "tbNameXml";
      this.tbNameXml.Size = new System.Drawing.Size(577, 22);
      this.tbNameXml.TabIndex = 0;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(3, 12);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(49, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "XML File";
      // 
      // bSelectFileXml
      // 
      this.bSelectFileXml.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.bSelectFileXml.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
      this.bSelectFileXml.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
      this.bSelectFileXml.FlatAppearance.BorderSize = 0;
      this.bSelectFileXml.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.bSelectFileXml.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.bSelectFileXml.Image = global::Sources.Properties.Resources.selectfile;
      this.bSelectFileXml.Location = new System.Drawing.Point(638, 11);
      this.bSelectFileXml.Margin = new System.Windows.Forms.Padding(0);
      this.bSelectFileXml.Name = "bSelectFileXml";
      this.bSelectFileXml.Size = new System.Drawing.Size(19, 18);
      this.bSelectFileXml.TabIndex = 2;
      this.bSelectFileXml.UseVisualStyleBackColor = true;
      this.bSelectFileXml.Click += new System.EventHandler(this.bSelectFileXml_Click);
      // 
      // cbFromTag
      // 
      this.cbFromTag.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.cbFromTag.AutoSize = true;
      this.cbFromTag.Checked = true;
      this.cbFromTag.CheckState = System.Windows.Forms.CheckState.Checked;
      this.cbFromTag.Location = new System.Drawing.Point(311, 134);
      this.cbFromTag.Name = "cbFromTag";
      this.cbFromTag.Size = new System.Drawing.Size(102, 17);
      this.cbFromTag.TabIndex = 8;
      this.cbFromTag.Text = "Data from tags";
      this.cbFromTag.UseVisualStyleBackColor = true;
      this.cbFromTag.CheckedChanged += new System.EventHandler(this.cbFrom_CheckedChanged);
      // 
      // groupBox2
      // 
      this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox2.Controls.Add(this.label4);
      this.groupBox2.Controls.Add(this.rbXsltNotUse);
      this.groupBox2.Controls.Add(this.tbNameXslt);
      this.groupBox2.Controls.Add(this.bSelectFileXslt);
      this.groupBox2.Controls.Add(this.rbXsltScript);
      this.groupBox2.Controls.Add(this.rbXsltFile);
      this.groupBox2.Location = new System.Drawing.Point(6, 55);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(654, 70);
      this.groupBox2.TabIndex = 5;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = " XSLT ";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(6, 43);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(50, 13);
      this.label4.TabIndex = 3;
      this.label4.Text = "XSLT File";
      // 
      // rbXsltNotUse
      // 
      this.rbXsltNotUse.Checked = true;
      this.rbXsltNotUse.Location = new System.Drawing.Point(9, 17);
      this.rbXsltNotUse.Name = "rbXsltNotUse";
      this.rbXsltNotUse.Size = new System.Drawing.Size(65, 17);
      this.rbXsltNotUse.TabIndex = 0;
      this.rbXsltNotUse.TabStop = true;
      this.rbXsltNotUse.Text = "Not use";
      this.rbXsltNotUse.UseVisualStyleBackColor = true;
      this.rbXsltNotUse.CheckedChanged += new System.EventHandler(this.rbXslt_CheckedChanged);
      // 
      // tbNameXslt
      // 
      this.tbNameXslt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tbNameXslt.BackColor = System.Drawing.SystemColors.Window;
      this.tbNameXslt.Location = new System.Drawing.Point(61, 40);
      this.tbNameXslt.Name = "tbNameXslt";
      this.tbNameXslt.Size = new System.Drawing.Size(568, 22);
      this.tbNameXslt.TabIndex = 4;
      // 
      // bSelectFileXslt
      // 
      this.bSelectFileXslt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.bSelectFileXslt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
      this.bSelectFileXslt.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
      this.bSelectFileXslt.FlatAppearance.BorderSize = 0;
      this.bSelectFileXslt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.bSelectFileXslt.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.bSelectFileXslt.Image = global::Sources.Properties.Resources.selectfile;
      this.bSelectFileXslt.Location = new System.Drawing.Point(632, 42);
      this.bSelectFileXslt.Margin = new System.Windows.Forms.Padding(0);
      this.bSelectFileXslt.Name = "bSelectFileXslt";
      this.bSelectFileXslt.Size = new System.Drawing.Size(19, 18);
      this.bSelectFileXslt.TabIndex = 5;
      this.bSelectFileXslt.UseVisualStyleBackColor = true;
      this.bSelectFileXslt.Click += new System.EventHandler(this.bSelectFileXslt_Click);
      // 
      // rbXsltScript
      // 
      this.rbXsltScript.Location = new System.Drawing.Point(82, 17);
      this.rbXsltScript.Name = "rbXsltScript";
      this.rbXsltScript.Size = new System.Drawing.Size(83, 17);
      this.rbXsltScript.TabIndex = 1;
      this.rbXsltScript.Text = "From Script";
      this.rbXsltScript.UseVisualStyleBackColor = true;
      this.rbXsltScript.CheckedChanged += new System.EventHandler(this.rbXslt_CheckedChanged);
      // 
      // rbXsltFile
      // 
      this.rbXsltFile.Location = new System.Drawing.Point(173, 17);
      this.rbXsltFile.Name = "rbXsltFile";
      this.rbXsltFile.Size = new System.Drawing.Size(73, 17);
      this.rbXsltFile.TabIndex = 2;
      this.rbXsltFile.Text = "From File";
      this.rbXsltFile.UseVisualStyleBackColor = true;
      this.rbXsltFile.CheckedChanged += new System.EventHandler(this.rbXslt_CheckedChanged);
      // 
      // tabs
      // 
      this.tabs.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
      this.tabs.Controls.Add(this.tabPageXslt);
      this.tabs.Controls.Add(this.tabPageXml);
      this.tabs.Controls.Add(this.tabPageResult);
      this.tabs.Controls.Add(this.tabPageMap);
      this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabs.Location = new System.Drawing.Point(3, 190);
      this.tabs.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
      this.tabs.Multiline = true;
      this.tabs.Name = "tabs";
      this.tabs.SelectedIndex = 0;
      this.tabs.Size = new System.Drawing.Size(669, 350);
      this.tabs.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
      this.tabs.TabIndex = 1;
      this.tabs.SelectedIndexChanged += new System.EventHandler(this.tabs_SelectedIndexChanged);
      // 
      // tabPageXslt
      // 
      this.tabPageXslt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.tabPageXslt.Controls.Add(this.tbXslt);
      this.tabPageXslt.Location = new System.Drawing.Point(4, 25);
      this.tabPageXslt.Name = "tabPageXslt";
      this.tabPageXslt.Size = new System.Drawing.Size(661, 321);
      this.tabPageXslt.TabIndex = 0;
      this.tabPageXslt.Text = "XSLT Script";
      // 
      // tbXslt
      // 
      this.tbXslt.AutoScrollMinSize = new System.Drawing.Size(2, 12);
      this.tbXslt.BackBrush = null;
      this.tbXslt.CommentPrefix = null;
      this.tbXslt.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.tbXslt.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
      this.tbXslt.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tbXslt.Font = new System.Drawing.Font("Consolas", 8F);
      this.tbXslt.IsReplaceMode = false;
      this.tbXslt.Language = FastColoredTextBoxNS.Language.HTML;
      this.tbXslt.LeftBracket = '<';
      this.tbXslt.LeftBracket2 = '(';
      this.tbXslt.Location = new System.Drawing.Point(0, 0);
      this.tbXslt.Margin = new System.Windows.Forms.Padding(0);
      this.tbXslt.Name = "tbXslt";
      this.tbXslt.Paddings = new System.Windows.Forms.Padding(0);
      this.tbXslt.RightBracket = '>';
      this.tbXslt.RightBracket2 = ')';
      this.tbXslt.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
      this.tbXslt.ShowLineNumbers = false;
      this.tbXslt.Size = new System.Drawing.Size(659, 319);
      this.tbXslt.TabIndex = 0;
      this.tbXslt.TabLength = 2;
      // 
      // tabPageXml
      // 
      this.tabPageXml.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.tabPageXml.Controls.Add(this.tbResult);
      this.tabPageXml.Location = new System.Drawing.Point(4, 25);
      this.tabPageXml.Name = "tabPageXml";
      this.tabPageXml.Size = new System.Drawing.Size(661, 321);
      this.tabPageXml.TabIndex = 2;
      this.tabPageXml.Text = "Prepared XML";
      // 
      // tbResult
      // 
      this.tbResult.AutoScrollMinSize = new System.Drawing.Size(2, 12);
      this.tbResult.BackBrush = null;
      this.tbResult.BackColor = System.Drawing.SystemColors.Control;
      this.tbResult.CommentPrefix = null;
      this.tbResult.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.tbResult.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
      this.tbResult.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tbResult.Font = new System.Drawing.Font("Consolas", 8F);
      this.tbResult.IsReplaceMode = false;
      this.tbResult.Language = FastColoredTextBoxNS.Language.HTML;
      this.tbResult.LeftBracket = '<';
      this.tbResult.LeftBracket2 = '(';
      this.tbResult.Location = new System.Drawing.Point(0, 0);
      this.tbResult.Margin = new System.Windows.Forms.Padding(0);
      this.tbResult.Name = "tbResult";
      this.tbResult.Paddings = new System.Windows.Forms.Padding(0);
      this.tbResult.ReadOnly = true;
      this.tbResult.RightBracket = '>';
      this.tbResult.RightBracket2 = ')';
      this.tbResult.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
      this.tbResult.ShowLineNumbers = false;
      this.tbResult.Size = new System.Drawing.Size(659, 319);
      this.tbResult.TabIndex = 0;
      this.tbResult.TabLength = 2;
      // 
      // tabPageResult
      // 
      this.tabPageResult.Controls.Add(this.dgResult);
      this.tabPageResult.Location = new System.Drawing.Point(4, 25);
      this.tabPageResult.Name = "tabPageResult";
      this.tabPageResult.Size = new System.Drawing.Size(661, 321);
      this.tabPageResult.TabIndex = 1;
      this.tabPageResult.Text = "Result";
      // 
      // dgResult
      // 
      this.dgResult.AllowUserToAddRows = false;
      this.dgResult.AllowUserToDeleteRows = false;
      this.dgResult.AllowUserToOrderColumns = true;
      this.dgResult.BackgroundColor = System.Drawing.SystemColors.Control;
      this.dgResult.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.dgResult.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
      this.dgResult.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dgResult.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
      dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle2.NullValue = "[NULL]";
      dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.dgResult.DefaultCellStyle = dataGridViewCellStyle2;
      this.dgResult.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dgResult.EnableHeadersVisualStyles = false;
      this.dgResult.Location = new System.Drawing.Point(0, 0);
      this.dgResult.Name = "dgResult";
      this.dgResult.ReadOnly = true;
      this.dgResult.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
      dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
      dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dgResult.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
      this.dgResult.RowHeadersWidth = 23;
      this.dgResult.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
      this.dgResult.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
      this.dgResult.Size = new System.Drawing.Size(661, 321);
      this.dgResult.TabIndex = 0;
      // 
      // tabPageMap
      // 
      this.tabPageMap.Controls.Add(this.dgFields);
      this.tabPageMap.Location = new System.Drawing.Point(4, 25);
      this.tabPageMap.Name = "tabPageMap";
      this.tabPageMap.Size = new System.Drawing.Size(661, 321);
      this.tabPageMap.TabIndex = 3;
      this.tabPageMap.Text = "Fields Map";
      // 
      // dgFields
      // 
      this.dgFields.AllowUserToAddRows = false;
      this.dgFields.AllowUserToResizeRows = false;
      this.dgFields.BackgroundColor = System.Drawing.SystemColors.Control;
      this.dgFields.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.dgFields.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
      dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
      dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      this.dgFields.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
      this.dgFields.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
      this.dgFields.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colFieldName,
            this.colPath,
            this.colDefault});
      this.dgFields.ContextMenuStrip = this.cmsFields;
      this.dgFields.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dgFields.EnableHeadersVisualStyles = false;
      this.dgFields.Location = new System.Drawing.Point(0, 0);
      this.dgFields.MultiSelect = false;
      this.dgFields.Name = "dgFields";
      this.dgFields.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
      dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
      dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dgFields.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
      this.dgFields.RowHeadersVisible = false;
      this.dgFields.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
      this.dgFields.ShowEditingIcon = false;
      this.dgFields.Size = new System.Drawing.Size(661, 321);
      this.dgFields.TabIndex = 0;
      this.dgFields.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgFields_CellEndEdit);
      this.dgFields.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgFields_RowValidating);
      this.dgFields.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgFields_KeyDown);
      this.dgFields.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgFields_MouseDown);
      // 
      // colFieldName
      // 
      this.colFieldName.FillWeight = 152.2843F;
      this.colFieldName.HeaderText = "Field Name";
      this.colFieldName.MinimumWidth = 100;
      this.colFieldName.Name = "colFieldName";
      this.colFieldName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
      // 
      // colPath
      // 
      this.colPath.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.colPath.FillWeight = 145.1801F;
      this.colPath.HeaderText = "Path to node for field";
      this.colPath.MinimumWidth = 200;
      this.colPath.Name = "colPath";
      this.colPath.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
      this.colPath.ToolTipText = "XPath from row\'s definition tag to tag or attribute for field definition";
      // 
      // colDefault
      // 
      this.colDefault.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      dataGridViewCellStyle5.NullValue = "[NULL]";
      this.colDefault.DefaultCellStyle = dataGridViewCellStyle5;
      this.colDefault.FillWeight = 2.5356F;
      this.colDefault.HeaderText = "Default value";
      this.colDefault.MinimumWidth = 100;
      this.colDefault.Name = "colDefault";
      this.colDefault.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
      // 
      // cmsFields
      // 
      this.cmsFields.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miAdd,
            this.miAddFromResult,
            this.miDelete,
            this.miDeleteAll});
      this.cmsFields.Name = "cmsFields";
      this.cmsFields.Size = new System.Drawing.Size(189, 92);
      this.cmsFields.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.cmsFields_ItemClicked);
      // 
      // miAdd
      // 
      this.miAdd.Name = "miAdd";
      this.miAdd.Size = new System.Drawing.Size(188, 22);
      this.miAdd.Text = "Add field (Ctrl+Ins)";
      // 
      // miAddFromResult
      // 
      this.miAddFromResult.Name = "miAddFromResult";
      this.miAddFromResult.Size = new System.Drawing.Size(188, 22);
      this.miAddFromResult.Text = "Add from Result";
      // 
      // miDelete
      // 
      this.miDelete.Name = "miDelete";
      this.miDelete.Size = new System.Drawing.Size(188, 22);
      this.miDelete.Text = "Delete field (Ctrl+Del)";
      // 
      // miDeleteAll
      // 
      this.miDeleteAll.Name = "miDeleteAll";
      this.miDeleteAll.Size = new System.Drawing.Size(188, 22);
      this.miDeleteAll.Text = "Delete All";
      // 
      // status
      // 
      this.status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
      this.status.Location = new System.Drawing.Point(0, 540);
      this.status.Name = "status";
      this.status.Size = new System.Drawing.Size(672, 22);
      this.status.TabIndex = 1;
      this.status.Text = "statusStrip1";
      // 
      // statusLabel
      // 
      this.statusLabel.Name = "statusLabel";
      this.statusLabel.Size = new System.Drawing.Size(0, 17);
      // 
      // cmsResult
      // 
      this.cmsResult.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miExcel});
      this.cmsResult.Name = "cmsFields";
      this.cmsResult.Size = new System.Drawing.Size(146, 26);
      this.cmsResult.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.cmsResult_ItemClicked);
      // 
      // miExcel
      // 
      this.miExcel.Name = "miExcel";
      this.miExcel.Size = new System.Drawing.Size(145, 22);
      this.miExcel.Text = "Open in Excel";
      // 
      // FormXml
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(672, 562);
      this.Controls.Add(this.tableLayoutPanel1);
      this.Controls.Add(this.status);
      this.DoubleBuffered = true;
      this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.KeyPreview = true;
      this.MinimumSize = new System.Drawing.Size(600, 550);
      this.Name = "FormXml";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "XML to DataTable converter";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormXslt_FormClosing);
      this.Load += new System.EventHandler(this.FormXslt_Load);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormXslt_KeyDown);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.tabs.ResumeLayout(false);
      this.tabPageXslt.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.tbXslt)).EndInit();
      this.tabPageXml.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.tbResult)).EndInit();
      this.tabPageResult.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dgResult)).EndInit();
      this.tabPageMap.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dgFields)).EndInit();
      this.cmsFields.ResumeLayout(false);
      this.status.ResumeLayout(false);
      this.status.PerformLayout();
      this.cmsResult.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.TabControl tabs;
    private System.Windows.Forms.TabPage tabPageXslt;
    private FastColoredTextBoxNS.FastColoredTextBox tbXslt;
    private System.Windows.Forms.TabPage tabPageResult;
    public System.Windows.Forms.Button bSelectFileXslt;
    public System.Windows.Forms.TextBox tbNameXml;
    public System.Windows.Forms.TextBox tbNameXslt;
    private System.Windows.Forms.RadioButton rbXsltFile;
    private System.Windows.Forms.RadioButton rbXsltScript;
    public System.Windows.Forms.Button bSelectFileXml;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.GroupBox groupBox1;
    public System.Windows.Forms.Button bConvert;
    public System.Windows.Forms.TextBox tbCodepage;
    private System.Windows.Forms.Label label2;
    public System.Windows.Forms.TextBox tbPathRow;
    private System.Windows.Forms.Label label3;
    public System.Windows.Forms.CheckBox cbFromAttrib;
    public System.Windows.Forms.CheckBox cbFromTag;
    private System.Windows.Forms.TabPage tabPageXml;
    private FastColoredTextBoxNS.FastColoredTextBox tbResult;
    private System.Windows.Forms.DataGridView dgResult;
    public System.Windows.Forms.CheckBox cbUseFieldsMap;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.RadioButton rbXsltNotUse;
    private System.Windows.Forms.TabPage tabPageMap;
    private System.Windows.Forms.DataGridView dgFields;
    private System.Windows.Forms.ContextMenuStrip cmsFields;
    private System.Windows.Forms.ToolStripMenuItem miAdd;
    private System.Windows.Forms.ToolStripMenuItem miDelete;
    private System.Windows.Forms.ToolStripMenuItem miAddFromResult;
    private System.Windows.Forms.ToolStripMenuItem miDeleteAll;
    private System.Windows.Forms.StatusStrip status;
    private System.Windows.Forms.ToolStripStatusLabel statusLabel;
    private System.Windows.Forms.DataGridViewTextBoxColumn colFieldName;
    private System.Windows.Forms.DataGridViewTextBoxColumn colPath;
    private System.Windows.Forms.DataGridViewTextBoxColumn colDefault;
    private System.Windows.Forms.ContextMenuStrip cmsResult;
    private System.Windows.Forms.ToolStripMenuItem miExcel;
  }
}