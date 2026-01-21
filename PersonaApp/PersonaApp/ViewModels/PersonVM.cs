using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using PersonaApp.Helppers;
using PersonaApp.Models;
using PersonaApp.Service;

namespace PersonaApp.ViewModels;

public class PersonVM : INotifyPropertyChanged
{
    private readonly ServicePerson _servicePerson = new ServicePerson();
    public ICommand AddPersonCommand { get; }
    public ICommand UpdatePersonCommand { get; }
    public ICommand DeletePersonCommand { get; }
    public ICommand CleanPersonCommand { get; }
    public PersonVM()
    {
        AddPersonCommand = new RelayCommand(Add);
        UpdatePersonCommand = new RelayCommand(Update);
        DeletePersonCommand = new RelayCommand(Remove);
        CleanPersonCommand = new RelayCommand(Clean);

        loadDates();
        Clean();
    }

    /*
     * se inizializa con  = new ( pro que no tiene datos
     */
    public ObservableCollection<Person> Persons { get; } = new();

    private Person _selectedPerson;
    private string _name;
    private DateTimeOffset _fechaNaci;
    private string _genre;
    private string _password;
    private bool _acceptTerms;
    
    public Person SelectedPerson
    {
        get => _selectedPerson;
        set
        {
            if (value != _selectedPerson)
            {
                _selectedPerson = value;
                OnProperityChanged();
                if (value != null)
                {
                    Name = value.Name;
                    FechaNaci = value.FechaNaci;
                    Genre = value.Genre;
                    Password = value.Password;
                    AcceptTerms = value.AccepTerms;
                }
                else
                {
                    Clean();
                }
            }
        }
    }
    
    //obtiene el valor _name 
    // Name => asigna
    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            OnProperityChanged();
        }
    }
    
    public DateTimeOffset? FechaNaci
    {
        get => _fechaNaci;
        set
        {
            _fechaNaci = (DateTimeOffset)value;
            OnProperityChanged();
        }
    }

    public string Genre
    {
        get => _genre;
        set
        {
            _genre = value;
            OnProperityChanged();
        }
    }
    
    public string Password
    {
        get => _password;
        set
        {
            _password = value;
            OnProperityChanged();
        }
    }
    public bool AcceptTerms
    {
        get => _acceptTerms;
        set
        {
            _acceptTerms = value;
            OnProperityChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    //es un metodo que necesito el Views Modelo
    protected void OnProperityChanged([CallerMemberName] string propertyName = null) 
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /*
     * Estamos usando el servicio y asignamos la collections que tenemos
     */
    public void loadDates()
    {
        Persons.Clear();
        foreach( var p in _servicePerson.ObtenerPersona())
        {
            Persons.Add(p);
        }
    }

    public void Clean()
    {
        if (Persons.Count != 0)
        {
            Name = string.Empty;
            FechaNaci = null;
            Genre = string.Empty;
            Password = string.Empty;
            AcceptTerms = false;
            SelectedPerson = null;
        }
    }

    public bool Validate()=>
        !string.IsNullOrWhiteSpace(Name) &&
            FechaNaci.HasValue && !string.IsNullOrWhiteSpace(Genre);

    public void Add()
    {
        if(!Validate()) return;
        var newPerson = new Person
        {
            Name = Name,
            FechaNaci = FechaNaci,
            Genre = Genre,
            Password = Password,
            AccepTerms = AcceptTerms,
        };
        _servicePerson.AddPerson(newPerson);
        loadDates();
        Clean();
    }

    public void Update()
    {
        if(SelectedPerson == null || !Validate()) return;
        // el name con trim  por que  es insertar el trim si no por que se hace una  base de datos
        // la persona asingada se mestra lso campos se actualiza y se cargar datos
        //limpaindo la perosna selecioanda
        SelectedPerson.Name = Name.Trim();
        SelectedPerson.FechaNaci = FechaNaci.Value;
        SelectedPerson.Genre = Genre.Trim();
        SelectedPerson.Password = Password.Trim();
        _servicePerson.UpdatePerson(SelectedPerson);
        loadDates();
        Clean();
    }
    public void Remove()
    {
        if(SelectedPerson == null ) return;
        _servicePerson.DeletePerson(SelectedPerson);
        loadDates();
        Clean();
    }
}