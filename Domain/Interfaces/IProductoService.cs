using ShoppingAPI_Jueves_2023II.DAL.Entities;

namespace ShoppingAPI_Jueves_2023II.Domain.Interfaces
{
    public interface IProductoService
    {
        Task<IEnumerable<Producto>>GetProductoAsync();
        Task<Producto>CreateProductoAsync(Producto producto);
        Task<Producto>GetProductoByIdAsync(Guid id);
        Task<Producto>GetProductoByNameAsync(string name);
        Task<Producto>EditProductoAsync(Producto producto);
        Task<Producto>DeleteProductoAsync(Guid id);
    }
}
