using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGenerator : MonoBehaviour
{
    public CharacterBase character = null;
    public Transform muzzleTransform = null;
    public float eyeHeight;

    void Start()
    {
        if(character == null) character = GetComponentInParent<CharacterBase>();
    }

    void Update()
    {
        if (muzzleTransform == null) muzzleTransform = transform.FindChildByName("Muzzle");

        //총구도 있고, 캐릭터도 있고!
        if(muzzleTransform && character)
        {
            //Ray 선
            //Cast 캐스팅
            //맞은 결과가 RayCastHit에 돌아옵니다!
            RaycastHit hit;

            //               총구의 위치에서 시작         목적지  -  출발지
            //Debug.DrawRay(muzzleTransform.position, objectPoint - muzzleTransform.position);
        };
    }
}
