using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace DataComparer
{
  /* Grid for results of a certain type */
  public partial class GridResult : UserControl
  {
    BindingSource bs = new BindingSource();
    DataTable dt, dtf; // current data - all (dt) or filtered (dtf)
    ResultType rt; 
    CompareResult cmp; 
    Action<string, Exception> onError = (m, ex) => { MessageBox.Show(string.Format("{0}\n{1}", m, ex.Message), "", MessageBoxButtons.OK, MessageBoxIcon.Error); };
    Keys pushKey = Keys.None; // pressed control keys
    Dictionary<int, string> filteredCols = new Dictionary<int, string>(); // columns with filter
    DataGridViewCell currCell; // current cell
    //-------------------------------------------------------------------------
    bool reserveCell; // don't reset current cell
    bool rtPairs { get { return rt == ResultType.rtDiff || rt == ResultType.rtIdent; } } // true if data in pairs
    bool rtDiff { get { return rt == ResultType.rtDiff; } } // true for differences 
    int delta { get { return rtPairs ? 2 : 0; } } // table for pairs has two first no-data columns
    DataTable data { get { return dtf ?? dt; } } // current data
    //-------------------------------------------------------------------------
    public GridResult()
    {
      InitializeComponent();
    }
    //-------------------------------------------------------------------------
    public void SetData(CompareResult res, ResultType resType)
    {
      rt = resType;
      cmp = res;
      switch (rt)
      {
        case ResultType.rtDiff:
          dt = cmp.DtDiff;
          break;
        case ResultType.rtIdent:
          dt = cmp.DtIdent;
          break;
        case ResultType.rtA:
          dt = cmp.DtA;
          break;
        case ResultType.rtB:
          dt = cmp.DtB;
          break;
        default:
          dt = new DataTable();
          break;
      }
      dtf = null;
      bs.DataSource = dt;
      dgData.DataSource = bs;
      bDiffs.Enabled = rtDiff;
      bRepeats.Enabled = rtPairs && cmp.RepeatRows.Count > 0;
    }
    //-------------------------------------------------------------------------
    void GridResult_Load(object sender, EventArgs e)
    {
      SetDoubleBuffered(dgData, true);
      PrepareGrid();
      dgData.Select();
    }
    //-------------------------------------------------------------------------
    public static void SetDoubleBuffered(Control control, bool setting) // prevent control blinking
    {
      System.Reflection.BindingFlags bFlags = System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic;
      control.GetType().GetProperty("DoubleBuffered", bFlags).SetValue(control, setting, null);
    }
    //-------------------------------------------------------------------------
    /* initial grid settings */
    void PrepareGrid()
    {
      int fontSize = Convert.ToInt32(dgData.Font.Size);
      if (rtPairs) dgData.TopLeftHeaderCell.Value = "A\nB";
      foreach (DataGridViewColumn c in dgData.Columns)
      {
        c.HeaderText = dt.Columns[c.DataPropertyName].Caption;
        c.Name = dt.Columns[c.DataPropertyName].ColumnName;
        int hsize = c.HeaderText.Split('\n').Select(x => x.Length).Max() * fontSize;
        if (hsize > c.Width) c.Width = hsize;
        if (rtPairs)
        {
          c.SortMode = DataGridViewColumnSortMode.NotSortable;
          //if (c.Index < delta) { c.Frozen = true; c.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader; } //test - show no-data columns
          if (c.Index < delta) c.Visible = false; //work - hide no-data columns
        }
        if (!rtPairs && c.HeaderText == DSComparer.emptyColumnMark) c.Visible = false;
        // columns with keys
        if (data.Columns.OfType<DataColumn>().Where(x => cmp.KeyCols.Contains(x.Ordinal - delta)).Select(y => y.ColumnName).Contains(c.Name))
        {
          c.HeaderCell.Style.ForeColor = Color.DarkBlue;
          c.HeaderCell.ToolTipText = "Key field";
        }
        // columns with compared data
        if (rtPairs && data.Columns.OfType<DataColumn>().Where(x => cmp.MatchCols.Contains(x.Ordinal - delta)).Select(y => y.ColumnName).Contains(c.Name))
        {
          c.HeaderCell.Style.ForeColor = Color.DarkGreen;
          c.HeaderCell.ToolTipText = "Matched";
        }
        // columns with differences
        if (rtDiff && data.Columns.OfType<DataColumn>().Where(x => cmp.Diffs.Values.SelectMany(v => v).Distinct().Contains(x.Ordinal - delta)).Select(y => y.ColumnName).Contains(c.Name))
        {
          c.HeaderCell.Style.ForeColor = Color.DarkRed;
          c.HeaderCell.ToolTipText = "There are differencies";
        }
      }
    }
    //-------------------------------------------------------------------------
    /* Coloring row */
    void dgData_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
    {
      DataGridView grid = sender as DataGridView;
      if (grid != null)
      {
        // highlight pair
        if (rtPairs)
          grid.Rows[e.RowIndex].DefaultCellStyle.BackColor = (int)grid[1, e.RowIndex].Value % 2 == 0 ? Color.White : Color.LightCyan;
        // highlight differencies
        if (rtDiff && cmp.Diffs.ContainsKey((int)grid[0, e.RowIndex].Value))
        {
          foreach (int c in cmp.Diffs[(int)grid[0, e.RowIndex].Value])
          {
            grid.Rows[e.RowIndex].Cells[data.Columns[c+delta].ColumnName].Style.ForeColor = Color.Red;
          }
        }
        // mark repeating row
        if (rtPairs && cmp.RepeatRows.ContainsKey((int)grid[0, e.RowIndex].Value * 10 + (int)grid[1, e.RowIndex].Value))
        {
          grid.Rows[e.RowIndex].HeaderCell.Style.ForeColor = Color.DarkRed;
          grid.Rows[e.RowIndex].HeaderCell.Value = "R";
          grid.Rows[e.RowIndex].HeaderCell.ToolTipText = "Repeated row #" + cmp.RepeatRows[(int)grid[0, e.RowIndex].Value * 10 + (int)grid[1, e.RowIndex].Value].ToString();
        }
      }
    }
    //-------------------------------------------------------------------------
    void dgData_SelectionChanged(object sender, EventArgs e)
    {
      if (!reserveCell) currCell = null;
      SetCounter();
    }
    //-------------------------------------------------------------------------
    protected override void Dispose(bool disposing) // I got it from designer
    {
      if (disposing && (components != null))
        components.Dispose();
      Parent = null;
      dt = null;
      dtf = null;
      bs = null;
      dgData = null;
      cmp = null;
      GC.Collect();
      base.Dispose(disposing);
    }
    //-------------------------------------------------------------------------
    void dgData_KeyUp(object sender, KeyEventArgs e)
    {
      pushKey = Keys.None;
      if (e.KeyCode == Keys.End && dgData.CurrentCell != null) // fix bug - no transition at the end of row after changing column width
        dgData.FirstDisplayedCell = dgData[dgData.CurrentCell.ColumnIndex, dgData.FirstDisplayedScrollingRowIndex];
    }
    void dgData_KeyDown(object sender, KeyEventArgs e)
    {
      pushKey = e.Modifiers;
      if (e.KeyCode == Keys.F && e.Modifiers == Keys.Control)
      {
        tbFind.Focus();
        tbFind.SelectAll();
      }
      if (e.KeyCode == Keys.F3)
      {
        Find(e.Modifiers);
      }
      if (e.KeyCode == Keys.F4 && e.Modifiers == Keys.None)
      {
        Filter(e.Modifiers);
      }
      if (e.KeyCode == Keys.F4 && e.Modifiers == Keys.Shift)
      {
        FilterCancel();
      }
      if (e.KeyCode == Keys.F5)
      {
        GoDiff(e.Modifiers);
      }
      if (e.KeyCode == Keys.F6)
      {
        GoKey(e.Modifiers);
      }
      if (e.KeyCode == Keys.F7)
      {
        GoRepeated(e.Modifiers);
      }
      if (e.KeyCode == Keys.F9 && e.Modifiers == Keys.None)
      {
        bExcel.PerformClick();
      }
    }
    void tbFind_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.F3 || e.KeyCode == Keys.F4)
        dgData_KeyDown(dgData, e);
    }
    void tbFind_KeyUp(object sender, KeyEventArgs e)
    {
      pushKey = Keys.None;
    }
    private void bPin_Click(object sender, EventArgs e)
    {
      FindForm().TopMost = !FindForm().TopMost;
      bPin.Image = FindForm().TopMost ? Properties.Resources.pinon : Properties.Resources.pinoff;
    }
    //~~~~~~~~ Move commands ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    #region
    //-------------------------------------------------------------------------
    void bFind_Click(object sender, EventArgs e)
    {
      Find(pushKey);
    }
    void bFilter_Click(object sender, EventArgs e)
    {
      Filter(pushKey);
    }
    void bFilterCancel_Click(object sender, EventArgs e)
    {
      FilterCancel();
    }
    void bDiffs_Click(object sender, EventArgs e)
    {
      GoDiff(pushKey);
    }
    void bKeys_Click(object sender, EventArgs e)
    {
      GoKey(pushKey);
    }
    void bRepeats_Click(object sender, EventArgs e)
    {
      GoRepeated(pushKey);
    }
    //-------------------------------------------------------------------------
    /* Search */
    void Find(Keys key)
    {
      if (string.IsNullOrEmpty(tbFind.Text)) return;
      //if ((key & Keys.Control) == Keys.Control) return;
      bool ctrl = ((key & Keys.Control) == Keys.Control);
      bool fwd = ((key & Keys.Shift) != Keys.Shift);
      bool alt = ((key & Keys.Alt) == Keys.Alt);
      Func<int, int, List<int>> fn = (r, c) =>
      {
        return dgData.Rows[r].Cells.OfType<DataGridViewCell>()
          .Where(x => x.OwningColumn.Index >= delta && (!alt ? (fwd ? x.OwningColumn.Index > c : c > x.OwningColumn.Index) : c == x.OwningColumn.Index))
          .Where(y => y.ValueType == typeof(string) 
            ? ctrl ? y.Value.ToString().Contains(tbFind.Text) : y.Value.ToString().ToLower().Contains(tbFind.Text.ToLower())
            : y.Value.ToString() == tbFind.Text)
          .Select(z => z.OwningColumn.Index).ToList();
      };
      MoveCell(fwd, false, alt, false, fn);
    }
    //-------------------------------------------------------------------------
    /* Filter current column */
    void Filter(Keys key)
    {
      if (string.IsNullOrEmpty(tbFind.Text)) return;
      if (dgData.CurrentCell == null) return;
      bool ctrl = ((key & Keys.Control) == Keys.Control);
      try
      {
        Cursor = Cursors.WaitCursor;

        int c = dgData.CurrentCell.ColumnIndex;
        string colName = dgData.Columns[c].DataPropertyName;
        DataTable dtTmp = dt.Clone(); // table for filtered rows

        data.Rows.OfType<DataRow>()
          .Where(x => x[colName] is string
            ? ctrl ? x[colName].ToString().Contains(tbFind.Text) : x[colName].ToString().ToLower().Contains(tbFind.Text.ToLower())
            : x[colName].ToString() == tbFind.Text)
          .CopyToDataTable(dtTmp, LoadOption.PreserveChanges);
        
        if (rtPairs)
        {
          data.Rows.OfType<DataRow>()  // adding paired rows to a filtered set...
            .Where(x => dtTmp.Rows.Contains(new[] { x[0], -1 * ((int)x[1]%2 - 1) + (rtDiff ? 0 : 2)}) && !dtTmp.Rows.Contains(new[] { x[0], x[1] }))
            .CopyToDataTable(dtTmp, LoadOption.PreserveChanges);
          dtTmp.DefaultView.Sort = dtTmp.Columns[0].ColumnName + ", " + dtTmp.Columns[1].ColumnName; //... and sort in pairs
        }
        
        dtf = dtTmp;
        bs.DataSource = dtf;
        
        if (!filteredCols.ContainsKey(c))
          filteredCols.Add(c, dgData.Columns[c].HeaderCell.ToolTipText);
        dgData.Columns[c].HeaderCell.Style.Font = new Font(dgData.DefaultCellStyle.Font, FontStyle.Bold);
        dgData.Columns[c].HeaderCell.ToolTipText = filteredCols[c] + string.Format("{0}Filtered on \"{1}\"", string.IsNullOrEmpty(filteredCols[c]) ? "" : Environment.NewLine, tbFind.Text);

        SetCurrentCell(c, 0);
      }
      finally
      {
        Cursor = Cursors.Default;
      }
    }
    //-------------------------------------------------------------------------
    /* Reset all filters */
    void FilterCancel()
    {
      try
      {
        Cursor = Cursors.WaitCursor;

        bs.DataSource = dt;
        dtf = null;
        foreach (var d in filteredCols)
        {
          dgData.Columns[d.Key].HeaderCell.Style.Font = new Font(dgData.DefaultCellStyle.Font, FontStyle.Regular);
          dgData.Columns[d.Key].HeaderCell.ToolTipText = d.Value;
        }
        filteredCols.Clear();
        SetCurrentCell(delta, 0);
      }
      finally
      {
        Cursor = Cursors.Default;
      }
    }
    //-------------------------------------------------------------------------
    /* Move to next difference */
    void GoDiff(Keys key)
    {
      if (!rtDiff) return;
      if ((key & Keys.Control) == Keys.Control) return;
      if (cmp.Diffs.Count == 0) return;

      bool fwd = ((key & Keys.Shift) != Keys.Shift);
      bool alt = ((key & Keys.Alt) == Keys.Alt); 
      Func<int, int, List<int>> fn = (r, c) =>
      {
        List<int> cn = null;
        int p = (int)dgData[0, r].Value; // pair
        if (cmp.Diffs.ContainsKey(p) && cmp.Diffs[p] != null) // there is a list of differences for this pair
        {
          cn = dgData.Columns.OfType<DataGridViewColumn>()
            .Where(x => (!alt ? (fwd ? x.Index > c : c > x.Index) : c == x.Index))
            .Where(y => cmp.Diffs[p].Contains(y.Index - delta))
            .Select(n => n.Index).ToList();
        }
        return cn;
      };
      MoveCell(fwd, true, alt, false, fn);
    }
    //-------------------------------------------------------------------------
    /* Move to key value */
    void GoKey(Keys key)
    {
      if ((key & Keys.Alt) == Keys.Alt || (key & Keys.Control) == Keys.Control) return;
      if (cmp.KeyCols.Count == 0) return;
      if (currCell != null && (dgData.CurrentCell == null || dgData.CurrentCell.RowIndex != currCell.OwningRow.Index))
        currCell = null;
      if (key == Keys.Shift)
      {
        SetCurrentCell(currCell);
        currCell = null;
        return;
      }
      if (key == Keys.None)
      {
        Func<int, int, List<int>> fn = (r, c) =>
        {
          return dgData.Columns.OfType<DataGridViewColumn>()
            .Where(x => x.Index > c)
            .Where(y => cmp.KeyCols.Contains(y.Index - delta))
            .Select(n => n.Index).ToList();
        };
        if (currCell == null) currCell = dgData.CurrentCell;
        reserveCell = true;
        MoveCell(true, false, false, true, fn);
        reserveCell = false; 
      }
    }
    //-------------------------------------------------------------------------
    /* Move to next repeating row */
    void GoRepeated(Keys key)
    {
      if (!rtPairs) return;
      if ((key & Keys.Control) == Keys.Control || (key & Keys.Alt) == Keys.Alt) return;
      if (cmp.RepeatRows.Count == 0) return;

      bool fwd = ((key & Keys.Shift) != Keys.Shift);
      Func<int, int, List<int>> fn = (r, c) =>
      {
        List<int> cn = null;
        int p = (int)dgData[0, r].Value * 10 + (int)dgData[1, r].Value;
        if (cmp.RepeatRows.ContainsKey(p)) 
        {
          cn = dgData.Columns.OfType<DataGridViewColumn>()
            .Where(x => c == x.Index)
            .Select(n => n.Index).ToList();
        }
        return cn;
      };
      MoveCell(fwd, false, true, false, fn);
    }
    //-------------------------------------------------------------------------
    /* Moving to a specific cell from selected by delegate GetNextPosInRow */
    void MoveCell(bool fwd, bool throughRow, bool inCol, bool inRow, Func<int, int, List<int>> GetNextPosInRow)
    {
      // fwd - move forward, inCol/inRow - move inside current column/row
      // throughRow - jump over row for tables that contain pairs
      try
      {
        Cursor = Cursors.WaitCursor;

        if (inCol && inRow) return;
        int step = (throughRow ? 2 : 1) * (fwd ? 1 : -1); // step through the rows
        int row = dgData.CurrentRow.Index; // current row
        int r = row + (inCol ? step : 0), // start row
          c = dgData.CurrentCell != null ? dgData.CurrentCell.OwningColumn.Index : delta; // start column
        bool start = true;
        List<int> cn = null;

        while (start || (!inRow && (fwd ? r <= row : r >= row))) // if you have not started the second round of column way
        {
          if (fwd ? r >= dgData.RowCount : r < 0) // have reached the end/beginning of column - start first/end of column
          {
            r = fwd ? 0 : (dgData.RowCount - Math.Abs(step));
            start = false;
          }
          cn = null;
          if (GetNextPosInRow != null) cn = GetNextPosInRow(r, c); // suitable cells in this row
          if (cn != null && cn.Count > 0) 
            break;
          if (!inRow) 
            r = r + step; // next row
          else if (c == (fwd ? delta - 1 : dgData.ColumnCount)) // start the second round of row way
            start = false;
          if (!inCol) 
            c = fwd ? delta-1 : dgData.ColumnCount; // start column of row
        }

        if (cn != null && cn.Count > 0) // have suitable cells in row r
        {
          c = (fwd ? cn.Min() : cn.Max()); // suitable cell in row by according direction
          SetCurrentCell(c, r);
        }
      }
      finally
      {
        Cursor = Cursors.Default;
      }
    }
    //-------------------------------------------------------------------------
    void SetCurrentCell(int c, int r)
    {
      if (c < dgData.ColumnCount && r < dgData.RowCount)
        SetCurrentCell(dgData[c, r]);
    }
    void SetCurrentCell(DataGridViewCell cell)
    {
      if (cell == null) return;
      dgData.CurrentCell = cell;
      if (!dgData.CurrentCell.Displayed) 
        dgData.FirstDisplayedCell = dgData[dgData.CurrentCell.ColumnIndex, dgData.FirstDisplayedScrollingRowIndex];
      dgData.Focus();
      SetCounter();
    }
    //-------------------------------------------------------------------------
    void SetCounter()
    {
      int k = rtPairs ? 2 : 1;
      lblCount.Text = string.Format("{0}{1} {2} {3}", 
        dtf == null ? "" : string.Format("{0} of ", dtf.Rows.Count/k), 
        dt.Rows.Count/k, 
        rtPairs ? "pairs" : "rows",
        rtPairs && dgData.CurrentCell != null ? " : Pair " + ((int)dgData[0, dgData.CurrentRow.Index].Value + 1).ToString() : ""
        );
    }
    #endregion
    //~~~~~~~~ Send result ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    #region
    //-------------------------------------------------------------------------
    private void bExcel_Click(object sender, EventArgs e)
    {
      cmp.ToHTML(true, true, (FindForm() as FormResult).ResultPath, (FindForm() as FormResult).ResultFileName, onError: onError);
    }
    private void bHTML_Click(object sender, EventArgs e)
    {
      cmp.ToHTML(true, false, (FindForm() as FormResult).ResultPath, (FindForm() as FormResult).ResultFileName, onError: onError);
    }
    private void bSave_Click(object sender, EventArgs e)
    {
      SaveFileDialog dlg = new SaveFileDialog();
      dlg.Filter = @"HTML|*.htm*";
      dlg.DefaultExt = "htm";
      dlg.FilterIndex = 1;
      dlg.OverwritePrompt = true;
      dlg.FileName = (FindForm() as FormResult).ResultFileName; 
      dlg.InitialDirectory = (FindForm() as FormResult).ResultPath;
      if (dlg.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(dlg.FileName))
        cmp.ToHTML(false, false, Path.GetDirectoryName(dlg.FileName), Path.GetFileName(dlg.FileName), onError: onError);
    }
    #endregion


  }
}
