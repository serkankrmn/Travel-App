using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelApp.Data.DomainClasses;

namespace TravelApp.Data.Mappings
{
    public class SeyahatMapping : IEntityTypeConfiguration<Seyahat>
    {
        public void Configure(EntityTypeBuilder<Seyahat> builder)
        {
            builder.Property(t => t.MiniDescription).HasMaxLength(4000);

            builder.Property(t => t.Header).HasMaxLength(120);

            builder.Property(t => t.ImagePath).HasMaxLength(200);

            builder.Property(t => t.ImagePathSlider).HasMaxLength(200);
        }
    }
}
