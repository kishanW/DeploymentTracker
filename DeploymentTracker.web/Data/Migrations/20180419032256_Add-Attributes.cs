using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DeploymentTracker.web.Data.Migrations
{
    public partial class AddAttributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChecklistTaskEntity_ChecklistEntity_ChecklistId",
                table: "ChecklistTaskEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ChecklistTaskEntity_TaskEntity_TaskId",
                table: "ChecklistTaskEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ChecklistTemplateTaskEntity_TaskEntity_TaskId",
                table: "ChecklistTemplateTaskEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ChecklistTemplateTaskEntity_ChecklistTemplateEntity_TemplateId",
                table: "ChecklistTemplateTaskEntity");

            migrationBuilder.AlterColumn<Guid>(
                name: "TemplateId",
                table: "ChecklistTemplateTaskEntity",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TaskId",
                table: "ChecklistTemplateTaskEntity",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TaskId",
                table: "ChecklistTaskEntity",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ChecklistId",
                table: "ChecklistTaskEntity",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistTaskEntity_ChecklistEntity_ChecklistId",
                table: "ChecklistTaskEntity",
                column: "ChecklistId",
                principalTable: "ChecklistEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistTaskEntity_TaskEntity_TaskId",
                table: "ChecklistTaskEntity",
                column: "TaskId",
                principalTable: "TaskEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistTemplateTaskEntity_TaskEntity_TaskId",
                table: "ChecklistTemplateTaskEntity",
                column: "TaskId",
                principalTable: "TaskEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistTemplateTaskEntity_ChecklistTemplateEntity_TemplateId",
                table: "ChecklistTemplateTaskEntity",
                column: "TemplateId",
                principalTable: "ChecklistTemplateEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChecklistTaskEntity_ChecklistEntity_ChecklistId",
                table: "ChecklistTaskEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ChecklistTaskEntity_TaskEntity_TaskId",
                table: "ChecklistTaskEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ChecklistTemplateTaskEntity_TaskEntity_TaskId",
                table: "ChecklistTemplateTaskEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ChecklistTemplateTaskEntity_ChecklistTemplateEntity_TemplateId",
                table: "ChecklistTemplateTaskEntity");

            migrationBuilder.AlterColumn<Guid>(
                name: "TemplateId",
                table: "ChecklistTemplateTaskEntity",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "TaskId",
                table: "ChecklistTemplateTaskEntity",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "TaskId",
                table: "ChecklistTaskEntity",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "ChecklistId",
                table: "ChecklistTaskEntity",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistTaskEntity_ChecklistEntity_ChecklistId",
                table: "ChecklistTaskEntity",
                column: "ChecklistId",
                principalTable: "ChecklistEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistTaskEntity_TaskEntity_TaskId",
                table: "ChecklistTaskEntity",
                column: "TaskId",
                principalTable: "TaskEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistTemplateTaskEntity_TaskEntity_TaskId",
                table: "ChecklistTemplateTaskEntity",
                column: "TaskId",
                principalTable: "TaskEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistTemplateTaskEntity_ChecklistTemplateEntity_TemplateId",
                table: "ChecklistTemplateTaskEntity",
                column: "TemplateId",
                principalTable: "ChecklistTemplateEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
