using System.ComponentModel.DataAnnotations;

namespace ShoppingAPI_Jueves_2023II.DAL.Entities
{
    public class Categoria: AuditBase
    {
        [Display(Name = "Producto/Categoria")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres")]
        [Required(ErrorMessage = "¡El campo {0} es obligatorio!")]
        public string Name { get; set; }

        [Display(Name = "Producto")]
        //Relación con Producto
        public Producto? Producto { get; set; } //Este representa un OBJETO DE COUNTRY

        [Display(Name = "Id País")]
        public Guid ProductoId { get; set; } //FK
    }
}
