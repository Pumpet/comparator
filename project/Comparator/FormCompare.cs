using System;
using System.Threading;
using System.Windows.Forms;
using Common;

namespace Comparator
{
  /* Form for control getting data and comparison processes */
  public partial class FormCompare : Form
  {
    bool inProc;
    SynchronizationContext sync { get; set; }
    public event Action<TaskContext, TaskContext, TaskContext> Start;
    public event Action Stop;

    public FormCompare()
    {
      InitializeComponent();
    }
    //-------------------------------------------------------------------------
    private void FormCompare_Load(object sender, EventArgs e)
    {
      sync = SynchronizationContext.Current;
    }
    private void FormCompare_Shown(object sender, EventArgs e)
    {
      Refresh();
      if (Start != null)
      {
        inProc = true;
        // start processes
        Start(new TaskContext() { ViewContext = sync, OnProgress = ProgressA, OnFinish = null, OnError = null },
          new TaskContext() { ViewContext = sync, OnProgress = ProgressB, OnFinish = null, OnError = null },
          new TaskContext() { ViewContext = sync, OnProgress = ProgressCompare, OnFinish = Finish, OnError = null }
          );
      }
    }
    //-------------------------------------------------------------------------
    private void bCancel_Click(object sender, EventArgs e)
    {
      if (inProc)
      {
        if (Stop != null)
          Stop();
        //Finish(null, null);
      }
      else
        Close();
    }
    //-------------------------------------------------------------------------
    void ProgressA(int step, string msg)
    {
      StepProgress(pbarSourceA, lblSourceA, step, 0, "Source A: " + msg);
    }
    //-------------------------------------------------------------------------
    void ProgressB(int step, string msg)
    {
      StepProgress(pbarSourceB, lblSourceB, step, 0, "Source B: " + msg);
    }
    //-------------------------------------------------------------------------
    void ProgressCompare(int step, string msg)
    {
      StepProgress(pbarCompare, lblCompare, step, 100, msg); // step value in percent
    }
    //-------------------------------------------------------------------------
    void StepProgress(ProgressBar pBar, Label lbl, int step, int max, string msg)
    {
      Func<double, int> Step = brake => // calculate step, gradually slowing...
      {
        int d = 1000;
        int min = d*100;
        double k = pBar.Maximum * brake;
        if (step == 0) step = 1;
        if (step > min) k = k - (1 - pBar.Value / (double)pBar.Maximum) * (step - min) / d;
        if (k < pBar.Maximum) k = pBar.Maximum;
        step = (int)(-k / Math.Pow(step, 1.0 / brake) + pBar.Maximum);
        if (step < pBar.Value) step = pBar.Value;
        if (step > pBar.Maximum) step = pBar.Maximum;
        if (step < pBar.Minimum) step = pBar.Minimum + 1;
        return step;
      };
      if (max > 0) pBar.Maximum = max;
      if (step < int.MaxValue) // calculate step value if maximum is unknown or less than current step
        pBar.Value = max > 0 && step <= max ? step : Step(4);
      else
        pBar.Value = pBar.Maximum;
      lbl.Text = msg;
      Refresh();
    }
    //-------------------------------------------------------------------------
    void Finish(object res, string msg) 
    {
      inProc = false;
      if ((bool)res)
      {
        Refresh();
        Thread.Sleep(300);
        Close();
      }
      else
        bStop.Text = "Close";
    }
    //-------------------------------------------------------------------------
    public void WaitState(bool on, string msg) 
    {
      lblWait.Text = msg;
      bStop.Enabled = !on;
      Cursor = on ? Cursors.WaitCursor : Cursors.Default;
      Refresh();
    }
  }
}
