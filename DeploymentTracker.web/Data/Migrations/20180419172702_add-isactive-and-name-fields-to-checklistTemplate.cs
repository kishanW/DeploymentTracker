using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DeploymentTracker.web.Data.Migrations
{
    public partial class addisactiveandnamefieldstochecklistTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ChecklistTemplateEntity",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ChecklistTemplateEntity",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ChecklistTemplateEntity");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ChecklistTemplateEntity");
        }
    }
}
