using Microsoft.EntityFrameworkCore;
using ShoppingAPI_Jueves_2023II.DAL;
using ShoppingAPI_Jueves_2023II.DAL.Entities;
using ShoppingAPI_Jueves_2023II.Domain.Interfaces;

namespace ShoppingAPI_Jueves_2023II.Domain.Services
{
    public class ProductoService : IProductoService
    {
        private readonly DataBaseContext _context;

        public ProductoService(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Producto>> GetProductoAsync()
        {
            return await _context.Producto
                .Include(c => c.States) //include countries and state list
                .ToListAsync();
        }

        public async Task<Producto> CreateProductoAsync(Producto producto)
        {
            try
            {
                producto.Id = Guid.NewGuid();
                producto.CreatedDate = DateTime.Now;

                _context.Countries.Add(producto);
                await _context.SaveChangesAsync();

                return producto;
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
            }
        }

        public async Task<Producto> GetProductoByIdAsync(Guid id)
        {
            //return await _context.Countries.FindAsync(id); // FindAsync es un método propio del DbContext (DbSet)
            //return await _context.Countries.FirstAsync(x => x.Id == id); //FirstAsync es un método de EF CORE
            return await _context.Producto
                .Include(c => c.States)
                .FirstOrDefaultAsync(c => c.Id == id); //FirstOrDefaultAsync es un método de EF CORE
        }

        public async Task<Producto> GetProductoByNameAsync(string name)
        {
            return await _context.Countries.FirstOrDefaultAsync(c => c.Name == name);
        }

        public async Task<Producto> EditProductoAsync(Producto producto)
        {
            try
            {
                producto.ModifiedDate = DateTime.Now;

                _context.Producto.Update(producto); //El método Update que es de EF CORE me sirve para Actualizar un objeto
                await _context.SaveChangesAsync();

                return producto;
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
            }
        }

        public async Task<Producto> DeleteProductoAsync(Guid id)
        {
            try
            {
                //Aquí, con el ID que traigo desde el controller, estoy recuperando el país que luego voy a eliminar.
                //Ese país que recupero lo guardo en la variable country
                var producto = await _context.Producto
                    .Include(c => c.States) // cascade removing
                    .FirstOrDefaultAsync(c => c.Id == id);
                if (producto == null) return null; //Si el país no existe, entonces me retorna un NULL

                _context.Producto.Remove(producto);
                await _context.SaveChangesAsync();

                return producto;
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
            }
        }
    }
}
