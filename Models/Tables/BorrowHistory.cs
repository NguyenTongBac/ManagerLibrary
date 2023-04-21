using System.ComponentModel.DataAnnotations;

namespace Models.Tables;

public class BorrowHistory : BaseEntity
{
    public int Cost { get; set; }

    public Borrower Borrower { get; set; }

    [Required]
    public Guid BorrowerId { get; set; }

    public ICollection<BorrowHistoryDetail> BorrowHistoryDetails { get; set; }

    public BorrowHistory() : base()
    {
        BorrowHistoryDetails = new HashSet<BorrowHistoryDetail>();
    }
}