using Blazored.Toast;
using Newtonsoft.Json;
using ProSwapperLobby.Data;
using ProSwapperLobby.Services;
using System.Diagnostics;


public class Program
{
    private static void OpenUrl(string url)
    {
        Process webProcess = new Process();
        webProcess.StartInfo = new ProcessStartInfo() { UseShellExecute = true, FileName = url };
        webProcess.Start();
    }

    /// <summary>
    /// Kills all proccesses with the same name that is not the current process.
    /// </summary>
    private static void KillDuplicates()
    {
        Process thisProc = Process.GetCurrentProcess();
        Process[] procs = Process.GetProcessesByName(thisProc.ProcessName);
        foreach (var proc in procs)
        {
            if (proc.Id != thisProc.Id)
                proc.Kill();
        }
    }

    public static void Main()
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

        string url = "http://localhost:6969/";

        KillDuplicates();

        var builder = WebApplication.CreateBuilder();

        // Add services to the container.
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();
        // builder.Services.AddSingleton<MainService>();
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

        string data = MainService.httpClient.GetStringAsync("https://pro-swapper.github.io/api/LobbyAPI.json").Result;

        MainService.ProSwapperAPI = JsonConvert.DeserializeObject<ProSwapperLobby.Data.API.Rootobject>(data);

        if (MainService.ProSwapperAPI.Disable == true)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Pro Swapper is currently down: ");
            Console.Write(MainService.ProSwapperAPI.DisableMessage);
            Console.ReadLine();
            Process.GetCurrentProcess().Kill();
        }

        MainService.DiscordWidgetAPI = JsonConvert.DeserializeObject<DiscordWidgetAPI.Root>(MainService.httpClient.GetStringAsync($"https://discord.com/api/guilds/{MainService.ProSwapperAPI.DiscordID}/widget.json").Result);


        ProSwapperLobby.DiscordRPC.InitializeRPC();

        Directory.CreateDirectory(MainService.ProSwapperFolder + "\\.Config");
        MainService.InitConfig();

        Console.Clear();
        Console.Title = "Pro Swapper Lobby | Made by Kye#5000";
        Console.ForegroundColor = ConsoleColor.Green;


        Console.WriteLine("Welcome to Pro Swapper Lobby!");
        Console.WriteLine("You will be directed to linkvertise so you can access Pro Swapper Lobby, leave Pro Swapper Lobby opened if you don't want to go through linkvertise again.");
        OpenUrl("https://direct-link.net/86737/pro-swapper-lobby2");
        app.Run(url);
    }
}