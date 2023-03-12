using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//                         몬가 상속받는 중!
//                         이걸 상속받으면 컴포넌트로 추가하실 수 있습니다!
public class GameManager : MonoBehaviour
{
    // 게임매니저는.. 게임 전체를 관리하는 친구..
    // 현재 캐릭터의 개수를 셀 때에!
    // 인스턴스한테 주는 것이 아니라, 클래스에 딱 붙여놓고 모두의 것으로 사용했던거!
    // Static이라고 하는 것이 있었습니다!
    // 게임 전체를 관리하는 친구니까 딱 하나의 인스턴스만 있어야 합니다.
    // 싱글톤이라고 불러요!
    // 현재 만들어져있는 친구를 확인해보는 거죠!
    // 누군가 왕좌를 위협한다면 죽여버릴 거예요!
    // 클래스에는 변수 메소드 프로퍼티(두 개 다!)
    // 변수가 할 수 있는 일
    // Debug.Log(a); 값을 전달!  get
    // a = 3;        값을 저장!  set
    // get과 set을 제 마음대로 바꾸기!
    // 변수를 보거나 바꿀 때에 추가적인 기능 추가!
    //                           Instance의 저장용 instance는
    //                           자동완성으로 나오지 않게 _를 넣어줍시다!
    protected static GameManager _instance;
    public static GameManager Instance
    {
        //get은 값을 보고 싶어서 온 친구예요!
        //Debug.Log(Instance)처럼!
        //값을 전달해줘야 하겠죠! 메소드에서 값을 전달하는 방법은 return!
        get
        {
            //게임매니저를 찾는 자가 있었다면.. 절대로 "없다"고 알려주면 안됩니다!
            if(_instance == null)
            {
                //현재 이미 올려놓은 게임매니저가 있을 수 있지 않을까요?
                //유니티에서 지금 가지고 있는 인스턴스를 찾을 수 있는 방법!
                //Start가 실행되기 전에 바꾸려고 한다면!
                //그럼 일단 찾아봐!
                _instance = FindObjectOfType<GameManager>();

                //찾았는데도 없어? 그럼.. 사와야지..
                if (_instance == null)
                {
                    //새로운 게임매니저를 올리려면 게임매니저는 "컴포넌트"이니까
                    //컴포넌트는 "게임오브젝트" 위에 올라가야 해요!
                    //                                 이름 정도는 가능!
                    GameObject manager = new GameObject("GameManager");
                    //게임 오브젝트 안에 새로운 컴포넌트를 "코드"를 통해서 넣는 방법!
                    _instance = manager.AddComponent<GameManager>();
                };
            };
            return _instance;
        }
        //protected : 이 친구와, 이 친구를 상속받는 자식만 볼 수 있음!
        protected set  //Instance = null
        {
            _instance = value;
        }
    }

    //플레이어는.. 하나가 아닐 수도 있지 않을까?
    protected static List<ControllerBase> players = new List<ControllerBase>();

    public static void PlayerAdd(ControllerBase newPlayer) { players.Add(newPlayer); }
    public static void PlayerRemove(ControllerBase target) { players.Remove(target); }
    //리스트는 "목록"이라고 했어요! 배열이랑 똑같아요!
    public static ControllerBase PlayerFind(int index = 0)
    {
        if (index < 0 || index >= players.Count) return null;

        return players[index];
    }
    
    void Start()
    {
        //왕좌에 오르려고 시도하는 거예요!
        //전에 만들었던 인스턴스가 없다면?
        //그럼 내가 왕이 될 관상인가?

        //너네가 찾던 왕이 누구냐? 바로 나다!
        //Instance = value; 이건 Instance의 set을 부르게 되니까!
        //                  그렇게 되면 무한 반복하게 됩니다!
        //                  그래서 Instance의 set안쪽에서는
        //                  Instance에 대입하는 것을 넣을 수 없고
        //                  그러면 자연스럽게 "저장"기능이 불가능해지는 겁니다!
        //get이나 set에 기능을 넣으면 저장기능을 상실!
        //Instance를 바꾼다는 건 _instance를 바꾼다는 거죠!
        //set은 "대입"할 때에 나오는 친구니까! 대입하려는 대상을 value에 넣어줘요!
        if (Instance != this)
        {
            //엇.. 누가 있다고..?
            //도망가자! 잘못 왔나부다!
            //이 인스턴스는 파괴되어야 해요!
            Destroy(this);
        };
    }

    // 게임을 할 때에 프레임을 굉장히 신경쓰죠?
    // 30Fps이라고 하면 최적화 실환가..
    // 1초에 몇 번 계산하는가? 프레임!
    // 업데이트라고 하는 친구는 프레임마다 할 일을 이야기합니다!
    void Update()
    {
    }
}
