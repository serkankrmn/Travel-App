using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TravelApp.Data.DomainClasses;

namespace TravelApp.Data.Mappings
{
   
    public class CustomerServiceMapping : IEntityTypeConfiguration<CustomerService>
    {
        public void Configure(EntityTypeBuilder<CustomerService> builder)
        {
            builder.Property(t => t.Name).HasMaxLength(400);

            builder.Property(t => t.Email).HasMaxLength(400);

        }
    }
}
