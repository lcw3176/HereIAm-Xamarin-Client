using HereIAm.Services;
using HereIAm.Views;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace HereIAm.ViewModels
{
    class MyInfoViewModel : BaseVIewModel
    {
        public ICommand logout { get; private set; }
        public ICommand checkout { get; set; }
        public bool useBackgroundService { get; set; }

        private AuthService authSerivce = new AuthService();
        private RoomService roomService = new RoomService();

        public MyInfoViewModel()
        {
            logout = new Command(logoutExecuteMethod);
            checkout = new Command(checkoutExecute);

       
            object background = authSerivce.GetDataFromProperties("background");

            if (background == null)
            {
                useBackgroundService = true;
            }

            else
            {
                useBackgroundService = bool.Parse(background.ToString());
            }
        }


        /// <summary>
        /// 방 나가기
        /// </summary>
        /// <param name="obj"></param>
        private async void checkoutExecute(object obj)
        {
            bool result = await roomService.CheckoutRoom();

            if (!result)
            {
                DependencyService.Get<IMessage>().Alert("에러 발생");
                return;
            }

            EnqueueRoomEventQueue();
            Application.Current.Properties.Remove("room");
            DependencyService.Get<IMessage>().Alert("소속된 방에서 나갔습니다.");
        }

        /// <summary>
        /// 로그아웃 커맨드
        /// </summary>
        /// <param name="obj"></param>
        private void logoutExecuteMethod(object obj)
        {
            Application.Current.Properties.Remove("name");

            App.Current.MainPage = new LoginView();
        }
    }
}
