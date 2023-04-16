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

            //쏘려는 목표위치!                 일단 제 눈에서 시작!
            Vector3 objectPoint = character.transform.position + (Vector3.up * eyeHeight);
            //앞으로 이동! 자주 교전이 일어나는 거리만큼 떨어뜨려주시면 됩니다!
            objectPoint += character.lookForward * 50.0f;

            //               총구의 위치에서 시작         목적지  -  출발지
            Debug.DrawRay(muzzleTransform.position, objectPoint - muzzleTransform.position);
        };
    }
}
