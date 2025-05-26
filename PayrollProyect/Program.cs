using Core.Interfaces;
using Infrastructure.Repositories;
using Services;
using ConsoleApp;

class Program
{
    static void Main()
    {
        IEmpleadoRepository repo = new EmpleadoRepository();
        var servicio = new EmpleadoService(repo);

        MenuHandler.MostrarMenu(servicio);
    }
}
