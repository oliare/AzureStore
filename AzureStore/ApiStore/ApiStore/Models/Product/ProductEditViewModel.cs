using Microsoft.AspNetCore.Mvc;

namespace ApiStore.Models.Product;

public class ProductEditViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    [BindProperty(Name = "newImages[]")]
    public List<IFormFile>? NewImages { get; set; }
    public List<string>? RemoveImages { get; set; }
}