using System.ComponentModel.DataAnnotations;

public class EmplacementCard
{
    [Key]
    public Guid Id {get; set;}
    public Guid IdCard {get; set;}
    public Card Card {get; set;}
    public Guid IdPage {get; set;}
    public Page Page {get; set;}
    public int IndexInPage {get; set;}
}