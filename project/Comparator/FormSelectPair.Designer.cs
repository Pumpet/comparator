namespace Comparator
{
  partial class FormSelectPair
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSelectPair));
      this.clbA = new System.Windows.Forms.CheckedListBox();
      this.clbB = new System.Windows.Forms.CheckedListBox();
      this.bSet = new System.Windows.Forms.Button();
      this.checkBox1 = new System.Windows.Forms.CheckBox();
      this.checkBox2 = new System.Windows.Forms.CheckBox();
      this.cbClear = new System.Windows.Forms.CheckBox();
      this.gbPairType = new System.Windows.Forms.GroupBox();
      this.rbNone = new System.Windows.Forms.RadioButton();
      this.rbMatch = new System.Windows.Forms.RadioButton();
      this.rbKey = new System.Windows.Forms.RadioButton();
      this.bDownB = new System.Windows.Forms.Button();
      this.bUpB = new System.Windows.Forms.Button();
      this.bDownA = new System.Windows.Forms.Button();
      this.bUpA = new System.Windows.Forms.Button();
      this.gbPairType.SuspendLayout();
      this.SuspendLayout();
      // 
      // clbA
      // 
      this.clbA.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
      this.clbA.BackColor = System.Drawing.SystemColors.Window;
      this.clbA.CheckOnClick = true;
      this.clbA.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.clbA.FormattingEnabled = true;
      this.clbA.Location = new System.Drawing.Point(12, 27);
      this.clbA.Name = "clbA";
      this.clbA.Size = new System.Drawing.Size(153, 157);
      this.clbA.TabIndex = 2;
      this.clbA.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbA_ItemCheck);
      this.clbA.SelectedIndexChanged += new System.EventHandler(this.clbA_SelectedIndexChanged);
      this.clbA.KeyDown += new System.Windows.Forms.KeyEventHandler(this.clbA_KeyDown);
      this.clbA.MouseMove += new System.Windows.Forms.MouseEventHandler(this.clbA_MouseMove);
      // 
      // clbB
      // 
      this.clbB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
      this.clbB.CheckOnClick = true;
      this.clbB.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.clbB.FormattingEnabled = true;
      this.clbB.Location = new System.Drawing.Point(178, 27);
      this.clbB.Name = "clbB";
      this.clbB.Size = new System.Drawing.Size(153, 157);
      this.clbB.TabIndex = 3;
      this.clbB.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbB_ItemCheck);
      this.clbB.SelectedIndexChanged += new System.EventHandler(this.clbB_SelectedIndexChanged);
      this.clbB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.clbA_KeyDown);
      this.clbB.MouseMove += new System.Windows.Forms.MouseEventHandler(this.clbB_MouseMove);
      // 
      // bSet
      // 
      this.bSet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.bSet.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.bSet.Location = new System.Drawing.Point(12, 236);
      this.bSet.Name = "bSet";
      this.bSet.Size = new System.Drawing.Size(75, 23);
      this.bSet.TabIndex = 5;
      this.bSet.Text = "Set";
      this.bSet.UseVisualStyleBackColor = true;
      this.bSet.Click += new System.EventHandler(this.bSet_Click);
      // 
      // checkBox1
      // 
      this.checkBox1.AutoSize = true;
      this.checkBox1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.checkBox1.Location = new System.Drawing.Point(15, 6);
      this.checkBox1.Name = "checkBox1";
      this.checkBox1.Size = new System.Drawing.Size(72, 17);
      this.checkBox1.TabIndex = 0;
      this.checkBox1.Text = "Check all";
      this.checkBox1.UseVisualStyleBackColor = true;
      this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
      // 
      // checkBox2
      // 
      this.checkBox2.AutoSize = true;
      this.checkBox2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.checkBox2.Location = new System.Drawing.Point(181, 6);
      this.checkBox2.Name = "checkBox2";
      this.checkBox2.Size = new System.Drawing.Size(72, 17);
      this.checkBox2.TabIndex = 1;
      this.checkBox2.Text = "Check all";
      this.checkBox2.UseVisualStyleBackColor = true;
      this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
      // 
      // cbClear
      // 
      this.cbClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.cbClear.AutoSize = true;
      this.cbClear.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.cbClear.Location = new System.Drawing.Point(99, 240);
      this.cbClear.Name = "cbClear";
      this.cbClear.Size = new System.Drawing.Size(86, 17);
      this.cbClear.TabIndex = 6;
      this.cbClear.Text = "Clear target";
      this.cbClear.UseVisualStyleBackColor = true;
      // 
      // gbPairType
      // 
      this.gbPairType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.gbPairType.Controls.Add(this.rbNone);
      this.gbPairType.Controls.Add(this.rbMatch);
      this.gbPairType.Controls.Add(this.rbKey);
      this.gbPairType.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.gbPairType.Location = new System.Drawing.Point(12, 187);
      this.gbPairType.Name = "gbPairType";
      this.gbPairType.Size = new System.Drawing.Size(319, 44);
      this.gbPairType.TabIndex = 4;
      this.gbPairType.TabStop = false;
      this.gbPairType.Text = "Pair type";
      // 
      // rbNone
      // 
      this.rbNone.AutoSize = true;
      this.rbNone.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.rbNone.Location = new System.Drawing.Point(180, 17);
      this.rbNone.Name = "rbNone";
      this.rbNone.Size = new System.Drawing.Size(53, 17);
      this.rbNone.TabIndex = 2;
      this.rbNone.Text = "None";
      this.rbNone.UseVisualStyleBackColor = true;
      // 
      // rbMatch
      // 
      this.rbMatch.AutoSize = true;
      this.rbMatch.Checked = true;
      this.rbMatch.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.rbMatch.Location = new System.Drawing.Point(77, 17);
      this.rbMatch.Name = "rbMatch";
      this.rbMatch.Size = new System.Drawing.Size(89, 17);
      this.rbMatch.TabIndex = 1;
      this.rbMatch.TabStop = true;
      this.rbMatch.Text = "For compare";
      this.rbMatch.UseVisualStyleBackColor = true;
      // 
      // rbKey
      // 
      this.rbKey.AutoSize = true;
      this.rbKey.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.rbKey.Location = new System.Drawing.Point(9, 17);
      this.rbKey.Name = "rbKey";
      this.rbKey.Size = new System.Drawing.Size(47, 17);
      this.rbKey.TabIndex = 0;
      this.rbKey.Text = "Keys";
      this.rbKey.UseVisualStyleBackColor = true;
      // 
      // bDownB
      // 
      this.bDownB.FlatAppearance.BorderSize = 0;
      this.bDownB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.bDownB.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.bDownB.Image = ((System.Drawing.Image)(resources.GetObject("bDownB.Image")));
      this.bDownB.Location = new System.Drawing.Point(316, 3);
      this.bDownB.Name = "bDownB";
      this.bDownB.Size = new System.Drawing.Size(17, 21);
      this.bDownB.TabIndex = 10;
      this.bDownB.TabStop = false;
      this.bDownB.UseVisualStyleBackColor = true;
      this.bDownB.Click += new System.EventHandler(this.bDownB_Click);
      // 
      // bUpB
      // 
      this.bUpB.FlatAppearance.BorderSize = 0;
      this.bUpB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.bUpB.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.bUpB.Image = ((System.Drawing.Image)(resources.GetObject("bUpB.Image")));
      this.bUpB.Location = new System.Drawing.Point(299, 3);
      this.bUpB.Name = "bUpB";
      this.bUpB.Size = new System.Drawing.Size(17, 21);
      this.bUpB.TabIndex = 9;
      this.bUpB.TabStop = false;
      this.bUpB.UseVisualStyleBackColor = true;
      this.bUpB.Click += new System.EventHandler(this.bUpB_Click);
      // 
      // bDownA
      // 
      this.bDownA.FlatAppearance.BorderSize = 0;
      this.bDownA.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.bDownA.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.bDownA.Image = ((System.Drawing.Image)(resources.GetObject("bDownA.Image")));
      this.bDownA.Location = new System.Drawing.Point(149, 3);
      this.bDownA.Name = "bDownA";
      this.bDownA.Size = new System.Drawing.Size(17, 21);
      this.bDownA.TabIndex = 8;
      this.bDownA.TabStop = false;
      this.bDownA.UseVisualStyleBackColor = true;
      this.bDownA.Click += new System.EventHandler(this.bDown_Click);
      // 
      // bUpA
      // 
      this.bUpA.FlatAppearance.BorderSize = 0;
      this.bUpA.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.bUpA.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.bUpA.Image = ((System.Drawing.Image)(resources.GetObject("bUpA.Image")));
      this.bUpA.Location = new System.Drawing.Point(132, 3);
      this.bUpA.Name = "bUpA";
      this.bUpA.Size = new System.Drawing.Size(17, 21);
      this.bUpA.TabIndex = 7;
      this.bUpA.TabStop = false;
      this.bUpA.UseVisualStyleBackColor = true;
      this.bUpA.Click += new System.EventHandler(this.bUp_Click);
      // 
      // FormSelectPair
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(344, 266);
      this.Controls.Add(this.bDownB);
      this.Controls.Add(this.bUpB);
      this.Controls.Add(this.gbPairType);
      this.Controls.Add(this.cbClear);
      this.Controls.Add(this.checkBox2);
      this.Controls.Add(this.checkBox1);
      this.Controls.Add(this.bDownA);
      this.Controls.Add(this.bUpA);
      this.Controls.Add(this.bSet);
      this.Controls.Add(this.clbB);
      this.Controls.Add(this.clbA);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
      this.KeyPreview = true;
      this.Location = new System.Drawing.Point(10, 10);
      this.MaximumSize = new System.Drawing.Size(360, 600);
      this.MinimumSize = new System.Drawing.Size(360, 300);
      this.Name = "FormSelectPair";
      this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
      this.Text = "Select fields for compare";
      this.Load += new System.EventHandler(this.FormSelectPair_Load);
      this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FormSelectPair_KeyUp);
      this.gbPairType.ResumeLayout(false);
      this.gbPairType.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.CheckedListBox clbA;
    private System.Windows.Forms.CheckedListBox clbB;
    private System.Windows.Forms.Button bSet;
    private System.Windows.Forms.Button bUpA;
    private System.Windows.Forms.Button bDownA;
    private System.Windows.Forms.CheckBox checkBox1;
    private System.Windows.Forms.CheckBox checkBox2;
    private System.Windows.Forms.CheckBox cbClear;
    private System.Windows.Forms.GroupBox gbPairType;
    private System.Windows.Forms.RadioButton rbNone;
    private System.Windows.Forms.RadioButton rbMatch;
    private System.Windows.Forms.RadioButton rbKey;
    private System.Windows.Forms.Button bDownB;
    private System.Windows.Forms.Button bUpB;
  }
}