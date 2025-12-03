var MyAllowsSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
  options.AddPolicy(name: MyAllowsSpecificOrigins,
                    builder =>
                    {
                      builder.WithOrigins("https://localhost", "https://localhost:4200")
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .SetIsOriginAllowedToAllowWildcardSubdomains();
                    });
  options.AddPolicy("AllowAngular",
                      policy =>
                      {
                        policy.WithOrigins("https://localhost", "http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .SetIsOriginAllowedToAllowWildcardSubdomains();
                      });

});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("AllowAngular");
app.UseCors(MyAllowsSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
