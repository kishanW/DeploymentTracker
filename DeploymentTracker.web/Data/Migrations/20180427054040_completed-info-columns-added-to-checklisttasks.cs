using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DeploymentTracker.web.Data.Migrations
{
    public partial class completedinfocolumnsaddedtochecklisttasks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompletedBy",
                table: "ChecklistTaskEntity",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CompletedOn",
                table: "ChecklistTaskEntity",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompletedBy",
                table: "ChecklistTaskEntity");

            migrationBuilder.DropColumn(
                name: "CompletedOn",
                table: "ChecklistTaskEntity");
        }
    }
}
