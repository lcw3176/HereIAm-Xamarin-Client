using HereIAm.Models;
using HereIAm.Services;
using HereIAm.Views;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace HereIAm.ViewModels
{
    class LoginViewModel : BaseVIewModel
    {
        private string Email;
        public string email
        {
            get { return Email; }
            set
            {
                Email = value;
                OnPropertyChanged("email");
            }
        }
        public string pwPlaceholder { get; set; } = "Password";
        public bool remember { get; set; }
        public ICommand loginCommand { get; private set; }
        private AuthService authService = new AuthService();

        public LoginViewModel()
        {
            loginCommand = new Command(LoginExecuteCommand);
            init();
        }

        /// <summary>
        ///  로그인 정보 기억하기 체크한 상태면
        ///  기록한 정보 로딩하기
        /// </summary>
        private void init()
        {
            object result = authService.GetDataFromProperties("rememberInfo");

            if(result != null)
            {
                remember = bool.Parse(result.ToString());
            }

            if (remember)
            {
                object tempEmail = authService.GetDataFromProperties("email");
                
                if(tempEmail != null)
                {
                    email = tempEmail.ToString();
                }

                object tempPw = authService.GetDataFromProperties("pw");
                if(tempPw != null)
                {
                    pwPlaceholder = "";
                    for (int i = 0; i < tempPw.ToString().Length; i++)
                    {
                        pwPlaceholder += "*";
                    }
                }
            }
        }


        /// <summary>
        /// 로그인 버튼 커맨드
        /// </summary>
        /// <param name="entryPw">비밀번호 Entry 객체</param>
        private async void LoginExecuteCommand(object entryPw)
        {
            MemberService memberService = new MemberService();
            string pw = (entryPw as Entry).Text;

            if (pwPlaceholder.Contains("*") && string.IsNullOrEmpty(pw))
            {
                Application.Current.Properties.TryGetValue("pw", out object password);
                pw = password.ToString();
            }
            

            bool result = await authService.GetAuth(email, pw);

            if (result)
            {
                if (remember) 
                {
                    authService.SetDataToProperties("email", email);
                    authService.SetDataToProperties("pw", pw);
                }

                Member mem = await memberService.GetMyInfo(email);

                authService.SetDataToProperties("name", mem.name);
                authService.SetDataToProperties("room", mem.room);

                App.Current.MainPage = new HomeView();
            }

            else
            {
                DependencyService.Get<IMessage>().Alert("일치하는 정보가 없습니다");
            }
        }
    }
}
