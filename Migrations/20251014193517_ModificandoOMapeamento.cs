using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ApiPerfil.Migrations
{
    /// <inheritdoc />
    public partial class ModificandoOMapeamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categoria_Produto",
                table: "Produtos");

            migrationBuilder.DropForeignKey(
                name: "FK_Produto_VendaItem",
                table: "VendaItens");

            migrationBuilder.DropForeignKey(
                name: "FK_Venda_VendaItem",
                table: "VendaItens");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Venda",
                table: "Vendas");

            migrationBuilder.DropIndex(
                name: "IX_Venda_VendaID",
                table: "Vendas");

            migrationBuilder.DropIndex(
                name: "IX_VendaItem_VendaItemID",
                table: "VendaItens");

            migrationBuilder.DropIndex(
                name: "IX_VendaItens_VendaID",
                table: "VendaItens");

            migrationBuilder.DropIndex(
                name: "IX_User_NomeCompleto",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Produto_Nome",
                table: "Produtos");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioID",
                table: "Vendas",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INT");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataVenda",
                table: "Vendas",
                type: "DATETIME",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "DATETIME");

            migrationBuilder.AlterColumn<int>(
                name: "VendaID",
                table: "VendaItens",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INT");

            migrationBuilder.AlterColumn<int>(
                name: "ProdutoID",
                table: "VendaItens",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INT");

            migrationBuilder.AlterColumn<string>(
                name: "Telefone",
                table: "Usuarios",
                type: "VARCHAR(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<decimal>(
                name: "Saldo",
                table: "Usuarios",
                type: "DECIMAL(10,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(10,2)");

            migrationBuilder.AlterColumn<int>(
                name: "Quantidade",
                table: "Produtos",
                type: "INT",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INT");

            migrationBuilder.AlterColumn<int>(
                name: "CategoriaID",
                table: "Produtos",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INT");

            migrationBuilder.InsertData(
                table: "Permissoes",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "UsuarioPadrao" },
                    { 3, "GerenteDeEstoque" }
                });

            migrationBuilder.AddCheckConstraint(
                name: "CHK_Vendas_ValorTotal",
                table: "Vendas",
                sql: "[ValorTotal] >= 0");

            migrationBuilder.CreateIndex(
                name: "UQ_Venda_Produto",
                table: "VendaItens",
                columns: new[] { "VendaID", "ProdutoID" },
                unique: true);

            migrationBuilder.AddCheckConstraint(
                name: "CHK_VendaItens_PrecoUnitario",
                table: "VendaItens",
                sql: "[PrecoUnitario] >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "CHK_VendaItens_Quantidade",
                table: "VendaItens",
                sql: "[Quantidade] > 0");

            migrationBuilder.CreateIndex(
                name: "IX_User_CPF",
                table: "Usuarios",
                column: "CPF",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "Usuarios",
                column: "Email",
                unique: true);

            migrationBuilder.AddCheckConstraint(
                name: "CHK_Usuarios_Saldo",
                table: "Usuarios",
                sql: "[Saldo] >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "CHK_Produtos_Preco",
                table: "Produtos",
                sql: "[Preco] >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "CHK_Produtos_Quantidade",
                table: "Produtos",
                sql: "[Quantidade] >= 0");

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Categorias",
                table: "Produtos",
                column: "CategoriaID",
                principalTable: "Categorias",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VendaItens_Produtos",
                table: "VendaItens",
                column: "ProdutoID",
                principalTable: "Produtos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VendaItens_Vendas",
                table: "VendaItens",
                column: "VendaID",
                principalTable: "Vendas",
                principalColumn: "VendaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vendas_Usuarios",
                table: "Vendas",
                column: "UsuarioID",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Categorias",
                table: "Produtos");

            migrationBuilder.DropForeignKey(
                name: "FK_VendaItens_Produtos",
                table: "VendaItens");

            migrationBuilder.DropForeignKey(
                name: "FK_VendaItens_Vendas",
                table: "VendaItens");

            migrationBuilder.DropForeignKey(
                name: "FK_Vendas_Usuarios",
                table: "Vendas");

            migrationBuilder.DropCheckConstraint(
                name: "CHK_Vendas_ValorTotal",
                table: "Vendas");

            migrationBuilder.DropIndex(
                name: "UQ_Venda_Produto",
                table: "VendaItens");

            migrationBuilder.DropCheckConstraint(
                name: "CHK_VendaItens_PrecoUnitario",
                table: "VendaItens");

            migrationBuilder.DropCheckConstraint(
                name: "CHK_VendaItens_Quantidade",
                table: "VendaItens");

            migrationBuilder.DropIndex(
                name: "IX_User_CPF",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_User_Email",
                table: "Usuarios");

            migrationBuilder.DropCheckConstraint(
                name: "CHK_Usuarios_Saldo",
                table: "Usuarios");

            migrationBuilder.DropCheckConstraint(
                name: "CHK_Produtos_Preco",
                table: "Produtos");

            migrationBuilder.DropCheckConstraint(
                name: "CHK_Produtos_Quantidade",
                table: "Produtos");

            migrationBuilder.DeleteData(
                table: "Permissoes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Permissoes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Permissoes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioID",
                table: "Vendas",
                type: "INT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataVenda",
                table: "Vendas",
                type: "DATETIME",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "DATETIME",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<int>(
                name: "VendaID",
                table: "VendaItens",
                type: "INT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ProdutoID",
                table: "VendaItens",
                type: "INT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Telefone",
                table: "Usuarios",
                type: "VARCHAR(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "VARCHAR(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Saldo",
                table: "Usuarios",
                type: "DECIMAL(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(10,2)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<int>(
                name: "Quantidade",
                table: "Produtos",
                type: "INT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INT",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "CategoriaID",
                table: "Produtos",
                type: "INT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Venda_VendaID",
                table: "Vendas",
                column: "VendaID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VendaItem_VendaItemID",
                table: "VendaItens",
                column: "VendaItemID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VendaItens_VendaID",
                table: "VendaItens",
                column: "VendaID");

            migrationBuilder.CreateIndex(
                name: "IX_User_NomeCompleto",
                table: "Usuarios",
                column: "NomeCompleto",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Produto_Nome",
                table: "Produtos",
                column: "Nome",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Categoria_Produto",
                table: "Produtos",
                column: "CategoriaID",
                principalTable: "Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Produto_VendaItem",
                table: "VendaItens",
                column: "ProdutoID",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Venda_VendaItem",
                table: "VendaItens",
                column: "VendaID",
                principalTable: "Vendas",
                principalColumn: "VendaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Venda",
                table: "Vendas",
                column: "UsuarioID",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
