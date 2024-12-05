using API.Extensions;
using Application.Extensions;
using Persistence.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithFiltersExtension();
builder.Services.AddSwaggerExtension();

builder.Services.AddExceptionHandlerExtension();

builder.Services.AddRepositories(builder.Configuration);
builder.Services.AddServices();

builder.Services.AddCachingExtension();

var app = builder.Build();

app.UseExceptionHandler(x => { });

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerExtension();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
