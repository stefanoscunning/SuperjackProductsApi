﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SuperjackProducts.Api.DataAccess
{
  public class ProductTag
  {
    [Column("ProductId")]
    [Required]
    public long ProductId { get; set; }

    [JsonIgnore]
    public Product Product { get; set; }


    [Column("TagId")]
    [Required]
    public long TagId { get; set; }
    public Tag Tag { get; set; }
  }
}
