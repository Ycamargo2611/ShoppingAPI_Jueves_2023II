using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ShoppingAPI_Jueves_2023II.DAL.Entities
{
    public class State : AuditBase
    {
        [Display(Name = "Estado/Departamento")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres")]
        [Required(ErrorMessage = "¡El campo {0} es obligatorio!")]
        public string Name { get; set; }

        [Display(Name = "producto")]
        //Relación con Producto
        public Producto? Producto { get; set; } //Este representa un OBJETO DE Producto

        [Display(Name = "Id producto")]
        public Guid ProductoId { get; set; } //FK
    }
}
