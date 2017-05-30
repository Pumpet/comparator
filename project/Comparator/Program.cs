//
//  THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
//  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR
//  PURPOSE.
//
//  License: GNU Lesser General Public License (LGPLv3)
//
//  Email: pumpet.net@gmail.com
//  Git: https://github.com/Pumpet/comparator
//  Copyright (C) Alex Rozanov, 2016 
//
// Special thanks to Pavel Torgashov for his excellent FastColoredTextBox component!
// https://github.com/PavelTorgashov/FastColoredTextBox
//

using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Comparator
{
  static class Program
  {
    [DllImport("kernel32.dll")]
    private static extern bool AllocConsole(); 

    [STAThread]
    static void Main(string[] args)
    {
      if (args.Length == 1 && args[0] == "-?")
      {
        Legend();
        Environment.Exit(0);
      }
      bool batch = Array.Exists(args, s => s.ToLower() == "-batch");
      if (batch) 
      {
        AllocConsole();
        Master master = new Master(true, null, args);
        Application.Run();
      }
      else 
      {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        FormView form = new FormView();
        Master master = new Master(false, form, args);
        Application.Run(form);
      }
    }
    //-------------------------------------------------------------------------
    static void Legend()
    {
      string Legend = @"
Launch modes:
1) comparator [-profile:""[path]filename""]
2) comparator -batch -profile:""[path]filename"" [-path:""path""][-log:""[path]filename""][-type:excel|html][-sendto:""address[;address...]""][-file:""filename""][-open]

Required:
-batch – defines console mode
-profile:""pathfilename"" – file containing the profile;

Optional, ignored if -batch is not defined, have higher priority than respective settings from the application profile and settings:
-path:""path"" – path where the results will be saved (do not use ""/"" in the end);
-log:""[path]filename"" – log file;
-type:excel|html – result file type (excel or html);
-sendto:""address[;address...]"" – mailing list; when specified but the list is empty, the results will not be sent;
-file:""filename"" – result file name;
-open – when specified, the result file will be opened.

Press any key...";

      AllocConsole();
      Console.WriteLine(Legend);
      Console.ReadKey(true);
    }
  }
}
