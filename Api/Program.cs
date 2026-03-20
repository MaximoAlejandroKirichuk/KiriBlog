using Api.Extensions;
using Application;
using Infrastructure;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

// 1. OpenAPI (Swagger)
builder.Services.AddSwaggerConfiguration();

// 2. Inject layout
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// 3. Controllers
builder.Services.AddControllers();

// CORS
// read app settings origin 
var corsOrigins = builder.Configuration.GetSection("CorsOrigins").Get<string[]>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        if (corsOrigins != null && corsOrigins.Length > 0)
        {
            policy.WithOrigins(corsOrigins) 
                .AllowAnyHeader()
                .AllowAnyMethod();
        }
    });
});


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "KiriBlog V1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseCors(); 

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//await DataSeeder.SeedAsync(app.Services);

app.Run();
