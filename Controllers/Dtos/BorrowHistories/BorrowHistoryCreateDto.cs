using Controllers.Dtos.BorrowerHistoryDetails;

namespace Controllers.Dtos.BorrowHistories;

public class BorrowHistoryCreateDto
{
    public Guid BorrowerId { get; set; }

    public int Cost { get; set; }

    public ICollection<BorrowHistoryDetailCreateDto> BorrowHistoryDetailCreateDtos{ get; set; }
}