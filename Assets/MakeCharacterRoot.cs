using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeCharacterRoot : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<AppearanceBase>()?.CharacterMovable(false);
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<AppearanceBase>()?.CharacterMovable(true);
    }
}
