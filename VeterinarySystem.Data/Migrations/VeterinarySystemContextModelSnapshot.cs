﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VeterinarySystem.Data;

#nullable disable

namespace VeterinarySystem.Data.Migrations
{
    [DbContext(typeof(VeterinarySystemDbContext))]
    partial class VeterinarySystemContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("VeterinarySystem.Data.Domain.Entities.Animal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<int>("AnimalOwnerId")
                        .HasColumnType("int");

                    b.Property<int>("AnimalTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(50)");

                    b.Property<double>("Weight")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("AnimalOwnerId");

                    b.HasIndex("AnimalTypeId");

                    b.ToTable("Animals");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Age = 13,
                            AnimalOwnerId = 1,
                            AnimalTypeId = 2,
                            Name = "Bobo",
                            Weight = 6.0
                        },
                        new
                        {
                            Id = 2,
                            Age = 1,
                            AnimalOwnerId = 1,
                            AnimalTypeId = 1,
                            Name = "Buba",
                            Weight = 3.0
                        });
                });

            modelBuilder.Entity("VeterinarySystem.Data.Domain.Entities.AnimalOwner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasMaxLength(90)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(90)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("Id");

                    b.ToTable("AnimalOwners");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FirstName = "Plamen",
                            LastName = "Pehlivanov",
                            PhoneNumber = "0123456789"
                        });
                });

            modelBuilder.Entity("VeterinarySystem.Data.Domain.Entities.AnimalType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("AnimalTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Cat"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Dog"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Bird"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Livestock"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Transportation Animal"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Other mammal"
                        });
                });

            modelBuilder.Entity("VeterinarySystem.Data.Domain.Entities.Appointment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AnimalOwnerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("AppointmentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("AppointmentDesctiption")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool>("IsUpcoming")
                        .HasColumnType("bit");

                    b.Property<string>("StaffMemberId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("AnimalOwnerId");

                    b.HasIndex("StaffMemberId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("VeterinarySystem.Data.Domain.Entities.Prescription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AnimalId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("IssueDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.Property<string>("StaffMemberId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("AnimalId");

                    b.HasIndex("StaffMemberId");

                    b.ToTable("Prescriptions");
                });

            modelBuilder.Entity("VeterinarySystem.Data.Domain.Entities.PrescriptionCounter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CurrentNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("PrescriptionCounters");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CurrentNumber = 0
                        });
                });

            modelBuilder.Entity("VeterinarySystem.Data.Domain.Entities.Procedure", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AnimalId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("StaffMemberId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("AnimalId");

                    b.HasIndex("StaffMemberId");

                    b.ToTable("Procedures");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AnimalId = 1,
                            Date = new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Extraction of a broken upper-right wisdom tooth that causes infections",
                            Name = "Tooth Extraction",
                            StaffMemberId = "d49db8c9-73e5-4e24-8cb3-80b9ee257ba3"
                        });
                });

            modelBuilder.Entity("VeterinarySystem.Data.Domain.Entities.StaffMember", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsDisabled")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "d49db8c9-73e5-4e24-8cb3-80b9ee257ba3",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "884e5359-93d5-4886-81dc-93d474e5abf2",
                            Email = "steli@vet.com",
                            EmailConfirmed = false,
                            FirstName = "Steliyana",
                            IsDisabled = false,
                            LastName = "Trifonova",
                            LockoutEnabled = false,
                            NormalizedEmail = "STELI@VET.COM",
                            NormalizedUserName = "STELI@VET.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEDFrT+ZD0P14e9aETbZz+kLiVusELwadzuE/Cjq8CRXalRx7yFsg859Mrp23w4zRZQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "02fdd1be-aa48-44bf-93de-42d9892a7664",
                            TwoFactorEnabled = false,
                            UserName = "steli@vet.com"
                        },
                        new
                        {
                            Id = "bcb4f420-ch4d-g1ga-ab26-c069c6f364e",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "c851a8ea-620c-4868-b7c2-e9743acc4916",
                            Email = "chad@admin.com",
                            EmailConfirmed = false,
                            FirstName = "Giga",
                            IsDisabled = false,
                            LastName = "Chad",
                            LockoutEnabled = false,
                            NormalizedEmail = "CHAD@ADMIN.COM",
                            NormalizedUserName = "CHAD@ADMIM.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEPKv5NsoX8ctz15CD9JJKqcjldnTPN7CunhKcjVFQcS7LjY0nF7lfB42iGZXJ9oK9g==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "f88f0e9e-9e22-4c73-be0a-bfd9cf97e402",
                            TwoFactorEnabled = false,
                            UserName = "chad@admin.com"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("VeterinarySystem.Data.Domain.Entities.StaffMember", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("VeterinarySystem.Data.Domain.Entities.StaffMember", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VeterinarySystem.Data.Domain.Entities.StaffMember", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("VeterinarySystem.Data.Domain.Entities.StaffMember", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VeterinarySystem.Data.Domain.Entities.Animal", b =>
                {
                    b.HasOne("VeterinarySystem.Data.Domain.Entities.AnimalOwner", "AnimalOwner")
                        .WithMany("Animals")
                        .HasForeignKey("AnimalOwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VeterinarySystem.Data.Domain.Entities.AnimalType", "AnimalType")
                        .WithMany("Animals")
                        .HasForeignKey("AnimalTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AnimalOwner");

                    b.Navigation("AnimalType");
                });

            modelBuilder.Entity("VeterinarySystem.Data.Domain.Entities.Appointment", b =>
                {
                    b.HasOne("VeterinarySystem.Data.Domain.Entities.AnimalOwner", "AnimalOwner")
                        .WithMany("Appointments")
                        .HasForeignKey("AnimalOwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VeterinarySystem.Data.Domain.Entities.StaffMember", "StaffMember")
                        .WithMany("Appointments")
                        .HasForeignKey("StaffMemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AnimalOwner");

                    b.Navigation("StaffMember");
                });

            modelBuilder.Entity("VeterinarySystem.Data.Domain.Entities.Prescription", b =>
                {
                    b.HasOne("VeterinarySystem.Data.Domain.Entities.Animal", "Animal")
                        .WithMany("Prescriptions")
                        .HasForeignKey("AnimalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VeterinarySystem.Data.Domain.Entities.StaffMember", "StaffMember")
                        .WithMany()
                        .HasForeignKey("StaffMemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Animal");

                    b.Navigation("StaffMember");
                });

            modelBuilder.Entity("VeterinarySystem.Data.Domain.Entities.Procedure", b =>
                {
                    b.HasOne("VeterinarySystem.Data.Domain.Entities.Animal", "Animal")
                        .WithMany("Procedures")
                        .HasForeignKey("AnimalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VeterinarySystem.Data.Domain.Entities.StaffMember", "StaffMember")
                        .WithMany("Procedures")
                        .HasForeignKey("StaffMemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Animal");

                    b.Navigation("StaffMember");
                });

            modelBuilder.Entity("VeterinarySystem.Data.Domain.Entities.Animal", b =>
                {
                    b.Navigation("Prescriptions");

                    b.Navigation("Procedures");
                });

            modelBuilder.Entity("VeterinarySystem.Data.Domain.Entities.AnimalOwner", b =>
                {
                    b.Navigation("Animals");

                    b.Navigation("Appointments");
                });

            modelBuilder.Entity("VeterinarySystem.Data.Domain.Entities.AnimalType", b =>
                {
                    b.Navigation("Animals");
                });

            modelBuilder.Entity("VeterinarySystem.Data.Domain.Entities.StaffMember", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("Procedures");
                });
#pragma warning restore 612, 618
        }
    }
}
