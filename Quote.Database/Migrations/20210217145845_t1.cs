using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Quote.Database.Migrations
{
    public partial class t1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "spAccessLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    TT = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreateUser = table.Column<int>(type: "INTEGER", nullable: false),
                    UpdateUser = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_spAccessLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "spCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreateUser = table.Column<int>(type: "INTEGER", nullable: false),
                    UpdateUser = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_spCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "spRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    UserAccess = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreateUser = table.Column<int>(type: "INTEGER", nullable: false),
                    UpdateUser = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_spRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "spSenders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreateUser = table.Column<int>(type: "INTEGER", nullable: false),
                    UpdateUser = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_spSenders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbAuthors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Patronymic = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreateUser = table.Column<int>(type: "INTEGER", nullable: false),
                    UpdateUser = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbAuthors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Patronymic = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Password = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    RoleId = table.Column<int>(type: "INTEGER", nullable: false),
                    Phone = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreateUser = table.Column<int>(type: "INTEGER", nullable: false),
                    UpdateUser = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbUsers_spRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "spRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbQuotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Text = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    AuthorId = table.Column<int>(type: "INTEGER", nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreateUser = table.Column<int>(type: "INTEGER", nullable: false),
                    UpdateUser = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbQuotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbQuotes_spCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "spCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbQuotes_tbAuthors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "tbAuthors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbSubscribes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SubscribeUserId = table.Column<int>(type: "INTEGER", nullable: false),
                    SenderId = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreateUser = table.Column<int>(type: "INTEGER", nullable: false),
                    UpdateUser = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbSubscribes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbSubscribes_spSenders_SenderId",
                        column: x => x.SenderId,
                        principalTable: "spSenders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbSubscribes_tbUsers_SubscribeUserId",
                        column: x => x.SubscribeUserId,
                        principalTable: "tbUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "spAccessLists",
                columns: new[] { "Id", "CreateDate", "CreateUser", "Name", "Status", "TT", "UpdateDate", "UpdateUser" },
                values: new object[] { 1, new DateTime(2021, 2, 17, 14, 58, 44, 407, DateTimeKind.Utc).AddTicks(172), 1, "Create", 1, 0, null, null });

            migrationBuilder.InsertData(
                table: "spAccessLists",
                columns: new[] { "Id", "CreateDate", "CreateUser", "Name", "Status", "TT", "UpdateDate", "UpdateUser" },
                values: new object[] { 2, new DateTime(2021, 2, 17, 14, 58, 44, 407, DateTimeKind.Utc).AddTicks(176), 1, "Delete", 1, 0, null, null });

            migrationBuilder.InsertData(
                table: "spAccessLists",
                columns: new[] { "Id", "CreateDate", "CreateUser", "Name", "Status", "TT", "UpdateDate", "UpdateUser" },
                values: new object[] { 3, new DateTime(2021, 2, 17, 14, 58, 44, 407, DateTimeKind.Utc).AddTicks(179), 1, "Update", 1, 0, null, null });

            migrationBuilder.InsertData(
                table: "spAccessLists",
                columns: new[] { "Id", "CreateDate", "CreateUser", "Name", "Status", "TT", "UpdateDate", "UpdateUser" },
                values: new object[] { 4, new DateTime(2021, 2, 17, 14, 58, 44, 407, DateTimeKind.Utc).AddTicks(181), 1, "View", 1, 0, null, null });

            migrationBuilder.InsertData(
                table: "spCategories",
                columns: new[] { "Id", "CreateDate", "CreateUser", "Name", "Status", "UpdateDate", "UpdateUser" },
                values: new object[] { 1, new DateTime(2021, 2, 17, 14, 58, 44, 406, DateTimeKind.Utc).AddTicks(8916), 1, "Category 1", 1, null, null });

            migrationBuilder.InsertData(
                table: "spCategories",
                columns: new[] { "Id", "CreateDate", "CreateUser", "Name", "Status", "UpdateDate", "UpdateUser" },
                values: new object[] { 2, new DateTime(2021, 2, 17, 14, 58, 44, 406, DateTimeKind.Utc).AddTicks(8920), 1, "Category 2", 1, null, null });

            migrationBuilder.InsertData(
                table: "spRoles",
                columns: new[] { "Id", "CreateDate", "CreateUser", "Name", "Status", "UpdateDate", "UpdateUser", "UserAccess" },
                values: new object[] { 1, new DateTime(2021, 2, 17, 14, 58, 44, 401, DateTimeKind.Utc).AddTicks(781), 1, "admin", 1, null, null, "1,2,3" });

            migrationBuilder.InsertData(
                table: "spRoles",
                columns: new[] { "Id", "CreateDate", "CreateUser", "Name", "Status", "UpdateDate", "UpdateUser", "UserAccess" },
                values: new object[] { 2, new DateTime(2021, 2, 17, 14, 58, 44, 401, DateTimeKind.Utc).AddTicks(1602), 1, "user", 1, null, null, "4" });

            migrationBuilder.InsertData(
                table: "spSenders",
                columns: new[] { "Id", "CreateDate", "CreateUser", "Name", "Status", "UpdateDate", "UpdateUser" },
                values: new object[] { 1, new DateTime(2021, 2, 17, 14, 58, 44, 406, DateTimeKind.Utc).AddTicks(7655), 1, "email", 1, null, null });

            migrationBuilder.InsertData(
                table: "spSenders",
                columns: new[] { "Id", "CreateDate", "CreateUser", "Name", "Status", "UpdateDate", "UpdateUser" },
                values: new object[] { 2, new DateTime(2021, 2, 17, 14, 58, 44, 406, DateTimeKind.Utc).AddTicks(7660), 1, "sms", 1, null, null });

            migrationBuilder.InsertData(
                table: "tbAuthors",
                columns: new[] { "Id", "CreateDate", "CreateUser", "LastName", "Name", "Patronymic", "Status", "UpdateDate", "UpdateUser" },
                values: new object[] { 1, new DateTime(2021, 2, 17, 14, 58, 44, 407, DateTimeKind.Utc).AddTicks(1980), 1, "Alimov", "Rustam", "Rustam", 1, null, null });

            migrationBuilder.InsertData(
                table: "tbAuthors",
                columns: new[] { "Id", "CreateDate", "CreateUser", "LastName", "Name", "Patronymic", "Status", "UpdateDate", "UpdateUser" },
                values: new object[] { 2, new DateTime(2021, 2, 17, 14, 58, 44, 407, DateTimeKind.Utc).AddTicks(1985), 1, "Sherali", "Juray", "Ozod", 1, null, null });

            migrationBuilder.InsertData(
                table: "tbQuotes",
                columns: new[] { "Id", "AuthorId", "CategoryId", "CreateDate", "CreateUser", "Status", "Text", "UpdateDate", "UpdateUser" },
                values: new object[] { 1, 1, 1, new DateTime(2021, 2, 17, 14, 58, 44, 407, DateTimeKind.Utc).AddTicks(3755), 1, 1, "Text 1", null, null });

            migrationBuilder.InsertData(
                table: "tbQuotes",
                columns: new[] { "Id", "AuthorId", "CategoryId", "CreateDate", "CreateUser", "Status", "Text", "UpdateDate", "UpdateUser" },
                values: new object[] { 3, 1, 2, new DateTime(2021, 2, 17, 14, 58, 44, 407, DateTimeKind.Utc).AddTicks(3761), 1, 1, "Text 3", null, null });

            migrationBuilder.InsertData(
                table: "tbQuotes",
                columns: new[] { "Id", "AuthorId", "CategoryId", "CreateDate", "CreateUser", "Status", "Text", "UpdateDate", "UpdateUser" },
                values: new object[] { 2, 2, 2, new DateTime(2021, 2, 17, 14, 58, 44, 407, DateTimeKind.Utc).AddTicks(3759), 1, 1, "Text 2", null, null });

            migrationBuilder.InsertData(
                table: "tbQuotes",
                columns: new[] { "Id", "AuthorId", "CategoryId", "CreateDate", "CreateUser", "Status", "Text", "UpdateDate", "UpdateUser" },
                values: new object[] { 4, 2, 1, new DateTime(2021, 2, 17, 14, 58, 44, 407, DateTimeKind.Utc).AddTicks(3762), 1, 1, "Text 4", null, null });

            migrationBuilder.InsertData(
                table: "tbUsers",
                columns: new[] { "Id", "CreateDate", "CreateUser", "Email", "LastName", "Name", "Password", "Patronymic", "Phone", "RoleId", "Status", "UpdateDate", "UpdateUser" },
                values: new object[] { 1, new DateTime(2021, 2, 17, 14, 58, 44, 406, DateTimeKind.Utc).AddTicks(5031), 1, "admin@gmail.com", "LastName", "Name", "c4ca4238a0b923820dcc509a6f75849b", "Patronymic", null, 1, 1, null, null });

            migrationBuilder.InsertData(
                table: "tbUsers",
                columns: new[] { "Id", "CreateDate", "CreateUser", "Email", "LastName", "Name", "Password", "Patronymic", "Phone", "RoleId", "Status", "UpdateDate", "UpdateUser" },
                values: new object[] { 2, new DateTime(2021, 2, 17, 14, 58, 44, 406, DateTimeKind.Utc).AddTicks(5801), 1, "user@gmail.com", "LastName", "Name", "c4ca4238a0b923820dcc509a6f75849b", "Patronymic", null, 2, 1, null, null });

            migrationBuilder.InsertData(
                table: "tbSubscribes",
                columns: new[] { "Id", "CreateDate", "CreateUser", "SenderId", "Status", "SubscribeUserId", "UpdateDate", "UpdateUser" },
                values: new object[] { 1, new DateTime(2021, 2, 17, 14, 58, 44, 407, DateTimeKind.Utc).AddTicks(5457), 1, 1, 1, 1, null, null });

            migrationBuilder.InsertData(
                table: "tbSubscribes",
                columns: new[] { "Id", "CreateDate", "CreateUser", "SenderId", "Status", "SubscribeUserId", "UpdateDate", "UpdateUser" },
                values: new object[] { 2, new DateTime(2021, 2, 17, 14, 58, 44, 407, DateTimeKind.Utc).AddTicks(5463), 1, 2, 1, 2, null, null });

            migrationBuilder.CreateIndex(
                name: "IX_tbQuotes_AuthorId",
                table: "tbQuotes",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_tbQuotes_CategoryId",
                table: "tbQuotes",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_tbSubscribes_SenderId",
                table: "tbSubscribes",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_tbSubscribes_SubscribeUserId",
                table: "tbSubscribes",
                column: "SubscribeUserId");

            migrationBuilder.CreateIndex(
                name: "IX_tbUsers_Email",
                table: "tbUsers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbUsers_RoleId",
                table: "tbUsers",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "spAccessLists");

            migrationBuilder.DropTable(
                name: "tbQuotes");

            migrationBuilder.DropTable(
                name: "tbSubscribes");

            migrationBuilder.DropTable(
                name: "spCategories");

            migrationBuilder.DropTable(
                name: "tbAuthors");

            migrationBuilder.DropTable(
                name: "spSenders");

            migrationBuilder.DropTable(
                name: "tbUsers");

            migrationBuilder.DropTable(
                name: "spRoles");
        }
    }
}
