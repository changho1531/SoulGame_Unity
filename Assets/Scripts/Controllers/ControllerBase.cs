using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerBase : MonoBehaviour
{
    [SerializeField] protected CharacterBase controlTarget;
    protected virtual void Start()  //�������̵� �ϱ����� virtual
    {
        
    }
}
