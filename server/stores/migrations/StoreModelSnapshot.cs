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

            modelBuilder.Entity("Server.Stores.Entities.Assignment", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int(10) unsigned");

                    b.Property<string>("Answer")
                        .HasColumnName("answer")
                        .HasColumnType("varchar(64)");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("create_time")
                        .HasColumnType("timestamp")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("description")
                        .HasColumnType("varchar(256)");

                    b.Property<uint>("Experience")
                        .HasColumnName("experience")
                        .HasColumnType("int(10) unsigned");

                    b.Property<uint>("LessonId")
                        .HasColumnName("lesson_id")
                        .HasColumnType("int(10) unsigned");

                    b.Property<DateTimeOffset?>("Updated")
                        .ValueGeneratedOnUpdate()
                        .HasColumnName("update_time")
                        .HasColumnType("timestamp")
                        .HasDefaultValueSql("NULL ON UPDATE CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.HasIndex("LessonId")
                        .HasName("INDEX_ASSIGNMENT_LESSON_ID");

                    b.ToTable("assignment");
                });

            modelBuilder.Entity("Server.Stores.Entities.Category", b =>
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

                    b.ToTable("category");
                });

            modelBuilder.Entity("Server.Stores.Entities.Lesson", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int(10) unsigned");

                    b.Property<byte[]>("Badge")
                        .HasColumnName("badge")
                        .HasColumnType("blob");

                    b.Property<uint>("CategoryId")
                        .HasColumnName("category_id")
                        .HasColumnType("int(10) unsigned");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("create_time")
                        .HasColumnType("timestamp")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("description")
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar(64)");

                    b.Property<uint>("OwnerId")
                        .HasColumnName("owner_id")
                        .HasColumnType("int(10) unsigned");

                    b.Property<uint>("StateId")
                        .HasColumnName("state_id")
                        .HasColumnType("int(10) unsigned");

                    b.Property<string>("Status")
                        .HasColumnName("status")
                        .HasColumnType("varchar(256)");

                    b.Property<uint>("TypeId")
                        .HasColumnName("type_id")
                        .HasColumnType("int(10) unsigned");

                    b.Property<DateTimeOffset?>("Updated")
                        .ValueGeneratedOnUpdate()
                        .HasColumnName("update_time")
                        .HasColumnType("timestamp")
                        .HasDefaultValueSql("NULL ON UPDATE CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId")
                        .HasName("INDEX_LESSON_CATEGORY_ID");

                    b.HasIndex("OwnerId")
                        .HasName("INDEX_LESSON_OWNER_ID");

                    b.HasIndex("StateId")
                        .HasName("INDEX_LESSON_STATE_ID");

                    b.HasIndex("TypeId")
                        .HasName("INDEX_LESSON_TYPE_ID");

                    b.ToTable("lesson");
                });

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
                            Id = 5u,
                            Created = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Name = "User.Modify"
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
                        },
                        new
                        {
                            Id = 15u,
                            Created = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Name = "Lesson.Authorize"
                        });
                });

            modelBuilder.Entity("Server.Stores.Entities.Progress", b =>
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

                    b.ToTable("progress");

                    b.HasData(
                        new
                        {
                            Id = 1u,
                            Created = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Name = "Active"
                        },
                        new
                        {
                            Id = 2u,
                            Created = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Name = "Paused"
                        },
                        new
                        {
                            Id = 3u,
                            Created = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Name = "Completed"
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

                    b.HasData(
                        new
                        {
                            RoleId = 1u,
                            PermissionId = 1u,
                            Created = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0))
                        },
                        new
                        {
                            RoleId = 1u,
                            PermissionId = 2u,
                            Created = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0))
                        },
                        new
                        {
                            RoleId = 1u,
                            PermissionId = 3u,
                            Created = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0))
                        },
                        new
                        {
                            RoleId = 1u,
                            PermissionId = 4u,
                            Created = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0))
                        },
                        new
                        {
                            RoleId = 3u,
                            PermissionId = 15u,
                            Created = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0))
                        },
                        new
                        {
                            RoleId = 3u,
                            PermissionId = 5u,
                            Created = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0))
                        },
                        new
                        {
                            RoleId = 2u,
                            PermissionId = 13u,
                            Created = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0))
                        },
                        new
                        {
                            RoleId = 2u,
                            PermissionId = 12u,
                            Created = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0))
                        });
                });

            modelBuilder.Entity("Server.Stores.Entities.State", b =>
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

                    b.ToTable("state");

                    b.HasData(
                        new
                        {
                            Id = 2u,
                            Created = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Name = "Waiting"
                        },
                        new
                        {
                            Id = 3u,
                            Created = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Name = "Published"
                        },
                        new
                        {
                            Id = 4u,
                            Created = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Name = "Rejected"
                        },
                        new
                        {
                            Id = 1u,
                            Created = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Name = "Created"
                        });
                });

            modelBuilder.Entity("Server.Stores.Entities.Type", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int(10) unsigned");

                    b.Property<uint>("CategoryId")
                        .HasColumnName("category_id")
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

                    b.HasIndex("CategoryId")
                        .HasName("INDEX_TYPE_CATEGORY_ID");

                    b.ToTable("type");
                });

            modelBuilder.Entity("Server.Stores.Entities.User", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int(10) unsigned");

                    b.Property<bool>("Blocked")
                        .HasColumnName("blocked")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("create_time")
                        .HasColumnType("timestamp")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<uint>("Experience")
                        .HasColumnName("experience")
                        .HasColumnType("int(10) unsigned");

                    b.Property<uint>("Level")
                        .HasColumnName("level")
                        .HasColumnType("int(10) unsigned");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar(64)");

                    b.Property<byte[]>("Password")
                        .IsRequired()
                        .HasColumnName("password")
                        .HasColumnType("tinyblob");

                    b.Property<uint>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("role_id")
                        .HasColumnType("int(10) unsigned")
                        .HasDefaultValue(1u);

                    b.Property<byte[]>("Salt")
                        .IsRequired()
                        .HasColumnName("salt")
                        .HasColumnType("tinyblob");

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

            modelBuilder.Entity("Server.Stores.Entities.UserAssignment", b =>
                {
                    b.Property<uint>("UserId")
                        .HasColumnName("user_id")
                        .HasColumnType("int(10) unsigned");

                    b.Property<uint>("AssignmentId")
                        .HasColumnName("assignment_id")
                        .HasColumnType("int(10) unsigned");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("create_time")
                        .HasColumnType("timestamp")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<uint>("ProgressId")
                        .HasColumnName("progress_id")
                        .HasColumnType("int(10) unsigned");

                    b.Property<DateTimeOffset?>("Updated")
                        .ValueGeneratedOnUpdate()
                        .HasColumnName("update_time")
                        .HasColumnType("timestamp")
                        .HasDefaultValueSql("NULL ON UPDATE CURRENT_TIMESTAMP");

                    b.HasKey("UserId", "AssignmentId")
                        .HasName("INDEX_USER_ASSIGNMENT_ID");

                    b.HasIndex("AssignmentId")
                        .HasName("INDEX_USER_ASSIGNMENT_ASSIGNMENT_ID");

                    b.HasIndex("ProgressId")
                        .HasName("INDEX_USER_ASSIGNMENT_PROGRESS_ID");

                    b.HasIndex("UserId")
                        .HasName("INDEX_USER_ASSIGNMENT_USER_ID");

                    b.ToTable("user_assignment");
                });

            modelBuilder.Entity("Server.Stores.Entities.UserLesson", b =>
                {
                    b.Property<uint>("UserId")
                        .HasColumnName("user_id")
                        .HasColumnType("int(10) unsigned");

                    b.Property<uint>("LessonId")
                        .HasColumnName("lesson_id")
                        .HasColumnType("int(10) unsigned");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("create_time")
                        .HasColumnType("timestamp")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<uint>("ProgressId")
                        .HasColumnName("progress_id")
                        .HasColumnType("int(10) unsigned");

                    b.Property<DateTimeOffset?>("Updated")
                        .ValueGeneratedOnUpdate()
                        .HasColumnName("update_time")
                        .HasColumnType("timestamp")
                        .HasDefaultValueSql("NULL ON UPDATE CURRENT_TIMESTAMP");

                    b.HasKey("UserId", "LessonId")
                        .HasName("INDEX_USER_LESSON_ID");

                    b.HasIndex("LessonId")
                        .HasName("INDEX_USER_LESSON_LESSON_ID");

                    b.HasIndex("ProgressId")
                        .HasName("INDEX_USER_LESSON_PROGRESS_ID");

                    b.HasIndex("UserId")
                        .HasName("INDEX_USER_LESSON_USER_ID");

                    b.ToTable("user_lesson");
                });

            modelBuilder.Entity("Server.Stores.Entities.Assignment", b =>
                {
                    b.HasOne("Server.Stores.Entities.Lesson", "Lesson")
                        .WithMany("Assignments")
                        .HasForeignKey("LessonId")
                        .HasConstraintName("FOREIGN_ASSIGNMENT_LESSON_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Server.Stores.Entities.Lesson", b =>
                {
                    b.HasOne("Server.Stores.Entities.Category", "Category")
                        .WithMany("Lessons")
                        .HasForeignKey("CategoryId")
                        .HasConstraintName("FOREIGN_LESSON_CATEGORY_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Server.Stores.Entities.User", "Owner")
                        .WithMany("Lessons")
                        .HasForeignKey("OwnerId")
                        .HasConstraintName("FOREIGN_LESSON_OWNER_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Server.Stores.Entities.State", "State")
                        .WithMany("Lessons")
                        .HasForeignKey("StateId")
                        .HasConstraintName("FOREIGN_LESSON_STATE_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Server.Stores.Entities.Type", "Type")
                        .WithMany("Lessons")
                        .HasForeignKey("TypeId")
                        .HasConstraintName("FOREIGN_LESSON_TYPE_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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

            modelBuilder.Entity("Server.Stores.Entities.Type", b =>
                {
                    b.HasOne("Server.Stores.Entities.Category", "Category")
                        .WithMany("Types")
                        .HasForeignKey("CategoryId")
                        .HasConstraintName("FOREIGN_TYPE_CATEGORY_ID")
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

            modelBuilder.Entity("Server.Stores.Entities.UserAssignment", b =>
                {
                    b.HasOne("Server.Stores.Entities.Assignment", "Assignment")
                        .WithMany("AssignmentUsers")
                        .HasForeignKey("AssignmentId")
                        .HasConstraintName("FOREIGN_USER_ASSIGNMENT_ASSIGNMENT_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Server.Stores.Entities.Progress", "Progress")
                        .WithMany("UserAssignments")
                        .HasForeignKey("ProgressId")
                        .HasConstraintName("FOREIGN_USER_ASSIGNMENT_PROGRESS_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Server.Stores.Entities.User", "User")
                        .WithMany("UserAssignments")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FOREIGN_USER_ASSIGNMENT_USER_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Server.Stores.Entities.UserLesson", b =>
                {
                    b.HasOne("Server.Stores.Entities.Lesson", "Lesson")
                        .WithMany("LessonUsers")
                        .HasForeignKey("LessonId")
                        .HasConstraintName("FOREIGN_USER_LESSON_LESSON_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Server.Stores.Entities.Progress", "Progress")
                        .WithMany("UserLessons")
                        .HasForeignKey("ProgressId")
                        .HasConstraintName("FOREIGN_USER_LESSON_PROGRESS_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Server.Stores.Entities.User", "User")
                        .WithMany("UserLessons")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FOREIGN_USER_LESSON_USER_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
