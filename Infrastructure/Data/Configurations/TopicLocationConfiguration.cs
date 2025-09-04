
namespace Infrastructure.Data.Configurations
{
    public class TopicLocationConfiguration : IEntityTypeConfiguration<Topic>
    {
        public void Configure(EntityTypeBuilder<Topic> builder)
        {

            builder.OwnsOne(topic => topic.Location, location =>
                {
                    location.Property(loc => loc.City).HasColumnName("City");
                    location.Property(loc => loc.Street).HasColumnName("Street");
                });
        }
    }
}
