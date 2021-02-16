using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HereIAm.Services
{
    class KakaoService
    {
        public async Task<string> Search(string lng, string lat)
        {

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            HttpClient client = new HttpClient(clientHandler);

            string url = string.Format("https://dapi.kakao.com/v2/local/geo/coord2regioncode.json?x={0}&y={1}", lng, lat);
            string rkey = "개발자 키";
            string header = "KakaoAK " + rkey;

            client.DefaultRequestHeaders.Add("Authorization", header);
            var response = await client.GetAsync(url);
            var json = await response.Content.ReadAsStringAsync();

            JObject js = JObject.Parse(json);
            JArray arr = JArray.Parse(js["documents"].ToString());

            JObject value = JObject.Parse(arr[0].ToString());

            return value["address_name"].ToString();
        }
    }
}
