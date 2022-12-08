namespace WebApi.DTOs.CategoryDTOs
{
    public class CategoryUpdateDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public static Domain.Models.Category ConvertToDomain(CategoryUpdateDto categoryUpdateDto)
        {
            return new Domain.Models.Category()
            {
                Id = categoryUpdateDto.CategoryId,
                Name = categoryUpdateDto.Name,
                Description = categoryUpdateDto.Description,
            };
        }
    }
}
