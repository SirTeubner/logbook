using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogBook.Lib;

public class MemoryRepository : IRepository
{
    readonly List<Entry> list = new();

    public bool Add(Entry entry)
    {
        list.Add(entry);
        return true;
    }

    public bool Delete(Entry entry)
    {
        return list.Remove(entry);
    }

    public Entry? Find(string id)
    {
        var entries = from e in list
                      where e.Id == id 
                      select e;

        return entries.FirstOrDefault();
    }

    public List<Entry> GetAll()
    {
        return list;
    }

    public bool Save()
    {
        return true;
    }

    public bool Update(Entry entry)
    {
        int pos = list.IndexOf(entry);

        if (pos == -1) return false;

        var item = (from search in list
                   where search.Id == entry.Id
                   select search).FirstOrDefault();

        if (item != null)
        {
            item = entry;
            return true;
        }

        return false;

    }
}
