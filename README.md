# 스파르타 던전 탐험

### 필수 기능 가이드
    * 기본 이동 및 점프
        * wasd로 이동 구현
        * space로 점프
    * 체력바 UI
        * 체력바는 Popup/main에서 SceneLoad할 때 세팅
    * 동적 환경 조사
        * PlayerEquipment 스크립트 작성
    * 점프대
        * Jumppad 스크립트 작성
    * 아이템 데이터
        * ScriptableObject로 생성
    * 아이템 사용
        * 마우스 좌클릭으로 사용 가능(음식만)

### 도전 기능 가이드
    * 추가 UI
        * 스테미나 표시바 구현
    * 3인칭 시점
        * F5번로 동작
    * 움직이는 플랫폼 구현
        * FootBoard 스크립트 구현
    * 벽타기 및 매달리기
        * 미구현
    * 다양한 아이템 구현
        * 커피는 무적, 버섯은 movespeed 증가
    * 레이저 트립
        * LaserSpawner 구현
    * 상호작용 가능한 오브젝트 표시
        * 카메라에 배터리 충전하는 텍스트 표시및 동작
    * 발전된 AI
        * 미구현

### 사적 도전
    * PopupManager -> UIManager 업그레이드(아직도 부족할 수도..)
    * Interface 애용해보기
    * player를 static단에 안 올리고 작업해보기
    * addressable Asset을 활용해서 데이터 저장 및 불러오기