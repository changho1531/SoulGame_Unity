using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase3D : CharacterBase
{
    Rigidbody rigid;
    WeaponBase weapon;
    public List<Collider> floorList = new List<Collider>();

    //���� ����
    public float HorizontalAngle
    {
        get => lookForward.GetHorizontalAngle();
        set => lookForward = lookForward.ToAngularVector(value, VerticalAngle);
    }

    //���� ����
    public float VerticalAngle
    {
        get => lookForward.GetVerticalAngle();
        set => lookForward = lookForward.ToAngularVector(HorizontalAngle, Mathf.Clamp(value, -80, 80));
    }

    protected override void Start()
    {
        base.Start();
        rigid = GetComponent<Rigidbody>();
        weapon = GetComponentInChildren<WeaponBase>();
    }

    public override void ClaimAttack()
    {
        //����� ��ǥ��ġ!                 �ϴ� �� ������ ����!
        Vector3 objectPoint = transform.position + (Vector3.up * 1.8f);
        //������ �̵�! ���� ������ �Ͼ�� �Ÿ���ŭ ����߷��ֽø� �˴ϴ�!
        objectPoint += lookForward * 50.0f;

        //(���Ⱑ ������ �ִϸ��̼����� ������ ���!   ex: 2Dĳ����)
        //���Ⱑ ���ų� ���Ⱑ �־ �߻翡 �������� ��!
        if (!weapon || (weapon && weapon.Shot(objectPoint)))
        {
            base.ClaimAttack();
        };
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
