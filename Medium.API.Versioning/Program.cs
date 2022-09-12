using API.Version.Sample.Extension;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.ConfigureController();
builder.Services.AddHttpContextAccessor();
builder.Services.ConfigureSwagger();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwaggerApp();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
