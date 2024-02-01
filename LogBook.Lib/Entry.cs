﻿namespace LogBook.Lib;

public class Entry
{

    /// <summary>
    /// Creates an entry for the log book
    /// </summary>
    public string Description { get; set; } = string.Empty;

    public DateTime Start { get; set; }

    public DateTime End { get; set; }

    public int StartKM { get; set; }

    public int EndKM { get; set; }

    public string NumberPlate { get; set; }

    public string From { get; set; }

    public string To { get; set; }

    /// <summary>
    /// Id for an entry
    /// </summary>
    public string Id { get; set; }
    public Entry(DateTime start, DateTime end, int startKM, int endKM, string numberPlate, string from, string to, string id)
    {
        this.Id = id;
        this.Start = start;
        this.End = end;
        this.StartKM = startKM;
        this.EndKM = endKM;
        this.NumberPlate = numberPlate;
        this.From = from;
        this.To = to;

    }

    public Entry(DateTime start, DateTime end, int startKM, int endKM, string numberPlate, string from, string to)
    {
        this.Id = Guid.NewGuid().ToString();
        this.Start = start;
        this.End = end;
        this.StartKM = startKM;
        this.EndKM = endKM;
        this.NumberPlate = numberPlate;
        this.From = from;
        this.To = to;

    }

    public override string ToString()
    {
        return String.Format("{0} nach {1}", From, To);
    }
}
