using MessagePack;
using Newtonsoft.Json;

namespace ProSwapperLobby.Services
{
    public static class MessagePack
    {
        public static readonly HttpClient client = new HttpClient();
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
    }
}
