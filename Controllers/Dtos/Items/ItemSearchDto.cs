using Controllers.Dtos;
using Models.Enums;

namespace Controllers.Dtos.Items;

public class ItemSearchDto : ConditionResultDto
{
    public Category[] Categories { get; set; }
}