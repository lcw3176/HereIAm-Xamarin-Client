# HereIAm-Xamarin-Client 
그룹단위 위치 추적 앱
#### !본 프로젝트는 [HereIAm-Spring-Server](https://github.com/lcw3176/HereIAm-Spring-Server)와 연동된 어플입니다.

## 기술 스택
* C#, Xamarin
* kakao map api(위,경도 -> 주소 변환)
* google map api(지도 view)
* Newtonsoft.Json(서버 json 파일 분석)

## 구현 기능
* gps를 통한 위치 전송
* foregroundService를 이용, sleep 상태에서도 위치 전송
* 로그인, 로그아웃, 방 검색, 방 나가기
* 멤버 위치 시간대별 기록 조회
* 서버와 연결되는 Service는 사용자 경험을 위해 비동기로 구성

## 작동 모습
#### user permission, 로그인
<div>
<img src="https://user-images.githubusercontent.com/59993347/108021525-12d53280-7062-11eb-80fa-d35d419fa235.jpg" width=250>

<img src="https://user-images.githubusercontent.com/59993347/108021523-123c9c00-7062-11eb-8316-0d540ab4783e.jpg" width=250>
</div>

#### 메뉴, 방 검색, 내 설정
<div>

<img src="https://user-images.githubusercontent.com/59993347/108021518-110b6f00-7062-11eb-801d-28b7e428acb9.jpg" width=250>

<img src="https://user-images.githubusercontent.com/59993347/108021516-1072d880-7062-11eb-81f9-de7779addd9b.jpg" width=250>

<img src="https://user-images.githubusercontent.com/59993347/108021512-0fda4200-7062-11eb-9df1-c917401c786c.jpg" width=250>
</div>


#### 기록 조회, 유저 위치 검색
<div>
<img src="https://user-images.githubusercontent.com/59993347/108021519-11a40580-7062-11eb-979d-efd184fed936.jpg" width=250>

<img src="https://user-images.githubusercontent.com/59993347/108021522-11a40580-7062-11eb-9e21-b0095d8c8d9c.jpg" width=250>
</div>