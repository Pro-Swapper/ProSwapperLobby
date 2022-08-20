using CUE4Parse;
using CUE4Parse.FileProvider;
using CUE4Parse.UE4.Versions;
using ProSwapperLobby.Data;
using System.Text;
using ProSwapperLobby.Services;
using CUE4Parse.UE4.Assets.Exports;
using Newtonsoft.Json;

namespace ProSwapperLobby.Data
{
    public static class DataMine
    {
        public static AllSkinsJson.Root GetAllSkins()
        {
            return null;
            const string AllSkinsPath = @"C:\Users\ProMa\Documents\GitHub\ProSwapperLobby\bin\Debug\net6.0-windows\AllSkins.json";

            if (!File.Exists(AllSkinsPath))
            {
                //We use AllSkinsJsonSerialize.Root type to serialize
                var provider = SwapperLogic.SwapLogic.GetProvider(MainService.CurrentConfig.Paks, new string[] { "pakchunk10-WindowsClient", "pakchunk20-WindowsClient" });
                provider.LoadMappings();
                AllSkinsJsonSerialize.Root AllSkinsPairs = new();




                foreach (var cid in MainService.GetItemByType(MainService.ItemType.AthenaCharacter))
                {
                    if (provider.TryLoadPackage($"FortniteGame/Content/Athena/Items/Cosmetics/Characters/{cid.id}.uasset", out CUE4Parse.UE4.Assets.IPackage package))
                    {
                        var exports = package.GetExports();
                        //var exportss = provider.LoadObjectExports($"FortniteGame/Content/Athena/Items/Cosmetics/Characters/{cid.id}.uasset");


                        //string json = JsonConvert.SerializeObject(exports);
                        //skins.Add(JsonConvert.DeserializeObject<GameSkin.Root>(json));
                        //skins.Add((GameSkin.Root)exports);

                        AllSkinsPairs.data.Add(new KeyValuePair<string, IEnumerable<UObject>>(cid.id, exports));
                    }
                }
                string allJson = JsonConvert.SerializeObject(AllSkinsPairs, Formatting.Indented);
                File.WriteAllText(AllSkinsPath, allJson);
                provider.Dispose();
            }


            //We use AllSkinsJson.Root type to deserialize
            string text = File.ReadAllText(AllSkinsPath);

            return JsonConvert.DeserializeObject<AllSkinsJson.Root>(text, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, MissingMemberHandling = MissingMemberHandling.Ignore });
        }
    }
}
