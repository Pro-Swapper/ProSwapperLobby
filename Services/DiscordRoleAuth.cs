using System.Collections.Specialized;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Web;

namespace ProSwapperLobby.Services
{
    internal class GuildMember
    {

        public class Root
        {
            public string avatar { get; set; }
            public object communication_disabled_until { get; set; }
            public int flags { get; set; }
            public bool is_pending { get; set; }
            public DateTime joined_at { get; set; }
            public string nick { get; set; }
            public bool pending { get; set; }
            public object premium_since { get; set; }
            public string[] roles { get; set; }
            public User user { get; set; }
            public bool mute { get; set; }
            public bool deaf { get; set; }
        }

        public class User
        {
            public string id { get; set; }
            public string username { get; set; }
            public string avatar { get; set; }
            public object avatar_decoration { get; set; }
            public string discriminator { get; set; }
            public int public_flags { get; set; }
        }

    }

    public class TokenResponse
    {
        public class Root
        {
            public string access_token { get; set; }
            public int expires_in { get; set; }
            public string refresh_token { get; set; }
            public string scope { get; set; }
            public string token_type { get; set; }
        }
    }

    public class DiscordRoleAuth
    {
        //Change these values
        public const string CLIENT_ID = "1005421448355135520";
        public const string CLIENT_SECRET = "SmfsDIL-_SdC7FWvLF8hk1IsD_vML6C4";
        public const string REDIRECT_URI = "http://localhost:5000";
        public static string GuildID => MainService.DiscordWidgetAPI.id;
        public static string[] TargetRoles = { "7094847781555405121" };

        private static HttpListener _httpListener = new HttpListener();

        private static GuildMember.Root CurrentUser = null;
        public static bool IsServerBooster()
        {
            if (CurrentUser != null && CurrentUser.roles.Any(x => TargetRoles.Contains(x)))
            {
                return true;
            }

            Console.WriteLine("Checking if you are a server booster...");

            if (!_httpListener.Prefixes.Contains(REDIRECT_URI + "/"))
                _httpListener.Prefixes.Add(REDIRECT_URI + "/");

            if (!_httpListener.IsListening)
                _httpListener.Start();

            //First check if a previously stored auth token is saved.
            start: if (MainService.CurrentConfig.OAuthToken != null)
            {
                CurrentUser = Discord.GetGuildMember(MainService.CurrentConfig.OAuthToken.access_token, GuildID).Result;
                if (CurrentUser != null)
                {
                    MainService.SaveConfig();
                    if (CurrentUser.roles.Any(x => TargetRoles.Contains(x)))
                    {
                        Console.WriteLine("You are a server booster!");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("It does not look like you're a server booster. Looks like you'll have to get a key through Linkvertise");
                        return false;
                    }

                }
                else
                {
                    //Expired Auth Token
                    MainService.CurrentConfig.OAuthToken = null;
                    goto start;
                }
            }
            else //No Auth token at all (OAuthToken == null)
            {
                //Start local server
                string url = $"https://discord.com/api/oauth2/authorize?client_id={CLIENT_ID}&redirect_uri={REDIRECT_URI}&response_type=code&scope=guilds.members.read";
                Process webProcess = new Process();
                webProcess.StartInfo = new ProcessStartInfo() { UseShellExecute = true, FileName = url };
                webProcess.Start();

                //Listen to server until OAuth is filled
                while (MainService.CurrentConfig.OAuthToken == null)
                {
                    HttpListenerContext context = _httpListener.GetContext(); // get a context
                    if (!string.IsNullOrEmpty(context.Request.Url.Query))
                    {
                        NameValueCollection queryDictionary = HttpUtility.ParseQueryString(context.Request.Url.Query);
                        if (queryDictionary["code"] != null)
                        {
                            string DiscordCode = queryDictionary["code"];
                            byte[] _responseArray = Encoding.UTF8.GetBytes($"You may close this tab now"); // get the bytes to response
                            context.Response.OutputStream.Write(_responseArray, 0, _responseArray.Length); // write bytes to the output stream
                            context.Response.KeepAlive = false; // set the KeepAlive bool to false
                            context.Response.Close(); // close the connection
                                                      //Console.Write("Received Discord Code: " + DiscordCode);
                            Console.WriteLine("Fetching access_token from Discord Code");
                            MainService.CurrentConfig.OAuthToken = Discord.GetToken(DiscordCode).Result;
                        }

                    }
                }
                goto start;
            }
        }
    }

    internal class Discord
    {
        /// <summary>
        /// Uses https://discord.com/api/oauth2/token endpoint and converts the code received from the browser to a bearer token
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        /// <exception cref="Exception">Error from Discord API</exception>
        public static async Task<TokenResponse.Root> GetToken(string code)
        {
            HttpClient client = new();

            var data = new[]
                {
                new KeyValuePair<string, string>("client_id", DiscordRoleAuth.CLIENT_ID),
                new KeyValuePair<string, string>("client_secret", DiscordRoleAuth.CLIENT_SECRET),
                new KeyValuePair<string, string>("code", code),
                new KeyValuePair<string, string>("redirect_uri", DiscordRoleAuth.REDIRECT_URI),
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                };
            var resp = await client.PostAsync("https://discord.com/api/oauth2/token", new FormUrlEncodedContent(data));
            string stringResp = await resp.Content.ReadAsStringAsync();
            if (resp.IsSuccessStatusCode)
                return JsonSerializer.Deserialize<TokenResponse.Root>(stringResp);
            else
                return null;
        }


        public static async Task<GuildMember.Root> GetGuildMember(string access_Token, string GuildID)
        {
            HttpClient client = new();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_Token);
            string url = $"https://discord.com/api/users/@me/guilds/{GuildID}/member";
            var resp = await client.GetAsync(url);
            string stringResp = await resp.Content.ReadAsStringAsync();
            if (resp.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<GuildMember.Root>(stringResp);
            }
            else
            {
                return null;
            }
        }
    }
}
