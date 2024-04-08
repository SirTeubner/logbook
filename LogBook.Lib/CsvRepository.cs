using CsvHelper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogBook.Lib;

public class CsvRepository : IRepository
{
    // title, money
    // test, 23.22
    // Probe, 14.11

    private string _path = string.Empty;
    readonly List<Entry> list = new();

    public CsvRepository(string path)
    {
        this._path = path;

        if (File.Exists(path))
        {

            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<EntryMap>();
                csv.Read();
                csv.ReadHeader();

                var records = csv.GetRecords<Entry>();
                list = records.ToList();
            }
        }
    }

    public bool Add(Entry entry)
    {
        list.Add(entry);
        return this.Save();
    }

    public bool Delete(Entry entry)
    {
        var item = list.FirstOrDefault((item) => item.Id == entry.Id);

        if (item != null)
        {
            list.Remove(item);
        }

        return this.Save();
    }

    public Entry? Find(string id)
    {
        return list.FirstOrDefault((item) => item.Id == id);
    }

    public List<Entry> GetAll()
    {
        return list;
    }

    public bool Save()
    {
        try
        {
            using (var writer = new StreamWriter(_path))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<EntryMap>();

                csv.WriteHeader<Entry>();

                csv.NextRecord();

                foreach(var record in list)
                {
                    csv.WriteRecord(record);
                    csv.NextRecord();
                }

            }



            return true;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public bool Update(Entry entry)
    {
        var item = (from search in list
                    where search.Id == entry.Id
                    select search).FirstOrDefault();
        if (item != null)
        {
            item = entry;
        }

        return this.Save();
    }
}
