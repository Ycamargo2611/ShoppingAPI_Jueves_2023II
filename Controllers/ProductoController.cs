using Microsoft.AspNetCore.Mvc;
using ShoppingAPI_Jueves_2023II.DAL.Entities;
using ShoppingAPI_Jueves_2023II.Domain.Interfaces;
using System.Drawing;
using System;

namespace ShoppingAPI_Jueves_2023II.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductoController(IProductoService productoService)
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

        [HttpGet, ActionName("Get")]
        [Route("GetById/{id}")] //URL: api/countries/get
        public async Task<ActionResult<Producto>> GetProductoByIdAsync(Guid id)
        {
            //No es necesario validar id == null porque Guid es un tipo por valor, nunca será null.
            //if (id == null) return BadRequest("Id es requerido!");

            var producto = await _productoService.GetProductoByIdAsync(id);

            if (producto == null) return NotFound(); // 404

            return Ok(producto); // 200
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
        [Route("GetByName/{name}")] //URL: api/countries/get
        public async Task<ActionResult<Producto>> GetProductoByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("El nombre del producto es requerido."); // 400

            var producto = await _productoService.GetProductoByNameAsync(name);

            return producto != null ? Ok(producto) : NotFound(); // 200 o 404
        }

        [HttpPut, ActionName("Edit")]
        [Route("Edit")]
        public async Task<ActionResult<Producto>> EditProductoAsync(Producto producto)
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
            //No es necesario validar id == null porque Guid es un tipo por valor, nunca será null.
            //if (id == null) return BadRequest("Id es requerido!");

            try
            {
                var deletedProducto = await _productoService.DeleteProductoAsync(id);

                if (deletedProducto == null) return NotFound("Producto no encontrado!");

                return Ok(deletedProducto);
            }
            catch (Exception ex)
            {

                return NotFound();
            }


        }
    }
}