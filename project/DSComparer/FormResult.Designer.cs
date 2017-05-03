namespace DataComparer
{
  sealed partial class FormResult
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormResult));
      this.tabs = new System.Windows.Forms.TabControl();
      this.tabDiff = new System.Windows.Forms.TabPage();
      this.resDiff = new DataComparer.GridResult();
      this.tabIdent = new System.Windows.Forms.TabPage();
      this.resIdent = new DataComparer.GridResult();
      this.tabA = new System.Windows.Forms.TabPage();
      this.resA = new DataComparer.GridResult();
      this.tabB = new System.Windows.Forms.TabPage();
      this.resB = new DataComparer.GridResult();
      this.tabs.SuspendLayout();
      this.tabDiff.SuspendLayout();
      this.tabIdent.SuspendLayout();
      this.tabA.SuspendLayout();
      this.tabB.SuspendLayout();
      this.SuspendLayout();
      // 
      // tabs
      // 
      this.tabs.Appearance = System.Windows.Forms.TabAppearance.Buttons;
      this.tabs.Controls.Add(this.tabDiff);
      this.tabs.Controls.Add(this.tabIdent);
      this.tabs.Controls.Add(this.tabA);
      this.tabs.Controls.Add(this.tabB);
      this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabs.ItemSize = new System.Drawing.Size(150, 23);
      this.tabs.Location = new System.Drawing.Point(0, 0);
      this.tabs.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
      this.tabs.Name = "tabs";
      this.tabs.SelectedIndex = 0;
      this.tabs.Size = new System.Drawing.Size(764, 362);
      this.tabs.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
      this.tabs.TabIndex = 0;
      this.tabs.SelectedIndexChanged += new System.EventHandler(this.tabs_SelectedIndexChanged);
      // 
      // tabDiff
      // 
      this.tabDiff.Controls.Add(this.resDiff);
      this.tabDiff.Location = new System.Drawing.Point(4, 27);
      this.tabDiff.Margin = new System.Windows.Forms.Padding(0);
      this.tabDiff.Name = "tabDiff";
      this.tabDiff.Size = new System.Drawing.Size(756, 331);
      this.tabDiff.TabIndex = 0;
      this.tabDiff.Text = "Differences";
      this.tabDiff.UseVisualStyleBackColor = true;
      // 
      // resDiff
      // 
      this.resDiff.Dock = System.Windows.Forms.DockStyle.Fill;
      this.resDiff.Location = new System.Drawing.Point(0, 0);
      this.resDiff.Name = "resDiff";
      this.resDiff.Size = new System.Drawing.Size(756, 331);
      this.resDiff.TabIndex = 0;
      // 
      // tabIdent
      // 
      this.tabIdent.Controls.Add(this.resIdent);
      this.tabIdent.Location = new System.Drawing.Point(4, 27);
      this.tabIdent.Margin = new System.Windows.Forms.Padding(0);
      this.tabIdent.Name = "tabIdent";
      this.tabIdent.Size = new System.Drawing.Size(756, 331);
      this.tabIdent.TabIndex = 1;
      this.tabIdent.Text = "Identicals";
      this.tabIdent.UseVisualStyleBackColor = true;
      // 
      // resIdent
      // 
      this.resIdent.Dock = System.Windows.Forms.DockStyle.Fill;
      this.resIdent.Location = new System.Drawing.Point(0, 0);
      this.resIdent.Name = "resIdent";
      this.resIdent.Size = new System.Drawing.Size(756, 331);
      this.resIdent.TabIndex = 0;
      // 
      // tabA
      // 
      this.tabA.Controls.Add(this.resA);
      this.tabA.Location = new System.Drawing.Point(4, 27);
      this.tabA.Margin = new System.Windows.Forms.Padding(0);
      this.tabA.Name = "tabA";
      this.tabA.Size = new System.Drawing.Size(756, 331);
      this.tabA.TabIndex = 2;
      this.tabA.Text = "Only in Source A";
      this.tabA.UseVisualStyleBackColor = true;
      // 
      // resA
      // 
      this.resA.Dock = System.Windows.Forms.DockStyle.Fill;
      this.resA.Location = new System.Drawing.Point(0, 0);
      this.resA.Name = "resA";
      this.resA.Size = new System.Drawing.Size(756, 331);
      this.resA.TabIndex = 0;
      // 
      // tabB
      // 
      this.tabB.Controls.Add(this.resB);
      this.tabB.Location = new System.Drawing.Point(4, 27);
      this.tabB.Margin = new System.Windows.Forms.Padding(0);
      this.tabB.Name = "tabB";
      this.tabB.Size = new System.Drawing.Size(756, 331);
      this.tabB.TabIndex = 3;
      this.tabB.Text = "Only in Source B";
      this.tabB.UseVisualStyleBackColor = true;
      // 
      // resB
      // 
      this.resB.Dock = System.Windows.Forms.DockStyle.Fill;
      this.resB.Location = new System.Drawing.Point(0, 0);
      this.resB.Name = "resB";
      this.resB.Size = new System.Drawing.Size(756, 331);
      this.resB.TabIndex = 0;
      // 
      // FormResult
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(764, 362);
      this.Controls.Add(this.tabs);
      this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.KeyPreview = true;
      this.MinimumSize = new System.Drawing.Size(640, 300);
      this.Name = "FormResult";
      this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
      this.Text = "Compare results";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormResult_FormClosing);
      this.Load += new System.EventHandler(this.FormResult_Load);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormResult_KeyDown);
      this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FormResult_KeyUp);
      this.tabs.ResumeLayout(false);
      this.tabDiff.ResumeLayout(false);
      this.tabIdent.ResumeLayout(false);
      this.tabA.ResumeLayout(false);
      this.tabB.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TabControl tabs;
    private System.Windows.Forms.TabPage tabDiff;
    private System.Windows.Forms.TabPage tabIdent;
    private System.Windows.Forms.TabPage tabA;
    private System.Windows.Forms.TabPage tabB;
    public GridResult resDiff;
    public GridResult resIdent;
    public GridResult resA;
    public GridResult resB;
  }
}

