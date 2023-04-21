namespace Controllers.Dtos;

public class PageResultDto<T>
{
    public IReadOnlyCollection<T>? Items { get; set;} 

    public int TotalCount { get; set; }
}