using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DeploymentTracker.web.Data.Migrations
{
    public partial class checklisttaskiscompletefield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsComplete",
                table: "ChecklistTaskEntity",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsComplete",
                table: "ChecklistTaskEntity");
        }
    }
}
