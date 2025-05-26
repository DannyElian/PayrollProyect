namespace Core.Entities
{
    public abstract class Empleado
    {
        public string PrimerNombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string NumeroSeguroSocial { get; set; }

        public abstract decimal CalcularPagoSemanal();
    }

    public class Asalariado : Empleado
    {
        public decimal SalarioSemanal { get; set; }
        public override decimal CalcularPagoSemanal() => SalarioSemanal;
    }

    public class PorHoras : Empleado
    {
        public decimal SueldoPorHora { get; set; }
        public decimal HorasTrabajadas { get; set; }
        public override decimal CalcularPagoSemanal()
        {
            return HorasTrabajadas <= 40 ?
                SueldoPorHora * HorasTrabajadas :
                (SueldoPorHora * 40) + (SueldoPorHora * 1.5m * (HorasTrabajadas - 40));
        }
    }

    public class PorComision : Empleado
    {
        public decimal VentasBrutas { get; set; }
        public decimal TarifaComision { get; set; }
        public override decimal CalcularPagoSemanal() => VentasBrutas * TarifaComision;
    }

    public class AsalariadoPorComision : Empleado
    {
        public decimal VentasBrutas { get; set; }
        public decimal TarifaComision { get; set; }
        public decimal SalarioBase { get; set; }
        public override decimal CalcularPagoSemanal() =>
            (VentasBrutas * TarifaComision) + SalarioBase + (SalarioBase * 0.10m);
    }
}