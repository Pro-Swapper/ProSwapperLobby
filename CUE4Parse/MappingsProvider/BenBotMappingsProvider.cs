﻿using System;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Serilog;

namespace CUE4Parse.MappingsProvider
{
    public class BenBotMappingsProvider : UsmapTypeMappingsProvider
    {
        private readonly string? _specificVersion;
        private readonly string _gameName;
        private readonly bool _isWindows64Bit;

        public BenBotMappingsProvider(string gameName, string? specificVersion = null)
        {
            _specificVersion = specificVersion;
            _gameName = gameName;
            _isWindows64Bit = Environment.Is64BitOperatingSystem && RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            Reload();
        }
        
        public const string BenMappingsEndpoint = "https://benbot.app/api/v1/mappings";
        
        private readonly HttpClient _client = new HttpClient { Timeout = TimeSpan.FromSeconds(2), DefaultRequestHeaders = { { "User-Agent", "CUE4Parse" } }};
        
        public sealed override bool Reload()
        {
            return ReloadAsync().GetAwaiter().GetResult();
        }

        public sealed override async Task<bool> ReloadAsync()
        {
            try
            {
                //var jsonText = _specificVersion != null
                //    ? await LoadEndpoint(BenMappingsEndpoint + $"?version={_specificVersion}")
                //    : await LoadEndpoint(BenMappingsEndpoint);
                //string jsonText = new System.Net.WebClient().DownloadString(BenMappingsEndpoint);

                //if (jsonText == null)
                //{
                //    Log.Warning("Failed to get BenBot Mappings Endpoint");
                //    return false;
                //}
                //var json =  JArray.Parse(jsonText);
                //var preferredCompression = _isWindows64Bit ? "Oodle" : "Brotli";

                //if (!json.HasValues)
                //{
                //    Log.Warning("Couldn't reload mappings, json array was empty");
                //    return false;
                //}

                //string? usmapUrl = null;
                //string? usmapName = null;
                //foreach (var arrayEntry in json)
                //{
                //    var method = arrayEntry["meta"]?["compressionMethod"]?.ToString();
                //    if (method != null && method == preferredCompression)
                //    {
                //        usmapUrl = arrayEntry["url"]?.ToString();
                //        usmapName = arrayEntry["fileName"]?.ToString();
                //        break;
                //    }
                //}

                //if (usmapUrl == null)
                //{
                //    usmapUrl = json[0]["url"]?.ToString()!;
                //    usmapName = json[0]["fileName"]?.ToString()!;
                //}
                string InstalledVersion = ProSwapperLobby.Services.MainService.CurrentConfig.CurrentInstalledFortniteVersion;

                string usmapUrl = $"https://benbot.app/api/v1/mappings/{InstalledVersion}_oo.usmap";
                string usmapName = $"{InstalledVersion}_oo.usmap";

               // var usmapBytes = await LoadEndpointBytes(usmapUrl);
                var usmapBytes = new System.Net.WebClient().DownloadData(usmapUrl);
                if (usmapBytes == null)
                {
                    Log.Warning("Failed to download usmap");
                    return false;
                }

                AddUsmap(usmapBytes, _gameName, usmapName!);
                return true;
            }
            catch (Exception e)
            {
                Log.Warning(e, "Uncaught exception while reloading mappings from BenBot");
                return false;
            }
        }
        
        private async Task<string?> LoadEndpoint(string url)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, url);
            try
            {
                var response = await _client.SendAsync(request, HttpCompletionOption.ResponseContentRead).ConfigureAwait(false);
                return await response.Content.ReadAsStringAsync();
            }
            catch
            {
                return null;
            }
        }
        
        private async Task<byte[]?> LoadEndpointBytes(string url)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, url);
            try
            {
                var response = await _client.SendAsync(request, HttpCompletionOption.ResponseContentRead).ConfigureAwait(false);
                return await response.Content.ReadAsByteArrayAsync();
            }
            catch
            {
                return null;
            }
        }
    }
}