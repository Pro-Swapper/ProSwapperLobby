using Blazored.Toast;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using ProSwapperLobby.Data;
using ProSwapperLobby.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<MainService>();
builder.Services.AddBlazoredToast();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}


app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
Console.Clear();