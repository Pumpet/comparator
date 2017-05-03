using System;
using System.Collections.Generic;

namespace Common
{
  /* интерфейс профиля */
  public interface IView
  {
    List<string> RecentFiles { set; } // последние профили - для меню
    IViewSource viewSourceA { get; } // интерфейс источника A
    IViewSource viewSourceB { get; } // интерфейс источника B
    bool Compared { set; } // признак успешного запуска последней сверки текущего профиля
    IntPtr ResultHWnd { get; set; } // окно результатов

    event Action<bool> NewProfile;
    event Action<string> LoadProfile;
    event Func<bool> SaveProfile;
    event Func<bool> Check; // проверить данные профиля
    event Action<TaskContext, TaskContext, TaskContext> Compare; // запуск сверки
    event Action CompareStop; // остановка сверки
    event Action Result; // выдача результатов последней сверки
    event Action DataEdit; // данные изменились
    event Func<bool> CloseView;

    event Action<bool, bool, bool, List<string>, List<string>> FillColPairs; // заполнение списка пар выбранными полями источников
    event Action<int[]> RemoveColPair; // удаление пар из списка
    event Func<int[], int, int> MoveColPair; // перемещение пар
    event Func<List<string>, List<string>, bool> GetFields; // получить списки имен полей источников для выбора в пары

    void SetDataProps(Dictionary<string, string> propNames); // установка имен полей объектов с данными, которые будут привязаны к элементам вью
    void SetData(Dictionary<string, object> bindings); // смена данных - прием объектов с данными (словарь имя_объекта:объект) и обновление вью
    void RefreshData(Dictionary<string, object> bindings, params string[] bsNames); // обновление вью для объектов данных с указанными именами
    void WaitResult(bool wait, string msg = ""); // ожидание формирования результата сверки (часы, запрет прерывания)
    string LoadFile(string folder, string filter, string ext); // диалог загрузки файла
    string SaveFile(string folder, string name, string filter, string ext); // диалог сохранения файла
    string SaveRequest(); // предложение сохранить профиль
  }
  //===========================================================================
  /* интерфейс настроек источников */
  public interface IViewSource
  {
    IView ParentView { get; set; } // интерфейс профиля
    ISqlView SqlView { get; set; } // редактор для источника-БД
    event Func<string, object, object> Command; // событие источника, требующее обработки в соответствии с параметром-командой
  }
  //===========================================================================
  /* интерфейс вью коннекта и запроса */
  public interface ISqlView
  {
    event Func<Action<string, Exception>, bool> TestConnect; // проверить подключение
    event Action<string, TaskContext> GetData; // запустить процесс получения данных
    event Action StopGetData; // остановить процесс получения данных
    void SetData(object inData, object providers, object fields); // прием объектов с данными и обновление вью
    void SetDataProps(Dictionary<string, string> propNames); // установка имен полей объектов с данными, которые будут привязаны к элементам вью
  }
}
