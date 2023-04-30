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

        //�ѱ��� �ְ�, ĳ���͵� �ְ�!
        if(muzzleTransform && character)
        {
            //Ray ��
            //Cast ĳ����
            //���� ����� RayCastHit�� ���ƿɴϴ�!
            RaycastHit hit;

            //               �ѱ��� ��ġ���� ����         ������  -  �����
            //Debug.DrawRay(muzzleTransform.position, objectPoint - muzzleTransform.position);
        };
    }
}
