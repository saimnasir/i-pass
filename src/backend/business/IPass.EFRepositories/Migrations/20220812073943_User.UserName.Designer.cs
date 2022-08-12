﻿// <auto-generated />
using System;
using IPass.EFRepositories.IPassContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace IPass.EFRepositories.Migrations
{
    [DbContext(typeof(MyMemoryDbContext))]
    [Migration("20220812073943_User.UserName")]
    partial class UserUserName
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("MEM")
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("IPass.Domain.CommonDomain.Entities.PinCode", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<long>("Expiration")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("PinCodes", "MEM");
                });

            modelBuilder.Entity("IPass.Domain.CommonDomain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastSeen")
                        .HasColumnType("datetime2");

                    b.Property<string>("PhotoId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users", "MEM");
                });

            modelBuilder.Entity("IPass.Domain.PasswordDomain.Entities.EnvironmentType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("EnvironmentTypes", "MEM");
                });

            modelBuilder.Entity("IPass.Domain.PasswordDomain.Entities.Memory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("EnvironmentTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("HostOrIpAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsHostOrIpAddressSecure")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPortSecure")
                        .HasColumnType("bit");

                    b.Property<bool>("IsUEmailSecure")
                        .HasColumnType("bit");

                    b.Property<bool>("IsUserNameSecure")
                        .HasColumnType("bit");

                    b.Property<Guid>("MemoryTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Port")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ValidationEndAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasColumnName("ValidationEndAt");

                    b.Property<DateTime>("ValidationStartAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasColumnName("ValidationStartAt");

                    b.HasKey("Id");

                    b.HasIndex("EnvironmentTypeId");

                    b.HasIndex("MemoryTypeId");

                    b.HasIndex("OrganizationId");

                    b.ToTable("Memories", "MEM");

                    b.ToTable(tb => tb.IsTemporal(ttb =>
                        {
                            ttb.UseHistoryTable("MemoryHistories");
                            ttb
                                .HasPeriodStart("ValidationStartAt")
                                .HasColumnName("ValidationStartAt");
                            ttb
                                .HasPeriodEnd("ValidationEndAt")
                                .HasColumnName("ValidationEndAt");
                        }
                    ));
                });

            modelBuilder.Entity("IPass.Domain.PasswordDomain.Entities.MemoryType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("MemoryTypes", "MEM");
                });

            modelBuilder.Entity("IPass.Domain.PasswordDomain.Entities.Organization", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<Guid>("OrganizationTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ParentOrganizationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationTypeId");

                    b.HasIndex("ParentOrganizationId");

                    b.ToTable("Organizations", "MEM");
                });

            modelBuilder.Entity("IPass.Domain.PasswordDomain.Entities.OrganizationType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("OrganizationTypes", "MEM");
                });

            modelBuilder.Entity("Patika.Shared.Entities.Loggy.Entities.Log", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("ApplicationName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("EndDateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Logs", "LOG");
                });

            modelBuilder.Entity("Patika.Shared.Entities.Loggy.Entities.LogDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InputAsJson")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("LogId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("LogType")
                        .HasColumnType("int");

                    b.Property<string>("Method")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Module")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OutputAsJson")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("LogId");

                    b.ToTable("LogDetails", "LOG");
                });

            modelBuilder.Entity("IPass.Domain.CommonDomain.Entities.PinCode", b =>
                {
                    b.HasOne("IPass.Domain.CommonDomain.Entities.User", null)
                        .WithMany("PinCodes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("IPass.Domain.CommonDomain.Entities.User", b =>
                {
                    b.OwnsMany("IPass.Domain.CommonDomain.Entities.OTPHistory", "OTPHistories", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"), 1L, 1);

                            b1.Property<DateTime>("SentAt")
                                .HasColumnType("datetime2");

                            b1.HasKey("UserId", "Id");

                            b1.ToTable("OTPHistory", "MEM");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("OTPHistories");
                });

            modelBuilder.Entity("IPass.Domain.PasswordDomain.Entities.Memory", b =>
                {
                    b.HasOne("IPass.Domain.PasswordDomain.Entities.EnvironmentType", "EnvironmentType")
                        .WithMany()
                        .HasForeignKey("EnvironmentTypeId");

                    b.HasOne("IPass.Domain.PasswordDomain.Entities.MemoryType", "MemoryType")
                        .WithMany()
                        .HasForeignKey("MemoryTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IPass.Domain.PasswordDomain.Entities.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EnvironmentType");

                    b.Navigation("MemoryType");

                    b.Navigation("Organization");
                });

            modelBuilder.Entity("IPass.Domain.PasswordDomain.Entities.Organization", b =>
                {
                    b.HasOne("IPass.Domain.PasswordDomain.Entities.OrganizationType", "OrganizationType")
                        .WithMany("Organizations")
                        .HasForeignKey("OrganizationTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IPass.Domain.PasswordDomain.Entities.Organization", "ParentOrganization")
                        .WithMany()
                        .HasForeignKey("ParentOrganizationId");

                    b.Navigation("OrganizationType");

                    b.Navigation("ParentOrganization");
                });

            modelBuilder.Entity("Patika.Shared.Entities.Loggy.Entities.LogDetail", b =>
                {
                    b.HasOne("Patika.Shared.Entities.Loggy.Entities.Log", "Log")
                        .WithMany("Details")
                        .HasForeignKey("LogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Log");
                });

            modelBuilder.Entity("IPass.Domain.CommonDomain.Entities.User", b =>
                {
                    b.Navigation("PinCodes");
                });

            modelBuilder.Entity("IPass.Domain.PasswordDomain.Entities.OrganizationType", b =>
                {
                    b.Navigation("Organizations");
                });

            modelBuilder.Entity("Patika.Shared.Entities.Loggy.Entities.Log", b =>
                {
                    b.Navigation("Details");
                });
#pragma warning restore 612, 618
        }
    }
}
