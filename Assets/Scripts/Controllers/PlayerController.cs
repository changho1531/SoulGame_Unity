using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ControllerBase
{
    protected override void Start()
    {
        base.Start();
        //내가 바로 플레이어다!
        GameManager.PlayerAdd(this);
    }

    protected virtual void OnDestroy()
    {
        //나는 이제 삭제되므로.. 더 이상 플레이어가 아닙니다..
        GameManager.PlayerRemove(this);
    }

    protected virtual void Update()
    {
        if (controlTarget == null)
        {
            Destroy(this); //나는.. 플레이어가 없어..
            return;
        };

        //인풋매니저의 움직임을 감지하는데, 움직임의 크기가 0이라면?
        //안 움직이고 싶으면? 움직이시면 안되겠죠!
        //magnitude 규모!
        if (InputManager.movement.magnitude > 0)
        {
            //움직여달라!               너의 위치에서                  +   움직이려는 양
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

    //플레이어는 어디로 이동하고 싶어하는가?
    protected virtual Vector3 GetPlayerMovement()
    {
        return InputManager.movement;
    }
}
