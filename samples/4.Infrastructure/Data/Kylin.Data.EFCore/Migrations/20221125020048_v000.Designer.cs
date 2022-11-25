﻿// <auto-generated />
using System;
using Kylin.Data.EFCore.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Kylin.Data.EFCore.Migrations
{
    [DbContext(typeof(KylinContext))]
    [Migration("20221125020048_v000")]
    partial class v000
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Kylin.Data.Models.Auth.Customer", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(10, 2)")
                        .HasColumnName("amount")
                        .HasComment("消费总额");

                    b.Property<long>("CompanyId")
                        .HasColumnType("bigint")
                        .HasColumnName("company_id");

                    b.Property<int>("Count")
                        .HasColumnType("int")
                        .HasColumnName("count")
                        .HasComment("消费次数");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("create_time");

                    b.Property<string>("Creator")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("creator");

                    b.Property<long>("CreatorId")
                        .HasColumnType("bigint")
                        .HasColumnName("creator_id");

                    b.Property<long>("GroupId")
                        .HasColumnType("bigint")
                        .HasColumnName("group_id");

                    b.Property<int>("InCount")
                        .HasColumnType("int")
                        .HasColumnName("in_count")
                        .HasComment("入场次数");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("is_deleted");

                    b.Property<DateTime?>("ReviseTime")
                        .IsRequired()
                        .HasColumnType("datetime(6)")
                        .HasColumnName("revise_time");

                    b.Property<string>("Reviser")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("reviser");

                    b.Property<long>("ReviserId")
                        .HasColumnType("bigint")
                        .HasColumnName("reviser_id");

                    b.Property<long>("StoreId")
                        .HasColumnType("bigint")
                        .HasColumnName("store_id");

                    b.HasKey("Id");

                    b.HasIndex("GroupId", "CompanyId", "StoreId");

                    b.ToTable("customer", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
