using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    //Serial : ����ȭ, ����Ƽ���� �� �� �ְ� �������ֱ�
    [SerializeField] protected string   characterName;
    [SerializeField] protected FloatBar health;
    [SerializeField] protected Ally     currentAlly;  //�Ҽӵ� ��ü
    [SerializeField] protected float    moveSpeedValue; //������ �����̴� �ӵ�
    [SerializeField] protected float    moveSpeedMultiplier =1 ;  //�ӵ��� ����
    [SerializeField] protected int      Level =1 ;
    [SerializeField] protected int      exp ;

    protected virtual void OnMove()
    {

    }
    //position�� �����غ� : Vector3(0,0,0);
    //���ʹ� ���� �Ǵ� ��ġ�� �˷��ְ� 3�� ���ڰ� �� ����� ��(3����)
    //Claim : ��û�ϴ�
    public void ClaimMove(Vector3 wantPosition)
    {
        transform.position += wantPosition;
    }
}
