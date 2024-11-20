using ApiStore.Data.Entities;

namespace ApiStore.Models.Product;

public class ProductDescImageIdViewModel
{
    public required int Id { get; set; }
    public required string Image { get; set; }

    public static ProductDescImageIdViewModel Create(ProductDescImageEntity entity)
    {
        return new ProductDescImageIdViewModel { Id = entity.Id, Image = entity.Image };
    }
}
