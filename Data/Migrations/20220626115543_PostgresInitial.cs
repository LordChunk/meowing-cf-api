using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Data.Migrations
{
    public partial class PostgresInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HttpHeaders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Header = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HttpHeaders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RequestUrls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Url = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestUrls", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HttpRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UrlId = table.Column<int>(type: "integer", nullable: false),
                    Method = table.Column<string>(type: "text", nullable: false),
                    Body = table.Column<string>(type: "text", nullable: true),
                    Fetchers = table.Column<string>(type: "text", nullable: true),
                    BodyUsed = table.Column<bool>(type: "boolean", nullable: false),
                    Redirect = table.Column<string>(type: "text", nullable: true),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ContentLength = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HttpRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HttpRequests_RequestUrls_UrlId",
                        column: x => x.UrlId,
                        principalTable: "RequestUrls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HttpHeaderHttpRequest",
                columns: table => new
                {
                    HeadersId = table.Column<int>(type: "integer", nullable: false),
                    HttpRequestsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HttpHeaderHttpRequest", x => new { x.HeadersId, x.HttpRequestsId });
                    table.ForeignKey(
                        name: "FK_HttpHeaderHttpRequest_HttpHeaders_HeadersId",
                        column: x => x.HeadersId,
                        principalTable: "HttpHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HttpHeaderHttpRequest_HttpRequests_HttpRequestsId",
                        column: x => x.HttpRequestsId,
                        principalTable: "HttpRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HttpRequestLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CAST(NOW() at time zone 'utc' AS date)"),
                    RequestId = table.Column<int>(type: "integer", nullable: false),
                    RequestSize = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HttpRequestLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HttpRequestLog_HttpRequests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "HttpRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HttpHeaderHttpRequest_HttpRequestsId",
                table: "HttpHeaderHttpRequest",
                column: "HttpRequestsId");

            migrationBuilder.CreateIndex(
                name: "IX_HttpHeaders_Header_Value",
                table: "HttpHeaders",
                columns: new[] { "Header", "Value" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HttpRequestLog_RequestId",
                table: "HttpRequestLog",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_HttpRequests_UrlId",
                table: "HttpRequests",
                column: "UrlId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestUrls_Url",
                table: "RequestUrls",
                column: "Url",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HttpHeaderHttpRequest");

            migrationBuilder.DropTable(
                name: "HttpRequestLog");

            migrationBuilder.DropTable(
                name: "HttpHeaders");

            migrationBuilder.DropTable(
                name: "HttpRequests");

            migrationBuilder.DropTable(
                name: "RequestUrls");
        }
    }
}
