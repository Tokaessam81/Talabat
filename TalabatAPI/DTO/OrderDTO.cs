using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Talabat.Core.Order_Aggregate;

namespace TalabatAPI.DTO
{
    public class OrderDTO
    {
        [Required]
        public string BuyerEmail { get; set; }
        [Required]
        public string basketId { get; set; }
        [Required]
        public AddressDTO shippingAddress { get; set; }
 
        [Required]
        public int DeliveryMethodId { get; set; }
       
    }
}