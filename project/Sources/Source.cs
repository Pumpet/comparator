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
using System.Xml.Serialization;
using DataTable = System.Data.DataTable;

namespace Sources
{
  // types of data sources
  public enum SourceType { Database, Excel, CSV, XML } 
  //===========================================================================
  /* Data Source for comparison */
  public class Source 
  {
    string name;
    public string Name { get { return string.IsNullOrEmpty(name) ? InnerName : name; } set { name = value; } }
    [XmlIgnore]
    public string InnerName { get; set; } // default source name, for messages

    SourceType srcType; // data source type
    public SourceType SrcType
    {
      get { return srcType; }
      set
      {
        srcType = value;
        content = GetContent(value); // set current source content (from already created or new) in depend of its type 
        content.Parent = this;
      }
    }

    SourceContent content; // data source content - object that getting data from specific type source
    public SourceContent Content
    {
      get { return content; }
      set
      {
        content = value;
        currContent[srcType] = value; 
        currContent[srcType].Parent = this;
      }
    }

    DataTable dt;
    [XmlIgnore]
    public DataTable DT { get { return dt; } set { dt = value; } } // source data
    [XmlIgnore]
    public bool InProc { get; set; } // true if active get data process

    Dictionary<SourceType, SourceContent> currContent = new Dictionary<SourceType, SourceContent>(); // already created contents

    // get already created typed content:
    [XmlIgnore]
    public DbContent DbSource { get { return (DbContent)GetContent(SourceType.Database); } } 
    [XmlIgnore]
    public ExcelContent ExcelSource { get { return (ExcelContent)GetContent(SourceType.Excel); } } 
    [XmlIgnore]
    public CsvContent CsvSource { get { return (CsvContent)GetContent(SourceType.CSV); } }
    [XmlIgnore]
    public XmlContent XmlSource { get { return (XmlContent)GetContent(SourceType.XML); } }

    //-------------------------------------------------------------------------
    public Source()
    {
      SrcType = SourceType.Database;
    }
    //-------------------------------------------------------------------------
    // Get already created or create new content according of type
    private SourceContent GetContent(SourceType type) 
    {
      if (!currContent.ContainsKey(type))
        switch (type)
        {
          case SourceType.Database:
            currContent.Add(type, new DbContent());
            break;
          case SourceType.Excel:
            currContent.Add(type, new ExcelContent());
            break;
          case SourceType.CSV:
            currContent.Add(type, new CsvContent());
            break;
          case SourceType.XML:
            currContent.Add(type, new XmlContent());
            break;
        }
      if (currContent[type] != null) currContent[type].Parent = this; // link content object to this object
      return currContent[type];
    }
    //-------------------------------------------------------------------------
    // Check current content
    public void Check() 
    {
      Content.Check();
    }
    //-------------------------------------------------------------------------
    public void DTClear() 
    {
      if (DT != null)
      {
        //DT.Dispose();
        DT = null;
        GC.Collect();
      }
    }
  }
}
