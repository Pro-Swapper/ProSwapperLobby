using MessagePack;
using System.Net;
using Newtonsoft.Json;
using System.Net.Http;
using ProSwapperLobby.Data;
using System.Reflection;
using CUE4Parse.Encryption.Aes;

namespace ProSwapperLobby.Services
{
    public class MainService
    {
        public static readonly HttpClient client = new HttpClient();
        public static string version = Assembly.GetEntryAssembly().GetName().Version.ToString().Substring(0, 5);

        public static DiscordWidgetAPI.Root DiscordWidgetAPI { get; set; }
        public static Data.API.Rootobject ProSwapperAPI { get; set; }


        #region FortniteItemAPIInstances
        private static SkinObj.Root _skinObj { get; set; } = null;
        private static SkinObj.Root _backblingObj { get; set; } = null;
        private static SkinObj.Root _emotesObj { get; set; } = null;
        private static SkinObj.Root _pickaxeObj { get; set; } = null;
        private static SkinObj.Root _musicObj { get; set; } = null;
        private static SkinObj.Root _gliderObj { get; set; } = null;
        private static AllSkinsJson.Root _GameSkin { get; set; } = null;
        public SkinObj.Root skinObj
        {
            get
            {
                if (_skinObj == null)
                    _skinObj = MessagePack.MsgPacklz4<SkinObj.Root>($"{FortniteAPI.FortniteAPIcomBaseURL}/cosmetics/br/search/all?backendType=AthenaCharacter&responseOptions=ignore_null&responseFormat=msgpack_lz4");
                return _skinObj;
            }
        }

        public SkinObj.Root backblingObj
        {
            get
            {
                if (_backblingObj == null)
                    _backblingObj = MessagePack.MsgPacklz4<SkinObj.Root>($"{FortniteAPI.FortniteAPIcomBaseURL}/cosmetics/br/search/all?backendType=AthenaBackpack&responseOptions=ignore_null&responseFormat=msgpack_lz4");
                return _backblingObj;
            }
        }
        public SkinObj.Root emotesObj
        {
            get
            {
                if (_emotesObj == null)
                    _emotesObj = MessagePack.MsgPacklz4<SkinObj.Root>($"{FortniteAPI.FortniteAPIcomBaseURL}/cosmetics/br/search/all?backendType=AthenaDance&responseOptions=ignore_null&responseFormat=msgpack_lz4");
                return _emotesObj;
            }
        }

        public SkinObj.Root pickaxeObj
        {
            get
            {
                if (_pickaxeObj == null)
                    _pickaxeObj = MessagePack.MsgPacklz4<SkinObj.Root>($"{FortniteAPI.FortniteAPIcomBaseURL}/cosmetics/br/search/all?backendType=AthenaPickaxe&responseOptions=ignore_null&responseFormat=msgpack_lz4");
                return _pickaxeObj;
            }
        }

        public SkinObj.Root musicObj
        {
            get
            {
                if (_musicObj == null)
                    _musicObj = MessagePack.MsgPacklz4<SkinObj.Root>($"{FortniteAPI.FortniteAPIcomBaseURL}/cosmetics/br/search/all?backendType=AthenaMusicPack&responseOptions=ignore_null&responseFormat=msgpack_lz4");
                return _musicObj;
            }
        }
        public SkinObj.Root gliderObj
        {
            get
            {
                if (_gliderObj == null)
                    _gliderObj = MessagePack.MsgPacklz4<SkinObj.Root>($"{FortniteAPI.FortniteAPIcomBaseURL}/cosmetics/br/search/all?backendType=AthenaGlider&responseOptions=ignore_null&responseFormat=msgpack_lz4");
                return _gliderObj;
            }
        }


        public AllSkinsJson.Root AllSkins()
        {
            if (_GameSkin == null)
                _GameSkin = DataMine.GetAllSkins();

            return _GameSkin;
        }

        #endregion




        /// <summary>
        /// Resets all currently swapped items
        /// </summary>
        public static void ResetAll()
        {
            SwapperLogic.SwapLogic.ResetAllSwaps();
            CurrentConfig.swaplogs.Clear();
            SaveConfig();
        }

        public static string ProSwapperFolder => Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Pro_Swapper\";

        public class SwapLogs
        {
            public SkinObj.Datum SwapFrom { get; set; }
            public SkinObj.Datum SwapTo { get; set; }

            public SwapLogs(SkinObj.Datum _SwapsFrom, SkinObj.Datum _SwapsTo)
            {
                SwapFrom = _SwapsFrom;
                SwapTo = _SwapsTo;
            }
        }


        #region ConfigHandler

        private static string ConfigPath => ProSwapperFolder + @"ProSwapperLobby\" + version + "_config.json";

        public static ConfigObj CurrentConfig;
        public static void InitConfig()
        {
            Directory.CreateDirectory(ProSwapperFolder + @"ProSwapperLobby\");
            if (!File.Exists(ConfigPath))
                File.WriteAllText(ConfigPath, JsonConvert.SerializeObject(new ConfigObj()), System.Text.Encoding.Unicode);
            CurrentConfig = JsonConvert.DeserializeObject<ConfigObj>(File.ReadAllText(ConfigPath));
        }
        public static void SaveConfig() => File.WriteAllText(ConfigPath, JsonConvert.SerializeObject(CurrentConfig), System.Text.Encoding.Unicode);


        private static string _Paks { get; set; } = null;

        public class ConfigObj
        {
            public List<SwapLogs> swaplogs { get; set; } = new List<SwapLogs>();

            public string Key { get; set; } = "";
            public Services.TokenResponse.Root OAuthToken { get; set; } = null;//Uses Discord OAuth2
            public bool AskIfServerBooster { get; set; } = true;
            public string Paks
            {
                get
                {
                    try
                    {
                        if (_Paks == null)
                            _Paks = SwapperLogic.EpicGamesLauncher.FindPakFiles();

                        return _Paks;
                    }
                    catch
                    {
                        return null;
                    }

                }
            }
            public FAesKey? aesKey => FortniteAPI.GetCurrentAESKey();

            public string CurrentInstalledFortniteVersion
            {
                get
                {
                    try
                    {
                        return SwapperLogic.EpicGamesLauncher.GetCurrentInstalledFortniteVersion();
                    }
                    catch { return null; }
                }
            }

            public string CurrentLiveFortniteVersion => FortniteAPI.GetCurrentFortniteVersion();
        }

        #endregion
    }
}
