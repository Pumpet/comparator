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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Globalization;
using System.Drawing;
using System.Windows.Forms;

namespace DataComparer
{
  public enum ResultType { rtDiff, rtIdent, rtA, rtB };
  public enum MatchType { NoMatch, All, Selected }
  //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
  /* Comparison of two tables */
  public static class DSComparer
  {
    public static Rectangle FormRect;
    public static FormWindowState WinState = FormWindowState.Normal;
    public const string emptyColumnMark = "__EMPTY__";
    //-------------------------------------------------------------------------
    /* Pair of matched rows */
    class RowPair  
    {
      public DataRow row1, row2;
      public int rn1 = -1, rn2 = -1; // row number
      public List<int> diffCols = new List<int>(); // indexes of columns that have different values
      public bool isRepeatRow1, isRepeatRow2; // true if row is repeated
      public bool eq = true; // true if no differencies
    }
    //-------------------------------------------------------------------------------------------------
    /// <summary>
    /// Compare two DataTables
    /// </summary>
    /// <param name="data">array of two DataTables</param>
    /// <param name="keysMatchType">keys match type</param>
    /// <param name="colsMatchType">columns match type</param>
    /// <param name="checkRepeats">check repeating rows</param>
    /// <param name="tryConvert">try to convert string to date-time or number</param>
    /// <param name="nullAsStr">null as empty string</param>
    /// <param name="caseSens">case sensitive</param>
    /// <param name="diffOnly">not include identically rows in result</param>
    /// <param name="onlyA">not include rows unique to table 2</param>
    /// <param name="onlyB">not include rows unique to table 1</param>
    /// <param name="colNames">all column names that will be included in result (if not set - use all columns)</param>
    /// <param name="keyColNames">key columns names (in the same order for both tables)</param>
    /// <param name="matchColNames">comparison columns names (in the same order for both tables)</param>
    /// <param name="onProgress">action for progress monitoring</param>
    /// <returns>CompareResult object contains DataTables for different and identically row pairs and unique rows in each source, and related data</returns>
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
      List<int> keyCols = new List<int>(); // key columns indexes
      List<int> matchCols = new List<int>(); // comparison columns indexes

      // prepare temp tables
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

      // add empty columns to be the same number of columns
      for (int i = 0; i < Math.Abs(dts[0].Columns.Count - dts[1].Columns.Count); i++)
      {
        DataColumn newCol = new DataColumn("", typeof(string));
        newCol.Caption = emptyColumnMark;
        newCol.DefaultValue = string.Empty;
        ((dts[0].Columns.Count - dts[1].Columns.Count) > 0 ? dts[0] : dts[1]).Columns.Add(newCol);
      }
      
      // get key columns indexes
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
      
      // get comparison columns indexes
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

      // matching pairs of rows
      List<RowPair> rowPairs = GetPairs(dts[0], dts[1], keyCols, (keysMatchType != MatchType.NoMatch), checkRepeats, caseSens, onProgress);
      
      // comparison fields in pairs
      for (int i = 0; i < rowPairs.Count; i++)
      {
        if (onProgress != null && i % 100 == 0)
          onProgress(new Tuple<int, string>(100 * i / rowPairs.Count, string.Format("{0} pairs matched", i)));
        MatchPair(rowPairs[i], matchCols, tryConvert, nullAsStr, caseSens);
      }

