using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[SerializeField]라고 하는 걸 통해서 직렬화를 하려면!
//직렬화 할 수 있는 애들만 할 수 있어요!
//직렬화하는 친구들의 조건은 뭘까요?
[Serializable]
public class FloatBar
{
    [SerializeField] protected float _current;
    [SerializeField] protected float _max;
}
