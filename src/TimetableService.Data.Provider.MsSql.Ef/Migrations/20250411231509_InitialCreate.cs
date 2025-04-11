using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using UniversityHelper.TimetableService.Data.Provider.MsSql.Ef;
using UniversityHelper.TimetableService.Models.Db;

namespace HerzenHelper.TimetableService.Data.Provider.MsSql.Ef.Migrations;

[DbContext(typeof(TimetableServiceDbContext))]
[Migration("20220301112700_Initial")]
public partial class InitialCreate : Migration
{
  protected override void Up(MigrationBuilder migrationBuilder)
  {
    migrationBuilder.CreateTable(
        name: DbGroup.TableName,
        columns: table => new
        {
          Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
          Institute = table.Column<string>(type: "nvarchar(max)", nullable: true),
          Faculcity = table.Column<string>(type: "nvarchar(max)", nullable: true),
          Degree = table.Column<string>(type: "nvarchar(max)", nullable: true),
          FormEducation = table.Column<string>(type: "nvarchar(max)", nullable: true),
          Course = table.Column<int>(type: "int", nullable: false),
          Group = table.Column<string>(type: "nvarchar(max)", nullable: true),
          Direction = table.Column<string>(type: "nvarchar(max)", nullable: true),
          SubGroup = table.Column<string>(type: "nvarchar(max)", nullable: true),
          UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_Groups", x => x.Id);
        });

    migrationBuilder.CreateTable(
        name: DbSubject.TableName,
        columns: table => new
        {
          Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
          GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
          Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
          Date = table.Column<DateTime>(type: "datetime2", nullable: false),
          Professor = table.Column<string>(type: "nvarchar(max)", nullable: true),
          PointId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
          UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
          Place = table.Column<string>(type: "nvarchar(max)", nullable: true)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_Subjects", x => x.Id);
          table.ForeignKey(
                  name: "FK_Subjects_Groups_GroupId",
                  column: x => x.GroupId,
                  principalTable: DbGroup.TableName,
                  principalColumn: "Id",
                  onDelete: ReferentialAction.Cascade);
        });

    migrationBuilder.CreateIndex(
        name: "IX_Subjects_GroupId",
        table: DbSubject.TableName,
        column: "GroupId");
  }

  protected override void Down(MigrationBuilder migrationBuilder)
  {
    migrationBuilder.DropTable(name: DbSubject.TableName);
    migrationBuilder.DropTable(name: DbGroup.TableName);
  }
}
