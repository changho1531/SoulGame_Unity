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
        //2D��� ģ���� ������, y���� ���ֹ����� �;��!
        //�� y���̶� �Ȱ��� ����� �ؿ�!
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
            Vector3 result = rigid.velocity;//������ �����ͼ�
            result.y = force;               //y�� �ٲ� �ű�
            rigid.velocity = result;        //�ٽ� �־��ֱ�
        };
    }

    public override bool IsGround()
    {
        return floorList.Count > 0; //�� �� �ؿ� ���� �ִµ�?
    }

    //                             �ε��� ����� Collider2D������Ʈ
    //�ε����� ����ִ� ��� ��� +
    //�������� ����ִٰ� �� �̻� �ƴϴϱ�! -
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.isTrigger) return;

        floorList.Add(collision);
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.isTrigger) return;

        floorList.Remove(collision);
        //�� �ؿ� �� �̻� �ƹ� �͵� ���� �ƴ��Ͽ��� ��!
        if (floorList.Count <= 0) jumpLeft = jumpCount - 1;
    }
}
