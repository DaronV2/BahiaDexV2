using System.ComponentModel.DataAnnotations;

public class Envy
{
    [Key]
    public Guid Id { get; set; }
    public Guid IdCard { get; set; }
    public Card Card { get; set; }
    public string IdUser { get; set; }
    public User User { get; set; }
}