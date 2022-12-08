using Application.Category;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs.CategoryDTOs;
using WebApi.Model;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        /// <summary>
        /// Get the list of categories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var result = (await _categoryService.GetCategoriesAsync()).Select(c => CategoryQueryDto.ConvertToDto(c)).ToList();
            if (result is null)
                return NotFound();

            return Ok(ResponseModel.SuccessResponse("Success", result));
        }
        /// <summary>
        /// Get Category by id
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <returns></returns>
        [HttpGet("{CategoryId}")]
        public async Task<IActionResult> GetCategoryById(int CategoryId)
        {
            var DmResult = (await _categoryService.GetCategoryByIdAsync(CategoryId));
            if (DmResult is not null)
            {
                var DtoResult = CategoryQueryDto.ConvertToDto(DmResult);
                return Ok(ResponseModel.SuccessResponse("Success", DtoResult));
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost("AddNew")]
        public async Task<IActionResult> AddNewCategory(CategoryAddNewDto categoryAddNewDto)
        {
            var DmResult = await _categoryService.AddCategoryAsync(CategoryAddNewDto.ConvertToDomain(categoryAddNewDto));
            var DtoResult = CategoryQueryDto.ConvertToDto(DmResult);
            return Ok(ResponseModel.SuccessResponse("Success", DtoResult));
        }
        [HttpPost("Update")]
        public async Task<IActionResult> UpdateCategory(CategoryUpdateDto categoryUpdateDto)
        {
            var DmResult = await _categoryService.UpdateCategoryAsync(CategoryUpdateDto.ConvertToDomain(categoryUpdateDto));
            var DtoResult = CategoryQueryDto.ConvertToDto(DmResult);
            return Ok(ResponseModel.SuccessResponse("Success", DtoResult));
        }
        [HttpDelete("{CategoryId}")]
        public async Task<IActionResult> DeleteCategory(int CategoryId)
        {
            var deleted = (await _categoryService.DeleteCategoryAsync(CategoryId));
            if (deleted)
                return Ok(ResponseModel.SuccessResponse("Category Was Deleted", CategoryId));

            return Ok(ResponseModel.FailureResponse("Not Found"));
        }
    }
}
