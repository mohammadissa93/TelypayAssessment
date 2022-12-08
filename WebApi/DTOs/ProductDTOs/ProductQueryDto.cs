using WebApi.DTOs.CategoryDTOs;

namespace WebApi.DTOs.ProductDTOs
{
    public class ProductQueryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long Quantity { get; set; }
        public string PriceWithCurrency { get; set; }
        public int MinPurchaseQty { get; set; }
        public int MaxPurchaseQty { get; set; }
        public CategoryQueryDto Category { get; set; }

        public static ProductQueryDto ConvertToDto(Domain.Models.Product DM)
        {
            return new ProductQueryDto
            {
                Id = DM.Id,
                Name = DM.Name,
                Description = DM.Description,
                Quantity = DM.Quantity,
                PriceWithCurrency = DM.Price.ToString()+" " + DM.Currency,
                MinPurchaseQty = DM.MinPurchaseQty,
                MaxPurchaseQty = DM.MaxPurchaseQty,
                Category = CategoryQueryDto.ConvertToDto(DM.Category),
            };
        }
        public static List<ProductQueryDto> ConvertToDto(List<Domain.Models.Product> DMs)
        {
            List<ProductQueryDto> ProductsQueryDto = new List<ProductQueryDto>();
            foreach (var CatDm in DMs)
                ProductsQueryDto.Add(ConvertToDto(CatDm));

            return ProductsQueryDto;
        }
    }
}
