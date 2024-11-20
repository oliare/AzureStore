using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiStore.Data.Entities;

public class ProductDescImageEntity
{
    [Key]
    public int Id { get; set; }
    [Required, StringLength(255)]
    public string Image { get; set; } = string.Empty;
    public DateTime DateCreate { get; set; }
    [ForeignKey("Product")]
    public int? ProductId { get; set; }
    public virtual ProductEntity? Product { get; set; }
}
