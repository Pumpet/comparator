//
//  THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
//  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR
//  PURPOSE.
//
//  License: GNU Lesser General Public License (LGPLv3)
//
//  Email: datastrings.net@gmail.com
//
//  Copyright (C) Alex Rozanov, 2016 

// Special thanks to Pavel Torgashov for his FastColoredTextBox component!

using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Comparator
{
  static class Program
  {
    [DllImport("kernel32.dll")]
    private static extern bool AllocConsole(); // для вывода консоли в пакетном режиме

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
запуск:
1) comparator [-profile:""[путь]имя""]
2) comparator -batch -profile:""[путь]имя"" [-path:""путь""][-log:""[путь]имя""][-type:excel|html][-sendto:""адрес[;адрес...]""][-file:""имя""][-open]

параметры:
-profile:""[путь]имя_профиля"" - должен быть указан, если указан параметр -batch
-batch если не указан - откроем окно с пустым или указанным профилем, 
       если указан - запускаем в режиме командной строки, используя секцию <Options Host=""BATCH""> в comparator.xml и указанный профиль

параметры (необязательные) используемые только в случае -batch 
игнорируются если -batch не указан, перекрывают аналогичные из профиля и настроек приложения
-path:""путь для результата"" - в конце не должно быть слэша
-log:""[путь]имя_лога"" - лог-файл
-type:excel|html - тип результата
-sendto:""список адресатов для рассылки результата"" - если задан, но список пустой, результат не рассылается
-file:""имя файла результата""
-open - если задан, открыть результат

Press any key...";

      AllocConsole();
      Console.WriteLine(Legend);
      Console.ReadKey(true);
    }
  }
}
