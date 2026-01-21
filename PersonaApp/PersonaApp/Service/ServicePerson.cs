using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PersonaApp.Data;
using PersonaApp.Models;

namespace PersonaApp.Service;

public class ServicePerson
{
    private readonly AppDBContext _db = new AppDBContext();

    public List<Person> ObtenerPersona()
    {
        return _db.Persons.AsNoTracking().ToList();
    }
    //Retorna uan lsita de personas traqueadas para poder hacer un CRUD
    
    public void AddPerson(Person person)
    {
        _db.Persons.Add(person); // agrega las personas
        _db.SaveChanges();//guarda cambios
    }

    public void  UpdatePerson(Person person)
    {
        /* se creara un contexto nuevo por que la entidad
         lo cual no viene actuaalizada de este EL DB por eso 
         se hacer manualmente para queel entity genere un update
         osea "person" no viene traqueada
         */
        using var context = new AppDBContext();
        _db.Persons.Attach(person);
        context.Entry(person).State = EntityState.Modified;
        context.SaveChanges();
    }

    public void DeletePerson(Person person)
    {
        /*
         * Usa estes context para buscar
         * ala primera persona con el mismo Id
         * que tiene el person pero en la DB
         */
        var context = _db.Persons.FirstOrDefault(p => p.Id == person.Id)
            ?? _db.Persons.Find(person.Id);
        if (context != null)
        {
            _db.Persons.Remove(person);
            _db.SaveChanges();
        }
        //context.Remove(person).State = EntityState.Deleted;
    }
    
}