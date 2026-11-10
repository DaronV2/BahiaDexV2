using System.ComponentModel.DataAnnotations;

namespace BahiaDex.ApiClient.Models;

public class Variant
{
    [Key]
    public Guid Id { get; set; }
    public Guid IdCard { get; set; }
    public Card Card { get; set; }
    public VariantEnum VariantEnum { get; set; }
}