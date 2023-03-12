using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    //���� � ����ΰ�..
    protected AppearanceBase appearance;

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
    //                                  ���� ����
    [SerializeField] protected int      jumpCount = 1;
    //                                  ���� ����
    protected int                       jumpLeft;

    //������Ƽ�� get�� set�� ����ؼ� ������ ����� �� �� �ֱ�� ������
    //get�� ���� ��찡 ���� ������! => ��ȯ���� ������ get�� ������ݴϴ�!
    public float jumpPower    => jumpValue * jumpMultiplier;
    public float jumpAirPower => jumpPower * 0.02f;

    float lastMoveTime = -10;

    [SerializeField] protected int      level = 1;
    [SerializeField] protected int      exp;

    //���� �ٶ󺸰� �ִ� ����!
    public Vector3 lookForward = Vector3.right;
    //velocity : �ӵ�
    public Vector3 velocity { get; protected set; }

    // �Ÿ� = �ð� * �ӵ�       �Ÿ� / �ð� = �ӵ�
    protected Vector3 prePosition;

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
        //�ӵ� = �Ÿ� / �ð�
        velocity = (transform.position - prePosition) / Time.fixedDeltaTime;

        //���� ����� ���ؼ� ���� ��ġ�� ���� ��ġ�� ��������Կ�!
        prePosition = transform.position;

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
        //���� ����� ��� �ִ� ���� �ƴ϶�, �ڽ��� ����� ������ �ֱ� ������!
        appearance = GetComponentInChildren<AppearanceBase>();
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
        if (collision.isTrigger) return;

        floorList.Add(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.isTrigger) return;

        floorList.Remove(collision);
        //�� �ؿ� �� �̻� �ƹ� �͵� ���� �ƴ��Ͽ��� ��!
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
            //���� �� �� �����ϱ� �� �� ����!
            jumpLeft--;

            //���� ������ �� AddForce�� �ָ��� ���ΰ�?
            //������ �ȵǰų� �ڱ�ĥ �� �ֱ� ������!

            //velocity�� ������Ƽ�� ������, ��ü�� �� ���� �ٲ�� ���������� ����˴ϴ�!
            //�̸� ������ �����Ҵٰ� ������ �����ؼ� �ٽ� �־���� �ؿ�!
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
