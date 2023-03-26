using todo.service.Infrastructure.Data;
using todo.service.Services.Authentication;
using todo.service.Services.ToDo_;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(AutoMapperMarker));
builder.Services.RegisterDatabaseServices(builder.Configuration);
builder.Services.RegisterAuthenticationServices();
builder.Services.RegisterToDoServices();
builder.Services.AddControllers();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseCors(policyBuilder =>
{
    policyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
});

app.Run();

public class AutoMapperMarker { }