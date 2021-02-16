using HereIAm.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HereIAm.Services
{
    class MemberService
    {

        public async Task<Member> GetMyInfo(string email)
        {

            string url = string.Format("https://www.joebrooks.tk/m/members/myinfo/{0}", email);

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            
            HttpClient client = new HttpClient(clientHandler);

            var response = await client.GetAsync(url);
            var jsonString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Member>(jsonString);
        }

        public async Task<Member> GetMemberByName(string name)
        {
            string url = string.Format("https://www.joebrooks.tk/m/members/location/{0}", name);

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            HttpClient client = new HttpClient(clientHandler);

            var response = await client.GetAsync(url);
            var jsonString = await response.Content.ReadAsStringAsync();
            
            return JsonConvert.DeserializeObject<Member>(jsonString);
        }

    }
}
