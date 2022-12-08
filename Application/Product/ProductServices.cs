using Application.Common.Interface;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Product
{
    internal class ProductServices : IProductServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Domain.Models.Product> AddProductAsync(Domain.Models.Product product)
        {
            try
            {
                _unitOfWork.ProductRepo.Add(product);
                await _unitOfWork.SaveAsync();
                product.Category =await _unitOfWork.CategoryRepo.TableNoTracking.FirstOrDefaultAsync(c => c.Id == product.CategoryId);
                return product;
            }
            catch (Exception)
            {
                //Handle Exception
                throw;
            }
        }

        public async Task<bool> DeleteProductAsync(int Id)
        {
            try
            {
                var IsDeteted = await _unitOfWork.ProductRepo.Delete(Id);
                await _unitOfWork.SaveAsync();
                return IsDeteted;
            }
            catch (Exception)
            {
                //handle exception
                return false;
            }
        }

        public async Task<Domain.Models.Product> GetProductByIdAsync(int Id)
        {
            try
            {
                var prodDM = await _unitOfWork.ProductRepo.Table.Include(c => c.Category).FirstOrDefaultAsync(p => p.Id == Id);
                if (prodDM is not null)
                    return prodDM;

                return null;
            }
            catch (Exception)
            {
                //handle exception
                throw;
            }
        }

        public async Task<List<Domain.Models.Product>> GetProductsAsync()
        {
            var result = await _unitOfWork.ProductRepo
              .TableNoTracking.Include(a => a.Category)
              .OrderBy(t => t.Id)
              .ToListAsync();
            return result;
        }

        public async Task<List<Domain.Models.Product>> GetProductsByCategoryIdAsync(int CategoryId)
        {
            var result = await _unitOfWork.ProductRepo
              .TableNoTracking.Where(a => a.CategoryId == CategoryId).Include(a => a.Category)
              .OrderBy(t => t.Id)
              .ToListAsync();
            return result;
        }

        public async Task<Domain.Models.Product> UpdateProductAsync(Domain.Models.Product product)
        {
            try
            {
                var prodDM = await _unitOfWork.ProductRepo.TableNoTracking.FirstOrDefaultAsync(p => p.Id == product.Id);
                if (prodDM is null)
                {
                    throw new Exception($"The Product With Id: {product.Id} Not Fount");
                }
                product.CreatedDate = prodDM.CreatedDate;
                _unitOfWork.ProductRepo.Update(product);
                await _unitOfWork.SaveAsync();
                product.Category = await _unitOfWork.CategoryRepo.TableNoTracking.FirstOrDefaultAsync(c => c.Id == product.CategoryId);
                return product;
            }
            catch (Exception)
            {
                //Handle Exception
                throw;
            }
        }
    }
}
