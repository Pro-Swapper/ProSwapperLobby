using System.Text.Json;
namespace ProSwapperLobby.SwapperLogic
{
    public class EpicGamesLauncher
    {
        private static readonly string LauncherPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\Epic\UnrealEngineLauncher\LauncherInstalled.dat";
        public class InstallationList
        {
            public string InstallLocation { get; set; }
            public string NamespaceId { get; set; }
            public string ItemId { get; set; }
            public string ArtifactId { get; set; }
            public string AppVersion { get; set; }
            public string AppName { get; set; }
        }

        public class Root
        {
            public List<InstallationList> InstallationList { get; set; }
        }
        public static string FindPakFiles()=> JsonSerializer.Deserialize<Root>(File.ReadAllText(LauncherPath)).InstallationList.First(x => x.AppName.Equals("Fortnite")).InstallLocation + @"\FortniteGame\Content\Paks";
        public static string GetCurrentInstalledFortniteVersion()=> JsonSerializer.Deserialize<Root>(File.ReadAllText(LauncherPath)).InstallationList.First(x => x.AppName.Equals("Fortnite")).AppVersion;
    }
}
