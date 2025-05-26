using Core.Entities;
using Core.Interfaces;

namespace Services
{
    public class EmpleadoService
    {
        private readonly IEmpleadoRepository _repo;

        public EmpleadoService(IEmpleadoRepository repo) => _repo = repo;

        public void AgregarEmpleado(Empleado e)
        {
            if (e == null) throw new ArgumentNullException(nameof(e));
            _repo.Agregar(e);
        }

        public void ActualizarEmpleado(Empleado e)
        {
            if (e == null) throw new ArgumentNullException(nameof(e));
            _repo.Actualizar(e);
        }

        public Empleado ObtenerPorSSN(string ssn)
        {
            return _repo.ObtenerTodos().FirstOrDefault(e => e.NumeroSeguroSocial == ssn);
        }

        public IEnumerable<string> GenerarReporteSemanal()
        {
            var empleados = _repo.ObtenerTodos();
            return empleados.Select(e =>
            {
                var tipo = e.GetType().Name;
                var pago = e.CalcularPagoSemanal();
                return $"Empleado: {e.PrimerNombre} {e.ApellidoPaterno} ({tipo})\n" +
                       $"- NSS: {e.NumeroSeguroSocial}\n" +
                       $"- Pago semanal: ${pago:0.00}\n";
            });
        }
    }
}