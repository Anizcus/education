﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Server.Stores;

namespace server.stores.migrations
{
    [DbContext(typeof(Store))]
    partial class StoreModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Server.Stores.Entities.Permission", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int(10) unsigned");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("create_time")
                        .HasColumnType("timestamp")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar(32)");

                    b.Property<DateTimeOffset?>("Updated")
                        .ValueGeneratedOnUpdate()
                        .HasColumnName("update_time")
                        .HasColumnType("timestamp")
                        .HasDefaultValueSql("NULL ON UPDATE CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.ToTable("permission");

                    b.HasData(
                        new
                        {
                            Id = 1u,
                            Created = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Name = "User.View"
                        },
                        new
                        {
                            Id = 2u,
                            Created = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Name = "User.Create"
                        },
                        new
                        {
                            Id = 3u,
                            Created = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Name = "User.Update"
                        },
                        new
                        {
                            Id = 4u,
                            Created = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Name = "User.Delete"
                        },
                        new
                        {
                            Id = 11u,
                            Created = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Name = "Lesson.View"
                        },
                        new
                        {
                            Id = 12u,
                            Created = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Name = "Lesson.Create"
                        },
                        new
                        {
                            Id = 13u,
                            Created = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Name = "Lesson.Update"
                        },
                        new
                        {
                            Id = 14u,
                            Created = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Name = "Lesson.Delete"
                        });
                });

            modelBuilder.Entity("Server.Stores.Entities.Role", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int(10) unsigned");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("create_time")
                        .HasColumnType("timestamp")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar(32)");

                    b.Property<DateTimeOffset?>("Updated")
                        .ValueGeneratedOnUpdate()
                        .HasColumnName("update_time")
                        .HasColumnType("timestamp")
                        .HasDefaultValueSql("NULL ON UPDATE CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.ToTable("role");

                    b.HasData(
                        new
                        {
                            Id = 1u,
                            Created = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Name = "Student"
                        },
                        new
                        {
                            Id = 2u,
                            Created = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Name = "Teacher"
                        },
                        new
                        {
                            Id = 3u,
                            Created = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Name = "Administrator"
                        });
                });

            modelBuilder.Entity("Server.Stores.Entities.RolePermission", b =>
                {
                    b.Property<uint>("RoleId")
                        .HasColumnName("role_id")
                        .HasColumnType("int(10) unsigned");

                    b.Property<uint>("PermissionId")
                        .HasColumnName("permission_id")
                        .HasColumnType("int(10) unsigned");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("create_time")
                        .HasColumnType("timestamp")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<DateTimeOffset?>("Updated")
                        .ValueGeneratedOnUpdate()
                        .HasColumnName("update_time")
                        .HasColumnType("timestamp")
                        .HasDefaultValueSql("NULL ON UPDATE CURRENT_TIMESTAMP");

                    b.HasKey("RoleId", "PermissionId")
                        .HasName("INDEX_ROLE_PERMISSION_ID");

                    b.HasIndex("PermissionId")
                        .HasName("INDEX_ROLE_PERMISSION_PERMISSION_ID");

                    b.HasIndex("RoleId")
                        .HasName("INDEX_ROLE_PERMISSION_ROLE_ID");

                    b.ToTable("role_permission");
                });

            modelBuilder.Entity("Server.Stores.Entities.User", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int(10) unsigned");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("create_time")
                        .HasColumnType("timestamp")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar(64)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnName("password")
                        .HasColumnType("varchar(64)");

                    b.Property<uint>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("role_id")
                        .HasColumnType("int(10) unsigned")
                        .HasDefaultValue(1u);

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnName("salt")
                        .HasColumnType("varchar(64)");

                    b.Property<DateTimeOffset?>("Updated")
                        .ValueGeneratedOnUpdate()
                        .HasColumnName("update_time")
                        .HasColumnType("timestamp")
                        .HasDefaultValueSql("NULL ON UPDATE CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.HasIndex("RoleId")
                        .HasName("INDEX_USER_ROLE_ID");

                    b.ToTable("user");
                });

            modelBuilder.Entity("Server.Stores.Entities.RolePermission", b =>
                {
                    b.HasOne("Server.Stores.Entities.Permission", "Permission")
                        .WithMany("PermissionRoles")
                        .HasForeignKey("PermissionId")
                        .HasConstraintName("FOREIGN_ROLE_PERMISSION_PERMISSION_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Server.Stores.Entities.Role", "Role")
                        .WithMany("RolePermissions")
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FOREIGN_ROLE_PERMISSION_ROLE_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Server.Stores.Entities.User", b =>
                {
                    b.HasOne("Server.Stores.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FOREIGN_USER_ROLE_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
