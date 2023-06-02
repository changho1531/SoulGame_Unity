using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;

public class OVRInputManager : MonoBehaviour
{
    //왼손의 위치
    public static Vector3 primaryPosition;
    //오른손의 위치
    public static Vector3 secondaryPosition;

    public static bool triggerUp;
    public static bool triggerDown;
    public static bool trigger;

    //VR 같은 경우에는 무언가를 잡고 움직이실 때에, 물리적으로 움직여주시면 좋아요!
    //들고 손을 움직이죠?
    //던지는 기능이나, 박스 바깥쪽으로 물건을 가지고 가는 행위!

    //OVRInput의 FixedUpdate와 Update에서 VR입력이 들어오게 됩니다!
    private void FixedUpdate()
    {
        OVRInput.FixedUpdate();
    }

    void Update()
    {
        OVRInput.Update();
        //손은 머리의 자식!
        primaryPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LHand);
        secondaryPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RHand);

        float secondaryTrigger = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);
        //OVRInput.Get()
        //                   트리거는 0 ~ 1까지입니다! 50%정도 당기면 눌렸다고 해도 되지 않을까?
        bool currentTrigger = secondaryTrigger > 0.5f;
        //이제 막 눌렸다! = 아까 전에는 Trigger가 아니었는데 이제는 Trigger!
        triggerDown = trigger == false && currentTrigger == true;
        //이제 막 떼어졌다 = 아까 전에는 눌려있었는데.. 이젠 아니야...
        triggerUp   = trigger == true  && currentTrigger == false;
        //트리거에 현재 트리거를 저장!
        trigger = currentTrigger;
    }
}
