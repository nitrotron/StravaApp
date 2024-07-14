using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class Activities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    SportType = table.Column<string>(type: "TEXT", nullable: false),
                    Distance = table.Column<float>(type: "REAL", nullable: false),
                    MovingTime = table.Column<int>(type: "INTEGER", nullable: false),
                    ElapsedTime = table.Column<int>(type: "INTEGER", nullable: false),
                    ElevationGain = table.Column<float>(type: "REAL", nullable: false),
                    HasKudoed = table.Column<bool>(type: "INTEGER", nullable: false),
                    AverageHeartrate = table.Column<float>(type: "REAL", nullable: false),
                    MaxHeartrate = table.Column<float>(type: "REAL", nullable: false),
                    Truncated = table.Column<int>(type: "INTEGER", nullable: true),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    State = table.Column<string>(type: "TEXT", nullable: false),
                    Country = table.Column<string>(type: "TEXT", nullable: false),
                    GearId = table.Column<string>(type: "TEXT", nullable: false),
                    AverageSpeed = table.Column<float>(type: "REAL", nullable: false),
                    MaxSpeed = table.Column<float>(type: "REAL", nullable: false),
                    AverageCadence = table.Column<float>(type: "REAL", nullable: false),
                    AverageTemperature = table.Column<float>(type: "REAL", nullable: false),
                    AveragePower = table.Column<float>(type: "REAL", nullable: false),
                    Kilojoules = table.Column<float>(type: "REAL", nullable: false),
                    IsTrainer = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsCommute = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsManual = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsPrivate = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsFlagged = table.Column<bool>(type: "INTEGER", nullable: false),
                    AchievementCount = table.Column<int>(type: "INTEGER", nullable: false),
                    KudosCount = table.Column<int>(type: "INTEGER", nullable: false),
                    CommentCount = table.Column<int>(type: "INTEGER", nullable: false),
                    AthleteCount = table.Column<int>(type: "INTEGER", nullable: false),
                    PhotoCount = table.Column<int>(type: "INTEGER", nullable: false),
                    StartDate = table.Column<string>(type: "TEXT", nullable: false),
                    StartDateLocal = table.Column<string>(type: "TEXT", nullable: false),
                    TimeZone = table.Column<string>(type: "TEXT", nullable: false),
                    StartPoint = table.Column<string>(type: "TEXT", nullable: false),
                    StartLatitude = table.Column<double>(type: "REAL", nullable: true),
                    WeightedAverageWatts = table.Column<int>(type: "INTEGER", nullable: false),
                    StartLongitude = table.Column<double>(type: "REAL", nullable: true),
                    EndPoint = table.Column<string>(type: "TEXT", nullable: false),
                    EndLatitude = table.Column<double>(type: "REAL", nullable: true),
                    EndLongitude = table.Column<double>(type: "REAL", nullable: true),
                    HasPowerMeter = table.Column<bool>(type: "INTEGER", nullable: false),
                    MapId = table.Column<long>(type: "INTEGER", nullable: false),
                    AthleteId = table.Column<long>(type: "INTEGER", nullable: false),
                    SufferScore = table.Column<double>(type: "REAL", nullable: false),
                    uploadId = table.Column<long>(type: "INTEGER", nullable: true),
                    uploadIdStr = table.Column<string>(type: "TEXT", nullable: false),
                    deviceName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Activities");
        }
    }
}
