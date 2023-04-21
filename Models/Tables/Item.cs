using System.ComponentModel.DataAnnotations;
using Models.Enums;

namespace Models.Tables;

public class Item : BaseEntity
{
    [Required]
    [MaxLength(255)]
    public string Name { get; set; }

    [Required]
    [MaxLength(255)]
    public string Author { get; set; }

    public DateTime PublishDate { get; set; }

    public Category Category { get; set; }

    public int NumberOfPage { get; set; }

    public TimeSpan RunTime { get; set; }

    public int Price { get; set; }

    ICollection<BorrowHistoryDetail> BorrowHistoryDetails { get; set; }

    public Item() : base()
    {
        BorrowHistoryDetails = new HashSet<BorrowHistoryDetail>();
    }
}