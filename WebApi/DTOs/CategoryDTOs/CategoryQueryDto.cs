namespace WebApi.DTOs.CategoryDTOs
{
    public class CategoryQueryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public static CategoryQueryDto ConvertToDto(Domain.Models.Category DM)
        {
            return new CategoryQueryDto
            {
                Id = DM.Id,
                Name = DM.Name,
                Description = DM.Description,
                CreatedAt = DM.CreatedDate,
                ModifiedAt = DM.ModifiedDate,
            };
        }
        public static List<CategoryQueryDto> ConvertToDto(List<Domain.Models.Category> DMs)
        {
            List<CategoryQueryDto> categoriesQueryDto = new List<CategoryQueryDto>();
            foreach (var CatDm in DMs)
                categoriesQueryDto.Add(ConvertToDto(CatDm));

            return categoriesQueryDto;
        }
    }
}
