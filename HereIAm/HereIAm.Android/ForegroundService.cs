using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HereIAm.Services;
using AndroidApp = Android.App.Application;
using Android.Support.V4.App;
using Android.Graphics;

namespace HereIAm.Droid
{
    [Service]
    class ForegroundService : Service
    {
        public const string ResourceName = "resourceName";
        const string channelId = "default";
        const string channelName = "Default";
        const string channelDescription = "The default channel for notifications.";
        int serviceNotifID = 1234;

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            StartNotification();

            return StartCommandResult.Sticky;
        }

        private void StartNotification()
        {


            NotificationCompat.Builder builder = new NotificationCompat.Builder(AndroidApp.Context, channelId);

            var notification = builder.Build();
            notification.Flags = NotificationFlags.NoClear;

            var manager = (NotificationManager)AndroidApp.Context.GetSystemService(AndroidApp.NotificationService);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                var channelNameJava = new Java.Lang.String(channelName);
                var channel = new NotificationChannel(channelId, channelNameJava, NotificationImportance.Default)
                {
                    Description = channelDescription
                };

                channel.SetSound(null, null);
                channel.SetVibrationPattern(new long[] { 0 });
                channel.EnableVibration(true);
                manager.CreateNotificationChannel(channel);
            }

            StartForeground(serviceNotifID, notification);

        }
    }
}