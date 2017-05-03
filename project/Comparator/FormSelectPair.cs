using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Comparator
{
  public partial class FormSelectPair : Form
  {
    public event Action<bool, bool, bool, List<string>, List<string>> SetPairs;
    List<string> listA;
    List<string> listB;
    ToolTip tt = new ToolTip();
    //-------------------------------------------------------------------------
    public FormSelectPair()
    {
      InitializeComponent();
    }

    public FormSelectPair(List<string> a, List<string> b)
    {
      InitializeComponent();  
      listA = new List<string>(a);
      listB = new List<string>(b);
      listA.RemoveAll(x => string.IsNullOrEmpty(x));
      listB.RemoveAll(x => string.IsNullOrEmpty(x));
      tt.InitialDelay = 1;
    }

    private void FormSelectPair_Load(object sender, EventArgs e)
    {
      clbA.Items.AddRange(listA.ToArray());
      clbB.Items.AddRange(listB.ToArray());
    }

    private void FormSelectPair_KeyUp(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Escape)
        Close();
      if (e.KeyCode == Keys.Enter)
        bSet.PerformClick();
    }

    //-------------------------------------------------------------------------
    private void checkBox1_CheckedChanged(object sender, EventArgs e)
    {
      CheckedChanged(clbA, ((CheckBox)sender).Checked);
    }

    private void checkBox2_CheckedChanged(object sender, EventArgs e)
    {
      CheckedChanged(clbB, ((CheckBox)sender).Checked);
    }

    private void CheckedChanged(CheckedListBox clb, bool check)
    {
      for (int i = 0; i < clb.Items.Count; i++)
        clb.SetItemChecked(i, check);
      clb.Refresh();
      clb.Focus();
      if (clb.SelectedIndex >= 0)
        clb.SetSelected(clb.SelectedIndex, true);
      else if (clb.Items.Count > 0)
        clb.SetSelected(0, true);
    }
    
    private void bSet_Click(object sender, EventArgs e)
    {
      if (SetPairs == null || (clbA.CheckedItems.Count == 0 && clbB.CheckedItems.Count == 0)) return;
      SetPairs(cbClear.Checked, rbKey.Checked, rbMatch.Checked,
        clbA.CheckedItems.OfType<string>().ToList(), clbB.CheckedItems.OfType<string>().ToList());
      List<object> l = new List<object>(clbA.CheckedItems.OfType<object>());
      foreach (var item in l)
	    {
        clbA.Items.Remove(item);
	    }
      l = new List<object>(clbB.CheckedItems.OfType<object>());
      foreach (var item in l)
      {
        clbB.Items.Remove(item);
      }
      Refresh();
    }

    //-------------------------------------------------------------------------
    private void clbA_MouseMove(object sender, MouseEventArgs e)
    {
      SetToolTip(clbA, e.Location);
    }
    
    private void clbB_MouseMove(object sender, MouseEventArgs e)
    {
      SetToolTip(clbB, e.Location);
    }

    private void SetToolTip(ListBox list, Point p)
    {
      int idx = list.IndexFromPoint(p);
      if (idx >= 0)
      {
        string s = list.Items[idx].ToString();
        Graphics g = list.CreateGraphics();
        if (s != tt.GetToolTip(list))
          tt.SetToolTip(list, list.Width - 30 < g.MeasureString(s, list.Font).Width ? s : null);
      }
      else
        tt.SetToolTip(list, null);    
    }

    //-------------------------------------------------------------------------
    private void bUp_Click(object sender, EventArgs e)
    {
      MoveListRow(-1, clbA);
    }

    private void bDown_Click(object sender, EventArgs e)
    {
      MoveListRow(1, clbA);
    }

    private void bUpB_Click(object sender, EventArgs e)
    {
      MoveListRow(-1, clbB);
    }

    private void bDownB_Click(object sender, EventArgs e)
    {
      MoveListRow(1, clbB);
    }

    private void clbA_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.Control && e.KeyCode == Keys.Up)
      {
        MoveListRow(-1, (sender as CheckedListBox));
        e.SuppressKeyPress = true;
      }
      if (e.Control && e.KeyCode == Keys.Down)
      {
        MoveListRow(1, (sender as CheckedListBox));
        e.SuppressKeyPress = true;
      }
    }

    private void MoveListRow(int offset, CheckedListBox lb)
    {
      if (lb.SelectedIndex < 0) return;
      int idx = lb.SelectedIndex;
      if ((offset == -1 && idx == 0) || (offset == 1 && idx == lb.Items.Count - 1))
        return;

      string s = lb.Items[idx].ToString();
      bool ch = false;
      ch = lb.GetItemChecked(idx);
      lb.Items.RemoveAt(idx);
      lb.Items.Insert(idx + offset, s);
      lb.SetItemChecked(idx+offset, ch);
      lb.Refresh();
      lb.SetSelected(idx, false);
      lb.Focus();
      lb.SetSelected(idx+offset, true);
    }

    //-------------------------------------------------------------------------
    private void clbA_SelectedIndexChanged(object sender, EventArgs e)
    {
      SelectPair(clbA, clbB);
    }

    private void clbB_SelectedIndexChanged(object sender, EventArgs e)
    {
      SelectPair(clbB, clbA);
    }

    private void clbA_ItemCheck(object sender, ItemCheckEventArgs e)
    {
      SelectPair(clbA, clbB, e.NewValue == CheckState.Checked, e.NewValue == CheckState.Checked);
    }

    private void clbB_ItemCheck(object sender, ItemCheckEventArgs e)
    {
      SelectPair(clbB, clbA, e.NewValue == CheckState.Checked, e.NewValue == CheckState.Checked);
    }

    private void SelectPair(CheckedListBox lb, CheckedListBox lb2, bool select = true, bool justSelected = false)
    {
      if (!lb.Focused) return;
      lb2.ClearSelected();
      if (lb.SelectedIndices.Count > 0 && (lb.GetItemChecked(lb.SelectedIndex) || justSelected))
      {
        int i = lb.CheckedIndices.OfType<int>().Count(x => x < lb.SelectedIndex);
        if (i < lb2.CheckedItems.Count)
          lb2.SetSelected(lb2.CheckedIndices[i], select);
      }
    }
  }
}
