using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperjackProducts.Api.DataAccess
{
  public class Language
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

    [Column("Culture")]
    [StringLength(10)]
    [Required]
    public string Culture { get; set; }
  }
}
