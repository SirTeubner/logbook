﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogBook.Lib;

public class MemoryRepository : IRepository
{
    public bool Add(Entry entry)
    {
        throw new NotImplementedException();
    }

    public bool Delete(Entry entry)
    {
        throw new NotImplementedException();
    }

    public List<Entry> GetAll()
    {
        List<Entry> list = new List<Entry>()
        {
            new Entry(DateTime.Now, DateTime.Now, 100000, 100100, "Ze-1234", "Zell am See", "Salzburg"),
            new Entry(DateTime.Now, DateTime.Now, 100100, 100200, "Ze-1234", "Salzburg", "Zell am See")
        };

        return list;
    }

    public bool Save()
    {
        throw new NotImplementedException();
    }

    public bool Update(Entry entry)
    {
        throw new NotImplementedException();
    }
}
