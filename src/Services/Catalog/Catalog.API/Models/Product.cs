﻿namespace Catalog.API.Models;

public sealed class Product
{
    public Guid Id {  get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageFile { get; set; } = string.Empty;
    public List<string> Category {  get; set; } = [];
    public decimal Price { get; set; }  
}
