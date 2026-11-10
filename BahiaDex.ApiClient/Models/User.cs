using System.ComponentModel.DataAnnotations;

namespace BahiaDex.ApiClient.Models;

public class User
{
    [Key]
    public string IdGoogleAcc { get; set; }
    public string Email { get; set; }
}