using CUE4Parse.Encryption.Aes;

namespace ProSwapperLobby.Services
{
    public static class FortniteAPI
    {
        public static HttpClient http = new HttpClient();
        public const string FortniteAPIcomBaseURL = "https://fortnite-api.com/v2";
        public const string FortniteCentralBaseURL = "https://fortnitecentral.gmatrixgames.ga/api/v1";

        private static string Version = null;

        public static FAesKey GetCurrentAESKey()
        {
            switch (MainService.CurrentConfig.AesKeySource)
            {
                case "Fortnite-API.com":
                    string aesKey = FortniteAPIcom.AESResponse.GetData().data.mainKey;
                    if (aesKey != null && aesKey.Length > 0)
                        return new FAesKey(aesKey);
                    else
                    {
                        throw new Exception("Fortnite-api.com's AES Key was invalid");
                    }

                    break;

                case "Fortnite Central":
                    string fnCentralAES = FortniteCentral.Rootobject.GetData().mainKey;
                    if (fnCentralAES != null && fnCentralAES.Length > 0)
                        return new FAesKey(fnCentralAES);
                    break;
            }


            //This AES Key will not work but we dont want the user to have any errors
            return new FAesKey(new string('0', 64));
        }

        public static string GetCurrentFortniteVersion()
        {
            if (Version == null)
            {

                try
                {
                    string build = FortniteCentral.Rootobject.GetData().version;
                    if (build != null && build.Length > 0)
                        return build;
                }
                catch
                {

                    throw new Exception("Fortnite Central's Fortnite Version was invalid");
                }

                try
                {
                    string build = FortniteAPIcom.AESResponse.GetData().data.build;
                    if (build != null && build.Length > 0)
                        return build;
                    else
                    {
                        throw new Exception("Fortnite-api.com's Fortnite Version was invalid");
                    }
                }
                catch { }




                //This AES Key will not work but we dont want the user to have any errors
                return "Unknown Verson";
            }
            else
            {
                return Version;
            }
        }

    }

    namespace FortniteAPIcom
    {

        public class AESResponse
        {
            private static AESResponse.Root _data { get; set; } = null;

            public static AESResponse.Root GetData()
            {
                if (_data == null)
                    _data = MessagePack.MsgPacklz4<AESResponse.Root>($"{FortniteAPI.FortniteAPIcomBaseURL}/aes?responseFormat=msgpack_lz4&responseOptions=ignore_null");

                return _data;
            }

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
    }

    namespace FortniteCentral
    {

        public class Rootobject
        {
            private static Rootobject _data { get; set; } = null;

            public static Rootobject GetData()
            {
                if (_data == null)
                    _data = FortniteAPI.http.GetFromJsonAsync<Rootobject>($"{FortniteAPI.FortniteCentralBaseURL}/aes").Result;

                return _data;
            }


            public string version { get; set; }
            public string mainKey { get; set; }
            public Dynamickey[] dynamicKeys { get; set; }
            public Unloaded[] unloaded { get; set; }
        }

        public class Dynamickey
        {
            public string name { get; set; }
            public string key { get; set; }
            public string guid { get; set; }
            public string keychain { get; set; }
            public int fileCount { get; set; }
            public bool hasHighResTextures { get; set; }
            public Size size { get; set; }
        }

        public class Size
        {
            public int raw { get; set; }
            public string formatted { get; set; }
        }

        public class Unloaded
        {
            public string name { get; set; }
            public string guid { get; set; }
            public int fileCount { get; set; }
            public bool hasHighResTextures { get; set; }
            public Size size { get; set; }
        }

    }

}
