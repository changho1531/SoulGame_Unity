using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ControllerBase
{
    protected override void Start()
    {
        base.Start();
        //내가 바로 플레이어다 라는 걸 알려준다
        GameManager.PlayerAdd(this);
    }
    private void Update()
    {
     if(InputManager.GetKey(KeyType.Jump))
        {
            controlTarget.ClaimMove(Vector3.up);
        }
    }
}
