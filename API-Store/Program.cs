using Domain.Interfaces;
using Infra;
using Infra.Context;
using Infra.Repositories;

public class Program
{
    public static void Main(string[] args) 
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddDbContext<DbContextBase>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}

