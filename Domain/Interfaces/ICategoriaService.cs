using ShoppingAPI_Jueves_2023II.DAL.Entities;

namespace ShoppingAPI_Jueves_2023II.Domain.Interfaces
{
    public interface ICategoriaService
    {
        Task<IEnumerable<Categoria>> GetCategoriaByProductoIdAsync(Guid productoId);
        Task<Categoria> GetCategoriaByIdAsync(Guid id);
        Task<Categoria> CreateCategoriaAsync(Categoria categoria, Guid productoId);       
        Task<Categoria> EditCategoriaAsync(Categoria categoria, Guid id);
        Task<Categoria> DeleteCategoriaAsync(Guid id);
    }
}
