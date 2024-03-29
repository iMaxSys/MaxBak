﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using iMaxSys.Core.Data.EFCore;

#nullable disable

namespace iMaxSys.Core.Migrations
{
    [DbContext(typeof(CoreContext))]
    [Migration("20230306023728_v000")]
    partial class v000
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("iMaxSys.Core.Data.Entities.Dict", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<string>("Alias")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("alias")
                        .HasComment("别名");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("code")
                        .HasComment("编号");

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

                    b.Property<int>("DataType")
                        .HasColumnType("int")
                        .HasColumnName("data_type")
                        .HasComment("数据类型");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("description")
                        .HasComment("描述");

                    b.Property<bool>("Editable")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("editable")
                        .HasComment("可编辑");

                    b.Property<string>("Image")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("image")
                        .HasComment("图像");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("is_deleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("name")
                        .HasComment("名称");

                    b.Property<int>("Ordinal")
                        .HasColumnType("int")
                        .HasColumnName("ordinal")
                        .HasComment("序号");

                    b.Property<string>("QuickCode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("quick_code")
                        .HasComment("速查码");

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

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasColumnName("status")
                        .HasComment("状态");

                    b.Property<string>("Style")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("style")
                        .HasComment("style");

                    b.Property<long>("TenantId")
                        .HasColumnType("bigint")
                        .HasColumnName("tenant_id");

                    b.Property<string>("Thumbnail")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("thumbnail")
                        .HasComment("缩略图");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("value")
                        .HasComment("value");

                    b.Property<long>("XppId")
                        .HasColumnType("bigint")
                        .HasColumnName("xpp_id")
                        .HasComment("XppId");

                    b.HasKey("Id");

                    b.HasIndex("TenantId");

                    b.HasIndex("Name", "QuickCode", "Code");

                    b.ToTable("dict", (string)null);
                });

            modelBuilder.Entity("iMaxSys.Core.Data.Entities.DictItem", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<string>("Alias")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("alias")
                        .HasComment("别名");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("code")
                        .HasComment("编号");

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

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("description")
                        .HasComment("描述");

                    b.Property<long>("DictId")
                        .HasColumnType("bigint")
                        .HasColumnName("dict_id")
                        .HasComment("字典id");

                    b.Property<bool>("Editable")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("editable")
                        .HasComment("可编辑");

                    b.Property<string>("Image")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("image")
                        .HasComment("图像");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("is_deleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("name")
                        .HasComment("名称");

                    b.Property<int>("Ordinal")
                        .HasColumnType("int")
                        .HasColumnName("ordinal")
                        .HasComment("序号");

                    b.Property<string>("QuickCode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("quick_code")
                        .HasComment("速查码");

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

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasColumnName("status")
                        .HasComment("状态");

                    b.Property<string>("Style")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("style")
                        .HasComment("style");

                    b.Property<long>("TenantId")
                        .HasColumnType("bigint")
                        .HasColumnName("tenant_id");

                    b.Property<string>("Thumbnail")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("thumbnail")
                        .HasComment("缩略图");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("value")
                        .HasComment("value");

                    b.HasKey("Id");

                    b.HasIndex("DictId");

                    b.HasIndex("TenantId");

                    b.ToTable("dict_item", (string)null);
                });

            modelBuilder.Entity("iMaxSys.Core.Data.Entities.Tenant", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<string>("Address")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("address");

                    b.Property<string>("Alias")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("alias");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("code");

                    b.Property<string>("Contact")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("contact");

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

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("description");

                    b.Property<DateTime>("End")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("end");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("is_deleted");

                    b.Property<int>("Level")
                        .HasColumnType("int")
                        .HasColumnName("level");

                    b.Property<string>("License")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("license");

                    b.Property<string>("Logo")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("logo");

                    b.Property<string>("Mail")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("mail");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("name");

                    b.Property<string>("Phone")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("phone");

                    b.Property<string>("QuickCode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("quick_code");

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

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("start");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasColumnName("status");

                    b.HasKey("Id");

                    b.HasIndex("Name", "Code");

                    b.ToTable("tenant", (string)null);
                });

            modelBuilder.Entity("iMaxSys.Core.Data.Entities.Xpp", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<string>("AccountId")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("account_id");

                    b.Property<string>("Alias")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("alias");

                    b.Property<string>("AppId")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("app_id");

                    b.Property<string>("AppKey")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("app_key");

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

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("description");

                    b.Property<string>("Host")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("host");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("is_deleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("name");

                    b.Property<bool>("NeedMobile")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("need_mobile");

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

                    b.Property<int>("Source")
                        .HasColumnType("int")
                        .HasColumnName("source");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasColumnName("status");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.ToTable("xpp", (string)null);
                });

            modelBuilder.Entity("iMaxSys.Core.Data.Entities.XppSns", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<string>("AccountId")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("account_id");

                    b.Property<string>("Alias")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("alias");

                    b.Property<string>("AppId")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("app_id");

                    b.Property<string>("AppKey")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("app_key");

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

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("description");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("is_deleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("name");

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

                    b.Property<int>("Source")
                        .HasColumnType("int")
                        .HasColumnName("source");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasColumnName("status");

                    b.Property<long>("XppId")
                        .HasColumnType("bigint")
                        .HasColumnName("xpp_id");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.HasIndex("XppId");

                    b.ToTable("xpp_sns", (string)null);
                });

            modelBuilder.Entity("iMaxSys.Core.Data.Entities.DictItem", b =>
                {
                    b.HasOne("iMaxSys.Core.Data.Entities.Dict", "Dict")
                        .WithMany("DictItems")
                        .HasForeignKey("DictId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Dict");
                });

            modelBuilder.Entity("iMaxSys.Core.Data.Entities.XppSns", b =>
                {
                    b.HasOne("iMaxSys.Core.Data.Entities.Xpp", "Xpp")
                        .WithMany("XppSnses")
                        .HasForeignKey("XppId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Xpp");
                });

            modelBuilder.Entity("iMaxSys.Core.Data.Entities.Dict", b =>
                {
                    b.Navigation("DictItems");
                });

            modelBuilder.Entity("iMaxSys.Core.Data.Entities.Xpp", b =>
                {
                    b.Navigation("XppSnses");
                });
#pragma warning restore 612, 618
        }
    }
}
