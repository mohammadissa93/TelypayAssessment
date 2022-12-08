using Application.Category;
using Application.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs.CategoryDTOs;
using WebApi.DTOs.ProductDTOs;
using WebApi.Model;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductServices _productServices;
        public ProductsController(IProductServices productService)
        {
            _productServices = productService;
        }
        /// <summary>
        /// Get the list of products
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = (await _productServices.GetProductsAsync()).Select(p => ProductQueryDto.ConvertToDto(p)).ToList();
            if (result is null)
                return NotFound();

            return Ok(ResponseModel.SuccessResponse("Success", result));
        }
        /// <summary>
        /// Get product by id
        /// </summary>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        [HttpGet("{ProductId}")]
        public async Task<IActionResult> GetProductById(int ProductId)
        {
            var DmResult = (await _productServices.GetProductByIdAsync(ProductId));
            if (DmResult is not null)
            {
                var DtoResult = ProductQueryDto.ConvertToDto(DmResult);
                return Ok(ResponseModel.SuccessResponse("Success", DtoResult));
            }
            else
            {
                return NotFound();
            }
        }
        /// <summary>
        /// Get product by category Id
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <returns></returns>
        [HttpGet("ByCategoryId/{CategoryId}")]
        public async Task<IActionResult> GetProductByCategoryId(int CategoryId)
        {
            var DmResult = (await _productServices.GetProductsByCategoryIdAsync(CategoryId));
            if (DmResult is not null)
            {
                var DtoResult = ProductQueryDto.ConvertToDto(DmResult);
                return Ok(ResponseModel.SuccessResponse("Success", DtoResult));
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost("AddNew")]
        public async Task<IActionResult> AddNewProduct(ProductPostDto productPostDto)
        {
            var DmResult = await _productServices.AddProductAsync(ProductPostDto.ConvertToDomain(productPostDto));
            var DtoResult = ProductQueryDto.ConvertToDto(DmResult);
            return Ok(ResponseModel.SuccessResponse("Success", DtoResult));
        }
        [HttpPost("Update")]
        public async Task<IActionResult> UpdateProduct(ProductUpdateDto productPostDto)
        {
            var DmResult = await _productServices.UpdateProductAsync(ProductUpdateDto.ConvertToDomain(productPostDto));
            var DtoResult = ProductQueryDto.ConvertToDto(DmResult);
            return Ok(ResponseModel.SuccessResponse("Success", DtoResult));
        }
        [HttpDelete("{ProductId}")]
        public async Task<IActionResult> DeleteCategory(int ProductId)
        {
            var deleted = (await _productServices.DeleteProductAsync(ProductId));
            if (deleted)
                return Ok(ResponseModel.SuccessResponse("Product Was Deleted", ProductId));

            return Ok(ResponseModel.FailureResponse("Not Found"));
        }
    }
}
