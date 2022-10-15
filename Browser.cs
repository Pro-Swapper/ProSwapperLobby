using System.Diagnostics;

namespace ProSwapperLobby
{
    internal sealed class Browser
    {
        private string Path { get; set; }
        public Browser(string Path)
        {
            this.Path = Path;
        }

        public bool OpenWebsite(string url)
        {
            if (File.Exists(this.Path))
            {
                ProcessStartInfo startInfo = new ProcessStartInfo() { Arguments = $"--incognito --app={url}", FileName = this.Path };
                Process.Start(startInfo);
                return true;
            }
            else
                return false;

        }
    }
}
