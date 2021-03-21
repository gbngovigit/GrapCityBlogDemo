

namespace Infrastructure.Persistence.Configurations
{
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    public class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasKey(k => k.Id);
            builder.Property(k => k.Id).ValueGeneratedOnAdd();
            builder.Property(t => t.Title).HasMaxLength(200).IsRequired();
            builder.Property(t => t.Abstract).HasMaxLength(250).IsRequired();
            builder.Property(t => t.Contents).IsRequired();
            

        }
    }
}
