using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimPlayerController : PlayerController
{
    CharacterBase3D character3D = null;

    //���� �ΰ���
    public float horizontalSensitive = 1;
    //���� �ΰ���
    public float verticalSensitive = 1;

    protected override void Start()
    {
        base.Start();
        //�ش� ĳ���Ͱ� 3D�� ��ӹ޾�����, ������ ä������
        //��ӹ��� �ʾ����� Character3D�� Null�� �ٲ�ϴ�!
        //3Dȿ���� �������� nullüũ�� �Ͻø� �˴ϴ�!
        if (controlTarget) character3D = controlTarget.IsSubclassOf<CharacterBase3D>();
    }

    protected override void Update()
    {
        base.Update();

        //�̷��� ĳ���Ͱ� 3D��Ҹ� ������ �ִ��� Ȯ���ҰԿ�!
        //���콺�� ����־�� ���� ������ �� �ֵ���!
        //���콺�� �����̰� �ִ� �� ȭ��ǥ?
        if(character3D)
        {
            if (Cursor.lockState != CursorLockMode.Locked)
            {
                // 0 : ��Ŭ��
                // 1 : ��Ŭ��
                // 2 : ��Ŭ��
                // 3 : ������Ŭ��
                // 4 : �����Ʒ�Ŭ��
                if(Input.GetMouseButtonDown(0))
                {
                    Cursor.lockState = CursorLockMode.Locked;
                };
            }
            else
            {
                //���콺�� ������ �縸ŭ ���� ������ �������ֱ�!
                //���콺�� +�� �����̸�, ������ +�� �� �ݽð�������� �����̱� ������
                //���콺�� +�� �� ���������� ȸ���Ϸ��� ������ -(�ð����)���� �����ּž��մϴ�!
                character3D.HorizontalAngle -= Input.GetAxis("Mouse X") * horizontalSensitive;
                character3D.VerticalAngle += Input.GetAxis("Mouse Y") * verticalSensitive;
                if(InputManager.menuDown)
                {
                    Cursor.lockState = CursorLockMode.None;
                }
                /*
                else if (Input.GetMouseButtonDown(0))
                {
                    character3D.ClaimAttack();
                };
                */
            };
        };
    }

    protected override Vector3 GetPlayerMovement()
    {
        if(character3D)
        {
            //��, �� �Է��� forward�� ������!
            //���򰢵��� �ٽ� ����������� �ٲ۴� -> ���������� �����ϰ� ���򰢵��� ���ڴ�!
            Vector3 forwardVector = character3D.HorizontalAngle.ToHorizontalVector();
            //������, ���� �Է��� right�� ������ �ſ���!
            Vector3 rightVector = (character3D.HorizontalAngle - 90).ToHorizontalVector();

            //          x���� �¿�ϱ�!                             z���� �յڴϱ�!
            return (InputManager.movement.x * rightVector) + (InputManager.movement.z * forwardVector);
        }
        else
        {
            return base.GetPlayerMovement();
        };
    }
}
