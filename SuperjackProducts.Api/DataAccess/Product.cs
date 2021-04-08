using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperjackProducts.Api.DataAccess
{
  public class Product
  {

    [Column("Id")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required]
    public long Id { get; set; }

    [Column("Name")]
    [StringLength(150)]
    [Required]
    public string Name { get; set; }

    [Column("ManufacturerId")]
    [Required]
    public long ManufacturerId { get; set; }

    [ForeignKey("ManufacturerId")]
    public Manufacturer Manufacturer { get; set; }

    public virtual ICollection<ProductCategory> ProductCategories { get; set; }
    public virtual ICollection<ProductTag> ProductTags { get; set; }

  }
}
