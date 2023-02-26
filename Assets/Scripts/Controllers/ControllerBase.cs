using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerBase : MonoBehaviour
{
    [SerializeField] protected CharacterBase controlTarget;

    //controlTarget은 직렬화 가능하기 때문에, 유니티 인스펙터에서 보여요!
    //직접 설정할 수도 있죠?
    //설정을 안했다라면?
    protected virtual void Start()
    {
        if(controlTarget == null)
        {
            //컨트롤러 베이스가 들어있는 "GameObject"
            //GameObject안에 다른 "CharacterBase"가 존재한다면!
            //그럼 저거 내가 하면 안되나?
            //제 게임오브젝트 안쪽에서 다른 컴포넌트를 찾는 방법!
            controlTarget = GetComponent<CharacterBase>();
        }
    }
}
