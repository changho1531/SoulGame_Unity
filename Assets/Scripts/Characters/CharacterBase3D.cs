using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase3D : CharacterBase
{
    Rigidbody rigid;
    public List<Collider> floorList = new List<Collider>();

    protected override void Start()
    {
        base.Start();
        rigid = GetComponent<Rigidbody>();
    }

    public override void ClaimMove(Vector3 wantPosition)
    {
        //2D라는 친구기 때문에, y축을 없애버리고 싶어요!
        //제 y축이랑 똑같이 맞춰야 해요!
        wantPosition.y = transform.position.y;
        //wantPosition.z = transform.position.z;
        base.ClaimMove(wantPosition);
    }

    public override void ResetFloor() { floorList.Clear(); }
    public override void AddVelocity(Vector3 force)
    {
        if (rigid) rigid.AddForce(force);
    }
    public override void SetVelocity(Vector3 force)
    {
        if (rigid) rigid.velocity = force;
    }
    public override void SetVerticalVelocity(float force)
    {
        if (rigid)
        {
            Vector3 result = rigid.velocity;//원본을 가져와서
            result.y = force;               //y만 바꿀 거구
            rigid.velocity = result;        //다시 넣어주기
        };
    }

    public override bool IsGround()
    {
        return floorList.Count > 0; //내 발 밑에 뭔가 있는데?
    }

    //                             부딪힌 대상의 Collider2D컴포넌트
    //부딪히면 닿아있는 대상 목록 +
    //떨어지면 닿아있다가 더 이상 아니니까! -
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.isTrigger) return;

        floorList.Add(collision);
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.isTrigger) return;

        floorList.Remove(collision);
        //발 밑에 더 이상 아무 것도 남지 아니하였을 때!
        if (floorList.Count <= 0) jumpLeft = jumpCount - 1;
    }
}
