using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedNever();

        builder.Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(80);

        builder.Property(p => p.Content)
            .IsRequired();

        builder.HasOne(p => p.Author)       
            .WithMany()
            .HasForeignKey(p => p.IdAuthor) 
            .IsRequired()                
            .OnDelete(DeleteBehavior.Cascade); // if I delete User, it will delete him Postsf
        
    }
}