namespace Infrastructure.Repositories
{
    using Core.Entities;
    using Core.Interfaces;

    public class EmpleadoRepository : IEmpleadoRepository
    {
        private readonly List<Empleado> _empleados = new();

        public void Agregar(Empleado empleado) => _empleados.Add(empleado);

        public IEnumerable<Empleado> ObtenerTodos() => _empleados;

        public void Actualizar(Empleado empleado)
        {
            var index = _empleados.FindIndex(e => e.NumeroSeguroSocial == empleado.NumeroSeguroSocial);
            if (index >= 0)
                _empleados[index] = empleado;
        }
    }
}