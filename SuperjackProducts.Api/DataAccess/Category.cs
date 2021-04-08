using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SuperjackProducts.Api.DataAccess
{
  public class Category
  {
    
    [Column("Id")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required]
    public long Id { get; set; }

    [Column("Title")]
    [StringLength(150)]
    [Required]
    public string Title { get; set; }

    [JsonIgnore]
    public virtual ICollection<ProductCategory> ProductCategories { get; set; }
  }
}
