using LogBook.Lib;

Console.WriteLine("Willkommen beim Fahrtenbuch!");

IRepository repository = new MemoryRepository();

repository.Add(new Entry(DateTime.Now, DateTime.Now.AddHours(2).AddMinutes(22), 25000, 25170, "ZE-XY123", "Zell am See", "München"));


/*--------Das Selbe wie oben nur mit einem Schritt extra.---------*/
Entry entrySaalfelden = new Entry(DateTime.Now.AddDays(3), DateTime.Now.AddDays(3).AddMinutes(20), 25500, 25514, "ZE-XY123", "Zell am See", "Saalfelden");

repository.Add(entrySaalfelden);
/*----------------------------Ende-------------------------------*/


List<Entry> entries = repository.GetAll();


foreach(Entry entry in entries)
{
    Console.WriteLine(entry.From);
}

