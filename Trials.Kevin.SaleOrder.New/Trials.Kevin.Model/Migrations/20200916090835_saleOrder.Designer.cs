﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Trials.Kevin.Model;

namespace Trials.Kevin.Model.Migrations
{
    [DbContext(typeof(SaleOrderContext))]
    [Migration("20200916090835_saleOrder")]
    partial class saleOrder
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Trials.Kevin.Model.SaleOrderDB.SaleOrderDetailEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime");

                    b.Property<string>("CreateUserNo")
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("IsDeleted")
                        .HasColumnType("int");

                    b.Property<string>("MaterialNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)");

                    b.Property<double>("Num")
                        .HasColumnType("double");

                    b.Property<string>("OrderNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)");

                    b.Property<long>("PId")
                        .HasColumnType("bigint");

                    b.Property<int>("ProjectNo")
                        .HasColumnType("int");

                    b.Property<string>("Remark")
                        .HasColumnType("nvarchar(2000)");

                    b.Property<int>("SortNo")
                        .HasColumnType("int");

                    b.Property<string>("Unit")
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("datetime");

                    b.Property<string>("UpdateUserNo")
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("PId");

                    b.ToTable("Kevin.SaleOrder.OrderDetail");
                });

            modelBuilder.Entity("Trials.Kevin.Model.SaleOrderDB.SaleOrderEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime");

                    b.Property<string>("CreateUserNo")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Customer")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("IsDeleted")
                        .HasColumnType("int");

                    b.Property<string>("OrderNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Remark")
                        .HasColumnType("nvarchar(2000)");

                    b.Property<DateTime>("SignDate")
                        .HasColumnType("datetime");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("datetime");

                    b.Property<string>("UpdateUserNo")
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Kevin.SaleOrder.Order");
                });

            modelBuilder.Entity("Trials.Kevin.Model.SaleOrderDB.SaleOrderDetailEntity", b =>
                {
                    b.HasOne("Trials.Kevin.Model.SaleOrderDB.SaleOrderEntity", "OrderEntity")
                        .WithMany("SaleOrderDetailEntities")
                        .HasForeignKey("PId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
