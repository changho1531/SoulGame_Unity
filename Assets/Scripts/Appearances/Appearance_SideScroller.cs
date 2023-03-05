using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Appearance_SideScroller : AppearanceBase
{
    public bool rightSideSprite;
    protected override void Update()
    {
        base.Update();
        // 1. Sprite Renderer      flipX 변환!    ->  여러 개로 구성된 경우 전부 돌려줘야 해요!
        // 2. transform.rotation.y 180도 회전!    -> 애니메이션에서 회전하려고 하면 막혀요!
        // 3. transform.scale.x    -로 돌리기!    -> 애니메이션에서 크기바꾸려고 하면 막힙니다!
        bool isRight = targetCharacter.lookForward.x > 0;
        //오일러! 오일러 각도! 0 ~ 360도
        //180도로 회전해야하는 이유? -> 반대로 돌리기 위해서! -> 이미지가 보고 있는 방향과 가고 있는 방향이 다르기 때문!
        //오른쪽 보던 애가 오른쪽으로 감 || 왼쪽 보던 애가 왼쪽으로 감   ->  안 돎
        //                                        조건 ? 참 : 거짓
        transform.rotation = Quaternion.Euler(0, isRight ^ rightSideSprite? 180 : 0, 0);
        //                                      XOR는 두 개가 다를 때에 True!
    }
}
