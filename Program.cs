using Blazored.Toast;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Newtonsoft.Json;
using ProSwapperLobby.Data;
using ProSwapperLobby.Services;
using System.Diagnostics;


public class Program
{
    private const string ClientExe = "ProSwapperLobby.Client.exe";
    private static void OpenUrl(string url)
    {
        Process webProcess = new Process();
        webProcess.StartInfo = new ProcessStartInfo() { UseShellExecute = true, FileName = url };
        webProcess.Start();
    }
    public static void Main(string[] args)
    {
#if RELEASE
        if (!File.Exists(ClientExe))
        {
            Console.Clear();
            Console.WriteLine("Please extract Pro Swapper Lobby.zip to a folder instead of doubling clicking it inside a zipping program.");

            Console.ReadKey();
            Process.GetCurrentProcess().Kill();
        }
#endif

#if DEBUG
        args = new string[] { "http://localhost:6969/" };

#endif

        ////
        args = new string[] { "http://localhost:6969/" };

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






        using (HttpClient http = new())
        {
            string data = http.GetStringAsync("https://pro-swapper.github.io/api/LobbyAPI.json").Result;

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

            MainService.DiscordWidgetAPI = JsonConvert.DeserializeObject<DiscordWidgetAPI.Root>(http.GetStringAsync($"https://discord.com/api/guilds/{MainService.ProSwapperAPI.DiscordID}/widget.json").Result);
        }

        ProSwapperLobby.DiscordRPC.InitializeRPC();

        Directory.CreateDirectory(MainService.ProSwapperFolder + "\\.Config");
        MainService.InitConfig();

        Console.Clear();
        Console.Title = "Pro Swapper Lobby | Made by Kye#5000";
        Console.ForegroundColor = ConsoleColor.Green;


        Console.WriteLine("Welcome to Pro Swapper Lobby!");
        Console.ForegroundColor = ConsoleColor.Cyan;
        if (MainService.CurrentConfig.AskIfServerBooster == true)
        {
            Console.Write("Are you a server booster in the Discord server? Please type either Yes or No: ");
            string IsBoosterAnswer = Console.ReadLine().ToLower();
            if (IsBoosterAnswer == "yes" || IsBoosterAnswer == "y" || IsBoosterAnswer == "ye")
            {
                bool IsBooster = DiscordRoleAuth.IsServerBooster();
            }
            MainService.CurrentConfig.AskIfServerBooster = false;
            MainService.SaveConfig();
        }

        if (MainService.CurrentConfig.OAuthToken != null)
        {
            bool IsBooster = DiscordRoleAuth.IsServerBooster();
            if (IsBooster)
            {
                OpenUrl(args[0]);
                app.Run(args[0]);
            }
        }

#if DEBUG
        Console.WriteLine("\n\n\n[DEBUG]   Current Key: " + MainService.CurrentConfig.Key);
#endif

    start: if (KeyService.IsValidKey(MainService.CurrentConfig.Key))
        {
            Console.WriteLine("Please do not close this window when using Pro Swapper or the website will not work");
            Console.WriteLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Pro Swapper Lobby is currently running on {args[0]}, if you need any help please ask for help on the Pro Swapper Discord server: {MainService.DiscordWidgetAPI.instant_invite}");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"If you have paid for this software, you have been scammed. It is free to download at {MainService.DiscordWidgetAPI.instant_invite} and is also available on GitHub: https://github.com/Pro-Swapper");
            OpenUrl(args[0]);
            app.Run(args[0]);
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("To use Pro Swapper you must go through Linkvertise and get a " + TimeSpan.FromSeconds(MainService.ProSwapperAPI.KeyLifetime).TotalHours + " hour key");
            Thread.Sleep(1000);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Note: You can Server Boost the Discord server so you don't need to get a key! " + MainService.DiscordWidgetAPI.instant_invite);

            Console.WriteLine("\n\nOpening Linkvertise now...");
            Thread.Sleep(500);
            KeyService.OpenKeyUrl($"https://kyeondiscord.github.io/KeySite/{KeyService.GetIPHash() + DateTime.Now.Day}");
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("Paste your key in here: ");
                string key = Console.ReadLine();



                if (KeyService.IsValidKey(key))
                {
                    MainService.CurrentConfig.Key = key;
                    MainService.SaveConfig();
                    Console.Clear();
                    goto start;
                }
                else if (key == "Zm9ydG5pdGViYXR0bGVwYXNzaWp1c3RzaGl0b3V0bXlhc3Nmb3J0bml0ZWJhdHRsZXBhc3Npc2hpdG91dG15ITE1MDA1NTkyMDA=")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("[Success] Success");
                    Thread.Sleep(5000);
                    Console.WriteLine("or so you thought.");
                    Thread.Sleep(5000);
                    Console.WriteLine("Please use linkvertise like any other person please or boost the Discord Server.");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("[Error] Invalid Key!");
                }
            }


        }







    }
}