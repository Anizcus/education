﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Server.Stores;

namespace server.stores.migrations
{
    [DbContext(typeof(Store))]
    [Migration("20200321235655_fix_timestamps")]
    partial class fix_timestamps
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Server.Stores.Entities.User", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int(10) unsigned");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("create_time")
                        .HasColumnType("timestamp");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar(45)");

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnUpdate()
                        .HasColumnName("update_time")
                        .HasColumnType("timestamp");

                    b.HasKey("Id");

                    b.ToTable("user");
                });
#pragma warning restore 612, 618
        }
    }
}
