using Ionic.Zlib;

namespace ProSwapperLobby.SwapperLogic
{
    public class AssetRegistryCompression
    {
        public static byte[] Decompress(byte[] input)=> ZlibStream.UncompressBuffer(input);

        /// <summary>
        /// Compresses a byte array with CompressionLevel.BestCompression
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static byte[] Compress(byte[] input) => ZlibStream.CompressBuffer(input);
    }
}
