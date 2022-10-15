using CUE4Parse;
using CUE4Parse.FileProvider;
using CUE4Parse.UE4.Versions;
using ProSwapperLobby.Data;
using System.Text;
using ProSwapperLobby.Services;
using CUE4Parse.Compression;

namespace ProSwapperLobby.SwapperLogic
{
    public static class SwapLogic
    {
        public const string ProSwapperLobby = "Pro Swapper Lobby";

        private const string AssetRegFile = "pakchunk0-WindowsClient";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="folderPath"></param>
        /// <param name="LoadSpecific">Load only a specific file by name without extension</param>
        /// <returns></returns>
        public static DefaultFileProvider GetProvider(string folderPath, string[] LoadSpecific = null)
        {
            if (LoadSpecific == null)
                LoadSpecific = new[] { "." };

            var Provider = new DefaultFileProvider(folderPath, SearchOption.TopDirectoryOnly, false, new VersionContainer(EGame.GAME_UE5_1));
            Provider.Initialize();
            //Provider.LoadMappings(); Don't need mappings for asset reg
            foreach (var vfs in Provider.UnloadedVfs.Where(x => LoadSpecific.Any(x.Name.Contains)))
            {
                Provider.SubmitKey(vfs.EncryptionKeyGuid, MainService._aesKey);
            }
            return Provider;
        }


        public static AssetRegBlock assetRegBlock = null;
        public static string SearchCID_s = null;
        public class AssetRegBlock
        {
            public long BlockStart { get; set; }
            public long BlockEnd { get; set; }
            public byte[] decompressed { get; set; }
            public byte[] compressed { get; set; }
            public int compressedLength => compressed.Length;
            public CompressionMethod compressionMethod { get; set; }
            public AssetRegBlock(long BlockStart, long BlockEnd, byte[] decompressed, byte[] compressed, CompressionMethod compressionMethod)
            {
                this.BlockStart = BlockStart;
                this.BlockEnd = BlockEnd;
                this.decompressed = decompressed;
                this.compressed = compressed;
                this.compressionMethod = compressionMethod;
            }
        }

        public static void ResetAllSwaps()
        {
            string LobbyPaksPath = Path.Combine(Services.MainService.CurrentConfig.Paks, ProSwapperLobby);
            if (Directory.Exists(LobbyPaksPath))
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                Directory.Delete(LobbyPaksPath, true);
            }
        }

        public static bool LobbySwap(SkinObj.Datum SwapsFrom, SkinObj.Datum SwapsTo, bool Converting, ref string ex)
        {
            try
            {
                string LobbyPaksPath = Path.Combine(Services.MainService.CurrentConfig.Paks, ProSwapperLobby);
                string OriginalFilePath = $"{Services.MainService.CurrentConfig.Paks}\\{AssetRegFile}";
                string ModifiedFilePath = $"{Services.MainService.CurrentConfig.Paks}\\{ProSwapperLobby}\\{AssetRegFile}";

                string OriginalSig = Hashing.FileToMd5($"{OriginalFilePath}.sig");
                string ModifiedSig = Hashing.FileToMd5($"{ModifiedFilePath}.sig");

                if (OriginalSig != ModifiedSig)
                {
                    //Revert lobby swaps all

                    if (Directory.Exists(LobbyPaksPath))
                    {
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        Directory.Delete(LobbyPaksPath, true);
                    }
                }

                if (!File.Exists($"{ModifiedFilePath}.pak"))
                {
                    Directory.CreateDirectory($"{Services.MainService.CurrentConfig.Paks}\\{ProSwapperLobby}");

                    string[] ue5fileExtensions = new string[] { "pak", "sig", "ucas", "utoc" };

                    foreach (var extension in ue5fileExtensions)
                    {
                        File.Copy($"{OriginalFilePath}.{extension}", $"{ModifiedFilePath}.{extension}");
                    }
                }

                var provider = GetProvider(LobbyPaksPath);
                byte[] SearchCID = Encoding.Default.GetBytes(SwapsFrom.id + "." + SwapsFrom.id);
                SearchCID_s = SwapsFrom.id + "." + SwapsFrom.id;

                const string AssetRegPath = "FortniteGame/AssetRegistry.bin";
                if (provider.TrySaveAsset(AssetRegPath, out byte[] assetReg))
                {

                    provider.Dispose();
                    if (assetRegBlock != null)
                    {
                        //Edit file
                        byte[] searchB = SearchCID;
                        byte[] replaceB = Encoding.Default.GetBytes(SwapsTo.id + "." + SwapsTo.id);


                        if (searchB.Length >= replaceB.Length)//Converting
                        {
                            FillEnd(ref replaceB, searchB.Length);


                            byte[] EditedAsset = EditAsset(assetRegBlock.decompressed, searchB, replaceB);
                            //Compress zlib
                            byte[] compressed = AssetRegistryCompression.Compress(EditedAsset, assetRegBlock.compressionMethod);
                            FillEnd(ref compressed, assetRegBlock.compressedLength);
                            //Write to pakchunk0
                            RevertEngine.CreateRevertItem(new RevertItem(assetRegBlock.BlockStart, assetRegBlock.compressed, $"{ModifiedFilePath}.pak", AssetRegPath));
                            using (FileStream PakEditor = new FileStream($"{ModifiedFilePath}.pak", FileMode.Open, FileAccess.ReadWrite))
                            {
                                PakEditor.Position = assetRegBlock.BlockStart;
                                PakEditor.Write(compressed, 0, compressed.Length);
                            }
                            return true;
                        }
                        else if (Converting == false)//Reverting
                        {
                            return RevertEngine.RevertItem(assetRegBlock);
                        }
                        else
                        {
                            ex = $"Odd error {SwapsFrom.id} is smaller than {SwapsTo.id}";
                            return false;
                        }
                    }
                    else
                    {
                        ex = $"Could not find {SearchCID_s} in the Asset Registry.";
                        return false;
                    }

                }
                else
                {
                    ex = $"Could not export Asset Registry";
                    return false;
                }
            }
            catch (Exception exception)
            {
                ex = exception.Message;
                return false;
            }
        }


        private static byte[] EditAsset(byte[] buffer, byte[] search, byte[] replace)
        {
            if (search.Length != replace.Length)
                throw new Exception("Search and replace lengths are different!");

            using (MemoryStream ms = new MemoryStream(buffer))
            {
                ms.Position = IndexOfSequence(buffer, search);
                ms.Write(replace, 0, replace.Length);
                return ms.ToArray();
            }
        }

        public static void FillEnd(ref byte[] buffer, int len) => Array.Resize(ref buffer, len);

        public static int IndexOfSequence(byte[] buffer, byte[] pattern)
        {
            int i = Array.IndexOf(buffer, pattern[0], 0);
            while (i >= 0 && i <= buffer.Length - pattern.Length)
            {
                byte[] segment = new byte[pattern.Length];
                Buffer.BlockCopy(buffer, i, segment, 0, pattern.Length);
                if (segment.SequenceEqual(pattern))
                    return i;
                i = Array.IndexOf(buffer, pattern[0], i + 1);
            }
            return -1;
        }
    }
}
