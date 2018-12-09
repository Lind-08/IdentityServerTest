using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityServer.Migrations.ApplicationDb
{
    public partial class Test2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DomainId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Domains",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true)
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
                    Port = table.Column<int>(nullable: false),
                    DomainId = table.Column<string>(nullable: true)
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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "DomainId",
                table: "AspNetUsers");
        }
    }
}
