using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projeto_apave.Migrations
{
    /// <inheritdoc />
    public partial class AddPainelIdTabelaSolicitacaoManutencao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PainelId",
                table: "SolicitacaoManutencao",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacaoManutencao_PainelId",
                table: "SolicitacaoManutencao",
                column: "PainelId");

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitacaoManutencao_Painel_PainelId",
                table: "SolicitacaoManutencao",
                column: "PainelId",
                principalTable: "Painel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SolicitacaoManutencao_Painel_PainelId",
                table: "SolicitacaoManutencao");

            migrationBuilder.DropIndex(
                name: "IX_SolicitacaoManutencao_PainelId",
                table: "SolicitacaoManutencao");

            migrationBuilder.DropColumn(
                name: "PainelId",
                table: "SolicitacaoManutencao");
        }
    }
}
