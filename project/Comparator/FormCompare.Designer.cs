namespace Comparator
{
  partial class FormCompare
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
      this.pbarSourceA = new System.Windows.Forms.ProgressBar();
      this.pbarSourceB = new System.Windows.Forms.ProgressBar();
      this.pbarCompare = new System.Windows.Forms.ProgressBar();
      this.lblSourceA = new System.Windows.Forms.Label();
      this.lblSourceB = new System.Windows.Forms.Label();
      this.lblCompare = new System.Windows.Forms.Label();
      this.bStop = new System.Windows.Forms.Button();
      this.lblWait = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // pbarSourceA
      // 
      this.pbarSourceA.Location = new System.Drawing.Point(12, 23);
      this.pbarSourceA.Maximum = 500;
      this.pbarSourceA.Name = "pbarSourceA";
      this.pbarSourceA.Size = new System.Drawing.Size(460, 8);
      this.pbarSourceA.Step = 1;
      this.pbarSourceA.TabIndex = 0;
      // 
      // pbarSourceB
      // 
      this.pbarSourceB.Location = new System.Drawing.Point(12, 33);
      this.pbarSourceB.Maximum = 500;
      this.pbarSourceB.Name = "pbarSourceB";
      this.pbarSourceB.Size = new System.Drawing.Size(460, 8);
      this.pbarSourceB.Step = 1;
      this.pbarSourceB.TabIndex = 1;
      // 
      // pbarCompare
      // 
      this.pbarCompare.Location = new System.Drawing.Point(12, 82);
      this.pbarCompare.Name = "pbarCompare";
      this.pbarCompare.Size = new System.Drawing.Size(460, 8);
      this.pbarCompare.Step = 1;
      this.pbarCompare.TabIndex = 2;
      // 
      // lblSourceA
      // 
      this.lblSourceA.AutoSize = true;
      this.lblSourceA.Location = new System.Drawing.Point(10, 6);
      this.lblSourceA.Name = "lblSourceA";
      this.lblSourceA.Size = new System.Drawing.Size(92, 13);
      this.lblSourceA.TabIndex = 3;
      this.lblSourceA.Text = "Source A: 0 rows";
      // 
      // lblSourceB
      // 
      this.lblSourceB.AutoSize = true;
      this.lblSourceB.Location = new System.Drawing.Point(10, 44);
      this.lblSourceB.Name = "lblSourceB";
      this.lblSourceB.Size = new System.Drawing.Size(92, 13);
      this.lblSourceB.TabIndex = 4;
      this.lblSourceB.Text = "Source B: 0 rows";
      // 
      // lblCompare
      // 
      this.lblCompare.AutoSize = true;
      this.lblCompare.Location = new System.Drawing.Point(10, 66);
      this.lblCompare.Name = "lblCompare";
      this.lblCompare.Size = new System.Drawing.Size(56, 13);
      this.lblCompare.TabIndex = 5;
      this.lblCompare.Text = "Compare ";
      // 
      // bStop
      // 
      this.bStop.Location = new System.Drawing.Point(397, 96);
      this.bStop.Name = "bStop";
      this.bStop.Size = new System.Drawing.Size(75, 23);
      this.bStop.TabIndex = 6;
      this.bStop.Text = "Stop";
      this.bStop.UseVisualStyleBackColor = true;
      this.bStop.Click += new System.EventHandler(this.bCancel_Click);
      // 
      // lblWait
      // 
      this.lblWait.AutoSize = true;
      this.lblWait.Location = new System.Drawing.Point(12, 101);
      this.lblWait.Name = "lblWait";
      this.lblWait.Size = new System.Drawing.Size(0, 13);
      this.lblWait.TabIndex = 7;
      // 
      // FormCompare
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(484, 124);
      this.ControlBox = false;
      this.Controls.Add(this.lblWait);
      this.Controls.Add(this.bStop);
      this.Controls.Add(this.lblCompare);
      this.Controls.Add(this.lblSourceB);
      this.Controls.Add(this.lblSourceA);
      this.Controls.Add(this.pbarCompare);
      this.Controls.Add(this.pbarSourceB);
      this.Controls.Add(this.pbarSourceA);
      this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "FormCompare";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Load += new System.EventHandler(this.FormCompare_Load);
      this.Shown += new System.EventHandler(this.FormCompare_Shown);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ProgressBar pbarSourceA;
    private System.Windows.Forms.ProgressBar pbarSourceB;
    private System.Windows.Forms.ProgressBar pbarCompare;
    private System.Windows.Forms.Label lblSourceA;
    private System.Windows.Forms.Label lblSourceB;
    private System.Windows.Forms.Label lblCompare;
    private System.Windows.Forms.Button bStop;
    private System.Windows.Forms.Label lblWait;
  }
}