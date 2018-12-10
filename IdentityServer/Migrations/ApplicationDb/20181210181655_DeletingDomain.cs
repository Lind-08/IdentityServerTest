using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityServer.Migrations.ApplicationDb
{
    public partial class DeletingDomain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Domains_DomainId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "RdpEndpoints");

            migrationBuilder.DropTable(
                name: "Domains");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DomainId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "DomainId",
                table: "AspNetUsers",
                newName: "Domain");

            migrationBuilder.AlterColumn<string>(
                name: "Domain",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Domain",
                table: "AspNetUsers",
                newName: "DomainId");

            migrationBuilder.AlterColumn<string>(
                name: "DomainId",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Domains",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Domains", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RdpEndpoints",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    DomainId = table.Column<string>(nullable: true),
                    Port = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RdpEndpoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RdpEndpoints_Domains_DomainId",
                        column: x => x.DomainId,
                        principalTable: "Domains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DomainId",
                table: "AspNetUsers",
                column: "DomainId");

            migrationBuilder.CreateIndex(
                name: "IX_RdpEndpoints_DomainId",
                table: "RdpEndpoints",
                column: "DomainId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Domains_DomainId",
                table: "AspNetUsers",
                column: "DomainId",
                principalTable: "Domains",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
