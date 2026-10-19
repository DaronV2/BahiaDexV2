using System.ComponentModel.DataAnnotations;

public class Serie
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? UrlLogo { get; set; }
}