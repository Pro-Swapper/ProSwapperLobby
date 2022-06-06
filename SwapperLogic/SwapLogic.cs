using CUE4Parse;
using CUE4Parse.FileProvider;
using CUE4Parse.UE4.Versions;
using ProSwapperLobby.Data;
using System.Text;
using ProSwapperLobby.Services;
namespace ProSwapperLobby.SwapperLogic
{
    public static class SwapLogic
    {
        private const string ProSwapperLobby = "Pro Swapper Lobby";

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
                LoadSpecific = new[] { "."};

            var Provider = new DefaultFileProvider(folderPath, SearchOption.TopDirectoryOnly, false, new VersionContainer(EGame.GAME_UE5_1));
            Provider.Initialize();

            foreach (var vfs in Provider.UnloadedVfs.Where(x => LoadSpecific.Any(x.Name.Contains)))
            {
                Provider.SubmitKey(vfs.EncryptionKeyGuid, MainService.CurrentConfig.aesKey);
            }
            return Provider;
        }


        public static ZlibBlock zlibblock = null;
        public static string SearchCID_s = null;
        public class ZlibBlock
        {
            public long BlockStart { get; set; }
            public long BlockEnd { get; set; }
            public byte[] decompressed { get; set; }
            public byte[] compressed { get; set; }
            public ZlibBlock(long start, long end, byte[] decomp, byte[] comp)
            {
                BlockStart = start;
                BlockEnd = end;
                decompressed = decomp;
                compressed = comp;
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

        public static bool LobbySwap(SkinObj.Datum SwapsFrom, SkinObj.Datum SwapsTo, bool Converting, ref Exception ex)
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


                if (provider.TrySaveAsset("FortniteGame/AssetRegistry.bin", out byte[] _))
                {
                    provider.Dispose();
                    if (zlibblock != null)
                    {
                        //Edit file
                        byte[] searchB = SearchCID;
                        byte[] replaceB = Encoding.Default.GetBytes(SwapsTo.id + "." + SwapsTo.id);


                        if (searchB.Length >= replaceB.Length)
                        {
                            FillEnd(ref replaceB, searchB.Length);

                            byte[] EditedAsset = zlibblock.decompressed;

                            EditedAsset = EditAsset(EditedAsset, searchB, replaceB);

                            //Compress zlib
                            byte[] compressed = AssetRegistryCompression.Compress(EditedAsset);
                            FillEnd(ref compressed, zlibblock.compressed.Length);
                            //Write to pakchunk0
                            using (FileStream PakEditor = new FileStream($"{ModifiedFilePath}.pak", FileMode.Open, FileAccess.ReadWrite))
                            {
                                PakEditor.Position = zlibblock.BlockStart;
                                PakEditor.Write(compressed, 0, compressed.Length);
                            }
                            return true;
                        }
                        else if (Converting == false)
                        {
                            FillEnd(ref searchB, replaceB.Length);

                            byte[] EditedAsset = zlibblock.decompressed;

                            EditedAsset = EditAsset(EditedAsset, searchB, replaceB);

                            //Compress zlib
                            byte[] compressed = AssetRegistryCompression.Compress(EditedAsset);
                            FillEnd(ref compressed, zlibblock.compressed.Length);
                            //Write to pakchunk0
                            using (FileStream PakEditor = new FileStream($"{ModifiedFilePath}.pak", FileMode.Open, FileAccess.ReadWrite))
                            {
                                PakEditor.Position = zlibblock.BlockStart;
                                PakEditor.Write(compressed, 0, compressed.Length);
                            }
                            return true;
                        }
                    }
                    return false;
                }
                return false;
            }
            catch (Exception exc)
            {
                exc = ex;
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
