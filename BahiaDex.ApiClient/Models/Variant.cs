using System.ComponentModel.DataAnnotations;

public class Variant
{
    [Key]
    public Guid Id { get; set; }
    public Guid IdCard { get; set; }
    public Card Card { get; set; }
    public VariantEnum VariantEnum { get; set; }
}