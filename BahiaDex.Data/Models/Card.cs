public class Card
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? UrlImage { get; set; }
    public Category Category { get; set; }
    public string? Illustrator { get; set; }
    public string? Rarity { get; set; }
    public Guid IdSet { get; set; }
    public Set Set { get; set; }
}