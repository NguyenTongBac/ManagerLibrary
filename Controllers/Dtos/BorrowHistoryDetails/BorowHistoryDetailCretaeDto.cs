using Controllers.Dtos.Items;
using Microsoft.AspNetCore.Mvc;
using Models.Tables;

namespace Controllers.Dtos.BorrowerHistoryDetails;

public class BorrowHistoryDetailCreateDto
{
    public int Price { get; set; }

    public int Quantity { get; set; }

    [HiddenInput]
    public Guid ItemId { get; set; }

    public ItemDto? Item { get; set; }

    public Guid BorrowHistoryId { get; set; }
}