using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostileContoller : ControllerBase
{
    ControllerBase player;
    void Update()
    {
        //�÷��̾ �𸣰ڴ� �̤� �÷��̾ ã�ư���!
        if(player == null) player = GameManager.PlayerFind();

        //�÷��̾ ã�Ҵٸ�! ������ ����!
        if(player != null)
        {
            //������ - �����
            Vector3 toPlayer = player.transform.position - transform.position;
            controlTarget.ClaimMove(player.transform.position);

            //�÷��̾�� ���� �濡.. y���� 0.5���� Ů�ϴ�..
            if (toPlayer.y > 0.5f) controlTarget.ClaimJump();

            //Mathf.Abs();

            //����ĳ��Ʈ�� ���� �߻�!
            //���� ���� ģ���� �����ޱ�!                   ������ġ                     ����
            RaycastHit2D hit = Physics2D.Raycast(controlTarget.transform.position, toPlayer);
            //   ����.Ʈ������!
            //if(hit.transform == null)
        };
        //controlTarget.ClaimAttack();
    }
}