      return ResultPrepare(dts, keyCols, matchCols, rowPairs, diffOnly, onlyA, onlyB);
    }
    //-------------------------------------------------------------------------
    /* Create result object */
    static CompareResult ResultPrepare(DataTable[] dt, List<int> keyCols, List<int> matchCols, List<RowPair> rowPairs,
      bool diffOnly, bool onlyA, bool onlyB)
    {
      int cc = dt[0].Columns.Count;
      int rowsA = dt[0].Rows.Count, rowsB = dt[1].Rows.Count;
      Dictionary<int, List<int>> diffs = new Dictionary<int, List<int>>(); 
      Dictionary<int, int> repeatRows = new Dictionary<int, int>(); 
      List<string>[] colNames = new List<string>[]{new List<string>(), new List<string>()}; 
      string nameA = dt[0].TableName, nameB = dt[1].TableName;
      
      // table for differences
      DataTable dtDiff = new DataTable();
      dtDiff.Columns.Add("", typeof(int)).Caption = "PairNo"; // service column #0 for pair number
      dtDiff.Columns.Add("", typeof(int)).Caption = "Source"; // service column #0 for number inside pair
      dtDiff.PrimaryKey = new[] { dtDiff.Columns[0], dtDiff.Columns[1] };
      for (int i = 0; i < cc; i++) 
      {
        DataColumn dc = dtDiff.Columns.Add(dt[0].Columns[i].ColumnName == dt[1].Columns[i].ColumnName ? dt[0].Columns[i].ColumnName : dt[0].Columns[i].ColumnName + "_" + dt[1].Columns[i].ColumnName);
        dc.DataType = dt[0].Columns[i].DataType == dt[1].Columns[i].DataType ? dt[0].Columns[i].DataType : typeof(object);
        colNames[0].Add(dt[0].Columns[i].Caption != emptyColumnMark ? dt[0].Columns[i].Caption : "");
        colNames[1].Add(dt[1].Columns[i].Caption != emptyColumnMark ? dt[1].Columns[i].Caption : "");
        dc.Caption = colNames[0][i] + "\n" + colNames[1][i];
      }
      int rp = 0;
      foreach (RowPair pair in rowPairs.Where(x => !x.eq)) 
      {
        object[] arr1 = new object[cc + 2], arr2 = new object[cc + 2];
        arr1[0] = rp; arr1[1] = 0; Array.Copy(pair.row1.ItemArray, 0, arr1, 2, cc);
        arr2[0] = rp; arr2[1] = 1; Array.Copy(pair.row2.ItemArray, 0, arr2, 2, cc);
        dtDiff.Rows.Add(arr1);
        dtDiff.Rows.Add(arr2);
        diffs.Add(rp, pair.diffCols);
        if (pair.isRepeatRow1) repeatRows.Add(rp * 10, pair.rn1);
        if (pair.isRepeatRow2) repeatRows.Add(rp * 10 + 1, pair.rn2);
        rp++;
      }
      
      // table for identicals
      DataTable dtIdent = null;
      if (!diffOnly)
      {
        rp = 0;
        dtIdent = dtDiff.Clone(); 
        foreach (RowPair pair in rowPairs.Where(x => x.eq)) 
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

      // tables for rows that don't find pairs in other source
      for (int i = 0; i < 2; i++)
      {
        if (i == 0 ? onlyA : onlyB)
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

      // create result object
      CompareResult res = new CompareResult(nameA, nameB, rowsA, rowsB, colNames, dtDiff, dtIdent, dt[0], dt[1],
        diffs, keyCols, matchCols, repeatRows);
      return res;
    }
    //-------------------------------------------------------------------------
    /* Prepare temp table in accordance with list of columns */
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
    /* Matching pairs of rows - old method, without hash, not used, works slowly on large volumes of very different sources */
    static List<RowPair> GetPairsOld(DataTable dt1, DataTable dt2, List<int> keyCols, bool checkKeys, bool checkRepeats, bool caseSens, Action<object> onProgress)
    {
      List<RowPair> rowPairs = new List<RowPair>();
      int kc = keyCols.Count;

      // key objects for each row
      Func<DataRowCollection, Dictionary<int, object[]>> a = (drc) =>
      {
        Dictionary<int, object[]> ret = new Dictionary<int, object[]>();
        for (int r = 0; r < drc.Count; r++)
        {
          object[] o = new object[kc];
          for (int k = 0; k < kc; k++)
            o[k] = drc[r][keyCols[k]]; // object from key field
          ret.Add(r, o);
        }
        return ret;
      };
      Dictionary<int, object[]> o1 = a(dt1.Rows), o2 = a(dt2.Rows);

      int rows = 0;
      for (int i1 = 0; i1 < o1.Count; i1++) // rows from dt1
      {
        if (onProgress != null && i1 % 100 == 0)
          onProgress(new Tuple<int, string>(100 * i1 / o1.Count, string.Format("{0} rows processed, {1} pairs found", rows, rowPairs.Count)));
        // get pair with same keys
        if (checkKeys) 
        {
          for (int i2 = 0; i2 < o2.Count; i2++) // rows from dt2
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
        else // get pair in order
        {
          if (o2.Count > i1)
            rowPairs.Add(new RowPair { row1 = dt1.Rows[i1], row2 = dt2.Rows[i1] });
        }
        rows++;
      }

      if (checkKeys && checkRepeats) // mark repeating rows
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
    /* Matching pairs of rows */
    static List<RowPair> GetPairs(DataTable dt1, DataTable dt2, List<int> keyCols, bool checkKeys, bool checkRepeats, bool caseSens, Action<object> onProgress)
    {
      List<RowPair> rowPairs = new List<RowPair>();
      int kc = keyCols.Count;
      
      // hash for all key objects in row
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

      // fill dicrionaries "rownumber:hash" and "rownumber:keys"
      Action<DataRowCollection, Dictionary<int, int>, Dictionary<int, object[]>> fillDict = (drc, dHash, dObj) =>
      {
        for (int r = 0; r < drc.Count; r++)
        {
          object[] o = new object[kc];
          for (int k = 0; k < kc; k++)
            o[k] = drc[r][keyCols[k]]; // object from key field
          dHash.Add(r, keyHash(o));
          dObj.Add(r, o);
        }
        if (checkKeys)
          dHash = dHash.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
      };

      bool d12 = dt1.Rows.Count < dt2.Rows.Count; // first rowset to be from table1 
      Dictionary<int, int> h1 = new Dictionary<int, int>(), h2 = new Dictionary<int, int>();
      Dictionary<int, object[]> o1 = new Dictionary<int, object[]>(), o2 = new Dictionary<int, object[]>();
      fillDict(d12 ? dt1.Rows : dt2.Rows, h1, o1); // get hash and keys for rowset1
      fillDict(d12 ? dt2.Rows : dt1.Rows, h2, o2); // get hash and keys for rowset2

      // keys comparison
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

      for (int i1 = 0; i1 < h1.Count; i1++) // rowset1
      {
        if (onProgress != null && i1 % 100 == 0)
          onProgress(new Tuple<int, string>(100 * i1 / h1.Count, string.Format("{0} rows processed, {1} pairs found", i1, rowPairs.Count)));
        // get pair with same hash and keys
        if (checkKeys) 
        {
          KeyValuePair<int, int> kp1 = h1.ElementAt(i1);
          if (!h2.ContainsValue(kp1.Value)) // no hash in rowset2
            continue;
          int key1 = kp1.Key;
          if (!checkRepeats) // not check repeating rows - get first pair
          {
            int key2 = h2.First(x => x.Value == kp1.Value).Key;
            if (!check(key1, key2)) // compare each key value, since hash equality does not yet mean equality of objects
              continue;
            rowPairs.Add(new RowPair { row1 = dt1.Rows[d12 ? key1 : key2], 
              row2 = dt2.Rows[d12 ? key2 : key1], 
              rn1 = d12 ? key1 : key2, 
              rn2 = d12 ? key2 : key1 });
            h2.Remove(key2);
          }
          else // check repeating rows - get all matching pairs
          {
            int[] keys = h2.Where(x => x.Value == kp1.Value).Select(k => k.Key).ToArray();
            for (int i2 = 0; i2 < keys.Length; i2++)
            {
              int key2 = keys[i2];
              if (check(key1, key2))
                rowPairs.Add(new RowPair { row1 = dt1.Rows[d12 ? key1 : key2], 
                  row2 = dt2.Rows[d12 ? key2 : key1], 
                  rn1 = d12 ? key1 : key2, 
                  rn2 = d12 ? key2 : key1 });
            }
          }
        }
        else // get pair in order
        {
          if (h2.Count > i1)
            rowPairs.Add(new RowPair { row1 = dt1.Rows[i1], row2 = dt2.Rows[i1] });
        }
      }

      if (checkKeys && checkRepeats) // mark repeating rows
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
    /* Comparison fields in pair */
    static void MatchPair(RowPair pair, List<int> matchCols, bool tryConvert, bool nullAsStr, bool caseSens)
    {
      foreach (var c in matchCols)
        if (!ObjEq(pair.row1[c], pair.row2[c], true, tryConvert, caseSens, !nullAsStr ? DBNull.Value : (object)string.Empty))
          pair.diffCols.Add(c);
      pair.eq = (pair.diffCols.Count == 0);
    }
    //-------------------------------------------------------------------------
    /* Compare two objects */
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
    /* Prepare object value depending on its type and comparison parameters */
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

