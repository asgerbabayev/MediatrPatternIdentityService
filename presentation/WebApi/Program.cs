using Code.Application;
using Code.Infrastructure;
using Code.Infrastructure.Persistance;
using Code.WebApi.Filters;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(opt => opt.Filters.Add<ApiExceptionFilterAttribute>());
builder.Services.AddFluentValidationAutoValidation(x => x.DisableDataAnnotationsValidation = false)
    .AddFluentValidationClientsideAdapters();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);


var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using (var scope = app.Services.CreateScope())
    {
        AppDbContextInitializer initializer = scope.ServiceProvider.GetRequiredService<AppDbContextInitializer>();
        await initializer.InitialAsync();
        await initializer.SeedAsync();
    }
}

app.UseHttpsRedirection();

//app.UseAuthentication();
//app.UseIdentityServer();
app.UseAuthorization();

app.MapControllers();

app.Run();

//dotnet ef migrations add Initial -p .\src\Code.Infrastructure\ -s .\presentation\WebApi\
//dotnet ef database update -p .\src\Code.Infrastructure\ -s .\presentation\WebApi\