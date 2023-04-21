using System.ComponentModel.DataAnnotations;

namespace Models.Tables;

public class Borrower : BaseEntity
{
    [Required]
    [MaxLength(255)]
    public string Name { get; set; }

    [Required]
    [MaxLength(255)]
    public string Email { get; set; }

    public ICollection<BorrowHistory> BorrowHistories { get; set; }

    public Borrower() : base()
    {
        BorrowHistories = new HashSet<BorrowHistory>();
    }
}