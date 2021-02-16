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
    public partial class LoginView : ContentPage
    {
        private AuthService authService = new AuthService();

        public LoginView()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel();
        }

        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if(e.Value == false)
            {
                DependencyService.Get<IMessage>().Alert("재작동은 로그아웃 이후부터 반영됩니다.");
            }

            authService.SetDataToProperties("rememberInfo", e.Value);
        }
    }
}