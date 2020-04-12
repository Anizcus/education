using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Enums;
using Server.Stores.Entities;

namespace Server.Stores.Configurations
{
   public class TypeConfiguration : IEntityTypeConfiguration<Type>
   {
      public void Configure(EntityTypeBuilder<Type> builder)
      {
         builder.ToTable("type");

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

         builder.Property(e => e.CategoryId)
            .HasColumnName("category_id")
            .HasColumnType("int(10) unsigned");

         builder.HasIndex(e => e.CategoryId).HasName("INDEX_TYPE_CATEGORY_ID");

         builder
            .HasOne(u => u.Category)
               .WithMany(r => r.Types)
            .HasForeignKey(u => u.CategoryId)
            .HasConstraintName("FOREIGN_TYPE_CATEGORY_ID");

         builder.HasData(
            new Type { Name = nameof(TypeEnum.English), Id = (uint) TypeEnum.English, CategoryId = (uint) CategoryEnum.Language },
            new Type { Name = nameof(TypeEnum.Math), Id = (uint) TypeEnum.Math, CategoryId = (uint) CategoryEnum.Science },
            new Type { Name = nameof(TypeEnum.Russian), Id = (uint) TypeEnum.Russian, CategoryId = (uint) CategoryEnum.Language }
         );
      }
   }
}
