using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ControllerBase
{
    protected override void Start()
    {
        base.Start();
        //���� �ٷ� �÷��̾��!
        GameManager.PlayerAdd(this);
    }

    protected virtual void OnDestroy()
    {
        //���� ���� �����ǹǷ�.. �� �̻� �÷��̾ �ƴմϴ�..
        GameManager.PlayerRemove(this);
    }

    protected virtual void Update()
    {
        if (controlTarget == null)
        {
            Destroy(this); //����.. �÷��̾ ����..
            return;
        };

        //��ǲ�Ŵ����� �������� �����ϴµ�, �������� ũ�Ⱑ 0�̶��?
        //�� �����̰� ������? �����̽ø� �ȵǰ���!
        //magnitude �Ը�!
        if (InputManager.movement.magnitude > 0)
        {
            //�������޶�!               ���� ��ġ����                  +   �����̷��� ��
            controlTarget.ClaimMove(controlTarget.transform.position + GetPlayerMovement());
        };
        if (InputManager.GetKeyDown(KeyType.Jump))
        {
            controlTarget.ClaimJump();
        };
        if (InputManager.GetKeyDown(KeyType.Attack))
        {
            controlTarget.ClaimAttack();
        };
    }

    //�÷��̾�� ���� �̵��ϰ� �;��ϴ°�?
    protected virtual Vector3 GetPlayerMovement()
    {
        return InputManager.movement;
    }
}
