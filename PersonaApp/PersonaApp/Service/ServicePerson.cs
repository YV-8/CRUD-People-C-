using System;
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
        try 
        {
            _db.Persons.Add(person); // agrega las personas
            _db.SaveChanges();//guarda cambios
        }
        catch (Exception ex) 
        {
            // Esto evita que la app se cierre. 
            // El error aparecerá en la consola de Rider, pero la app seguirá viva.
            Console.WriteLine("ERROR AL GUARDAR: " + ex.Message);
            if (ex.InnerException != null) 
                Console.WriteLine("DETALLE: " + ex.InnerException.Message);
        }
    }

    public void  UpdatePerson(Person person)
    {
        /* se creara un contexto nuevo por que la entidad
         lo cual no viene actuaalizada de este EL DB por eso 
         se hacer manualmente para queel entity genere un update
         osea "person" no viene traqueada
         */
        //using var context = new AppDBContext();
        var tracked = _db.Persons.Local.FirstOrDefault(p => p.Id == person.Id);
        if (tracked != null)
        {
            _db.Entry(tracked).State = EntityState.Detached;
        }
        _db.Persons.Attach(person);
        _db.Entry(person).State = EntityState.Modified;
        _db.SaveChanges();
    }

    public void DeletePerson(Person person)
    {
        /*
         * Usa estos context para buscar
         * ala primera persona con el mismo Id
         * que tiene el person pero en la DB
         */
        var context = _db.Persons.FirstOrDefault(p => p.Id == person.Id)
            ?? _db.Persons.Find(person.Id);
        if (context != null)
        {
            _db.Persons.Remove(context);
            _db.SaveChanges();
        }
        //context.Remove(person).State = EntityState.Deleted;
    }
    
}