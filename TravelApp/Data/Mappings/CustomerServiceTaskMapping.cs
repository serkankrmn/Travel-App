using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelApp.Data.DomainClasses;

namespace TravelApp.Data.Mappings
{
    public class CustomerServiceTaskMapping : IEntityTypeConfiguration<CustomerServiceTask>
    {
        public void Configure(EntityTypeBuilder<CustomerServiceTask> builder)
        {
            builder.Property(t => t.Description).HasMaxLength(4000);

            builder.Property(t => t.Name).HasMaxLength(100);

            builder.Property(t => t.Email).HasMaxLength(100);
        }
    }
}
