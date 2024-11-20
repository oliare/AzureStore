﻿namespace ApiStore.Models.Product;

public class ProductItemViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public string? CategoryName { get; set; }
    public string? Description { get; set; }
    public List<string>? DescriptionImages { get; set; }
    public List<string>? Images { get; set; }
}
