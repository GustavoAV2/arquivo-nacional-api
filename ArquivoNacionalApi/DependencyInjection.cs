using ArquivoNacionalApi.Data;
using ArquivoNacionalApi.Services;
using Microsoft.EntityFrameworkCore;
using ArquivoNacionalApi.Data.Repositories;

namespace ArquivoNacionalApi;

public static class DependencyInjection
{
    public static void AddDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<DatabaseContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IDocumentRepository, DocumentRepository>();
        services.AddTransient<IDocumentMetadataRepository, DocumentMetadataRepository>();
        services.AddTransient<ISessionRepository, SessionRepository>();
        services.AddTransient<IIndexPointRepository, IndexPointRepository>();
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IDocumentService, DocumentService>();
        services.AddTransient<IDocumentMetadataService, DocumentMetadataService>();
        services.AddTransient<ISessionService, SessionService>();
        services.AddTransient<IIndexPointService, IndexPointService>();
    }
}