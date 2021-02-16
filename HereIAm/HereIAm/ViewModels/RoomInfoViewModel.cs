using HereIAm.Models;
using HereIAm.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace HereIAm.ViewModels
{
    class RoomInfoViewModel : BaseVIewModel
    {
        private ObservableCollection<Member> MemberCollection = new ObservableCollection<Member>();
        public ObservableCollection<Member> memberCollection
        {
            get { return MemberCollection; }
            set
            {
                MemberCollection = value;
                OnPropertyChanged("memberCollection");
            }
        }
        private bool isrealtime = true;
        public bool isRealtime
        {
            get { return isrealtime; }
            set
            {
                isrealtime = value;
                OnPropertyChanged("isRealtime");
            }
        }

        public Map map { get; private set; }
        public ICommand memberOnClick { get; set; }
        public ICommand showMovePath { get; set; }
        public ICommand clearDisplay { get; set; }
        public ICommand changeMode { get; set; }

        public DateTime date { get; set; }
        public TimeSpan fromtime { get; set; }
        public TimeSpan totime { get; set; }

        private MemberService memberService = new MemberService();
        private LocationSerivce locationService = new LocationSerivce();
        private RoomService roomService = new RoomService();
        private AuthService authService = new AuthService();

        private List<Pin> pinList = new List<Pin>();

        public RoomInfoViewModel()
        {
            memberOnClick = new Command(MemberOnClickExecute);
            showMovePath = new Command(ShowMovePathExecute);
            clearDisplay = new Command(ClearDisplayExecute);
            changeMode = new Command(ChangeModeExecute);
            date = DateTime.Now;
            fromtime = TimeSpan.FromHours(1);
            totime = TimeSpan.FromHours(23);

            Task.Run(() => ExecuteMessage());
            
            InitMember();
            InitMap();
            CallGpsService();
        }

       
        /// <summary>
        /// 위치 정보 수집 시작
        /// </summary>
        private void CallGpsService()
        {
            if (GpsService.IsAgree() && !GpsService.run)
            {
                GpsService.Init();
                Device.StartTimer(TimeSpan.FromSeconds(10), () =>
                {
                    GpsService.SendCurrentLocation();
                    return GpsService.run;
                });
                
                
            }
        }

        /// <summary>
        /// 모드 변경(실시간, 시간별 보기)
        /// </summary>
        /// <param name="obj"></param>
        private void ChangeModeExecute(object obj)
        {
            isRealtime = bool.Parse(obj.ToString());
        }

        /// <summary>
        /// 화면 클리어
        /// </summary>
        /// <param name="obj"></param>
        private void ClearDisplayExecute(object obj)
        {
            map.Pins.Clear();
            map.MapElements.Clear();
        }


        /// <summary>
        /// 메세지 큐, 새로운 방 입장시 실행
        /// </summary>
        private void ExecuteMessage()
        {
            try
            {
                while (true)
                {
                    while (roomEventQueue.Count > 0)
                    {
                        roomEventQueue.Dequeue();
                        InitMember();
                        CallGpsService();
                    }

                    Controller.Reset();
                    Controller.WaitOne(Timeout.Infinite);
                }
            }

            catch { }
        }


        /// <summary>
        /// 멤버 이동 경로 보여주기
        /// </summary>
        /// <param name="memName">멤버 이름</param>
        private async void ShowMovePathExecute(object memName)
        {
            if(isRealtime)
            {
                isRealtime = false;
                return;
            }

            List<Location> locations = await locationService.GetLocationByName(memName.ToString(), date, fromtime, totime);

            if(locations == null)
            {
                DependencyService.Get<IMessage>().Alert("기록이 존재하지 않습니다");
                return;
            }


            Polyline polyline = new Polyline();
            CultureInfo provider = CultureInfo.InvariantCulture;

            foreach (var item in locations)
            {
                Position pos = new Position(item.lat, item.lng);
                DateTime.ParseExact(item.time, "yyyy-MM-dd HH:mm:ss.f", provider);
                polyline.Geopath.Add(pos);
            }

            map.MapElements.Clear();
            map.MapElements.Add(polyline);            
            MapSpan mapSpan = new MapSpan(polyline.Geopath[polyline.Geopath.Count - 1], 0.01, 0.01);
            map.MoveToRegion(mapSpan);
        }

        /// <summary>
        /// 멤버 클릭 시 지도에 마커 표시
        /// </summary>
        /// <param name="memName">멤버 이름</param>
        private async void MemberOnClickExecute(object memName)
        {
            string memberName = memName.ToString();
            foreach (var i in pinList)
            {
                if (i.Label == memberName)
                {
                    MapSpan span = new MapSpan(i.Position, 0.01, 0.01);
                    map.MoveToRegion(span);
                    return;
                }
            }

            Member member = await memberService.GetMemberByName(memberName);
            Position position = new Position(Double.Parse(member.lat), Double.Parse(member.lng));

            Pin pin = new Pin
            {
                Label = member.name,
                Address = member.place,
                Type = PinType.Place,
                Position = position,
            };

            map.Pins.Add(pin);

            MapSpan mapSpan = new MapSpan(position, 0.01, 0.01);
            map.MoveToRegion(mapSpan);
        }


        /// <summary>
        /// 소속된 방 멤버들 초기화
        /// </summary>
        private async void InitMember()
        {
            object roomName = authService.GetDataFromProperties("room");
            string userName = authService.GetDataFromProperties("name").ToString();
            
            if(roomName == null)
            {
                MemberCollection.Clear();
                return;
            }
            
            List<Member> data = await roomService.GetMembers(roomName.ToString(), userName.ToString());
            
            if(data == null)
            {
                MemberCollection.Clear();
                return;
            }

            foreach (Member i in data)
            {
                i.onClick = memberOnClick;
                i.showMovePath = showMovePath;
                memberCollection.Add(i);
            }

            return;

        }

        /// < summary >
        /// 멤버 위치 표시될 지도 초기화
        /// </ summary >
        private void InitMap()
        {
            Position position = new Position(37.5665, 126.9780);
            MapSpan mapSpan = new MapSpan(position, 0.01, 0.01);
            map = new Map(mapSpan);
        }
        
    
    }
}
