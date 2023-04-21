using Microsoft.AspNetCore.Mvc;
using Controllers.Dtos.BorrowerHistoryDetails;

namespace Controllers.Dtos.BorrowHistories;

public class BorrowHistoryDto
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }
    public string BorrowerName { get; set; }

    public string AddDate { get; set; }

    public int Cost { get; set; }

    public int Quantity { get; set; }

    public ICollection<BorrowHistoryDetailDto> BorrowHistoryDetailDtos { get; set; }
}