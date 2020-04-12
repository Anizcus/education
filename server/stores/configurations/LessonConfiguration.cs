using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Stores.Entities;

namespace Server.Stores.Configurations
{
   public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
   {
      public void Configure(EntityTypeBuilder<Lesson> builder)
      {
         builder.ToTable("lesson");

         builder.HasKey(e => e.Id);

         builder.Property(e => e.Id)
            .HasColumnName("id")
            .HasColumnType("int(10) unsigned");

         builder.Property(e => e.Name)
            .HasColumnName("name")
            .HasColumnType("varchar(64)");

         builder.Property(e => e.Description)
            .HasColumnName("description")
            .HasColumnType("varchar(256)")
            .IsRequired();

         builder.Property(e => e.Badge)
            .HasColumnName("badge")
            .HasColumnType("tinyblob");

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

         builder.HasIndex(e => e.CategoryId).HasName("INDEX_LESSON_CATEGORY_ID");

         builder.Property(e => e.TypeId)
            .HasColumnName("type_id")
            .HasColumnType("int(10) unsigned");

         builder.HasIndex(e => e.TypeId).HasName("INDEX_LESSON_TYPE_ID");

         builder.Property(e => e.OwnerId)
            .HasColumnName("owner_id")
            .HasColumnType("int(10) unsigned");

         builder.HasIndex(e => e.OwnerId).HasName("INDEX_LESSON_OWNER_ID");

         builder.Property(e => e.StatusId)
            .HasColumnName("status_id")
            .HasColumnType("int(10) unsigned");

         builder.HasIndex(e => e.StatusId).HasName("INDEX_LESSON_STATUS_ID");

         builder
            .HasOne(u => u.Category)
               .WithMany(r => r.Lessons)
            .HasForeignKey(u => u.CategoryId)
            .HasConstraintName("FOREIGN_LESSON_CATEGORY_ID");

         builder
            .HasOne(u => u.Type)
               .WithMany(r => r.Lessons)
            .HasForeignKey(u => u.TypeId)
            .HasConstraintName("FOREIGN_LESSON_TYPE_ID");

         builder
            .HasOne(u => u.Owner)
               .WithMany(r => r.Lessons)
            .HasForeignKey(u => u.OwnerId)
            .HasConstraintName("FOREIGN_LESSON_OWNER_ID");

         builder
            .HasOne(u => u.Status)
               .WithMany(r => r.Lessons)
            .HasForeignKey(u => u.StatusId)
            .HasConstraintName("FOREIGN_LESSON_STATUS_ID");
      }
   }
}
