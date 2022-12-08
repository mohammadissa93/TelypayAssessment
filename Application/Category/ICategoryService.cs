using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Category
{
    public interface ICategoryService
    {
        Task<List<Domain.Models.Category>> GetCategoriesAsync();
        Task<Domain.Models.Category> GetCategoryByIdAsync(int Id);
        Task<Domain.Models.Category> AddCategoryAsync(Domain.Models.Category category);
        Task<Domain.Models.Category> UpdateCategoryAsync(Domain.Models.Category category);
        Task<bool> DeleteCategoryAsync(int Id);
    }
}
