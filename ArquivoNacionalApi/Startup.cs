using ArquivoNacionalApi.Extensions;

namespace ArquivoNacionalApi;

public class Startup
{
    private readonly IConfiguration Configuration;

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder
                    .AllowAnyOrigin() // Permitir qualquer origem (Isso é apenas para fins de desenvolvimento; ajuste conforme sua necessidade)
                    .AllowAnyMethod() // Permitir qualquer método HTTP
                    .AllowAnyHeader(); // Permitir qualquer cabeçalho HTTP
            });
        });
        services.AddExceptionHandler(options =>
        {
            options.ExceptionHandler = GlobalExceptionHandler.Handle;
            options.AllowStatusCode404Response = true;
        });

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddDatabase(Configuration.GetConnectionString("DefaultConnection"));
        services.AddRepositories();
        services.AddServices();
    }

    public void Configure(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();
        app.UseCors();

        app.MapControllers();
    }
}