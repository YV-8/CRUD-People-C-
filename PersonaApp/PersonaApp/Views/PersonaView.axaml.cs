using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using PersonaApp.ViewModels;

namespace PersonApp.Views;

public partial class PersonaView : UserControl
{
    public PersonaView()
    {
        InitializeComponent();
        // inicializado llamada al db context para  darle contexto con la referencia
        DataContext = new PersonVM();
    }
}