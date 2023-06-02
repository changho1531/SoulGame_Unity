using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;

public class OVRInputManager : MonoBehaviour
{
    //�޼��� ��ġ
    public static Vector3 primaryPosition;
    //�������� ��ġ
    public static Vector3 secondaryPosition;

    public static bool triggerUp;
    public static bool triggerDown;
    public static bool trigger;

    //VR ���� ��쿡�� ���𰡸� ��� �����̽� ����, ���������� �������ֽø� ���ƿ�!
    //��� ���� ��������?
    //������ ����̳�, �ڽ� �ٱ������� ������ ������ ���� ����!

    //OVRInput�� FixedUpdate�� Update���� VR�Է��� ������ �˴ϴ�!
    private void FixedUpdate()
    {
        OVRInput.FixedUpdate();
    }

    void Update()
    {
        OVRInput.Update();
        //���� �Ӹ��� �ڽ�!
        primaryPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LHand);
        secondaryPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RHand);

        float secondaryTrigger = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);
        //OVRInput.Get()
        //                   Ʈ���Ŵ� 0 ~ 1�����Դϴ�! 50%���� ���� ���ȴٰ� �ص� ���� ������?
        bool currentTrigger = secondaryTrigger > 0.5f;
        //���� �� ���ȴ�! = �Ʊ� ������ Trigger�� �ƴϾ��µ� ������ Trigger!
        triggerDown = trigger == false && currentTrigger == true;
        //���� �� �������� = �Ʊ� ������ �����־��µ�.. ���� �ƴϾ�...
        triggerUp   = trigger == true  && currentTrigger == false;
        //Ʈ���ſ� ���� Ʈ���Ÿ� ����!
        trigger = currentTrigger;
    }
}
