using Microsoft.AspNetCore.Mvc;
using Models.Enums;

namespace Controllers.Dtos.Items;

public class ItemDto
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }
    
    public string Name { get; set; }

    public string Author { get; set; }

    public string PublishDate { get; set; }

    public int Price { get; set; }

    public Category Category { get; set; }

    public int NumberOfPage { get; set; }

    public string RunTime { get; set; }
}