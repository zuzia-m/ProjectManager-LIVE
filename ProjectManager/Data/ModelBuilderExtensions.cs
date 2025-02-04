using Microsoft.EntityFrameworkCore;
using ProjectManager.Data.Models;

namespace ProjectManager.Data
{
    public static class ModelBuilderExtensions
    {
        public static void ConfigureProject(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
               .Property(p => p.CreatedDate)
               .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Project>()
                .Property(p => p.Name)
                .IsRequired(true)
                .HasMaxLength(50);

            modelBuilder.Entity<Project>()
                .Property(p => p.Description)
                .IsRequired(false)
                .HasMaxLength(500);

            // analogiczny sposób zapisu:
            //modelBuilder.Entity<Project>(entity =>
            //{
            //    entity.Property(p => p.Name)
            //        .IsRequired(true)
            //        .HasMaxLength(30);
            //    entity.Property(p => p.Description)
            //        .HasMaxLength(500)
            //        .IsRequired(false);
            //    entity.Property(p => p.CreatedDate)
            //        .HasDefaultValueSql("GETDATE()");
            //});
        }

        public static void ConfigureProjectTask(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProjectTask>()
                .HasOne(pt => pt.Project) // ProjectTask ma jeden Project
                .WithMany(p => p.ProjectTasks) // Project może mieć wiele tasków
                .HasForeignKey(pt => pt.ProjectId) // Klucz obcy który wskazuje na ProjectId
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProjectTask>()
                .Property(p => p.Title)
                .IsRequired(true)
                .HasMaxLength(100);

            modelBuilder.Entity<ProjectTask>()
                .Property(p => p.Description)
                .IsRequired(false);

            modelBuilder.Entity<ProjectTask>()
                .Property(p => p.IsCompleted)
                .IsRequired(true);
        }

