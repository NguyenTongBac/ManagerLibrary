using System.ComponentModel.DataAnnotations;

namespace Models.Tables;

public class BaseEntity
{
    [Key]
    public Guid Id { get; set; }

    public DateTime AddDate { get; set; }

    public BaseEntity() => AddDate = DateTime.Now;
}