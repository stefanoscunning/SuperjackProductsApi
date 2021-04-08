using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SuperjackProducts.Api.DataAccess
{
  public class ProductCategory
  {
    [Column("ProductId")]
    [Required]
    public long ProductId { get; set; }

    [JsonIgnore]
    public Product Product { get; set; }


    [Column("CategoryId")]
    [Required]
    public long CategoryId { get; set; }
    public Category Category { get; set; }
  }
}
