﻿// <auto-generated />
using System;
using FindSelf.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FindSelf.Infrastructure.Migrations
{
    [DbContext(typeof(FindSelfDbContext))]
    [Migration("20201203222657_AddMessageBox2")]
    partial class AddMessageBox2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("FindSelf.Domain.Entities.UserAggregate.MessageBox", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<Guid>("Uid")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("Uid")
                        .IsUnique();

                    b.ToTable("MessageBox");
                });

            modelBuilder.Entity("FindSelf.Domain.Entities.UserAggregate.SiteMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("BoxId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<Guid>("ReceiverId")
                        .HasColumnType("char(36)");

                    b.Property<DateTimeOffset>("SendTimesteamp")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("SnederId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("BoxId");

                    b.ToTable("SiteMessage");
                });

            modelBuilder.Entity("FindSelf.Domain.Entities.UserAggregate.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Nickname")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("FindSelf.Domain.Entities.UserAggregate.MessageBox", b =>
                {
                    b.HasOne("FindSelf.Domain.Entities.UserAggregate.User", null)
                        .WithOne()
                        .HasForeignKey("FindSelf.Domain.Entities.UserAggregate.MessageBox", "Uid")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("FindSelf.Domain.Entities.UserAggregate.SiteMessage", b =>
                {
                    b.HasOne("FindSelf.Domain.Entities.UserAggregate.MessageBox", null)
                        .WithMany("Messages")
                        .HasForeignKey("BoxId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}