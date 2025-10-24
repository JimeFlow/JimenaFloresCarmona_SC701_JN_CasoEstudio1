namespace CasoEstudio1.Models
{
    public class EjerciciosModel
    {
        public long Consecutivo { get; set; }
        public required string Identificacion { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public int TipoEjercicio { get; set; }
        public required string DescripcionTipoEjercicio { get; set; }
    }
}
