using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    //Serial : 직렬화, 유니티에서 볼 수 있게 조절해주기
    [SerializeField] protected string   characterName;
    [SerializeField] protected FloatBar health;
    [SerializeField] protected Ally     currentAlly;  //소속된 단체
    [SerializeField] protected float    moveSpeedValue; //실제로 움직이는 속도
    [SerializeField] protected float    moveSpeedMultiplier =1 ;  //속도의 배율
    [SerializeField] protected int      Level =1 ;
    [SerializeField] protected int      exp ;

    protected virtual void OnMove()
    {

    }
    //position을 복사해봄 : Vector3(0,0,0);
    //벡터는 방향 또는 위치를 알려주고 3는 숫자가 세 개라는 뜻(3차원)
    //Claim : 요청하다
    public void ClaimMove(Vector3 wantPosition)
    {
        transform.position += wantPosition;
    }
}
