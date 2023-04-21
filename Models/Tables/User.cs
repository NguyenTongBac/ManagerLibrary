namespace Models.Tables;

public class User : BaseEntity
{
    public string UserName { get; set; }
    
    public string Password { get; set; }

    public string? Token { get; set; }
    
    public User() : base() {}
}