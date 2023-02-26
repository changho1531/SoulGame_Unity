using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
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

    //프로퍼티는 get과 set을 사용해서 완전히 만들어 줄 수 있기는 하지만
    //get만 쓰는 경우가 많단 말이죠! => 반환값만 넣으면 get을 만들어줍니다!
    public float jumpPower => jumpValue * jumpMultiplier;

    [SerializeField] protected int      level = 1;
    [SerializeField] protected int      exp;

    //내가 바라보고 있는 방향!
    public Vector3 lookForward = Vector3.right;
    //내가 움직이고 있는지 여부!
    public bool isMove = false;

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
        //Time.deltaTime
        //델타타임은 마지막 프레임과 현재 프레임의 시간 차이!
        //저번이랑 비교했을때 얼마나 차이가 나는가?
        //FixedUpdate는 저번이랑 비교한 값으로 들어가는 게 아니예요!
        //항상 정해진 시간을 꼭 준수하는 친구입니다!
        //이 친구가 약속한 시간이 중요한 거예요!
        OnMove(Time.fixedDeltaTime);
        isMove = false;
    }

    protected virtual void OnMove(float passedTime)
    {
        //난 움직이라고 할 때만 움직여!
        if(isMove)
        {
            // 거리 = 속도 * 시간
            transform.position += lookForward * moveSpeedValue * moveSpeedMultiplier * passedTime;

            //나 움직였으니까 이제 안 움직일래!
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
        lookForward = wantPosition - transform.position;
        //방향의 z축을 제거(2D기 때문!)
        lookForward.z = 0;
        //그 다음 크기를 1로 변경!
        lookForward.Normalize();

        //네! 이제 움직일래요!
        isMove = true;
    }

    Rigidbody2D rigid;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    public virtual void ClaimJump()             
    {
        rigid.AddForce(Vector2.up * jumpPower);
    }

    public virtual void ClaimRun(bool value)    {}
    public virtual void ClaimAttack()           {}
    public virtual void ClaimSkill()            {}
    public virtual void ClaimRoll()             {}
}
