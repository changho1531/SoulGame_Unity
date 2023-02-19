using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ControllerBase
{
    protected override void Start()
    {
        base.Start();
        //���� �ٷ� �÷��̾�� ��� �� �˷��ش�
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
