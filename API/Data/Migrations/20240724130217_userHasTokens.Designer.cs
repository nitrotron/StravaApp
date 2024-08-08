﻿// <auto-generated />
using System;
using API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace API.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240724130217_userHasTokens")]
    partial class userHasTokens
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.7");

            modelBuilder.Entity("API.Models.Activity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AchievementCount")
                        .HasColumnType("INTEGER");

                    b.Property<int>("AthleteCount")
                        .HasColumnType("INTEGER");

                    b.Property<long>("AthleteId")
                        .HasColumnType("INTEGER");

                    b.Property<float>("AverageCadence")
                        .HasColumnType("REAL");

                    b.Property<float>("AverageHeartrate")
                        .HasColumnType("REAL");

                    b.Property<float>("AveragePower")
                        .HasColumnType("REAL");

                    b.Property<float>("AverageSpeed")
                        .HasColumnType("REAL");

                    b.Property<float>("AverageTemperature")
                        .HasColumnType("REAL");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("CommentCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("DeviceName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<float>("Distance")
                        .HasColumnType("REAL");

                    b.Property<int>("ElapsedTime")
                        .HasColumnType("INTEGER");

                    b.Property<float>("ElevationGain")
                        .HasColumnType("REAL");

                    b.Property<double?>("EndLatitude")
                        .HasColumnType("REAL");

                    b.Property<double?>("EndLongitude")
                        .HasColumnType("REAL");

                    b.Property<string>("EndPoint")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("GearId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("HasKudoed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("HasPowerMeter")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsCommute")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsFlagged")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsManual")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsPrivate")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsTrainer")
                        .HasColumnType("INTEGER");

                    b.Property<float>("Kilojoules")
                        .HasColumnType("REAL");

                    b.Property<int>("KudosCount")
                        .HasColumnType("INTEGER");

                    b.Property<long>("MapId")
                        .HasColumnType("INTEGER");

                    b.Property<float>("MaxHeartrate")
                        .HasColumnType("REAL");

                    b.Property<float>("MaxSpeed")
                        .HasColumnType("REAL");

                    b.Property<int>("MovingTime")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("PhotoCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Polyline")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("SportType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("StartDate")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("StartDateLocal")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double?>("StartLatitude")
                        .HasColumnType("REAL");

                    b.Property<double?>("StartLongitude")
                        .HasColumnType("REAL");

                    b.Property<string>("StartPoint")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("SufferScore")
                        .HasColumnType("REAL");

                    b.Property<string>("SummaryPolyline")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TimeZone")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("Truncated")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long?>("UploadId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UploadIdStr")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("WeightedAverageWatts")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Activities");
                });

            modelBuilder.Entity("API.Models.Athlete", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedAt")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("DatePreference")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("FollowerCount")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FriendCount")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Ftp")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsPremium")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("MeasurementPreference")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Profile")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ProfileMedium")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Sex")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UpdatedAt")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Athletes");
                });

            modelBuilder.Entity("API.Models.Oauth", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AccessCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("AccessToken")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ClientID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClientSecret")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long>("ExpiresAtEpoch")
                        .HasColumnType("INTEGER");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Scope")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Oauths");
                });

            modelBuilder.Entity("API.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AccessCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("AccessToken")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long>("ExpiresAtEpoch")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Scope")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
