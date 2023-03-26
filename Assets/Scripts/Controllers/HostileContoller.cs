using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostileContoller : ControllerBase
{
    ControllerBase player = null;

    void Update()
    {
        //플레이어를 모르겠다 ㅜㅜ 플레이어를 찾아가고!
        if(player == null) player = GameManager.PlayerFind();

        //플레이어를 찾았다면! 때리러 가기!
        if(player != null)
        {
            //목적지 - 출발지
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

                //플레이어로 가는 길에.. y축이 0.5보다 큽니다..
                if (toPlayer.y > 0.5f) controlTarget.ClaimJump();
            };
            
            //abs Absolute Number 대상과의 거리를 잴 때에 x축을 기준으로 재고 싶었다!
            // 왼쪽(-), 오른쪽(+)일 때에 둘 다 똑같이 적용해야해요!
            //Mathf.Abs(toPlayer.x);

            //레이캐스트는 선을 발사!
            //선에 맞은 친구를 돌려받기!                   시작위치                     방향
            RaycastHit2D hit = Physics2D.Raycast(controlTarget.transform.position, toPlayer);
            //   맞은.트랜스폼!
            if(hit.transform)
            {

            };
        };
    }
}
