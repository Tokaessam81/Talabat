using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Order_Aggregate;

namespace Talabat.Repository.Data.Configrations
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(s => s.Status)
                   .HasConversion((s => s.ToString()), (s => (OrderStatus)Enum.Parse(typeof(OrderStatus), s)));

            builder.OwnsOne(o => o.shippingAddress,a=>a.WithOwner());
            
            builder.Property(o => o.subTotal)
                   .HasColumnType("decimal(18,2)");
            builder.HasOne(o => o.deliveryMethod)
                   .WithMany()
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
