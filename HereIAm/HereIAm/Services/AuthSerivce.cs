using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HereIAm.Services
{
    class AuthService
    {
        public object GetDataFromProperties(string key)
        {

            if (!Application.Current.Properties.ContainsKey(key))
            {
                return null;
            }

            Application.Current.Properties.TryGetValue(key, out object value);

            return value;
        }

        public void SetDataToProperties(string key, object value)
        {
            if(value == null)
            {
                return;
            }

            if (Application.Current.Properties.ContainsKey(key))
            {
                Application.Current.Properties[key] = value;
            }

            else
            {
                Application.Current.Properties.Add(key, value);
            }
        }


        public async Task<bool> GetAuth(string email, string pw)
        {
            try
            {
                string url = string.Format("https://www.joebrooks.tk/m/login/{0}/{1}", email, pw);
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                HttpClient client = new HttpClient(clientHandler);

                var response = await client.GetAsync(url);
                var jsonString = await response.Content.ReadAsStringAsync();
                var data = JObject.Parse(jsonString);

                if (data["result"].ToString() == "notMember")
                {
                    return false;
                }
                
                return true;
            }

            catch (Exception e)
            {
                return false;
            }
        }
    }
}
