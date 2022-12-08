using WebApi.DTOs.CategoryDTOs;

namespace WebApi.DTOs.ProductDTOs
{
    public class ProductPostDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public long Quantity { get; set; }
        public double Price { get; set; }
        public string Currency { get; set; }
        public int MinPurchaseQty { get; set; }
        public int MaxPurchaseQty { get; set; }
        public int CategoryId { get; set; }
        public static Domain.Models.Product ConvertToDomain(ProductPostDto productPostDto)
        {
            return new Domain.Models.Product()
            {
                Id = 0,
                Name = productPostDto.Name,
                Description = productPostDto.Description,
                Quantity = productPostDto.Quantity,
                Price = productPostDto.Price,
                Currency = productPostDto.Currency,
                MinPurchaseQty = productPostDto.MinPurchaseQty,
                MaxPurchaseQty = productPostDto.MaxPurchaseQty,
                CategoryId = productPostDto.CategoryId,
            };
        }
    }
}
