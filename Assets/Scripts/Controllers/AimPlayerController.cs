using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimPlayerController : PlayerController
{
    CharacterBase3D character3D = null;

    //수평 민감도
    public float horizontalSensitive = 1;
    //수직 민감도
    public float verticalSensitive = 1;

    protected override void Start()
    {
        base.Start();
        //해당 캐릭터가 3D를 상속받았으면, 내용이 채워지고
        //상속받지 않았으면 Character3D가 Null로 바뀝니다!
        //3D효과를 넣으려면 null체크를 하시면 됩니다!
        if (controlTarget) character3D = controlTarget.IsSubclassOf<CharacterBase3D>();
    }

    protected override void Update()
    {
        base.Update();

        //이렇게 캐릭터가 3D요소를 가지고 있는지 확인할게요!
        //마우스가 잠겨있어야 저희가 움직일 수 있도록!
        //마우스로 움직이고 있는 저 화살표?
        if(character3D)
        {
            if (Cursor.lockState != CursorLockMode.Locked)
            {
                // 0 : 좌클릭
                // 1 : 우클릭
                // 2 : 휠클릭
                // 3 : 엄지위클릭
                // 4 : 엄지아래클릭
                if(Input.GetMouseButtonDown(0))
                {
                    Cursor.lockState = CursorLockMode.Locked;
                };
            }
            else
            {
                //마우스가 움직인 양만큼 수평 각도를 움직여주기!
                //마우스가 +로 움직이면, 각도는 +일 때 반시계방향으로 움직이기 때문에
                //마우스가 +일 때 오른쪽으로 회전하려면 각도는 -(시계방향)으로 돌려주셔야합니다!
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
            //앞, 뒤 입력은 forward로 움직임!
            //수평각도를 다시 수평방향으로 바꾼다 -> 수직각도를 무시하고 수평각도만 빼겠다!
            Vector3 forwardVector = character3D.HorizontalAngle.ToHorizontalVector();
            //오른쪽, 왼쪽 입력은 right로 움직일 거예요!
            Vector3 rightVector = (character3D.HorizontalAngle - 90).ToHorizontalVector();

            //          x축은 좌우니까!                             z축은 앞뒤니까!
            return (InputManager.movement.x * rightVector) + (InputManager.movement.z * forwardVector);
        }
        else
        {
            return base.GetPlayerMovement();
        };
    }
}
