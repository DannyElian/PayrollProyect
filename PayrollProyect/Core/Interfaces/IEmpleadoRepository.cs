namespace Core.Interfaces
{
    using Core.Entities;
    public interface IEmpleadoRepository
    {
        void Agregar(Empleado empleado);
        IEnumerable<Empleado> ObtenerTodos();
        void Actualizar(Empleado empleado);
    }
}