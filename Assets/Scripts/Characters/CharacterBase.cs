using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    //나는 어떤 모습인가..
    protected AppearanceBase appearance;

    //Attribute
    //Serial : 직렬화! (안쪽의 내용을 볼 수 있게 조절해주기!)
    [SerializeField] protected string   characterName;
    [SerializeField] protected FloatBar health;
    [SerializeField] protected Ally     currentAlly;
    //                                  실제로 움직이는 속도!
    [SerializeField] protected float    moveSpeedValue;
    //                                  속도의 배율!
    [SerializeField] protected float    moveSpeedMultiplier = 1;
    //                                  점프값
    [SerializeField] protected float    jumpValue;
    //                                  점프 배율!
    [SerializeField] protected float    jumpMultiplier = 1;
    //                                  점프 개수
    [SerializeField] protected int      jumpCount = 1;
    //                                  남은 점프
    protected int                       jumpLeft;

    //프로퍼티는 get과 set을 사용해서 완전히 만들어 줄 수 있기는 하지만
    //get만 쓰는 경우가 많단 말이죠! => 반환값만 넣으면 get을 만들어줍니다!
    public float jumpPower    => jumpValue * jumpMultiplier;
    public float jumpAirPower => jumpPower * 0.02f;

    float lastMoveTime = -10;

    [SerializeField] protected int      level = 1;
    [SerializeField] protected int      exp;

    //내가 바라보고 있는 방향!
    public Vector3 lookForward = Vector3.right;
    //velocity : 속도
    public Vector3 velocity { get; protected set; }

    // 거리 = 시간 * 속도       거리 / 시간 = 속도
    protected Vector3 prePosition;

    //내가 움직이고 있는지 여부!
    protected bool isMove = false;
    //                   마지막으로 움직인 시간과의 차이가  판단 시간보다 큰 경우! : 직전 판단에선 안 움직였네?
    public bool Moving => Time.time - lastMoveTime <= Time.fixedDeltaTime;


    //Update는 종류가 3가지!
    //Update      : 게임 프레임에 맞춰 업데이트!
    //LateUpdate  : Update 끝난 직후 실행!
    //FixedUpdate : 고정된 주기로 업데이트!
    //              만약 중간에 렉이 걸렸다? 0.1초마다 한다고 하면
    //                                     1초 뒤에 컴퓨터가 다시 켜졌거든요?
    //                                     그럼 나 10번 함 ㅡㅡ
    //              물리연산은 FixedUpdate!
    protected virtual void FixedUpdate()
    {
        //속도 = 거리 / 시간
        velocity = (transform.position - prePosition) / Time.fixedDeltaTime;

        //다음 계산을 위해서 이전 위치를 현재 위치로 맞춰놓을게요!
        prePosition = transform.position;

        //Time.deltaTime
        //델타타임은 마지막 프레임과 현재 프레임의 시간 차이!
        //저번이랑 비교했을때 얼마나 차이가 나는가?
        //FixedUpdate는 저번이랑 비교한 값으로 들어가는 게 아니예요!
        //항상 정해진 시간을 꼭 준수하는 친구입니다!
        //이 친구가 약속한 시간이 중요한 거예요!
        OnMove(Time.fixedDeltaTime);
    }

    protected virtual void OnMove(float passedTime)
    {
        //난 움직이라고 할 때만 움직여!
        if(isMove)
        {
            // 거리 = 속도 * 시간
            transform.position += lookForward * moveSpeedValue * moveSpeedMultiplier * passedTime;

            //내가 움직인 시간의 스탬프를 찍어놓는 거죠!
            lastMoveTime = Time.time;

            isMove = false;
        };
    }
    //position을 복사해봄 : Vector3(0,0,0)
    //벡터는 방향 또는 위치를 알려주고! 3는 숫자가 세 개라는 뜻 (3차원)
    //Claim : 요청하다
    public virtual void ClaimMove(Vector3 wantPosition)
    {
        //내가 저기까지 가야 한다면..
        //일단 그쪽 방향을 보게 하는것이 1번!
        //2D라서 저는 왼쪽 아님 오른쪽이예요!
        //출발지에서 목적지를 향해서 가는 방법!
        //(5,3)     (8,1)   =  (3,-2)
        //(7,1)     (0,0)   =  (-7,-1)

        // 목적지 - 출발지 = 목적지를 향해서 가는 방법!
        Vector3 direction = wantPosition - transform.position;
        //방향의 z축을 제거(2D기 때문!)
        direction.z = 0;
        //그 다음 크기를 1로 변경!
        direction.Normalize();

        //z를 제거해도 여전히 움직일 수 있어요!
        if(direction.magnitude > 0)
        {
            //넣어주는 것까지 하셔야 해요!
            lookForward = direction;
        
            //네! 이제 움직일래요!
            isMove = true;
        };
    }

    //================================================
    Rigidbody2D rigid;
    public List<Collider2D> floorList = new List<Collider2D>();

    void Start()
    {
        //제가 모습을 띄고 있는 것이 아니라, 자식이 모습을 가지고 있기 때문에!
        appearance = GetComponentInChildren<AppearanceBase>();
        rigid = GetComponent<Rigidbody2D>();
    }

    public virtual bool IsGround()
    {
        return floorList.Count > 0; //내 발 밑에 뭔가 있는데?
    }

    //                             부딪힌 대상의 Collider2D컴포넌트
    //부딪히면 닿아있는 대상 목록 +
    //떨어지면 닿아있다가 더 이상 아니니까! -
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger) return;

        floorList.Add(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.isTrigger) return;

        floorList.Remove(collision);
        //발 밑에 더 이상 아무 것도 남지 아니하였을 때!
        if(floorList.Count <= 0) jumpLeft = jumpCount - 1;
    }
    //================================================

    public virtual void ClaimJump()             
    {
        if(IsGround())
        {
            rigid.AddForce(Vector3.up * jumpPower);
        }
        else if (jumpLeft > 0)
        {
            //점프 한 번 했으니까 한 번 빼기!
            jumpLeft--;

            //공중 점프는 왜 AddForce가 애매할 것인가?
            //점프가 안되거나 솟구칠 수 있기 때문에!

            //velocity는 프로퍼티기 때문에, 전체를 한 번에 바꿔야 정상적으로 실행됩니다!
            //미리 정보를 빼놓았다가 정보를 수정해서 다시 넣어줘야 해요!
            Vector3 originVelocity = rigid.velocity;
            originVelocity.y = jumpAirPower;
            rigid.velocity = originVelocity;
        };
    }

    public virtual void ClaimRun(bool value)    {}
    public virtual void ClaimAttack()           { appearance?.SetAttack(); }
    public virtual void ClaimSkill()            {}
    public virtual void ClaimRoll()             {}
}
