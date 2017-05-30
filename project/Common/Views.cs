using System;
using System.Collections.Generic;

namespace Common
{
  /* Profile View */
  public interface IView
  {
    List<string> RecentFiles { set; } // last used profiles list in menu
    IViewSource viewSourceA { get; } // view for source A
    IViewSource viewSourceB { get; } // view for source B
    bool Compared { set; } // flag of successful comparison for current profile
    IntPtr ResultHWnd { get; set; } // result window

    event Action<bool> NewProfile;
    event Action<string> LoadProfile; 
    event Func<bool> SaveProfile; 
    event Func<bool> Check; // check profile
    event Action<TaskContext, TaskContext, TaskContext> Compare; // prepare and start comparison
    event Action CompareStop; // stop comparison
    event Action Result; // get last comparison result
    event Action DataEdit; // profile data has changed
    event Func<bool> CloseView; // attempt to close work

    event Action<bool, bool, bool, List<string>, List<string>> FillColPairs; // forming pairs of fields from field lists
    event Action<int[]> RemoveColPair; // delete pairs with specified indices
    event Func<int[], int, int> MoveColPair; // moving pairs with specified indices
    event Func<List<string>, List<string>, bool> GetFields; // lists of source field names, except for existing in pairs

    void SetDataProps(Dictionary<string, string> propNames); // set names of objects fields that will be bound to View elements (element name : field name)
    void SetData(Dictionary<string, object> bindings); // receive data objects (object name : object) and refresh
    void RefreshData(Dictionary<string, object> bindings, params string[] bsNames); // refresh view from data objects (object name : object) in source with bsNames
    void WaitResult(bool wait, string msg = ""); // start-stop waiting result message
    string LoadFile(string folder, string filter, string ext); // load file prompt
    string SaveFile(string folder, string name, string filter, string ext); // save file prompt
    string SaveRequest(); // message if need to save
  }
  //===========================================================================
  /* Source View */
  public interface IViewSource
  {
    IView ParentView { get; set; } // profile view
    ISqlView SqlView { get; set; } // db-source editor view 
    event Func<string, object, object> Command; // source event that requires processing in accordance with parameters (command name, some data)
  }
  //===========================================================================
  /* DB-Source editor View */
  public interface ISqlView
  {
    event Func<Action<string, Exception>, bool> TestConnect; // test connection
    event Action<string, TaskContext> GetData; // start getting data process
    event Action StopGetData; // stop getting data process
    void SetData(object inData, object providers, object fields); // receive data object and refresh
    void SetDataProps(Dictionary<string, string> propNames); // set names of objects fields that will be bound to View elements (element name : field name)
  }
}
