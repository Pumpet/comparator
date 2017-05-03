using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Globalization;
using System.Drawing;
using System.Windows.Forms;

namespace DataComparer
{
  // тип результата
  public enum ResultType { rtDiff, rtIdent, rtA, rtB };
  // тип сравнения
  /// <summary>
  /// тип сравнения для определенного типа столбцов: 
  /// не сравнивать (NoMatch), сравнивать все поля таблицы (All), сравнивать указанные (Selected)
  /// </summary>
  // типы столбцов м.б.ключевые или "для сравнения", т.е. которые сравниваем в парах 
  // если ключевые столбцы не сравниваются, то пары определяются по порядку строк
  public enum MatchType { NoMatch, All, Selected }
  //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
  /// <summary>Cравнение двух таблиц</summary>
  public static class DSComparer
  {
    public static Rectangle FormRect;
    public static FormWindowState WinState = FormWindowState.Normal;
    public const string emptyColumnMark = "__EMPTY__";
    //-------------------------------------------------------------------------
    class RowPair   // пара строк, метод и результат сверки
    {
      public DataRow row1, row2;
      public int rn1 = -1, rn2 = -1;
      public List<int> diffCols = new List<int>(); // столбцы, значения которых не совпали
      public bool isRepeatRow1, isRepeatRow2; // признак повторного включения столбца в пару
      public bool eq = true; // признак отсутвия расхождений (нет diffCols)
    }
    //-------------------------------------------------------------------------------------------------
    /// <summary>Сверка</summary>
    /// <param name="data">таблицы 1 и 2</param>
    /// <param name="keysMatchType">тип сравнения ключей</param>
    /// <param name="colsMatchType">тип сравнения столбцов</param>
    /// <param name="checkRepeats">проверять повторы строк</param>
    /// <param name="tryConvert">преобразовать строку в дату-время или число</param>
    /// <param name="nullAsStr">null как пустая строка</param>
    /// <param name="caseSens">различать регистр</param>
    /// <param name="diffOnly">выводить только отличия (т.е. пары с отличиями и строки в одной из таблиц)</param>
    /// <param name="onlyA">выводить строки, обнаруженные только в таблице 1</param>
    /// <param name="onlyB">выводить строки, обнаруженные только в таблице 2</param>
    /// <param name="colNames">имена всех столбцов таблиц, которые хотим видеть в результате (если не задать - будут все поля таблицы)</param>/// 
    /// <param name="keyColNames">набор имен ключевых столбцов таблиц (в одинаковом порядке)</param>
    /// <param name="matchColNames">набор имен столбцов для сравнения (в одинаковом порядке)</param>
    /// <param name="onProgress">метод отражения хода процесса</param>
    public static CompareResult Compare(DataTable[] data, MatchType keysMatchType, MatchType colsMatchType, bool checkRepeats, 
                      bool tryConvert, bool nullAsStr, bool caseSens,
                      bool diffOnly, bool onlyA, bool onlyB,
                      IEnumerable<string>[] colNames, IEnumerable<string>[] keyColNames, IEnumerable<string>[] matchColNames,
                      Action<object> onProgress = null)
    {
      DataTable[] dts = new DataTable[2];
      List<string>[] cols = new List<string>[2];
      List<string>[] keys = new List<string>[2];
      List<string>[] match = new List<string>[2];
      List<int> keyCols = new List<int>(); // номера ключевых столбцов
      List<int> matchCols = new List<int>(); // номера столбцов для сверки

      // подготовка
      for (int i = 0; i < 2; i++)
      {
        if (data[i] == null)
          throw new Exception("Data table is not defined");
        dts[i] = data[i].Copy();
        cols[i] = colNames[i] != null ? colNames[i].ToList() : dts[i].Columns.OfType<DataColumn>().Select(x=>x.ColumnName).ToList();
        keys[i] = keyColNames[i] != null ? keyColNames[i].ToList() : new List<string>();
        match[i] = matchColNames[i] != null ? matchColNames[i].ToList() : new List<string>();
        DtPrepare(dts[i], cols[i]);      
      }
      // пустые столбцы в меньшую таблицу
      for (int i = 0; i < Math.Abs(dts[0].Columns.Count - dts[1].Columns.Count); i++)
      {
        DataColumn newCol = new DataColumn("", typeof(string));
        newCol.Caption = emptyColumnMark;
        newCol.DefaultValue = string.Empty;
        ((dts[0].Columns.Count - dts[1].Columns.Count) > 0 ? dts[0] : dts[1]).Columns.Add(newCol);
      }
      // список номеров ключевых столбцов
      if (keysMatchType == MatchType.All)
      {
        keyCols = dts[0].Columns.OfType<DataColumn>().Select(x => x.Ordinal).OrderBy(y => y).ToList();
        colsMatchType = MatchType.NoMatch;
      }
      if (keysMatchType == MatchType.Selected)
      {
        keyCols = dts[0].Columns.OfType<DataColumn>().Where(c => keys[0].Contains(c.ColumnName)).Select(x => x.Ordinal).OrderBy(y => y).ToList();
        if (!keyCols.SequenceEqual(dts[1].Columns.OfType<DataColumn>().Where(c => keys[1].Contains(c.ColumnName)).Select(x => x.Ordinal).OrderBy(y => y).ToList()))
          throw new Exception("Not identical key sets!");
      }
      if (keyCols.Count == 0 && keysMatchType != MatchType.NoMatch)
        throw new Exception("Key fields is not defined");
      // список номеров столбцов для сверки
      if (colsMatchType == MatchType.All)
        matchCols = dts[0].Columns.OfType<DataColumn>()
          .Where(c => c.Caption != emptyColumnMark && dts[1].Columns[c.Ordinal].Caption != emptyColumnMark)
          .Select(x => x.Ordinal).Except(keyCols).OrderBy(y => y).ToList();
      if (colsMatchType == MatchType.Selected)
      {
        matchCols = dts[0].Columns.OfType<DataColumn>().Where(c => match[0].Contains(c.ColumnName)).Select(x => x.Ordinal).Except(keyCols).OrderBy(y => y).ToList();
        if (!matchCols.SequenceEqual(dts[1].Columns.OfType<DataColumn>().Where(c => match[1].Contains(c.ColumnName)).Select(x => x.Ordinal).Except(keyCols).ToList()))
          throw new Exception("Not identical match sets!");
      }
      // подбор пар строк
      List<RowPair> rowPairs = GetPairs(dts[0], dts[1], keyCols, (keysMatchType != MatchType.NoMatch), checkRepeats, caseSens, onProgress);
      // сравнение полей в парах
      for (int i = 0; i < rowPairs.Count; i++)
      {
        if (onProgress != null && i % 100 == 0)
          onProgress(new Tuple<int, string>(100 * i / rowPairs.Count, string.Format("{0} pairs matched", i)));
        MatchPair(rowPairs[i], matchCols, tryConvert, nullAsStr, caseSens);
      }
      // объект результата
      return ResultPrepare(dts, keyCols, matchCols, rowPairs, diffOnly, onlyA, onlyB);
    }
    //-------------------------------------------------------------------------
    /* формирование объекта результата */
    static CompareResult ResultPrepare(DataTable[] dt, List<int> keyCols, List<int> matchCols, List<RowPair> rowPairs,
      bool diffOnly, bool onlyA, bool onlyB)
    {
      int cc = dt[0].Columns.Count;
      int rowsA = dt[0].Rows.Count, rowsB = dt[1].Rows.Count;
      Dictionary<int, List<int>> diffs = new Dictionary<int, List<int>>(); // словарь для списков номеров столбцов расхождений (по номеру пары)
      Dictionary<int, int> repeatRows = new Dictionary<int, int>(); // номера повторяющихся строк (ключ=как номер_пары*10+номер_строки_в_паре)
      List<string>[] colNames = new List<string>[]{new List<string>(), new List<string>()}; // заголовки столбцов результата для каждого источника
      string nameA = dt[0].TableName, nameB = dt[1].TableName;
      
      // заполнение таблицы расхождений (она есть всегда)
      DataTable dtDiff = new DataTable();
      dtDiff.Columns.Add("", typeof(int)).Caption = "PairNo"; // служебный столбец 0 для номеров пар
      dtDiff.Columns.Add("", typeof(int)).Caption = "Source"; // служебный столбец 1 для номеров строк внутри пары
      dtDiff.PrimaryKey = new[] { dtDiff.Columns[0], dtDiff.Columns[1] };
      for (int i = 0; i < cc; i++) // столбцы
      {
        DataColumn dc = dtDiff.Columns.Add(dt[0].Columns[i].ColumnName == dt[1].Columns[i].ColumnName ? dt[0].Columns[i].ColumnName : dt[0].Columns[i].ColumnName + "_" + dt[1].Columns[i].ColumnName);
        dc.DataType = dt[0].Columns[i].DataType == dt[1].Columns[i].DataType ? dt[0].Columns[i].DataType : typeof(object);
        colNames[0].Add(dt[0].Columns[i].Caption != emptyColumnMark ? dt[0].Columns[i].Caption : "");
        colNames[1].Add(dt[1].Columns[i].Caption != emptyColumnMark ? dt[1].Columns[i].Caption : "");
        dc.Caption = colNames[0][i] + "\n" + colNames[1][i];
      }
      int rp = 0;
      foreach (RowPair pair in rowPairs.Where(x => !x.eq)) // строки - по парам, через массив, с учетом служебных столбцов, номера строк внутри пары - 0 и 1
      {
        object[] arr1 = new object[cc + 2], arr2 = new object[cc + 2];
        arr1[0] = rp; arr1[1] = 0; Array.Copy(pair.row1.ItemArray, 0, arr1, 2, cc);
        arr2[0] = rp; arr2[1] = 1; Array.Copy(pair.row2.ItemArray, 0, arr2, 2, cc);
        dtDiff.Rows.Add(arr1);
        dtDiff.Rows.Add(arr2);
        diffs.Add(rp, pair.diffCols); // номера столбцов расхождений для пары
        if (pair.isRepeatRow1) repeatRows.Add(rp * 10, pair.rn1);
        if (pair.isRepeatRow2) repeatRows.Add(rp * 10 + 1, pair.rn2);
        rp++;
      }
      // заполнение таблицы соответствий
      DataTable dtIdent = null;
      if (!diffOnly)
      {
        rp = 0;
        dtIdent = dtDiff.Clone(); // столбцы - как в табл.расхождений
        foreach (RowPair pair in rowPairs.Where(x => x.eq)) // строки - по парам, через массив, с учетом служебных столбцов, номера строк внутри пары - 2 и 3
        {
          object[] arr1 = new object[cc + 2], arr2 = new object[cc + 2];
          arr1[0] = rp; arr1[1] = 2; Array.Copy(pair.row1.ItemArray, 0, arr1, 2, cc);
          arr2[0] = rp; arr2[1] = 3; Array.Copy(pair.row2.ItemArray, 0, arr2, 2, cc);
          dtIdent.Rows.Add(arr1);
          dtIdent.Rows.Add(arr2);
          if (pair.isRepeatRow1) repeatRows.Add(rp * 10 + 2, pair.rn1);
          if (pair.isRepeatRow2) repeatRows.Add(rp * 10 + 3, pair.rn2);
          rp++;
        }
      }
      // таблицы для строк только в одном из источников - это таблицы, которые сравнивались, без строк, включенных в пары
      for (int i = 0; i < 2; i++)
      {
        if (i == 0 ? onlyA : onlyB) // включаем в результат
        {
          int itmp = i;
          foreach (DataRow r in rowPairs.Select(x => itmp == 0 ? x.row1 : x.row2).Distinct())
            dt[i].Rows.Remove(r);
          dt[i].AcceptChanges();
        }
        else
        {
          //dt[i].Dispose();
          dt[i] = null;
          GC.Collect();
        }
      }
      // объект результата
      CompareResult res = new CompareResult(nameA, nameB, rowsA, rowsB, colNames, dtDiff, dtIdent, dt[0], dt[1],
        diffs, keyCols, matchCols, repeatRows);
      return res;
    }
    //-------------------------------------------------------------------------
    /* подгонка таблицы под перечень имен столбцов и их порядок */
    static void DtPrepare(DataTable dt, List<string> cols) 
    {
      for (int i = dt.Columns.Count-1; i >= 0; i--)
      {
        if (!cols.Contains(dt.Columns[i].ColumnName))
          dt.Columns.Remove(dt.Columns[i]);
      }
      for (int i = 0; i < cols.Count; i++)
      {
        if (!string.IsNullOrEmpty(cols[i]) && dt.Columns.Contains(cols[i]))
        {
          if (i < dt.Columns.Count)
            dt.Columns[cols[i]].SetOrdinal(i);
          else
            dt.Columns[cols[i]].SetOrdinal(dt.Columns.Count - 1);
        }
        if (string.IsNullOrEmpty(cols[i]))
        {
          DataColumn newCol = new DataColumn("", typeof(string));
          newCol.Caption = emptyColumnMark;
          newCol.DefaultValue = string.Empty;
          dt.Columns.Add(newCol);
          if (i < dt.Columns.Count) newCol.SetOrdinal(i);
        }
      }
    }
    //-------------------------------------------------------------------------
    /* подбор пар строк - старый метод, без хэша */
    static List<RowPair> GetPairsOld(DataTable dt1, DataTable dt2, List<int> keyCols, bool checkKeys, bool checkRepeats, bool caseSens, Action<object> onProgress)
    {
      List<RowPair> rowPairs = new List<RowPair>();
      int kc = keyCols.Count;
      // значения ключевых столбцов набора строк
      Func<DataRowCollection, Dictionary<int, object[]>> a = (drc) =>
      {
        Dictionary<int, object[]> ret = new Dictionary<int, object[]>();
        for (int r = 0; r < drc.Count; r++)
        {
          object[] o = new object[kc];
          for (int k = 0; k < kc; k++)
            o[k] = drc[r][keyCols[k]]; // значение ключевого поля
          ret.Add(r, o);
        }
        return ret;
      };
      Dictionary<int, object[]> o1 = a(dt1.Rows), o2 = a(dt2.Rows);

      int rows = 0;
      for (int i1 = 0; i1 < o1.Count; i1++) // для строки первого набора...
      {
        if (onProgress != null && i1 % 100 == 0)
          onProgress(new Tuple<int, string>(100 * i1 / o1.Count, string.Format("{0} rows processed, {1} pairs found", rows, rowPairs.Count)));
        if (checkKeys) // пара определяется по совпадению ключевых столбцов
        {
          for (int i2 = 0; i2 < o2.Count; i2++) // ... ищем подходящие строки второго набора
          {
            rows++;
            int key2 = !checkRepeats ? o2.ElementAt(i2).Key : i2;
            bool ok = true;
            for (int i = 0; i < kc; i++)
              if (!ObjEq(o1[i1][i], o2[key2][i], false, false, caseSens, DBNull.Value))
              {
                ok = false;
                break;
              }

            if (ok)
            {
              rowPairs.Add(new RowPair { row1 = dt1.Rows[i1], row2 = dt2.Rows[key2], rn1 = i1, rn2 = key2 });
              if (!checkRepeats)
              {
                o2.Remove(key2);
                break;
              }
            }
          }
        }
        else // пара из строк с одинаковым порядковым номером
        {
          if (o2.Count > i1)
            rowPairs.Add(new RowPair { row1 = dt1.Rows[i1], row2 = dt2.Rows[i1] });
        }
        rows++;
      }

      if (checkKeys && checkRepeats) // поиск повторившихся строк
      {
        if (onProgress != null)
          onProgress(new Tuple<int, string>(100, string.Format("{0} rows processed, {1} pairs found. Check repeating rows...", rows, rowPairs.Count)));
        foreach (var pair in rowPairs.Where(x => rowPairs.Exists(c => c != x && c.rn1 == x.rn1)))
          pair.isRepeatRow1 = true;
        foreach (var pair in rowPairs.Where(x => rowPairs.Exists(c => c != x && c.rn2 == x.rn2)))
          pair.isRepeatRow2 = true;
      }

      return rowPairs;
    }
    //-------------------------------------------------------------------------
    /* подбор пар строк - по хэшу */
    static List<RowPair> GetPairs(DataTable dt1, DataTable dt2, List<int> keyCols, bool checkKeys, bool checkRepeats, bool caseSens, Action<object> onProgress)
    {
      List<RowPair> rowPairs = new List<RowPair>();
      int kc = keyCols.Count;
      
      // хэш для массива объектов
      Func<object[], int> keyHash = (p) =>
      {
        int ret = 0;
        for (int i = 0; i < p.Length; i++)
        {
          if (caseSens && p[i] is string)
            p[i] = ((string)p[i]).ToUpper();
          object val = DataValueToObj(p[i], false, false, DBNull.Value);
          ret ^= (val == null || val == DBNull.Value) ? 0 : val.GetHashCode();
        }
        return ret;
      };

      // заполнение словарей "номерстроки-хэш" и "номерстроки-ключ"
      Action<DataRowCollection, Dictionary<int, int>, Dictionary<int, object[]>> fillDict = (drc, dHash, dObj) =>
      {
        for (int r = 0; r < drc.Count; r++)
        {
          object[] o = new object[kc];
          for (int k = 0; k < kc; k++)
            o[k] = drc[r][keyCols[k]]; // значение ключевого поля
          dHash.Add(r, keyHash(o));
          dObj.Add(r, o);
        }
        if (checkKeys)
          dHash = dHash.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
      };

      bool d12 = dt1.Rows.Count < dt2.Rows.Count; // первый набор будет верхний 
      Dictionary<int, int> h1 = new Dictionary<int, int>(), h2 = new Dictionary<int, int>();
      Dictionary<int, object[]> o1 = new Dictionary<int, object[]>(), o2 = new Dictionary<int, object[]>();
      fillDict(d12 ? dt1.Rows : dt2.Rows, h1, o1); // хэши и ключи верхнего набора
      fillDict(d12 ? dt2.Rows : dt1.Rows, h2, o2); // хэши и ключи нижнего набора

      // сверка ключей
      Func<int, int, bool> check = (k1, k2) =>
      {
        bool ok = true;
        for (int i = 0; i < kc; i++)
          if (!ObjEq(o1[k1][i], o2[k2][i], false, false, caseSens, DBNull.Value))
          {
            ok = false;
            break;
          }
        return ok;
      };

      for (int i1 = 0; i1 < h1.Count; i1++) // для строки верхнего набора...
      {
        if (onProgress != null && i1 % 100 == 0)
          onProgress(new Tuple<int, string>(100 * i1 / h1.Count, string.Format("{0} rows processed, {1} pairs found", i1, rowPairs.Count)));
        if (checkKeys) // пара определяется по совпадению ключевых столбцов
        {
          KeyValuePair<int, int> kp1 = h1.ElementAt(i1);
          if (!h2.ContainsValue(kp1.Value)) // нет такого же хэша в нижнем наборе
            continue;
          int key1 = kp1.Key;
          if (!checkRepeats) // не ищем повторы - достаточно первого совпадения
          {
            int key2 = h2.First(x => x.Value == kp1.Value).Key;
            if (!check(key1, key2)) // сверяем ключи, т.к. совпадение хешей еще не значит равенства объектов
              continue;
            rowPairs.Add(new RowPair { row1 = dt1.Rows[d12 ? key1 : key2], row2 = dt2.Rows[d12 ? key2 : key1], rn1 = d12 ? key1 : key2, rn2 = d12 ? key2 : key1 });
            h2.Remove(key2);
          }
          else
          {
            int[] keys = h2.Where(x => x.Value == kp1.Value).Select(k => k.Key).ToArray();
            for (int i2 = 0; i2 < keys.Length; i2++)
            {
              int key2 = keys[i2];
              if (check(key1, key2))
                rowPairs.Add(new RowPair { row1 = dt1.Rows[d12 ? key1 : key2], row2 = dt2.Rows[d12 ? key2 : key1], rn1 = d12 ? key1 : key2, rn2 = d12 ? key2 : key1 });
            }
          }
        }
        else // пара из строк с одинаковым порядковым номером
        {
          if (h2.Count > i1)
            rowPairs.Add(new RowPair { row1 = dt1.Rows[i1], row2 = dt2.Rows[i1] });
        }
      }

      if (checkKeys && checkRepeats) // поиск повторившихся строк
      {
        if (onProgress != null)
          onProgress(new Tuple<int, string>(100, string.Format("{0} pairs found. Check repeating rows...", rowPairs.Count)));
        foreach (var pair in rowPairs.Where(x => rowPairs.Exists(c => c != x && c.rn1 == x.rn1)))
          pair.isRepeatRow1 = true;
        foreach (var pair in rowPairs.Where(x => rowPairs.Exists(c => c != x && c.rn2 == x.rn2)))
          pair.isRepeatRow2 = true;
      }

      return rowPairs;
    }
    //-------------------------------------------------------------------------
    /* сравнение полей в паре */
    static void MatchPair(RowPair pair, List<int> matchCols, bool tryConvert, bool nullAsStr, bool caseSens)
    {
      // сверка значений столбцов "для сравнения", заполнение diffCols в случае не совпадения
      foreach (var c in matchCols)
        if (!ObjEq(pair.row1[c], pair.row2[c], true, tryConvert, caseSens, !nullAsStr ? DBNull.Value : (object)string.Empty))
          pair.diffCols.Add(c);
      pair.eq = (pair.diffCols.Count == 0);
    }
    //-------------------------------------------------------------------------
    /* сверка значений объектов */
    static bool ObjEq(object in1, object in2, bool checkType, bool tryParse, bool caseSensitive,  object nullValue)
    {
      bool res;

      object obj1 = DataValueToObj(in1, checkType, tryParse, nullValue)
           , obj2 = DataValueToObj(in2, checkType, tryParse, nullValue);

      if (obj1 == null || obj1 == DBNull.Value || obj2 == null || obj2 == DBNull.Value)
        res = ((obj1 == null || obj1 == DBNull.Value) & (obj2 == null || obj2 == DBNull.Value));
      else
      {
        if (obj1 is string || obj2 is string)
        {
          string s1 = obj1.ToString().TrimEnd(), s2 = obj2.ToString().TrimEnd();
          res = string.Equals(s1, s2, !caseSensitive ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
        }
        else
          res = Equals(obj1, obj2);
      }
      return res;
    }
    //-------------------------------------------------------------------------
    /* подготовка значения объекта в зависимости от типа и параметров сверки */
    static object DataValueToObj(object valueIn, bool checkType = false, bool tryParse = false,
      object nullValue = null, string dateFormat = "yyyyMMdd", string dateTimeFormat = "yyyyMMdd hh:mm:ss.fff")
    {
      object value = string.Empty;
      try
      {
        if (valueIn == null || valueIn == DBNull.Value)
          value = nullValue;
        else if (!checkType)
          value = valueIn.ToString();
        else
        {
          Type type = valueIn.GetType();
          DateTime vDT = new DateTime(); decimal vDec; 
          string[] formatsDT = { dateFormat, dateTimeFormat };

          if ((!tryParse && (type == typeof(float) || type == typeof(decimal) || type == typeof(double)))
            || (tryParse && decimal.TryParse(valueIn.ToString(), out vDec)))
            value = Convert.ToDecimal(valueIn);
          else if (type == typeof(Guid))
            value = valueIn.ToString();
          else if (type == typeof(Boolean))
            value = valueIn.ToString();
          else if ((!tryParse && valueIn is DateTime)
                    || (tryParse && (valueIn is DateTime 
                        || DateTime.TryParse(valueIn.ToString(), out vDT)
                        || DateTime.TryParseExact(valueIn.ToString(), formatsDT, DateTimeFormatInfo.InvariantInfo, 
                                                  DateTimeStyles.None, out vDT)))) 
          {
            if (valueIn is DateTime) vDT = (DateTime)valueIn;
            value = vDT.ToString(vDT.Date == vDT ? dateFormat : dateTimeFormat);
          }
          else if (type.IsValueType && !type.IsEnum)
            value = valueIn;
          else 
            value = valueIn.ToString();
        }
      }
      catch (Exception ex)
      {
        value = String.Format("Error convert data value <{0}> to object:\n{1}", valueIn, ex.Message);
      }
      return value;
    }
  }
}

