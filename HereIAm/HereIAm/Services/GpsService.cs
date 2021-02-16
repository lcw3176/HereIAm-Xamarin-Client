using System;
using System.Threading;
using Xamarin.Essentials;

namespace HereIAm.Services
{
    static class GpsService
    {
        private static GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(10));
        private static CancellationTokenSource cts;
        private static KakaoService kakao = new KakaoService();
        private static LocationSerivce locationService = new LocationSerivce();
        
        private static string name;
        public static bool run = false;
        private static bool isagree;

        public static void Init()
        {
            AuthService authService = new AuthService();
            name = authService.GetDataFromProperties("name").ToString();
            cts = new CancellationTokenSource();
            run = true;
        }

        public static bool IsAgree()
        {
            AuthService authService = new AuthService();
            object background = authService.GetDataFromProperties("background");

            if (background == null)
            {
                authService.SetDataToProperties("background", true);
                return true;
            }

            else
            {
                isagree = bool.Parse(background.ToString());
                return isagree;
            }
        }

        public static async void SendCurrentLocation()
        {
            //while(run)
            //{
                try
                {
                if (!IsAgree()) 
                { 
                    run = false;
                    return;
                }
                    run = true;
                    var location = await Geolocation.GetLocationAsync(request, cts.Token);

                    if (location != null)
                    {
                        string place = await kakao.Search(location.Longitude.ToString(), location.Latitude.ToString());
                        await locationService.SendLocation(name, location.Latitude.ToString(), location.Longitude.ToString(), place);
                    }
                }
                //catch (FeatureNotSupportedException fnsEx)
                //{
                //    // Handle not supported on device exception
                //}
                //catch (FeatureNotEnabledException fneEx)
                //{
                //    // Handle not enabled on device exception
                //}
                //catch (PermissionException pEx)
                //{
                //    // Handle permission exception
                //}
                catch (Exception ex)
                {
                    run = false;
                    //break;
                }
            //}
         
        }

        public static void StopSend()
        {
            if (cts != null && !cts.IsCancellationRequested)
            {
                cts.Cancel();
            }

            run = false;

        }

    }
}
