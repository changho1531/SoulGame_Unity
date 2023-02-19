using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerBase : MonoBehaviour
{
    [SerializeField] protected CharacterBase controlTarget;
    protected virtual void Start()  //오버라이드 하기위해 virtual
    {
        
    }
}
