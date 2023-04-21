namespace Controllers.Dtos.BorrowerHistoryDetails;

public class BorrowHistoryDetailCreateDto
{
    public int Price { get; set; }

    public int Quantity { get; set; }

    public Guid ItemId { get; set; }

    public Guid BorrowHistoryId { get; set; }
}