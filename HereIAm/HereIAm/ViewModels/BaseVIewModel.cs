using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;

namespace HereIAm.ViewModels
{
    class BaseVIewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected static Queue<char> roomEventQueue = new Queue<char>();
        protected static ManualResetEvent Controller = new ManualResetEvent(false);

        /// <summary>
        /// 새로운 방 입장, 기존방 퇴장 시 새로고침 이벤트 발생
        /// </summary>
        protected void EnqueueRoomEventQueue()
        {
            roomEventQueue.Enqueue('1');
            Controller.Set();
        }


        protected void OnPropertyChanged(string param)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(param));
            }
        }
    }
}
