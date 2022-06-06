using Blazored.Toast;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using ProSwapperLobby.Data;
using ProSwapperLobby.Services;
using System.Diagnostics;


public class Program
{
    public static void Main(string[] args)
    {
#if RELEASE
        if (!File.Exists("aspnetcorev2_inprocess.dll"))
        {
            Console.Clear();
            Console.WriteLine("Please extract Pro Swapper Lobby.zip to a folder instead of doubling clicking it inside a zipping program.");

            Console.ReadKey();
            Process.GetCurrentProcess().Kill();
        }
#endif


        const string url = "http://localhost:6969";

        ProcessStartInfo info = new ProcessStartInfo();
        info.FileName = url;
        info.UseShellExecute = true;

        new Process() { StartInfo = info }.Start();

        Process thisProc = Process.GetCurrentProcess();
        Process.GetProcessesByName(thisProc.ProcessName).Where(x => x.Id != thisProc.Id).All(x => { x.Kill(); return true; });

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

        app.Run(url);
#if !RELEASE
        Console.Clear();
#endif
    }
}

