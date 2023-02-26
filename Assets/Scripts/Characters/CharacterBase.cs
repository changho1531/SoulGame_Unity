using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    //Attribute
    //Serial : ����ȭ! (������ ������ �� �� �ְ� �������ֱ�!)
    [SerializeField] protected string   characterName;
    [SerializeField] protected FloatBar health;
    [SerializeField] protected Ally     currentAlly;
    //                                  ������ �����̴� �ӵ�!
    [SerializeField] protected float    moveSpeedValue;
    //                                  �ӵ��� ����!
    [SerializeField] protected float    moveSpeedMultiplier = 1;
    //                                  ������
    [SerializeField] protected float    jumpValue;
    //                                  ���� ����!
    [SerializeField] protected float    jumpMultiplier = 1;

    //������Ƽ�� get�� set�� ����ؼ� ������ ����� �� �� �ֱ�� ������
    //get�� ���� ��찡 ���� ������! => ��ȯ���� ������ get�� ������ݴϴ�!
    public float jumpPower => jumpValue * jumpMultiplier;

    [SerializeField] protected int      level = 1;
    [SerializeField] protected int      exp;

    //���� �ٶ󺸰� �ִ� ����!
    public Vector3 lookForward = Vector3.right;
    //���� �����̰� �ִ��� ����!
    public bool isMove = false;

    //Update�� ������ 3����!
    //Update      : ���� �����ӿ� ���� ������Ʈ!
    //LateUpdate  : Update ���� ���� ����!
    //FixedUpdate : ������ �ֱ�� ������Ʈ!
    //              ���� �߰��� ���� �ɷȴ�? 0.1�ʸ��� �Ѵٰ� �ϸ�
    //                                     1�� �ڿ� ��ǻ�Ͱ� �ٽ� �����ŵ��?
    //                                     �׷� �� 10�� �� �Ѥ�
    //              ���������� FixedUpdate!
    protected virtual void FixedUpdate()
    {
        //Time.deltaTime
        //��ŸŸ���� ������ �����Ӱ� ���� �������� �ð� ����!
        //�����̶� �������� �󸶳� ���̰� ���°�?
        //FixedUpdate�� �����̶� ���� ������ ���� �� �ƴϿ���!
        //�׻� ������ �ð��� �� �ؼ��ϴ� ģ���Դϴ�!
        //�� ģ���� ����� �ð��� �߿��� �ſ���!
        OnMove(Time.fixedDeltaTime);
        isMove = false;
    }

    protected virtual void OnMove(float passedTime)
    {
        //�� �����̶�� �� ���� ������!
        if(isMove)
        {
            // �Ÿ� = �ӵ� * �ð�
            transform.position += lookForward * moveSpeedValue * moveSpeedMultiplier * passedTime;

            //�� ���������ϱ� ���� �� �����Ϸ�!
            isMove = false;
        };
    }
    //position�� �����غ� : Vector3(0,0,0)
    //���ʹ� ���� �Ǵ� ��ġ�� �˷��ְ�! 3�� ���ڰ� �� ����� �� (3����)
    //Claim : ��û�ϴ�
    public virtual void ClaimMove(Vector3 wantPosition)
    {
        //���� ������� ���� �Ѵٸ�..
        //�ϴ� ���� ������ ���� �ϴ°��� 1��!
        //2D�� ���� ���� �ƴ� �������̿���!
        //��������� �������� ���ؼ� ���� ���!
        //(5,3)     (8,1)   =  (3,-2)
        //(7,1)     (0,0)   =  (-7,-1)

        // ������ - ����� = �������� ���ؼ� ���� ���!
        lookForward = wantPosition - transform.position;
        //������ z���� ����(2D�� ����!)
        lookForward.z = 0;
        //�� ���� ũ�⸦ 1�� ����!
        lookForward.Normalize();

        //��! ���� �����Ϸ���!
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
