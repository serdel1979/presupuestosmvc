using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class TipoCuenta
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="El nombre es requerido")]
        [StringLength(maximumLength:50, MinimumLength =5, ErrorMessage = "La longitud debe ser de {2} a {1}")]
        public string Nombre { get; set; }
        public int UsuarioId { get; set; }
        [Required(ErrorMessage = "Debe ingresar un número")]
        public int Orden { get; set; }
    }
}
