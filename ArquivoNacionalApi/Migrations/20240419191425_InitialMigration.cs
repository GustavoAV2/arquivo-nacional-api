using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArquivoNacionalApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Document",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    filePath = table.Column<string>(type: "varchar(400)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Document", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "IndexPoint",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "varchar(150)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndexPoint", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Session",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    playerLimit = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Session", x => x.id);
                    table.ForeignKey(
                        name: "FK_Session_Document_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Document",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "varchar(100)", nullable: false),
                    password = table.Column<string>(type: "varchar(50)", maxLength: 255, nullable: false),
                    state = table.Column<string>(type: "varchar(50)", maxLength: 255, nullable: false),
                    SessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id);
                    table.ForeignKey(
                        name: "FK_User_Session_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Session",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "DocumentMetadata",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    title = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false),
                    context = table.Column<string>(type: "varchar(1000)", maxLength: 4000, nullable: false),
                    socialMarkers = table.Column<string>(type: "varchar(1000)", nullable: false),
                    points = table.Column<int>(type: "int", maxLength: 100000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentMetadata", x => x.id);
                    table.ForeignKey(
                        name: "FK_DocumentMetadata_Document_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Document",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentMetadata_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocumentMetadataIndexPoint",
                columns: table => new
                {
                    DocumentsMetadataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IndexPointsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentMetadataIndexPoint", x => new { x.DocumentsMetadataId, x.IndexPointsId });
                    table.ForeignKey(
                        name: "FK_DocumentMetadataIndexPoint_DocumentMetadata_DocumentsMetadataId",
                        column: x => x.DocumentsMetadataId,
                        principalTable: "DocumentMetadata",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentMetadataIndexPoint_IndexPoint_IndexPointsId",
                        column: x => x.IndexPointsId,
                        principalTable: "IndexPoint",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentMetadata_DocumentId",
                table: "DocumentMetadata",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentMetadata_UserId",
                table: "DocumentMetadata",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentMetadataIndexPoint_IndexPointsId",
                table: "DocumentMetadataIndexPoint",
                column: "IndexPointsId");

            migrationBuilder.CreateIndex(
                name: "IX_Session_DocumentId",
                table: "Session",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_User_SessionId",
                table: "User",
                column: "SessionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentMetadataIndexPoint");

            migrationBuilder.DropTable(
                name: "DocumentMetadata");

            migrationBuilder.DropTable(
                name: "IndexPoint");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Session");

            migrationBuilder.DropTable(
                name: "Document");
        }
    }
}
