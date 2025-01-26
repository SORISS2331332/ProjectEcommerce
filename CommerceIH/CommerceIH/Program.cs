using CommerceIH.Authentication;
using CommerceIH.Components;
using CommerceIH.Data;
using CommerceIH.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
var conStrBuilder = new SqlConnectionStringBuilder(builder.Configuration.GetConnectionString("Auth"));
builder.Services.AddPooledDbContextFactory<_2024Prog3CommerceIhContext>(options => options.UseSqlServer(conStrBuilder.ConnectionString));

builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthenticationCore();
builder.Services.AddScoped<ConnexionService>();
builder.Services.AddScoped<InscriptionSrvc>();
builder.Services.AddScoped<ProfilService>();
builder.Services.AddScoped<ListeProduitService>();
builder.Services.AddScoped<AjoutProduitService>();
builder.Services.AddScoped<ProtectedSessionStorage>(); 
builder.Services.AddScoped<AuthenticationStateProvider,CustomAuthenticationStateProvider>();
builder.Services.AddScoped<AffichageProduits>();
builder.Services.AddScoped<Recherche>();
builder.Services.AddScoped<DetailsProduitService>();
builder.Services.AddScoped<PanierService>();
builder.Services.AddScoped<CommandesService>();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
