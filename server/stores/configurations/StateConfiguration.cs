using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Enums;
using Server.Stores.Entities;

namespace Server.Stores.Configurations
{
   public class StateConfiguration : IEntityTypeConfiguration<State>
   {
      public void Configure(EntityTypeBuilder<State> builder)
      {
         builder.ToTable("state");

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
            new State { Name = nameof(StateEnum.Waiting), Id = (uint) StateEnum.Waiting },
            new State { Name = nameof(StateEnum.Accepted), Id = (uint) StateEnum.Accepted },
            new State { Name = nameof(StateEnum.Rejected), Id = (uint) StateEnum.Rejected }
         );
      }
   }
}