        public static void Seed(this ModelBuilder modelBuilder)
        {
            var projects = new List<Project>
            {
                new Project
                {
                    Id = 1,
                    Name = "System zarządzania użytkownikami",
                    Description = "Aplikacja backendowa w .NET do obsługi kont użytkowników",
                    CreatedDate = DateTime.UtcNow.AddDays(-30.2)
                },
                new Project
                {
                    Id = 2,
                    Name = "Panel administracyjny Angular",
                    Description = "Frontendowy panel administracyjny dla systemu zarządzania użytkownikami",
                    CreatedDate = DateTime.UtcNow.AddDays(-22.5)
                },
                new Project
                {
                    Id = 3,
                    Name = "API do obsługi płatności",
                    Description = "Moduł płatności w .NET integrujący różne bramki płatności",
                    CreatedDate = DateTime.UtcNow.AddDays(-40.8)
                },
                new Project
                {
                    Id = 4,
                    Name = "Dashboard dla e-commerce",
                    Description = "Aplikacja w Angular do analizy sprzedaży i monitorowania zamówień",
                    CreatedDate =  DateTime.UtcNow.AddDays(-25.7)
                },
                new Project
                {
                    Id = 5,
                    Name = "Aplikacja mobilna dla klientów",
                    Description = "API backendowe dla aplikacji mobilnej do zarządzania zamówieniami",
                    CreatedDate = DateTime.UtcNow.AddDays(-2)
                },
                new Project
                {
                    Id = 6,
                    Name = "CMS dla blogerów",
                    Description = "System zarządzania treścią w .NET dla blogów technologicznych",
                    CreatedDate = DateTime.UtcNow.AddDays(-1.3)
                }
            };

            modelBuilder.Entity<Project>().HasData(projects);

            var projectTasks = new List<ProjectTask>
            {
                // System zarządzania użytkownikami
                new ProjectTask { Id = 1, Title = "Implementacja rejestracji użytkowników", Description = "Dodanie funkcji rejestracji z walidacją danych", DueDate = DateTime.UtcNow.AddDays(-14), IsCompleted = false, ProjectId = 1 },
                new ProjectTask { Id = 2, Title = "Obsługa resetowania hasła", Description = "Funkcja resetowania hasła z kodem weryfikacyjnym na e-mail", DueDate = DateTime.UtcNow.AddDays(-20), IsCompleted = false, ProjectId = 1 },
                new ProjectTask { Id = 3, Title = "Dodanie ról użytkowników", Description = "System ról i uprawnień dla różnych typów kont", DueDate = DateTime.UtcNow.AddDays(-25), IsCompleted = false, ProjectId = 1 },
                new ProjectTask { Id = 4, Title = "Logowanie z wykorzystaniem JWT", Description = "Uwierzytelnianie użytkowników za pomocą tokenów JWT", DueDate = DateTime.UtcNow.AddDays(-10), IsCompleted = true, ProjectId = 1 },
                // Panel administracyjny Angular
                new ProjectTask { Id = 5, Title = "Stworzenie dashboardu administratora", Description = "Panel z listą użytkowników i ich statusem", DueDate = DateTime.UtcNow.AddDays(-12), IsCompleted = false, ProjectId = 2 },
                new ProjectTask { Id = 6, Title = "Dodanie edycji profilu użytkownika", Description = "Formularz edycji danych użytkownika", DueDate = DateTime.UtcNow.AddDays(-18), IsCompleted = false, ProjectId = 2 },
                new ProjectTask { Id = 7, Title = "Obsługa autoryzacji w panelu", Description = "Blokowanie dostępu do sekcji dla nieuprawnionych użytkowników", DueDate = DateTime.UtcNow.AddDays(-22), IsCompleted = false, ProjectId = 2 },
                // API do obsługi płatności
                new ProjectTask { Id = 8, Title = "Integracja z PayU", Description = "Dodanie obsługi płatności przez PayU", DueDate = DateTime.UtcNow.AddDays(-30), IsCompleted = false, ProjectId = 3 },
                new ProjectTask { Id = 9, Title = "Implementacja webhooków dla transakcji", Description = "Obsługa powiadomień zwrotnych z bramki płatności", DueDate = DateTime.UtcNow.AddDays(-35), IsCompleted = false, ProjectId = 3 },
                new ProjectTask { Id = 10, Title = "Obsługa zwrotów płatności", Description = "Możliwość zwrotu środków użytkownikowi", DueDate = DateTime.UtcNow.AddDays(-40), IsCompleted = false, ProjectId = 3 },
                // Dashboard dla e-commerce
                new ProjectTask { Id = 11, Title = "Wyświetlanie statystyk sprzedaży", Description = "Wykresy i dane dotyczące sprzedaży w sklepie", DueDate = DateTime.UtcNow.AddDays(-15), IsCompleted = false, ProjectId = 4 },
                new ProjectTask { Id = 12, Title = "Moduł analizy trendów zakupowych", Description = "Podsumowanie najczęściej kupowanych produktów", DueDate = DateTime.UtcNow.AddDays(-25), IsCompleted = false, ProjectId = 4 },
                new ProjectTask { Id = 13, Title = "Eksport raportów do PDF", Description = "Funkcja generowania raportów sprzedaży w formacie PDF", DueDate = DateTime.UtcNow.AddDays(-20), IsCompleted = false, ProjectId = 4 },
                // Aplikacja mobilna dla klientów
                new ProjectTask { Id = 14, Title = "API do logowania i rejestracji", Description = "Obsługa użytkowników w aplikacji mobilnej", DueDate = DateTime.UtcNow.AddDays(-2), IsCompleted = true, ProjectId = 5 },
                new ProjectTask { Id = 15, Title = "Obsługa zamówień przez API", Description = "Funkcja składania zamówień w aplikacji", DueDate = DateTime.UtcNow.AddDays(-1), IsCompleted = false, ProjectId = 5 },
                new ProjectTask { Id = 16, Title = "Obsługa płatności mobilnych", Description = "Implementacja płatności mobilnych w aplikacji", DueDate = DateTime.UtcNow.AddDays(-2), IsCompleted = false, ProjectId = 5 },
                // CMS dla blogerów
                new ProjectTask { Id = 17, Title = "Dodanie edytora tekstu dla blogów", Description = "Edytor WYSIWYG do tworzenia treści blogowych", DueDate = DateTime.UtcNow, IsCompleted = false, ProjectId = 6 },
                new ProjectTask { Id = 18, Title = "Moduł komentarzy", Description = "Obsługa komentarzy pod postami", DueDate = DateTime.UtcNow, IsCompleted = false, ProjectId = 6 },
                new ProjectTask { Id = 19, Title = "Integracja z social media", Description = "Udostępnianie postów na Facebooku i Twitterze", DueDate = DateTime.UtcNow.AddDays(-0.5), IsCompleted = false, ProjectId = 6 }
            };

            modelBuilder.Entity<ProjectTask>().HasData(projectTasks);
        }
    }
}
