using System;

namespace PersonaApp.Models;

public class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTimeOffset? FechaNaci { get; set; }
    public string Genre { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool AccepTerms { get; set; }
}