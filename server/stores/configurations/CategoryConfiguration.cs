using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Enums;
using Server.Stores.Entities;

namespace Server.Stores.Configurations
{
   public class CategoryConfiguration : IEntityTypeConfiguration<Category>
   {
      public void Configure(EntityTypeBuilder<Category> builder)
      {
         builder.ToTable("category");

         builder.HasKey(e => e.Id);

         builder.Property(e => e.Id)
            .HasColumnName("id")
            .HasColumnType("int(10) unsigned");

         builder.Property(e => e.Name)
            .HasColumnName("name")
            .HasColumnType("varchar(32)")
            .IsRequired();

         builder.Property(e => e.Created)
            .HasColumnName("create_time")
            .HasColumnType("timestamp")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .ValueGeneratedOnAdd();

         builder.Property(e => e.Updated)
            .HasColumnName("update_time")
            .HasColumnType("timestamp")
            .HasDefaultValueSql("NULL ON UPDATE CURRENT_TIMESTAMP")
            .ValueGeneratedOnUpdate();

         builder.HasData(
            new Category { Name = nameof(CategoryEnum.Language), Id = (uint) CategoryEnum.Language },
            new Category { Name = nameof(CategoryEnum.Programming), Id = (uint) CategoryEnum.Programming },
            new Category { Name = nameof(CategoryEnum.Science), Id = (uint) CategoryEnum.Science }
         );
      }
   }
}