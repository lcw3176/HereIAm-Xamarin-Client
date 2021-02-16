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
    public partial class SearchRoomView : ContentPage
    {
        public SearchRoomView()
        {
            InitializeComponent();
            BindingContext = new SearchRoomViewModel();
        }
    }
}