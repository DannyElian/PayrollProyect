using Core.Entities;
using Services;

namespace ConsoleApp
{
    public static class MenuHandler
    {
        public static void MostrarMenu(EmpleadoService servicio)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Sistema de Nómina - Menú Principal");
                Console.WriteLine("1. Agregar Empleado");
                Console.WriteLine("2. Actualizar Empleado");
                Console.WriteLine("3. Ver Reporte Semanal");
                Console.WriteLine("4. Salir");
                Console.Write("Seleccione una opción: ");

                var opcion = Console.ReadLine();
                switch (opcion)
                {
                    case "1": AgregarEmpleado(servicio); break;
                    case "2": ActualizarEmpleado(servicio); break;
                    case "3": VerReporte(servicio); break;
                    case "4": return;
                    default: Console.WriteLine("Opción inválida"); break;
                }

                Console.WriteLine("Presione una tecla para continuar...");
                Console.ReadKey();
            }
        }

        private static void AgregarEmpleado(EmpleadoService servicio)
        {
            Console.WriteLine("Seleccione tipo de empleado:");
            Console.WriteLine("1. Asalariado");
            Console.WriteLine("2. Por Horas");
            Console.WriteLine("3. Por Comisión");
            Console.WriteLine("4. Asalariado por Comisión");
            var tipo = Console.ReadLine();

            Empleado empleado = tipo switch
            {
                "1" => new Asalariado
                {
                    PrimerNombre = Leer("Primer Nombre"),
                    ApellidoPaterno = Leer("Apellido Paterno"),
                    NumeroSeguroSocial = Leer("Número de Seguro Social"),
                    SalarioSemanal = decimal.Parse(Leer("Salario Semanal"))
                },
                "2" => new PorHoras
                {
                    PrimerNombre = Leer("Primer Nombre"),
                    ApellidoPaterno = Leer("Apellido Paterno"),
                    NumeroSeguroSocial = Leer("Número de Seguro Social"),
                    SueldoPorHora = decimal.Parse(Leer("Sueldo por Hora")),
                    HorasTrabajadas = decimal.Parse(Leer("Horas Trabajadas"))
                },
                "3" => new PorComision
                {
                    PrimerNombre = Leer("Primer Nombre"),
                    ApellidoPaterno = Leer("Apellido Paterno"),
                    NumeroSeguroSocial = Leer("Número de Seguro Social"),
                    VentasBrutas = decimal.Parse(Leer("Ventas Brutas")),
                    TarifaComision = decimal.Parse(Leer("Tarifa de Comisión"))
                },
                "4" => new AsalariadoPorComision
                {
                    PrimerNombre = Leer("Primer Nombre"),
                    ApellidoPaterno = Leer("Apellido Paterno"),
                    NumeroSeguroSocial = Leer("Número de Seguro Social"),
                    VentasBrutas = decimal.Parse(Leer("Ventas Brutas")),
                    TarifaComision = decimal.Parse(Leer("Tarifa de Comisión")),
                    SalarioBase = decimal.Parse(Leer("Salario Base"))
                },
                _ => null
            };

            if (empleado != null)
            {
                servicio.AgregarEmpleado(empleado);
                Console.WriteLine("Empleado agregado correctamente.");
            }
            else
            {
                Console.WriteLine("Tipo de empleado inválido.");
            }
        }

        private static void ActualizarEmpleado(EmpleadoService servicio)
        {
            string ssn = Leer("Número de Seguro Social del empleado a actualizar");
            var existente = servicio.ObtenerPorSSN(ssn);

            if (existente == null)
            {
                Console.WriteLine("Empleado no encontrado.");
                return;
            }

            Console.WriteLine("Deja vacío cualquier campo que no desees cambiar.");
            existente.PrimerNombre = LeerOpcional("Primer Nombre", existente.PrimerNombre);
            existente.ApellidoPaterno = LeerOpcional("Apellido Paterno", existente.ApellidoPaterno);

            switch (existente)
            {
                case Asalariado a:
                    a.SalarioSemanal = LeerDecimalOpcional("Salario Semanal", a.SalarioSemanal);
                    break;
                case PorHoras h:
                    h.SueldoPorHora = LeerDecimalOpcional("Sueldo por Hora", h.SueldoPorHora);
                    h.HorasTrabajadas = LeerDecimalOpcional("Horas Trabajadas", h.HorasTrabajadas);
                    break;
                case PorComision c:
                    c.VentasBrutas = LeerDecimalOpcional("Ventas Brutas", c.VentasBrutas);
                    c.TarifaComision = LeerDecimalOpcional("Tarifa Comisión", c.TarifaComision);
                    break;
                case AsalariadoPorComision ac:
                    ac.VentasBrutas = LeerDecimalOpcional("Ventas Brutas", ac.VentasBrutas);
                    ac.TarifaComision = LeerDecimalOpcional("Tarifa Comisión", ac.TarifaComision);
                    ac.SalarioBase = LeerDecimalOpcional("Salario Base", ac.SalarioBase);
                    break;
            }

            servicio.ActualizarEmpleado(existente);
            Console.WriteLine("Empleado actualizado correctamente.");
        }

        private static string LeerOpcional(string campo, string valorActual)
        {
            Console.Write($"{campo} (actual: {valorActual}): ");
            string input = Console.ReadLine();
            return string.IsNullOrWhiteSpace(input) ? valorActual : input;
        }

        private static decimal LeerDecimalOpcional(string campo, decimal valorActual)
        {
            Console.Write($"{campo} (actual: {valorActual}): ");
            string input = Console.ReadLine();
            return decimal.TryParse(input, out var val) ? val : valorActual;
        }


        private static void VerReporte(EmpleadoService servicio)
        {
            Console.WriteLine("--- Reporte Semanal ---");
            foreach (var r in servicio.GenerarReporteSemanal())
            {
                Console.WriteLine(r);
            }
        }

        private static string Leer(string campo)
        {
            Console.Write($"{campo}: ");
            return Console.ReadLine();
        }
    }
}