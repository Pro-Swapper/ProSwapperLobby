using CUE4Parse.Compression;
using Ionic.Zlib;

namespace ProSwapperLobby.SwapperLogic
{
    public class AssetRegistryCompression
    {
        public static byte[] Compress(byte[] input, CompressionMethod compressionMethod)
        {
            switch (compressionMethod)
            {
                case CompressionMethod.Zlib:
                    return ZlibStream.CompressBuffer(input);
                case CompressionMethod.Oodle:
                    if (!File.Exists(OodleImports.OodleDll))
                    {
                        using (HttpClient client = new HttpClient())
                        {
                            byte[] dll = client.GetByteArrayAsync($"https://cdn.proswapper.xyz/{OodleImports.OodleDll}").Result;
                            File.WriteAllBytes(OodleImports.OodleDll, dll);
                        }
                    }


                    return Oodle.Compress(input);
                default:
                    return null;
            }

        }
    }
}
