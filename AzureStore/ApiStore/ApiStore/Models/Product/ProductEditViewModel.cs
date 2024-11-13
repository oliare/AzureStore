using Microsoft.AspNetCore.Mvc;

namespace ApiStore.Models.Product;

public class ProductEditViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    [BindProperty(Name = "images[]")]
    public List<IFormFile>? Images { get; set; }
}