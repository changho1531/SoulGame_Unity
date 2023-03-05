using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearanceBase : MonoBehaviour
{
    protected CharacterBase targetCharacter;
    protected Animator targetAnimator;
    protected virtual void Start()
    {
        //GetComponent : �� ������Ʈ�� ������ �ִ� ���� ������Ʈ�� �ִ� ������Ʈ�� ã����!
        //GetComponentInChild<>(); : �� ������Ʈ�� ���ӿ�����Ʈ�� �ڽ��� ������Ʈ
        //�θ� ������Ʈ�� �ִ� ������Ʈ�� ������!
        targetCharacter = GetComponentInParent<CharacterBase>();
        targetAnimator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        //�ִϸ����ʹ� ������ �Ķ���Ͱ� "�̸�"���� ����Ǿ��־��!
        //Get �Ǵ� Set �Լ��� �� ���̰�
        //Int Float Bool Trigger

        //Ÿ�پִϸ�������bool���� -> "isMove"�� ĳ������ isMove��!
        targetAnimator.SetBool("isMove", targetCharacter.Moving);
        targetAnimator.SetBool("isGround", targetCharacter.IsGround());
    }
}
