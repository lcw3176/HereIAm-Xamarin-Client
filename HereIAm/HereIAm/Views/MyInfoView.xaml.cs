using HereIAm.Services;
using HereIAm.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HereIAm.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyInfoView : ContentPage
    {
        public MyInfoView()
        {
            InitializeComponent();
            BindingContext = new MyInfoViewModel();
        }

  

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            AuthService auth = new AuthService();
            auth.SetDataToProperties("background", e.Value);

            DependencyService.Get<IMessage>().Alert("변경사항은 다시 로그인해야 적용됩니다");
            GpsService.run = false;

        }
    }
}