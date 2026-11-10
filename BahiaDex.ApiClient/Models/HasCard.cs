using System.ComponentModel.DataAnnotations;

namespace BahiaDex.ApiClient.Models;

public class HasCard
{
    [Key]
    public Guid Id { get; set; }
    public string IdUser { get; set; }
    public User User {get; set;}
    public Guid IdCard { get; set; }
    public Card Card { get; set; }
}