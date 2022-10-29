using MessagePack;
using System.Net;
using Newtonsoft.Json;
using System.Net.Http;
using ProSwapperLobby.Data;
using System.Reflection;
using CUE4Parse.Encryption.Aes;

namespace ProSwapperLobby.Services
{
    public static class MainService
    {
        public static readonly HttpClient httpClient = new();
        public static string version = Assembly.GetEntryAssembly().GetName().Version.ToString().Substring(0, 5);

        public static DiscordWidgetAPI.Root DiscordWidgetAPI { get; set; }
        public static Data.API.Rootobject ProSwapperAPI { get; set; }

        public enum ItemType
        {
            AthenaCharacter,
            AthenaBackpack,
            AthenaDance,
            AthenaPickaxe,
            AthenaMusicPack,
            AthenaGlider,
            BannerToken
        }

        public static SkinObj.Root _allData { get; set; } = null;
        public static IEnumerable<SkinObj.Datum> GetItemByType(ItemType type)
        {
            if (_allData == null)
                _allData = MessagePack.MsgPacklz4<SkinObj.Root>($"{FortniteAPI.FortniteAPIcomBaseURL}/cosmetics/br?responseOptions=ignore_null&responseFormat=msgpack_lz4");

            string ItemType = type.ToString();
            foreach (var item in _allData.data)
            {
                if (item.type.backendValue == ItemType)
                    yield return item;
            }
        }

        public static string ItemTypeToString(ItemType item)
        {
            switch (item)
            {
                case ItemType.AthenaCharacter: return "Skin";
                case ItemType.AthenaBackpack: return "Backbling";
                case ItemType.AthenaDance: return "Emote";
                case ItemType.AthenaPickaxe: return "Pickaxe";
                case ItemType.AthenaMusicPack: return "Music";
                case ItemType.AthenaGlider: return "Glider";
                case ItemType.BannerToken: return "Banner";
                default: return "Item";
            }
        }

        public static ItemType StringToItemType(string item)
        {
            switch (item)
            {
                case "Skin": return ItemType.AthenaCharacter;
                case "Backbling": return ItemType.AthenaBackpack;
                case "Emote": return ItemType.AthenaDance;
                case "Pickaxe": return ItemType.AthenaPickaxe;
                case "Music": return ItemType.AthenaMusicPack;
                case "Glider": return ItemType.AthenaGlider;
                default: return ItemType.AthenaCharacter;
            }
        }


        public static AllSkinsJson.Root GameSkin => DataMine.GetAllSkins();


        //public static SkinObj.Root GetItems()


        /// <summary>
        /// Resets all currently swapped items
        /// </summary>
        public static void ResetAll()
        {
            if (SwapperLogic.SwapLogic.ResetAllSwaps())
            {
                CurrentConfig.swaplogs.Clear();
            }

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
        public static FAesKey? _aesKey => FortniteAPI.GetCurrentAESKey();
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
            public string AesKeySource = "Fortnite Central";
        }

        #endregion
    }
}
