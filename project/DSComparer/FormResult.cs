using System;
using System.Linq;
using System.Windows.Forms;

namespace DataComparer
{
  public sealed partial class FormResult : Form
  {
    bool needClose;
    public string ResultPath { get; private set; }
    public string ResultFileName { get; private set; }
    public string StyleFile { get; private set; }
    //-------------------------------------------------------------------------
    public FormResult(CompareResult cmp, string resultPath, string resultFileName, string styleFile)
    {
      InitializeComponent();
      Cursor = Cursors.WaitCursor;
      ResultPath = resultPath;
      ResultFileName = resultFileName;
      StyleFile = styleFile;
      try
      {
        Text = "Compare \"" + cmp.NameA + "\"(A) and \"" + cmp.NameB + "\"(B)";
        // Get available results
        resDiff.SetData(cmp, ResultType.rtDiff);
        if (cmp.DtIdent == null) tabs.TabPages.RemoveByKey("tabIdent"); else resIdent.SetData(cmp, ResultType.rtIdent);
        if (cmp.DtA == null) tabs.TabPages.RemoveByKey("tabA"); else resA.SetData(cmp, ResultType.rtA);
        if (cmp.DtB == null) tabs.TabPages.RemoveByKey("tabB"); else resB.SetData(cmp, ResultType.rtB);

        if (DSComparer.FormRect.IsEmpty)
          StartPosition = FormStartPosition.CenterScreen;
        else
        {
          StartPosition = FormStartPosition.Manual;
          Location = DSComparer.FormRect.Location;
          Size = DSComparer.FormRect.Size;
        }
        WindowState = DSComparer.WinState;
      }
      finally
      {
        Cursor = Cursors.Default;
      }
    }
    //-------------------------------------------------------------------------
    private void FormResult_Load(object sender, EventArgs e)
    {

    }
    //-------------------------------------------------------------------------
    private void FormResult_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Escape)
        Close();
    }
    //-------------------------------------------------------------------------
    private void FormResult_KeyUp(object sender, KeyEventArgs e)
    {
      // Switch between tabs
      if (e.Modifiers == Keys.Control && e.KeyCode == Keys.Tab)
      {
        e.Handled = true;
        if (tabs.SelectedIndex == -1)
          tabs.SelectedIndex = tabs.TabCount - 1;
        else if (tabs.SelectedIndex == tabs.TabCount - 1)
          tabs.SelectedIndex = 0;
        else
          tabs.SelectedIndex++;
      }
      if (e.Modifiers == (Keys.Control | Keys.Shift) && e.KeyCode == Keys.Tab)
      {
        e.Handled = true;
        if (tabs.SelectedIndex == -1)
          tabs.SelectedIndex = tabs.TabCount - 1;
        else if (tabs.SelectedIndex == 0)
          tabs.SelectedIndex = tabs.TabCount - 1;
        else
          tabs.SelectedIndex--;
      }
    }
    //-------------------------------------------------------------------------
    public void Close(bool need) 
    {
      needClose = need;
      Close();
    }
    //-------------------------------------------------------------------------
    private void FormResult_FormClosing(object sender, FormClosingEventArgs e)
    {
      // save current window position and states
      if (WindowState == FormWindowState.Normal)
      {
        DSComparer.FormRect.Location = Location;
        DSComparer.FormRect.Size = Size;
      }
      DSComparer.WinState = WindowState;

      if (!needClose)
      {
        e.Cancel = true;
        Hide();
      }
    }
    //-------------------------------------------------------------------------
    private void tabs_SelectedIndexChanged(object sender, EventArgs e)
    {
      // focus from button to grid
      if (tabs.SelectedTab != null && tabs.SelectedTab.Controls.OfType<GridResult>().Any())
        tabs.SelectedTab.Controls.OfType<GridResult>().First().Focus();
    }

  }
}
