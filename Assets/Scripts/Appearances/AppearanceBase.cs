using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearanceBase : MonoBehaviour
{
    protected CharacterBase targetCharacter;
    protected Animator targetAnimator;
    protected virtual void Start()
    {
        //GetComponent : 이 컴포넌트를 가지고 있는 게임 오브젝트에 있는 컴포넌트를 찾아줌!
        //GetComponentInChild<>(); : 이 컴포넌트의 게임오브젝트의 자식의 컴포넌트
        //부모 오브젝트에 있는 컴포넌트를 가져옴!
        targetCharacter = GetComponentInParent<CharacterBase>();
        targetAnimator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        //애니메이터는 안쪽의 파라미터가 "이름"으로 저장되어있어요!
        //Get 또는 Set 함수를 쓸 것이고
        //Int Float Bool Trigger

        //타겟애니메이터의bool세팅 -> "isMove"를 캐릭터의 isMove로!
        targetAnimator.SetBool ("isMove", targetCharacter.Moving);
        targetAnimator.SetBool ("isGround", targetCharacter.IsGround());
        targetAnimator.SetFloat("VerticalSpeed", targetCharacter.velocity.y);
    }

    public virtual void SetAttack()
    {
        targetAnimator.SetTrigger("Attack");
    }

    public virtual void SetControllable(bool value)
    {
        targetAnimator.SetBool("Control", value);
    }

    public virtual void CharacterMovable(bool value)
    {
        targetCharacter.isMovable = value;
    }
}
