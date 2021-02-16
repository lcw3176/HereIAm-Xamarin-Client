using HereIAm.Services;
using HereIAm.Views;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HereIAm
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override void OnStart()
        {

            if (!GpsService.run)
            {
                Application.Current.MainPage = new LoginView();
            }

            else
            {
                Application.Current.MainPage = new HomeView();
            }

            RequestPermission();
            //RequestBatteryOpt();
        }

        private async void RequestBatteryOpt()
        {

            //if (!DependencyService.Get<IBatteryInfo>().CheckIsEnableBatteryOptimizations())
            //{

            //    var action = await Application.Current.MainPage.DisplayAlert("배터리 세팅", "배터리 최적화를 꺼주세요", "yes", "No");

            //    if (action)
            //    {
            //        DependencyService.Get<IBatteryInfo>().StartSetting();
            //    }
            //    else
            //    {

            //    }
            //}

        }

        private async void RequestPermission()
        {

            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync<LocationPermission>();
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                    {

                    }

                    status = await CrossPermissions.Current.RequestPermissionAsync<LocationPermission>();
                }

            }
            catch (Exception ex)
            {
                //Something went wrong
            }

        }

        protected override void OnSleep()
        {
            DependencyService.Get<IForeground>().StartService();
        }

        protected override void OnResume()
        {
            DependencyService.Get<IForeground>().StopService();
        }
    }
}
