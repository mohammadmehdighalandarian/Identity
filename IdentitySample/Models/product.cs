using System.ComponentModel.DataAnnotations;


namespace IdentitySample.Models;

public class product
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }
}
