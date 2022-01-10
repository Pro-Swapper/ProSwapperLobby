using System.Security.Cryptography;

namespace ProSwapperLobby.SwapperLogic
{
    public class Hashing
    {
        public static string FileToMd5(string filename)
        {
            if (File.Exists(filename))
                return BitConverter.ToString(MD5.Create().ComputeHash(File.OpenRead(filename))).Replace("-", string.Empty).ToUpperInvariant();
            else
                return string.Empty;
        }
    }
}
