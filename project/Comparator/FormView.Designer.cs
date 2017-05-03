namespace Comparator
{
  partial class FormView
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormView));
      this.toolTop = new System.Windows.Forms.ToolStrip();
      this.bNew = new System.Windows.Forms.ToolStripDropDownButton();
      this.bNewNew = new System.Windows.Forms.ToolStripMenuItem();
      this.bNewCopy = new System.Windows.Forms.ToolStripMenuItem();
      this.bSave = new System.Windows.Forms.ToolStripButton();
      this.bLoad = new System.Windows.Forms.ToolStripDropDownButton();
      this.bCheck = new System.Windows.Forms.ToolStripButton();
      this.bRun = new System.Windows.Forms.ToolStripButton();
      this.bResult = new System.Windows.Forms.ToolStripButton();
      this.bPin = new System.Windows.Forms.ToolStripButton();
      this.status = new System.Windows.Forms.StatusStrip();
      this.lblMsg = new System.Windows.Forms.ToolStripStatusLabel();
      this.gbSourceB = new System.Windows.Forms.GroupBox();
      this.gbSourceA = new System.Windows.Forms.GroupBox();
      this.gbFields = new System.Windows.Forms.GroupBox();
      this.dgCols = new System.Windows.Forms.DataGridView();
      this.Key = new System.Windows.Forms.DataGridViewCheckBoxColumn();
      this.Match = new System.Windows.Forms.DataGridViewCheckBoxColumn();
      this.ColsA = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.ColsB = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.pFieldsCommands = new System.Windows.Forms.Panel();
      this.cbMatchAllPairs = new System.Windows.Forms.CheckBox();
      this.bDown = new System.Windows.Forms.Button();
      this.cbMatchInOrder = new System.Windows.Forms.CheckBox();
      this.bUp = new System.Windows.Forms.Button();
      this.bColSelect = new System.Windows.Forms.Button();
      this.bColDel = new System.Windows.Forms.Button();
      this.gbOptions = new System.Windows.Forms.GroupBox();
      this.cbTryConvert = new System.Windows.Forms.CheckBox();
      this.cbNullAsStr = new System.Windows.Forms.CheckBox();
      this.cbCaseSens = new System.Windows.Forms.CheckBox();
      this.cbCheckRepeats = new System.Windows.Forms.CheckBox();
      this.cbOnlyB = new System.Windows.Forms.CheckBox();
      this.cbOnlyA = new System.Windows.Forms.CheckBox();
      this.cbDiffOnly = new System.Windows.Forms.CheckBox();
      this.gbResultType = new System.Windows.Forms.GroupBox();
      this.rbResExcel = new System.Windows.Forms.RadioButton();
      this.rbResHTML = new System.Windows.Forms.RadioButton();
      this.tbSendTo = new System.Windows.Forms.TextBox();
      this.cbSend = new System.Windows.Forms.CheckBox();
      this.tableAll = new System.Windows.Forms.TableLayoutPanel();
      this.tableProfile = new System.Windows.Forms.TableLayoutPanel();
      this.tableSources = new System.Windows.Forms.TableLayoutPanel();
      this.tableOptions = new System.Windows.Forms.TableLayoutPanel();
      this.gbBatchOpt = new System.Windows.Forms.GroupBox();
      this.tbSubject = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.cbTimeInResFile = new System.Windows.Forms.CheckBox();
      this.cboxResMail = new System.Windows.Forms.ComboBox();
      this.label4 = new System.Windows.Forms.Label();
      this.tbResFile = new System.Windows.Forms.TextBox();
      this.tbResFolder = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.ttip = new System.Windows.Forms.ToolTip(this.components);
      this.toolTop.SuspendLayout();
      this.status.SuspendLayout();
      this.gbFields.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dgCols)).BeginInit();
      this.pFieldsCommands.SuspendLayout();
      this.gbOptions.SuspendLayout();
      this.gbResultType.SuspendLayout();
      this.tableAll.SuspendLayout();
      this.tableProfile.SuspendLayout();
      this.tableSources.SuspendLayout();
      this.tableOptions.SuspendLayout();
      this.gbBatchOpt.SuspendLayout();
      this.SuspendLayout();
      // 
      // toolTop
      // 
      this.toolTop.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bNew,
            this.bSave,
            this.bLoad,
            this.bCheck,
            this.bRun,
            this.bResult,
            this.bPin});
      this.toolTop.Location = new System.Drawing.Point(0, 0);
      this.toolTop.Name = "toolTop";
      this.toolTop.Size = new System.Drawing.Size(784, 25);
      this.toolTop.TabIndex = 1;
      this.toolTop.TabStop = true;
      this.toolTop.Text = "toolStrip1";
      // 
      // bNew
      // 
      this.bNew.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bNewNew,
            this.bNewCopy});
      this.bNew.Font = new System.Drawing.Font("Segoe UI", 8.25F);
      this.bNew.Image = global::Comparator.Properties.Resources.document;
      this.bNew.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.bNew.Name = "bNew";
      this.bNew.Size = new System.Drawing.Size(59, 22);
      this.bNew.Text = "New";
      this.bNew.ToolTipText = "New profile (Shift + F3)";
      // 
      // bNewNew
      // 
      this.bNewNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.bNewNew.Name = "bNewNew";
      this.bNewNew.Size = new System.Drawing.Size(201, 22);
      this.bNewNew.Text = "New profile";
      this.bNewNew.Click += new System.EventHandler(this.bNew_Click);
      // 
      // bNewCopy
      // 
      this.bNewCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.bNewCopy.Name = "bNewCopy";
      this.bNewCopy.Size = new System.Drawing.Size(201, 22);
      this.bNewCopy.Text = "New profile from current";
      this.bNewCopy.Click += new System.EventHandler(this.bNew_Click);
      // 
      // bSave
      // 
      this.bSave.Font = new System.Drawing.Font("Segoe UI", 8.25F);
      this.bSave.Image = global::Comparator.Properties.Resources.diskette;
      this.bSave.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.bSave.Name = "bSave";
      this.bSave.Size = new System.Drawing.Size(50, 22);
      this.bSave.Text = "Save";
      this.bSave.ToolTipText = "Save profile (F2)";
      this.bSave.Click += new System.EventHandler(this.bSave_Click);
      // 
      // bLoad
      // 
      this.bLoad.Font = new System.Drawing.Font("Segoe UI", 8.25F);
      this.bLoad.Image = global::Comparator.Properties.Resources.folder;
      this.bLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.bLoad.Name = "bLoad";
      this.bLoad.Size = new System.Drawing.Size(61, 22);
      this.bLoad.Text = "Load";
      this.bLoad.ToolTipText = "Load profile (F3)";
      // 
      // bCheck
      // 
      this.bCheck.Font = new System.Drawing.Font("Segoe UI", 8.25F);
      this.bCheck.Image = global::Comparator.Properties.Resources.tick;
      this.bCheck.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.bCheck.Name = "bCheck";
      this.bCheck.Size = new System.Drawing.Size(58, 22);
      this.bCheck.Text = "Check";
      this.bCheck.ToolTipText = "Check profile (Shift + F5)";
      this.bCheck.Click += new System.EventHandler(this.bCheck_Click);
      // 
      // bRun
      // 
      this.bRun.Font = new System.Drawing.Font("Segoe UI", 8.25F);
      this.bRun.Image = global::Comparator.Properties.Resources.compare;
      this.bRun.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.bRun.Name = "bRun";
      this.bRun.Size = new System.Drawing.Size(73, 22);
      this.bRun.Text = "Compare";
      this.bRun.ToolTipText = "Run compare (F5)";
      this.bRun.Click += new System.EventHandler(this.bRun_Click);
      // 
      // bResult
      // 
      this.bResult.Font = new System.Drawing.Font("Segoe UI", 8.25F);
      this.bResult.Image = global::Comparator.Properties.Resources.lastresult;
      this.bResult.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.bResult.Name = "bResult";
      this.bResult.Size = new System.Drawing.Size(79, 22);
      this.bResult.Text = "Last result";
      this.bResult.ToolTipText = "Show last compare result (F6)";
      this.bResult.Click += new System.EventHandler(this.bResult_Click);
      // 
      // bPin
      // 
      this.bPin.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.bPin.Image = global::Comparator.Properties.Resources.pinoff;
      this.bPin.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.bPin.Name = "bPin";
      this.bPin.Size = new System.Drawing.Size(23, 22);
      this.bPin.ToolTipText = "Always on top On/Off";
      this.bPin.Click += new System.EventHandler(this.bPin_Click);
      // 
      // status
      // 
      this.status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblMsg});
      this.status.Location = new System.Drawing.Point(0, 540);
      this.status.Name = "status";
      this.status.Size = new System.Drawing.Size(784, 22);
      this.status.TabIndex = 2;
      this.status.Text = "statusStrip1";
      // 
      // lblMsg
      // 
      this.lblMsg.Name = "lblMsg";
      this.lblMsg.Size = new System.Drawing.Size(39, 17);
      this.lblMsg.Text = "Ready";
      // 
      // gbSourceB
      // 
      this.gbSourceB.BackColor = System.Drawing.SystemColors.Control;
      this.gbSourceB.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gbSourceB.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.gbSourceB.ForeColor = System.Drawing.SystemColors.ControlText;
      this.gbSourceB.Location = new System.Drawing.Point(267, 3);
      this.gbSourceB.Margin = new System.Windows.Forms.Padding(5, 3, 0, 3);
      this.gbSourceB.Name = "gbSourceB";
      this.gbSourceB.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
      this.gbSourceB.Size = new System.Drawing.Size(258, 186);
      this.gbSourceB.TabIndex = 1;
      this.gbSourceB.TabStop = false;
      this.gbSourceB.Text = "SOURCE B ";
      // 
      // gbSourceA
      // 
      this.gbSourceA.BackColor = System.Drawing.SystemColors.Control;
      this.gbSourceA.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gbSourceA.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.gbSourceA.ForeColor = System.Drawing.SystemColors.ControlText;
      this.gbSourceA.Location = new System.Drawing.Point(0, 3);
      this.gbSourceA.Margin = new System.Windows.Forms.Padding(0, 3, 5, 3);
      this.gbSourceA.Name = "gbSourceA";
      this.gbSourceA.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
      this.gbSourceA.Size = new System.Drawing.Size(257, 186);
      this.gbSourceA.TabIndex = 0;
      this.gbSourceA.TabStop = false;
      this.gbSourceA.Text = "SOURCE A ";
      // 
      // gbFields
      // 
      this.gbFields.Controls.Add(this.dgCols);
      this.gbFields.Controls.Add(this.pFieldsCommands);
      this.gbFields.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gbFields.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.gbFields.Location = new System.Drawing.Point(3, 198);
      this.gbFields.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
      this.gbFields.Name = "gbFields";
      this.gbFields.Size = new System.Drawing.Size(525, 311);
      this.gbFields.TabIndex = 1;
      this.gbFields.TabStop = false;
      this.gbFields.Text = "FIELDS";
      // 
      // dgCols
      // 
      this.dgCols.AllowUserToAddRows = false;
      this.dgCols.AllowUserToResizeRows = false;
      this.dgCols.BackgroundColor = System.Drawing.SystemColors.Control;
      this.dgCols.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.dgCols.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.dgCols.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
      this.dgCols.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
      this.dgCols.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Key,
            this.Match,
            this.ColsA,
            this.ColsB});
      this.dgCols.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dgCols.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
      this.dgCols.EnableHeadersVisualStyles = false;
      this.dgCols.Location = new System.Drawing.Point(3, 46);
      this.dgCols.Name = "dgCols";
      this.dgCols.ReadOnly = true;
      this.dgCols.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
      dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
      dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dgCols.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
      this.dgCols.RowHeadersVisible = false;
      this.dgCols.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
      this.dgCols.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.dgCols.Size = new System.Drawing.Size(519, 262);
      this.dgCols.TabIndex = 1;
      this.dgCols.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgCols_CellContentClick);
      this.dgCols.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgCols_KeyDown);
      // 
      // Key
      // 
      this.Key.HeaderText = "Key";
      this.Key.Name = "Key";
      this.Key.ReadOnly = true;
      this.Key.Resizable = System.Windows.Forms.DataGridViewTriState.False;
      this.Key.Width = 50;
      // 
      // Match
      // 
      this.Match.HeaderText = "Match";
      this.Match.Name = "Match";
      this.Match.ReadOnly = true;
      this.Match.Resizable = System.Windows.Forms.DataGridViewTriState.False;
      this.Match.Width = 50;
      // 
      // ColsA
      // 
      this.ColsA.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.ColsA.HeaderText = "Fileds Source A";
      this.ColsA.MinimumWidth = 100;
      this.ColsA.Name = "ColsA";
      this.ColsA.ReadOnly = true;
      this.ColsA.Resizable = System.Windows.Forms.DataGridViewTriState.True;
      this.ColsA.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
      // 
      // ColsB
      // 
      this.ColsB.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.ColsB.HeaderText = "Fields Source B";
      this.ColsB.MinimumWidth = 100;
      this.ColsB.Name = "ColsB";
      this.ColsB.ReadOnly = true;
      this.ColsB.Resizable = System.Windows.Forms.DataGridViewTriState.True;
      this.ColsB.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
      // 
      // pFieldsCommands
      // 
      this.pFieldsCommands.Controls.Add(this.cbMatchAllPairs);
      this.pFieldsCommands.Controls.Add(this.bDown);
      this.pFieldsCommands.Controls.Add(this.cbMatchInOrder);
      this.pFieldsCommands.Controls.Add(this.bUp);
      this.pFieldsCommands.Controls.Add(this.bColSelect);
      this.pFieldsCommands.Controls.Add(this.bColDel);
      this.pFieldsCommands.Dock = System.Windows.Forms.DockStyle.Top;
      this.pFieldsCommands.Location = new System.Drawing.Point(3, 18);
      this.pFieldsCommands.Name = "pFieldsCommands";
      this.pFieldsCommands.Size = new System.Drawing.Size(519, 28);
      this.pFieldsCommands.TabIndex = 0;
      // 
      // cbMatchAllPairs
      // 
      this.cbMatchAllPairs.AutoSize = true;
      this.cbMatchAllPairs.Location = new System.Drawing.Point(283, 6);
      this.cbMatchAllPairs.Name = "cbMatchAllPairs";
      this.cbMatchAllPairs.Size = new System.Drawing.Size(101, 17);
      this.cbMatchAllPairs.TabIndex = 5;
      this.cbMatchAllPairs.Text = "Match all pairs";
      this.cbMatchAllPairs.UseVisualStyleBackColor = true;
      this.cbMatchAllPairs.CheckedChanged += new System.EventHandler(this.cbMatchAllPairs_CheckedChanged);
      // 
      // bDown
      // 
      this.bDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.bDown.FlatAppearance.BorderSize = 0;
      this.bDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.bDown.Image = ((System.Drawing.Image)(resources.GetObject("bDown.Image")));
      this.bDown.Location = new System.Drawing.Point(498, 3);
      this.bDown.Name = "bDown";
      this.bDown.Size = new System.Drawing.Size(15, 20);
      this.bDown.TabIndex = 3;
      this.bDown.TabStop = false;
      this.bDown.UseVisualStyleBackColor = true;
      this.bDown.Click += new System.EventHandler(this.bDown_Click);
      // 
      // cbMatchInOrder
      // 
      this.cbMatchInOrder.AutoSize = true;
      this.cbMatchInOrder.Location = new System.Drawing.Point(149, 6);
      this.cbMatchInOrder.Name = "cbMatchInOrder";
      this.cbMatchInOrder.Size = new System.Drawing.Size(130, 17);
      this.cbMatchInOrder.TabIndex = 4;
      this.cbMatchInOrder.Text = "Match in rows order";
      this.cbMatchInOrder.UseVisualStyleBackColor = true;
      this.cbMatchInOrder.CheckedChanged += new System.EventHandler(this.cbMatchInOrder_CheckedChanged);
      // 
      // bUp
      // 
      this.bUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.bUp.FlatAppearance.BorderSize = 0;
      this.bUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.bUp.Image = ((System.Drawing.Image)(resources.GetObject("bUp.Image")));
      this.bUp.Location = new System.Drawing.Point(483, 3);
      this.bUp.Name = "bUp";
      this.bUp.Size = new System.Drawing.Size(15, 20);
      this.bUp.TabIndex = 2;
      this.bUp.TabStop = false;
      this.bUp.UseVisualStyleBackColor = true;
      this.bUp.Click += new System.EventHandler(this.bUp_Click);
      // 
      // bColSelect
      // 
      this.bColSelect.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
      this.bColSelect.FlatAppearance.BorderSize = 0;
      this.bColSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.bColSelect.Image = ((System.Drawing.Image)(resources.GetObject("bColSelect.Image")));
      this.bColSelect.Location = new System.Drawing.Point(1, 1);
      this.bColSelect.Name = "bColSelect";
      this.bColSelect.Size = new System.Drawing.Size(73, 23);
      this.bColSelect.TabIndex = 0;
      this.bColSelect.Text = " Select...";
      this.bColSelect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.bColSelect.UseVisualStyleBackColor = true;
      this.bColSelect.Click += new System.EventHandler(this.bColSelect_Click);
      // 
      // bColDel
      // 
      this.bColDel.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
      this.bColDel.FlatAppearance.BorderSize = 0;
      this.bColDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.bColDel.Image = ((System.Drawing.Image)(resources.GetObject("bColDel.Image")));
      this.bColDel.Location = new System.Drawing.Point(74, 1);
      this.bColDel.Name = "bColDel";
      this.bColDel.Size = new System.Drawing.Size(67, 23);
      this.bColDel.TabIndex = 1;
      this.bColDel.Text = " Delete";
      this.bColDel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.bColDel.UseVisualStyleBackColor = true;
      this.bColDel.Click += new System.EventHandler(this.bColDel_Click);
      // 
      // gbOptions
      // 
      this.gbOptions.Controls.Add(this.cbTryConvert);
      this.gbOptions.Controls.Add(this.cbNullAsStr);
      this.gbOptions.Controls.Add(this.cbCaseSens);
      this.gbOptions.Controls.Add(this.cbCheckRepeats);
      this.gbOptions.Controls.Add(this.cbOnlyB);
      this.gbOptions.Controls.Add(this.cbOnlyA);
      this.gbOptions.Controls.Add(this.cbDiffOnly);
      this.gbOptions.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gbOptions.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.gbOptions.Location = new System.Drawing.Point(3, 3);
      this.gbOptions.Name = "gbOptions";
      this.gbOptions.Size = new System.Drawing.Size(235, 187);
      this.gbOptions.TabIndex = 0;
      this.gbOptions.TabStop = false;
      // 
      // cbTryConvert
      // 
      this.cbTryConvert.AutoSize = true;
      this.cbTryConvert.Location = new System.Drawing.Point(10, 133);
      this.cbTryConvert.Name = "cbTryConvert";
      this.cbTryConvert.Size = new System.Drawing.Size(214, 17);
      this.cbTryConvert.TabIndex = 37;
      this.cbTryConvert.Text = "Try to convert text to date or number";
      this.cbTryConvert.UseVisualStyleBackColor = true;
      // 
      // cbNullAsStr
      // 
      this.cbNullAsStr.AutoSize = true;
      this.cbNullAsStr.Location = new System.Drawing.Point(10, 114);
      this.cbNullAsStr.Name = "cbNullAsStr";
      this.cbNullAsStr.Size = new System.Drawing.Size(128, 17);
      this.cbNullAsStr.TabIndex = 36;
      this.cbNullAsStr.Text = "Null as empty string";
      this.cbNullAsStr.UseVisualStyleBackColor = true;
      // 
      // cbCaseSens
      // 
      this.cbCaseSens.AutoSize = true;
      this.cbCaseSens.Location = new System.Drawing.Point(10, 95);
      this.cbCaseSens.Name = "cbCaseSens";
      this.cbCaseSens.Size = new System.Drawing.Size(97, 17);
      this.cbCaseSens.TabIndex = 35;
      this.cbCaseSens.Text = "Case sensitive";
      this.cbCaseSens.UseVisualStyleBackColor = true;
      // 
      // cbCheckRepeats
      // 
      this.cbCheckRepeats.AutoSize = true;
      this.cbCheckRepeats.Location = new System.Drawing.Point(10, 76);
      this.cbCheckRepeats.Name = "cbCheckRepeats";
      this.cbCheckRepeats.Size = new System.Drawing.Size(138, 17);
      this.cbCheckRepeats.TabIndex = 34;
      this.cbCheckRepeats.Text = "Check repeating rows";
      this.cbCheckRepeats.UseVisualStyleBackColor = true;
      // 
      // cbOnlyB
      // 
      this.cbOnlyB.AutoSize = true;
      this.cbOnlyB.Location = new System.Drawing.Point(10, 57);
      this.cbOnlyB.Name = "cbOnlyB";
      this.cbOnlyB.Size = new System.Drawing.Size(182, 17);
      this.cbOnlyB.TabIndex = 33;
      this.cbOnlyB.Text = "Show records only in Source B";
      this.cbOnlyB.UseVisualStyleBackColor = true;
      // 
      // cbOnlyA
      // 
      this.cbOnlyA.AutoSize = true;
      this.cbOnlyA.Location = new System.Drawing.Point(10, 38);
      this.cbOnlyA.Name = "cbOnlyA";
      this.cbOnlyA.Size = new System.Drawing.Size(182, 17);
      this.cbOnlyA.TabIndex = 32;
      this.cbOnlyA.Text = "Show records only in Source A";
      this.cbOnlyA.UseVisualStyleBackColor = true;
      // 
      // cbDiffOnly
      // 
      this.cbDiffOnly.AutoSize = true;
      this.cbDiffOnly.Location = new System.Drawing.Point(10, 19);
      this.cbDiffOnly.Name = "cbDiffOnly";
      this.cbDiffOnly.Size = new System.Drawing.Size(140, 17);
      this.cbDiffOnly.TabIndex = 2;
      this.cbDiffOnly.Text = "Show differences only";
      this.cbDiffOnly.UseVisualStyleBackColor = true;
      // 
      // gbResultType
      // 
      this.gbResultType.Controls.Add(this.rbResExcel);
      this.gbResultType.Controls.Add(this.rbResHTML);
      this.gbResultType.Location = new System.Drawing.Point(74, 14);
      this.gbResultType.Name = "gbResultType";
      this.gbResultType.Size = new System.Drawing.Size(155, 33);
      this.gbResultType.TabIndex = 0;
      this.gbResultType.TabStop = false;
      // 
      // rbResExcel
      // 
      this.rbResExcel.AutoSize = true;
      this.rbResExcel.Checked = true;
      this.rbResExcel.Location = new System.Drawing.Point(6, 11);
      this.rbResExcel.Name = "rbResExcel";
      this.rbResExcel.Size = new System.Drawing.Size(51, 17);
      this.rbResExcel.TabIndex = 0;
      this.rbResExcel.TabStop = true;
      this.rbResExcel.Text = "Excel";
      this.rbResExcel.UseVisualStyleBackColor = true;
      // 
      // rbResHTML
      // 
      this.rbResHTML.AutoSize = true;
      this.rbResHTML.Location = new System.Drawing.Point(62, 11);
      this.rbResHTML.Name = "rbResHTML";
      this.rbResHTML.Size = new System.Drawing.Size(55, 17);
      this.rbResHTML.TabIndex = 1;
      this.rbResHTML.Text = "HTML";
      this.rbResHTML.UseVisualStyleBackColor = true;
      // 
      // tbSendTo
      // 
      this.tbSendTo.BackColor = System.Drawing.SystemColors.Control;
      this.tbSendTo.Location = new System.Drawing.Point(8, 147);
      this.tbSendTo.Multiline = true;
      this.tbSendTo.Name = "tbSendTo";
      this.tbSendTo.ReadOnly = true;
      this.tbSendTo.Size = new System.Drawing.Size(220, 60);
      this.tbSendTo.TabIndex = 8;
      // 
      // cbSend
      // 
      this.cbSend.AutoSize = true;
      this.cbSend.Location = new System.Drawing.Point(9, 128);
      this.cbSend.Name = "cbSend";
      this.cbSend.Size = new System.Drawing.Size(101, 17);
      this.cbSend.TabIndex = 7;
      this.cbSend.Text = "Send result to:";
      this.cbSend.UseVisualStyleBackColor = true;
      this.cbSend.CheckedChanged += new System.EventHandler(this.cbSend_CheckedChanged);
      // 
      // tableAll
      // 
      this.tableAll.ColumnCount = 2;
      this.tableAll.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableAll.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 247F));
      this.tableAll.Controls.Add(this.tableProfile, 0, 0);
      this.tableAll.Controls.Add(this.tableOptions, 1, 0);
      this.tableAll.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableAll.Location = new System.Drawing.Point(0, 25);
      this.tableAll.Name = "tableAll";
      this.tableAll.RowCount = 1;
      this.tableAll.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableAll.Size = new System.Drawing.Size(784, 515);
      this.tableAll.TabIndex = 0;
      // 
      // tableProfile
      // 
      this.tableProfile.ColumnCount = 1;
      this.tableProfile.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableProfile.Controls.Add(this.gbFields, 0, 1);
      this.tableProfile.Controls.Add(this.tableSources, 0, 0);
      this.tableProfile.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableProfile.Location = new System.Drawing.Point(3, 0);
      this.tableProfile.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
      this.tableProfile.Name = "tableProfile";
      this.tableProfile.RowCount = 2;
      this.tableProfile.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 198F));
      this.tableProfile.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableProfile.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.tableProfile.Size = new System.Drawing.Size(531, 512);
      this.tableProfile.TabIndex = 0;
      // 
      // tableSources
      // 
      this.tableSources.ColumnCount = 2;
      this.tableSources.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableSources.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableSources.Controls.Add(this.gbSourceB, 1, 0);
      this.tableSources.Controls.Add(this.gbSourceA, 0, 0);
      this.tableSources.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableSources.Location = new System.Drawing.Point(3, 3);
      this.tableSources.Name = "tableSources";
      this.tableSources.RowCount = 1;
      this.tableSources.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tableSources.Size = new System.Drawing.Size(525, 192);
      this.tableSources.TabIndex = 0;
      // 
      // tableOptions
      // 
      this.tableOptions.ColumnCount = 1;
      this.tableOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableOptions.Controls.Add(this.gbOptions, 0, 0);
      this.tableOptions.Controls.Add(this.gbBatchOpt, 0, 1);
      this.tableOptions.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableOptions.Location = new System.Drawing.Point(540, 3);
      this.tableOptions.Name = "tableOptions";
      this.tableOptions.RowCount = 3;
      this.tableOptions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 193F));
      this.tableOptions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 316F));
      this.tableOptions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableOptions.Size = new System.Drawing.Size(241, 509);
      this.tableOptions.TabIndex = 1;
      // 
      // gbBatchOpt
      // 
      this.gbBatchOpt.Controls.Add(this.tbSubject);
      this.gbBatchOpt.Controls.Add(this.label5);
      this.gbBatchOpt.Controls.Add(this.cbTimeInResFile);
      this.gbBatchOpt.Controls.Add(this.cboxResMail);
      this.gbBatchOpt.Controls.Add(this.label4);
      this.gbBatchOpt.Controls.Add(this.tbResFile);
      this.gbBatchOpt.Controls.Add(this.tbResFolder);
      this.gbBatchOpt.Controls.Add(this.label3);
      this.gbBatchOpt.Controls.Add(this.label1);
      this.gbBatchOpt.Controls.Add(this.label2);
      this.gbBatchOpt.Controls.Add(this.gbResultType);
      this.gbBatchOpt.Controls.Add(this.cbSend);
      this.gbBatchOpt.Controls.Add(this.tbSendTo);
      this.gbBatchOpt.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gbBatchOpt.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.gbBatchOpt.Location = new System.Drawing.Point(3, 196);
      this.gbBatchOpt.Name = "gbBatchOpt";
      this.gbBatchOpt.Size = new System.Drawing.Size(235, 310);
      this.gbBatchOpt.TabIndex = 1;
      this.gbBatchOpt.TabStop = false;
      this.gbBatchOpt.Text = "Batch mode options";
      // 
      // tbSubject
      // 
      this.tbSubject.BackColor = System.Drawing.SystemColors.Control;
      this.tbSubject.Location = new System.Drawing.Point(85, 239);
      this.tbSubject.Name = "tbSubject";
      this.tbSubject.ReadOnly = true;
      this.tbSubject.Size = new System.Drawing.Size(143, 22);
      this.tbSubject.TabIndex = 12;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(6, 242);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(70, 13);
      this.label5.TabIndex = 11;
      this.label5.Text = "Mail Subject";
      // 
      // cbTimeInResFile
      // 
      this.cbTimeInResFile.AutoSize = true;
      this.cbTimeInResFile.Location = new System.Drawing.Point(9, 109);
      this.cbTimeInResFile.Name = "cbTimeInResFile";
      this.cbTimeInResFile.Size = new System.Drawing.Size(143, 17);
      this.cbTimeInResFile.TabIndex = 6;
      this.cbTimeInResFile.Text = "Timestamp in file name";
      this.cbTimeInResFile.UseVisualStyleBackColor = true;
      // 
      // cboxResMail
      // 
      this.cboxResMail.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboxResMail.Enabled = false;
      this.cboxResMail.FormattingEnabled = true;
      this.cboxResMail.Location = new System.Drawing.Point(86, 212);
      this.cboxResMail.Name = "cboxResMail";
      this.cboxResMail.Size = new System.Drawing.Size(142, 21);
      this.cboxResMail.TabIndex = 10;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(6, 215);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(79, 13);
      this.label4.TabIndex = 9;
      this.label4.Text = "Send result as";
      // 
      // tbResFile
      // 
      this.tbResFile.Location = new System.Drawing.Point(74, 80);
      this.tbResFile.Name = "tbResFile";
      this.tbResFile.Size = new System.Drawing.Size(157, 22);
      this.tbResFile.TabIndex = 5;
      // 
      // tbResFolder
      // 
      this.tbResFolder.Location = new System.Drawing.Point(74, 53);
      this.tbResFolder.Name = "tbResFolder";
      this.tbResFolder.Size = new System.Drawing.Size(157, 22);
      this.tbResFolder.TabIndex = 3;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(6, 85);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(58, 13);
      this.label3.TabIndex = 4;
      this.label3.Text = "Result file";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(6, 57);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(66, 13);
      this.label1.TabIndex = 2;
      this.label1.Text = "Result path";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(6, 27);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(64, 13);
      this.label2.TabIndex = 1;
      this.label2.Text = "Result type";
      // 
      // ttip
      // 
      this.ttip.AutomaticDelay = 50;
      this.ttip.AutoPopDelay = 10000;
      this.ttip.InitialDelay = 50;
      this.ttip.ReshowDelay = 10;
      // 
      // FormView
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(784, 562);
      this.Controls.Add(this.tableAll);
      this.Controls.Add(this.status);
      this.Controls.Add(this.toolTop);
      this.DoubleBuffered = true;
      this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.KeyPreview = true;
      this.MinimumSize = new System.Drawing.Size(800, 600);
      this.Name = "FormView";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Comparator";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormView_FormClosing);
      this.Load += new System.EventHandler(this.FormView_Load);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormView_KeyDown);
      this.toolTop.ResumeLayout(false);
      this.toolTop.PerformLayout();
      this.status.ResumeLayout(false);
      this.status.PerformLayout();
      this.gbFields.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dgCols)).EndInit();
      this.pFieldsCommands.ResumeLayout(false);
      this.pFieldsCommands.PerformLayout();
      this.gbOptions.ResumeLayout(false);
      this.gbOptions.PerformLayout();
      this.gbResultType.ResumeLayout(false);
      this.gbResultType.PerformLayout();
      this.tableAll.ResumeLayout(false);
      this.tableProfile.ResumeLayout(false);
      this.tableSources.ResumeLayout(false);
      this.tableOptions.ResumeLayout(false);
      this.gbBatchOpt.ResumeLayout(false);
      this.gbBatchOpt.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ToolStrip toolTop;
    private System.Windows.Forms.ToolStripButton bSave;
    private System.Windows.Forms.ToolStripDropDownButton bLoad;
    private System.Windows.Forms.ToolStripButton bRun;
    private System.Windows.Forms.StatusStrip status;
    private System.Windows.Forms.ToolStripStatusLabel lblMsg;
    private System.Windows.Forms.CheckBox cbDiffOnly;
    private System.Windows.Forms.DataGridView dgCols;
    private System.Windows.Forms.GroupBox gbSourceA;
    private System.Windows.Forms.GroupBox gbSourceB;
    private System.Windows.Forms.GroupBox gbFields;
    private System.Windows.Forms.CheckBox cbMatchAllPairs;
    private System.Windows.Forms.CheckBox cbMatchInOrder;
    private System.Windows.Forms.GroupBox gbOptions;
    private System.Windows.Forms.TextBox tbSendTo;
    private System.Windows.Forms.CheckBox cbSend;
    private System.Windows.Forms.TableLayoutPanel tableAll;
    private System.Windows.Forms.TableLayoutPanel tableSources;
    private System.Windows.Forms.TableLayoutPanel tableProfile;
    private System.Windows.Forms.Button bColSelect;
    private System.Windows.Forms.GroupBox gbResultType;
    private System.Windows.Forms.RadioButton rbResExcel;
    private System.Windows.Forms.RadioButton rbResHTML;
    private System.Windows.Forms.Button bColDel;
    private System.Windows.Forms.Button bUp;
    private System.Windows.Forms.Button bDown;
    private System.Windows.Forms.Panel pFieldsCommands;
    private System.Windows.Forms.ToolStripButton bCheck;
    private System.Windows.Forms.ToolStripDropDownButton bNew;
    private System.Windows.Forms.ToolStripMenuItem bNewNew;
    private System.Windows.Forms.ToolStripMenuItem bNewCopy;
    private System.Windows.Forms.ToolTip ttip;
    private System.Windows.Forms.CheckBox cbOnlyB;
    private System.Windows.Forms.CheckBox cbOnlyA;
    private System.Windows.Forms.ToolStripButton bResult;
    private System.Windows.Forms.CheckBox cbCheckRepeats;
    private System.Windows.Forms.TableLayoutPanel tableOptions;
    private System.Windows.Forms.GroupBox gbBatchOpt;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.DataGridViewCheckBoxColumn Key;
    private System.Windows.Forms.DataGridViewCheckBoxColumn Match;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColsA;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColsB;
    private System.Windows.Forms.CheckBox cbTryConvert;
    private System.Windows.Forms.CheckBox cbNullAsStr;
    private System.Windows.Forms.CheckBox cbCaseSens;
    private System.Windows.Forms.ToolStripButton bPin;
    private System.Windows.Forms.TextBox tbResFile;
    private System.Windows.Forms.TextBox tbResFolder;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label4;
    public System.Windows.Forms.ComboBox cboxResMail;
    private System.Windows.Forms.CheckBox cbTimeInResFile;
    private System.Windows.Forms.TextBox tbSubject;
    private System.Windows.Forms.Label label5;
  }
}

