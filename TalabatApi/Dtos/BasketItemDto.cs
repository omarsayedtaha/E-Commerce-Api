﻿using System.ComponentModel.DataAnnotations;

namespace TalabatApi.Dtos
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }

        [Required]
        public string ProductPictureUrl { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be one item at least ")]
        public int Quantity { get; set; }

        [Required]
        [Range(0.1, double.MaxValue , ErrorMessage = "Price must be greater than zero ")]
        public int Price { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public string Type { get; set; }

    }
}
