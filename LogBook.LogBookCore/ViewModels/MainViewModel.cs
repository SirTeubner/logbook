using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using LogBook.Lib;
using LogBook.LogBookCore.Messages;
using LogBook.LogBookCore.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogBook.LogBookCore.ViewModels;

public partial class MainViewModel(IRepository repository, IAlertService alertService) : ObservableObject // Primärer Konstruktor
{
    public string Header => "Fahrtenbuch";
    IRepository _repository = repository;
    IAlertService _alertService = alertService;
    private bool _isLoaded = false;


    [ObservableProperty]
    ObservableCollection<Lib.Entry> _entries = [];

    [ObservableProperty]
    Lib.Entry? _selectedEntry = null;


    // Region verändert im Code nichts, man kann Region einklappen
    #region Properties 

    [ObservableProperty]
    DateTime _start = DateTime.Now;
    
    [ObservableProperty]
    DateTime _end = DateTime.Now;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(AddCommand))] // Durch diese Zeile wird der Button deaktiviert
    string _description = string.Empty;

    [ObservableProperty]
    string _numberplate = string.Empty;

    [ObservableProperty]
    int _startKM = 0;

    [ObservableProperty]
    int _endKM = 0;

    [ObservableProperty]
    string _from = string.Empty;

    [ObservableProperty]
    string _to = string.Empty;

    #endregion


    void ToggleFavorite(Lib.Entry entry)
    {
        entry.Favorite = !entry.Favorite; // Wenn der entry true ist wird er false und wenn er false ist wird er true

        var result = _repository.Update(entry);

        if (result)
        {
            int pos = this.Entries.IndexOf(entry);

            if (pos != -1)
            {
                this.Entries[pos] = entry;
                _alertService.ShowAlert("Erfolg", "Der Status wurde verändert.");
            }
        }
        else
        {
            _alertService.ShowAlert("Fehler", "Der Status konnte nicht verändert werden.");
        }
    }


    [RelayCommand]
    void Delete(Lib.Entry entry)
    {
        Lib.Entry entryToDelete = _repository.Find(entry.Id);

        if (entryToDelete != null)
        {
            var res = _repository.Delete(entryToDelete);

            if (res)
            {
                this.SelectedEntry = null;
                this.Entries.Remove(entry);

                _alertService.ShowAlert("Erfolg", "Der Eintrag wurde gelöscht.");
            }
            else
            {
                // alert not possible to delete from repository
                _alertService.ShowAlert("Fehler", "Der Eintrag konnte nicht gelöscht werden.");
            }
        }
        else
        {
            // alert alert entry not found
            _alertService.ShowAlert("Fehler", "Der Eintrag konnte nicht gefunden werden.");
        }
    }

    [RelayCommand]
    void LoadData()
    {
        // naja: Entier.Clear();

        if (!_isLoaded)
        {
            var entries = _repository.GetAll();

            foreach (var entry in entries)
            {
                Entries.Add(entry);
            }

            _isLoaded = true;
        }        
    }

    private bool CanAdd => this.Description.Length > 0;

    [RelayCommand (CanExecute = nameof(CanAdd))]
    void Add()
    {
        /*
        Lib.Entry entrySaalfelden = new(DateTime.Now.AddDays(3), DateTime.Now.AddDays(3).AddMinutes(20), 25500, 25514, "ZE-XY123", "Zell am See", "Saalfelden")
        {
            Description = "Fahrt nach Saalfelden"
        };
        */

        Lib.Entry entry = new(this.Start, this.End, this.StartKM, this.EndKM, this.Numberplate, this.From, this.To, false);
        
        if(this.Description.Length > 0)
        {
            entry.Description = this.Description;
        }


        var result = _repository.Add(entry);
        if (result)
        {
            this.Entries.Add(entry);
            this.Description = string.Empty;
            this.From = string.Empty;
            this.To = string.Empty;
            this.StartKM = this.EndKM;
            this.EndKM = 0;

            WeakReferenceMessenger.Default.Send(new AddMessage(entry));
        }


    }

}
