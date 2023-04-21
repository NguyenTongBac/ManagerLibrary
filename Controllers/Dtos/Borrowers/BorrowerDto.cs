using Microsoft.AspNetCore.Mvc;

namespace Controllers.Dtos.Borrowers;

public class BorrowerDto
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }
    
    public string Name { get; set; }

    public string Email { get; set; }
}