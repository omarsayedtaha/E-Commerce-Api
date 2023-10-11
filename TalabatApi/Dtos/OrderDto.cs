using CoreLayer.Entities.Order_Agregate;

namespace TalabatApi.Dtos
{
    public class OrderDto
    {
        public string BasketId { get; set; }
        public int DeliverMethodId { get; set; }
        public AddressDto ShippingAddress { get; set; }
    }
}
