﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Orders.Data;

namespace Orders.Migrations
{
    [DbContext(typeof(OrderContext))]
    [Migration("20191018065611_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("gunnebo.Data.Order", b =>
                {
                    b.Property<long>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("OrderDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<long>("OrderNumber")
                        .HasColumnType("bigint");

                    b.Property<long>("OrderRegistrationNumber")
                        .HasColumnType("bigint");

                    b.Property<string>("OrderStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OrderId");

                    b.HasIndex("OrderNumber")
                        .IsUnique()
                        .HasName("Index_OrderNumber");

                    b.HasIndex("OrderRegistrationNumber")
                        .HasName("Index_OrderRegistrationNumber");

                    b.ToTable("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
