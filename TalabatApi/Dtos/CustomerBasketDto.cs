using CoreLayer.Entities.Basket_Module;
using System.ComponentModel.DataAnnotations;

namespace TalabatApi.Dtos
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public List<BasketItemDto> Items { get; set; }
    }
}
