using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookCharacterForward : MonoBehaviour
{
    //누구의 앞을 기준으로 볼 것인가?
    public CharacterBase target;

    void Update()
    {
        //대상이 있긴 해야해요!
        if(target)
        {
            //transform 위치, 회전, 크기에 대한 이야기죠!
            //forward는 현재 회전값에서 앞은 어디인가?
            //이 친구의 "앞"을 캐릭터의 "앞"으로 맞춰주기!
            transform.forward = target.lookForward;
        };
    }
}
