using Microsoft.AspNetCore.Mvc;
using ShoppingAPI_Jueves_2023II.DAL.Entities;
using ShoppingAPI_Jueves_2023II.Domain.Interfaces;

namespace ShoppingAPI_Jueves_2023II.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public CountriesController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet, ActionName("Get")]
        [Route("GetAll")]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductoAsync()
        {
            var productos = await _productoService.GetProductoAsync();

            if (productos == null || !productos.Any()) return NotFound();

            return Ok(productos);
        }

        [HttpPost, ActionName("Create")]
        [Route("Create")]
        public async Task<ActionResult> CreatePruductoAsync(Producto producto)
        {
            try
            {
                var createdProducto = await _productoService.CreateProductoAsync(producto);
                return Ok(createdProducto);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate"))
                    return Conflict(String.Format("{0} ya existe", producto.Name));

                return Conflict(ex.Message);
            }
        }

        [HttpGet, ActionName("Get")]
        [Route("GetById/{id}")] //URL: api/countries/get
        public async Task<ActionResult<Producto>> GetProductoByIdAsync(Guid id)
        {
            if (id == null) return BadRequest("Id es requerido!");

            var producto = await _productoService.GetProductoByIdAsync(id);

            if (producto == null) return NotFound(); // 404

            return Ok(producto); // 200
        }

        [HttpGet, ActionName("Get")]
        [Route("GetByName/{name}")] //URL: api/countries/get
        public async Task<ActionResult<Producto>> GetProductoByNameAsync(string name)
        {
            if (name == null) return BadRequest("Nombre del país requerido!");

            var producto = await _productoService.GetProductoByNameAsync(name);

            if (producto == null) return NotFound(); // 404

            return Ok(producto); // 200
        }

        [HttpPut, ActionName("Edit")]
        [Route("Edit")]
        public async Task<ActionResult<Country>> EditProductoAsync(Producto producto)
        {
            try
            {
                var editedProducto = await _productoService.EditProductoAsync(producto);
                return Ok(editedProducto);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate"))
                    return Conflict(String.Format("{0} ya existe", producto.Name));

                return Conflict(ex.Message);
            }
        }

        [HttpDelete, ActionName("Delete")]
        [Route("Delete")]
        public async Task<ActionResult<Producto>> DeleteProductoAsync(Guid id)
        {
            if (id == null) return BadRequest("Id es requerido!");

            var deletedProducto = await _productoService.DeleteProductoAsync(id);

            if (deletedProducto == null) return NotFound("País no encontrado!");

            return Ok(deletedProducto);
        }
    }
}