using Application.Common.Interface;
using Microsoft.EntityFrameworkCore;


namespace Application.Category
{
    internal class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Domain.Models.Category> AddCategoryAsync(Domain.Models.Category category)
        {
            try
            {
                _unitOfWork.CategoryRepo.Add(category);
                await _unitOfWork.SaveAsync();
                return category;
            }
            catch (Exception)
            {
                //Handle Exception
                throw;
            }
        }
        public async Task<Domain.Models.Category> UpdateCategoryAsync(Domain.Models.Category category)
        {
            try
            {
                var catDM = await _unitOfWork.CategoryRepo.TableNoTracking.FirstOrDefaultAsync(c => c.Id == category.Id);
                if (catDM is null) {
                    throw new Exception($"The Category With Id: {category.Id} Not Fount");
                }
                category.CreatedDate = catDM.CreatedDate;
                _unitOfWork.CategoryRepo.Update(category);
                await _unitOfWork.SaveAsync();
                return category;
            }
            catch (Exception)
            {
                //Handle Exception
                throw;
            }
        }
        public async Task<bool> DeleteCategoryAsync(int Id)
        {
            try
            {
                var IsDeteted = await _unitOfWork.CategoryRepo.Delete(Id);
                await _unitOfWork.SaveAsync();
                return IsDeteted;
            }
            catch (Exception)
            {
                //handle exception
                return false;
            }
        }
        public async Task<List<Domain.Models.Category>> GetCategoriesAsync()
        {
            var result = await _unitOfWork.CategoryRepo
              .TableNoTracking
              .OrderBy(t => t.Id)
              .ToListAsync();
            return result;
        }
        public async Task<Domain.Models.Category> GetCategoryByIdAsync(int Id)
        {
            try
            {
                var catDM = await _unitOfWork.CategoryRepo.Get(Id);
                if (catDM is not null)
                    return catDM;

                return null;
            }
            catch (Exception)
            {
                //handle exception
                throw;
            }
        }
    }
}
