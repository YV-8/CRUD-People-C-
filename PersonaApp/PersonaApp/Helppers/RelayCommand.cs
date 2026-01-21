using System;
using System.Windows.Input;
using PersonaApp.Models;

namespace PersonaApp.Helppers;

public class RelayCommand:ICommand
{
    /*
     * Delegado  Se encargara de manejar los comandos
     * cuando tengamos que llemarlos
     */
    private readonly Action _execute;
    private readonly Func<bool> _canExecute;
    public RelayCommand(Action execute, Func<bool> canExecute = null)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }
    public bool CanExecute(object? parameter) => _canExecute?.Invoke() ?? true;

    public void Execute(object? parameter) => _execute();

    /*
     * es requerido por el Icommand
     */
    public event EventHandler? CanExecuteChanged;
}