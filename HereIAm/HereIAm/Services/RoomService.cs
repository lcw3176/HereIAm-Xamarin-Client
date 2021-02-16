using HereIAm.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HereIAm.Services
{
    class RoomService
    {
        public async Task<List<Member>> GetMembers(string roomName, string userName)
        {

            string url =  string.Format("https://www.joebrooks.tk/m/room/members/{0}/{1}", roomName, userName);

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            HttpClient client = new HttpClient(clientHandler);

            var response = await client.GetAsync(url);
            var jsonString = await response.Content.ReadAsStringAsync();
            JObject obj = JObject.Parse(jsonString);

            var arr = obj["data"].Value<JArray>();
            List<Member> data = arr.ToObject<List<Member>>();

            if(data[0].error == "failed")
            {
                return null;
            }

            return data;
        }

        public async Task<bool> CheckinRoom(string roomName, string pw)
        {
            try
            {

                Application.Current.Properties.TryGetValue("name", out object userName);

                string url = "https://www.joebrooks.tk/m/room/checkin";
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                HttpClient client = new HttpClient(clientHandler);
                var parameters = new Dictionary<string, string>();
                parameters.Add("name", roomName.ToString());
                parameters.Add("member", userName.ToString());
                parameters.Add("pw", pw);

                var encodedContent = new FormUrlEncodedContent(parameters);
                var response = await client.PostAsync(url, encodedContent);
                var jsonString = await response.Content.ReadAsStringAsync();
                JObject obj = JObject.Parse(jsonString);

                if (obj["data"].ToString() != "success")
                {
                    return false;
                }
               
                return true;
            }

            catch
            {
                return false;
            }

        }


        public async Task<bool> CheckoutRoom()
        {
            try
            {
                Application.Current.Properties.TryGetValue("room", out object roomName);
                Application.Current.Properties.TryGetValue("name", out object userName);

                if(roomName == null)
                {
                    return false;
                }

                string url = string.Format("https://www.joebrooks.tk/m/room/checkout/{0}/{1}", roomName.ToString(), userName.ToString());
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                HttpClient client = new HttpClient(clientHandler);

                var response = await client.DeleteAsync(url);
                var jsonString = await response.Content.ReadAsStringAsync();
                JObject obj = JObject.Parse(jsonString);
                
                if(obj["data"].ToString() != "success")
                {
                    return false;
                }

                return true;
            }

            catch
            {
                return false;
            }

        }

    }
}
