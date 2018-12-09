using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityServer.Migrations.ApplicationDb
{
    public partial class Test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Domains_DomainId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_RdpEndpoints_Domains_DomainId",
                table: "RdpEndpoints");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Domains",
                table: "Domains");

            migrationBuilder.RenameTable(
                name: "Domains",
                newName: "Domain");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Domain",
                table: "Domain",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Domain_DomainId",
                table: "AspNetUsers",
                column: "DomainId",
                principalTable: "Domain",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RdpEndpoints_Domain_DomainId",
                table: "RdpEndpoints",
                column: "DomainId",
                principalTable: "Domain",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Domain_DomainId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_RdpEndpoints_Domain_DomainId",
                table: "RdpEndpoints");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Domain",
                table: "Domain");

            migrationBuilder.RenameTable(
                name: "Domain",
                newName: "Domains");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Domains",
                table: "Domains",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Domains_DomainId",
                table: "AspNetUsers",
                column: "DomainId",
                principalTable: "Domains",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RdpEndpoints_Domains_DomainId",
                table: "RdpEndpoints",
                column: "DomainId",
                principalTable: "Domains",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
