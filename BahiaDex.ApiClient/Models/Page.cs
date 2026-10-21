using System.ComponentModel.DataAnnotations;

public class Page
{
    [Key]
    public Guid Id {get; set;}
    public Format Format {get; set;}
    public int IndexInBinder {get; set;}
    public Guid IdBinder {get; set;}
    public Binder Binder {get; set;}
    public List<EmplacementCard> EmplacementCard {get; set;}
}