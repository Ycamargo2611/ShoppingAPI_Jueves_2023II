using System.ComponentModel.DataAnnotations;

namespace ShoppingAPI_Jueves_2023II.DAL.Entities
{
    public class Categoria: AuditBase
    {


        [Display(Name = "Producto/Categoria")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres")]
        [Required(ErrorMessage = "¡El campo {0} es obligatorio!")]
        public string Name { get; set; }

        [Display(Name = "Producto Id")]
        //Relación con Producto
        public Producto? Producto { get; set; } //Este representa un OBJETO DE PRODUCTO

        [Display(Name = "Id producto")]
        public Guid ProductoId { get; set; } //FK
    }
}
