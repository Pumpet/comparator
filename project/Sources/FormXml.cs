using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using FastColoredTextBoxNS;
using Common;

namespace Sources
{
  public partial class FormXml : Form
  {
    const int ROWS_TO_XLS = 100000; // максимальное кол-во строк грида в эксель
    const int MAX_XMLBYTES_FOR_COLOR = 256000; // макс.размер байт xml, который сможем подсветить без тормозов
    string appPath = AppDomain.CurrentDomain.BaseDirectory;
    BindingSource bs = new BindingSource();
    BindingSource bsFields = new BindingSource();
    //~~~~~~~~ Form ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    #region
    //-------------------------------------------------------------------------
    public FormXml()
    {
      InitializeComponent();
    }
    //-------------------------------------------------------------------------
    public void SetData(object data)
    {
      bs.DataSource = data;
      bsFields.DataSource = ((XmlContent)bs.DataSource).FieldsMap;
    }
    //-------------------------------------------------------------------------
    private void FormXslt_Load(object sender, EventArgs e)
    {
      SetDoubleBuffered(dgResult, true);
      SetDoubleBuffered(dgFields, true);
      SetBindings();
      rbXslt_CheckedChanged(null, null);
      cbUseFieldsMap_CheckedChanged(null, null);
      tbNameXml.Select(); // тут Focus() для TextBox не работает
      tbNameXml.ScrollToCaret();
    }
    //-------------------------------------------------------------------------
    private void FormXslt_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (Validate(false))
      {
        bs.EndEdit();
        bsFields.EndEdit();
        tbResult.Clear();
        tbResult.ClearUndo(); // если так не делать - будет утекать память при смене xml
        dgResult.DataSource = null;
        XmlController.Dt = null;
        XmlController.Fields = null;
        GC.Collect();
        XmlController.FreeForm();
      }
      else
        e.Cancel = true;
    }
    //-------------------------------------------------------------------------
    private void SetBindings()
    {
      tbNameXml.DataBindings.Add("Text", bs, "NameXml", true, DataSourceUpdateMode.OnPropertyChanged);
      tbCodepage.DataBindings.Add("Text", bs, "Codepage", true, DataSourceUpdateMode.OnPropertyChanged);
      tbNameXslt.DataBindings.Add("Text", bs, "NameXslt", true, DataSourceUpdateMode.OnPropertyChanged);
      tbPathRow.DataBindings.Add("Text", bs, "PathRow", true, DataSourceUpdateMode.OnPropertyChanged);
      tbXslt.DataBindings.Add("Text", bs, "XsltScript");

      dgFields.AutoGenerateColumns = false;
      dgFields.DataSource = bsFields;
      dgFields.Columns["colFieldName"].DataPropertyName = "FieldName";
      dgFields.Columns["colPath"].DataPropertyName = "Path";
      dgFields.Columns["colDefault"].DataPropertyName = "Default";
      
      // порядок важен! элементы с событиями - в конце 
      rbXsltNotUse.DataBindings.Add("Checked", bs, "XsltNone");
      rbXsltScript.DataBindings.Add("Checked", bs, "XsltFromScript");
      rbXsltFile.DataBindings.Add("Checked", bs, "XsltFromFile");

      cbUseFieldsMap.DataBindings.Add("Checked", bs, "UseFieldsMap", true, DataSourceUpdateMode.OnPropertyChanged);
      cbFromTag.DataBindings.Add("Checked", bs, "DataFromTag", true, DataSourceUpdateMode.OnPropertyChanged);
      cbFromAttrib.DataBindings.Add("Checked", bs, "DataFromAttr", true, DataSourceUpdateMode.OnPropertyChanged);
    }
    //-------------------------------------------------------------------------
    void SetDoubleBuffered(Control control, bool setting) // чтобы грид не мерцал
    {
      System.Reflection.BindingFlags bFlags = System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic;
      control.GetType().GetProperty("DoubleBuffered", bFlags).SetValue(control, setting, null);
    }
    //-------------------------------------------------------------------------
    private void FormXslt_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.F5)
        bConvert.PerformClick();
      if (e.KeyCode == Keys.Escape)
        Close();
    }
    #endregion
    //~~~~~~~~ Behavior ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    #region
    //-------------------------------------------------------------------------
    private void bSelectFileXml_Click(object sender, EventArgs e)
    {
      FileDialog fileDialog = new OpenFileDialog() { Filter = "XML|*.xml", Title = "Select XML File", InitialDirectory = appPath };
      fileDialog.ShowDialog();
      if (!string.IsNullOrEmpty(fileDialog.FileName))
        tbNameXml.Text = fileDialog.FileName;
    }
    //-------------------------------------------------------------------------
    private void bSelectFileXslt_Click(object sender, EventArgs e)
    {
      FileDialog fileDialog = new OpenFileDialog() { Filter = "XSLT|*.xsl;*.xslt", Title = "Select XSLT File", InitialDirectory = appPath };
      fileDialog.ShowDialog();
      if (!string.IsNullOrEmpty(fileDialog.FileName))
        tbNameXslt.Text = fileDialog.FileName;
    }
    //-------------------------------------------------------------------------
    private void rbXslt_CheckedChanged(object sender, EventArgs e)
    {
      if (sender != null && !((RadioButton) sender).Checked)
        return;

      tbNameXslt.ReadOnly = !rbXsltFile.Checked;
      tbNameXslt.BackColor = rbXsltFile.Checked ? SystemColors.Window : SystemColors.Control;
      bSelectFileXslt.Enabled = rbXsltFile.Checked;
      tbXslt.ReadOnly = !rbXsltScript.Checked;
      tbXslt.BackColor = rbXsltScript.Checked ? SystemColors.Window : SystemColors.Control;
      if (rbXsltScript.Checked && tabPageXslt.Parent == null)
      {
        tabs.TabPages.Insert(0, tabPageXslt);
        tabs.SelectedTab = tabPageXslt;
      }
      else if (!rbXsltScript.Checked)
        tabPageXslt.Parent = null;

      if (sender != null) 
        ((RadioButton) sender).Focus();
    }
    //-------------------------------------------------------------------------
    private void tabs_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (tabs.SelectedTab != null)
        tabs.SelectedTab.Controls[0].Focus();
    }
    //-------------------------------------------------------------------------
    private void cbFrom_CheckedChanged(object sender, EventArgs e)
    {
      if (!cbFromTag.Checked && !cbFromAttrib.Checked)
        ((CheckBox)sender).Checked = true;
    }
    //-------------------------------------------------------------------------
    private void cbUseFieldsMap_CheckedChanged(object sender, EventArgs e)
    {
      dgFields.ReadOnly = !cbUseFieldsMap.Checked;
      dgFields.ContextMenuStrip = cbUseFieldsMap.Checked ? cmsFields : null;
      cbFromTag.Enabled = !cbUseFieldsMap.Checked;
      cbFromAttrib.Enabled = !cbUseFieldsMap.Checked;

      if (cbUseFieldsMap.Checked && tabPageMap.Parent == null)
      {
        tabs.TabPages.Add(tabPageMap);
        tabs.SelectedTab = tabPageMap;
      }
      else if (!cbUseFieldsMap.Checked)
        tabPageMap.Parent = null;
    }
    //-------------------------------------------------------------------------
    private void cmsResult_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
    {
      if (e.ClickedItem == miExcel)
      {
        try
        {
          ExcelProc.GridToExcel(dgResult, ROWS_TO_XLS);
        }
        catch (Exception ex)
        {
          MessageBox.Show(string.Format("Send to Excel error: {0}", ex.Message),"Excel error", MessageBoxButtons.OK, MessageBoxIcon.Error);          
        }
      }
    }
    //-------------------------------------------------------------------------
    private void cmsFields_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
    {
      if (!cbUseFieldsMap.Checked) return;

      if ((e.ClickedItem == miAdd || e.ClickedItem == miAddFromResult)
          && Validate(false)  
          && !(dgFields.CurrentRow != null
                       && (dgFields.CurrentRow.Cells[0].Value == null
                           || dgFields.CurrentRow.Cells[1].Value == null
                           || string.IsNullOrEmpty(dgFields.CurrentRow.Cells[0].Value.ToString().Trim())
                           || string.IsNullOrEmpty(dgFields.CurrentRow.Cells[1].Value.ToString().Trim()))))
      {
        dgFields.EndEdit();
        if (e.ClickedItem == miAdd)
          ((List<FieldMap>)bsFields.DataSource).Add(new FieldMap());
        if (e.ClickedItem == miAddFromResult && XmlController.Dt != null)
          foreach (var field in XmlController.Fields)
            ((List<FieldMap>) bsFields.DataSource).Add(new FieldMap {FieldName = field.FieldName, Path = field.Path, Default = field.Default});
        bsFields.ResetBindings(false);
        if (dgFields.RowCount > 0) dgFields[0, dgFields.RowCount - 1].Selected = true;
        bsFields.EndEdit();
      }

      if (e.ClickedItem == miDelete && dgFields.RowCount != 0 && dgFields.CurrentRow != null)
      {
        ((List<FieldMap>) bsFields.DataSource).RemoveAt(dgFields.CurrentRow.Index);
        if (dgFields.CurrentRow.Index > 0 && dgFields.CurrentRow.Index == dgFields.RowCount - 1)
          dgFields[0, dgFields.CurrentRow.Index - 1].Selected = true;
        dgFields.CausesValidation = false;
        bsFields.ResetBindings(false);
        dgFields.CausesValidation = true;
        bsFields.EndEdit();
      }

      if (e.ClickedItem == miDeleteAll)
      {
        ((List<FieldMap>) bsFields.DataSource).Clear();
        bsFields.ResetBindings(false);
        bsFields.EndEdit();
      }
    }
    //-------------------------------------------------------------------------
    private void dgFields_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.Control && e.KeyCode == Keys.Insert)
        cmsFields_ItemClicked(null, new ToolStripItemClickedEventArgs(miAdd));
      if (e.Control && e.KeyCode == Keys.Delete)
        cmsFields_ItemClicked(null, new ToolStripItemClickedEventArgs(miDelete));
    }
    //-------------------------------------------------------------------------
    private void dgFields_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
    {
      if (e.RowIndex >= 0 && dgFields.CausesValidation)
      {
        bool err0 = dgFields[0, e.RowIndex].Value == null || string.IsNullOrEmpty(dgFields[0, e.RowIndex].Value.ToString().Trim());
        bool err1 = dgFields[1, e.RowIndex].Value == null || string.IsNullOrEmpty(dgFields[1, e.RowIndex].Value.ToString().Trim());
        dgFields[0, e.RowIndex].ErrorText = err0 ? dgFields.Columns[0].Name + " not set!" : "";
        dgFields[1, e.RowIndex].ErrorText = err1 ? dgFields.Columns[1].Name + " not set!" : "";
        e.Cancel = err0 || err1;
      }
    }
    //-------------------------------------------------------------------------
    private void dgFields_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        DataGridView.HitTestInfo hit = dgFields.HitTest(e.X, e.Y);
        if (hit.Type == DataGridViewHitTestType.Cell)
          dgFields[hit.ColumnIndex, hit.RowIndex].Selected = true;
      }
    }
    //-------------------------------------------------------------------------
    private void dgFields_CellEndEdit(object sender, DataGridViewCellEventArgs e)
    {
      dgFields[e.ColumnIndex, e.RowIndex].ErrorText = "";
    }
    #endregion
    //~~~~~~~~ Convert ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    #region
    //-------------------------------------------------------------------------
    private void bConvert_Click(object sender, EventArgs e)
    {
      Cursor = Cursors.WaitCursor;
      try
      {
        bs.EndEdit();
        bsFields.EndEdit();

        tbResult.Clear();
        tbResult.ClearUndo();
        dgResult.DataSource = null;
        XmlController.Dt = null;
        GC.Collect();

        statusLabel.Text = "";
        dgResult.ContextMenuStrip = null;
        Refresh();

        XmlDocument xml = XmlController.GetXml(CommonProc.GetFilePath(tbNameXml.Text), tbCodepage.Text);
        if (!rbXsltNotUse.Checked)
          xml = XmlController.Transform(xml, rbXsltFile.Checked ? CommonProc.GetFilePath(tbNameXslt.Text) : tbXslt.Text, rbXsltFile.Checked);
        if (xml != null)
        {
          tbResult.Language = xml.OuterXml.Length > MAX_XMLBYTES_FOR_COLOR ? Language.Custom : Language.HTML;
          tbResult.Text = xml.OuterXml;
        }
        if (cbUseFieldsMap.Checked)
          XmlController.Dt = XmlController.GetData(xml, tbPathRow.Text, (List<FieldMap>) bsFields.DataSource);
        else
          XmlController.Dt = XmlController.GetData(xml, tbPathRow.Text, cbFromTag.Checked, cbFromAttrib.Checked);
        dgResult.DataSource = XmlController.Dt;
        statusLabel.Text = string.Format("{0} rows converted", XmlController.Dt.Rows.Count);
        if (XmlController.Dt.Rows.Count > 0) dgResult.ContextMenuStrip = cmsResult;
        tabs.SelectedTab = tabPageResult;
      }
      catch (Exception ex)
      {
#if DEBUG
        MessageBox.Show(string.Format("{0}\n{1}", ex.Message, ex.InnerException), "Convert error", MessageBoxButtons.OK, MessageBoxIcon.Error);
#else        
        MessageBox.Show(ex.Message, "Convert error", MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif
      }
      finally
      {
        Cursor = Cursors.Default;
      }
    }
    #endregion
  }
}
