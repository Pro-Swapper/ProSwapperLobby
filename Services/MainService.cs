using MessagePack;
using System.Net;
using Newtonsoft.Json;
using System.Net.Http;
using ProSwapperLobby.Data;
using CUE4Parse;
using System.Reflection;
using CUE4Parse.Encryption.Aes;

namespace ProSwapperLobby.Services
{
    public class MainService
    {
        public static readonly HttpClient client = new HttpClient();
        public static string version = Assembly.GetExecutingAssembly().GetName().Version.ToString().Substring(2, 5);
        private const string FortniteAPIURL = "https://fortnite-api.com/v2";

        private static SkinObj.Root _skinObj { get; set; } = null;
        private static SkinObj.Root _backblingObj { get; set; } = null;
        private static SkinObj.Root _emotesObj { get; set; } = null;
        private static SkinObj.Root _pickaxeObj { get; set; } = null;
        private static SkinObj.Root _musicObj { get; set; } = null;
        public SkinObj.Root skinObj
        {
            get
            {
                if (_skinObj == null)
                    _skinObj = MsgPacklz4<SkinObj.Root>($"{FortniteAPIURL}/cosmetics/br/search/all?backendType=AthenaCharacter&responseOptions=ignore_null&responseFormat=msgpack_lz4");
                return _skinObj;
            }
        }

        public SkinObj.Root backblingObj
        {
            get
            {
                if (_backblingObj == null)
                    _backblingObj = MsgPacklz4<SkinObj.Root>($"{FortniteAPIURL}/cosmetics/br/search/all?backendType=AthenaBackpack&responseOptions=ignore_null&responseFormat=msgpack_lz4");
                return _backblingObj;
            }
        }
        public SkinObj.Root emotesObj
        {
            get
            {
                if (_emotesObj == null)
                    _emotesObj = MsgPacklz4<SkinObj.Root>($"{FortniteAPIURL}/cosmetics/br/search/all?backendType=AthenaDance&responseOptions=ignore_null&responseFormat=msgpack_lz4");
                return _emotesObj;
            }
        }

        public SkinObj.Root pickaxeObj
        {
            get
            {
                if (_pickaxeObj == null)
                    _pickaxeObj = MsgPacklz4<SkinObj.Root>($"{FortniteAPIURL}/cosmetics/br/search/all?backendType=AthenaPickaxe&responseOptions=ignore_null&responseFormat=msgpack_lz4");
                return _pickaxeObj;
            }
        }
        
        public SkinObj.Root musicObj
        {
            get
            {
                if (_musicObj == null)
                    _musicObj = MsgPacklz4<SkinObj.Root>($"{FortniteAPIURL}/cosmetics/br/search/all?backendType=AthenaMusicPack&responseOptions=ignore_null&responseFormat=msgpack_lz4");
                return _musicObj;
            }
        }

        /// <summary>
        /// Input a url which responds with msgpack compressed in lz4, responds with json object which is 'T'
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        public static T MsgPacklz4<T>(string url)
        {
            Stream responseBody = client.GetStreamAsync(url).Result;
            var lz4Options = MessagePackSerializerOptions.Standard.WithCompression(MessagePackCompression.Lz4BlockArray);
            var AllCosmeticsLz4 = MessagePackSerializer.Deserialize<dynamic>(responseBody, lz4Options);
            string json = MessagePackSerializer.SerializeToJson(AllCosmeticsLz4, lz4Options);
            return JsonConvert.DeserializeObject<T>(json);

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

        public class AESResponse
        {
            public class Root
            {
                public int status { get; set; }
                public Data data { get; set; }
            }

            public class Data
            {
                public string build { get; set; }
                public string mainKey { get; set; }
                public Dynamickey[] dynamicKeys { get; set; }
                public DateTime updated { get; set; }
            }

            public class Dynamickey
            {
                public string pakFilename { get; set; }
                public string pakGuid { get; set; }
                public string key { get; set; }
            }
        }
       

        #region ConfigHandler

        private static string ConfigPath => ProSwapperFolder + @".Config\" + version + "_config.json";

        public static ConfigObj CurrentConfig;
        public static void InitConfig()
        {
            if (!File.Exists(ConfigPath))
                File.WriteAllText(ConfigPath, JsonConvert.SerializeObject(new ConfigObj()), System.Text.Encoding.Unicode);
            CurrentConfig = JsonConvert.DeserializeObject<ConfigObj>(File.ReadAllText(ConfigPath));
        }
        public static void SaveConfig() => File.WriteAllText(ConfigPath, JsonConvert.SerializeObject(CurrentConfig), System.Text.Encoding.Unicode);


        private static FAesKey _aesKey { get; set; } = null;
        private static string _Paks { get; set; } = null;
        private static string _fnVer { get; set; } = null;
        private static AESResponse.Root aesResponse { get; set; } = null;




        public class ConfigObj
        {
            public List<SwapLogs> swaplogs { get; set; } = new List<SwapLogs>();

            public string Paks
            {
                get
                {
                    if (_Paks == null)
                        _Paks = SwapperLogic.EpicGamesLauncher.FindPakFiles();

                    return _Paks;
                }
            }
            public FAesKey aesKey
            {
                get
                {
                    if (aesResponse == null)
                        aesResponse = MsgPacklz4<AESResponse.Root>($"{FortniteAPIURL}/aes?responseFormat=msgpack_lz4&responseOptions=ignore_null");

                    if (_aesKey == null)
                        _aesKey = new FAesKey(aesResponse.data.mainKey);

                    return _aesKey;
                }
            }

            public string CurrentInstalledFortniteVersion
            {
                get
                {
                    if (_fnVer == null)
                        _fnVer = SwapperLogic.EpicGamesLauncher.GetCurrentInstalledFortniteVersion();

                    return _fnVer;
                }
            }

            public string CurrentLiveFortniteVersion
            {
                get
                {
                    if (aesResponse == null)
                        aesResponse = MsgPacklz4<AESResponse.Root>($"{FortniteAPIURL}/aes?responseFormat=msgpack_lz4&responseOptions=ignore_null");

                    return aesResponse.data.build;
                }
            }
        }

        #endregion
    }
}
