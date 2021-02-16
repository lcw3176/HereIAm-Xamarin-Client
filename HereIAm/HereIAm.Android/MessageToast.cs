using Android.Widget;
using HereIAm.Droid;
using HereIAm.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(MessageToast))]
namespace HereIAm.Droid
{
    class MessageToast : IMessage
    {
        public void Alert(string message)
        {
            Toast.MakeText(Android.App.Application.Context, message, ToastLength.Short).Show();
        }
    }
}