﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models.Dtos
{
    public class CartDetailDto
    {
        [Required(ErrorMessage = "UserId không được để trống")]
        public string UserId { get; set; }
        public int ProductId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
        public int Quantity { get; set; }
        [Column(TypeName = "DECIMAL(18,2)")]
        public decimal Total { get; }
    }
}
