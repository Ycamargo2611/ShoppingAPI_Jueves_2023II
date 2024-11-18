using ShoppingAPI_Jueves_2023II.DAL.Entities;
using ShoppingAPI_Jueves_2023II.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingAPI_Jueves_2023II.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : Controller
    {
        private readonly ICategoriaService _categoriaService;
        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet, ActionName("Get")]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategoriaByProductoIdAsync(Guid productoId)
        {
            var categoria = await _categoriaService.GetCategoriaByProductoIdAsync(productoId);
            if (categoria == null || !categoria.Any()) return NotFound();

            return Ok(categoria);
        }

        [HttpPost, ActionName("Create")]
        [Route("Create")]
        public async Task<ActionResult> CreateCategoriaAsync(Categoria categoria, Guid productoId)
        {
            try
            {
                var createdCategoria = await _categoriaService.CreateCategoriaAsync(categoria, productoId);

                if (createdCategoria == null) return NotFound();

                return Ok(createdCategoria);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate"))
                {
                    return Conflict(String.Format("El Producto {0} ya existe.", categoria.Name));
                }

                return Conflict(ex.Message);
            }
        }

        [HttpGet, ActionName("Get")]
        [Route("GetById/{id}")]
        public async Task<ActionResult<Categoria>> GetCategoriaByIdAsync(Guid id)
        {
            if (id == null) return BadRequest("Id es requerido!");

            var categoria = await _categoriaService.GetCategoriaByIdAsync(id);

            if (categoria == null) return NotFound();

            return Ok(categoria);
        }

        [HttpPut, ActionName("Edit")]
        [Route("Edit")]
        public async Task<ActionResult<Categoria>> EditCategoriaAsync(Categoria categoria, Guid id)
        {
            try
            {
                var editedCategoria = await _categoriaService.EditCategoriaAsync(categoria, id);
                return Ok(editedCategoria);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate"))
                    return Conflict(String.Format("{0} ya existe", categoria.Name));

                return Conflict(ex.Message);
            }
        }

        [HttpDelete, ActionName("Delete")]
        [Route("Delete")]
        public async Task<ActionResult<Categoria>> DeleteCategoriaAsync(Guid id)
        {
            if (id == null) return BadRequest("Id es requerido!");

            var deletedCategoria = await _categoriaService.DeleteCategoriaAsync(id);

            if (deletedCategoria == null) return NotFound("Producto no encontrado!");

            return Ok("State Deleted"); //in Ok() method you can send a message in swagger instead send the object
        }
    }
}
