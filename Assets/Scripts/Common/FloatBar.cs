using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[SerializeField]라고 하는 걸 통해서 직렬화를 하려면
//직렬화 할 수 있는 애들만 할 수 있어요
//직렬화하는 친구들의 조건은 뭘까요?
//using System; 선언
[Serializable]
public class FloatBar
{
    //Field : 멤버변수
    //public안쓰고 [SerializeField] 쓰는 이유
    //변수를 직접 바꾸는걸 막기위해
    //다른 사람이랑 협업 시 너무 많은걸 보여줄 시 방해
    //남들이 쓸 수 있게 필요한 것만 보여주기 위해. 보안상
    [SerializeField] protected float _current;
    [SerializeField] protected float _max;
}
