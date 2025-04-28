# IdentityServer

This is a project for testing .NET 8.0 features and C# 11.0 features.

## EF Core Migration Instructions

1. **Add a New Migration:**
   To create a new migration, run the following command in the terminal:

   ```bash
   dotnet ef migrations add <MigrationName> --context ApplicationDbContext --project .\IdentityServer.Infrastructure\IdentityServer.Infrastructure.csproj --startup-project .\IdentityServer.Web\IdentityServer.Web.csproj --output-dir .\Data\Migrations\
   ```

   Replace `<MigrationName>` with a descriptive name for the migration.

2. **Update the Database:**
   After creating a migration, update the database to apply the changes:

   ```bash
   dotnet ef database update --context ApplicationDbContext --project .\IdentityServer.Infrastructure\IdentityServer.Infrastructure.csproj --startup-project .\IdentityServer.Web\IdentityServer.Web.csproj
   ```

3. **Revert a Migration:**
   To revert the last applied migration, use:

   ```bash
   dotnet ef migrations add <PreviousMigrationName> --context ApplicationDbContext --project .\IdentityServer.Infrastructure\IdentityServer.Infrastructure.csproj --startup-project .\IdentityServer.Web\IdentityServer.Web.csproj
   ```

   Replace `<PreviousMigrationName>` with the name of the migration you want to revert to.

4. **Remove the Last Migration:**
   If you need to remove the last migration (and it hasn't been applied to the database), use:

   ```bash
   dotnet ef migrations remove --context ApplicationDbContext --project .\IdentityServer.Infrastructure\IdentityServer.Infrastructure.csproj --startup-project .\IdentityServer.Web\IdentityServer.Web.csproj
   ```

5. **List All Migrations:**
   To see a list of all migrations, run:
   ```bash
   dotnet ef migrations list --context ApplicationDbContext --project .\IdentityServer.Infrastructure\IdentityServer.Infrastructure.csproj --startup-project .\IdentityServer.Web\IdentityServer.Web.csproj
   ```

Make sure that you are in the project root directory when running these commands.
