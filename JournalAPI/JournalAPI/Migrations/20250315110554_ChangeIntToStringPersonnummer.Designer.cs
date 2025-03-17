﻿// <auto-generated />
using JournalAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace JournalAPI.Migrations
{
    [DbContext(typeof(JournalDbContext))]
    [Migration("20250315110554_ChangeIntToStringPersonnummer")]
    partial class ChangeIntToStringPersonnummer
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("JournalAPI.Models.Journal", b =>
                {
                    b.Property<int>("JournalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("JournalId"));

                    b.Property<string>("Anteckning")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Personnummer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("JournalId");

                    b.ToTable("Journaler");
                });
#pragma warning restore 612, 618
        }
    }
}
