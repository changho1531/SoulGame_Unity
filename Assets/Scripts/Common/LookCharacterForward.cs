using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookCharacterForward : MonoBehaviour
{
    //������ ���� �������� �� ���ΰ�?
    public CharacterBase target;

    void Update()
    {
        //����� �ֱ� �ؾ��ؿ�!
        if(target)
        {
            //transform ��ġ, ȸ��, ũ�⿡ ���� �̾߱���!
            //forward�� ���� ȸ�������� ���� ����ΰ�?
            //�� ģ���� "��"�� ĳ������ "��"���� �����ֱ�!
            transform.forward = target.lookForward;
        };
    }
}
