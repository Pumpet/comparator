namespace DataComparer
{
  partial class GridResult
  {
    /// <summary> 
    /// Требуется переменная конструктора.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Освободить все используемые ресурсы.
    /// </summary>
    /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
    //protected override void Dispose(bool disposing)
    //{
    //  if (disposing && (components != null))
    //  {
    //    components.Dispose();
    //  }
    //  base.Dispose(disposing);
    //}

    #region Код, автоматически созданный конструктором компонентов

    /// <summary> 
    /// Обязательный метод для поддержки конструктора - не изменяйте 
    /// содержимое данного метода при помощи редактора кода.
    /// </summary>
    private void InitializeComponent()
    {
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GridResult));
      this.pTop = new System.Windows.Forms.ToolStrip();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
      this.tbFind = new System.Windows.Forms.ToolStripTextBox();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.dgData = new System.Windows.Forms.DataGridView();
      this.pStatus = new System.Windows.Forms.StatusStrip();
      this.lblCount = new System.Windows.Forms.ToolStripStatusLabel();
      this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
      this.panel = new System.Windows.Forms.Panel();
      this.gbTopLine = new System.Windows.Forms.GroupBox();
      this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
      this.bExcel = new System.Windows.Forms.ToolStripMenuItem();
      this.bHtml = new System.Windows.Forms.ToolStripMenuItem();
      this.bSaveHtml = new System.Windows.Forms.ToolStripMenuItem();
      this.bFind = new System.Windows.Forms.ToolStripButton();
      this.bFilter = new System.Windows.Forms.ToolStripButton();
      this.bFilterCancel = new System.Windows.Forms.ToolStripButton();
      this.bDiffs = new System.Windows.Forms.ToolStripButton();
      this.bKeys = new System.Windows.Forms.ToolStripButton();
      this.bRepeats = new System.Windows.Forms.ToolStripButton();
      this.bPin = new System.Windows.Forms.ToolStripButton();
      this.pTop.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dgData)).BeginInit();
      this.pStatus.SuspendLayout();
      this.panel.SuspendLayout();
      this.SuspendLayout();
      // 
      // pTop
      // 
      this.pTop.BackColor = System.Drawing.SystemColors.Control;
      this.pTop.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.pTop.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.toolStripSeparator3,
            this.toolStripLabel1,
            this.tbFind,
            this.bFind,
            this.bFilter,
            this.bFilterCancel,
            this.toolStripSeparator2,
            this.bDiffs,
            this.bKeys,
            this.bRepeats,
            this.toolStripSeparator1,
            this.bPin});
      this.pTop.Location = new System.Drawing.Point(0, 0);
      this.pTop.Name = "pTop";
      this.pTop.Padding = new System.Windows.Forms.Padding(0);
      this.pTop.Size = new System.Drawing.Size(700, 25);
      this.pTop.TabIndex = 0;
      this.pTop.Text = "toolStrip1";
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
      // 
      // toolStripLabel1
      // 
      this.toolStripLabel1.Name = "toolStripLabel1";
      this.toolStripLabel1.Size = new System.Drawing.Size(42, 22);
      this.toolStripLabel1.Text = "Search";
      // 
      // tbFind
      // 
      this.tbFind.AutoSize = false;
      this.tbFind.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.tbFind.Name = "tbFind";
      this.tbFind.Size = new System.Drawing.Size(100, 23);
      this.tbFind.ToolTipText = "Text for search or filter (Ctrl+F)";
      this.tbFind.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbFind_KeyDown);
      this.tbFind.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbFind_KeyUp);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
      // 
      // dgData
      // 
      this.dgData.AllowUserToAddRows = false;
      this.dgData.AllowUserToDeleteRows = false;
      this.dgData.AllowUserToOrderColumns = true;
      this.dgData.BackgroundColor = System.Drawing.SystemColors.Control;
      this.dgData.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.dgData.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dgData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
      this.dgData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle2.NullValue = "[NULL]";
      dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.dgData.DefaultCellStyle = dataGridViewCellStyle2;
      this.dgData.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dgData.EnableHeadersVisualStyles = false;
      this.dgData.Location = new System.Drawing.Point(0, 25);
      this.dgData.Name = "dgData";
      this.dgData.ReadOnly = true;
      this.dgData.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
      dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
      dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dgData.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
      this.dgData.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
      this.dgData.Size = new System.Drawing.Size(700, 177);
      this.dgData.TabIndex = 1;
      this.dgData.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dgData_RowPrePaint);
      this.dgData.SelectionChanged += new System.EventHandler(this.dgData_SelectionChanged);
      this.dgData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgData_KeyDown);
      this.dgData.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgData_KeyUp);
      // 
      // pStatus
      // 
      this.pStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblCount,
            this.lblStatus});
      this.pStatus.Location = new System.Drawing.Point(0, 202);
      this.pStatus.Name = "pStatus";
      this.pStatus.Size = new System.Drawing.Size(700, 22);
      this.pStatus.SizingGrip = false;
      this.pStatus.TabIndex = 2;
      this.pStatus.Text = "statusStrip1";
      // 
      // lblCount
      // 
      this.lblCount.Name = "lblCount";
      this.lblCount.Size = new System.Drawing.Size(41, 17);
      this.lblCount.Text = "0 rows";
      // 
      // lblStatus
      // 
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Size = new System.Drawing.Size(10, 17);
      this.lblStatus.Text = " ";
      // 
      // panel
      // 
      this.panel.Controls.Add(this.gbTopLine);
      this.panel.Controls.Add(this.dgData);
      this.panel.Controls.Add(this.pStatus);
      this.panel.Controls.Add(this.pTop);
      this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel.Location = new System.Drawing.Point(0, 0);
      this.panel.Name = "panel";
      this.panel.Size = new System.Drawing.Size(700, 224);
      this.panel.TabIndex = 3;
      // 
      // gbTopLine
      // 
      this.gbTopLine.Dock = System.Windows.Forms.DockStyle.Top;
      this.gbTopLine.Location = new System.Drawing.Point(0, 25);
      this.gbTopLine.Margin = new System.Windows.Forms.Padding(0);
      this.gbTopLine.Name = "gbTopLine";
      this.gbTopLine.Padding = new System.Windows.Forms.Padding(0);
      this.gbTopLine.Size = new System.Drawing.Size(700, 2);
      this.gbTopLine.TabIndex = 3;
      this.gbTopLine.TabStop = false;
      // 
      // toolStripDropDownButton1
      // 
      this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bExcel,
            this.bHtml,
            this.bSaveHtml});
      this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
      this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
      this.toolStripDropDownButton1.Size = new System.Drawing.Size(60, 22);
      this.toolStripDropDownButton1.Text = "Send to";
      // 
      // bExcel
      // 
      this.bExcel.Image = global::DataComparer.Properties.Resources.Excel;
      this.bExcel.Name = "bExcel";
      this.bExcel.Size = new System.Drawing.Size(137, 22);
      this.bExcel.Text = "Excel (F9)";
      this.bExcel.Click += new System.EventHandler(this.bExcel_Click);
      // 
      // bHtml
      // 
      this.bHtml.Image = global::DataComparer.Properties.Resources.html;
      this.bHtml.Name = "bHtml";
      this.bHtml.Size = new System.Drawing.Size(137, 22);
      this.bHtml.Text = "HTML";
      this.bHtml.Click += new System.EventHandler(this.bHTML_Click);
      // 
      // bSaveHtml
      // 
      this.bSaveHtml.Image = global::DataComparer.Properties.Resources.diskette;
      this.bSaveHtml.Name = "bSaveHtml";
      this.bSaveHtml.Size = new System.Drawing.Size(137, 22);
      this.bSaveHtml.Text = "HTML File...";
      this.bSaveHtml.Click += new System.EventHandler(this.bSave_Click);
      // 
      // bFind
      // 
      this.bFind.Image = global::DataComparer.Properties.Resources.find;
      this.bFind.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.bFind.Name = "bFind";
      this.bFind.Size = new System.Drawing.Size(50, 22);
      this.bFind.Text = "Find";
      this.bFind.ToolTipText = "Go over search results (F3, Alt for current column, Shift for backwards, Ctrl for" +
    " case sensitive)";
      this.bFind.Click += new System.EventHandler(this.bFind_Click);
      // 
      // bFilter
      // 
      this.bFilter.Image = global::DataComparer.Properties.Resources.filter;
      this.bFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.bFilter.Name = "bFilter";
      this.bFilter.Size = new System.Drawing.Size(53, 22);
      this.bFilter.Text = "Filter";
      this.bFilter.ToolTipText = "Filter for search results in current column (F4, Ctrl for case sensitive)";
      this.bFilter.Click += new System.EventHandler(this.bFilter_Click);
      // 
      // bFilterCancel
      // 
      this.bFilterCancel.Image = global::DataComparer.Properties.Resources.clear_filter;
      this.bFilterCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.bFilterCancel.Name = "bFilterCancel";
      this.bFilterCancel.Size = new System.Drawing.Size(81, 22);
      this.bFilterCancel.Text = "Clear filter";
      this.bFilterCancel.ToolTipText = "Cancel all filters (Shift+F4)";
      this.bFilterCancel.Click += new System.EventHandler(this.bFilterCancel_Click);
      // 
      // bDiffs
      // 
      this.bDiffs.Image = global::DataComparer.Properties.Resources.diff;
      this.bDiffs.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.bDiffs.Name = "bDiffs";
      this.bDiffs.Size = new System.Drawing.Size(81, 22);
      this.bDiffs.Text = "Difference";
      this.bDiffs.ToolTipText = "Go over differences (F5, Alt for current column, Shift for backwards)";
      this.bDiffs.Click += new System.EventHandler(this.bDiffs_Click);
      // 
      // bKeys
      // 
      this.bKeys.Image = global::DataComparer.Properties.Resources.key;
      this.bKeys.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.bKeys.Name = "bKeys";
      this.bKeys.Size = new System.Drawing.Size(46, 22);
      this.bKeys.Text = "Key";
      this.bKeys.ToolTipText = "Go over key fields in row (F6, Shift for back to current cell)";
      this.bKeys.Click += new System.EventHandler(this.bKeys_Click);
      // 
      // bRepeats
      // 
      this.bRepeats.Image = global::DataComparer.Properties.Resources.Repeat;
      this.bRepeats.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.bRepeats.Name = "bRepeats";
      this.bRepeats.Size = new System.Drawing.Size(77, 22);
      this.bRepeats.Text = "Duplicate";
      this.bRepeats.ToolTipText = "Go over repeated rows (F7, Shift for backwards)";
      this.bRepeats.Click += new System.EventHandler(this.bRepeats_Click);
      // 
      // bPin
      // 
      this.bPin.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.bPin.Image = global::DataComparer.Properties.Resources.pinoff;
      this.bPin.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.bPin.Name = "bPin";
      this.bPin.Size = new System.Drawing.Size(23, 22);
      this.bPin.ToolTipText = "Always on top On/Off";
      this.bPin.Click += new System.EventHandler(this.bPin_Click);
      // 
      // GridResult
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.panel);
      this.Name = "GridResult";
      this.Size = new System.Drawing.Size(700, 224);
      this.Load += new System.EventHandler(this.GridResult_Load);
      this.pTop.ResumeLayout(false);
      this.pTop.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dgData)).EndInit();
      this.pStatus.ResumeLayout(false);
      this.pStatus.PerformLayout();
      this.panel.ResumeLayout(false);
      this.panel.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ToolStrip pTop;
    private System.Windows.Forms.ToolStripButton bKeys;
    private System.Windows.Forms.ToolStripButton bDiffs;
    private System.Windows.Forms.ToolStripLabel toolStripLabel1;
    private System.Windows.Forms.ToolStripTextBox tbFind;
    private System.Windows.Forms.ToolStripButton bFind;
    private System.Windows.Forms.ToolStripButton bFilter;
    private System.Windows.Forms.ToolStripButton bFilterCancel;
    private System.Windows.Forms.StatusStrip pStatus;
    private System.Windows.Forms.ToolStripStatusLabel lblCount;
    private System.Windows.Forms.ToolStripStatusLabel lblStatus;
    private System.Windows.Forms.Panel panel;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    public System.Windows.Forms.DataGridView dgData;
    private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
    private System.Windows.Forms.ToolStripMenuItem bExcel;
    private System.Windows.Forms.ToolStripMenuItem bHtml;
    private System.Windows.Forms.ToolStripMenuItem bSaveHtml;
    private System.Windows.Forms.ToolStripButton bPin;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.GroupBox gbTopLine;
    private System.Windows.Forms.ToolStripButton bRepeats;
  }
}
