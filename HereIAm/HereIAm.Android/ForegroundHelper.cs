using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HereIAm.Droid;
using HereIAm.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[assembly: Xamarin.Forms.Dependency(typeof(ForegroundHelper))]
namespace HereIAm.Droid
{
    class ForegroundHelper : IForeground
    {
        public void StartService()
        {
            Intent intent = new Intent(Android.App.Application.Context, typeof(ForegroundService));

            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
            {
                Android.App.Application.Context.StartForegroundService(intent);
            }
            else
            {
                Android.App.Application.Context.StartService(intent);
            }
        }

        public void StopService()
        {
            Intent intent = new Intent(Android.App.Application.Context, typeof(ForegroundService));

            Android.App.Application.Context.StopService(intent);
        }
    }
}