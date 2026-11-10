using System.ComponentModel.DataAnnotations;

namespace BahiaDex.ApiClient.Models;

public class Set
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? UrlLogo { get; set; }
    public string? UrlSymbol { get; set; }
    public int CardCount { get; set; }
    public int CardCountTotal { get; set; }
    public int CardCountOfficial { get; set; }
    public Guid IdSerie { get; set; }
    public Serie Serie { get; set; }
}