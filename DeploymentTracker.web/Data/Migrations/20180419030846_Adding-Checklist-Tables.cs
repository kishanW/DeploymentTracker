using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DeploymentTracker.web.Data.Migrations
{
    public partial class AddingChecklistTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EnvironmentEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModifiedOn = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnvironmentEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Comments = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModifiedOn = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChecklistEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Comments = table.Column<string>(nullable: true),
                    CompletedBy = table.Column<string>(nullable: true),
                    CompletedOn = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    EnvironmentId = table.Column<Guid>(nullable: true),
                    GitHash = table.Column<string>(nullable: true),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModifiedOn = table.Column<DateTime>(nullable: false),
                    ScheduledBy = table.Column<string>(nullable: true),
                    ScheduledOn = table.Column<DateTime>(nullable: true),
                    StartedBy = table.Column<string>(nullable: true),
                    StartedOn = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChecklistEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChecklistEntity_EnvironmentEntity_EnvironmentId",
                        column: x => x.EnvironmentId,
                        principalTable: "EnvironmentEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChecklistTemplateEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    EnvironmentId = table.Column<Guid>(nullable: true),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModifiedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChecklistTemplateEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChecklistTemplateEntity_EnvironmentEntity_EnvironmentId",
                        column: x => x.EnvironmentId,
                        principalTable: "EnvironmentEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChecklistTaskEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ChecklistId = table.Column<Guid>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModifiedOn = table.Column<DateTime>(nullable: false),
                    TaskId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChecklistTaskEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChecklistTaskEntity_ChecklistEntity_ChecklistId",
                        column: x => x.ChecklistId,
                        principalTable: "ChecklistEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChecklistTaskEntity_TaskEntity_TaskId",
                        column: x => x.TaskId,
                        principalTable: "TaskEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChecklistTemplateTaskEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModifiedOn = table.Column<DateTime>(nullable: false),
                    TaskId = table.Column<Guid>(nullable: true),
                    TemplateId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChecklistTemplateTaskEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChecklistTemplateTaskEntity_TaskEntity_TaskId",
                        column: x => x.TaskId,
                        principalTable: "TaskEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChecklistTemplateTaskEntity_ChecklistTemplateEntity_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "ChecklistTemplateEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistEntity_EnvironmentId",
                table: "ChecklistEntity",
                column: "EnvironmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistTaskEntity_ChecklistId",
                table: "ChecklistTaskEntity",
                column: "ChecklistId");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistTaskEntity_TaskId",
                table: "ChecklistTaskEntity",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistTemplateEntity_EnvironmentId",
                table: "ChecklistTemplateEntity",
                column: "EnvironmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistTemplateTaskEntity_TaskId",
                table: "ChecklistTemplateTaskEntity",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistTemplateTaskEntity_TemplateId",
                table: "ChecklistTemplateTaskEntity",
                column: "TemplateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChecklistTaskEntity");

            migrationBuilder.DropTable(
                name: "ChecklistTemplateTaskEntity");

            migrationBuilder.DropTable(
                name: "ChecklistEntity");

            migrationBuilder.DropTable(
                name: "TaskEntity");

            migrationBuilder.DropTable(
                name: "ChecklistTemplateEntity");

            migrationBuilder.DropTable(
                name: "EnvironmentEntity");
        }
    }
}
