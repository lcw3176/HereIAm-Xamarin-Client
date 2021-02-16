using HereIAm.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HereIAm.Services
{
    class LocationSerivce
    {
        public async Task<bool> SendLocation(string name, string lat, string lng, string place)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            HttpClient client = new HttpClient(clientHandler);

            var parameters = new Dictionary<string, string>();
            parameters.Add("name", name);
            parameters.Add("lat", lat);
            parameters.Add("lng", lng);
            parameters.Add("place", place);

            var encodedContent = new FormUrlEncodedContent(parameters);
            string url = "https://www.joebrooks.tk/m/location";

            var result = await client.PostAsync(url, encodedContent);
            return true;
        }


        public async Task<List<Location>> GetLocationByName(string name, DateTime date, TimeSpan fromtiem, TimeSpan totime)
        {
            string sDate = string.Format("{0}-{1}-{2}", date.Year, date.Month, date.Day);
            string url = string.Format("https://www.joebrooks.tk/m/location/{0}/{1}/{2}/{3}", name, sDate, fromtiem.ToString(), totime.ToString());
            
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            HttpClient client = new HttpClient(clientHandler);

            var response = await client.GetAsync(url);
            var jsonString = await response.Content.ReadAsStringAsync();

            JObject obj = JObject.Parse(jsonString);

            
            var arr = obj["data"].Value<JArray>();
            var data = arr.ToObject<List<Location>>();

            if(data[0].time == "null")
            {
                return null;
            }

            return data;
        }
    }
}
