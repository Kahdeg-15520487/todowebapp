using todo.service.Infrastructure.Data;
using todo.service.Services.Authentication;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterDatabaseServices(builder.Configuration);
builder.Services.RegisterAuthenticationServices();
builder.Services.AddControllers();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
