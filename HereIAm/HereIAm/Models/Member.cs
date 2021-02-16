using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace HereIAm.Models
{
    class Member
    {
        public string name { get; set; }
        public string room { get; set; }
        public string lat { get; set; } // 유저가 있던 마지막 위도
        public string lng { get; set; } // 유저가 있던 마지막 경도
        public string place { get; set; }
        public string error { get; set; }
        public string time { get; set; } // 유저의 마지막 위치 기록 시간
        public ICommand onClick { get; set; }
        public ICommand showMovePath { get; set; }
    }
}
