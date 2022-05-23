﻿// <auto-generated />
using LinkShortener.Dal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LinkShortener.Dal.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20220523155611_SeedInitialUser")]
    partial class SeedInitialUser
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("LinkShortener.Dal.Entities.ShortedLinkEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("OriginalLink")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("original_link");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.Property<string>("ShortLink")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("short_link");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("shorted_links");
                });

            modelBuilder.Entity("LinkShortener.Dal.Entities.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("login");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("password");

                    b.HasKey("Id");

                    b.ToTable("users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Login = "initialUser",
                            Password = "pass"
                        });
                });

            modelBuilder.Entity("LinkShortener.Dal.Entities.ShortedLinkEntity", b =>
                {
                    b.HasOne("LinkShortener.Dal.Entities.UserEntity", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("LinkShortener.Dal.Entities.UserEntity", b =>
                {
                    b.OwnsOne("LinkShortener.Dal.Entities.BalanceEntity", "Balance", b1 =>
                        {
                            b1.Property<int>("OwnerId")
                                .HasColumnType("int");

                            b1.Property<decimal>("Balance")
                                .HasColumnType("DECIMAL(65,30)")
                                .HasColumnName("balance");

                            b1.HasKey("OwnerId");

                            b1.ToTable("balance");

                            b1.WithOwner("Owner")
                                .HasForeignKey("OwnerId");

                            b1.Navigation("Owner");

                            b1.HasData(
                                new
                                {
                                    OwnerId = 1,
                                    Balance = 1000m
                                });
                        });

                    b.Navigation("Balance")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
