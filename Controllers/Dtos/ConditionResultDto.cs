namespace Controllers.Dtos;

public class ConditionResultDto
{
    public string Keyword { get; set; }

    public int SkipCount { get; set; }

    public int MaxResultCount { get; set; }

    public string Sorting { get; set; }
}