using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Diagnostics;
namespace ProSwapperLobby.Services
{
    public static class KeyService
    {
        public static string GetIPHash() => ComputeSHA256Hash(new WebClient().DownloadString("http://ip-api.com/line/?fields=8192").Replace("\n", ""));


        public static string ComputeSHA256Hash(string text) => BitConverter.ToString(new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(text))).Replace("-", "");

        public static string CreateKey() => Base64Encode(GetIPHash() + DateTimeOffset.Now.ToUnixTimeSeconds().ToString());

        public static string Base64Encode(string plainText) => Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText));

        public static string Base64Decode(string base64EncodedData) => Encoding.UTF8.GetString(Convert.FromBase64String(base64EncodedData));
        public static void OpenKeyUrl(string url)
        {
            Process process = new Process();
            process.StartInfo.UseShellExecute = true;
            process.StartInfo.FileName = Base64Decode("aHR0cHM6Ly9saW5rLXRvLm5ldC84NjczNy81MzUuOTgzMDU5NTAyNDYxMy9keW5hbWljLz9yPQ==") + Base64Encode(url);
            process.Start();
        }

        public static bool IsValidKey(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return false;
            }

            //594e519ae499312b29433b7dd8a97ff068defcba9755b6d5d00e84c524d67b06 1659768999
            try
            {
                key = Base64Decode(key);
            }
            catch
            {
                return false;
            }


            long KeyExpireTime = long.Parse(key.Substring(64, key.Length - 64)) + MainService.ProSwapperAPI.KeyLifetime;

            long timeNow = DateTimeOffset.Now.ToUnixTimeSeconds();

            /*
             * OriginalKey = 1659768999
             * TimeNow = 1659768899
             * KeyExpireTime = OriginalKey + KeyLifeTime
             * 
             * IF KeyExpireTime > TimeNow:
             *      //Key has not expired
             * ELSE:
             *      //Key has expired
             */

            return KeyExpireTime > timeNow;

        }
    }
}
