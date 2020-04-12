using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Stores.Entities;

namespace Server.Stores.Configurations
{
   public class UserLessonConfiguration : IEntityTypeConfiguration<UserLesson>
   {
      public void Configure(EntityTypeBuilder<UserLesson> builder)
      {
         builder.ToTable("user_lesson");

         builder
            .HasKey(e => new { e.UserId, e.LessonId })
            .HasName("INDEX_USER_LESSON_ID");

         builder
            .HasIndex(e => e.UserId)
            .HasName("INDEX_USER_LESSON_USER_ID");

         builder
            .HasIndex(e => e.LessonId)
            .HasName("INDEX_USER_LESSON_LESSON_ID");

         builder.Property(e => e.UserId)
            .HasColumnName("user_id")
            .HasColumnType("int(10) unsigned");

         builder.Property(e => e.LessonId)
            .HasColumnName("lesson_id")
            .HasColumnType("int(10) unsigned");

         builder.Property(e => e.ProgressId)
            .HasColumnName("progress_id")
            .HasColumnType("int(10) unsigned");

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

         builder
            .HasOne(e => e.Lesson)
            .WithMany(r => r.LessonUsers)
            .HasForeignKey(e => e.LessonId)
            .HasConstraintName("FOREIGN_USER_LESSON_LESSON_ID");

         builder
            .HasOne(e => e.User)
            .WithMany(p => p.UserLessons)
            .HasForeignKey(e => e.UserId)
            .HasConstraintName("FOREIGN_USER_LESSON_USER_ID");

         builder.HasIndex(e => e.ProgressId).HasName("INDEX_USER_LESSON_PROGRESS_ID");

         builder
            .HasOne(u => u.Progress)
               .WithMany(r => r.UserLessons)
            .HasForeignKey(u => u.ProgressId)
            .HasConstraintName("FOREIGN_USER_LESSON_PROGRESS_ID");
      }
   }
}