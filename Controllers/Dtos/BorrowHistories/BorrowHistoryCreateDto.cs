using System.Collections.Generic;
using Controllers.Dtos.BorrowerHistoryDetails;

namespace Controllers.Dtos.BorrowHistories;

public class BorrowHistoryCreateDto
{
    public Guid BorrowerId { get; set; }

    public List<BorrowHistoryDetailCreateDto> BorrowHistoryDetailCreateDtos{ get; set; }

    public BorrowHistoryCreateDto(){
        BorrowHistoryDetailCreateDtos = new List<BorrowHistoryDetailCreateDto>();
    }
}