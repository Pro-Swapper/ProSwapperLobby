using Blazored.Toast;
using Newtonsoft.Json;
using ProSwapperLobby;
using ProSwapperLobby.Data;
using ProSwapperLobby.Services;
using Serilog;
using System.Diagnostics;
using System.Runtime.InteropServices;

public class Program
{
    [DllImport("kernel32.dll")]
    static extern IntPtr GetConsoleWindow();

    [DllImport("user32.dll")]
    static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    const int SW_HIDE = 0;
    const int SW_SHOW = 5;


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
        Log.Logger.Information($"Current Process is : {thisProc.ProcessName}, ID: {thisProc.Id}");
        Process[] procs = Process.GetProcessesByName(thisProc.ProcessName).Where(x => !x.Id.Equals(thisProc.Id)).ToArray();
        if (procs.Length == 0)
        {
            Log.Logger.Information("OK, No other duplicate processes were found!");
            return;
        }

        foreach (Process proc in procs)
        {
            Log.Logger.Warning($"Duplicate process found: {proc.ProcessName}, ID: {proc.Id}, trying to kill");
            try
            {
                proc.Kill(true);
                Log.Logger.Warning($"Successfully killed {proc.Id}");
            }
            catch (Exception ex)
            {
                Log.Logger.Warning($"Could not kill {proc.Id}, Error: {ex.Message}");
            }
        }
    }

    public static void Main()
    {

        var handle = GetConsoleWindow();

        // Hide
        ShowWindow(handle, SW_HIDE);


#if RELEASE
        if (!File.Exists("aspnetcorev2_inprocess.dll"))
        {
            Console.Clear();
            Console.WriteLine("Please extract Pro Swapper Lobby.zip to a folder instead of doubling clicking it inside a zipping program.");

            Console.ReadKey();
            Process.GetCurrentProcess().Kill();
        }
#endif
        string LobbyLogsFolder = Path.Combine(MainService.ProSwapperFolder, "Logs", "ProSwapperLobby.log");

        if (File.Exists(LobbyLogsFolder))
            File.Delete(LobbyLogsFolder);


        Log.Logger = new LoggerConfiguration()
        .WriteTo.Console()
        .WriteTo.File(LobbyLogsFolder, rollingInterval: RollingInterval.Infinite, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} {Message:lj}{NewLine}{Exception}")
        .CreateLogger();
        Log.Logger.Information("Starting Pro Swapper Lobby");

        string url = "http://localhost:6969/";
        Log.Logger.Information("Trying to kill duplicate processes of self");
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

        MainService.ProSwapperAPI = JsonConvert.DeserializeObject<API.Rootobject>(data);

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

        string ProgramFilesX86Folder = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
        Browser Edge = new Browser(ProgramFilesX86Folder + @"\Microsoft\Edge\Application\msedge.exe");
        //Edge.OpenWebsite("https://direct-link.net/86737/pro-swapper-lobby2");
        Edge.OpenWebsite(url);

        app.Run(url);
        Console.WriteLine("a");
    }
}