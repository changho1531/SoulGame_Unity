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

    float lastMoveTime;

    [SerializeField] protected int      level = 1;
    [SerializeField] protected int      exp;

    //���� �ٶ󺸰� �ִ� ����!
    public Vector3 lookForward = Vector3.right;
    //���� �����̰� �ִ��� ����!
    protected bool isMove = false;
    //                   ���������� ������ �ð����� ���̰�  �Ǵ� �ð����� ū ���! : ���� �Ǵܿ��� �� ��������?
    public bool Moving => Time.time - lastMoveTime <= Time.fixedDeltaTime;

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
    }

    protected virtual void OnMove(float passedTime)
    {
        //�� �����̶�� �� ���� ������!
        if(isMove)
        {
            // �Ÿ� = �ӵ� * �ð�
            transform.position += lookForward * moveSpeedValue * moveSpeedMultiplier * passedTime;

            //���� ������ �ð��� �������� ������ ����!
            lastMoveTime = Time.time;

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
        Vector3 direction = wantPosition - transform.position;
        //������ z���� ����(2D�� ����!)
        direction.z = 0;
        //�� ���� ũ�⸦ 1�� ����!
        direction.Normalize();

        //z�� �����ص� ������ ������ �� �־��!
        if(direction.magnitude > 0)
        {
            //�־��ִ� �ͱ��� �ϼž� �ؿ�!
            lookForward = direction;
        
            //��! ���� �����Ϸ���!
            isMove = true;
        };
    }

    //================================================
    Rigidbody2D rigid;
    public List<Collider2D> floorList = new List<Collider2D>();
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public virtual bool IsGround()
    {
        return floorList.Count > 0; //�� �� �ؿ� ���� �ִµ�?
    }

    //                             �ε��� ����� Collider2D������Ʈ
    //�ε����� ����ִ� ��� ��� +
    //�������� ����ִٰ� �� �̻� �ƴϴϱ�! -
    private void OnTriggerEnter2D(Collider2D collision)
    {
        floorList.Add(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        floorList.Remove(collision);
    }
    //================================================

    public virtual void ClaimJump()             
    {
        if(IsGround())
        {
            rigid.AddForce(Vector3.up * jumpPower);
        };
    }

    public virtual void ClaimRun(bool value)    {}
    public virtual void ClaimAttack()           {}
    public virtual void ClaimSkill()            {}
    public virtual void ClaimRoll()             {}
}
