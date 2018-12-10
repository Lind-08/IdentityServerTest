using Microsoft.EntityFrameworkCore.Migrations;

namespace ThinClientApi.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientFiles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Version = table.Column<string>(nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    Checksum = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientFiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Domains",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
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
                name: "IX_RdpEndpoints_DomainId",
                table: "RdpEndpoints",
                column: "DomainId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientFiles");

            migrationBuilder.DropTable(
                name: "RdpEndpoints");

            migrationBuilder.DropTable(
                name: "Domains");
        }
    }
}
