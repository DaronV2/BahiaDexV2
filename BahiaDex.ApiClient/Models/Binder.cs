using System.ComponentModel.DataAnnotations;

public class Binder
{
    [Key]
    public Guid Id { get; set; }
    public string Name {get; set; }
    public string IdUser {get; set; }
    public User User { get; set; }
    public List<Page> Pages {get; set;} 
}