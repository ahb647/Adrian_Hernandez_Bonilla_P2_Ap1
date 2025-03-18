global using BlazorBootstrap;
global using Microsoft.EntityFrameworkCore;
global using Adrian_Hernandez_Bonilla_P2_Ap1.Models;
global using Adrian_Hernandez_Bonilla_P2_Ap1.Context;
global using Adrian_Hernandez_Bonilla_P2_Ap1.Services;
using Adrian_Hernandez_Bonilla_P2_Ap1.Components;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddBlazorBootstrap();


builder.Services.AddDbContextFactory<Contexto>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConStr"));
});



builder.Services.AddSingleton<ToastService>();
builder.Services.AddScoped<CiudadesServices>();
builder.Services.AddScoped<EncuestasServices>();
builder.Services.AddScoped<EncuestasDetalleService>();
builder.Services.AddBlazorBootstrap();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
