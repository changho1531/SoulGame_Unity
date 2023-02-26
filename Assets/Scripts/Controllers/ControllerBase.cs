using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerBase : MonoBehaviour
{
    [SerializeField] protected CharacterBase controlTarget;

    //controlTarget�� ����ȭ �����ϱ� ������, ����Ƽ �ν����Ϳ��� ������!
    //���� ������ ���� ����?
    //������ ���ߴٶ��?
    protected virtual void Start()
    {
        if(controlTarget == null)
        {
            //��Ʈ�ѷ� ���̽��� ����ִ� "GameObject"
            //GameObject�ȿ� �ٸ� "CharacterBase"�� �����Ѵٸ�!
            //�׷� ���� ���� �ϸ� �ȵǳ�?
            //�� ���ӿ�����Ʈ ���ʿ��� �ٸ� ������Ʈ�� ã�� ���!
            controlTarget = GetComponent<CharacterBase>();
        }
    }
}
