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
    [SerializeField] public float       moveSpeedMultiplier = 1;
    //                                  ������
    [SerializeField] protected float    jumpValue;
    //                                  ���� ����!
    [SerializeField] public float       jumpMultiplier = 1;
    //                                  ���� ����
    [SerializeField] public int         jumpCount = 1;
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

    //���� �����̰� �ִ°�?
    public Vector3 moveVector;

    //velocity : �ӵ�
    public Vector3 velocity { get; protected set; }

    // �Ÿ� = �ð� * �ӵ�       �Ÿ� / �ð� = �ӵ�
    protected Vector3 prePosition;

    //���� �����̰� �ִ��� ����!
    protected bool isMove = false;

    //�����ӿ� ���� ȸ���� �� ���ΰ�?
    public bool isRotateByMove = true;

    //��Ʈ���� �ϴµ���, ������ �׿������� ��Ʈ���� �Ұ����� �����Դϴ�!
    //                     bool            byte
    // 0.5�� ¥�� ����       T               1
    // 3�� ¥�� ����         T               2
    // 0.5�� ��             F               1
    // 3�� ��               F               0
    byte controlStack = 0;
    //���� �ൿ�� ������ �� �ִ°�..?
    public bool isControllable
    {
        //get { return controlStack <= 0; }
        get => controlStack <= 0;
        set
        {
            controlStack += (byte)(value ? -1 : 1);
            //                        true : ������ �� ����! -1
            appearance?.SetControllable(isControllable);
        }
    }

    byte moveStack = 0;
    public bool isMovable
    {
        get => moveStack <= 0;
        set => moveStack += (byte)(value ? -1 : 1);
    }

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
        if(isMove && isControllable && isMovable)
        {
            // �Ÿ� = �ӵ� * �ð�
            transform.position += moveVector * moveSpeedValue * Mathf.Max(moveSpeedMultiplier, 0.1f) * passedTime;

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
        if (isMovable == false || isControllable == false) return;

        //���� ������� ���� �Ѵٸ�..
        //�ϴ� ���� ������ ���� �ϴ°��� 1��!
        //2D�� ���� ���� �ƴ� �������̿���!
        //��������� �������� ���ؼ� ���� ���!
        //(5,3)     (8,1)   =  (3,-2)
        //(7,1)     (0,0)   =  (-7,-1)

        // ������ - ����� = �������� ���ؼ� ���� ���!
        Vector3 direction = wantPosition - transform.position;

        //�� ���� ũ�⸦ 1�� ����!
        direction.Normalize();

        //z�� �����ص� ������ ������ �� �־��!
        if(direction.magnitude > 0)
        {
            //�־��ִ� �ͱ��� �ϼž� �ؿ�!
            moveVector = direction;

            //�����ӿ� ���� ȸ���ϴ� ģ����� �������� ȸ���� �� �� �ְԲ�!
            if (isRotateByMove) lookForward = moveVector;

            //��! ���� �����Ϸ���!
            isMove = true;
        };
    }

    protected virtual void Start()
    {
        //���� ����� ��� �ִ� ���� �ƴ϶�, �ڽ��� ����� ������ �ֱ� ������!
        appearance = GetComponentInChildren<AppearanceBase>();
    }
    public virtual void ResetFloor() { }
    public virtual void AddVelocity(Vector3 force) { }
    public virtual void SetVelocity(Vector3 force) { }
    public virtual void SetVerticalVelocity(float force) { }
    public virtual bool IsGround() { return true; }

    public virtual void ClaimJump()             
    {
        //��.. �� ��������..
        if (isControllable == false || isMovable == false) return;

        if(IsGround())
        {
            AddVelocity(Vector3.up * jumpPower);
            ResetFloor();
        }
        else if (jumpLeft > 0)
        {
            //���� �� �� �����ϱ� �� �� ����!
            jumpLeft--;

            //���� ������ �� AddForce�� �ָ��� ���ΰ�?
            //������ �ȵǰų� �ڱ�ĥ �� �ֱ� ������!

            //velocity�� ������Ƽ�� ������, ��ü�� �� ���� �ٲ�� ���������� ����˴ϴ�!
            //�̸� ������ �����Ҵٰ� ������ �����ؼ� �ٽ� �־���� �ؿ�!
            SetVerticalVelocity(jumpAirPower);
        };
    }

    public virtual void ClaimRun(bool value)    {}
    public virtual void ClaimAttack()           { if(isControllable) appearance?.SetAttack(); }
    public virtual void ClaimSkill()            {}
    public virtual void ClaimRoll()             {}

    //�������� �ޱ�!
    public virtual void TakeDamage(CharacterBase from, float damage)
    {
        //����� �ֱ� �ִµ�, �� ���� �����ε�?
        if (from && from.currentAlly == this.currentAlly) return;

        //������� ���Խô�!
        health.Current -= damage;
    }
    public virtual void TakeHeal(CharacterBase from, float heal)
    {
        //����� �ֱ� �ִµ�, �� ���� ���;��ε�?
        if (from && from.currentAlly != this.currentAlly) return;

        //������� ���Խô�!
        health.Current += heal;
    }
}
