using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiPerfil.Migrations
{
    /// <inheritdoc />
    public partial class AddAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "CPF", "Email", "NomeCompleto", "Saldo", "SenhaHash", "Telefone" },
                values: new object[] { 1, "00000000000", "admin@email.com", "Admin Master", 500m, "10000.T+mynenJ2vkjFAjTMWvJkg==.fcN+tJH6rWsSjuvGbHlcCjaQoVEspNSczr0pwsX8v80=", "999999999" });

            migrationBuilder.InsertData(
                table: "UsuarioPermissoes",
                columns: new[] { "PermissaoId", "UsuarioId" },
                values: new object[] { 1, 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UsuarioPermissoes",
                keyColumns: new[] { "PermissaoId", "UsuarioId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
