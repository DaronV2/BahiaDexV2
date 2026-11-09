using System.ComponentModel.DataAnnotations;

public class User
{
    [Key]
    public string IdGoogleAcc { get; set; }
    public string Email { get; set; }
}