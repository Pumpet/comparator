namespace SqlSource
{
  partial class SqlView
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

    #region Код, автоматически созданный конструктором форм Windows

    /// <summary>
    /// Обязательный метод для поддержки конструктора - не изменяйте
    /// содержимое данного метода при помощи редактора кода.
    /// </summary>
    private void InitializeComponent()
    {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SqlView));
            this.splitH = new System.Windows.Forms.SplitContainer();
            this.pSource = new System.Windows.Forms.Panel();
            this.tpSource = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tbServer = new System.Windows.Forms.TextBox();
            this.tbDB = new System.Windows.Forms.TextBox();
            this.cbProvider = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tbLogin = new System.Windows.Forms.TextBox();
            this.tbPwd = new System.Windows.Forms.MaskedTextBox();
            this.tbConnStr = new System.Windows.Forms.TextBox();
            this.bTestConn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.pCommandTimeout = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.numCommandTimeout = new System.Windows.Forms.NumericUpDown();
            this.splitV = new System.Windows.Forms.SplitContainer();
            this.txtSQL = new FastColoredTextBoxNS.FastColoredTextBox();
            this.pTabs = new System.Windows.Forms.Panel();
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabResult = new System.Windows.Forms.TabPage();
            this.dgResult = new System.Windows.Forms.DataGridView();
            this.tabFields = new System.Windows.Forms.TabPage();
            this.dgFields = new System.Windows.Forms.DataGridView();
            this.ColName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pBar = new System.Windows.Forms.ProgressBar();
            this.pTop = new System.Windows.Forms.ToolStrip();
            this.bSource = new System.Windows.Forms.ToolStripButton();
            this.bData = new System.Windows.Forms.ToolStripButton();
            this.bStop = new System.Windows.Forms.ToolStripButton();
            this.bExcel = new System.Windows.Forms.ToolStripButton();
            this.pBottom = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.splitH)).BeginInit();
            this.splitH.Panel1.SuspendLayout();
            this.splitH.Panel2.SuspendLayout();
            this.splitH.SuspendLayout();
            this.pSource.SuspendLayout();
            this.tpSource.SuspendLayout();
            this.pCommandTimeout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCommandTimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitV)).BeginInit();
            this.splitV.Panel1.SuspendLayout();
            this.splitV.Panel2.SuspendLayout();
            this.splitV.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSQL)).BeginInit();
            this.pTabs.SuspendLayout();
            this.tabs.SuspendLayout();
            this.tabResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgResult)).BeginInit();
            this.tabFields.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgFields)).BeginInit();
            this.pTop.SuspendLayout();
            this.pBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitH
            // 
            this.splitH.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitH.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitH.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.splitH.Location = new System.Drawing.Point(0, 25);
            this.splitH.Name = "splitH";
            // 
            // splitH.Panel1
            // 
            this.splitH.Panel1.Controls.Add(this.pSource);
            this.splitH.Panel1MinSize = 200;
            // 
            // splitH.Panel2
            // 
            this.splitH.Panel2.Controls.Add(this.splitV);
            this.splitH.Panel2MinSize = 200;
            this.splitH.Size = new System.Drawing.Size(784, 449);
            this.splitH.SplitterDistance = 205;
            this.splitH.TabIndex = 1;
            this.splitH.TabStop = false;
            // 
            // pSource
            // 
            this.pSource.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pSource.Controls.Add(this.tpSource);
            this.pSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pSource.Location = new System.Drawing.Point(0, 0);
            this.pSource.Name = "pSource";
            this.pSource.Size = new System.Drawing.Size(205, 449);
            this.pSource.TabIndex = 0;
            // 
            // tpSource
            // 
            this.tpSource.BackColor = System.Drawing.SystemColors.Control;
            this.tpSource.ColumnCount = 2;
            this.tpSource.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.tpSource.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tpSource.Controls.Add(this.label1, 0, 1);
            this.tpSource.Controls.Add(this.label7, 0, 2);
            this.tpSource.Controls.Add(this.label8, 0, 3);
            this.tpSource.Controls.Add(this.tbServer, 1, 2);
            this.tpSource.Controls.Add(this.tbDB, 1, 3);
            this.tpSource.Controls.Add(this.cbProvider, 1, 1);
            this.tpSource.Controls.Add(this.label9, 0, 4);
            this.tpSource.Controls.Add(this.label10, 0, 5);
            this.tpSource.Controls.Add(this.tbLogin, 1, 4);
            this.tpSource.Controls.Add(this.tbPwd, 1, 5);
            this.tpSource.Controls.Add(this.tbConnStr, 0, 8);
            this.tpSource.Controls.Add(this.bTestConn, 1, 11);
            this.tpSource.Controls.Add(this.label2, 0, 6);
            this.tpSource.Controls.Add(this.pCommandTimeout, 0, 12);
            this.tpSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tpSource.Location = new System.Drawing.Point(0, 0);
            this.tpSource.Name = "tpSource";
            this.tpSource.RowCount = 14;
            this.tpSource.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tpSource.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tpSource.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tpSource.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tpSource.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tpSource.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tpSource.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tpSource.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tpSource.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tpSource.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tpSource.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tpSource.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tpSource.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tpSource.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tpSource.Size = new System.Drawing.Size(201, 445);
            this.tpSource.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 27);
            this.label1.TabIndex = 1;
            this.label1.Text = "Provider";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(3, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 28);
            this.label7.TabIndex = 3;
            this.label7.Text = "Server";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Location = new System.Drawing.Point(3, 55);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 28);
            this.label8.TabIndex = 5;
            this.label8.Text = "Database";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbServer
            // 
            this.tbServer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbServer.Location = new System.Drawing.Point(68, 30);
            this.tbServer.Name = "tbServer";
            this.tbServer.Size = new System.Drawing.Size(130, 22);
            this.tbServer.TabIndex = 1;
            // 
            // tbDB
            // 
            this.tbDB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbDB.Location = new System.Drawing.Point(68, 58);
            this.tbDB.Name = "tbDB";
            this.tbDB.Size = new System.Drawing.Size(130, 22);
            this.tbDB.TabIndex = 2;
            // 
            // cbProvider
            // 
            this.cbProvider.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProvider.FormattingEnabled = true;
            this.cbProvider.Location = new System.Drawing.Point(68, 3);
            this.cbProvider.Name = "cbProvider";
            this.cbProvider.Size = new System.Drawing.Size(130, 21);
            this.cbProvider.TabIndex = 0;
            this.cbProvider.SelectedIndexChanged += new System.EventHandler(this.cbProvider_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Location = new System.Drawing.Point(3, 83);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 28);
            this.label9.TabIndex = 7;
            this.label9.Text = "Login";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Location = new System.Drawing.Point(3, 111);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 28);
            this.label10.TabIndex = 9;
            this.label10.Text = "Password";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbLogin
            // 
            this.tbLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbLogin.Location = new System.Drawing.Point(68, 86);
            this.tbLogin.Name = "tbLogin";
            this.tbLogin.Size = new System.Drawing.Size(130, 22);
            this.tbLogin.TabIndex = 3;
            // 
            // tbPwd
            // 
            this.tbPwd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbPwd.Location = new System.Drawing.Point(68, 114);
            this.tbPwd.Name = "tbPwd";
            this.tbPwd.Size = new System.Drawing.Size(130, 22);
            this.tbPwd.TabIndex = 4;
            this.tbPwd.UseSystemPasswordChar = true;
            // 
            // tbConnStr
            // 
            this.tpSource.SetColumnSpan(this.tbConnStr, 2);
            this.tbConnStr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbConnStr.Enabled = false;
            this.tbConnStr.Location = new System.Drawing.Point(3, 164);
            this.tbConnStr.Multiline = true;
            this.tbConnStr.Name = "tbConnStr";
            this.tpSource.SetRowSpan(this.tbConnStr, 3);
            this.tbConnStr.Size = new System.Drawing.Size(195, 131);
            this.tbConnStr.TabIndex = 5;
            // 
            // bTestConn
            // 
            this.bTestConn.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.bTestConn.FlatAppearance.BorderSize = 0;
            this.bTestConn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bTestConn.Image = global::SqlSource.Properties.Resources.Check;
            this.bTestConn.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.bTestConn.Location = new System.Drawing.Point(81, 301);
            this.bTestConn.Name = "bTestConn";
            this.bTestConn.Size = new System.Drawing.Size(117, 24);
            this.bTestConn.TabIndex = 6;
            this.bTestConn.Text = " Test Connection";
            this.bTestConn.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.bTestConn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bTestConn.UseVisualStyleBackColor = true;
            this.bTestConn.Click += new System.EventHandler(this.bTestConn_Click);
            // 
            // label2
            // 
            this.tpSource.SetColumnSpan(this.label2, 2);
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 139);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(195, 22);
            this.label2.TabIndex = 13;
            this.label2.Text = "Connection string";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pCommandTimeout
            // 
            this.pCommandTimeout.ColumnCount = 2;
            this.tpSource.SetColumnSpan(this.pCommandTimeout, 2);
            this.pCommandTimeout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 69.23077F));
            this.pCommandTimeout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.76923F));
            this.pCommandTimeout.Controls.Add(this.label3, 0, 0);
            this.pCommandTimeout.Controls.Add(this.numCommandTimeout, 1, 0);
            this.pCommandTimeout.Location = new System.Drawing.Point(3, 331);
            this.pCommandTimeout.Name = "pCommandTimeout";
            this.pCommandTimeout.RowCount = 1;
            this.pCommandTimeout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.pCommandTimeout.Size = new System.Drawing.Size(195, 29);
            this.pCommandTimeout.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 29);
            this.label3.TabIndex = 0;
            this.label3.Text = "Command Timeout (sec)";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numCommandTimeout
            // 
            this.numCommandTimeout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numCommandTimeout.Location = new System.Drawing.Point(137, 3);
            this.numCommandTimeout.Maximum = new decimal(new int[] {
            86400,
            0,
            0,
            0});
            this.numCommandTimeout.Name = "numCommandTimeout";
            this.numCommandTimeout.Size = new System.Drawing.Size(55, 22);
            this.numCommandTimeout.TabIndex = 1;
            this.numCommandTimeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // splitV
            // 
            this.splitV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitV.Location = new System.Drawing.Point(0, 0);
            this.splitV.Name = "splitV";
            this.splitV.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitV.Panel1
            // 
            this.splitV.Panel1.Controls.Add(this.txtSQL);
            this.splitV.Panel1MinSize = 90;
            // 
            // splitV.Panel2
            // 
            this.splitV.Panel2.Controls.Add(this.pTabs);
            this.splitV.Panel2MinSize = 90;
            this.splitV.Size = new System.Drawing.Size(575, 449);
            this.splitV.SplitterDistance = 249;
            this.splitV.TabIndex = 0;
            this.splitV.TabStop = false;
            // 
            // txtSQL
            // 
            this.txtSQL.AutoScrollMinSize = new System.Drawing.Size(8, 21);
            this.txtSQL.BackBrush = null;
            this.txtSQL.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtSQL.CommentPrefix = "--";
            this.txtSQL.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSQL.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.txtSQL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSQL.Font = new System.Drawing.Font("Consolas", 10F);
            this.txtSQL.IsReplaceMode = false;
            this.txtSQL.Language = FastColoredTextBoxNS.Language.SQL;
            this.txtSQL.LeftBracket = '(';
            this.txtSQL.Location = new System.Drawing.Point(0, 0);
            this.txtSQL.Name = "txtSQL";
            this.txtSQL.Paddings = new System.Windows.Forms.Padding(3);
            this.txtSQL.RightBracket = ')';
            this.txtSQL.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.txtSQL.ShowLineNumbers = false;
            this.txtSQL.Size = new System.Drawing.Size(575, 249);
            this.txtSQL.TabIndex = 0;
            this.txtSQL.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSQL_KeyDown);
            // 
            // pTabs
            // 
            this.pTabs.Controls.Add(this.tabs);
            this.pTabs.Controls.Add(this.pBar);
            this.pTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pTabs.Location = new System.Drawing.Point(0, 0);
            this.pTabs.Name = "pTabs";
            this.pTabs.Size = new System.Drawing.Size(575, 196);
            this.pTabs.TabIndex = 0;
            // 
            // tabs
            // 
            this.tabs.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabs.Controls.Add(this.tabResult);
            this.tabs.Controls.Add(this.tabFields);
            this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabs.Location = new System.Drawing.Point(0, 0);
            this.tabs.Multiline = true;
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(575, 188);
            this.tabs.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabs.TabIndex = 0;
            // 
            // tabResult
            // 
            this.tabResult.Controls.Add(this.dgResult);
            this.tabResult.Location = new System.Drawing.Point(4, 25);
            this.tabResult.Name = "tabResult";
            this.tabResult.Size = new System.Drawing.Size(567, 159);
            this.tabResult.TabIndex = 0;
            this.tabResult.Text = "Result";
            this.tabResult.UseVisualStyleBackColor = true;
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
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgResult.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgResult.RowHeadersWidth = 23;
            this.dgResult.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgResult.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgResult.Size = new System.Drawing.Size(567, 159);
            this.dgResult.TabIndex = 0;
            this.dgResult.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgResult_KeyDown);
            // 
            // tabFields
            // 
            this.tabFields.Controls.Add(this.dgFields);
            this.tabFields.Location = new System.Drawing.Point(4, 25);
            this.tabFields.Name = "tabFields";
            this.tabFields.Size = new System.Drawing.Size(567, 159);
            this.tabFields.TabIndex = 1;
            this.tabFields.Text = "Fields";
            this.tabFields.UseVisualStyleBackColor = true;
            // 
            // dgFields
            // 
            this.dgFields.AllowUserToAddRows = false;
            this.dgFields.AllowUserToDeleteRows = false;
            this.dgFields.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgFields.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgFields.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.dgFields.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgFields.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgFields.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgFields.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColName});
            this.dgFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgFields.EnableHeadersVisualStyles = false;
            this.dgFields.Location = new System.Drawing.Point(0, 0);
            this.dgFields.Name = "dgFields";
            this.dgFields.ReadOnly = true;
            this.dgFields.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgFields.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgFields.RowHeadersVisible = false;
            this.dgFields.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgFields.Size = new System.Drawing.Size(567, 159);
            this.dgFields.TabIndex = 1;
            // 
            // ColName
            // 
            this.ColName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColName.HeaderText = "Field name";
            this.ColName.Name = "ColName";
            this.ColName.ReadOnly = true;
            // 
            // pBar
            // 
            this.pBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pBar.Location = new System.Drawing.Point(0, 188);
            this.pBar.Maximum = 500;
            this.pBar.Name = "pBar";
            this.pBar.Size = new System.Drawing.Size(575, 8);
            this.pBar.Step = 1;
            this.pBar.TabIndex = 2;
            // 
            // pTop
            // 
            this.pTop.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.pTop.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bSource,
            this.bData,
            this.bStop,
            this.bExcel});
            this.pTop.Location = new System.Drawing.Point(0, 0);
            this.pTop.Name = "pTop";
            this.pTop.Size = new System.Drawing.Size(784, 25);
            this.pTop.TabIndex = 0;
            this.pTop.TabStop = true;
            this.pTop.Text = "toolStrip1";
            // 
            // bSource
            // 
            this.bSource.BackColor = System.Drawing.SystemColors.Control;
            this.bSource.Image = global::SqlSource.Properties.Resources.Collapse;
            this.bSource.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bSource.Name = "bSource";
            this.bSource.Size = new System.Drawing.Size(62, 22);
            this.bSource.Text = "Source";
            this.bSource.ToolTipText = "Source properties OnOff";
            this.bSource.Click += new System.EventHandler(this.bSource_Click);
            // 
            // bData
            // 
            this.bData.Image = global::SqlSource.Properties.Resources.Go;
            this.bData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bData.Name = "bData";
            this.bData.Size = new System.Drawing.Size(49, 22);
            this.bData.Text = "Exec";
            this.bData.ToolTipText = "Execute query (F5)";
            this.bData.Click += new System.EventHandler(this.bData_Click);
            // 
            // bStop
            // 
            this.bStop.Enabled = false;
            this.bStop.ForeColor = System.Drawing.Color.DarkRed;
            this.bStop.Image = global::SqlSource.Properties.Resources.Stop;
            this.bStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bStop.Name = "bStop";
            this.bStop.Size = new System.Drawing.Size(51, 22);
            this.bStop.Text = "Stop";
            this.bStop.ToolTipText = "Stop (Shift+F5)";
            this.bStop.Click += new System.EventHandler(this.bStop_Click);
            // 
            // bExcel
            // 
            this.bExcel.Image = global::SqlSource.Properties.Resources.Excel;
            this.bExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bExcel.Name = "bExcel";
            this.bExcel.Size = new System.Drawing.Size(67, 22);
            this.bExcel.Text = "To Excel";
            this.bExcel.ToolTipText = "Result to Excel";
            this.bExcel.Click += new System.EventHandler(this.bExcel_Click);
            // 
            // pBottom
            // 
            this.pBottom.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.pBottom.Location = new System.Drawing.Point(0, 474);
            this.pBottom.Name = "pBottom";
            this.pBottom.Size = new System.Drawing.Size(784, 22);
            this.pBottom.TabIndex = 2;
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(41, 17);
            this.lblStatus.Text = "0 rows";
            // 
            // SqlView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 496);
            this.Controls.Add(this.splitH);
            this.Controls.Add(this.pTop);
            this.Controls.Add(this.pBottom);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(420, 380);
            this.Name = "SqlView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sql Query";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSource_FormClosing);
            this.Load += new System.EventHandler(this.FormSource_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormSource_KeyDown);
            this.splitH.Panel1.ResumeLayout(false);
            this.splitH.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitH)).EndInit();
            this.splitH.ResumeLayout(false);
            this.pSource.ResumeLayout(false);
            this.tpSource.ResumeLayout(false);
            this.tpSource.PerformLayout();
            this.pCommandTimeout.ResumeLayout(false);
            this.pCommandTimeout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCommandTimeout)).EndInit();
            this.splitV.Panel1.ResumeLayout(false);
            this.splitV.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitV)).EndInit();
            this.splitV.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtSQL)).EndInit();
            this.pTabs.ResumeLayout(false);
            this.tabs.ResumeLayout(false);
            this.tabResult.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgResult)).EndInit();
            this.tabFields.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgFields)).EndInit();
            this.pTop.ResumeLayout(false);
            this.pTop.PerformLayout();
            this.pBottom.ResumeLayout(false);
            this.pBottom.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.SplitContainer splitH;
    private System.Windows.Forms.SplitContainer splitV;
    private System.Windows.Forms.ToolStrip pTop;
    private System.Windows.Forms.ToolStripButton bSource;
    private System.Windows.Forms.ToolStripButton bData;
    private System.Windows.Forms.Panel pSource;
    private System.Windows.Forms.Panel pTabs;
    private System.Windows.Forms.TabControl tabs;
    private System.Windows.Forms.TabPage tabResult;
    private System.Windows.Forms.DataGridView dgResult;
    private System.Windows.Forms.TabPage tabFields;
    private System.Windows.Forms.DataGridView dgFields;
    private System.Windows.Forms.ToolStripButton bStop;
    private System.Windows.Forms.ProgressBar pBar;
    private System.Windows.Forms.ToolStripButton bExcel;
    private FastColoredTextBoxNS.FastColoredTextBox txtSQL;
    private System.Windows.Forms.StatusStrip pBottom;
    private System.Windows.Forms.ToolStripStatusLabel lblStatus;
    private System.Windows.Forms.TableLayoutPanel tpSource;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.TextBox tbServer;
    private System.Windows.Forms.TextBox tbDB;
    private System.Windows.Forms.ComboBox cbProvider;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.TextBox tbLogin;
    private System.Windows.Forms.MaskedTextBox tbPwd;
    private System.Windows.Forms.TextBox tbConnStr;
    private System.Windows.Forms.Button bTestConn;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TableLayoutPanel pCommandTimeout;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.NumericUpDown numCommandTimeout;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColName;

  }
}

