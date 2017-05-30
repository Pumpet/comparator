using System;
using System.Windows.Forms;

namespace Common
{
  /* Simple form for data table */
  public partial class FormFlatData : Form
  {
    BindingSource bs = new BindingSource();
    //-------------------------------------------------------------------------
    public FormFlatData(object data, string s)
    {
      InitializeComponent();
      bs.DataSource = data;
      dgData.DataSource = bs;
      status.Text = s;
    }
    //-------------------------------------------------------------------------
    private void FormFlatData_Load(object sender, EventArgs e)
    {
      SetDoubleBuffered(dgData, true);
    }
    //-------------------------------------------------------------------------
    private void FormFlatData_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Escape)
        Close();
    }
    //-------------------------------------------------------------------------
    public static void SetDoubleBuffered(Control control, bool setting) // prevent control blinking
    {
      System.Reflection.BindingFlags bFlags = System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic;
      control.GetType().GetProperty("DoubleBuffered", bFlags).SetValue(control, setting, null);
    }
  }
}
