using HereIAm.Services;
using HereIAm.Views;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace HereIAm.ViewModels
{
    class SearchRoomViewModel : BaseVIewModel
    {
        public ICommand roomSearch { get; set; }
        public string name { get; set; }
        private RoomService roomService = new RoomService();


        public SearchRoomViewModel()
        {
            roomSearch = new Command(roomSearchExecute);
        }

        /// <summary>
        /// 방 검색 커맨드
        /// </summary>
        /// <param name="pw">패스워드 Entry 객체</param>
        private async void roomSearchExecute(object pw)
        {
            Application.Current.Properties.TryGetValue("room", out object checkRoom);

            if (checkRoom != null)
            {
                DependencyService.Get<IMessage>().Alert("소속된 방을 나간후 진행해 주세요");
                return;
            }

            string roomPw = (pw as Entry).Text;
            bool result = await roomService.CheckinRoom(name, roomPw);

            if(!result)
            {
                DependencyService.Get<IMessage>().Alert("다시 확인해 주세요");
            }

            else
            {
                AuthService authService = new AuthService();
                authService.SetDataToProperties("room", name);
                EnqueueRoomEventQueue();
                DependencyService.Get<IMessage>().Alert("입장하였습니다. 내 방 정보를 확인하세요");
            }

        }
    }
}
