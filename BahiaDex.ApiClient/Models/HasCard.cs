using System.ComponentModel.DataAnnotations;

public class HasCard
{
    [Key]
    public Guid Id { get; set; }
    public string IdUser { get; set; }
    public User User {get; set;}
    public Guid IdCard { get; set; }
    public Card Card { get; set; }
}