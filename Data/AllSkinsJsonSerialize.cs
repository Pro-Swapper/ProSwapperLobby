using CUE4Parse.UE4.Assets.Exports;

namespace ProSwapperLobby.Data
{
    public class AllSkinsJsonSerialize
    {
        public class Root
        {
            public List<KeyValuePair<string, IEnumerable<UObject>>> data { get; set; } = new List<KeyValuePair<string, IEnumerable<UObject>>>();
        }


    }
}
