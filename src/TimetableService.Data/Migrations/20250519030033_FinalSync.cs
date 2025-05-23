﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityHelper.TimetableService.Data.Migrations
{
    /// <inheritdoc />
    public partial class FinalSync : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Institute = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Faculty = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Degree = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    FormEducation = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Course = table.Column<int>(type: "integer", nullable: false),
                    GroupName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Direction = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    SubGroup = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: true),
                    Teacher = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Room = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Building = table.Column<string>(type: "text", nullable: true),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: true),
                    Subgroup = table.Column<string>(type: "text", nullable: true),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    IsOnline = table.Column<bool>(type: "boolean", nullable: false),
                    OnlineLink = table.Column<string>(type: "text", nullable: true),
                    Color = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subjects_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimetableChanges",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    GroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    OriginalDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    NewDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    OriginalRoom = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    NewRoom = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    OriginalTeacher = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    NewTeacher = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Reason = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimetableChanges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimetableChanges_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TimetableChanges_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_GroupId",
                table: "Subjects",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_TimetableChanges_GroupId",
                table: "TimetableChanges",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_TimetableChanges_SubjectId",
                table: "TimetableChanges",
                column: "SubjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimetableChanges");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Groups");
        }
    }
}
