using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProjectManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Projects",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "CreatedDate", "Description", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 5, 14, 52, 36, 178, DateTimeKind.Utc).AddTicks(2868), "Aplikacja backendowa w .NET do obsługi kont użytkowników", "System zarządzania użytkownikami" },
                    { 2, new DateTime(2025, 1, 13, 7, 40, 36, 178, DateTimeKind.Utc).AddTicks(2877), "Frontendowy panel administracyjny dla systemu zarządzania użytkownikami", "Panel administracyjny Angular" },
                    { 3, new DateTime(2024, 12, 26, 0, 28, 36, 178, DateTimeKind.Utc).AddTicks(2880), "Moduł płatności w .NET integrujący różne bramki płatności", "API do obsługi płatności" },
                    { 4, new DateTime(2025, 1, 10, 2, 52, 36, 178, DateTimeKind.Utc).AddTicks(2881), "Aplikacja w Angular do analizy sprzedaży i monitorowania zamówień", "Dashboard dla e-commerce" },
                    { 5, new DateTime(2025, 2, 2, 19, 40, 36, 178, DateTimeKind.Utc).AddTicks(2881), "API backendowe dla aplikacji mobilnej do zarządzania zamówieniami", "Aplikacja mobilna dla klientów" },
                    { 6, new DateTime(2025, 2, 3, 12, 28, 36, 178, DateTimeKind.Utc).AddTicks(2884), "System zarządzania treścią w .NET dla blogów technologicznych", "CMS dla blogerów" }
                });

            migrationBuilder.InsertData(
                table: "ProjectTasks",
                columns: new[] { "Id", "Description", "DueDate", "IsCompleted", "ProjectId", "Title" },
                values: new object[,]
                {
                    { 1, "Dodanie funkcji rejestracji z walidacją danych", new DateTime(2025, 1, 21, 19, 40, 36, 178, DateTimeKind.Utc).AddTicks(2914), false, 1, "Implementacja rejestracji użytkowników" },
                    { 2, "Funkcja resetowania hasła z kodem weryfikacyjnym na e-mail", new DateTime(2025, 1, 15, 19, 40, 36, 178, DateTimeKind.Utc).AddTicks(2917), false, 1, "Obsługa resetowania hasła" },
                    { 3, "System ról i uprawnień dla różnych typów kont", new DateTime(2025, 1, 10, 19, 40, 36, 178, DateTimeKind.Utc).AddTicks(2918), false, 1, "Dodanie ról użytkowników" },
                    { 4, "Uwierzytelnianie użytkowników za pomocą tokenów JWT", new DateTime(2025, 1, 25, 19, 40, 36, 178, DateTimeKind.Utc).AddTicks(2919), true, 1, "Logowanie z wykorzystaniem JWT" },
                    { 5, "Panel z listą użytkowników i ich statusem", new DateTime(2025, 1, 23, 19, 40, 36, 178, DateTimeKind.Utc).AddTicks(2921), false, 2, "Stworzenie dashboardu administratora" },
                    { 6, "Formularz edycji danych użytkownika", new DateTime(2025, 1, 17, 19, 40, 36, 178, DateTimeKind.Utc).AddTicks(2923), false, 2, "Dodanie edycji profilu użytkownika" },
                    { 7, "Blokowanie dostępu do sekcji dla nieuprawnionych użytkowników", new DateTime(2025, 1, 13, 19, 40, 36, 178, DateTimeKind.Utc).AddTicks(2924), false, 2, "Obsługa autoryzacji w panelu" },
                    { 8, "Dodanie obsługi płatności przez PayU", new DateTime(2025, 1, 5, 19, 40, 36, 178, DateTimeKind.Utc).AddTicks(2925), false, 3, "Integracja z PayU" },
                    { 9, "Obsługa powiadomień zwrotnych z bramki płatności", new DateTime(2024, 12, 31, 19, 40, 36, 178, DateTimeKind.Utc).AddTicks(2927), false, 3, "Implementacja webhooków dla transakcji" },
                    { 10, "Możliwość zwrotu środków użytkownikowi", new DateTime(2024, 12, 26, 19, 40, 36, 178, DateTimeKind.Utc).AddTicks(2929), false, 3, "Obsługa zwrotów płatności" },
                    { 11, "Wykresy i dane dotyczące sprzedaży w sklepie", new DateTime(2025, 1, 20, 19, 40, 36, 178, DateTimeKind.Utc).AddTicks(2931), false, 4, "Wyświetlanie statystyk sprzedaży" },
                    { 12, "Podsumowanie najczęściej kupowanych produktów", new DateTime(2025, 1, 10, 19, 40, 36, 178, DateTimeKind.Utc).AddTicks(2932), false, 4, "Moduł analizy trendów zakupowych" },
                    { 13, "Funkcja generowania raportów sprzedaży w formacie PDF", new DateTime(2025, 1, 15, 19, 40, 36, 178, DateTimeKind.Utc).AddTicks(2933), false, 4, "Eksport raportów do PDF" },
                    { 14, "Obsługa użytkowników w aplikacji mobilnej", new DateTime(2025, 2, 2, 19, 40, 36, 178, DateTimeKind.Utc).AddTicks(2934), true, 5, "API do logowania i rejestracji" },
                    { 15, "Funkcja składania zamówień w aplikacji", new DateTime(2025, 2, 3, 19, 40, 36, 178, DateTimeKind.Utc).AddTicks(2936), false, 5, "Obsługa zamówień przez API" },
                    { 16, "Implementacja płatności mobilnych w aplikacji", new DateTime(2025, 2, 2, 19, 40, 36, 178, DateTimeKind.Utc).AddTicks(2937), false, 5, "Obsługa płatności mobilnych" },
                    { 17, "Edytor WYSIWYG do tworzenia treści blogowych", new DateTime(2025, 2, 4, 19, 40, 36, 178, DateTimeKind.Utc).AddTicks(2938), false, 6, "Dodanie edytora tekstu dla blogów" },
                    { 18, "Obsługa komentarzy pod postami", new DateTime(2025, 2, 4, 19, 40, 36, 178, DateTimeKind.Utc).AddTicks(2940), false, 6, "Moduł komentarzy" },
                    { 19, "Udostępnianie postów na Facebooku i Twitterze", new DateTime(2025, 2, 4, 7, 40, 36, 178, DateTimeKind.Utc).AddTicks(2941), false, 6, "Integracja z social media" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProjectTasks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProjectTasks",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProjectTasks",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProjectTasks",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ProjectTasks",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ProjectTasks",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ProjectTasks",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ProjectTasks",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ProjectTasks",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ProjectTasks",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ProjectTasks",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "ProjectTasks",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "ProjectTasks",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "ProjectTasks",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "ProjectTasks",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "ProjectTasks",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "ProjectTasks",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "ProjectTasks",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "ProjectTasks",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Projects",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }
    }
}
