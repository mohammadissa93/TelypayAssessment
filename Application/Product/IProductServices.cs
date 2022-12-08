using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Product
{
    public interface IProductServices
    {
        Task<List<Domain.Models.Product>> GetProductsAsync();
        Task<List<Domain.Models.Product>> GetProductsByCategoryIdAsync(int CategoryId);
        Task<Domain.Models.Product> GetProductByIdAsync(int Id);
        Task<Domain.Models.Product> AddProductAsync(Domain.Models.Product product);
        Task<Domain.Models.Product> UpdateProductAsync(Domain.Models.Product product);
        Task<bool> DeleteProductAsync(int Id);
    }
}
