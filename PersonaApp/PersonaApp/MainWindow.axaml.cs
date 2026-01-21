using Avalonia;
using Avalonia.Controls;
using Microsoft.EntityFrameworkCore;
using PersonaApp.Data;

namespace PersonaApp;

public partial class MainWindow : Window
{
    private readonly AppDBContext _db = new AppDBContext();
    public MainWindow()
    {
        InitializeComponent();
        _db.Database.Migrate();
    }
}