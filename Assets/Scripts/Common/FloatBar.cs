using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[SerializeField]��� �ϴ� �� ���ؼ� ����ȭ�� �Ϸ���!
//����ȭ �� �� �ִ� �ֵ鸸 �� �� �־��!
//����ȭ�ϴ� ģ������ ������ �����?
[Serializable]
public class FloatBar
{
    [SerializeField] protected float _current;
    [SerializeField] protected float _max;
}
