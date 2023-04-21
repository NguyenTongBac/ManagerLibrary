using System.ComponentModel.DataAnnotations;

namespace Models.Tables;

public class BorrowHistoryDetail : BaseEntity
{
    public int Quantity { get; set; }

    public int Price { get; set; }

    public Item Item { get; set; }

    [Required]
    public Guid ItemId { get; set; }
    
    public BorrowHistory BorrowHistory { get; set; }

    [Required]
    public Guid BorrowHistoryId { get; set; }
}