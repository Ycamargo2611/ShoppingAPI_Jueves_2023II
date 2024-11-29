using Microsoft.EntityFrameworkCore;
using ShoppingAPI_Jueves_2023II.DAL;
using ShoppingAPI_Jueves_2023II.DAL.Entities;
using ShoppingAPI_Jueves_2023II.Domain.Interfaces;

namespace ShoppingAPI_Jueves_2023II.Domain.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly DataBaseContext _context;

        public CategoriaService(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Categoria>> GetCategoriaByProductoIdAsync(Guid productoId)
        {
            return await _context.Categorias
                .Where(s => s.ProductoId == productoId)
                .ToListAsync();
        }

        public async Task<Categoria> CreateCategoriaAsync(Categoria categoria, Guid productoId)
        {
            try
            {
                categoria.Id = Guid.NewGuid();
                categoria.CreatedDate = DateTime.Now;
                categoria.ProductoId = productoId;
                categoria.Producto = await _context.Productos.FirstOrDefaultAsync(c => c.Id == productoId);

                _context.Categorias.Add(categoria);
                await _context.SaveChangesAsync();

                return categoria;
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
            }
        }

        public async Task<Producto> GetProductoByIdAsync(Guid id)
        {
            return await _context.Productos.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Categoria> GetCategoriaByIdAsync(Guid id)
        {
            return await _context.Categorias.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Categoria> EditCategoriaAsync(Categoria categoria, Guid id)
        {
            try
            {
                //categoria.ModifiedDate = DateTime.Now;

                _context.Categorias.Update(categoria);
                await _context.SaveChangesAsync();

                return categoria;
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
            }
        }

        public async Task<Categoria> DeleteCategoriaAsync(Guid id)
        {
            try
            {
                var categoria = await _context.Categorias.FirstOrDefaultAsync(s => s.Id == id);
                if (categoria == null) return null;

                _context.Categorias.Remove(categoria);
                await _context.SaveChangesAsync();

                return categoria;
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
            }
        }
    }
}
