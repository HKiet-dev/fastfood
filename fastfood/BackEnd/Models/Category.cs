﻿using System.ComponentModel.DataAnnotations;

namespace BackEnd.Models
{
    public class Category
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
        public List<Product>? Products { get; set; }
    }
}
