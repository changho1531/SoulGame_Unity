using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Appearance_SideScroller : AppearanceBase
{
    public bool rightSideSprite;
    protected override void Update()
    {
        base.Update();
        // 1. Sprite Renderer      flipX ��ȯ!    ->  ���� ���� ������ ��� ���� ������� �ؿ�!
        // 2. transform.rotation.y 180�� ȸ��!    -> �ִϸ��̼ǿ��� ȸ���Ϸ��� �ϸ� ������!
        // 3. transform.scale.x    -�� ������!    -> �ִϸ��̼ǿ��� ũ��ٲٷ��� �ϸ� �����ϴ�!
        bool isRight = targetCharacter.lookForward.x > 0;
        //���Ϸ�! ���Ϸ� ����! 0 ~ 360��
        //180���� ȸ���ؾ��ϴ� ����? -> �ݴ�� ������ ���ؼ�! -> �̹����� ���� �ִ� ����� ���� �ִ� ������ �ٸ��� ����!
        //������ ���� �ְ� ���������� �� || ���� ���� �ְ� �������� ��   ->  �� ��
        //                                        ���� ? �� : ����
        transform.rotation = Quaternion.Euler(0, isRight ^ rightSideSprite? 180 : 0, 0);
        //                                      XOR�� �� ���� �ٸ� ���� True!
    }
}
