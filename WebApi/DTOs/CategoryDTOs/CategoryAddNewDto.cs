namespace WebApi.DTOs.CategoryDTOs
{
    public class CategoryAddNewDto
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public static Domain.Models.Category ConvertToDomain(CategoryAddNewDto categoryUpdateDto)
        {
            return new Domain.Models.Category()
            {
                Id = 0,
                Name = categoryUpdateDto.Name,
                Description = categoryUpdateDto.Description,
            };
        }
    }
}
