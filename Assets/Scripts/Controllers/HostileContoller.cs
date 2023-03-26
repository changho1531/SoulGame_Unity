using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostileContoller : ControllerBase
{
    ControllerBase player = null;

    void Update()
    {
        //�÷��̾ �𸣰ڴ� �̤� �÷��̾ ã�ư���!
        if(player == null) player = GameManager.PlayerFind();

        //�÷��̾ ã�Ҵٸ�! ������ ����!
        if(player != null)
        {
            //������ - �����
            Vector3 toPlayer = player.transform.position - transform.position;
            if (Mathf.Abs(toPlayer.x) > 10)
            {

            }
            else if(Mathf.Abs(toPlayer.x) < 1)
            {
                controlTarget.ClaimAttack();
            }
            else
            {
                controlTarget.ClaimMove(player.transform.position);

                //�÷��̾�� ���� �濡.. y���� 0.5���� Ů�ϴ�..
                if (toPlayer.y > 0.5f) controlTarget.ClaimJump();
            };
            
            //abs Absolute Number ������ �Ÿ��� �� ���� x���� �������� ��� �;���!
            // ����(-), ������(+)�� ���� �� �� �Ȱ��� �����ؾ��ؿ�!
            //Mathf.Abs(toPlayer.x);

            //����ĳ��Ʈ�� ���� �߻�!
            //���� ���� ģ���� �����ޱ�!                   ������ġ                     ����
            RaycastHit2D hit = Physics2D.Raycast(controlTarget.transform.position, toPlayer);
            //   ����.Ʈ������!
            if(hit.transform)
            {

            };
        };
    }
}
