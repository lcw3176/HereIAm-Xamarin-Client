using System;
using System.Collections.Generic;
using System.Text;

namespace HereIAm.Services
{
    public interface IMessage
    {
        /// <summary>
        /// 토스트 알림 메세지
        /// </summary>
        /// <param name="message"></param>
        void Alert(string message);
    }
}
