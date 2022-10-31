﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjectFleetsOfDrones.DAL;

#nullable disable

namespace ProjectFleetsOfDrones.Migrations
{
    [DbContext(typeof(FleetsOfDronesDbContext))]
    partial class FleetsOfDronesDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ProjectFleetsOfDrones.Models.Drone", b =>
                {
                    b.Property<int>("DroneId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DroneId"), 1L, 1);

                    b.Property<double>("FlightTime")
                        .HasColumnType("float");

                    b.Property<int>("Pilot")
                        .HasColumnType("int");

                    b.Property<int>("Propulsion")
                        .HasColumnType("int");

                    b.HasKey("DroneId");

                    b.ToTable("Drones");
                });

            modelBuilder.Entity("ProjectFleetsOfDrones.Models.Flight", b =>
                {
                    b.Property<int>("FlightId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FlightId"), 1L, 1);

                    b.Property<int?>("DroneId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("FlightId");

                    b.HasIndex("DroneId");

                    b.ToTable("Flights");
                });

            modelBuilder.Entity("ProjectFleetsOfDrones.Models.Flight", b =>
                {
                    b.HasOne("ProjectFleetsOfDrones.Models.Drone", "Drone")
                        .WithMany("Flights")
                        .HasForeignKey("DroneId");

                    b.Navigation("Drone");
                });

            modelBuilder.Entity("ProjectFleetsOfDrones.Models.Drone", b =>
                {
                    b.Navigation("Flights");
                });
#pragma warning restore 612, 618
        }
    }
}