using System.ComponentModel.DataAnnotations;

namespace CasoEstudio1.Models
{
    public class EjercicioCreateModel
    {
        [Required(ErrorMessage = "La identificación es obligatoria")]
        [StringLength(15, ErrorMessage = "La identificación no puede exceder 15 caracteres")]

        public string Identificacion { get; set; } = string.Empty;

        [Required(ErrorMessage = "El monto es obligatorio")]
        [Range(0.01, 9999999.99, ErrorMessage = "El monto debe ser un número positivo")]
        public decimal Monto { get; set; }

        [Required(ErrorMessage = "El tipo de ejercicio es obligatorio")]
        [StringLength(50, ErrorMessage = "El tipo de ejercicio no puede exceder 50 caracteres")]
        public string DescripcionTipoEjercicio { get; set; } = string.Empty;
    }
}
