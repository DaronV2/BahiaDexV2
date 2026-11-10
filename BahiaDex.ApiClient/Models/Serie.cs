using System.ComponentModel.DataAnnotations;

namespace BahiaDex.ApiClient.Models;

public class Serie
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? UrlLogo { get; set; }
}