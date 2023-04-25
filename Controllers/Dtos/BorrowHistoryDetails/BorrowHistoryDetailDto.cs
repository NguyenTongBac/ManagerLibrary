using Controllers.Dtos.Items;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.Dtos.BorrowerHistoryDetails;

public class BorrowHistoryDetailDto
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }
    
    public string ItemName { get; set; }

    public int Price { get; set; }

    public int Quantity { get; set; }

    public ItemDto Item { get; set; }
}